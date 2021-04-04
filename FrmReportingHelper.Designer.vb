''' <summary>
''' Form for the settings of the HTML report generator.
''' </summary>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class FrmReportingHelper
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
        Me.SaveFileDialogReport = New System.Windows.Forms.SaveFileDialog()
        Me.DublicateTabControl = New System.Windows.Forms.TabControl()
        Me.TabPageHelper = New System.Windows.Forms.TabPage()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.CheckBoxReplaceNotes = New System.Windows.Forms.CheckBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.CheckBoxReplaceAlias = New System.Windows.Forms.CheckBox()
        Me.CheckBoxReplaceName = New System.Windows.Forms.CheckBox()
        Me.RadioButtonStereotype = New System.Windows.Forms.RadioButton()
        Me.RadioButtonAlias = New System.Windows.Forms.RadioButton()
        Me.RadioButtonName = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ButtonSortElements = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.CheckBoxRecursive = New System.Windows.Forms.CheckBox()
        Me.ButtonReplace = New System.Windows.Forms.Button()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.ButtonRemoveNesting = New System.Windows.Forms.Button()
        Me.TabPageHTML = New System.Windows.Forms.TabPage()
        Me.ButtonSelectDir = New System.Windows.Forms.Button()
        Me.ComboBoxPackageTemplate = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.ComboBoxCoverPageTemplate = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.LabelPackage = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxHTMLPath = New System.Windows.Forms.TextBox()
        Me.ButtonPublish = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.CheckBoxIncludeChildPackages = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.RadioButtonPDF = New System.Windows.Forms.RadioButton()
        Me.ComboBoxElementTemplate = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.RadioButtonDocx = New System.Windows.Forms.RadioButton()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.ComboBoxDiagramTemplate = New System.Windows.Forms.ComboBox()
        Me.CheckBoxIncludeToC = New System.Windows.Forms.CheckBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ButtonCreateDocument = New System.Windows.Forms.Button()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.CheckBoxSuppressEmptyNotes = New System.Windows.Forms.CheckBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.RadioButtonPDF2 = New System.Windows.Forms.RadioButton()
        Me.RadioButtonDocx2 = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.CheckBoxCreatePDF = New System.Windows.Forms.CheckBox()
        Me.CheckBoxCompositeClickable = New System.Windows.Forms.CheckBox()
        Me.CheckBoxDispayInBrowser = New System.Windows.Forms.CheckBox()
        Me.TextBoxTemplatesFile = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxStartURL = New System.Windows.Forms.TextBox()
        Me.ButtonReadTermplates = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TabPageTemplate = New System.Windows.Forms.TabPage()
        Me.ButtonSaveTemplates = New System.Windows.Forms.Button()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBoxSQL = New System.Windows.Forms.TextBox()
        Me.TextBoxTemplateName = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBoxBody = New System.Windows.Forms.TextBox()
        Me.TextBoxFooter = New System.Windows.Forms.TextBox()
        Me.TextBoxHeader = New System.Windows.Forms.TextBox()
        Me.ButtonUpdateTemplate = New System.Windows.Forms.Button()
        Me.ButtonAddTemplate = New System.Windows.Forms.Button()
        Me.ListBoxTemplates = New System.Windows.Forms.ListBox()
        Me.TabPageReportingSQL = New System.Windows.Forms.TabPage()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxElementDiagramSQL = New System.Windows.Forms.TextBox()
        Me.TextBoxElementPackageSQL = New System.Windows.Forms.TextBox()
        Me.TextBoxDiagramPackageSQL = New System.Windows.Forms.TextBox()
        Me.TabPageResult = New System.Windows.Forms.TabPage()
        Me.WebBrowserResult = New System.Windows.Forms.WebBrowser()
        Me.OpenFileDialogReport = New System.Windows.Forms.OpenFileDialog()
        Me.FolderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog()
        Me.DataGridViewSearchReplace = New System.Windows.Forms.DataGridView()
        Me.DublicateTabControl.SuspendLayout()
        Me.TabPageHelper.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.TabPageHTML.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TabPageTemplate.SuspendLayout()
        Me.TabPageReportingSQL.SuspendLayout()
        Me.TabPageResult.SuspendLayout()
        CType(Me.DataGridViewSearchReplace, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DublicateTabControl
        '
        Me.DublicateTabControl.Controls.Add(Me.TabPageHelper)
        Me.DublicateTabControl.Controls.Add(Me.TabPageHTML)
        Me.DublicateTabControl.Controls.Add(Me.TabPageTemplate)
        Me.DublicateTabControl.Controls.Add(Me.TabPageReportingSQL)
        Me.DublicateTabControl.Controls.Add(Me.TabPageResult)
        Me.DublicateTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DublicateTabControl.Location = New System.Drawing.Point(0, 0)
        Me.DublicateTabControl.Margin = New System.Windows.Forms.Padding(2)
        Me.DublicateTabControl.Name = "DublicateTabControl"
        Me.DublicateTabControl.SelectedIndex = 0
        Me.DublicateTabControl.Size = New System.Drawing.Size(904, 402)
        Me.DublicateTabControl.TabIndex = 0
        '
        'TabPageHelper
        '
        Me.TabPageHelper.BackColor = System.Drawing.Color.LightGray
        Me.TabPageHelper.Controls.Add(Me.CheckBoxRecursive)
        Me.TabPageHelper.Controls.Add(Me.ProgressBar1)
        Me.TabPageHelper.Controls.Add(Me.CheckBoxReplaceNotes)
        Me.TabPageHelper.Controls.Add(Me.Label21)
        Me.TabPageHelper.Controls.Add(Me.CheckBoxReplaceAlias)
        Me.TabPageHelper.Controls.Add(Me.CheckBoxReplaceName)
        Me.TabPageHelper.Controls.Add(Me.RadioButtonStereotype)
        Me.TabPageHelper.Controls.Add(Me.RadioButtonAlias)
        Me.TabPageHelper.Controls.Add(Me.RadioButtonName)
        Me.TabPageHelper.Controls.Add(Me.GroupBox1)
        Me.TabPageHelper.Controls.Add(Me.GroupBox4)
        Me.TabPageHelper.Controls.Add(Me.GroupBox5)
        Me.TabPageHelper.Location = New System.Drawing.Point(4, 22)
        Me.TabPageHelper.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPageHelper.Name = "TabPageHelper"
        Me.TabPageHelper.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPageHelper.Size = New System.Drawing.Size(896, 376)
        Me.TabPageHelper.TabIndex = 7
        Me.TabPageHelper.Text = "Extra"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(2, 359)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(898, 18)
        Me.ProgressBar1.TabIndex = 16
        '
        'CheckBoxReplaceNotes
        '
        Me.CheckBoxReplaceNotes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxReplaceNotes.AutoSize = True
        Me.CheckBoxReplaceNotes.Checked = True
        Me.CheckBoxReplaceNotes.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxReplaceNotes.Location = New System.Drawing.Point(287, 84)
        Me.CheckBoxReplaceNotes.Name = "CheckBoxReplaceNotes"
        Me.CheckBoxReplaceNotes.Size = New System.Drawing.Size(54, 17)
        Me.CheckBoxReplaceNotes.TabIndex = 10
        Me.CheckBoxReplaceNotes.Text = "Notes"
        Me.CheckBoxReplaceNotes.UseVisualStyleBackColor = True
        '
        'Label21
        '
        Me.Label21.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(284, 13)
        Me.Label21.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(0, 13)
        Me.Label21.TabIndex = 9
        '
        'CheckBoxReplaceAlias
        '
        Me.CheckBoxReplaceAlias.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxReplaceAlias.AutoSize = True
        Me.CheckBoxReplaceAlias.Checked = True
        Me.CheckBoxReplaceAlias.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxReplaceAlias.Location = New System.Drawing.Point(287, 61)
        Me.CheckBoxReplaceAlias.Name = "CheckBoxReplaceAlias"
        Me.CheckBoxReplaceAlias.Size = New System.Drawing.Size(48, 17)
        Me.CheckBoxReplaceAlias.TabIndex = 8
        Me.CheckBoxReplaceAlias.Text = "Alias"
        Me.CheckBoxReplaceAlias.UseVisualStyleBackColor = True
        '
        'CheckBoxReplaceName
        '
        Me.CheckBoxReplaceName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxReplaceName.AutoSize = True
        Me.CheckBoxReplaceName.Checked = True
        Me.CheckBoxReplaceName.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxReplaceName.Location = New System.Drawing.Point(287, 38)
        Me.CheckBoxReplaceName.Name = "CheckBoxReplaceName"
        Me.CheckBoxReplaceName.Size = New System.Drawing.Size(54, 17)
        Me.CheckBoxReplaceName.TabIndex = 7
        Me.CheckBoxReplaceName.Text = "Name"
        Me.CheckBoxReplaceName.UseVisualStyleBackColor = True
        '
        'RadioButtonStereotype
        '
        Me.RadioButtonStereotype.AutoSize = True
        Me.RadioButtonStereotype.Checked = True
        Me.RadioButtonStereotype.Location = New System.Drawing.Point(8, 81)
        Me.RadioButtonStereotype.Margin = New System.Windows.Forms.Padding(2)
        Me.RadioButtonStereotype.Name = "RadioButtonStereotype"
        Me.RadioButtonStereotype.Size = New System.Drawing.Size(113, 17)
        Me.RadioButtonStereotype.TabIndex = 3
        Me.RadioButtonStereotype.TabStop = True
        Me.RadioButtonStereotype.Text = "Stereotype - Name"
        Me.RadioButtonStereotype.UseVisualStyleBackColor = True
        '
        'RadioButtonAlias
        '
        Me.RadioButtonAlias.AutoSize = True
        Me.RadioButtonAlias.Location = New System.Drawing.Point(8, 59)
        Me.RadioButtonAlias.Margin = New System.Windows.Forms.Padding(2)
        Me.RadioButtonAlias.Name = "RadioButtonAlias"
        Me.RadioButtonAlias.Size = New System.Drawing.Size(47, 17)
        Me.RadioButtonAlias.TabIndex = 2
        Me.RadioButtonAlias.Text = "Alias"
        Me.RadioButtonAlias.UseVisualStyleBackColor = True
        '
        'RadioButtonName
        '
        Me.RadioButtonName.AutoSize = True
        Me.RadioButtonName.Location = New System.Drawing.Point(8, 37)
        Me.RadioButtonName.Margin = New System.Windows.Forms.Padding(2)
        Me.RadioButtonName.Name = "RadioButtonName"
        Me.RadioButtonName.Size = New System.Drawing.Size(53, 17)
        Me.RadioButtonName.TabIndex = 1
        Me.RadioButtonName.Text = "Name"
        Me.RadioButtonName.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ButtonSortElements)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 13)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(260, 152)
        Me.GroupBox1.TabIndex = 17
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Sort Package Elements"
        '
        'ButtonSortElements
        '
        Me.ButtonSortElements.Location = New System.Drawing.Point(5, 121)
        Me.ButtonSortElements.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonSortElements.Name = "ButtonSortElements"
        Me.ButtonSortElements.Size = New System.Drawing.Size(111, 26)
        Me.ButtonSortElements.TabIndex = 4
        Me.ButtonSortElements.Text = "Sort elements"
        Me.ButtonSortElements.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.DataGridViewSearchReplace)
        Me.GroupBox4.Controls.Add(Me.ButtonReplace)
        Me.GroupBox4.Location = New System.Drawing.Point(282, 13)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(337, 209)
        Me.GroupBox4.TabIndex = 18
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Replace Package Elements"
        '
        'CheckBoxRecursive
        '
        Me.CheckBoxRecursive.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxRecursive.AutoSize = True
        Me.CheckBoxRecursive.Checked = True
        Me.CheckBoxRecursive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxRecursive.Location = New System.Drawing.Point(796, 205)
        Me.CheckBoxRecursive.Name = "CheckBoxRecursive"
        Me.CheckBoxRecursive.Size = New System.Drawing.Size(74, 17)
        Me.CheckBoxRecursive.TabIndex = 16
        Me.CheckBoxRecursive.Text = "Recursive"
        Me.CheckBoxRecursive.UseVisualStyleBackColor = True
        '
        'ButtonReplace
        '
        Me.ButtonReplace.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonReplace.Location = New System.Drawing.Point(5, 178)
        Me.ButtonReplace.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonReplace.Name = "ButtonReplace"
        Me.ButtonReplace.Size = New System.Drawing.Size(305, 26)
        Me.ButtonReplace.TabIndex = 15
        Me.ButtonReplace.Text = "Replace item content"
        Me.ButtonReplace.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox5.Controls.Add(Me.ButtonRemoveNesting)
        Me.GroupBox5.Location = New System.Drawing.Point(637, 18)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(254, 147)
        Me.GroupBox5.TabIndex = 19
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Unnest Package Elements"
        '
        'ButtonRemoveNesting
        '
        Me.ButtonRemoveNesting.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonRemoveNesting.Location = New System.Drawing.Point(40, 116)
        Me.ButtonRemoveNesting.Name = "ButtonRemoveNesting"
        Me.ButtonRemoveNesting.Size = New System.Drawing.Size(193, 26)
        Me.ButtonRemoveNesting.TabIndex = 5
        Me.ButtonRemoveNesting.Text = "Remove element nesting"
        Me.ButtonRemoveNesting.UseVisualStyleBackColor = True
        '
        'TabPageHTML
        '
        Me.TabPageHTML.BackColor = System.Drawing.Color.LightGray
        Me.TabPageHTML.Controls.Add(Me.ButtonSelectDir)
        Me.TabPageHTML.Controls.Add(Me.ComboBoxPackageTemplate)
        Me.TabPageHTML.Controls.Add(Me.Label15)
        Me.TabPageHTML.Controls.Add(Me.ComboBoxCoverPageTemplate)
        Me.TabPageHTML.Controls.Add(Me.Label16)
        Me.TabPageHTML.Controls.Add(Me.Label5)
        Me.TabPageHTML.Controls.Add(Me.LabelPackage)
        Me.TabPageHTML.Controls.Add(Me.Label7)
        Me.TabPageHTML.Controls.Add(Me.Label1)
        Me.TabPageHTML.Controls.Add(Me.TextBoxHTMLPath)
        Me.TabPageHTML.Controls.Add(Me.ButtonPublish)
        Me.TabPageHTML.Controls.Add(Me.GroupBox3)
        Me.TabPageHTML.Controls.Add(Me.GroupBox2)
        Me.TabPageHTML.Location = New System.Drawing.Point(4, 22)
        Me.TabPageHTML.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPageHTML.Name = "TabPageHTML"
        Me.TabPageHTML.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPageHTML.Size = New System.Drawing.Size(896, 376)
        Me.TabPageHTML.TabIndex = 3
        Me.TabPageHTML.Text = "HTML & Document Publication"
        '
        'ButtonSelectDir
        '
        Me.ButtonSelectDir.Location = New System.Drawing.Point(844, 38)
        Me.ButtonSelectDir.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonSelectDir.Name = "ButtonSelectDir"
        Me.ButtonSelectDir.Size = New System.Drawing.Size(28, 19)
        Me.ButtonSelectDir.TabIndex = 58
        Me.ButtonSelectDir.Text = "!!"
        Me.ButtonSelectDir.UseVisualStyleBackColor = True
        '
        'ComboBoxPackageTemplate
        '
        Me.ComboBoxPackageTemplate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxPackageTemplate.FormattingEnabled = True
        Me.ComboBoxPackageTemplate.Location = New System.Drawing.Point(133, 86)
        Me.ComboBoxPackageTemplate.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBoxPackageTemplate.Name = "ComboBoxPackageTemplate"
        Me.ComboBoxPackageTemplate.Size = New System.Drawing.Size(708, 21)
        Me.ComboBoxPackageTemplate.TabIndex = 57
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(8, 86)
        Me.Label15.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(125, 13)
        Me.Label15.TabIndex = 56
        Me.Label15.Text = "Root/Package Template"
        '
        'ComboBoxCoverPageTemplate
        '
        Me.ComboBoxCoverPageTemplate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxCoverPageTemplate.FormattingEnabled = True
        Me.ComboBoxCoverPageTemplate.Location = New System.Drawing.Point(133, 62)
        Me.ComboBoxCoverPageTemplate.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBoxCoverPageTemplate.Name = "ComboBoxCoverPageTemplate"
        Me.ComboBoxCoverPageTemplate.Size = New System.Drawing.Size(708, 21)
        Me.ComboBoxCoverPageTemplate.TabIndex = 50
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(47, 64)
        Me.Label16.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(83, 13)
        Me.Label16.TabIndex = 46
        Me.Label16.Text = "PDF Coverpage"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(56, 15)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 13)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Rootpackage"
        '
        'LabelPackage
        '
        Me.LabelPackage.AutoSize = True
        Me.LabelPackage.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelPackage.Location = New System.Drawing.Point(129, 15)
        Me.LabelPackage.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LabelPackage.Name = "LabelPackage"
        Me.LabelPackage.Size = New System.Drawing.Size(21, 20)
        Me.LabelPackage.TabIndex = 15
        Me.LabelPackage.Text = "--"
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(684, 12)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(236, 17)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "See system.output for progress"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(47, 44)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Publication Path"
        '
        'TextBoxHTMLPath
        '
        Me.TextBoxHTMLPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxHTMLPath.Location = New System.Drawing.Point(133, 40)
        Me.TextBoxHTMLPath.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxHTMLPath.Name = "TextBoxHTMLPath"
        Me.TextBoxHTMLPath.Size = New System.Drawing.Size(708, 20)
        Me.TextBoxHTMLPath.TabIndex = 3
        '
        'ButtonPublish
        '
        Me.ButtonPublish.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonPublish.Location = New System.Drawing.Point(654, 316)
        Me.ButtonPublish.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonPublish.Name = "ButtonPublish"
        Me.ButtonPublish.Size = New System.Drawing.Size(235, 53)
        Me.ButtonPublish.TabIndex = 0
        Me.ButtonPublish.Text = "Publish HTML"
        Me.ButtonPublish.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.BackColor = System.Drawing.Color.Gainsboro
        Me.GroupBox3.Controls.Add(Me.CheckBoxIncludeChildPackages)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.RadioButtonPDF)
        Me.GroupBox3.Controls.Add(Me.ComboBoxElementTemplate)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.RadioButtonDocx)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Controls.Add(Me.ComboBoxDiagramTemplate)
        Me.GroupBox3.Controls.Add(Me.CheckBoxIncludeToC)
        Me.GroupBox3.Controls.Add(Me.Button1)
        Me.GroupBox3.Controls.Add(Me.ButtonCreateDocument)
        Me.GroupBox3.Controls.Add(Me.CheckBox1)
        Me.GroupBox3.Controls.Add(Me.CheckBoxSuppressEmptyNotes)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.RadioButtonPDF2)
        Me.GroupBox3.Controls.Add(Me.RadioButtonDocx2)
        Me.GroupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.GroupBox3.Location = New System.Drawing.Point(2, 120)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox3.Size = New System.Drawing.Size(427, 258)
        Me.GroupBox3.TabIndex = 55
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "PDF reporting"
        '
        'CheckBoxIncludeChildPackages
        '
        Me.CheckBoxIncludeChildPackages.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxIncludeChildPackages.AutoSize = True
        Me.CheckBoxIncludeChildPackages.Location = New System.Drawing.Point(112, 151)
        Me.CheckBoxIncludeChildPackages.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxIncludeChildPackages.Name = "CheckBoxIncludeChildPackages"
        Me.CheckBoxIncludeChildPackages.Size = New System.Drawing.Size(136, 17)
        Me.CheckBoxIncludeChildPackages.TabIndex = 54
        Me.CheckBoxIncludeChildPackages.Text = "Include child packages"
        Me.CheckBoxIncludeChildPackages.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(110, 173)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 13)
        Me.Label2.TabIndex = 52
        Me.Label2.Text = "Document type"
        '
        'RadioButtonPDF
        '
        Me.RadioButtonPDF.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadioButtonPDF.AutoSize = True
        Me.RadioButtonPDF.Location = New System.Drawing.Point(242, 173)
        Me.RadioButtonPDF.Margin = New System.Windows.Forms.Padding(2)
        Me.RadioButtonPDF.Name = "RadioButtonPDF"
        Me.RadioButtonPDF.Size = New System.Drawing.Size(46, 17)
        Me.RadioButtonPDF.TabIndex = 50
        Me.RadioButtonPDF.Tag = "PDF"
        Me.RadioButtonPDF.Text = "PDF"
        Me.RadioButtonPDF.UseVisualStyleBackColor = True
        '
        'ComboBoxElementTemplate
        '
        Me.ComboBoxElementTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxElementTemplate.FormattingEnabled = True
        Me.ComboBoxElementTemplate.Location = New System.Drawing.Point(112, 67)
        Me.ComboBoxElementTemplate.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBoxElementTemplate.Name = "ComboBoxElementTemplate"
        Me.ComboBoxElementTemplate.Size = New System.Drawing.Size(300, 21)
        Me.ComboBoxElementTemplate.TabIndex = 53
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(16, 67)
        Me.Label13.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(92, 13)
        Me.Label13.TabIndex = 49
        Me.Label13.Text = "Element Template"
        '
        'RadioButtonDocx
        '
        Me.RadioButtonDocx.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadioButtonDocx.AutoSize = True
        Me.RadioButtonDocx.Checked = True
        Me.RadioButtonDocx.Location = New System.Drawing.Point(192, 173)
        Me.RadioButtonDocx.Margin = New System.Windows.Forms.Padding(2)
        Me.RadioButtonDocx.Name = "RadioButtonDocx"
        Me.RadioButtonDocx.Size = New System.Drawing.Size(50, 17)
        Me.RadioButtonDocx.TabIndex = 51
        Me.RadioButtonDocx.TabStop = True
        Me.RadioButtonDocx.Tag = "Docx"
        Me.RadioButtonDocx.Text = "Docx"
        Me.RadioButtonDocx.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(15, 35)
        Me.Label14.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(93, 13)
        Me.Label14.TabIndex = 48
        Me.Label14.Text = "Diagram Template"
        '
        'ComboBoxDiagramTemplate
        '
        Me.ComboBoxDiagramTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxDiagramTemplate.FormattingEnabled = True
        Me.ComboBoxDiagramTemplate.Location = New System.Drawing.Point(112, 32)
        Me.ComboBoxDiagramTemplate.Margin = New System.Windows.Forms.Padding(2)
        Me.ComboBoxDiagramTemplate.Name = "ComboBoxDiagramTemplate"
        Me.ComboBoxDiagramTemplate.Size = New System.Drawing.Size(300, 21)
        Me.ComboBoxDiagramTemplate.TabIndex = 52
        '
        'CheckBoxIncludeToC
        '
        Me.CheckBoxIncludeToC.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxIncludeToC.AutoSize = True
        Me.CheckBoxIncludeToC.Location = New System.Drawing.Point(112, 129)
        Me.CheckBoxIncludeToC.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxIncludeToC.Name = "CheckBoxIncludeToC"
        Me.CheckBoxIncludeToC.Size = New System.Drawing.Size(138, 17)
        Me.CheckBoxIncludeToC.TabIndex = 49
        Me.CheckBoxIncludeToC.Text = "Include table of content"
        Me.CheckBoxIncludeToC.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(854, 407)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(118, 43)
        Me.Button1.TabIndex = 48
        Me.Button1.Text = "Create Document"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ButtonCreateDocument
        '
        Me.ButtonCreateDocument.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonCreateDocument.Location = New System.Drawing.Point(215, 193)
        Me.ButtonCreateDocument.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonCreateDocument.Name = "ButtonCreateDocument"
        Me.ButtonCreateDocument.Size = New System.Drawing.Size(196, 58)
        Me.ButtonCreateDocument.TabIndex = 42
        Me.ButtonCreateDocument.Text = "Create PDF/Docx Document"
        Me.ButtonCreateDocument.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Checked = True
        Me.CheckBox1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox1.Location = New System.Drawing.Point(-214, 7)
        Me.CheckBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(138, 17)
        Me.CheckBox1.TabIndex = 47
        Me.CheckBox1.Text = "Include table of content"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'CheckBoxSuppressEmptyNotes
        '
        Me.CheckBoxSuppressEmptyNotes.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxSuppressEmptyNotes.AutoSize = True
        Me.CheckBoxSuppressEmptyNotes.Checked = True
        Me.CheckBoxSuppressEmptyNotes.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxSuppressEmptyNotes.Location = New System.Drawing.Point(112, 107)
        Me.CheckBoxSuppressEmptyNotes.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxSuppressEmptyNotes.Name = "CheckBoxSuppressEmptyNotes"
        Me.CheckBoxSuppressEmptyNotes.Size = New System.Drawing.Size(197, 17)
        Me.CheckBoxSuppressEmptyNotes.TabIndex = 46
        Me.CheckBoxSuppressEmptyNotes.Text = "Suppress elements with empty notes"
        Me.CheckBoxSuppressEmptyNotes.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(-216, 50)
        Me.Label11.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(79, 13)
        Me.Label11.TabIndex = 45
        Me.Label11.Text = "Document type"
        '
        'RadioButtonPDF2
        '
        Me.RadioButtonPDF2.AutoSize = True
        Me.RadioButtonPDF2.Location = New System.Drawing.Point(-81, 50)
        Me.RadioButtonPDF2.Margin = New System.Windows.Forms.Padding(2)
        Me.RadioButtonPDF2.Name = "RadioButtonPDF2"
        Me.RadioButtonPDF2.Size = New System.Drawing.Size(46, 17)
        Me.RadioButtonPDF2.TabIndex = 43
        Me.RadioButtonPDF2.Text = "PDF"
        Me.RadioButtonPDF2.UseVisualStyleBackColor = True
        '
        'RadioButtonDocx2
        '
        Me.RadioButtonDocx2.AutoSize = True
        Me.RadioButtonDocx2.Location = New System.Drawing.Point(-130, 50)
        Me.RadioButtonDocx2.Margin = New System.Windows.Forms.Padding(2)
        Me.RadioButtonDocx2.Name = "RadioButtonDocx2"
        Me.RadioButtonDocx2.Size = New System.Drawing.Size(50, 17)
        Me.RadioButtonDocx2.TabIndex = 44
        Me.RadioButtonDocx2.Text = "Docx"
        Me.RadioButtonDocx2.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.Gainsboro
        Me.GroupBox2.Controls.Add(Me.CheckBoxCreatePDF)
        Me.GroupBox2.Controls.Add(Me.CheckBoxCompositeClickable)
        Me.GroupBox2.Controls.Add(Me.CheckBoxDispayInBrowser)
        Me.GroupBox2.Controls.Add(Me.TextBoxTemplatesFile)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.TextBoxStartURL)
        Me.GroupBox2.Controls.Add(Me.ButtonReadTermplates)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Location = New System.Drawing.Point(436, 120)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox2.Size = New System.Drawing.Size(460, 258)
        Me.GroupBox2.TabIndex = 54
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "HTML Publication"
        '
        'CheckBoxCreatePDF
        '
        Me.CheckBoxCreatePDF.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxCreatePDF.AutoSize = True
        Me.CheckBoxCreatePDF.Location = New System.Drawing.Point(105, 103)
        Me.CheckBoxCreatePDF.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxCreatePDF.Name = "CheckBoxCreatePDF"
        Me.CheckBoxCreatePDF.Size = New System.Drawing.Size(146, 17)
        Me.CheckBoxCreatePDF.TabIndex = 1
        Me.CheckBoxCreatePDF.Text = "Create PDF for packages"
        Me.CheckBoxCreatePDF.UseVisualStyleBackColor = True
        '
        'CheckBoxCompositeClickable
        '
        Me.CheckBoxCompositeClickable.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxCompositeClickable.AutoSize = True
        Me.CheckBoxCompositeClickable.Location = New System.Drawing.Point(107, 123)
        Me.CheckBoxCompositeClickable.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxCompositeClickable.Name = "CheckBoxCompositeClickable"
        Me.CheckBoxCompositeClickable.Size = New System.Drawing.Size(173, 17)
        Me.CheckBoxCompositeClickable.TabIndex = 12
        Me.CheckBoxCompositeClickable.Text = "(Composite) Diagrams clickable"
        Me.CheckBoxCompositeClickable.UseVisualStyleBackColor = True
        '
        'CheckBoxDispayInBrowser
        '
        Me.CheckBoxDispayInBrowser.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxDispayInBrowser.AutoSize = True
        Me.CheckBoxDispayInBrowser.Location = New System.Drawing.Point(106, 143)
        Me.CheckBoxDispayInBrowser.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxDispayInBrowser.Name = "CheckBoxDispayInBrowser"
        Me.CheckBoxDispayInBrowser.Size = New System.Drawing.Size(111, 17)
        Me.CheckBoxDispayInBrowser.TabIndex = 23
        Me.CheckBoxDispayInBrowser.Text = "Display in browser"
        Me.CheckBoxDispayInBrowser.UseVisualStyleBackColor = True
        '
        'TextBoxTemplatesFile
        '
        Me.TextBoxTemplatesFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTemplatesFile.Location = New System.Drawing.Point(109, 35)
        Me.TextBoxTemplatesFile.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxTemplatesFile.Name = "TextBoxTemplatesFile"
        Me.TextBoxTemplatesFile.Size = New System.Drawing.Size(318, 20)
        Me.TextBoxTemplatesFile.TabIndex = 17
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 35)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(97, 13)
        Me.Label6.TabIndex = 18
        Me.Label6.Text = "Templates XML file"
        '
        'TextBoxStartURL
        '
        Me.TextBoxStartURL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxStartURL.Location = New System.Drawing.Point(109, 69)
        Me.TextBoxStartURL.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxStartURL.Name = "TextBoxStartURL"
        Me.TextBoxStartURL.Size = New System.Drawing.Size(318, 20)
        Me.TextBoxStartURL.TabIndex = 19
        '
        'ButtonReadTermplates
        '
        Me.ButtonReadTermplates.Location = New System.Drawing.Point(429, 35)
        Me.ButtonReadTermplates.Name = "ButtonReadTermplates"
        Me.ButtonReadTermplates.Size = New System.Drawing.Size(28, 22)
        Me.ButtonReadTermplates.TabIndex = 24
        Me.ButtonReadTermplates.Text = "!!"
        Me.ButtonReadTermplates.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(52, 72)
        Me.Label10.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(53, 13)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "Startpage"
        '
        'TabPageTemplate
        '
        Me.TabPageTemplate.BackColor = System.Drawing.Color.LightGray
        Me.TabPageTemplate.Controls.Add(Me.ButtonSaveTemplates)
        Me.TabPageTemplate.Controls.Add(Me.Label18)
        Me.TabPageTemplate.Controls.Add(Me.Label17)
        Me.TabPageTemplate.Controls.Add(Me.Label9)
        Me.TabPageTemplate.Controls.Add(Me.TextBoxSQL)
        Me.TabPageTemplate.Controls.Add(Me.TextBoxTemplateName)
        Me.TabPageTemplate.Controls.Add(Me.Label8)
        Me.TabPageTemplate.Controls.Add(Me.TextBoxBody)
        Me.TabPageTemplate.Controls.Add(Me.TextBoxFooter)
        Me.TabPageTemplate.Controls.Add(Me.TextBoxHeader)
        Me.TabPageTemplate.Controls.Add(Me.ButtonUpdateTemplate)
        Me.TabPageTemplate.Controls.Add(Me.ButtonAddTemplate)
        Me.TabPageTemplate.Controls.Add(Me.ListBoxTemplates)
        Me.TabPageTemplate.Location = New System.Drawing.Point(4, 22)
        Me.TabPageTemplate.Name = "TabPageTemplate"
        Me.TabPageTemplate.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageTemplate.Size = New System.Drawing.Size(896, 376)
        Me.TabPageTemplate.TabIndex = 4
        Me.TabPageTemplate.Text = "HTML Templates"
        '
        'ButtonSaveTemplates
        '
        Me.ButtonSaveTemplates.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonSaveTemplates.Location = New System.Drawing.Point(0, 349)
        Me.ButtonSaveTemplates.Name = "ButtonSaveTemplates"
        Me.ButtonSaveTemplates.Size = New System.Drawing.Size(189, 23)
        Me.ButtonSaveTemplates.TabIndex = 12
        Me.ButtonSaveTemplates.Text = "Save Templates"
        Me.ButtonSaveTemplates.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(196, 282)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(113, 13)
        Me.Label18.TabIndex = 11
        Me.Label18.Text = "HTML Footer template"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(196, 29)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(118, 13)
        Me.Label17.TabIndex = 10
        Me.Label17.Text = "HTML Header template"
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(548, 29)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(61, 13)
        Me.Label9.TabIndex = 9
        Me.Label9.Text = "HTML SQL"
        '
        'TextBoxSQL
        '
        Me.TextBoxSQL.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSQL.Location = New System.Drawing.Point(550, 46)
        Me.TextBoxSQL.Multiline = True
        Me.TextBoxSQL.Name = "TextBoxSQL"
        Me.TextBoxSQL.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBoxSQL.Size = New System.Drawing.Size(345, 330)
        Me.TextBoxSQL.TabIndex = 8
        '
        'TextBoxTemplateName
        '
        Me.TextBoxTemplateName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTemplateName.Location = New System.Drawing.Point(196, 5)
        Me.TextBoxTemplateName.Name = "TextBoxTemplateName"
        Me.TextBoxTemplateName.Size = New System.Drawing.Size(700, 20)
        Me.TextBoxTemplateName.TabIndex = 7
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(196, 107)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(107, 13)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "HTML Body template"
        '
        'TextBoxBody
        '
        Me.TextBoxBody.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxBody.Location = New System.Drawing.Point(196, 127)
        Me.TextBoxBody.Multiline = True
        Me.TextBoxBody.Name = "TextBoxBody"
        Me.TextBoxBody.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBoxBody.Size = New System.Drawing.Size(353, 153)
        Me.TextBoxBody.TabIndex = 5
        '
        'TextBoxFooter
        '
        Me.TextBoxFooter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFooter.Location = New System.Drawing.Point(196, 299)
        Me.TextBoxFooter.Multiline = True
        Me.TextBoxFooter.Name = "TextBoxFooter"
        Me.TextBoxFooter.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBoxFooter.Size = New System.Drawing.Size(353, 77)
        Me.TextBoxFooter.TabIndex = 4
        '
        'TextBoxHeader
        '
        Me.TextBoxHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxHeader.Location = New System.Drawing.Point(196, 46)
        Me.TextBoxHeader.Multiline = True
        Me.TextBoxHeader.Name = "TextBoxHeader"
        Me.TextBoxHeader.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBoxHeader.Size = New System.Drawing.Size(353, 58)
        Me.TextBoxHeader.TabIndex = 3
        '
        'ButtonUpdateTemplate
        '
        Me.ButtonUpdateTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonUpdateTemplate.Location = New System.Drawing.Point(98, 322)
        Me.ButtonUpdateTemplate.Name = "ButtonUpdateTemplate"
        Me.ButtonUpdateTemplate.Size = New System.Drawing.Size(92, 23)
        Me.ButtonUpdateTemplate.TabIndex = 2
        Me.ButtonUpdateTemplate.Text = "Upd Template"
        Me.ButtonUpdateTemplate.UseVisualStyleBackColor = True
        '
        'ButtonAddTemplate
        '
        Me.ButtonAddTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonAddTemplate.Location = New System.Drawing.Point(0, 322)
        Me.ButtonAddTemplate.Name = "ButtonAddTemplate"
        Me.ButtonAddTemplate.Size = New System.Drawing.Size(92, 23)
        Me.ButtonAddTemplate.TabIndex = 1
        Me.ButtonAddTemplate.Text = "Add Template"
        Me.ButtonAddTemplate.UseVisualStyleBackColor = True
        '
        'ListBoxTemplates
        '
        Me.ListBoxTemplates.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ListBoxTemplates.FormattingEnabled = True
        Me.ListBoxTemplates.Location = New System.Drawing.Point(0, 0)
        Me.ListBoxTemplates.Name = "ListBoxTemplates"
        Me.ListBoxTemplates.Size = New System.Drawing.Size(190, 316)
        Me.ListBoxTemplates.TabIndex = 0
        '
        'TabPageReportingSQL
        '
        Me.TabPageReportingSQL.BackColor = System.Drawing.Color.LightGray
        Me.TabPageReportingSQL.Controls.Add(Me.Label12)
        Me.TabPageReportingSQL.Controls.Add(Me.Label4)
        Me.TabPageReportingSQL.Controls.Add(Me.Label3)
        Me.TabPageReportingSQL.Controls.Add(Me.TextBoxElementDiagramSQL)
        Me.TabPageReportingSQL.Controls.Add(Me.TextBoxElementPackageSQL)
        Me.TabPageReportingSQL.Controls.Add(Me.TextBoxDiagramPackageSQL)
        Me.TabPageReportingSQL.Location = New System.Drawing.Point(4, 22)
        Me.TabPageReportingSQL.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPageReportingSQL.Name = "TabPageReportingSQL"
        Me.TabPageReportingSQL.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPageReportingSQL.Size = New System.Drawing.Size(896, 376)
        Me.TabPageReportingSQL.TabIndex = 6
        Me.TabPageReportingSQL.Text = "Reporting SQL"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 249)
        Me.Label12.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(111, 13)
        Me.Label12.TabIndex = 5
        Me.Label12.Text = "Element Diagram SQL"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 136)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(115, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Element Package SQL"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(4, 24)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(116, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Diagram Package SQL"
        '
        'TextBoxElementDiagramSQL
        '
        Me.TextBoxElementDiagramSQL.Location = New System.Drawing.Point(134, 246)
        Me.TextBoxElementDiagramSQL.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxElementDiagramSQL.Multiline = True
        Me.TextBoxElementDiagramSQL.Name = "TextBoxElementDiagramSQL"
        Me.TextBoxElementDiagramSQL.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBoxElementDiagramSQL.Size = New System.Drawing.Size(693, 97)
        Me.TextBoxElementDiagramSQL.TabIndex = 2
        '
        'TextBoxElementPackageSQL
        '
        Me.TextBoxElementPackageSQL.Location = New System.Drawing.Point(134, 133)
        Me.TextBoxElementPackageSQL.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxElementPackageSQL.Multiline = True
        Me.TextBoxElementPackageSQL.Name = "TextBoxElementPackageSQL"
        Me.TextBoxElementPackageSQL.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBoxElementPackageSQL.Size = New System.Drawing.Size(693, 97)
        Me.TextBoxElementPackageSQL.TabIndex = 1
        '
        'TextBoxDiagramPackageSQL
        '
        Me.TextBoxDiagramPackageSQL.Location = New System.Drawing.Point(134, 21)
        Me.TextBoxDiagramPackageSQL.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxDiagramPackageSQL.Multiline = True
        Me.TextBoxDiagramPackageSQL.Name = "TextBoxDiagramPackageSQL"
        Me.TextBoxDiagramPackageSQL.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBoxDiagramPackageSQL.Size = New System.Drawing.Size(693, 97)
        Me.TextBoxDiagramPackageSQL.TabIndex = 0
        '
        'TabPageResult
        '
        Me.TabPageResult.Controls.Add(Me.WebBrowserResult)
        Me.TabPageResult.Location = New System.Drawing.Point(4, 22)
        Me.TabPageResult.Name = "TabPageResult"
        Me.TabPageResult.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageResult.Size = New System.Drawing.Size(896, 376)
        Me.TabPageResult.TabIndex = 5
        Me.TabPageResult.Text = "View HTML Result"
        Me.TabPageResult.UseVisualStyleBackColor = True
        '
        'WebBrowserResult
        '
        Me.WebBrowserResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebBrowserResult.Location = New System.Drawing.Point(3, 3)
        Me.WebBrowserResult.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowserResult.Name = "WebBrowserResult"
        Me.WebBrowserResult.Size = New System.Drawing.Size(890, 370)
        Me.WebBrowserResult.TabIndex = 0
        '
        'DataGridViewSearchReplace
        '
        Me.DataGridViewSearchReplace.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewSearchReplace.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewSearchReplace.Location = New System.Drawing.Point(78, 25)
        Me.DataGridViewSearchReplace.Name = "DataGridViewSearchReplace"
        Me.DataGridViewSearchReplace.Size = New System.Drawing.Size(232, 148)
        Me.DataGridViewSearchReplace.TabIndex = 16
        '
        'FrmReportingHelper
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.AutoScrollMinSize = New System.Drawing.Size(100, 100)
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(904, 402)
        Me.Controls.Add(Me.DublicateTabControl)
        Me.HelpButton = True
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "FrmReportingHelper"
        Me.Text = "IDEA Package Helper"
        Me.DublicateTabControl.ResumeLayout(False)
        Me.TabPageHelper.ResumeLayout(False)
        Me.TabPageHelper.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.TabPageHTML.ResumeLayout(False)
        Me.TabPageHTML.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TabPageTemplate.ResumeLayout(False)
        Me.TabPageTemplate.PerformLayout()
        Me.TabPageReportingSQL.ResumeLayout(False)
        Me.TabPageReportingSQL.PerformLayout()
        Me.TabPageResult.ResumeLayout(False)
        CType(Me.DataGridViewSearchReplace, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SaveFileDialogReport As System.Windows.Forms.SaveFileDialog
    Friend WithEvents DublicateTabControl As System.Windows.Forms.TabControl
    Friend WithEvents TabPageHTML As System.Windows.Forms.TabPage
    Friend WithEvents ButtonPublish As System.Windows.Forms.Button
    Friend WithEvents CheckBoxCreatePDF As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxHTMLPath As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxCompositeClickable As System.Windows.Forms.CheckBox
    Friend WithEvents Package As System.Windows.Forms.Label
    Friend WithEvents LabelPackage As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TabPageTemplate As System.Windows.Forms.TabPage
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBoxTemplatesFile As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TextBoxBody As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxFooter As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxHeader As System.Windows.Forms.TextBox
    Friend WithEvents ButtonUpdateTemplate As System.Windows.Forms.Button
    Friend WithEvents ButtonAddTemplate As System.Windows.Forms.Button
    Friend WithEvents ListBoxTemplates As System.Windows.Forms.ListBox
    Friend WithEvents TextBoxTemplateName As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TextBoxSQL As System.Windows.Forms.TextBox
    Friend WithEvents TabPageResult As System.Windows.Forms.TabPage
    Friend WithEvents WebBrowserResult As System.Windows.Forms.WebBrowser
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TextBoxStartURL As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxDispayInBrowser As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonReadTermplates As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialogReport As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ComboBoxElementTemplate As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxDiagramTemplate As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxCoverPageTemplate As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBoxIncludeToC As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ButtonCreateDocument As System.Windows.Forms.Button
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxSuppressEmptyNotes As System.Windows.Forms.CheckBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents RadioButtonPDF2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonDocx2 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents RadioButtonPDF As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonDocx As System.Windows.Forms.RadioButton
    Friend WithEvents ComboBoxPackageTemplate As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents ButtonSelectDir As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents TabPageReportingSQL As System.Windows.Forms.TabPage
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxElementDiagramSQL As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxElementPackageSQL As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxDiagramPackageSQL As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxIncludeChildPackages As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonSaveTemplates As System.Windows.Forms.Button
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents TabPageHelper As System.Windows.Forms.TabPage
    Friend WithEvents ButtonSortElements As System.Windows.Forms.Button
    Friend WithEvents RadioButtonStereotype As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonAlias As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonName As System.Windows.Forms.RadioButton
    Friend WithEvents ButtonRemoveNesting As System.Windows.Forms.Button
    Friend WithEvents ButtonReplace As System.Windows.Forms.Button
    Friend WithEvents CheckBoxReplaceNotes As System.Windows.Forms.CheckBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxReplaceAlias As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxReplaceName As System.Windows.Forms.CheckBox
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBoxRecursive As System.Windows.Forms.CheckBox
    Friend WithEvents DataGridViewSearchReplace As System.Windows.Forms.DataGridView
End Class
