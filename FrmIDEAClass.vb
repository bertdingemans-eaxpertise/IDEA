Imports TEA.DLAFormfactory
''' <summary>
''' Form for IDEA routines specific for UML class entities. For every type of
''' element a specific form is generated. This makes working with the IDEA AddOn
''' easier.
''' </summary>
Public Class FrmIDEAClass

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
    Public Sub LoadElement()
        Dim objDT As DataTable
        objDT = DLA2EAHelper.SQL2DataTable("SELECT t_object.object_id, t_object.name + ' ' + t_package.name as fullname FROM t_object, t_package WHERE t_object.package_id = t_package.package_id AND t_object.object_type In('Class', 'Interface') AND t_object.stereotype IS NULL ORDER BY t_object.name ", Me.Repository)
        '        objDS = DLA2EAHelper.SQL2DataSet("SELECT object_id, name, object_type FROM t_object WHERE object_type = 'Class' AND stereotype IS NULL ORDER BY name", Me.Repository)
        If Not IsNothing(objDT) Then
            Me.ComboBoxElement.DataSource = objDT
            Me.ComboBoxElement.DisplayMember = "fullname"
            Me.ComboBoxElement.ValueMember = "object_id"
            Me.ComboBoxElement.SelectedValue = Me.Element.ElementID
            Me.ComboBoxExistingRefactor.DataSource = objDT
            Me.ComboBoxExistingRefactor.DisplayMember = "fullname"
            Me.ComboBoxExistingRefactor.ValueMember = "object_id"
            Me.ComboBoxExistingEntity.DataSource = objDT
            Me.ComboBoxExistingEntity.DisplayMember = "fullname"
            Me.ComboBoxExistingEntity.ValueMember = "object_id"
        End If

        objDT = DLA2EAHelper.SQL2DataTable("SELECT package_id, name FROM t_package ORDER BY name", Me.Repository)
        If Not IsNothing(objDT) Then
            Me.ComboBoxTargetPackage.DataSource = objDT
            Me.ComboBoxTargetPackage.DisplayMember = "name"
            Me.ComboBoxTargetPackage.ValueMember = "package_id"
            Me.ComboBoxTargetPackage.SelectedValue = Me.Element.PackageID
            Me.LoadAttributes()
        End If

    End Sub
    Private Sub LoadAttributes()
        Dim objDT As DataTable
        Dim strSql As String
        If Not IsNothing(Me.ComboBoxElement.SelectedValue) Then
            strSql = String.Format("SELECT id as attribute_id, name FROM t_attribute WHERE object_id = {0} ORDER BY name", Me.ComboBoxElement.SelectedValue)
            objDT = DLA2EAHelper.SQL2DataTable(strSql, Me.Repository)
            If objDT.Rows.Count > 0 Then
                Me.ListBoxAttributes.DataSource = objDT
                Me.ListBoxAttributes.DisplayMember = "name"
                Me.ListBoxAttributes.ValueMember = "attribute_id"
            End If
        End If
    End Sub


    Private Sub FrmIDEAClass_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Me.ValidateGenerateControls()
        Me.SelectDeSelect(True)

    End Sub

    Private Sub ButtonLoad_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonLoad.Click
        Me.LoadAttributes()
    End Sub

    Private Sub RadioButtonNewEntity_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadioButtonNewEntity.CheckedChanged
        ValidateGenerateControls()
    End Sub
    Private Sub ValidateGenerateControls()
        Me.TextBoxNewEntity.Enabled = RadioButtonNewEntity.Checked
        Me.ComboBoxTargetPackage.Enabled = RadioButtonNewEntity.Checked
        Me.ComboBoxExistingEntity.Enabled = Me.RadioButtonExistingEntity.Checked
    End Sub
    Private Sub ValidateRefactorControls()
        Me.TextBoxNewRefactor.Enabled = RadioButtonNewEntityRefactor.Checked
        Me.ComboBoxExistingRefactor.Enabled = Me.RadioButtonExistingRefactor.Checked
    End Sub
    Sub LoadExistingItemListBox()
        Dim objDT As DataTable
        If Me.ListBoxType.SelectedIndex > -1 Then
            Dim strSelected As String
            strSelected = Me.ListBoxType.Items(Me.ListBoxType.SelectedIndex).ToString()
            Me.ComboBoxExistingEntity.DataSource = Nothing
            Me.ComboBoxExistingRefactor.DataSource = Nothing
            Select Case strSelected
                Case "Interface", "Class"
                    objDT = DLA2EAHelper.SQL2DataTable(String.Format("SELECT object_id, name, object_type FROM t_object WHERE object_type = '{0}' AND stereotype IS NULL ORDER BY name", strSelected), Me.Repository)
                Case "Table", "ArchiMate_DataObject", "ArchiMate_BusinessObject"
                    objDT = DLA2EAHelper.SQL2DataTable(String.Format("SELECT object_id, name, object_type FROM t_object WHERE stereotype = '{0}' ORDER BY name", strSelected), Me.Repository)
                Case Else
                    objDT = DLA2EAHelper.SQL2DataTable("SELECT object_id, name, object_type FROM t_object WHERE stereotype = 'XSDComplexType' ORDER BY name", Me.Repository)
            End Select
            If objDT.Rows.Count > 0 Then
                Me.ComboBoxExistingEntity.DataSource = objDT
                Me.ComboBoxExistingEntity.DisplayMember = "name"
                Me.ComboBoxExistingEntity.ValueMember = "object_id"
            End If
            objDT = DLA2EAHelper.SQL2DataTable("SELECT t_object.object_id, t_object.name + ' ' + t_package.name as fullname FROM t_object, t_package WHERE t_object.package_id = t_package.package_id AND t_object.object_type = 'Class' AND t_object.stereotype IS NULL ORDER BY t_object.name ", Me.Repository)
            If objDT.Rows.Count > 0 Then
                Me.ComboBoxExistingRefactor.DataSource = objDT
                Me.ComboBoxExistingRefactor.DisplayMember = "fullname"
                Me.ComboBoxExistingRefactor.ValueMember = "object_id"
            End If
        End If
    End Sub

    Private Sub RadioButtonExistingEntity_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadioButtonExistingEntity.CheckedChanged
        Me.ValidateGenerateControls()
    End Sub

    Private Sub ButtonGenerate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonGenerate.Click
        Dim objGenerator As New IDEAGenerator()
        objGenerator.Repository = Me.Repository
        If IsNothing(ListBoxType.SelectedItem) Then
            MsgBox("No targettype selected, select a target type first", MsgBoxStyle.OkOnly)
        Else
            If Me.ListBoxType.SelectedItem.ToString().ToUpper() = "USER-INTERFACE" Then
                objGenerator.CopyClass2UI(Me.Element, Me.ComboBoxTargetPackage.SelectedValue, Me.CheckBoxAttributeAssociation.Checked)
            Else
                Dim item As DataRowView
                Dim objNew As EA.Element

                If Me.RadioButtonNewEntity.Checked() Then
                    If Me.TextBoxNewEntity.Text.Length = 0 Then
                        MsgBox("No new entity defined, New used for naming", MsgBoxStyle.OkOnly)
                        Me.TextBoxNewEntity.Text = Me.Element.Name & "-" & Me.ListBoxType.SelectedItem.ToString()
                    End If
                    objNew = objGenerator.CreateElement(Me.TextBoxNewEntity.Text, Me.ListBoxType.SelectedItem.ToString(), Me.ComboBoxTargetPackage.SelectedValue)
                Else
                    objNew = Repository.GetElementByID(Me.ComboBoxExistingEntity.SelectedValue)
                End If
                For Each item In Me.ListBoxAttributes.CheckedItems
                    Dim objAttrib As EA.Attribute
                    objAttrib = objGenerator.CopyAttribute(Me.Element, objNew, Me.ListBoxType.SelectedItem.ToString().ToUpper(), item("attribute_id"), Me.CheckBoxAttributeAssociation.Checked)
                    If Me.CheckBoxAddAlias.Checked Then
                        objAttrib.Alias = objAttrib.Name.Replace("_", " ")
                        objAttrib.Alias = objAttrib.Alias.Substring(0, 1).ToUpper() + objAttrib.Alias.Substring(1).ToLower()
                    End If
                    If Me.CheckBoxControlType.Checked Then
                        Dim oTV As EA.AttributeTag
                        oTV = objAttrib.TaggedValues.AddNew("FormFactory_ControlType", "")
                        oTV.Value = FormFactoryGenerator.Attribute2ControlType(objAttrib)
                        oTV.Update()
                    End If
                    If Me.CheckBoxOperator.Checked Then
                        Dim oTV As EA.AttributeTag
                        oTV = objAttrib.TaggedValues.AddNew("FormFactory_Operator", "")
                        oTV.Update()
                    End If
                    objAttrib.Update()
                Next
                If Not Me.CheckBoxAttributeAssociation.Checked Then
                    objGenerator.CreateTraceAssociation(Me.Element, objNew)
                End If
            End If
            Me.Close()
        End If
    End Sub

    Private Sub ListBoxType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ListBoxType.SelectedIndexChanged
        Me.LoadExistingItemListBox()
        SetInterfaceCheckBox()
    End Sub
    Private Sub SetInterfaceCheckBox()

        Dim blnInterface As Boolean
            blnInterface = (Me.ListBoxType.Items(Me.ListBoxType.SelectedIndex).ToString() = "Interface")
        Me.CheckBoxAddAlias.Enabled = blnInterface
        Me.CheckBoxOperator.Enabled = blnInterface
        Me.CheckBoxControlType.Enabled = blnInterface

    End Sub

    Private Sub RadioButtonNewEntityRefactor_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadioButtonNewEntityRefactor.CheckedChanged
        Me.ValidateRefactorControls()
    End Sub

    Private Sub RadioButtonExistingRefactor_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadioButtonExistingRefactor.CheckedChanged
        Me.ValidateRefactorControls()
    End Sub

    Private Sub ButtonRefactor_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonRefactor.Click

        Dim item As DataRowView
        Dim objNew As EA.Element

        Dim objGenerator As New IDEAGenerator()
        objGenerator.Repository = Me.Repository
        If Me.RadioButtonNewEntityRefactor.Checked() Then
            If Me.TextBoxNewRefactor.Text.Length = 0 Then
                Me.TextBoxNewRefactor.Text = "New-Class"
            End If
            objNew = objGenerator.CreateElement(Me.TextBoxNewRefactor.Text, "CLASS", Me.Element.PackageID)
        Else
            objNew = Repository.GetElementByID(Me.ComboBoxExistingRefactor.SelectedValue)
        End If
        If Me.CheckBoxRefactor.Checked Then
            For Each item In Me.ListBoxAttributes.CheckedItems
                objGenerator.RefactorAttribute(item("attribute_id"), objNew.ElementID.ToString())
                objGenerator.RefactorAssociation(Me.Element, objNew, item("attribute_id"))
            Next
        Else
            For Each item In Me.ListBoxAttributes.CheckedItems
                objGenerator.CopyAttribute(Me.Element, objNew, "CLASS", item("attribute_id"), Me.CheckBoxAttributeAssociation.Checked)
            Next
        End If
        If CheckBoxSpecialisation.Checked Then
            objGenerator.CreateConnector(objNew, Element, "Generalization")
        End If
        Me.Repository.RefreshModelView(Element.PackageID)
        Me.Repository.RefreshOpenDiagrams(False)

        Me.Close()


    End Sub

    Private Sub ButtonSelectAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonSelectAll.Click
        SelectDeSelect(True)
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
    Private Sub ButtonUnselectAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonUnselectAll.Click
        SelectDeSelect(False)
    End Sub

    Private Sub ButtonToggleAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonToggleAll.Click
        SelectToggle()
    End Sub

    Private Sub ButtonControlType_Click(sender As Object, e As EventArgs) Handles ButtonControlType.Click
        For Each item In Me.ListBoxAttributes.CheckedItems
            Dim objAttrib As EA.Attribute
            objAttrib = Me.Repository.GetAttributeByID(item("attribute_id"))

            Dim oTV As EA.AttributeTag
            oTV = objAttrib.TaggedValues.AddNew("FormFactory_ControlType", "")
            oTV.Value = FormFactoryGenerator.Attribute2ControlType(objAttrib)
            oTV.Update()

            objAttrib.Update()
        Next
    End Sub

    Private Sub ButtonOperator_Click(sender As Object, e As EventArgs) Handles ButtonOperator.Click
        For Each item In Me.ListBoxAttributes.CheckedItems
            Dim objAttrib As EA.Attribute
            objAttrib = Me.Repository.GetAttributeByID(item("attribute_id"))

            Dim oTV As EA.AttributeTag
            oTV = objAttrib.TaggedValues.AddNew("FormFactory_Operator", "")
            oTV.Update()

            objAttrib.Update()
        Next
    End Sub

    Private Sub ButtonAlias_Click(sender As Object, e As EventArgs) Handles ButtonAlias.Click
        For Each item In Me.ListBoxAttributes.CheckedItems
            Dim objAttrib As EA.Attribute
            objAttrib = Me.Repository.GetAttributeByID(item("attribute_id"))
            objAttrib.Alias = objAttrib.Name.Replace("_", " ")
            objAttrib.Alias = objAttrib.Alias.Substring(0, 1).ToUpper() + objAttrib.Alias.Substring(1).ToLower()
            objAttrib.Update()
        Next
    End Sub


End Class