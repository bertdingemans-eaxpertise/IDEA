''' <summary>
''' Form for IDEA routines specific for UML class entities. For every type of
''' element a specific form is generated. This makes working with the IDEA AddOn
''' easier.
''' </summary>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class FrmIDEAClass
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.ComboBoxElement = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ButtonLoad = New System.Windows.Forms.Button()
        Me.TabControlActions = New System.Windows.Forms.TabControl()
        Me.TabPageGenerate = New System.Windows.Forms.TabPage()
        Me.CheckBoxAddAlias = New System.Windows.Forms.CheckBox()
        Me.CheckBoxOperator = New System.Windows.Forms.CheckBox()
        Me.CheckBoxControlType = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ComboBoxTargetPackage = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CheckBoxAttributeAssociation = New System.Windows.Forms.CheckBox()
        Me.ListBoxType = New System.Windows.Forms.ListBox()
        Me.ButtonGenerate = New System.Windows.Forms.Button()
        Me.ComboBoxExistingEntity = New System.Windows.Forms.ComboBox()
        Me.RadioButtonExistingEntity = New System.Windows.Forms.RadioButton()
        Me.TextBoxNewEntity = New System.Windows.Forms.TextBox()
        Me.RadioButtonNewEntity = New System.Windows.Forms.RadioButton()
        Me.TabPageRefactor = New System.Windows.Forms.TabPage()
        Me.CheckBoxSpecialisation = New System.Windows.Forms.CheckBox()
        Me.CheckBoxRefactor = New System.Windows.Forms.CheckBox()
        Me.ComboBoxExistingRefactor = New System.Windows.Forms.ComboBox()
        Me.RadioButtonExistingRefactor = New System.Windows.Forms.RadioButton()
        Me.TextBoxNewRefactor = New System.Windows.Forms.TextBox()
        Me.RadioButtonNewEntityRefactor = New System.Windows.Forms.RadioButton()
        Me.ButtonRefactor = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TabPageExtra = New System.Windows.Forms.TabPage()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ButtonAlias = New System.Windows.Forms.Button()
        Me.ButtonOperator = New System.Windows.Forms.Button()
        Me.ButtonControlType = New System.Windows.Forms.Button()
        Me.ListBoxAttributes = New System.Windows.Forms.CheckedListBox()
        Me.ButtonSelectAll = New System.Windows.Forms.Button()
        Me.ButtonToggleAll = New System.Windows.Forms.Button()
        Me.ButtonUnselectAll = New System.Windows.Forms.Button()
        Me.TabControlActions.SuspendLayout()
        Me.TabPageGenerate.SuspendLayout()
        Me.TabPageRefactor.SuspendLayout()
        Me.TabPageExtra.SuspendLayout()
        Me.SuspendLayout()
        '
        'ComboBoxElement
        '
        Me.ComboBoxElement.FormattingEnabled = True
        Me.ComboBoxElement.Location = New System.Drawing.Point(16, 47)
        Me.ComboBoxElement.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ComboBoxElement.Name = "ComboBoxElement"
        Me.ComboBoxElement.Size = New System.Drawing.Size(281, 24)
        Me.ComboBoxElement.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(117, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Selected element"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 92)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(126, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Selected attributes"
        '
        'ButtonLoad
        '
        Me.ButtonLoad.Location = New System.Drawing.Point(307, 47)
        Me.ButtonLoad.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ButtonLoad.Name = "ButtonLoad"
        Me.ButtonLoad.Size = New System.Drawing.Size(55, 28)
        Me.ButtonLoad.TabIndex = 4
        Me.ButtonLoad.Text = "Load"
        Me.ButtonLoad.UseVisualStyleBackColor = True
        '
        'TabControlActions
        '
        Me.TabControlActions.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControlActions.Controls.Add(Me.TabPageGenerate)
        Me.TabControlActions.Controls.Add(Me.TabPageRefactor)
        Me.TabControlActions.Controls.Add(Me.TabPageExtra)
        Me.TabControlActions.Location = New System.Drawing.Point(389, 0)
        Me.TabControlActions.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TabControlActions.Name = "TabControlActions"
        Me.TabControlActions.SelectedIndex = 0
        Me.TabControlActions.Size = New System.Drawing.Size(523, 470)
        Me.TabControlActions.TabIndex = 5
        '
        'TabPageGenerate
        '
        Me.TabPageGenerate.BackColor = System.Drawing.Color.Gainsboro
        Me.TabPageGenerate.Controls.Add(Me.CheckBoxAddAlias)
        Me.TabPageGenerate.Controls.Add(Me.CheckBoxOperator)
        Me.TabPageGenerate.Controls.Add(Me.CheckBoxControlType)
        Me.TabPageGenerate.Controls.Add(Me.Label6)
        Me.TabPageGenerate.Controls.Add(Me.Label5)
        Me.TabPageGenerate.Controls.Add(Me.ComboBoxTargetPackage)
        Me.TabPageGenerate.Controls.Add(Me.Label3)
        Me.TabPageGenerate.Controls.Add(Me.CheckBoxAttributeAssociation)
        Me.TabPageGenerate.Controls.Add(Me.ListBoxType)
        Me.TabPageGenerate.Controls.Add(Me.ButtonGenerate)
        Me.TabPageGenerate.Controls.Add(Me.ComboBoxExistingEntity)
        Me.TabPageGenerate.Controls.Add(Me.RadioButtonExistingEntity)
        Me.TabPageGenerate.Controls.Add(Me.TextBoxNewEntity)
        Me.TabPageGenerate.Controls.Add(Me.RadioButtonNewEntity)
        Me.TabPageGenerate.Location = New System.Drawing.Point(4, 25)
        Me.TabPageGenerate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TabPageGenerate.Name = "TabPageGenerate"
        Me.TabPageGenerate.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TabPageGenerate.Size = New System.Drawing.Size(515, 441)
        Me.TabPageGenerate.TabIndex = 0
        Me.TabPageGenerate.Text = "Generate"
        '
        'CheckBoxAddAlias
        '
        Me.CheckBoxAddAlias.AutoSize = True
        Me.CheckBoxAddAlias.Location = New System.Drawing.Point(339, 268)
        Me.CheckBoxAddAlias.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CheckBoxAddAlias.Name = "CheckBoxAddAlias"
        Me.CheckBoxAddAlias.Size = New System.Drawing.Size(89, 21)
        Me.CheckBoxAddAlias.TabIndex = 14
        Me.CheckBoxAddAlias.Text = "Add Alias"
        Me.CheckBoxAddAlias.UseVisualStyleBackColor = True
        '
        'CheckBoxOperator
        '
        Me.CheckBoxOperator.AutoSize = True
        Me.CheckBoxOperator.Location = New System.Drawing.Point(173, 268)
        Me.CheckBoxOperator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CheckBoxOperator.Name = "CheckBoxOperator"
        Me.CheckBoxOperator.Size = New System.Drawing.Size(116, 21)
        Me.CheckBoxOperator.TabIndex = 13
        Me.CheckBoxOperator.Text = "Add Operator"
        Me.CheckBoxOperator.UseVisualStyleBackColor = True
        '
        'CheckBoxControlType
        '
        Me.CheckBoxControlType.AutoSize = True
        Me.CheckBoxControlType.Location = New System.Drawing.Point(17, 268)
        Me.CheckBoxControlType.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CheckBoxControlType.Name = "CheckBoxControlType"
        Me.CheckBoxControlType.Size = New System.Drawing.Size(131, 21)
        Me.CheckBoxControlType.TabIndex = 12
        Me.CheckBoxControlType.Text = "Add Controltype"
        Me.CheckBoxControlType.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(13, 180)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(108, 17)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Target package"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 4)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 17)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Target type"
        '
        'ComboBoxTargetPackage
        '
        Me.ComboBoxTargetPackage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxTargetPackage.FormattingEnabled = True
        Me.ComboBoxTargetPackage.Location = New System.Drawing.Point(153, 180)
        Me.ComboBoxTargetPackage.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ComboBoxTargetPackage.Name = "ComboBoxTargetPackage"
        Me.ComboBoxTargetPackage.Size = New System.Drawing.Size(343, 24)
        Me.ComboBoxTargetPackage.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.LemonChiffon
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label3.Location = New System.Drawing.Point(17, 324)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(484, 65)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "With this option you can generate new items of a type (as available in the listbo" &
    "x) including a selected list of attributes (as selected in the checkbox listbox)" &
    " in one easy step."
        '
        'CheckBoxAttributeAssociation
        '
        Me.CheckBoxAttributeAssociation.AutoSize = True
        Me.CheckBoxAttributeAssociation.Checked = True
        Me.CheckBoxAttributeAssociation.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxAttributeAssociation.Location = New System.Drawing.Point(17, 240)
        Me.CheckBoxAttributeAssociation.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CheckBoxAttributeAssociation.Name = "CheckBoxAttributeAssociation"
        Me.CheckBoxAttributeAssociation.Size = New System.Drawing.Size(203, 21)
        Me.CheckBoxAttributeAssociation.TabIndex = 6
        Me.CheckBoxAttributeAssociation.Text = "Create attribute association"
        Me.CheckBoxAttributeAssociation.UseVisualStyleBackColor = True
        '
        'ListBoxType
        '
        Me.ListBoxType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBoxType.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.ListBoxType.FormattingEnabled = True
        Me.ListBoxType.ItemHeight = 16
        Me.ListBoxType.Items.AddRange(New Object() {"Class", "Interface", "Table", "ArchiMate_DataObject", "ArchiMate_BusinessObject", "XSD", "User-Interface"})
        Me.ListBoxType.Location = New System.Drawing.Point(17, 33)
        Me.ListBoxType.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ListBoxType.Name = "ListBoxType"
        Me.ListBoxType.Size = New System.Drawing.Size(483, 100)
        Me.ListBoxType.TabIndex = 5
        '
        'ButtonGenerate
        '
        Me.ButtonGenerate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonGenerate.Location = New System.Drawing.Point(312, 393)
        Me.ButtonGenerate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ButtonGenerate.Name = "ButtonGenerate"
        Me.ButtonGenerate.Size = New System.Drawing.Size(189, 39)
        Me.ButtonGenerate.TabIndex = 4
        Me.ButtonGenerate.Text = "Generate attributes"
        Me.ButtonGenerate.UseVisualStyleBackColor = True
        '
        'ComboBoxExistingEntity
        '
        Me.ComboBoxExistingEntity.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxExistingEntity.FormattingEnabled = True
        Me.ComboBoxExistingEntity.Location = New System.Drawing.Point(153, 209)
        Me.ComboBoxExistingEntity.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ComboBoxExistingEntity.Name = "ComboBoxExistingEntity"
        Me.ComboBoxExistingEntity.Size = New System.Drawing.Size(347, 24)
        Me.ComboBoxExistingEntity.TabIndex = 3
        '
        'RadioButtonExistingEntity
        '
        Me.RadioButtonExistingEntity.AutoSize = True
        Me.RadioButtonExistingEntity.Location = New System.Drawing.Point(20, 208)
        Me.RadioButtonExistingEntity.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RadioButtonExistingEntity.Name = "RadioButtonExistingEntity"
        Me.RadioButtonExistingEntity.Size = New System.Drawing.Size(116, 21)
        Me.RadioButtonExistingEntity.TabIndex = 2
        Me.RadioButtonExistingEntity.Text = "Existing Entity"
        Me.RadioButtonExistingEntity.UseVisualStyleBackColor = True
        '
        'TextBoxNewEntity
        '
        Me.TextBoxNewEntity.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNewEntity.Location = New System.Drawing.Point(153, 150)
        Me.TextBoxNewEntity.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TextBoxNewEntity.Name = "TextBoxNewEntity"
        Me.TextBoxNewEntity.Size = New System.Drawing.Size(343, 22)
        Me.TextBoxNewEntity.TabIndex = 1
        '
        'RadioButtonNewEntity
        '
        Me.RadioButtonNewEntity.AutoSize = True
        Me.RadioButtonNewEntity.Checked = True
        Me.RadioButtonNewEntity.Location = New System.Drawing.Point(17, 150)
        Me.RadioButtonNewEntity.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RadioButtonNewEntity.Name = "RadioButtonNewEntity"
        Me.RadioButtonNewEntity.Size = New System.Drawing.Size(95, 21)
        Me.RadioButtonNewEntity.TabIndex = 0
        Me.RadioButtonNewEntity.TabStop = True
        Me.RadioButtonNewEntity.Text = "New Entity"
        Me.RadioButtonNewEntity.UseVisualStyleBackColor = True
        '
        'TabPageRefactor
        '
        Me.TabPageRefactor.BackColor = System.Drawing.Color.Gainsboro
        Me.TabPageRefactor.Controls.Add(Me.CheckBoxSpecialisation)
        Me.TabPageRefactor.Controls.Add(Me.CheckBoxRefactor)
        Me.TabPageRefactor.Controls.Add(Me.ComboBoxExistingRefactor)
        Me.TabPageRefactor.Controls.Add(Me.RadioButtonExistingRefactor)
        Me.TabPageRefactor.Controls.Add(Me.TextBoxNewRefactor)
        Me.TabPageRefactor.Controls.Add(Me.RadioButtonNewEntityRefactor)
        Me.TabPageRefactor.Controls.Add(Me.ButtonRefactor)
        Me.TabPageRefactor.Controls.Add(Me.Label4)
        Me.TabPageRefactor.Location = New System.Drawing.Point(4, 25)
        Me.TabPageRefactor.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TabPageRefactor.Name = "TabPageRefactor"
        Me.TabPageRefactor.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TabPageRefactor.Size = New System.Drawing.Size(515, 441)
        Me.TabPageRefactor.TabIndex = 1
        Me.TabPageRefactor.Text = "Refactoring"
        '
        'CheckBoxSpecialisation
        '
        Me.CheckBoxSpecialisation.AutoSize = True
        Me.CheckBoxSpecialisation.Location = New System.Drawing.Point(17, 150)
        Me.CheckBoxSpecialisation.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CheckBoxSpecialisation.Name = "CheckBoxSpecialisation"
        Me.CheckBoxSpecialisation.Size = New System.Drawing.Size(236, 21)
        Me.CheckBoxSpecialisation.TabIndex = 15
        Me.CheckBoxSpecialisation.Text = "Create specialisation association"
        Me.CheckBoxSpecialisation.UseVisualStyleBackColor = True
        '
        'CheckBoxRefactor
        '
        Me.CheckBoxRefactor.AutoSize = True
        Me.CheckBoxRefactor.Checked = True
        Me.CheckBoxRefactor.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxRefactor.Location = New System.Drawing.Point(17, 122)
        Me.CheckBoxRefactor.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CheckBoxRefactor.Name = "CheckBoxRefactor"
        Me.CheckBoxRefactor.Size = New System.Drawing.Size(195, 21)
        Me.CheckBoxRefactor.TabIndex = 14
        Me.CheckBoxRefactor.Text = "Refactor (move) attributes"
        Me.CheckBoxRefactor.UseVisualStyleBackColor = True
        '
        'ComboBoxExistingRefactor
        '
        Me.ComboBoxExistingRefactor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxExistingRefactor.FormattingEnabled = True
        Me.ComboBoxExistingRefactor.Location = New System.Drawing.Point(153, 55)
        Me.ComboBoxExistingRefactor.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ComboBoxExistingRefactor.Name = "ComboBoxExistingRefactor"
        Me.ComboBoxExistingRefactor.Size = New System.Drawing.Size(347, 24)
        Me.ComboBoxExistingRefactor.TabIndex = 13
        '
        'RadioButtonExistingRefactor
        '
        Me.RadioButtonExistingRefactor.AutoSize = True
        Me.RadioButtonExistingRefactor.Location = New System.Drawing.Point(17, 55)
        Me.RadioButtonExistingRefactor.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RadioButtonExistingRefactor.Name = "RadioButtonExistingRefactor"
        Me.RadioButtonExistingRefactor.Size = New System.Drawing.Size(116, 21)
        Me.RadioButtonExistingRefactor.TabIndex = 12
        Me.RadioButtonExistingRefactor.TabStop = True
        Me.RadioButtonExistingRefactor.Text = "Existing Entity"
        Me.RadioButtonExistingRefactor.UseVisualStyleBackColor = True
        '
        'TextBoxNewRefactor
        '
        Me.TextBoxNewRefactor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNewRefactor.Location = New System.Drawing.Point(153, 20)
        Me.TextBoxNewRefactor.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TextBoxNewRefactor.Name = "TextBoxNewRefactor"
        Me.TextBoxNewRefactor.Size = New System.Drawing.Size(347, 22)
        Me.TextBoxNewRefactor.TabIndex = 11
        '
        'RadioButtonNewEntityRefactor
        '
        Me.RadioButtonNewEntityRefactor.AutoSize = True
        Me.RadioButtonNewEntityRefactor.Location = New System.Drawing.Point(17, 20)
        Me.RadioButtonNewEntityRefactor.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RadioButtonNewEntityRefactor.Name = "RadioButtonNewEntityRefactor"
        Me.RadioButtonNewEntityRefactor.Size = New System.Drawing.Size(95, 21)
        Me.RadioButtonNewEntityRefactor.TabIndex = 10
        Me.RadioButtonNewEntityRefactor.TabStop = True
        Me.RadioButtonNewEntityRefactor.Text = "New Entity"
        Me.RadioButtonNewEntityRefactor.UseVisualStyleBackColor = True
        '
        'ButtonRefactor
        '
        Me.ButtonRefactor.Location = New System.Drawing.Point(363, 332)
        Me.ButtonRefactor.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ButtonRefactor.Name = "ButtonRefactor"
        Me.ButtonRefactor.Size = New System.Drawing.Size(129, 43)
        Me.ButtonRefactor.TabIndex = 9
        Me.ButtonRefactor.Text = "Refactor"
        Me.ButtonRefactor.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.LemonChiffon
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label4.Location = New System.Drawing.Point(8, 220)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(484, 84)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "With this option you can copy or move selected attributes (as available in the ch" &
    "eckboxlistbox) to an existing entity or a new one for attribute refactoring in o" &
    "ne easy step."
        '
        'TabPageExtra
        '
        Me.TabPageExtra.BackColor = System.Drawing.Color.LightGray
        Me.TabPageExtra.Controls.Add(Me.Label9)
        Me.TabPageExtra.Controls.Add(Me.Label8)
        Me.TabPageExtra.Controls.Add(Me.Label7)
        Me.TabPageExtra.Controls.Add(Me.ButtonAlias)
        Me.TabPageExtra.Controls.Add(Me.ButtonOperator)
        Me.TabPageExtra.Controls.Add(Me.ButtonControlType)
        Me.TabPageExtra.Location = New System.Drawing.Point(4, 25)
        Me.TabPageExtra.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPageExtra.Name = "TabPageExtra"
        Me.TabPageExtra.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TabPageExtra.Size = New System.Drawing.Size(515, 441)
        Me.TabPageExtra.TabIndex = 2
        Me.TabPageExtra.Text = "Extra"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.LemonChiffon
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label9.Location = New System.Drawing.Point(179, 150)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(327, 43)
        Me.Label9.TabIndex = 11
        Me.Label9.Text = "Create an alias for the name for user readability"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.LemonChiffon
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label8.Location = New System.Drawing.Point(179, 87)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(327, 43)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "Create a tagged value with an operator for the form factory"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.LemonChiffon
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label7.Location = New System.Drawing.Point(179, 22)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(327, 43)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "Create a tagged value with a control type for the form factory"
        '
        'ButtonAlias
        '
        Me.ButtonAlias.Location = New System.Drawing.Point(21, 150)
        Me.ButtonAlias.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ButtonAlias.Name = "ButtonAlias"
        Me.ButtonAlias.Size = New System.Drawing.Size(124, 46)
        Me.ButtonAlias.TabIndex = 2
        Me.ButtonAlias.Text = "Set Alias"
        Me.ButtonAlias.UseVisualStyleBackColor = True
        '
        'ButtonOperator
        '
        Me.ButtonOperator.Location = New System.Drawing.Point(21, 87)
        Me.ButtonOperator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ButtonOperator.Name = "ButtonOperator"
        Me.ButtonOperator.Size = New System.Drawing.Size(124, 43)
        Me.ButtonOperator.TabIndex = 1
        Me.ButtonOperator.Text = "Set Operator"
        Me.ButtonOperator.UseVisualStyleBackColor = True
        '
        'ButtonControlType
        '
        Me.ButtonControlType.Location = New System.Drawing.Point(24, 22)
        Me.ButtonControlType.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ButtonControlType.Name = "ButtonControlType"
        Me.ButtonControlType.Size = New System.Drawing.Size(121, 43)
        Me.ButtonControlType.TabIndex = 0
        Me.ButtonControlType.Text = "Set Controltype"
        Me.ButtonControlType.UseVisualStyleBackColor = True
        '
        'ListBoxAttributes
        '
        Me.ListBoxAttributes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ListBoxAttributes.FormattingEnabled = True
        Me.ListBoxAttributes.Location = New System.Drawing.Point(16, 112)
        Me.ListBoxAttributes.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ListBoxAttributes.Name = "ListBoxAttributes"
        Me.ListBoxAttributes.Size = New System.Drawing.Size(344, 293)
        Me.ListBoxAttributes.TabIndex = 6
        '
        'ButtonSelectAll
        '
        Me.ButtonSelectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonSelectAll.Location = New System.Drawing.Point(13, 430)
        Me.ButtonSelectAll.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ButtonSelectAll.Name = "ButtonSelectAll"
        Me.ButtonSelectAll.Size = New System.Drawing.Size(100, 28)
        Me.ButtonSelectAll.TabIndex = 7
        Me.ButtonSelectAll.Text = "Select All"
        Me.ButtonSelectAll.UseVisualStyleBackColor = True
        '
        'ButtonToggleAll
        '
        Me.ButtonToggleAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonToggleAll.Location = New System.Drawing.Point(133, 430)
        Me.ButtonToggleAll.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ButtonToggleAll.Name = "ButtonToggleAll"
        Me.ButtonToggleAll.Size = New System.Drawing.Size(100, 28)
        Me.ButtonToggleAll.TabIndex = 8
        Me.ButtonToggleAll.Text = "Toggle"
        Me.ButtonToggleAll.UseVisualStyleBackColor = True
        '
        'ButtonUnselectAll
        '
        Me.ButtonUnselectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonUnselectAll.Location = New System.Drawing.Point(259, 430)
        Me.ButtonUnselectAll.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ButtonUnselectAll.Name = "ButtonUnselectAll"
        Me.ButtonUnselectAll.Size = New System.Drawing.Size(100, 28)
        Me.ButtonUnselectAll.TabIndex = 9
        Me.ButtonUnselectAll.Text = "Unselect All"
        Me.ButtonUnselectAll.UseVisualStyleBackColor = True
        '
        'FrmIDEAClass
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(912, 470)
        Me.Controls.Add(Me.ButtonUnselectAll)
        Me.Controls.Add(Me.ButtonToggleAll)
        Me.Controls.Add(Me.ButtonSelectAll)
        Me.Controls.Add(Me.ListBoxAttributes)
        Me.Controls.Add(Me.TabControlActions)
        Me.Controls.Add(Me.ButtonLoad)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComboBoxElement)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "FrmIDEAClass"
        Me.Text = "IDEA Class helper"
        Me.TabControlActions.ResumeLayout(False)
        Me.TabPageGenerate.ResumeLayout(False)
        Me.TabPageGenerate.PerformLayout()
        Me.TabPageRefactor.ResumeLayout(False)
        Me.TabPageRefactor.PerformLayout()
        Me.TabPageExtra.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ComboBoxElement As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ButtonLoad As System.Windows.Forms.Button
    Friend WithEvents TabControlActions As System.Windows.Forms.TabControl
    Friend WithEvents TabPageGenerate As System.Windows.Forms.TabPage
    Friend WithEvents TabPageRefactor As System.Windows.Forms.TabPage
    Friend WithEvents ListBoxType As System.Windows.Forms.ListBox
    Friend WithEvents ButtonGenerate As System.Windows.Forms.Button
    Friend WithEvents ComboBoxExistingEntity As System.Windows.Forms.ComboBox
    Friend WithEvents RadioButtonExistingEntity As System.Windows.Forms.RadioButton
    Friend WithEvents TextBoxNewEntity As System.Windows.Forms.TextBox
    Friend WithEvents RadioButtonNewEntity As System.Windows.Forms.RadioButton
    Friend WithEvents ListBoxAttributes As System.Windows.Forms.CheckedListBox
    Friend WithEvents CheckBoxAttributeAssociation As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxExistingRefactor As System.Windows.Forms.ComboBox
    Friend WithEvents RadioButtonExistingRefactor As System.Windows.Forms.RadioButton
    Friend WithEvents TextBoxNewRefactor As System.Windows.Forms.TextBox
    Friend WithEvents RadioButtonNewEntityRefactor As System.Windows.Forms.RadioButton
    Friend WithEvents ButtonRefactor As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxRefactor As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonSelectAll As System.Windows.Forms.Button
    Friend WithEvents ButtonToggleAll As System.Windows.Forms.Button
    Friend WithEvents ButtonUnselectAll As System.Windows.Forms.Button
    Friend WithEvents CheckBoxSpecialisation As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxTargetPackage As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxAddAlias As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxOperator As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxControlType As System.Windows.Forms.CheckBox
    Friend WithEvents TabPageExtra As System.Windows.Forms.TabPage
    Friend WithEvents ButtonOperator As System.Windows.Forms.Button
    Friend WithEvents ButtonControlType As System.Windows.Forms.Button
    Friend WithEvents ButtonAlias As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
End Class
