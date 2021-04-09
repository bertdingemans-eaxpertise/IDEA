Imports System.Windows.Forms
Imports System.Drawing
Imports TEA.DLAFormfactory

''' <summary>
''' Form for IDEA routines specific for diagram entities. For every type of element
''' a specific form is generated. This makes working with the IDEA AddOn easier.
''' </summary>
Public Class FrmIdeaDiagram
    Private _Repository As EA.Repository
    Private strTargetIds As String = ""
    Private tblSource As DataTable
    Private tblTarget As DataTable

    Public Property Repository() As EA.Repository
        Get
            Return _Repository
        End Get
        Set(ByVal value As EA.Repository)
            _Repository = value
        End Set
    End Property

    Private _Diagram As EA.Diagram
    Public Property Diagram() As EA.Diagram
        Get
            Return _Diagram
        End Get
        Set(ByVal value As EA.Diagram)
            _Diagram = value
            Me.Text = Me.Text + " " + _Diagram.Name
            Me.LabelDiagramName.Text = _Diagram.Name
            Me.LabelMappingDiagramName.Text = _Diagram.Name
        End Set
    End Property
    Public Function LoadElements() As Boolean
        Dim objDT As DataTable
        Dim strSql As String

        Try
            objDT = DLA2EAHelper.SQL2DataTable("SELECT package_id, name FROM t_package ORDER BY name", Me.Repository)
            If Not IsNothing(objDT) Then
                Me.ComboBoxTargetPackage.DataSource = objDT
                Me.ComboBoxTargetPackage.DisplayMember = "name"
                Me.ComboBoxTargetPackage.ValueMember = "package_id"
                Me.ComboBoxTargetPackage.SelectedValue = Me.Diagram.PackageID
            End If

            strSql = String.Format("Select t_object.Object_ID as id, t_object.name from t_object, t_diagramobjects where t_object.object_type = 'Class' and t_object.Object_ID = t_diagramobjects.Object_ID and t_diagramobjects.Diagram_ID = {0} Order by t_object.Name", Me.Diagram.DiagramID)
            objDT = DLA2EAHelper.SQL2DataTable(strSql, Me.Repository)
            If objDT.Rows.Count > 0 Then
                Me.ListBoxElements.DataSource = objDT
                Me.ListBoxElements.DisplayMember = "name"
                Me.ListBoxElements.ValueMember = "id"

                Me.ListBoxMappingElement.DataSource = objDT
                Me.ListBoxMappingElement.DisplayMember = "name"
                Me.ListBoxMappingElement.ValueMember = "id"
                CreateMappingGrid()
            Else
                'MsgBox("No elements on diagram", MsgBoxStyle.OkOnly)
                'Return False
            End If
            Return True
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
        Return False
    End Function
    Private Sub SelectDeSelectMapping(ByVal state As Boolean)
        Dim i As Int16 = 0
        While i < ListBoxMappingElement.Items.Count
            ListBoxMappingElement.SetItemChecked(i, state)
            i += 1
        End While
    End Sub

    Private Sub SelectDeSelect(ByVal state As Boolean)
        Dim i As Int16 = 0
        While i < ListBoxElements.Items.Count
            ListBoxElements.SetItemChecked(i, state)
            i += 1
        End While
    End Sub

    Private Sub SelectToggle()
        Dim i As Int16 = 0
        While i < ListBoxElements.Items.Count
            ListBoxElements.SetItemChecked(i, (Not ListBoxElements.GetItemChecked(i)))
            i += 1
        End While
    End Sub

    Private Sub ButtonSelectAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonSelectAll.Click
        SelectDeSelect(True)
    End Sub
    Private Sub ButtonUnselectAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonUnselectAll.Click
        SelectDeSelect(False)
    End Sub

    Private Sub ButtonToggleAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonToggleAll.Click
        SelectToggle()
    End Sub

    Private Sub CreateMappingGrid()
        Me.ButtonMappingTarget.Enabled = False
        Me.DataGridViewMapping.Columns.Clear()

        'Define the layout of the grid
        With DataGridViewMapping.ColumnHeadersDefaultCellStyle
            .BackColor = Color.Navy
            .ForeColor = Color.White
            .SelectionBackColor = Color.LightGreen
            .SelectionForeColor = Color.Black
            .Font = New Font(DataGridViewMapping.Font, FontStyle.Bold)
        End With

        'Create the combobox for the mapper column in the grid
        Dim cmb As New DataGridViewComboBoxColumn()
        cmb.HeaderText = "Merger/Splitter"
        cmb.ToolTipText = "Merge or Split one or more attributes"
        cmb.Name = "Merger"
        cmb.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
        cmb.DisplayStyleForCurrentCellOnly = True
        cmb.MaxDropDownItems = 2
        cmb.Items.Add("Merger")
        cmb.Items.Add("Splitter")
        cmb.Items.Add("No")
        'add the mapper column to the grid
        Me.DataGridViewMapping.Columns.Add(cmb)

    End Sub
    Private Sub ButtonMappingSource_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonMappingSource.Click
        'when we load the source column we reset the rows
        Try
            Me.DataGridViewMapping.Rows.Clear()
            Me.CreateMappingGrid()
            CreateComboBox("Source", "Select the correct source attribute", "source_attributeid", True)
            Me.ButtonMappingTarget.Enabled = True
            Me.ButtonMappingSource.Enabled = False
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub

    Private Sub ButtonMappingTarget_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonMappingTarget.Click
        Try
            CreateComboBox("Target", "Select the correct target attribute", "target_attributeid", False)
            Me.ButtonMappingTarget.Enabled = False
            Me.ButtonMatchNames.Enabled = True
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub
    Private Sub DataGridView1_DataError(ByVal sender As Object, ByVal e As DataGridViewDataErrorEventArgs) Handles DataGridViewMapping.DataError
        'deze laten staan anders krijgen we mogelijk foutmeldingen vanuit het besturingsysteem

    End Sub
    Private Sub CreateComboBox(ByVal header As String, ByVal tooltip As String, ByVal name As String, ByVal isSource As Boolean)
        'extend the grid with the target attributes in a combobox
        Dim item As DataRowView
        Dim cmb As New DataGridViewComboBoxColumn()

        cmb.HeaderText = header
        cmb.ToolTipText = tooltip
        cmb.Name = name
        cmb.AutoComplete = True
        cmb.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
        cmb.DisplayStyleForCurrentCellOnly = True
        cmb.MaxDropDownItems = 10
        'cmb.DefaultCellStyle = DataGridViewMapping.Columns(0).DefaultCellStyle
        Dim strElements As String = "-999"
        ' this we need for the IN element in the SQL statement
        For Each item In Me.ListBoxMappingElement.CheckedItems
            strElements += ", " + item("id")
        Next

        Dim objDT As DataTable
        Dim strSql As String
        strSql = String.Format("SELECT t_attribute.ea_guid as attribute_id, t_attribute.name + ' (' + t_object.name + ')' as attrfullname, t_object.name as object_name, t_attribute.name as attribute_name FROM t_attribute, t_object WHERE t_attribute.object_id = t_object.object_id AND t_object.object_id IN( {0} ) ORDER BY 3, 2", strElements)
        strSql = DLA2EAHelper.SQLForEAP(strSql, Me.Repository)
        objDT = DLA2EAHelper.SQL2DataTable(strSql, Me.Repository)

        If objDT.Rows.Count > 0 Then
            If (isSource = True And CheckBoxMapTarget.Checked = False) Or (isSource = False And CheckBoxMapTarget.Checked = True) Then
                Me.DataGridViewMapping.Rows.Add(objDT.Rows.Count)
            End If
            cmb.DataSource = objDT
            cmb.DisplayMember = "attrfullname"
            cmb.ValueMember = "attribute_id"
            cmb.Width = 300
            If isSource Then
                Me.DataGridViewMapping.Columns.Insert(0, cmb)
            Else
                Me.DataGridViewMapping.Columns.Add(cmb)
            End If
            If isSource Then
                Me.tblSource = objDT
            Else
                Me.tblTarget = objDT
            End If
            SelectDeSelectMapping(False)

            'laden van de data
            If CheckBoxLoadData.Checked Then
                If (isSource = True And CheckBoxMapTarget.Checked = False) Or (isSource = False And CheckBoxMapTarget.Checked = True) Then
                    Dim intTeller As Int16 = 0
                    Dim row As DataGridViewRow
                    While intTeller < DataGridViewMapping.Rows.Count - 1
                        row = DataGridViewMapping.Rows(intTeller)
                        Dim comboBoxCell As DataGridViewComboBoxCell = CType((row.Cells(name)), DataGridViewComboBoxCell)
                        comboBoxCell.Value = objDT.Rows(intTeller).Item("attribute_id")
                        Dim mergerCell As DataGridViewComboBoxCell = CType((row.Cells("Merger")), DataGridViewComboBoxCell)
                        mergerCell.Value = "No"
                        intTeller += 1
                    End While
                Else
                    Me.strTargetIds = ""
                    Dim objDR As DataRow
                    For Each objDR In objDT.Rows
                        strTargetIds += objDR.Item("attribute_id") + ", "
                    Next
                End If
            End If
        End If
    End Sub

    Private Sub ButtonGenerateMapping_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonGenerateMapping.Click
        Dim objRow As DataGridViewRow
        Dim objGenerator As New IDEAGenerator()
        Dim arrSleutels As New List(Of String())
        Dim objSource As EA.Attribute
        Dim objTarget As EA.Attribute
        Dim objMerger As EA.Element
        Try
            objGenerator.Repository = Me.Repository
            For Each objRow In DataGridViewMapping.Rows
                If Not String.IsNullOrEmpty(objRow.Cells("target_attributeid").Value) And Not String.IsNullOrEmpty(objRow.Cells("source_attributeid").Value) And objRow.ReadOnly = False Then
                    If objRow.Cells("Merger").Value = "Merger" Then
                        'when it is a merger first generate a list of keys for later processing
                        arrSleutels.Add({objRow.Cells("target_attributeid").Value, objRow.Cells("source_attributeid").Value, objRow.Cells("Merger").Value})
                    ElseIf objRow.Cells("Merger").Value = "Splitter" Then
                        'when it is a merger first generate a list of keys for later processing
                        arrSleutels.Add({objRow.Cells("source_attributeid").Value, objRow.Cells("target_attributeid").Value, objRow.Cells("Merger").Value})
                    Else
                        'when it is a regular mapping create an association for the attributes
                        objSource = Repository.GetAttributeByGuid(objRow.Cells("source_attributeid").Value)
                        objTarget = Me.Repository.GetAttributeByGuid(objRow.Cells("target_attributeid").Value)
                        objGenerator.CreateConnectorForAttribute(objTarget, objSource, "Source -> Destination", Me.CheckBoxGenerateName.Checked, Me.TextBoxStereoType.Text)
                    End If
                End If
            Next
            Dim strItem As String()
            For Each strItem In arrSleutels
                'for all the collected keys create mergers (one or more depending on the selection
                objTarget = Me.Repository.GetAttributeByGuid(strItem(0))
                objSource = Me.Repository.GetAttributeByGuid(strItem(1))
                Dim objPackage As EA.Package
                Dim objEleInPackage As EA.Element
                Dim blnFound As Boolean = False
                objPackage = Me.Repository.GetPackageByID(Me.Diagram.PackageID)
                For Each objEleInPackage In objPackage.Elements
                    'searh for mergers
                    If objEleInPackage.Alias = objTarget.AttributeGUID Then
                        objMerger = objEleInPackage
                        blnFound = True
                        Exit For
                    End If
                Next
                If blnFound = False Then
                    'if the merger is not found then create it
                    objMerger = objGenerator.CreateElement(strItem(2) + " " + objTarget.Name, "Class", Me.Diagram.PackageID)
                    objMerger.Alias = objTarget.AttributeGUID
                    objMerger.Update()

                    objGenerator.CopyAttribute(Me.Repository.GetElementByID(objTarget.ParentID), objMerger, objTarget.Type, Repository.GetAttributeByGuid(strItem(0)).AttributeID, True,
                                               IIf(strItem(2) <> "Merger", "Destination -> Source", "Source -> Destination"))
                    Dim objDO As EA.DiagramObject
                    objDO = Me.Diagram.DiagramObjects.AddNew(objMerger.Name, "")
                    objDO.ElementID = objMerger.ElementID
                    objDO.top = 200
                    objDO.left = 200
                    objDO.Update()
                End If
                'the merger is created or found so we gonna connect the attributes to it
                'Omdat we met guids werken een ingewikkelder aanroep van het id
                objGenerator.CopyAttribute(Me.Repository.GetElementByID(objSource.ParentID), objMerger, objSource.Type, Repository.GetAttributeByGuid(strItem(1)).AttributeID, True,
                                           IIf(strItem(2) = "Merger", "Destination -> Source", "Source -> Destination"))
                objMerger.Update()
            Next
            Me.Repository.ReloadDiagram(Me.Diagram.DiagramID)
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub
    Private Sub ButtonLoadMappings_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonLoadMappings.Click
        Dim strSourceID As String
        Dim strLinks As String
        Dim objDL As EA.DiagramLink
        Dim intTeller As Int16
        'load the existing mappings in the diagram and make them readonly
        Try
            For intTeller = 0 To DataGridViewMapping.Rows.Count - 1
                If CheckBoxMapTarget.Checked Then
                    strSourceID = DataGridViewMapping.Rows(intTeller).Cells("target_attributeid").Value
                Else
                    strSourceID = DataGridViewMapping.Rows(intTeller).Cells("source_attributeid").Value
                End If
                For Each objDL In Me.Diagram.DiagramLinks
                    'since the mappings are stored in a strange way in EA we have to do some processing in the styleex field to retrieve the correct association
                    strLinks = Me.Repository.GetConnectorByID(objDL.ConnectorID).StyleEx
                    strLinks = strLinks.Replace("LFEP=", "").Replace("LFSP=", "").Replace("L;", "R;")
                    Dim arrLinks As String()
                    arrLinks = strLinks.Split("R;")
                    If arrLinks.Length > 1 Then
                        Dim strKey As String = ""
                        Dim cmb As DataGridViewComboBoxCell = CType(DataGridViewMapping.Rows(intTeller).Cells(IIf(CheckBoxMapTarget.Checked, "source_attributeid", "target_attributeid")), DataGridViewComboBoxCell)
                        If arrLinks(0).Contains(strSourceID) Then
                            strKey = arrLinks(1).Replace(";", "").Trim()
                            If Me.strTargetIds.IndexOf(strKey) >= 0 Then
                                cmb.Value = strKey
                                DataGridViewMapping.Rows(intTeller).ReadOnly = True
                                DataGridViewMapping.Rows(intTeller).DefaultCellStyle.BackColor = Color.LightGray
                            End If

                        End If
                        If arrLinks(1).Contains(strSourceID) Then
                            strKey = arrLinks(0).Replace(";", "").Trim()
                            If Me.strTargetIds.IndexOf(strKey) >= 0 Then
                                cmb.Value = strKey
                                DataGridViewMapping.Rows(intTeller).ReadOnly = True
                                DataGridViewMapping.Rows(intTeller).DefaultCellStyle.BackColor = Color.LightGray
                            End If
                        End If
                    End If
                Next
            Next
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try

    End Sub

    Private Sub CheckBoxLoadData_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CheckBoxLoadData.CheckedChanged
        CheckBoxMapTarget.Enabled = CheckBoxLoadData.Checked
    End Sub
    Private Sub ButtonSwitchMapping_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonSwitchMapping.Click
        Dim selectedConnector As EA.Connector
        Dim objDL As EA.DiagramLink
        Dim blnActive As Boolean = False
        Try
            Me.Repository.EnableUIUpdates = False
            blnActive = CloseOpenDiagrams()
            For Each objDL In Me.Diagram.DiagramLinks
                selectedConnector = Repository.GetConnectorByID(objDL.ConnectorID)
                If selectedConnector.StyleEx.ToUpper().Contains("LFEP") Then
                    objDL.IsHidden = Me.CheckBoxIsHidden.Checked
                    objDL.Update()
                End If
            Next
            Diagram.DiagramLinks.Refresh()
            Diagram.Update()
            If blnActive Then
                Me.Repository.OpenDiagram(Me.Diagram.DiagramID)
            End If
            Me.Repository.RefreshOpenDiagrams(True)
            Me.Repository.ReloadDiagram(Diagram.DiagramID)
            Me.Repository.EnableUIUpdates = True
            If CheckBoxCloseWindowExtraReady.Checked Then
                Me.Close()
            End If
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try

    End Sub
    Private Sub ButtonMappingStyle_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonMappingStyle.Click
        Dim selectedConnector As EA.DiagramLink
        Try
            For Each selectedConnector In Diagram.DiagramLinks
                If RadioButtonDirect.Checked Then
                    selectedConnector.LineStyle = EA.LinkLineStyle.LineStyleDirect
                ElseIf RadioButtonAutoroute.Checked Then
                    selectedConnector.LineStyle = EA.LinkLineStyle.LineStyleCustomLine
                ElseIf RadioButtonTreeVertical.Checked Then
                    selectedConnector.LineStyle = EA.LinkLineStyle.LineStyleTreeVertical
                ElseIf RadioButtonLateralVertical.Checked Then
                    selectedConnector.LineStyle = EA.LinkLineStyle.LineStyleLateralVertical
                Else
                    selectedConnector.LineStyle = EA.LinkLineStyle.LineStyleOrthogonalRounded
                End If
                selectedConnector.Update()
            Next
            Diagram.Update()
            Repository.ReloadDiagram(Diagram.DiagramID)
            If CheckBoxCloseWindowExtraReady.Checked Then
                Me.Close()
            End If
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try

    End Sub

    Private Sub ButtonGenerateItems_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonGenerateItems.Click
        If IsNothing(Me.ListBoxType.SelectedItem) Then
            MsgBox("Target type not selected, select a target type first", MsgBoxStyle.OkOnly)
        Else
            Dim item As DataRowView
            Dim objNew As EA.Element
            Dim objElement As EA.Element
            Dim objGenerator As New IDEAGenerator()
            Dim ElementsCombi As New List(Of EA.Element())
            Try
                objGenerator.Repository = Me.Repository
                Me.Repository.EnableUIUpdates = False
                'copy elements
                For Each item In Me.ListBoxElements.CheckedItems
                    objElement = Repository.GetElementByID(item("id"))
                    objNew = objGenerator.CreateElement(Me.TextBoxPrefix.Text & objElement.Name, Me.ListBoxType.SelectedItem.ToString(), Me.ComboBoxTargetPackage.SelectedValue)
                    ElementsCombi.Add({objElement, objNew})
                    Dim objDT As DataTable
                    Dim strSql As String
                    strSql = String.Format("SELECT id as attribute_id, name FROM t_attribute WHERE object_id = {0} ORDER BY name", objElement.ElementID)
                    objDT = DLA2EAHelper.SQL2DataTable(strSql, Me.Repository)
                    Dim objDR As DataRow
                    For Each objDR In objDT.Rows
                        objGenerator.CopyAttribute(objElement, objNew, Me.ListBoxType.SelectedItem.ToString().ToUpper(), objDR("attribute_id"), Me.CheckBoxAttributeAssociation.Checked)
                        If Not Me.CheckBoxAttributeAssociation.Checked Then
                            objGenerator.CreateTraceAssociation(objElement, objNew)
                        End If
                    Next
                Next
                'assocations processing
                For Each arr In ElementsCombi
                    objElement = arr(0)
                    objNew = arr(1)
                    Dim objCon As EA.Connector
                    Dim objConNew As EA.Connector
                    Dim intConnectorEnd As Int32
                    For Each objCon In objElement.Connectors
                        intConnectorEnd = -999
                        If objCon.SupplierID = objElement.ElementID Then
                            intConnectorEnd = objCon.ClientID
                            For Each arrFind In ElementsCombi
                                If arrFind(0).ElementID = intConnectorEnd Then
                                    Dim objNewEnd As EA.Element
                                    objNewEnd = arrFind(1)
                                    objConNew = objGenerator.CopyAssociation(objCon, objNew, objNewEnd)
                                    If objNew.Stereotype.ToUpper() = "TABLE" Then
                                        objConNew.Stereotype = "FK"
                                        objConNew.Update()
                                    End If
                                    Exit For
                                End If
                            Next
                        End If
                    Next
                Next
                Me.Repository.EnableUIUpdates = True
                Me.Close()
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
        End If
    End Sub
    Private Function CloseOpenDiagrams() As Boolean
        If Repository.GetCurrentDiagram().DiagramID = Diagram.DiagramID Then
            Repository.CloseDiagram(Diagram.DiagramID)
            Return True
        Else
            Return False
        End If
    End Function
    Private Sub ButtonHideEmbedded_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonHideEmbedded.Click
        Try
            If Not Diagram Is Nothing Then
                ' Get a reference to any selected connector/objects

                Dim objDL As EA.DiagramLink
                Dim objDO As EA.DiagramObject
                Dim targetDO As EA.DiagramObject
                Dim sourceDO As EA.DiagramObject
                Dim blnOpenDiagram As Boolean = False

                Dim selectedConnector As EA.Connector
                Dim targetID, sourceID

                Repository.EnableUIUpdates = False
                blnOpenDiagram = CloseOpenDiagrams()
                For Each objDL In Diagram.DiagramLinks
                    selectedConnector = Repository.GetConnectorByID(objDL.ConnectorID)
                    sourceID = selectedConnector.SupplierID
                    targetID = selectedConnector.ClientID

                    For Each objDO In Diagram.DiagramObjects

                        If objDO.ElementID = targetID Then
                            targetDO = objDO
                        End If
                        If objDO.ElementID = sourceID Then
                            sourceDO = objDO
                        End If
                    Next
                    If (targetDO.top >= sourceDO.top And targetDO.bottom <= sourceDO.bottom And targetDO.left <= sourceDO.left And targetDO.right >= sourceDO.right) Or
                        (targetDO.top <= sourceDO.top And targetDO.bottom >= sourceDO.bottom And targetDO.left >= sourceDO.left And targetDO.right <= sourceDO.right) Then
                        objDL.IsHidden = True
                    Else
                        objDL.IsHidden = False
                    End If
                    objDL.Update()
                    objDL.Update()
                    Repository.WriteOutput("IDEA", "Assocations Processed!! ", 0)
                Next
                Diagram.Update()
                Repository.SaveAllDiagrams()
                Repository.RefreshOpenDiagrams(True)
                If blnOpenDiagram Then
                    Repository.OpenDiagram(Diagram.DiagramID)
                End If
                Repository.EnableUIUpdates = True
            End If
                If CheckBoxCloseWindowExtraReady.Checked Then
                Me.Close()
            End If
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try

    End Sub
    ''' <summary>
    ''' Routine for matching attributes based on the name
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonMatchNames_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonMatchNames.Click
        Dim objRow As DataGridViewRow
        Try
            For Each objRow In DataGridViewMapping.Rows
                'when the target is filled that is leadingotherwise the source
                If CheckBoxMapTarget.Checked Then
                    objRow.Cells("source_attributeid").Value = Me.FindIDbyName(objRow.Cells("target_attributeid").Value, CheckBoxMapTarget.Checked)
                Else
                    objRow.Cells("target_attributeid").Value = Me.FindIDbyName(objRow.Cells("source_attributeid").Value, CheckBoxMapTarget.Checked)
                End If
            Next
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try


    End Sub

    Public Function FindIDbyName(ByVal id As String, ByVal MapTarget As Boolean) As String
        Dim FoundKey As String = ""
        Dim strName As String = ""
        Dim colRows As DataRowCollection
        Dim colSearchRows As DataRowCollection

        If MapTarget = True Then
            colRows = Me.tblTarget.Rows
            colSearchRows = Me.tblSource.Rows
        Else
            colRows = Me.tblSource.Rows
            colSearchRows = Me.tblTarget.Rows
        End If

        For Each objRow In colRows
            If objRow("attribute_id") = id Then
                strName = objRow("attribute_name")
                For Each objSearchRow In colSearchRows
                    If objSearchRow("attribute_name") = strName Then
                        FoundKey = objSearchRow("attribute_id")
                        Exit For
                    End If
                Next
                Exit For
            End If
        Next
        Return FoundKey
    End Function

    Private Sub ButtonCollectElements_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonCollectElements.Click
        Dim theDO As EA.DiagramObject
        Try
            Repository.EnableUIUpdates = False
            For Each theDO In Diagram.DiagramObjects
                Dim theElement As EA.Element
                theElement = Repository.GetElementByID(theDO.ElementID)
                Dim objDS As New DataSet2Repository()
                objDS.AddOrUpdateTaggedValue(theElement, "originalpackage", theElement.PackageID, False)
                theElement.PackageID = Diagram.PackageID
                theElement.Update()
            Next
            Repository.SaveAllDiagrams()
            Repository.RefreshOpenDiagrams(True)
            Repository.ReloadDiagram(Diagram.DiagramID)
            Repository.EnableUIUpdates = True
            Repository.ReloadPackage(Diagram.PackageID)
            If CheckBoxCloseWindowExtraReady.Checked Then
                Me.Close()
            End If
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonShowHidden.Click
        Dim objDL As EA.DiagramLink
        Dim blnActive As Boolean = False

        Try
            blnActive = CloseOpenDiagrams()
            Me.Repository.EnableUIUpdates = False
            For Each objDL In Me.Diagram.DiagramLinks
                If Me.CheckBoxMakeHidden.Checked Then
                    If objDL.LineWidth = 5 Then
                        objDL.IsHidden = True
                        objDL.LineWidth = 1
                        objDL.LineColor = -1
                    End If
                Else
                    If objDL.IsHidden Then
                        objDL.IsHidden = False
                        objDL.LineWidth = 5
                        objDL.LineColor = 100
                    End If
                End If
                objDL.Update()
            Next
            Diagram.DiagramLinks.Refresh()
            Diagram.Update()
            If blnActive Then
                Me.Repository.OpenDiagram(Me.Diagram.DiagramID)
            End If
            Me.Repository.RefreshOpenDiagrams(True)
            Me.Repository.ReloadDiagram(Diagram.DiagramID)
            Me.Repository.EnableUIUpdates = True
            If CheckBoxCloseWindowExtraReady.Checked Then
                Me.Close()
            End If
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try

    End Sub

    Private Sub FrmIdeaDiagram_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        If Me.Repository.IsSecurityEnabled Then
            Me.ComboBoxSecGroup.DataSource = DLA2EAHelper.EAString2DataTable(Repository.SQLQuery("select groupname from t_secgroup order by 1"))
            Me.ComboBoxSecGroup.DisplayMember = "groupname"
            Me.ComboBoxSecGroup.ValueMember = "groupname"
        Else
            Me.ButtonLockElements.Enabled = False
            Me.ComboBoxSecGroup.Enabled = False
            Me.CheckBoxUserLock.Enabled = False
        End If
        LoadSqlStatements()
    End Sub

    Private Sub LoadSQLStatements()
        Dim oID As New IDEADefinitions()
        Dim oStatementDT As DataTable = oID.GetTable("SQL-Statement")
        Me.DataGridViewSQL.DataSource = oStatementDT
    End Sub
    Private Sub ButtonLoad_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonLoad.Click
        Try
            Dim oID As New IDEADefinitions()
            If DataGridViewSQL.SelectedRows.Count > 0 Then
                Dim oRow As DataGridViewRow = DataGridViewSQL.CurrentRow
                Dim strSQL As String = oRow.Cells("Statement").Value.ToString().Replace("#diagram_id#", Diagram.DiagramID)
                Me.DataGridViewStatement.DataSource = DLA2EAHelper.EAString2DataTable(Me.Repository.SQLQuery(strSQL))
                Me.TextBoxStatement.Text = oRow.Cells("Statement").Value.ToString()
                Me.TextBoxTemplate.Text = oRow.Cells("Template").Value.ToString()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Function SQL2File(Statement As String, Template As String, filename As String) As String
        Dim Helper As New DLADataSetHelper()
        Dim strSQL As String
        Dim oID As New IDEADefinitions()

        Helper.DataTable = DLA2EAHelper.EAString2DataTable(Me.Repository.SQLQuery(Statement.Replace("#diagram_id#", Diagram.DiagramID)))
        strSQL = Helper.DataTable2String(Template)
        If String.IsNullOrEmpty(filename) Then
            MsgBox("PLease select a file to export to first", MsgBoxStyle.Critical)
        Else
            Dim strFile As String = oID.GetSettingValue("ReleaseDirectory") + "\" + filename + ".sql"
            DLA2EAHelper.String2File(strSQL, strFile)
            Return strFile + vbCrLf
        End If
        Return "No file created!!"
    End Function


    Private Sub ButtonMakeSQL_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonMakeSQL.Click
        Dim Helper As New DLADataSetHelper()
        Dim strColumnSql As String = ""
        Dim strTableSql As String = ""
        Dim oID As New IDEADefinitions()
        Dim strResult As String = ""
        Try

            If Me.CheckBoxCreateAll.Checked Then
                For Each oRow As DataRow In Me.DataGridViewSQL.DataSource.Rows
                    strResult += SQL2File(oRow("statement"), oRow("template"), oRow("name"))
                Next
            Else
                If DataGridViewSQL.SelectedRows.Count > 0 Then
                    strResult += SQL2File(DataGridViewSQL.CurrentRow.Cells("Statement").Value.ToString(), DataGridViewSQL.CurrentRow.Cells("Template").Value.ToString(), DataGridViewSQL.CurrentRow.Cells("Name").Value.ToString())
                Else
                    MsgBox("Please select a row first in the grid of SQL statements", MsgBoxStyle.Critical)
                End If
            End If
            If strResult.Length > 0 Then
                Me.Close()
                MsgBox(strResult + vbCrLf + "files  created", MsgBoxStyle.OkOnly)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString(), MsgBoxStyle.Critical)
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub

    Private Sub ButtonLockElements_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonLockElements.Click
        Dim oDO As EA.DiagramObject
        For Each oDO In Me.Diagram.DiagramObjects
            Dim oElement As EA.Element
            oElement = Me.Repository.GetElementByID(oDO.ElementID)
            If Me.CheckBoxUserLock.Checked Then
                If oElement.Locked = False Then
                    oElement.ApplyUserLock()
                End If
            Else
                If oElement.Locked = False And Not String.IsNullOrEmpty(Me.ComboBoxSecGroup.SelectedValue) Then
                    oElement.ApplyGroupLock(Me.ComboBoxSecGroup.SelectedValue)

                End If
            End If
            oElement.Update()
        Next
    End Sub

    Private Sub ButtonElement2Original_Click(sender As Object, e As EventArgs) Handles ButtonElement2Original.Click
        Dim theDO As EA.DiagramObject
        Dim strPackage As String
        Try
            Repository.EnableUIUpdates = False
            For Each theDO In Diagram.DiagramObjects
                Dim theElement As EA.Element
                theElement = Repository.GetElementByID(theDO.ElementID)
                strPackage = DataSet2Repository.GetTaggedValue(theElement, "originalpackage")
                If strPackage.Length > 0 Then
                    theElement.PackageID = Convert.ToInt32(strPackage)
                    theElement.Update()
                End If
            Next
            Repository.SaveAllDiagrams()
            Repository.RefreshOpenDiagrams(True)
            Repository.ReloadDiagram(Diagram.DiagramID)
            Repository.EnableUIUpdates = True
            Repository.ReloadPackage(Diagram.PackageID)
            If CheckBoxCloseWindowExtraReady.Checked Then
                Me.Close()
            End If
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub

    Private Sub ButtonToggleGenerate_Click(sender As Object, e As EventArgs) Handles ButtonToggleGenerate.Click

        Dim i As Int16 = 0
        While i < ListBoxElements.Items.Count
            ListBoxElements.SetItemChecked(i, (Not ListBoxElements.GetItemChecked(i)))
            i += 1
        End While

    End Sub

    Private Sub ButtonSelectAllGenerate_Click(sender As Object, e As EventArgs) Handles ButtonSelectAllGenerate.Click
        SelectDeSelect(True)
    End Sub

    Private Sub ButtonUnselectGenerate_Click(sender As Object, e As EventArgs) Handles ButtonUnselectGenerate.Click
        SelectDeSelect(False)
    End Sub

    Private Sub ButtonArchiMateColoring_Click(sender As Object, e As EventArgs) Handles ButtonArchiMateColoring.Click
        For Each oDO As EA.DiagramObject In Me.Diagram.DiagramObjects
            Dim Element As EA.Element
            Element = Me.Repository.GetElementByID(oDO.ElementID)
            Dim StereoType As String = Element.Stereotype.ToString().ToUpper().Replace("ARCHIMATE_", "")
            If StereoType.Contains("ARTIFACT") Or StereoType.Contains("Object") Then
                oDO.BorderColor = IIf(CheckBoxRestoreColor.Checked, 0, 255)
            End If
            If StereoType.Contains("Event") Or StereoType.Contains("PROCESS") Or StereoType.Contains("Function") Or StereoType.Contains("SERVICE") Then
                oDO.BorderColor = IIf(CheckBoxRestoreColor.Checked, 0, 16748571)
            End If
            If StereoType.Contains("ROLE") Or StereoType.Contains("ACTOR") Or StereoType.Contains("Interface") Or StereoType.Contains("COMPONENT") _
                Or StereoType.Contains("PATH") Or StereoType.Contains("SYSTEMSOFTWARE") Or StereoType.Contains("NODE") Or StereoType.Contains("DEVICE") Then
                oDO.BorderColor = IIf(CheckBoxRestoreColor.Checked, 0, 7451452)
            End If
            oDO.Update()
        Next
    End Sub


    Private Sub TextBoxFilter_TextChanged(sender As Object, e As EventArgs) Handles TextBoxFilter.TextChanged
        Dim oDV As New DataView(Me.DataGridViewSQL.DataSource)
        If Me.TextBoxFilter.Text.Length > 0 Then
            Dim Filter As String = String.Format(" name Like '%{0}%' or type LIKE '%{0}%'  ", Me.TextBoxFilter.Text)
                    oDV.RowFilter = Filter
            Me.DataGridViewSQL.DataSource = oDV.ToTable()
        Else
            Dim oID As New IDEADefinitions()
            Me.DataGridViewSQL.DataSource = oID.GetTable("SQL-Statement")
        End If
    End Sub
End Class