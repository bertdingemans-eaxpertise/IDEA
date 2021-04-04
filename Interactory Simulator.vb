Imports TEA.DLAFormfactory
Imports System.ComponentModel
Imports System.Windows.Forms

Public Class Interactory_Simulator
    Protected ConnectionString As String = ""
    Protected oTL As New DLAFormfactory.DLALoadTree()
    Protected oDSC As DLADataSetContainer
    Protected ActiveDataTable As String
    Protected ActiveId As String
    Protected ActiveDataRow As DataRow
    Protected blnHasData As Boolean = False
    Public TableNames As DataTable
    Public WriteOnly Property DataSetContainer() As DLADataSetContainer
        Set(ByVal value As DLADataSetContainer)
            oDSC = value
        End Set
    End Property
    Public Sub LoadData2Tables()
        Me.ComboBoxModelType.Text = My.Settings.ConnectionType
        If My.Settings.ConnectionType = "SQL" Then
            If My.Settings.ConnectionString.Length > 0 Then
                Me.oDSC.LoadDataFromSQL()
            Else
                MsgBox("Define connectionstring first")
            End If
        End If
        If My.Settings.ConnectionType = "XML" Then
            If My.Settings.XMLFile.Length > 0 Then
                Me.oDSC.LoadDataFromXML()
            Else
                MsgBox("Select XML file for simulator first")
            End If
        End If
    End Sub

    Private Sub LoadData()
        Me.LoadData2Tables()
        If My.Settings.ConnectionString.Length > 0 Then
            LoadTreeView()
        Else
            LoadDataBase()
        End If
    End Sub
    Private Sub Interactory_Simulator_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
    End Sub
    Private Sub LoadDataBase()
        Me.LoadData2Tables()
    End Sub

    Private Sub LoadDataGrid(Name As String, id As String)
        If Name.Length > 0 Then
            Dim GridDataView As DataView = Me.oDSC.GetDataViewFromTable(Name)
            If id.Length > 0 And id <> "NEW" Then
                Dim oTable As DataTable
                oTable = Me.oDSC.ContainerDataSet.Tables(Name)
                If oTable.PrimaryKey.Length > 0 Then
                    GridDataView.RowFilter = oTable.PrimaryKey(0).ColumnName + " = " + id
                    If GridDataView.Count = 1 Then
                        Dim DRV As DataRowView
                        DRV = GridDataView.Item(0)
                        Dim DataTable As New DataTable("Detail")
                        DataTable.Columns.Add("Name")
                        DataTable.Columns.Add("Value")
                        Dim column As DataColumn
                        For Each column In oTable.Columns
                            Dim oRow As DataRow
                            oRow = DataTable.Rows.Add()
                            oRow.Item("Name") = column.Caption
                            oRow.Item("Value") = DRV.Item(column.ColumnName)
                        Next
                        Me.DataGridViewDetail.DataSource = DataTable
                    End If
                End If
            Else
                Me.DataGridViewDetail.DataSource = GridDataView
            End If
        End If
    End Sub


    Private Sub LoadTreeView()
        Me.TreeViewInteractory.Nodes.Clear()
        Dim objRoot As New TreeNode("Datamodel")
        Dim oTable As DataTable
        Dim Row As DataRow
        Dim strTabellen As String = ""
        For Each Row In Me.TableNames.Rows
            strTabellen += Row("Name").ToString().ToUpper + "|"
        Next
        Try
            For Each oTable In Me.oDSC.ContainerDataSet.Tables
                If Not oTable.TableName.ToUpper.Contains("SYS_") And strTabellen.Contains(oTable.TableName.ToUpper) Then
                    Dim objChild As New TreeNode(DLAFormfactory.DLA2EAHelper.InitCap(oTable.TableName))
                    objChild.Name = DLAFormfactory.DLA2EAHelper.InitCap(oTable.TableName)
                    oTL.LoadTreeViewNode(oTable, oTable.PrimaryKey(0).ColumnName, "lookup", objChild)
                    objRoot.Nodes.Add(objChild)
                End If
            Next
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
        Me.TreeViewInteractory.Nodes.Add(objRoot)
    End Sub

    Private Sub LoadTreeViewChildren(action As String())
        Dim objRoot As New TreeNode("Datamodel")
        Dim oTable As DataTable
        Dim oRelation As DataRelation
        oTable = Me.oDSC.GetDataTable(Me.ActiveDataTable)
        Dim id As String
        Try
            id = action(1)
            If id.Length > 0 And id <> "NEW" Then
                For Each oRelation In oTable.ChildRelations
                    Dim objChild As New TreeNode(oRelation.RelationName)
                    objChild.Name = oTable.TableName
                    Dim oChildTable As DataTable
                    oChildTable = oRelation.ChildTable
                    Dim ChildRows As DataRow()
                    Try
                        ChildRows = Me.ActiveDataRow.GetChildRows(oRelation)
                        oTL.LoadTreeViewChildNodes(ChildRows, oChildTable.PrimaryKey(0).ColumnName, "lookup", objChild, oChildTable.TableName)
                    Catch ex As Exception
                        DLA2EAHelper.Error2Log(ex)
                    End Try
                    Me.TreeViewInteractory.SelectedNode.Nodes.Add(objChild)
                Next
            End If
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub
    Private Sub FilterBut_Click(sender As Object, e As EventArgs) Handles FilterBut.Click
        Me.oTL.SetFilter(Me.ToolStripFilter.Text)
        Me.LoadTreeView()
    End Sub
    Private Sub Row2XML()
        Dim oDT As DataTable
        Try
            If My.Settings.ConnectionType = "XML" Then
                oDT = oDSC.GetDataTable(Me.ActiveDataTable)
                If Me.ActiveId = "NEW" Then
                    oDT.Rows.Add(Me.ActiveDataRow)
                End If
                Me.ActiveDataRow.AcceptChanges()
                oDT.AcceptChanges()
                'Me.oDSC.SaveDataToXML()
            End If
        Catch ex As Exception
            DLA2EAHelper.DebugAssertion(ex.ToString())
            DLA2EAHelper.Error2Log(ex)
        End Try

    End Sub

    Function ValidateActiveRow() As Boolean

        Dim strMessage As String = ""
        Try
            For Each Col As DataColumn In Me.ActiveDataRow.Table.Columns
                If Col.AllowDBNull = False And String.IsNullOrEmpty(ActiveDataRow(Col.ColumnName).ToString()) Then
                    strMessage += Col.Caption + " is required " + vbCrLf
                ElseIf Col.AllowDBNull = False And IsDBNull(ActiveDataRow(Col.ColumnName)) Then
                    strMessage += Col.Caption + " is required " + vbCrLf
                ElseIf Not IsDBNull(ActiveDataRow(Col.ColumnName)) Then
                    If ActiveDataRow(Col.ColumnName).ToString() = "-999" Then
                        strMessage += Col.Caption + " is not a valid reference to a connected entity" + vbCrLf
                    End If
                End If
            Next
            strMessage += vbCrLf + "Extra conditions" + vbCrLf + ActiveDataRow("Validation").ToString()

            If strMessage.Length > 20 Then
                MsgBox(strMessage)
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try

    End Function
    Private Sub Row2SQL()
        Dim sSql As String = ""
        Dim oDT As DataTable
        Try
            If My.Settings.ConnectionType = "SQL" Then
                If Me.ActiveId = "NEW" Then
                    sSql = DLADataSetContainer.DataTable2Insert(oDSC.GetDataTable(Me.ActiveDataTable))
                Else
                    sSql = DLADataSetContainer.DataTable2Update(oDSC.GetDataTable(Me.ActiveDataTable))
                    oDT = oDSC.GetCommandForTable(Me.ActiveDataTable, "UPDATE")
                End If
                Dim oCol As DataColumn
                For Each oCol In Me.ActiveDataRow.Table.Columns
                    sSql = sSql.Replace("@@" + oCol.ColumnName.ToLower() + "@@", IIf(IsDBNull(Me.ActiveDataRow(oCol.ColumnName)), "NULL", Me.ActiveDataRow(oCol.ColumnName)))
                Next
                sSql = sSql.Replace("''", "'")
                sSql = sSql.Replace("'NULL'", "NULL")
                    sSql = sSql.Replace("-999", "NULL")
                    sSql = sSql.Replace("''", "NULL")

                    DLA2EAHelper.ExecuteModifySQL(sSql)
                    If Me.ActiveId = "NEW" Then
                        oDSC.GetDataTable(Me.ActiveDataTable).Rows.Add(Me.ActiveDataRow)
                        oDSC.GetDataTable(Me.ActiveDataTable).AcceptChanges()
                    Else
                        Me.ActiveDataRow.AcceptChanges()
                    End If
                End If
                Catch ex As Exception
            DLAFormfactory.DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub

    Private Sub NewCon_Click(sender As Object, e As EventArgs)
        LoadDataBase()
    End Sub

    Private Sub DispCon_Click(sender As Object, e As EventArgs) Handles DispCon.Click
        MsgBox(My.Settings.ConnectionType + " | " + My.Settings.ConnectionString + " | " + My.Settings.XMLFile)
    End Sub

    Private Sub LoadChildrenButton_Click(sender As Object, e As EventArgs) Handles LoadChildrenButton.Click
        Dim action As String()
        action = Me.TreeViewInteractory.SelectedNode.Name.Split("|")
        If action.Length > 1 Then
            Me.LoadTreeViewChildren(action)
        Else
            MsgBox("Loading children is not possible", MsgBoxStyle.Question)
        End If
    End Sub

    Private Sub TreeViewInteractory_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeViewInteractory.AfterSelect
        Dim action As String()
        action = Me.TreeViewInteractory.SelectedNode.Name.Split("|")
        Me.ActiveId = ""
        Me.ActiveDataTable = action(0)
        If action.Length > 1 Then
            Me.ActiveId = action(1)
            Me.ActiveDataRow = Table2DataRow(Me.ActiveDataTable, Me.ActiveId)
            Me.LoadDataGrid(Me.ActiveDataTable, Me.ActiveId)
            Me.LoadModifyTab(Me.ActiveDataTable, Me.ActiveId)
            Me.TabControlDetail.SelectTab("TabpageDetail")
        Else
            Me.LoadDataGrid(Me.ActiveDataTable, "")
            Me.TabControlDetail.SelectTab("TabpageDisplay")
        End If
    End Sub
    Private Sub LoadModifyTab(Name As String, id As String)

        Dim oDV As DataTable
        Dim intTop As Int16 = 10
        Dim cc As Control.ControlCollection
        Dim strVal As String = "-999"
        Dim intLeft As Int32 = 250
        Dim intTabTeller As Int32 = 0
        Try
            Me.TabControlDetail.TabPages("TabPageDetail").TabStop = True
            cc = Me.TabControlDetail.TabPages("TabPageDetail").Controls
            cc.Clear()
            Me.TabControlDetail.TabPages("TabPageDetail").SuspendLayout()
            oDV = Me.oDSC.GetControlsForTable(Name).ToTable()
            For Each oRow In oDV.Rows
                Dim oLabel As New Label()
                oLabel.Top = intTop
                oLabel.Left = 10
                oLabel.Width = intLeft - 15
                oLabel.TextAlign = Drawing.ContentAlignment.TopRight
                oLabel.Text = oRow("controlcaption")
                cc.Add(oLabel)
                Select Case oRow("ControlType").ToUpper()
                    Case "SINGLELINEEDIT", "MULTILINEEDIT", "HIDDENSLE"
                        Dim oTextBox As New TextBox()
                        oTextBox.Name = oRow("controlname")
                        oTextBox.Top = intTop
                        oTextBox.Left = intLeft
                        oTextBox.Width = Me.TabControlDetail.TabPages("TabPageDetail").Width - (intLeft + 10)
                        oTextBox.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top
                        oTextBox.TabIndex = intTabTeller
                        oTextBox.TabStop = True
                        Select Case oRow("ControlType").ToUpper()
                            Case "MULTILINEEDIT"
                                oTextBox.Height = 150
                                oTextBox.Multiline = True
                                oTextBox.ScrollBars = ScrollBars.Both
                                intTop += 160
                            Case "HIDDENSLE"
                                oLabel.Visible = False
                                oTextBox.Visible = False
                            Case Else
                                oTextBox.Height = 25
                                intTop += 35
                        End Select
                        If Me.blnHasData Then
                            oTextBox.Text = IIf(IsDBNull(Me.ActiveDataRow(oRow("controlname"))), "", Me.ActiveDataRow(oRow("controlname")))
                        End If
                        cc.Add(oTextBox)
                        cc.SetChildIndex(oTextBox, intTabTeller)
                    Case "CHECKBOX"
                        Dim oCheckBox As New CheckBox()
                        oCheckBox.Name = oRow("controlname")
                        oCheckBox.Left = intLeft
                        oCheckBox.Top = intTop
                        oCheckBox.Text = oRow("controlcaption")
                        oCheckBox.TabIndex = intTabTeller
                        oCheckBox.TabStop = True
                        If Me.blnHasData Then
                            oCheckBox.Checked = IIf(IsDBNull(Me.ActiveDataRow(oRow("controlname"))), False, Me.ActiveDataRow(oRow("controlname")))
                        End If
                        cc.Add(oCheckBox)
                        cc.SetChildIndex(oCheckBox, intTabTeller)

                        intTop += 35
                    Case "CALENDARSLE"
                        Dim oCalendar As New DateTimePicker()
                        oCalendar.Name = oRow("controlname")
                        oCalendar.MinDate = "1-1-1753"
                        oCalendar.Left = intLeft
                        oCalendar.Top = intTop
                        oCalendar.Width = 250
                        oCalendar.Anchor = AnchorStyles.Left Or AnchorStyles.Top
                        oCalendar.Height = 25
                        oCalendar.TabIndex = intTabTeller
                        oCalendar.TabStop = True
                        If blnHasData Then
                            oCalendar.Value = IIf(IsDBNull(Me.ActiveDataRow(oRow("controlname"))), "1-1-1753", Me.ActiveDataRow(oRow("controlname")))
                        End If
                        cc.Add(oCalendar)
                        cc.SetChildIndex(oCalendar, intTabTeller)
                        intTop += 35
                    Case "NUMBERSLE"
                        Dim oNumber As New NumericUpDown()
                        oNumber.Name = oRow("controlname")
                        oNumber.Maximum = 1000000
                        oNumber.Minimum = -999
                        oNumber.Top = intTop
                        oNumber.Left = intLeft
                        oNumber.Anchor = AnchorStyles.Left Or AnchorStyles.Top
                        oNumber.Height = 25
                        oNumber.TabIndex = intTabTeller
                        oNumber.TabStop = True
                        If Me.blnHasData Then
                            oNumber.Text = IIf(IsDBNull(Me.ActiveDataRow(oRow("controlname"))), "", Me.ActiveDataRow(oRow("controlname")))
                        End If
                        cc.Add(oNumber)
                        cc.SetChildIndex(oNumber, intTabTeller)
                        intTop += 35
                    Case "COMBOBOX"
                        Dim oComboBox As New ComboBox()
                        oComboBox.Name = oRow("controlname")
                        oComboBox.Top = intTop
                        oComboBox.Left = intLeft
                        oComboBox.Width = Me.TabControlDetail.TabPages("TabPageDetail").Width - (intLeft + 10)
                        oComboBox.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top
                        oComboBox.DropDownStyle = ComboBoxStyle.DropDown
                        oComboBox.Items.Add("-999 | Make a choice")
                        oComboBox.TabIndex = intTabTeller
                        oComboBox.TabStop = True
                        Dim oTable As New DataTable()
                        strVal = IIf(IsDBNull(Me.ActiveDataRow(oRow("controlname"))), "-999", Me.ActiveDataRow(oRow("controlname")))
                        If oRow("controllookup").ToUpper().Contains("LOOKUP") Then
                            oTable = Me.oDSC.GetDataTable(oRow("controllookup").Replace("Lookup_", ""))
                            For Each Row In oTable.Rows
                                Dim strText As String
                                strText = Row("Lookup")
                                strText = strText.PadRight(50)
                                oComboBox.Items.Add(Row(oTable.PrimaryKey(0).ColumnName).ToString() + " | " + strText)
                            Next
                            Try
                                oComboBox.SelectedIndex = oComboBox.FindString(strVal)
                            Catch ex As Exception
                                oComboBox.SelectedIndex = -1
                                DLAFormfactory.DLA2EAHelper.Error2Log(ex)
                            End Try
                        ElseIf oRow("controllookup").ToUpper().Contains("ENUM_") Then
                            oTable = Me.oDSC.GetEnumeration(oRow("controllookup"))
                            For Each Row In oTable.Rows
                                oComboBox.Items.Add(Row("Enumvalue"))
                            Next
                            Try
                                oComboBox.SelectedIndex = oComboBox.FindString(strVal)
                            Catch ex As Exception
                                oComboBox.SelectedIndex = -1
                                DLAFormfactory.DLA2EAHelper.Error2Log(ex)
                            End Try
                        End If
                        cc.Add(oComboBox)
                        cc.SetChildIndex(oComboBox, intTabTeller)

                        intTop += 35
                End Select
                intTabTeller += 1
            Next
            Me.TabControlDetail.TabPages("TabPageDetail").ResumeLayout()
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub
    Private Function Table2DataRow(Name As String, id As String) As DataRow
        Dim oTable As DataTable
        oTable = Me.oDSC.ContainerDataSet.Tables(Name)
        If id.Length > 0 And id <> "NEW" Then
            Me.blnHasData = True
            Return oTable.Rows.Find(id)
        End If
        If id = "NEW" Then
            Me.blnHasData = True
            Return oTable.NewRow()
        End If
        Return Nothing
    End Function

    Private Sub ToolStripButtonSave_Click(sender As Object, e As EventArgs) Handles ToolStripButtonSave.Click
        Me.Control2Row()
        If Me.ValidateActiveRow() Then
            Me.Row2SQL()
            Me.Row2XML()
            Me.LoadTreeView()
        End If

    End Sub

    Private Sub Interactory_Simulator_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If My.Settings.ConnectionType = "XML" Then
            If MsgBox("Save XML data to file", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Me.oDSC.SaveDataToXML()
            End If
        End If
    End Sub
    Private Function Control2Row() As Boolean
        Dim oCtrl As Control
        Try
            If Not IsNothing(Me.ActiveDataRow) Then
                For Each oCtrl In Me.TabControlDetail.TabPages("TabPageDetail").Controls
                    Dim strType As String = oCtrl.GetType().ToString().ToUpper().Replace("SYSTEM.WINDOWS.FORMS.", "")
                    Select Case strType
                        Case "NUMERICUPDOWN"
                            Dim oNumeric As System.Windows.Forms.NumericUpDown
                            oNumeric = DirectCast(oCtrl, System.Windows.Forms.NumericUpDown)
                            Me.ActiveDataRow(oNumeric.Name) = IIf(oNumeric.Value.ToString().Length = 0 Or oNumeric.Value = Convert.ToDecimal(-999), DBNull.Value, oNumeric.Value)
                        Case "DATETIMEPICKER"
                            Dim oDate As System.Windows.Forms.DateTimePicker
                            oDate = DirectCast(oCtrl, System.Windows.Forms.DateTimePicker)
                            Me.ActiveDataRow(oDate.Name) = IIf(oDate.Value = "1-1-1753", DBNull.Value, oDate.Value)
                        Case "TEXTBOX"
                            Dim oText As System.Windows.Forms.TextBox
                            oText = DirectCast(oCtrl, System.Windows.Forms.TextBox)
                            Me.ActiveDataRow(oText.Name) = oText.Text
                        Case "CHECKBOX"
                            Dim oCheck As System.Windows.Forms.CheckBox
                            ' The following conversion succeeds.
                            oCheck = DirectCast(oCtrl, System.Windows.Forms.CheckBox)
                            Me.ActiveDataRow(oCheck.Name) = oCheck.Checked
                        Case "COMBOBOX"
                            Dim oCombo As System.Windows.Forms.ComboBox
                            ' The following conversion succeeds.
                            oCombo = DirectCast(oCtrl, System.Windows.Forms.ComboBox)
                            Dim aText As String() = oCombo.SelectedItem.ToString().Split(" | ")
                            '                            If aText.Length > 1 Then
                            Me.ActiveDataRow(oCombo.Name) = IIf(aText(0) = "-999", "", aText(0))
                            '                           Else
                            '                           Me.ActiveDataRow(oCombo.Name) = oCombo.SelectedText
                            '           End If
                    End Select
                Next
            End If
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
        Return False
    End Function

    Private Sub ToolStripButtonReload_Click(sender As Object, e As EventArgs) Handles ToolStripButtonReload.Click
        My.Settings.ConnectionType = Me.ComboBoxModelType.Text
        My.Settings.Save()
        LoadData()
    End Sub

    Private Sub ToolStripButtonDelete_Click(sender As Object, e As EventArgs) Handles ToolStripButtonDelete.Click
        Try

            Dim oDT As DataTable
            oDT = oDSC.GetDataTable(Me.ActiveDataTable)
            If My.Settings.ConnectionType = "SQL" Then

                Dim sSql As String
                sSql = DLADataSetContainer.DataTable2Delete(oDSC.GetDataTable(Me.ActiveDataTable))
                For Each oCol In Me.ActiveDataRow.Table.Columns
                    sSql = sSql.Replace("@@" + oCol.ColumnName.ToLower() + "@@", IIf(IsDBNull(Me.ActiveDataRow(oCol.ColumnName)), "NULL", Me.ActiveDataRow(oCol.ColumnName)))
                Next
                sSql = sSql.Replace("''", "'")
                sSql = sSql.Replace("'NULL'", "NULL")
                sSql = sSql.Replace("''", "NULL")
                DLA2EAHelper.DebugAssertion(sSql)
                DLA2EAHelper.ExecuteModifySQL(sSql)
            End If
            Me.ActiveDataRow.Delete()
            oDT.AcceptChanges()
            Me.LoadTreeView()
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try


    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        'Me.oDSC.GetJoinTest()
    End Sub
End Class