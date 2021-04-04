''' <summary>
''' Form for IDEA routines specific for UML class entities. For every type of
''' element a specific form is generated. This makes working with the IDEA AddOn
''' easier.
''' </summary>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class FrmIdeaDiagram
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmIdeaDiagram))
        Me.ButtonUnselectAll = New System.Windows.Forms.Button()
        Me.ButtonToggleAll = New System.Windows.Forms.Button()
        Me.ButtonSelectAll = New System.Windows.Forms.Button()
        Me.ListBoxElements = New System.Windows.Forms.CheckedListBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LabelDiagramName = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxPrefix = New System.Windows.Forms.TextBox()
        Me.ListBoxType = New System.Windows.Forms.ListBox()
        Me.ButtonGenerate = New System.Windows.Forms.Button()
        Me.CheckBoxAttributeAssociation = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.CheckBoxRestoreColor = New System.Windows.Forms.CheckBox()
        Me.ButtonArchiMateColoring = New System.Windows.Forms.Button()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.ButtonElement2Original = New System.Windows.Forms.Button()
        Me.CheckBoxOriginalPackage = New System.Windows.Forms.CheckBox()
        Me.ComboBoxSecGroup = New System.Windows.Forms.ComboBox()
        Me.CheckBoxUserLock = New System.Windows.Forms.CheckBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.ButtonLockElements = New System.Windows.Forms.Button()
        Me.CheckBoxMakeHidden = New System.Windows.Forms.CheckBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.ButtonShowHidden = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.ButtonCollectElements = New System.Windows.Forms.Button()
        Me.CheckBoxCloseWindowExtraReady = New System.Windows.Forms.CheckBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.ButtonHideEmbedded = New System.Windows.Forms.Button()
        Me.RadioButtonOrthogonalRounded = New System.Windows.Forms.RadioButton()
        Me.RadioButtonLateralVertical = New System.Windows.Forms.RadioButton()
        Me.RadioButtonTreeVertical = New System.Windows.Forms.RadioButton()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.RadioButtonAutoroute = New System.Windows.Forms.RadioButton()
        Me.RadioButtonDirect = New System.Windows.Forms.RadioButton()
        Me.ButtonMappingStyle = New System.Windows.Forms.Button()
        Me.CheckBoxIsHidden = New System.Windows.Forms.CheckBox()
        Me.ButtonSwitchMapping = New System.Windows.Forms.Button()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.TextBoxTemplate = New System.Windows.Forms.TextBox()
        Me.TextBoxStatement = New System.Windows.Forms.TextBox()
        Me.DataGridViewStatement = New System.Windows.Forms.DataGridView()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.ComboBoxStatement = New System.Windows.Forms.ComboBox()
        Me.ButtonMakeSQL = New System.Windows.Forms.Button()
        Me.ButtonLoad = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.TextBoxStereoType = New System.Windows.Forms.TextBox()
        Me.CheckBoxGenerateName = New System.Windows.Forms.CheckBox()
        Me.ButtonMatchNames = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.CheckBoxLoadData = New System.Windows.Forms.CheckBox()
        Me.CheckBoxMapTarget = New System.Windows.Forms.CheckBox()
        Me.ButtonLoadMappings = New System.Windows.Forms.Button()
        Me.ButtonGenerateMapping = New System.Windows.Forms.Button()
        Me.ButtonMappingTarget = New System.Windows.Forms.Button()
        Me.ButtonMappingSource = New System.Windows.Forms.Button()
        Me.DataGridViewMapping = New System.Windows.Forms.DataGridView()
        Me.ListBoxMappingElement = New System.Windows.Forms.CheckedListBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.LabelMappingDiagramName = New System.Windows.Forms.Label()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.ComboBoxTargetPackage = New System.Windows.Forms.ComboBox()
        Me.ButtonUnselectGenerate = New System.Windows.Forms.Button()
        Me.ButtonToggleGenerate = New System.Windows.Forms.Button()
        Me.ButtonSelectAllGenerate = New System.Windows.Forms.Button()
        Me.ButtonGenerateItems = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.SaveSQLFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.TabControl1.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        CType(Me.DataGridViewStatement, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.DataGridViewMapping, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonUnselectAll
        '
        Me.ButtonUnselectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonUnselectAll.Location = New System.Drawing.Point(199, 482)
        Me.ButtonUnselectAll.Name = "ButtonUnselectAll"
        Me.ButtonUnselectAll.Size = New System.Drawing.Size(75, 23)
        Me.ButtonUnselectAll.TabIndex = 21
        Me.ButtonUnselectAll.Text = "Unselect All"
        Me.ButtonUnselectAll.UseVisualStyleBackColor = True
        '
        'ButtonToggleAll
        '
        Me.ButtonToggleAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonToggleAll.Location = New System.Drawing.Point(106, 482)
        Me.ButtonToggleAll.Name = "ButtonToggleAll"
        Me.ButtonToggleAll.Size = New System.Drawing.Size(75, 23)
        Me.ButtonToggleAll.TabIndex = 20
        Me.ButtonToggleAll.Text = "Toggle"
        Me.ButtonToggleAll.UseVisualStyleBackColor = True
        '
        'ButtonSelectAll
        '
        Me.ButtonSelectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonSelectAll.Location = New System.Drawing.Point(15, 482)
        Me.ButtonSelectAll.Name = "ButtonSelectAll"
        Me.ButtonSelectAll.Size = New System.Drawing.Size(75, 23)
        Me.ButtonSelectAll.TabIndex = 19
        Me.ButtonSelectAll.Text = "Select All"
        Me.ButtonSelectAll.UseVisualStyleBackColor = True
        '
        'ListBoxElements
        '
        Me.ListBoxElements.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBoxElements.FormattingEnabled = True
        Me.ListBoxElements.Location = New System.Drawing.Point(15, 71)
        Me.ListBoxElements.Name = "ListBoxElements"
        Me.ListBoxElements.Size = New System.Drawing.Size(384, 349)
        Me.ListBoxElements.TabIndex = 18
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(94, 13)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Selected elements"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 13)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Selected diagram"
        '
        'LabelDiagramName
        '
        Me.LabelDiagramName.AutoSize = True
        Me.LabelDiagramName.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelDiagramName.Location = New System.Drawing.Point(110, 15)
        Me.LabelDiagramName.Name = "LabelDiagramName"
        Me.LabelDiagramName.Size = New System.Drawing.Size(120, 18)
        Me.LabelDiagramName.TabIndex = 22
        Me.LabelDiagramName.Text = "Diagram Name"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(430, 41)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(33, 13)
        Me.Label3.TabIndex = 23
        Me.Label3.Text = "Prefix"
        '
        'TextBoxPrefix
        '
        Me.TextBoxPrefix.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxPrefix.Location = New System.Drawing.Point(431, 61)
        Me.TextBoxPrefix.Name = "TextBoxPrefix"
        Me.TextBoxPrefix.Size = New System.Drawing.Size(276, 20)
        Me.TextBoxPrefix.TabIndex = 24
        '
        'ListBoxType
        '
        Me.ListBoxType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBoxType.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.ListBoxType.FormattingEnabled = True
        Me.ListBoxType.Items.AddRange(New Object() {"Class", "Interface", "Table", "XSD"})
        Me.ListBoxType.Location = New System.Drawing.Point(432, 99)
        Me.ListBoxType.Name = "ListBoxType"
        Me.ListBoxType.Size = New System.Drawing.Size(273, 82)
        Me.ListBoxType.TabIndex = 25
        '
        'ButtonGenerate
        '
        Me.ButtonGenerate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonGenerate.Location = New System.Drawing.Point(492, 474)
        Me.ButtonGenerate.Name = "ButtonGenerate"
        Me.ButtonGenerate.Size = New System.Drawing.Size(214, 32)
        Me.ButtonGenerate.TabIndex = 26
        Me.ButtonGenerate.Text = "Generate elements and attributes"
        Me.ButtonGenerate.UseVisualStyleBackColor = True
        '
        'CheckBoxAttributeAssociation
        '
        Me.CheckBoxAttributeAssociation.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxAttributeAssociation.AutoSize = True
        Me.CheckBoxAttributeAssociation.Checked = True
        Me.CheckBoxAttributeAssociation.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxAttributeAssociation.Location = New System.Drawing.Point(431, 197)
        Me.CheckBoxAttributeAssociation.Name = "CheckBoxAttributeAssociation"
        Me.CheckBoxAttributeAssociation.Size = New System.Drawing.Size(154, 17)
        Me.CheckBoxAttributeAssociation.TabIndex = 27
        Me.CheckBoxAttributeAssociation.Text = "Create attribute association"
        Me.CheckBoxAttributeAssociation.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.BackColor = System.Drawing.Color.LemonChiffon
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label4.Location = New System.Drawing.Point(431, 396)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(272, 51)
        Me.Label4.TabIndex = 28
        Me.Label4.Text = "With this option you can generate new items of a type based on the elements found" &
    " in the selected diagram in one easy step."
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(723, 487)
        Me.TabControl1.TabIndex = 29
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.Color.LightGray
        Me.TabPage3.Controls.Add(Me.Label14)
        Me.TabPage3.Controls.Add(Me.CheckBoxRestoreColor)
        Me.TabPage3.Controls.Add(Me.ButtonArchiMateColoring)
        Me.TabPage3.Controls.Add(Me.Label20)
        Me.TabPage3.Controls.Add(Me.ButtonElement2Original)
        Me.TabPage3.Controls.Add(Me.CheckBoxOriginalPackage)
        Me.TabPage3.Controls.Add(Me.ComboBoxSecGroup)
        Me.TabPage3.Controls.Add(Me.CheckBoxUserLock)
        Me.TabPage3.Controls.Add(Me.Label19)
        Me.TabPage3.Controls.Add(Me.ButtonLockElements)
        Me.TabPage3.Controls.Add(Me.CheckBoxMakeHidden)
        Me.TabPage3.Controls.Add(Me.Label12)
        Me.TabPage3.Controls.Add(Me.ButtonShowHidden)
        Me.TabPage3.Controls.Add(Me.Label11)
        Me.TabPage3.Controls.Add(Me.ButtonCollectElements)
        Me.TabPage3.Controls.Add(Me.CheckBoxCloseWindowExtraReady)
        Me.TabPage3.Controls.Add(Me.Label10)
        Me.TabPage3.Controls.Add(Me.ButtonHideEmbedded)
        Me.TabPage3.Controls.Add(Me.RadioButtonOrthogonalRounded)
        Me.TabPage3.Controls.Add(Me.RadioButtonLateralVertical)
        Me.TabPage3.Controls.Add(Me.RadioButtonTreeVertical)
        Me.TabPage3.Controls.Add(Me.Label9)
        Me.TabPage3.Controls.Add(Me.Label8)
        Me.TabPage3.Controls.Add(Me.RadioButtonAutoroute)
        Me.TabPage3.Controls.Add(Me.RadioButtonDirect)
        Me.TabPage3.Controls.Add(Me.ButtonMappingStyle)
        Me.TabPage3.Controls.Add(Me.CheckBoxIsHidden)
        Me.TabPage3.Controls.Add(Me.ButtonSwitchMapping)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPage3.Size = New System.Drawing.Size(715, 461)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Helper"
        '
        'Label14
        '
        Me.Label14.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label14.BackColor = System.Drawing.Color.LemonChiffon
        Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label14.Location = New System.Drawing.Point(321, 335)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(385, 28)
        Me.Label14.TabIndex = 51
        Me.Label14.Text = "Show elements border in ArchiMate aspect color"
        '
        'CheckBoxRestoreColor
        '
        Me.CheckBoxRestoreColor.AutoSize = True
        Me.CheckBoxRestoreColor.Location = New System.Drawing.Point(180, 341)
        Me.CheckBoxRestoreColor.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxRestoreColor.Name = "CheckBoxRestoreColor"
        Me.CheckBoxRestoreColor.Size = New System.Drawing.Size(90, 17)
        Me.CheckBoxRestoreColor.TabIndex = 50
        Me.CheckBoxRestoreColor.Text = "Restore Color"
        Me.CheckBoxRestoreColor.UseVisualStyleBackColor = True
        '
        'ButtonArchiMateColoring
        '
        Me.ButtonArchiMateColoring.Location = New System.Drawing.Point(15, 334)
        Me.ButtonArchiMateColoring.Name = "ButtonArchiMateColoring"
        Me.ButtonArchiMateColoring.Size = New System.Drawing.Size(160, 29)
        Me.ButtonArchiMateColoring.TabIndex = 49
        Me.ButtonArchiMateColoring.Text = "ArchiMate Coloring"
        Me.ButtonArchiMateColoring.UseVisualStyleBackColor = True
        '
        'Label20
        '
        Me.Label20.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label20.BackColor = System.Drawing.Color.LemonChiffon
        Me.Label20.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label20.Location = New System.Drawing.Point(321, 254)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(385, 28)
        Me.Label20.TabIndex = 48
        Me.Label20.Text = "Return all the elements to the original package"
        '
        'ButtonElement2Original
        '
        Me.ButtonElement2Original.Location = New System.Drawing.Point(15, 254)
        Me.ButtonElement2Original.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonElement2Original.Name = "ButtonElement2Original"
        Me.ButtonElement2Original.Size = New System.Drawing.Size(160, 28)
        Me.ButtonElement2Original.TabIndex = 47
        Me.ButtonElement2Original.Text = "Elements to original"
        Me.ButtonElement2Original.UseVisualStyleBackColor = True
        '
        'CheckBoxOriginalPackage
        '
        Me.CheckBoxOriginalPackage.AutoSize = True
        Me.CheckBoxOriginalPackage.Location = New System.Drawing.Point(180, 219)
        Me.CheckBoxOriginalPackage.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxOriginalPackage.Name = "CheckBoxOriginalPackage"
        Me.CheckBoxOriginalPackage.Size = New System.Drawing.Size(132, 17)
        Me.CheckBoxOriginalPackage.TabIndex = 46
        Me.CheckBoxOriginalPackage.Text = "Store original package"
        Me.CheckBoxOriginalPackage.UseVisualStyleBackColor = True
        '
        'ComboBoxSecGroup
        '
        Me.ComboBoxSecGroup.FormattingEnabled = True
        Me.ComboBoxSecGroup.Location = New System.Drawing.Point(15, 415)
        Me.ComboBoxSecGroup.Name = "ComboBoxSecGroup"
        Me.ComboBoxSecGroup.Size = New System.Drawing.Size(306, 21)
        Me.ComboBoxSecGroup.TabIndex = 45
        '
        'CheckBoxUserLock
        '
        Me.CheckBoxUserLock.AutoSize = True
        Me.CheckBoxUserLock.Location = New System.Drawing.Point(180, 385)
        Me.CheckBoxUserLock.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxUserLock.Name = "CheckBoxUserLock"
        Me.CheckBoxUserLock.Size = New System.Drawing.Size(71, 17)
        Me.CheckBoxUserLock.TabIndex = 44
        Me.CheckBoxUserLock.Text = "User lock"
        Me.CheckBoxUserLock.UseVisualStyleBackColor = True
        '
        'Label19
        '
        Me.Label19.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label19.BackColor = System.Drawing.Color.LemonChiffon
        Me.Label19.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label19.Location = New System.Drawing.Point(321, 381)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(385, 27)
        Me.Label19.TabIndex = 43
        Me.Label19.Text = "Lock the elements displayed on the diagram"
        '
        'ButtonLockElements
        '
        Me.ButtonLockElements.Location = New System.Drawing.Point(15, 378)
        Me.ButtonLockElements.Name = "ButtonLockElements"
        Me.ButtonLockElements.Size = New System.Drawing.Size(160, 29)
        Me.ButtonLockElements.TabIndex = 42
        Me.ButtonLockElements.Text = "Lock elements on diagram"
        Me.ButtonLockElements.UseVisualStyleBackColor = True
        '
        'CheckBoxMakeHidden
        '
        Me.CheckBoxMakeHidden.AutoSize = True
        Me.CheckBoxMakeHidden.Location = New System.Drawing.Point(180, 298)
        Me.CheckBoxMakeHidden.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxMakeHidden.Name = "CheckBoxMakeHidden"
        Me.CheckBoxMakeHidden.Size = New System.Drawing.Size(139, 17)
        Me.CheckBoxMakeHidden.TabIndex = 41
        Me.CheckBoxMakeHidden.Text = "Make connector hidden"
        Me.CheckBoxMakeHidden.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.BackColor = System.Drawing.Color.LemonChiffon
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label12.Location = New System.Drawing.Point(321, 292)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(385, 28)
        Me.Label12.TabIndex = 40
        Me.Label12.Text = "Show connectors on this diagram that are not visible in bold and in red"
        '
        'ButtonShowHidden
        '
        Me.ButtonShowHidden.Location = New System.Drawing.Point(15, 292)
        Me.ButtonShowHidden.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonShowHidden.Name = "ButtonShowHidden"
        Me.ButtonShowHidden.Size = New System.Drawing.Size(160, 28)
        Me.ButtonShowHidden.TabIndex = 39
        Me.ButtonShowHidden.Text = "Show hidden connectors"
        Me.ButtonShowHidden.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.BackColor = System.Drawing.Color.LemonChiffon
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label11.Location = New System.Drawing.Point(322, 212)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(385, 36)
        Me.Label11.TabIndex = 38
        Me.Label11.Text = "Bring all the elements displayed on the diagram to the same package as the diagra" &
    "m."
        '
        'ButtonCollectElements
        '
        Me.ButtonCollectElements.Location = New System.Drawing.Point(15, 212)
        Me.ButtonCollectElements.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonCollectElements.Name = "ButtonCollectElements"
        Me.ButtonCollectElements.Size = New System.Drawing.Size(160, 28)
        Me.ButtonCollectElements.TabIndex = 37
        Me.ButtonCollectElements.Text = "Collect Elements for Diagram"
        Me.ButtonCollectElements.UseVisualStyleBackColor = True
        '
        'CheckBoxCloseWindowExtraReady
        '
        Me.CheckBoxCloseWindowExtraReady.AutoSize = True
        Me.CheckBoxCloseWindowExtraReady.Checked = True
        Me.CheckBoxCloseWindowExtraReady.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxCloseWindowExtraReady.Location = New System.Drawing.Point(15, 442)
        Me.CheckBoxCloseWindowExtraReady.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxCloseWindowExtraReady.Name = "CheckBoxCloseWindowExtraReady"
        Me.CheckBoxCloseWindowExtraReady.Size = New System.Drawing.Size(149, 17)
        Me.CheckBoxCloseWindowExtraReady.TabIndex = 36
        Me.CheckBoxCloseWindowExtraReady.Text = "Close window when ready"
        Me.CheckBoxCloseWindowExtraReady.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.BackColor = System.Drawing.Color.LemonChiffon
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label10.Location = New System.Drawing.Point(321, 169)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(385, 30)
        Me.Label10.TabIndex = 35
        Me.Label10.Text = "Hide assocations for entities that are  included in the borders of another entity" &
    ", otherwise show the association"
        '
        'ButtonHideEmbedded
        '
        Me.ButtonHideEmbedded.Location = New System.Drawing.Point(15, 167)
        Me.ButtonHideEmbedded.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonHideEmbedded.Name = "ButtonHideEmbedded"
        Me.ButtonHideEmbedded.Size = New System.Drawing.Size(160, 32)
        Me.ButtonHideEmbedded.TabIndex = 34
        Me.ButtonHideEmbedded.Text = "Hide embedded associations"
        Me.ButtonHideEmbedded.UseVisualStyleBackColor = True
        '
        'RadioButtonOrthogonalRounded
        '
        Me.RadioButtonOrthogonalRounded.AutoSize = True
        Me.RadioButtonOrthogonalRounded.Location = New System.Drawing.Point(182, 141)
        Me.RadioButtonOrthogonalRounded.Margin = New System.Windows.Forms.Padding(2)
        Me.RadioButtonOrthogonalRounded.Name = "RadioButtonOrthogonalRounded"
        Me.RadioButtonOrthogonalRounded.Size = New System.Drawing.Size(124, 17)
        Me.RadioButtonOrthogonalRounded.TabIndex = 33
        Me.RadioButtonOrthogonalRounded.Tag = "3"
        Me.RadioButtonOrthogonalRounded.Text = "Orthogonal Rounded"
        Me.RadioButtonOrthogonalRounded.UseVisualStyleBackColor = True
        '
        'RadioButtonLateralVertical
        '
        Me.RadioButtonLateralVertical.AutoSize = True
        Me.RadioButtonLateralVertical.Location = New System.Drawing.Point(182, 119)
        Me.RadioButtonLateralVertical.Margin = New System.Windows.Forms.Padding(2)
        Me.RadioButtonLateralVertical.Name = "RadioButtonLateralVertical"
        Me.RadioButtonLateralVertical.Size = New System.Drawing.Size(94, 17)
        Me.RadioButtonLateralVertical.TabIndex = 32
        Me.RadioButtonLateralVertical.Tag = "3"
        Me.RadioButtonLateralVertical.Text = "Lateral vertical"
        Me.RadioButtonLateralVertical.UseVisualStyleBackColor = True
        '
        'RadioButtonTreeVertical
        '
        Me.RadioButtonTreeVertical.AutoSize = True
        Me.RadioButtonTreeVertical.Location = New System.Drawing.Point(182, 96)
        Me.RadioButtonTreeVertical.Margin = New System.Windows.Forms.Padding(2)
        Me.RadioButtonTreeVertical.Name = "RadioButtonTreeVertical"
        Me.RadioButtonTreeVertical.Size = New System.Drawing.Size(84, 17)
        Me.RadioButtonTreeVertical.TabIndex = 31
        Me.RadioButtonTreeVertical.Tag = "3"
        Me.RadioButtonTreeVertical.Text = "Tree vertical"
        Me.RadioButtonTreeVertical.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.BackColor = System.Drawing.Color.LemonChiffon
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label9.Location = New System.Drawing.Point(322, 44)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(385, 31)
        Me.Label9.TabIndex = 30
        Me.Label9.Text = "Helper to toggle the association style"
        '
        'Label8
        '
        Me.Label8.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.BackColor = System.Drawing.Color.LemonChiffon
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label8.Location = New System.Drawing.Point(321, 11)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(386, 29)
        Me.Label8.TabIndex = 29
        Me.Label8.Text = "Helper to toggle the visibility for attribute associations"
        '
        'RadioButtonAutoroute
        '
        Me.RadioButtonAutoroute.AutoSize = True
        Me.RadioButtonAutoroute.Location = New System.Drawing.Point(182, 73)
        Me.RadioButtonAutoroute.Margin = New System.Windows.Forms.Padding(2)
        Me.RadioButtonAutoroute.Name = "RadioButtonAutoroute"
        Me.RadioButtonAutoroute.Size = New System.Drawing.Size(71, 17)
        Me.RadioButtonAutoroute.TabIndex = 4
        Me.RadioButtonAutoroute.Tag = "3"
        Me.RadioButtonAutoroute.Text = "Autoroute"
        Me.RadioButtonAutoroute.UseVisualStyleBackColor = True
        '
        'RadioButtonDirect
        '
        Me.RadioButtonDirect.AutoSize = True
        Me.RadioButtonDirect.Checked = True
        Me.RadioButtonDirect.Location = New System.Drawing.Point(182, 50)
        Me.RadioButtonDirect.Margin = New System.Windows.Forms.Padding(2)
        Me.RadioButtonDirect.Name = "RadioButtonDirect"
        Me.RadioButtonDirect.Size = New System.Drawing.Size(53, 17)
        Me.RadioButtonDirect.TabIndex = 3
        Me.RadioButtonDirect.TabStop = True
        Me.RadioButtonDirect.Tag = "1"
        Me.RadioButtonDirect.Text = "Direct"
        Me.RadioButtonDirect.UseVisualStyleBackColor = True
        '
        'ButtonMappingStyle
        '
        Me.ButtonMappingStyle.Location = New System.Drawing.Point(15, 44)
        Me.ButtonMappingStyle.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonMappingStyle.Name = "ButtonMappingStyle"
        Me.ButtonMappingStyle.Size = New System.Drawing.Size(160, 29)
        Me.ButtonMappingStyle.TabIndex = 2
        Me.ButtonMappingStyle.Text = "Set linestyle for all connectors"
        Me.ButtonMappingStyle.UseVisualStyleBackColor = True
        '
        'CheckBoxIsHidden
        '
        Me.CheckBoxIsHidden.AutoSize = True
        Me.CheckBoxIsHidden.Location = New System.Drawing.Point(181, 18)
        Me.CheckBoxIsHidden.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxIsHidden.Name = "CheckBoxIsHidden"
        Me.CheckBoxIsHidden.Size = New System.Drawing.Size(112, 17)
        Me.CheckBoxIsHidden.TabIndex = 1
        Me.CheckBoxIsHidden.Text = "Mapping is hidden"
        Me.CheckBoxIsHidden.UseVisualStyleBackColor = True
        '
        'ButtonSwitchMapping
        '
        Me.ButtonSwitchMapping.Location = New System.Drawing.Point(15, 11)
        Me.ButtonSwitchMapping.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonSwitchMapping.Name = "ButtonSwitchMapping"
        Me.ButtonSwitchMapping.Size = New System.Drawing.Size(160, 29)
        Me.ButtonSwitchMapping.TabIndex = 0
        Me.ButtonSwitchMapping.Text = "Switch visibility mapping"
        Me.ButtonSwitchMapping.UseVisualStyleBackColor = True
        '
        'TabPage4
        '
        Me.TabPage4.BackColor = System.Drawing.Color.LightGray
        Me.TabPage4.Controls.Add(Me.TextBoxTemplate)
        Me.TabPage4.Controls.Add(Me.TextBoxStatement)
        Me.TabPage4.Controls.Add(Me.DataGridViewStatement)
        Me.TabPage4.Controls.Add(Me.Label13)
        Me.TabPage4.Controls.Add(Me.ComboBoxStatement)
        Me.TabPage4.Controls.Add(Me.ButtonMakeSQL)
        Me.TabPage4.Controls.Add(Me.ButtonLoad)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPage4.Size = New System.Drawing.Size(715, 461)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "SQL generator"
        '
        'TextBoxTemplate
        '
        Me.TextBoxTemplate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTemplate.Location = New System.Drawing.Point(0, 165)
        Me.TextBoxTemplate.Multiline = True
        Me.TextBoxTemplate.Name = "TextBoxTemplate"
        Me.TextBoxTemplate.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBoxTemplate.Size = New System.Drawing.Size(715, 72)
        Me.TextBoxTemplate.TabIndex = 56
        '
        'TextBoxStatement
        '
        Me.TextBoxStatement.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxStatement.Location = New System.Drawing.Point(0, 87)
        Me.TextBoxStatement.Multiline = True
        Me.TextBoxStatement.Name = "TextBoxStatement"
        Me.TextBoxStatement.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBoxStatement.Size = New System.Drawing.Size(715, 72)
        Me.TextBoxStatement.TabIndex = 55
        '
        'DataGridViewStatement
        '
        Me.DataGridViewStatement.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewStatement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewStatement.Location = New System.Drawing.Point(0, 243)
        Me.DataGridViewStatement.Name = "DataGridViewStatement"
        Me.DataGridViewStatement.Size = New System.Drawing.Size(715, 218)
        Me.DataGridViewStatement.TabIndex = 54
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(7, 5)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(55, 13)
        Me.Label13.TabIndex = 53
        Me.Label13.Text = "Statement"
        '
        'ComboBoxStatement
        '
        Me.ComboBoxStatement.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxStatement.FormattingEnabled = True
        Me.ComboBoxStatement.Location = New System.Drawing.Point(0, 23)
        Me.ComboBoxStatement.Name = "ComboBoxStatement"
        Me.ComboBoxStatement.Size = New System.Drawing.Size(715, 21)
        Me.ComboBoxStatement.TabIndex = 52
        '
        'ButtonMakeSQL
        '
        Me.ButtonMakeSQL.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonMakeSQL.Location = New System.Drawing.Point(617, 50)
        Me.ButtonMakeSQL.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonMakeSQL.Name = "ButtonMakeSQL"
        Me.ButtonMakeSQL.Size = New System.Drawing.Size(94, 31)
        Me.ButtonMakeSQL.TabIndex = 47
        Me.ButtonMakeSQL.Text = "Make SQL"
        Me.ButtonMakeSQL.UseVisualStyleBackColor = True
        '
        'ButtonLoad
        '
        Me.ButtonLoad.Location = New System.Drawing.Point(4, 49)
        Me.ButtonLoad.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonLoad.Name = "ButtonLoad"
        Me.ButtonLoad.Size = New System.Drawing.Size(113, 32)
        Me.ButtonLoad.TabIndex = 45
        Me.ButtonLoad.Text = "Load Grid"
        Me.ButtonLoad.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.LightGray
        Me.TabPage2.Controls.Add(Me.Label18)
        Me.TabPage2.Controls.Add(Me.TextBoxStereoType)
        Me.TabPage2.Controls.Add(Me.CheckBoxGenerateName)
        Me.TabPage2.Controls.Add(Me.ButtonMatchNames)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.CheckBoxLoadData)
        Me.TabPage2.Controls.Add(Me.CheckBoxMapTarget)
        Me.TabPage2.Controls.Add(Me.ButtonLoadMappings)
        Me.TabPage2.Controls.Add(Me.ButtonGenerateMapping)
        Me.TabPage2.Controls.Add(Me.ButtonMappingTarget)
        Me.TabPage2.Controls.Add(Me.ButtonMappingSource)
        Me.TabPage2.Controls.Add(Me.DataGridViewMapping)
        Me.TabPage2.Controls.Add(Me.ListBoxMappingElement)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Controls.Add(Me.LabelMappingDiagramName)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.TabPage2.Size = New System.Drawing.Size(715, 461)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Mapping"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(8, 186)
        Me.Label18.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(108, 13)
        Me.Label18.TabIndex = 37
        Me.Label18.Text = "Connector stereotype"
        '
        'TextBoxStereoType
        '
        Me.TextBoxStereoType.Location = New System.Drawing.Point(8, 204)
        Me.TextBoxStereoType.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxStereoType.Name = "TextBoxStereoType"
        Me.TextBoxStereoType.Size = New System.Drawing.Size(177, 20)
        Me.TextBoxStereoType.TabIndex = 36
        '
        'CheckBoxGenerateName
        '
        Me.CheckBoxGenerateName.AutoSize = True
        Me.CheckBoxGenerateName.Location = New System.Drawing.Point(10, 167)
        Me.CheckBoxGenerateName.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxGenerateName.Name = "CheckBoxGenerateName"
        Me.CheckBoxGenerateName.Size = New System.Drawing.Size(99, 17)
        Me.CheckBoxGenerateName.TabIndex = 35
        Me.CheckBoxGenerateName.Text = "Generate name"
        Me.CheckBoxGenerateName.UseVisualStyleBackColor = True
        '
        'ButtonMatchNames
        '
        Me.ButtonMatchNames.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonMatchNames.Enabled = False
        Me.ButtonMatchNames.Location = New System.Drawing.Point(519, 167)
        Me.ButtonMatchNames.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.ButtonMatchNames.Name = "ButtonMatchNames"
        Me.ButtonMatchNames.Size = New System.Drawing.Size(94, 27)
        Me.ButtonMatchNames.TabIndex = 34
        Me.ButtonMatchNames.Text = "Match names"
        Me.ButtonMatchNames.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.BackColor = System.Drawing.Color.LemonChiffon
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label7.Location = New System.Drawing.Point(190, 139)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(322, 83)
        Me.Label7.TabIndex = 33
        Me.Label7.Text = resources.GetString("Label7.Text")
        '
        'CheckBoxLoadData
        '
        Me.CheckBoxLoadData.AutoSize = True
        Me.CheckBoxLoadData.Checked = True
        Me.CheckBoxLoadData.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxLoadData.Location = New System.Drawing.Point(10, 139)
        Me.CheckBoxLoadData.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxLoadData.Name = "CheckBoxLoadData"
        Me.CheckBoxLoadData.Size = New System.Drawing.Size(74, 17)
        Me.CheckBoxLoadData.TabIndex = 32
        Me.CheckBoxLoadData.Text = "Load data"
        Me.CheckBoxLoadData.UseVisualStyleBackColor = True
        '
        'CheckBoxMapTarget
        '
        Me.CheckBoxMapTarget.AutoSize = True
        Me.CheckBoxMapTarget.Checked = True
        Me.CheckBoxMapTarget.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxMapTarget.Location = New System.Drawing.Point(10, 154)
        Me.CheckBoxMapTarget.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.CheckBoxMapTarget.Name = "CheckBoxMapTarget"
        Me.CheckBoxMapTarget.Size = New System.Drawing.Size(89, 17)
        Me.CheckBoxMapTarget.TabIndex = 31
        Me.CheckBoxMapTarget.Text = "Map to target"
        Me.CheckBoxMapTarget.UseVisualStyleBackColor = True
        '
        'ButtonLoadMappings
        '
        Me.ButtonLoadMappings.Location = New System.Drawing.Point(519, 197)
        Me.ButtonLoadMappings.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ButtonLoadMappings.Name = "ButtonLoadMappings"
        Me.ButtonLoadMappings.Size = New System.Drawing.Size(94, 25)
        Me.ButtonLoadMappings.TabIndex = 30
        Me.ButtonLoadMappings.Text = "Load Mappings"
        Me.ButtonLoadMappings.UseVisualStyleBackColor = True
        '
        'ButtonGenerateMapping
        '
        Me.ButtonGenerateMapping.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonGenerateMapping.Location = New System.Drawing.Point(617, 167)
        Me.ButtonGenerateMapping.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.ButtonGenerateMapping.Name = "ButtonGenerateMapping"
        Me.ButtonGenerateMapping.Size = New System.Drawing.Size(94, 55)
        Me.ButtonGenerateMapping.TabIndex = 29
        Me.ButtonGenerateMapping.Text = "Generate mappings"
        Me.ButtonGenerateMapping.UseVisualStyleBackColor = True
        '
        'ButtonMappingTarget
        '
        Me.ButtonMappingTarget.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonMappingTarget.Location = New System.Drawing.Point(519, 100)
        Me.ButtonMappingTarget.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.ButtonMappingTarget.Name = "ButtonMappingTarget"
        Me.ButtonMappingTarget.Size = New System.Drawing.Size(76, 27)
        Me.ButtonMappingTarget.TabIndex = 28
        Me.ButtonMappingTarget.Text = "To target"
        Me.ButtonMappingTarget.UseVisualStyleBackColor = True
        '
        'ButtonMappingSource
        '
        Me.ButtonMappingSource.Location = New System.Drawing.Point(108, 100)
        Me.ButtonMappingSource.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.ButtonMappingSource.Name = "ButtonMappingSource"
        Me.ButtonMappingSource.Size = New System.Drawing.Size(77, 27)
        Me.ButtonMappingSource.TabIndex = 27
        Me.ButtonMappingSource.Text = "To source"
        Me.ButtonMappingSource.UseVisualStyleBackColor = True
        '
        'DataGridViewMapping
        '
        Me.DataGridViewMapping.AllowUserToOrderColumns = True
        Me.DataGridViewMapping.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewMapping.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewMapping.Location = New System.Drawing.Point(-3, 224)
        Me.DataGridViewMapping.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.DataGridViewMapping.Name = "DataGridViewMapping"
        Me.DataGridViewMapping.RowHeadersWidth = 51
        Me.DataGridViewMapping.RowTemplate.Height = 24
        Me.DataGridViewMapping.Size = New System.Drawing.Size(723, 238)
        Me.DataGridViewMapping.TabIndex = 26
        '
        'ListBoxMappingElement
        '
        Me.ListBoxMappingElement.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBoxMappingElement.FormattingEnabled = True
        Me.ListBoxMappingElement.Location = New System.Drawing.Point(190, 27)
        Me.ListBoxMappingElement.Name = "ListBoxMappingElement"
        Me.ListBoxMappingElement.Size = New System.Drawing.Size(323, 94)
        Me.ListBoxMappingElement.TabIndex = 24
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 4)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(89, 13)
        Me.Label5.TabIndex = 23
        Me.Label5.Text = "Selected diagram"
        '
        'LabelMappingDiagramName
        '
        Me.LabelMappingDiagramName.AutoSize = True
        Me.LabelMappingDiagramName.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelMappingDiagramName.Location = New System.Drawing.Point(105, 4)
        Me.LabelMappingDiagramName.Name = "LabelMappingDiagramName"
        Me.LabelMappingDiagramName.Size = New System.Drawing.Size(120, 18)
        Me.LabelMappingDiagramName.TabIndex = 25
        Me.LabelMappingDiagramName.Text = "Diagram Name"
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.LightGray
        Me.TabPage1.Controls.Add(Me.Label21)
        Me.TabPage1.Controls.Add(Me.ComboBoxTargetPackage)
        Me.TabPage1.Controls.Add(Me.ButtonUnselectGenerate)
        Me.TabPage1.Controls.Add(Me.ButtonToggleGenerate)
        Me.TabPage1.Controls.Add(Me.ButtonSelectAllGenerate)
        Me.TabPage1.Controls.Add(Me.ButtonGenerateItems)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.ListBoxElements)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.CheckBoxAttributeAssociation)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.ButtonGenerate)
        Me.TabPage1.Controls.Add(Me.ButtonSelectAll)
        Me.TabPage1.Controls.Add(Me.ListBoxType)
        Me.TabPage1.Controls.Add(Me.ButtonToggleAll)
        Me.TabPage1.Controls.Add(Me.TextBoxPrefix)
        Me.TabPage1.Controls.Add(Me.ButtonUnselectAll)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.LabelDiagramName)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.TabPage1.Size = New System.Drawing.Size(715, 461)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Generate"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(429, 217)
        Me.Label21.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(83, 13)
        Me.Label21.TabIndex = 35
        Me.Label21.Text = "Target package"
        '
        'ComboBoxTargetPackage
        '
        Me.ComboBoxTargetPackage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxTargetPackage.FormattingEnabled = True
        Me.ComboBoxTargetPackage.Location = New System.Drawing.Point(432, 237)
        Me.ComboBoxTargetPackage.Name = "ComboBoxTargetPackage"
        Me.ComboBoxTargetPackage.Size = New System.Drawing.Size(272, 21)
        Me.ComboBoxTargetPackage.TabIndex = 34
        '
        'ButtonUnselectGenerate
        '
        Me.ButtonUnselectGenerate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonUnselectGenerate.Location = New System.Drawing.Point(323, 426)
        Me.ButtonUnselectGenerate.Name = "ButtonUnselectGenerate"
        Me.ButtonUnselectGenerate.Size = New System.Drawing.Size(75, 23)
        Me.ButtonUnselectGenerate.TabIndex = 33
        Me.ButtonUnselectGenerate.Text = "Unselect All"
        Me.ButtonUnselectGenerate.UseVisualStyleBackColor = True
        '
        'ButtonToggleGenerate
        '
        Me.ButtonToggleGenerate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonToggleGenerate.Location = New System.Drawing.Point(167, 426)
        Me.ButtonToggleGenerate.Name = "ButtonToggleGenerate"
        Me.ButtonToggleGenerate.Size = New System.Drawing.Size(75, 23)
        Me.ButtonToggleGenerate.TabIndex = 32
        Me.ButtonToggleGenerate.Text = "Toggle"
        Me.ButtonToggleGenerate.UseVisualStyleBackColor = True
        '
        'ButtonSelectAllGenerate
        '
        Me.ButtonSelectAllGenerate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonSelectAllGenerate.Location = New System.Drawing.Point(17, 426)
        Me.ButtonSelectAllGenerate.Name = "ButtonSelectAllGenerate"
        Me.ButtonSelectAllGenerate.Size = New System.Drawing.Size(75, 23)
        Me.ButtonSelectAllGenerate.TabIndex = 31
        Me.ButtonSelectAllGenerate.Text = "Select All"
        Me.ButtonSelectAllGenerate.UseVisualStyleBackColor = True
        '
        'ButtonGenerateItems
        '
        Me.ButtonGenerateItems.Location = New System.Drawing.Point(431, 332)
        Me.ButtonGenerateItems.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonGenerateItems.Name = "ButtonGenerateItems"
        Me.ButtonGenerateItems.Size = New System.Drawing.Size(272, 62)
        Me.ButtonGenerateItems.TabIndex = 30
        Me.ButtonGenerateItems.Text = "Generate elements"
        Me.ButtonGenerateItems.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(429, 82)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(61, 13)
        Me.Label6.TabIndex = 29
        Me.Label6.Text = "Target type"
        '
        'SaveSQLFileDialog
        '
        Me.SaveSQLFileDialog.FileName = "mapping.sql"
        Me.SaveSQLFileDialog.InitialDirectory = "c:\werkmap"
        '
        'FrmIdeaDiagram
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(723, 487)
        Me.Controls.Add(Me.TabControl1)
        Me.MinimumSize = New System.Drawing.Size(68, 63)
        Me.Name = "FrmIdeaDiagram"
        Me.Text = "IDEA Diagram Helper"
        Me.TopMost = True
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        CType(Me.DataGridViewStatement, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.DataGridViewMapping, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ButtonUnselectAll As System.Windows.Forms.Button
    Friend WithEvents ButtonToggleAll As System.Windows.Forms.Button
    Friend WithEvents ButtonSelectAll As System.Windows.Forms.Button
    Friend WithEvents ListBoxElements As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LabelDiagramName As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxPrefix As System.Windows.Forms.TextBox
    Friend WithEvents ListBoxType As System.Windows.Forms.ListBox
    Friend WithEvents ButtonGenerate As System.Windows.Forms.Button
    Friend WithEvents CheckBoxAttributeAssociation As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents ButtonGenerateMapping As System.Windows.Forms.Button
    Friend WithEvents ButtonMappingTarget As System.Windows.Forms.Button
    Friend WithEvents ButtonMappingSource As System.Windows.Forms.Button
    Friend WithEvents DataGridViewMapping As System.Windows.Forms.DataGridView
    Friend WithEvents ListBoxMappingElement As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents LabelMappingDiagramName As System.Windows.Forms.Label
    Friend WithEvents ButtonLoadMappings As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxMapTarget As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxLoadData As System.Windows.Forms.CheckBox
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents ButtonSwitchMapping As System.Windows.Forms.Button
    Friend WithEvents CheckBoxIsHidden As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonMappingStyle As System.Windows.Forms.Button
    Friend WithEvents RadioButtonAutoroute As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonDirect As System.Windows.Forms.RadioButton
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ButtonGenerateItems As System.Windows.Forms.Button
    Friend WithEvents RadioButtonTreeVertical As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonLateralVertical As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonOrthogonalRounded As System.Windows.Forms.RadioButton
    Friend WithEvents ButtonHideEmbedded As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxCloseWindowExtraReady As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonMatchNames As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents ButtonCollectElements As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents ButtonShowHidden As System.Windows.Forms.Button
    Friend WithEvents CheckBoxMakeHidden As System.Windows.Forms.CheckBox
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents ButtonLoad As System.Windows.Forms.Button
    Friend WithEvents ButtonMakeSQL As System.Windows.Forms.Button
    Friend WithEvents CheckBoxGenerateName As System.Windows.Forms.CheckBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents TextBoxStereoType As System.Windows.Forms.TextBox
    Friend WithEvents SaveSQLFileDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ComboBoxSecGroup As System.Windows.Forms.ComboBox
    Friend WithEvents CheckBoxUserLock As System.Windows.Forms.CheckBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents ButtonLockElements As System.Windows.Forms.Button
    Friend WithEvents CheckBoxOriginalPackage As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonElement2Original As System.Windows.Forms.Button
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents ButtonUnselectGenerate As System.Windows.Forms.Button
    Friend WithEvents ButtonToggleGenerate As System.Windows.Forms.Button
    Friend WithEvents ButtonSelectAllGenerate As System.Windows.Forms.Button
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxTargetPackage As System.Windows.Forms.ComboBox
    Friend WithEvents ButtonArchiMateColoring As System.Windows.Forms.Button
    Friend WithEvents CheckBoxRestoreColor As System.Windows.Forms.CheckBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxStatement As System.Windows.Forms.ComboBox
    Friend WithEvents DataGridViewStatement As System.Windows.Forms.DataGridView
    Friend WithEvents TextBoxTemplate As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxStatement As System.Windows.Forms.TextBox
End Class
