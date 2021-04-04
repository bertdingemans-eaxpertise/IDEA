Imports System.Windows.Forms
Imports System.Drawing
Imports TEA.DLAFormfactory


Public Class FrmElementDeduplicator
    Private oRepository As EA.Repository
    Private oElement As EA.Element
    Public Property Repository() As EA.Repository
        Get
            Return oRepository
        End Get
        Set(ByVal value As EA.Repository)
            oRepository = value

        End Set
    End Property
    Private newPropertyValue As String
    Public Property Element() As EA.Element
        Get
            Return oElement
        End Get
        Set(ByVal value As EA.Element)
            oElement = value
            SetElementInformation()
        End Set
    End Property
    Public Sub SetDuplicateValue(duplid As String)
        Me.ComboBoxElements.SelectedValue = duplid
    End Sub
    Private Sub ReloadOriginal()
        Dim objDSOriginals As DataTable

        objDSOriginals = DLA2EAHelper.SQL2DataTable(String.Format("SELECT t_object.object_id, t_object.name, t_object.name  + ' ' +  t_package.name + ' (' + t_object.version + ') by ' + t_object.author as fullname FROM t_object, t_package WHERE t_object.package_id = t_package.package_id and t_object.name LIKE '%{0}%' ORDER BY t_object.name", TextBoxOriginalSearch.Text, oElement.ElementID), Me.Repository)
        If Not IsNothing(objDSOriginals) Then
            Me.ComboBoxOriginals.DataSource = objDSOriginals
            Me.ComboBoxOriginals.DisplayMember = "fullname"
            Me.ComboBoxOriginals.ValueMember = "object_id"
            Me.ComboBoxOriginals.SelectedValue = oElement.ElementID.ToString()
        End If
    End Sub

    Private Sub ReloadDuplicate()
        Dim objDSDuplicates As DataTable

        objDSDuplicates = DLA2EAHelper.SQL2DataTable(String.Format("SELECT t_object.object_id, t_object.name, t_object.name   + ' ' +  t_package.name + ' (' + t_object.version + ') by ' + t_object.author as fullname FROM t_object, t_package WHERE t_object.package_id = t_package.package_id and t_object.name LIKE '%{0}%' ORDER BY t_object.name", TextBoxDuplicateSearch.Text, oElement.ElementID), Me.Repository)
        If Not IsNothing(objDSDuplicates) Then
            Me.ComboBoxElements.DataSource = objDSDuplicates
            Me.ComboBoxElements.DisplayMember = "fullname"
            Me.ComboBoxElements.ValueMember = "object_id"
            If Me.ComboBoxElements.Items.Count > 0 Then
                Me.ComboBoxElements.SelectedIndex = 0
            End If
        End If
    End Sub
    Private Sub SetElementInformation()
        Dim strSearch As String = oElement.Name
        If oElement.Name.IndexOf("[") >= 0 Then
            strSearch = oElement.Name.Substring(0, oElement.Name.IndexOf("["))
        End If
        TextBoxOriginalSearch.Text = strSearch
        TextBoxDuplicateSearch.Text = strSearch
        ReloadOriginal()
        ReloadDuplicate()
    End Sub

    Private Sub ButtonDeDuplicate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonDeDuplicate.Click
        Dim Diagrams As New ArrayList()
        Dim blnAutorized As Boolean = False
        Try
            If Me.Repository.IsSecurityEnabled = False Or DLA2EAHelper.IsUserGroupMember(Me.Repository, "Administrators") Then
                If oElement.Locked Then
                    If Not oElement.ReleaseUserLock() Then
                        MsgBox("You are not the user who locked this element", MsgBoxStyle.OkOnly)
                    Else
                        If oElement.ApplyUserLock() Then
                            oElement.Update()
                            blnAutorized = True
                        Else
                            MsgBox("Locking element failed, no merge possible", MsgBoxStyle.OkOnly)
                        End If
                    End If
                Else
                    blnAutorized = True
                End If
            Else
                MsgBox("You are not authorized to start this function", MsgBoxStyle.OkOnly)
            End If
            While Not IsNothing(Me.Repository.GetCurrentDiagram)
                Diagrams.Add(Me.Repository.GetCurrentDiagram.DiagramID)
                Me.Repository.CloseDiagram(Me.Repository.GetCurrentDiagram.DiagramID)
            End While
            If blnAutorized Then
                If CheckBoxManualDeduplication.Checked Then
                    DeduplicateManual()
                Else
                    DeduplicateDirect()
                End If
                DeleteDuplicate()
            End If
            For Each diagramid As Int32 In Diagrams
                Repository.OpenDiagram(diagramid)
            Next
            Me.Close()
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try

    End Sub
    Sub DeduplicateDirect()
        Dim DeDuplicator As New DeDuplicator()
        DeDuplicator.AllModules = True
        DeDuplicator.Repository = Me.Repository
        DeDuplicator.DeduplicateElement(Me.ComboBoxOriginals.SelectedValue.ToString(), Me.ComboBoxElements.SelectedValue.ToString(), Me.Element.PackageID.ToString(), Me.Element.PackageID.ToString())
    End Sub


    Private Sub DeduplicateManual()
        Try
            oRepository.EnableUIUpdates = False
            Dim oDuplElement As EA.Element
            oDuplElement = Repository.GetElementByID(Me.ComboBoxElements.SelectedValue)
            ' verplaatsen van de duplelementen naar de elementen in de diagrammen
            Dim strDiagramSQL As String()
            strDiagramSQL = {"DELETE FROM t_diagramobjects WHERE object_id = #dupl_id# AND EXISTS(SELECT 1 FROM t_diagramobjects AS DO WHERE DO.object_id = #orig_id# AND DO.diagram_id = t_diagramobjects.diagram_id)  ",
                "UPDATE t_diagramobjects SET object_id = #orig_id# WHERE object_id = #dupl_id# AND NOT EXISTS(SELECT 1 FROM t_diagramobjects AS DO WHERE DO.object_id = #orig_id# AND DO.diagram_id = t_diagramobjects.diagram_id)  "}
            For Each Sql As String In strDiagramSQL
                Sql = Sql.Replace("#orig_id#", Me.oElement.ElementID).Replace("#dupl_id#", oDuplElement.ElementID)
                Me.Repository.Execute(Sql)
            Next
            DeduplicateAttribute(DataGridViewAttrOriginal.Rows, DataGridViewAttrDuplicate.Rows)
            DeduplicateOperation(DataGridViewOprOriginal.Rows, DataGridViewOprDuplicate.Rows)
            DeduplicateConnector(DataGridViewAssOriginal.Rows, DataGridViewAssDuplicate.Rows)
            DeduplicateTaggedValue(DataGridViewTVOriginal.Rows, DataGridViewTVDuplicate.Rows)
            DeduplicateElement()

            oRepository.EnableUIUpdates = True
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
            Me.LabelResult.Text = ex.ToString()
        End Try

    End Sub
    Private Sub DeduplicateAttribute(ByVal original As DataGridViewRowCollection, ByVal duplicate As DataGridViewRowCollection)
        Dim newSQL As String = "UPDATE t_attribute SET object_id = {0} WHERE ID = {1} "
        Dim modifiedSQL As String = "DELETE FROM t_attribute WHERE object_id = {0} AND name= '{2}';UPDATE t_attribute SET object_id = {0} WHERE ID = {1} AND name= '{2}' "
        Dim deleteSQL As String = "DELETE FROM t_attribute WHERE ID = {1} "

        Dim strStatus As String
        Dim oRow As System.Windows.Forms.DataGridViewRow
        For Each oRow In duplicate
            strStatus = oRow.Cells("Sel_status").Value.ToString().ToUpper()
            Dim Sql As String = ""
            Select Case strStatus
                Case "NEW"
                    Sql = String.Format(newSQL, Me.oElement.ElementID, oRow.Cells("ID").Value.ToString(), oRow.Cells("name").Value.ToString())
                Case "MODIFIED"
                    Sql = String.Format(modifiedSQL, Me.oElement.ElementID, oRow.Cells("ID").Value.ToString(), oRow.Cells("name").Value.ToString())
            End Select
            If Sql.Length > 0 Then
                Dim aSql As String() = Sql.Split(";")
                For Each statement In aSql
                    Me.Repository.Execute(statement)
                Next
            End If
        Next
        For Each oRow In original
            strStatus = oRow.Cells("Sel_status").Value.ToString().ToUpper()
            Dim Sql As String = ""
            Select Case strStatus
                Case "DELETE"
                    Sql = String.Format(deleteSQL, Me.oElement.ElementID, oRow.Cells("ID").Value.ToString().ToUpper())
            End Select
            If Sql.Length > 0 Then
                Dim aSql As String() = Sql.Split(";")
                For Each statement In aSql
                    Me.Repository.Execute(statement)
                Next
            End If
        Next
        LoadAttributes()
    End Sub
    Private Sub DeleteDuplicate()
        If Me.CheckBoxDeleteDuplicate.Checked Then
            Dim oPackage As EA.Package
            Dim intTeller As Int16 = 0
            Dim oDuplElement As EA.Element
            oDuplElement = Repository.GetElementByID(Me.ComboBoxElements.SelectedValue)
            oPackage = Me.Repository.GetPackageByID(oDuplElement.PackageID)
            Do While intTeller < oPackage.Elements.Count
                Dim oPkgElement As EA.Element
                oPkgElement = oPackage.Elements(intTeller)
                If oPkgElement.ElementGUID = oDuplElement.ElementGUID Then
                    oPackage.Elements.Delete(intTeller)
                    oPackage.Update()
                    Exit Do
                End If
                intTeller += 1
            Loop
        End If
    End Sub
    Private Sub DeduplicateElement()
        oElement.Notes = Me.OrigNotes.Text
        oElement.Alias = Me.OrigAlias.Text
        oElement.Tag = Me.OrigKeywords.Text
        oElement.Update()
        LoadElements()
    End Sub
    Private Sub DeduplicateTaggedValue(ByVal original As DataGridViewRowCollection, ByVal duplicate As DataGridViewRowCollection)
        Dim newSQL As String = "UPDATE t_objectproperties SET object_id = {0} WHERE propertyID = {1} "
        Dim modifiedSQL As String = "DELETE FROM t_objectproperties WHERE object_id = {0} AND property= '{2}';UPDATE t_objectproperties SET object_id = {0} WHERE propertyID = {1} AND type= '{2}' "
        Dim deleteSQL As String = "DELETE FROM t_objectproperties WHERE propertyID = {1} "

        Dim strStatus As String
        Dim oRow As System.Windows.Forms.DataGridViewRow
        For Each oRow In duplicate
            strStatus = oRow.Cells("Sel_status").Value.ToString().ToUpper()
            Dim Sql As String = ""
            Select Case strStatus
                Case "NEW"
                    Sql = String.Format(newSQL, Me.oElement.ElementID, oRow.Cells("ID").Value.ToString(), oRow.Cells("name").Value.ToString())
                Case "MODIFIED"
                    Sql = String.Format(modifiedSQL, Me.oElement.ElementID, oRow.Cells("ID").Value.ToString(), oRow.Cells("name").Value.ToString())
            End Select
            If Sql.Length > 0 Then
                Dim aSql As String() = Sql.Split(";")
                For Each statement In aSql
                    Me.Repository.Execute(statement)
                Next
            End If
        Next
        For Each oRow In original
            strStatus = oRow.Cells("Sel_status").Value.ToString().ToUpper()
            Dim Sql As String = ""
            Select Case strStatus
                Case "DELETE"
                    Sql = String.Format(deleteSQL, Me.oElement.ElementID, oRow.Cells("ID").Value.ToString().ToUpper())
            End Select
            If Sql.Length > 0 Then
                Dim aSql As String() = Sql.Split(";")
                For Each statement In aSql
                    Me.Repository.Execute(statement)
                Next
            End If
        Next
        LoadTaggedValues()
    End Sub
    Private Sub DeduplicateOperation(ByVal original As DataGridViewRowCollection, ByVal duplicate As DataGridViewRowCollection)
        Dim newSQL As String = "UPDATE t_operation SET object_id = {0} WHERE operationID = {1} "
        Dim modifiedSQL As String = "DELETE FROM t_operation WHERE object_id = {0} AND name= '{2}';UPDATE t_operation SET object_id = {0} WHERE operationID = {1} AND name= '{2}' "
        Dim deleteSQL As String = "DELETE FROM t_operation WHERE operationID = {1} "

        Dim strStatus As String
        Dim oRow As System.Windows.Forms.DataGridViewRow
        For Each oRow In duplicate
            strStatus = oRow.Cells("Sel_status").Value.ToString().ToUpper()
            Dim Sql As String = ""
            Select Case strStatus
                Case "NEW"
                    Sql = String.Format(newSQL, Me.oElement.ElementID, oRow.Cells("ID").Value.ToString(), oRow.Cells("name").Value.ToString())
                Case "MODIFIED"
                    Sql = String.Format(modifiedSQL, Me.oElement.ElementID, oRow.Cells("ID").Value.ToString(), oRow.Cells("name").Value.ToString())
            End Select
            If Sql.Length > 0 Then
                Dim aSql As String() = Sql.Split(";")
                For Each statement In aSql
                    Me.Repository.Execute(statement)
                Next
            End If
        Next
        For Each oRow In original
            strStatus = oRow.Cells("Sel_status").Value.ToString().ToUpper()
            Dim Sql As String = ""
            Select Case strStatus
                Case "DELETE"
                    Sql = String.Format(deleteSQL, Me.oElement.ElementID, oRow.Cells("ID").Value.ToString().ToUpper())
            End Select
            If Sql.Length > 0 Then
                Dim aSql As String() = Sql.Split(";")
                For Each statement In aSql
                    Me.Repository.Execute(statement)
                Next
            End If
        Next
        LoadOperations()
    End Sub
    Private Sub DeduplicateConnector(ByVal original As DataGridViewRowCollection, ByVal duplicate As DataGridViewRowCollection)
        Dim newSQL As String = "UPDATE t_connector SET start_object_id = {0} WHERE connector_ID = {1} AND start_object_id = {3};UPDATE t_connector SET end_object_id = {0} WHERE connector_ID = {1} AND end_object_id = {3}  "
        Dim modifiedSQL As String = "DELETE FROM t_connector WHERE start_object_id = {0} AND name= '{2}';UPDATE t_connector SET start_object_id = {0} WHERE connector_ID = {1} AND name= '{2}' AND start_object_id = {3};DELETE FROM t_connector WHERE end_object_id = {0} AND name= '{2}';UPDATE t_connector SET end_object_id = {0} WHERE connector_ID = {1} AND name= '{2}'  AND end_object_id = {3} "
        Dim deleteSQL As String = "DELETE FROM t_connector WHERE connector_ID = {1} "

        Dim strStatus As String
        Dim oRow As System.Windows.Forms.DataGridViewRow
        For Each oRow In duplicate
            strStatus = oRow.Cells("Sel_status").Value.ToString().ToUpper()
            Dim Sql As String = ""
            Select Case strStatus
                Case "NEW"
                    Sql = String.Format(newSQL, Me.oElement.ElementID, oRow.Cells("ID").Value.ToString(), oRow.Cells("name").Value.ToString(), Me.ComboBoxElements.SelectedValue)
                    'MsgBox("New" + Sql)
                Case "MODIFIED"
                    Sql = String.Format(modifiedSQL, Me.oElement.ElementID, oRow.Cells("ID").Value.ToString(), oRow.Cells("name").Value.ToString(), Me.ComboBoxElements.SelectedValue)
                    'MsgBox("modified " + Sql)

            End Select
            If Sql.Length > 0 Then
                Dim aSql As String() = Sql.Split(";")
                For Each statement In aSql
                    Me.Repository.Execute(statement)
                Next
            End If
        Next
        For Each oRow In original
            strStatus = oRow.Cells("Sel_status").Value.ToString().ToUpper()
            Dim Sql As String = ""
            Select Case strStatus
                Case "DELETE"
                    Sql = String.Format(deleteSQL, Me.oElement.ElementID, oRow.Cells("ID").Value.ToString().ToUpper())
            End Select
            If Sql.Length > 0 Then
                Dim aSql As String() = Sql.Split(";")
                For Each statement In aSql
                    Me.Repository.Execute(statement)
                Next
            End If
        Next
        LoadAssociations()
    End Sub

    Private Sub ColorGrid(ByVal Grid As DataGridView)

        Dim strStatus As String
        Dim oRow As System.Windows.Forms.DataGridViewRow
        For Each oRow In Grid.Rows
            strStatus = oRow.Cells("Dupl_status").Value.ToString().ToUpper()
            Select Case strStatus
                Case "EQUAL"
                    oRow.DefaultCellStyle.BackColor = Color.LightGreen
                Case "NEW"
                    oRow.DefaultCellStyle.BackColor = Color.LightSlateGray
                Case "DELETE"
                    oRow.DefaultCellStyle.BackColor = Color.LightBlue
                Case "MODIFIED"
                    oRow.DefaultCellStyle.BackColor = Color.Yellow
            End Select
        Next

    End Sub
    Private Sub ButtonLoadGrid_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonLoadGrid.Click
        If Not String.IsNullOrEmpty(Me.ComboBoxElements.SelectedValue) Then
            Me.oElement = Repository.GetElementByID(Me.ComboBoxOriginals.SelectedValue)
            LoadElements()
            LoadAttributes()
            LoadOperations()
            LoadAssociations()
            LoadTaggedValues()
            Me.TabControlDuplicator.SelectTab(0)
            'Me.ButtonDeDuplicate.Enabled = True
        End If
    End Sub
    Public Function CompareDataTable(ByVal Original As DataTable, ByVal Duplicate As DataTable) As String
        Dim DuplRow As DataRow
        Dim OrigRow As DataRow
        Dim strRet As String = ""
        Dim oDupl As DeduplCompareItem
        Dim oOrig As DeduplCompareItem
        Try
            If Not IsNothing(Duplicate) And Not IsNothing(Original) Then
                For Each DuplRow In Duplicate.Rows
                    strRet = "New"
                    oDupl = Me.RowCompare2String(DuplRow)
                    For Each OrigRow In Original.Rows
                        oOrig = Me.RowCompare2String(OrigRow)
                        If oOrig.Name = oDupl.Name Then
                            strRet = "Modified"
                            If oOrig.Total = oDupl.Total Then
                                strRet = "Equal"
                                Exit For
                            End If
                        End If
                    Next
                    DuplRow.Item("Dupl_status") = strRet
                Next
                For Each OrigRow In Original.Rows
                    If Me.CheckBoxMerge.Checked Then
                        strRet = "Equal"
                    Else
                        strRet = "Delete"
                    End If
                    oOrig = Me.RowCompare2String(OrigRow)
                    For Each DuplRow In Duplicate.Rows
                        oDupl = Me.RowCompare2String(DuplRow)
                        If oOrig.Name = oDupl.Name Then
                            strRet = "Modified"
                            If oDupl.Total = oOrig.Total Then
                                strRet = "Equal"
                                Exit For
                            End If
                        End If
                    Next
                    OrigRow.Item("Dupl_status") = strRet
                Next
            End If
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try

        Return strRet
    End Function

    Public Function RowCompare2String(ByVal Row As DataRow) As DeduplCompareItem
        Dim oRet As New DeduplCompareItem()
        Dim oColumn As DataColumn
        Dim oColumns As DataColumnCollection
        oColumns = Row.Table.Columns
        For Each oColumn In oColumns
            If oColumn.ColumnName.ToUpper() <> "ID" And oColumn.ColumnName.ToUpper() <> "DUPL_STATUS" Then
                oRet.Total += Row.Item(oColumn.ColumnName).ToString() + ";"
            End If
            If oColumn.ColumnName.ToUpper() = "NAME" Then
                oRet.Name = Row.Item(oColumn.ColumnName).ToString()
            End If
        Next

        Return oRet
    End Function
    Sub LoadAttributes()
        Dim strSql As String = "select [name], [type], [length], [precision], [scale], [notes], null as Dupl_status, [id] from t_attribute where object_id = {0} order by [pos], [name], [type]"
        Dim oDT, dDT As DataTable

        Try
            Me.TabControlDuplicator.SelectTab("TabPageAttributes")

            oDT = DLA2EAHelper.EAString2DataTable(Me.Repository.SQLQuery(String.Format(strSql, Me.oElement.ElementID)))
            If Not IsNothing(oDT) Then
                Me.DataGridViewAttrOriginal.DataSource = oDT
            End If
            dDT = DLA2EAHelper.EAString2DataTable(Me.Repository.SQLQuery(String.Format(strSql, Me.ComboBoxElements.SelectedValue)))
            If Not IsNothing(dDT) Then
                Me.DataGridViewAttrDuplicate.DataSource = dDT
            End If
            Me.CompareDataTable(Me.DataGridViewAttrOriginal.DataSource, Me.DataGridViewAttrDuplicate.DataSource)
            LoadGridDuplStatus(Me.DataGridViewAttrOriginal)
            LoadGridDuplStatus(Me.DataGridViewAttrDuplicate)
            ColorGrid(Me.DataGridViewAttrOriginal)
            ColorGrid(Me.DataGridViewAttrDuplicate)
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub
    Sub LoadElements()

        Try
            Me.TabControlDuplicator.SelectTab("TabPageObject")
            Me.OrigAlias.Text = Me.Element.Alias
            Me.OrigKeywords.Text = Me.Element.Tag
            Me.OrigNotes.Text = Me.Element.Notes

            Dim DuplElement As EA.Element
            DuplElement = Repository.GetElementByID(Me.ComboBoxElements.SelectedValue)
            Me.DuplAlias.Text = DuplElement.Alias
            Me.DuplKeywords.Text = DuplElement.Tag
            Me.DuplNotes.Text = DuplElement.Notes

            If Me.OrigNotes.Text = Me.DuplNotes.Text Then
                Me.OrigNotes.BackColor = Color.LightGreen
                Me.DuplNotes.BackColor = Color.LightGreen
            Else
                Me.OrigNotes.BackColor = Color.Yellow
                Me.DuplNotes.BackColor = Color.Yellow
            End If
            If Me.OrigAlias.Text = Me.DuplAlias.Text Then
                Me.OrigAlias.BackColor = Color.LightGreen
                Me.DuplAlias.BackColor = Color.LightGreen
            Else
                Me.OrigAlias.BackColor = Color.Yellow
                Me.DuplAlias.BackColor = Color.Yellow

            End If
            If Me.OrigKeywords.Text = Me.DuplKeywords.Text Then
                Me.OrigKeywords.BackColor = Color.LightGreen
                Me.DuplKeywords.BackColor = Color.LightGreen
            Else
                Me.OrigKeywords.BackColor = Color.Yellow
                Me.DuplKeywords.BackColor = Color.Yellow
            End If
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub
    Sub LoadTaggedValues()
        Dim strSql As String = "select property as [name], [value], [notes], null as Dupl_status, propertyid as [id] from t_objectproperties where object_id = {0} order by 1 "
        Dim oDS As DataTable
        Dim dDS As DataTable
        Try
            Me.TabControlDuplicator.SelectTab("TabPageTaggedValues")

            oDS = DLA2EAHelper.EAString2DataTable(Me.Repository.SQLQuery(String.Format(strSql, Me.oElement.ElementID)))
            Me.DataGridViewTVOriginal.DataSource = oDS
            dDS = DLA2EAHelper.EAString2DataTable(Me.Repository.SQLQuery(String.Format(strSql, Me.ComboBoxElements.SelectedValue)))
            Me.DataGridViewTVDuplicate.DataSource = dDS
            Me.CompareDataTable(Me.DataGridViewTVOriginal.DataSource, Me.DataGridViewTVDuplicate.DataSource)
            LoadGridDuplStatus(Me.DataGridViewTVOriginal)
            LoadGridDuplStatus(Me.DataGridViewTVDuplicate)
            ColorGrid(Me.DataGridViewTVOriginal)
            ColorGrid(Me.DataGridViewTVDuplicate)
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub
    Public Sub LoadGridDuplStatus(ByVal Grid As DataGridView)

        Dim intTeller As Integer
        Dim row As DataGridViewRow
        Dim cmb As New DataGridViewComboBoxColumn()
        cmb.HeaderText = "Selected status"
        cmb.ToolTipText = "Display the duplicate status"
        cmb.Name = "Sel_status"
        cmb.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
        cmb.DisplayStyleForCurrentCellOnly = True
        cmb.MaxDropDownItems = 2
        cmb.Items.Add("Equal")
        cmb.Items.Add("New")
        cmb.Items.Add("Modified")
        cmb.Items.Add("Delete")
        Grid.Columns.Add(cmb)
        intTeller = 0
        While intTeller <= Grid.Rows.Count - 1
            row = Grid.Rows(intTeller)
            Dim duplCell As DataGridViewComboBoxCell = CType((row.Cells("Sel_status")), DataGridViewComboBoxCell)
            duplCell.Value = row.Cells("Dupl_status").Value
            intTeller += 1
        End While
        If intTeller > 0 Then
            Grid.Columns("Dupl_status").Visible = False
            Grid.Columns("id").Visible = False
        End If

    End Sub

    Sub LoadAssociations()
        Dim strSql As String =
            "select  t_connector.name as name, t_connector.direction, t_connector.Connector_type, dest.name as Linked_Element , t_connector.SourceCard, t_connector.DestCard, t_connector.notes, null as Dupl_status, connector_id as id " +
            " from t_connector, t_object as source, t_object as dest where t_connector.Start_Object_ID = source.object_id and t_connector.End_Object_ID = dest.object_id" +
            " and t_connector.Start_Object_ID = {0} and Connector_type <> 'Notelink' " +
            " and t_connector.name is not null " +
            " union all select t_connector.name as name, t_connector.direction, t_connector.Connector_type, source.name as Linked_Element , t_connector.SourceCard, t_connector.DestCard, t_connector.notes, null as Dupl_status, connector_id as id " +
            " from t_connector, t_object as source, t_object as dest where t_connector.Start_Object_ID = source.object_id and t_connector.End_Object_ID = dest.object_id" +
            " and t_connector.End_Object_ID = {0} and Connector_type <> 'Notelink' " +
            " and t_connector.name is not null " +
            " union all select  t_connector.stereotype as name, t_connector.direction, t_connector.Connector_type, dest.name as Linked_Element , t_connector.SourceCard, t_connector.DestCard, t_connector.notes, null as Dupl_status, connector_id as id " +
            " from t_connector, t_object as source, t_object as dest where t_connector.Start_Object_ID = source.object_id and t_connector.End_Object_ID = dest.object_id" +
            " and t_connector.Start_Object_ID = {0} and Connector_type <> 'Notelink' " +
            " and t_connector.name is  null " +
            " union all select t_connector.stereotype as name, t_connector.direction, t_connector.Connector_type, source.name as Linked_Element , t_connector.SourceCard, t_connector.DestCard, t_connector.notes, null as Dupl_status, connector_id as id " +
            " from t_connector, t_object as source, t_object as dest where t_connector.Start_Object_ID = source.object_id and t_connector.End_Object_ID = dest.object_id" +
            " and t_connector.End_Object_ID = {0} and Connector_type <> 'Notelink'  " +
            " and t_connector.name is null " +
            "ORDER BY 1,2,3 "


        Dim oDT As DataTable
        Dim dDT As DataTable
        Try
            Me.TabControlDuplicator.SelectTab("TabPageAssociations")
            oDT = DLA2EAHelper.EAString2DataTable(Me.Repository.SQLQuery(String.Format(strSql, Me.oElement.ElementID)))
            Me.DataGridViewAssOriginal.DataSource = oDT
            dDT = DLA2EAHelper.EAString2DataTable(Me.Repository.SQLQuery(String.Format(strSql, Me.ComboBoxElements.SelectedValue)))
            Me.DataGridViewAssDuplicate.DataSource = dDT
            Me.CompareDataTable(Me.DataGridViewAssOriginal.DataSource, Me.DataGridViewAssDuplicate.DataSource)
            LoadGridDuplStatus(Me.DataGridViewAssOriginal)
            LoadGridDuplStatus(Me.DataGridViewAssDuplicate)
            ColorGrid(Me.DataGridViewAssOriginal)
            ColorGrid(Me.DataGridViewAssDuplicate)

        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub
    Sub LoadOperations()
        Dim strSql As String = "select oper.name, oper.stereotype, oper.notes, null as Dupl_status, oper.operationid as id from t_operation oper where oper.object_id = {0} order by [name], [stereotype] "
        Dim oDT As DataTable
        Dim dDT As DataTable
        Try
            If Repository.RepositoryType = "SQLSVR" Then
                'als het sql server is komt er een sql statement met een aggregate van de parameters
                strSql = "select oper.name, oper.stereotype, oper.notes, null as Dupl_status, oper.operationid as id, STRING_AGG(parm.name, ', ')  as parameters "
                strSql += "From t_operation oper , t_operationparams parm "
                strSql += "where parm.OperationID = oper.OperationID AND oper.Object_ID = {0} group by oper.name, oper.stereotype, oper.notes, oper.operationid, oper.pos order by oper.pos, oper.name "
            End If

            Me.TabControlDuplicator.SelectTab("TabPageOperations")
            oDT = DLA2EAHelper.EAString2DataTable(Me.Repository.SQLQuery(String.Format(strSql, Me.oElement.ElementID)))
            If Not IsNothing(oDT) Then
                Me.DataGridViewOprOriginal.DataSource = oDT
            End If
            dDT = DLA2EAHelper.EAString2DataTable(Me.Repository.SQLQuery(String.Format(strSql, Me.ComboBoxElements.SelectedValue)))
            If Not IsNothing(dDT) Then
                Me.DataGridViewOprDuplicate.DataSource = dDT
            End If
            Me.CompareDataTable(Me.DataGridViewOprOriginal.DataSource, Me.DataGridViewOprDuplicate.DataSource)
            LoadGridDuplStatus(Me.DataGridViewOprOriginal)
            LoadGridDuplStatus(Me.DataGridViewOprDuplicate)
            ColorGrid(Me.DataGridViewOprOriginal)
            ColorGrid(Me.DataGridViewOprDuplicate)

        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub

    Private Sub ButtonSplitOrientation_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonSplitOrientation.Click
        Dim ori As Orientation = Orientation.Vertical

        If Me.SplitContainer1.Orientation = Orientation.Vertical Then
            ori = Orientation.Horizontal
        End If
        Me.SplitContainer1.Orientation = ori
        Me.SplitContainer2.Orientation = ori
        Me.SplitContainer3.Orientation = ori
        Me.SplitContainer4.Orientation = ori
        Me.SplitContainer5.Orientation = ori
    End Sub

    Private Sub ButtonAliasMove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonAliasMove.Click
        Me.OrigAlias.Text = Me.DuplAlias.Text

    End Sub

    Private Sub ButtonKeywordsMove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonKeywordsMove.Click
        Me.OrigKeywords.Text = Me.DuplKeywords.Text

    End Sub

    Private Sub ButtonNotesMove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonNotesMove.Click
        Me.OrigNotes.Text = Me.DuplNotes.Text

    End Sub

    Private Sub ButtonAllMove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonAllMove.Click
        Me.OrigAlias.Text = Me.DuplAlias.Text
        Me.OrigKeywords.Text = Me.DuplKeywords.Text
        Me.OrigNotes.Text = Me.DuplNotes.Text
    End Sub
    Private Sub ButtonReloadOriginal_Click(sender As Object, e As EventArgs) Handles ButtonReloadOriginal.Click
        ReloadOriginal()
    End Sub

    Private Sub ButtonReloadDuplicate_Click(sender As Object, e As EventArgs) Handles ButtonReloadDuplicate.Click
        ReloadDuplicate()
    End Sub

    Private Sub CheckBoxManualDeduplication_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxManualDeduplication.CheckedChanged
        If Me.CheckBoxManualDeduplication.Checked Then
            Me.TabControlDuplicator.Enabled = True
        Else
            Me.TabControlDuplicator.Enabled = False
        End If
    End Sub

    Private Sub FrmElementDeduplicator_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.TabControlDuplicator.Enabled = False
    End Sub

    Private Sub TextBoxDuplicateSearch_TextChanged(sender As Object, e As EventArgs) Handles TextBoxDuplicateSearch.TextChanged
        Me.ReloadDuplicate()
    End Sub

    Private Sub TextBoxOriginalSearch_TextChanged(sender As Object, e As EventArgs) Handles TextBoxOriginalSearch.TextChanged
        Me.ReloadOriginal()
    End Sub
End Class
''' <summary>
''' Class for comparing the elements in the deduplicator routine, it has two types
''' of compare properties, one for the name and one for the total of an element.
''' </summary>
Public Class DeduplCompareItem
    ''' <summary>
    ''' Property for comparing the total of two elements of the same EA type.
    ''' </summary>
    Public Total As String = ""
    ''' <summary>
    ''' Name element for doing the compare. The difference between total and name is an
    ''' update or an insert in the deduplicator screen.
    ''' </summary>
    Public Name As String = ""
End Class