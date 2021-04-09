Imports TEA.DLAFormfactory

''' <summary>
''' Form for IDEA routines specific for database table entities. For every type of
''' element a specific form is generated. This makes working with the IDEA AddOn
''' easier.
''' </summary>
Public Class FrmIDEATable
    Private _Repository As EA.Repository

    Public Property Repository() As EA.Repository
        Get
            Return _Repository
        End Get
        Set(ByVal value As EA.Repository)
            _Repository = value
        End Set
    End Property

    Private _Element As EA.Element
    Public Property Element() As EA.Element
        Get
            Return _Element
        End Get
        Set(ByVal value As EA.Element)
            _Element = value
        End Set
    End Property

    Private Sub ButtonLoad_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonLoad.Click
        Me.LoadAttributes()
    End Sub

    Public Sub LoadElement()
        Dim objDT As DataTable
        objDT = DLA2EAHelper.SQL2DataTable("SELECT object_id, name, object_type FROM t_object WHERE stereotype IN('table', 'XSDcomplexType') ORDER BY name", Me.Repository)
        Me.ComboBoxElement.DataSource = objDT
        Me.ComboBoxElement.DisplayMember = "name"
        Me.ComboBoxElement.ValueMember = "object_id"
        Me.ComboBoxElement.SelectedValue = Me.Element.ElementID
        Me.ComboBoxExistingRefactor.DataSource = objDT
        Me.ComboBoxExistingRefactor.DisplayMember = "name"
        Me.ComboBoxExistingRefactor.ValueMember = "object_id"

        objDT = DLA2EAHelper.SQL2DataTable("SELECT package_id, name FROM t_package ORDER BY name", Me.Repository)
        Me.ComboBoxTargetPackage.DataSource = objDT
        Me.ComboBoxTargetPackage.DisplayMember = "name"
        Me.ComboBoxTargetPackage.ValueMember = "package_id"
        Me.ComboBoxTargetPackage.SelectedValue = Me.Element.PackageID
        Me.ListBoxType.SelectedValue = "Class"
        Me.LoadAttributes()
    End Sub
    Private Sub LoadAttributes()
        Dim objDT As DataTable
        Dim strSql As String
        strSql = String.Format("SELECT id as attribute_id, name FROM t_attribute WHERE object_id = {0} ORDER BY name", Me.ComboBoxElement.SelectedValue)
        objDT = DLA2EAHelper.SQL2DataTable(strSql, Me.Repository)
        If objDT.Rows.Count > 0 Then
            Me.ListBoxAttributes.DataSource = objDT
            Me.ListBoxAttributes.DisplayMember = "name"
            Me.ListBoxAttributes.ValueMember = "attribute_id"
        End If
    End Sub

    Private Sub FrmIDEATable_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Me.ValidateGenerateControls()
        Me.SelectDeSelect(True)
    End Sub
    Private Sub ValidateGenerateControls()
        Me.TextBoxNewEntity.Enabled = RadioButtonNewEntity.Checked
        Me.ComboBoxTargetPackage.Enabled = RadioButtonNewEntity.Checked
        Me.ComboBoxExistingEntity.Enabled = Me.RadioButtonExistingEntity.Checked
    End Sub


    Sub LoadExistingItemListBox()
        Dim objDT As DataTable
        If Me.ListBoxType.SelectedIndex > -1 Then
            Dim strSelected As String
            strSelected = Me.ListBoxType.Items(Me.ListBoxType.SelectedIndex).ToString().ToUpper()
            Select Case strSelected
                Case "CLASS"
                    objDT = DLA2EAHelper.SQL2DataTable("SELECT object_id, name, object_type FROM t_object WHERE object_type = 'Class' AND stereotype IS NULL ORDER BY name", Me.Repository)
                Case "TABLE"
                    objDT = DLA2EAHelper.SQL2DataTable("SELECT object_id, name, object_type FROM t_object WHERE stereotype = 'table' ORDER BY name", Me.Repository)
                Case Else '"BUSINESS OBJECT"
                    objDT = DLA2EAHelper.SQL2DataTable("SELECT object_id, name, object_type FROM t_object WHERE stereotype = 'ArchiMate_BusinessObject' ORDER BY name", Me.Repository)
            End Select
            'Me.ComboBoxExistingEntity.Items.Clear()
            Me.ComboBoxExistingEntity.DataSource = objDT
            Me.ComboBoxExistingEntity.DisplayMember = "name"
            Me.ComboBoxExistingEntity.ValueMember = "object_id"
            objDT = DLA2EAHelper.SQL2DataTable("SELECT object_id, name, object_type FROM t_object WHERE object_type = 'Class' AND stereotype IS NULL ORDER BY name", Me.Repository)
            Me.ComboBoxExistingRefactor.DataSource = objDT
            Me.ComboBoxExistingRefactor.DisplayMember = "name"
            Me.ComboBoxExistingRefactor.ValueMember = "object_id"
        End If
    End Sub

    Private Sub ButtonGenerate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonGenerate.Click
        If IsNothing(Me.ListBoxType.SelectedItem) Then
            MsgBox("Target type not selected, select a target type first", MsgBoxStyle.OkOnly)
        Else
            Dim item As DataRowView
            Dim objNew As EA.Element

            Dim objGenerator As New IDEAGenerator()
            objGenerator.Repository = Me.Repository
            If Me.RadioButtonNewEntity.Checked() Then
                If Me.TextBoxNewEntity.Text.Length = 0 Then
                    MsgBox("No new entity defined, New used for naming", MsgBoxStyle.OkOnly)
                    Me.TextBoxNewEntity.Text = "New-" & Me.ListBoxType.SelectedItem.ToString()
                End If
                objNew = objGenerator.CreateElement(Me.TextBoxNewEntity.Text, Me.ListBoxType.SelectedItem.ToString(), Me.ComboBoxTargetPackage.SelectedValue)
            Else
                objNew = Repository.GetElementByID(Me.ComboBoxExistingEntity.SelectedValue)
            End If
            For Each item In Me.ListBoxAttributes.CheckedItems
                objGenerator.CopyAttribute(Me.Element, objNew, Me.ListBoxType.SelectedItem.ToString().ToUpper(), item("attribute_id"), Me.CheckBoxAttributeAssociation.Checked)
            Next
            If Not Me.CheckBoxAttributeAssociation.Checked Then
                objGenerator.CreateTraceAssociation(Me.Element, objNew)
            End If
            Me.Close()
        End If
    End Sub

    Private Sub ButtonSelectAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonSelectAll.Click
        SelectDeSelect(True)
    End Sub

    Private Sub ButtonToggleAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonToggleAll.Click
        SelectToggle()
    End Sub

    Private Sub ButtonUnselectAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonUnselectAll.Click
        SelectDeSelect(False)
    End Sub

    Private Sub SelectDeSelect(ByVal state As Boolean)
        Dim i As Int16 = 0
        While i < ListBoxAttributes.Items.Count
            ListBoxAttributes.SetItemChecked(i, state)
            i += 1
        End While
    End Sub

    Private Sub SelectToggle()
        Dim i As Int16 = 0
        While i < ListBoxAttributes.Items.Count
            ListBoxAttributes.SetItemChecked(i, (Not ListBoxAttributes.GetItemChecked(i)))
            i += 1
        End While
    End Sub

    Private Sub ButtonRefactor_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonRefactor.Click
        MsgBox("Not implemented YET!")
    End Sub
End Class