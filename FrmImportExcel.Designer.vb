''' <summary>
''' Import data from excel sheets with helper routines for to make advanced import
''' routines with associations and other entities
''' </summary>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class FrmImportExcel
	Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TextBoxConnection = New System.Windows.Forms.TextBox()
        Me.OpenExcelFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxExcelFile = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ButtonSelect = New System.Windows.Forms.Button()
        Me.ButtonLoad = New System.Windows.Forms.Button()
        Me.DataGridViewExcel = New System.Windows.Forms.DataGridView()
        Me.ButtonEntiteiten = New System.Windows.Forms.Button()
        Me.ButtonAttributes = New System.Windows.Forms.Button()
        Me.ButtonAssociaties = New System.Windows.Forms.Button()
        Me.TextBoxTableNo = New System.Windows.Forms.TextBox()
        Me.ButtonCleanLinkes = New System.Windows.Forms.Button()
        Me.ButtonImportRequirements = New System.Windows.Forms.Button()
        Me.ButtonKilMan = New System.Windows.Forms.Button()
        Me.ProgressBar = New System.Windows.Forms.ProgressBar()
        CType(Me.DataGridViewExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBoxConnection
        '
        Me.TextBoxConnection.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxConnection.Location = New System.Drawing.Point(394, 21)
        Me.TextBoxConnection.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.TextBoxConnection.Multiline = True
        Me.TextBoxConnection.Name = "TextBoxConnection"
        Me.TextBoxConnection.Size = New System.Drawing.Size(207, 47)
        Me.TextBoxConnection.TabIndex = 0
        '
        'OpenExcelFileDialog
        '
        Me.OpenExcelFileDialog.FileName = "OpenFileDialog1"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(392, 4)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Connectionstring"
        '
        'TextBoxExcelFile
        '
        Me.TextBoxExcelFile.Location = New System.Drawing.Point(11, 25)
        Me.TextBoxExcelFile.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.TextBoxExcelFile.Name = "TextBoxExcelFile"
        Me.TextBoxExcelFile.Size = New System.Drawing.Size(225, 20)
        Me.TextBoxExcelFile.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 7)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Excel file"
        '
        'ButtonSelect
        '
        Me.ButtonSelect.Location = New System.Drawing.Point(239, 21)
        Me.ButtonSelect.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ButtonSelect.Name = "ButtonSelect"
        Me.ButtonSelect.Size = New System.Drawing.Size(75, 26)
        Me.ButtonSelect.TabIndex = 4
        Me.ButtonSelect.Text = "Select file"
        Me.ButtonSelect.UseVisualStyleBackColor = True
        '
        'ButtonLoad
        '
        Me.ButtonLoad.Enabled = False
        Me.ButtonLoad.Location = New System.Drawing.Point(317, 21)
        Me.ButtonLoad.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ButtonLoad.Name = "ButtonLoad"
        Me.ButtonLoad.Size = New System.Drawing.Size(69, 26)
        Me.ButtonLoad.TabIndex = 5
        Me.ButtonLoad.Text = "Load data"
        Me.ButtonLoad.UseVisualStyleBackColor = True
        '
        'DataGridViewExcel
        '
        Me.DataGridViewExcel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewExcel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewExcel.Location = New System.Drawing.Point(0, 126)
        Me.DataGridViewExcel.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.DataGridViewExcel.Name = "DataGridViewExcel"
        Me.DataGridViewExcel.RowHeadersWidth = 51
        Me.DataGridViewExcel.RowTemplate.Height = 24
        Me.DataGridViewExcel.Size = New System.Drawing.Size(607, 287)
        Me.DataGridViewExcel.TabIndex = 6
        '
        'ButtonEntiteiten
        '
        Me.ButtonEntiteiten.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ButtonEntiteiten.Location = New System.Drawing.Point(15, 73)
        Me.ButtonEntiteiten.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ButtonEntiteiten.Name = "ButtonEntiteiten"
        Me.ButtonEntiteiten.Size = New System.Drawing.Size(65, 26)
        Me.ButtonEntiteiten.TabIndex = 8
        Me.ButtonEntiteiten.Text = "Entities"
        Me.ButtonEntiteiten.UseVisualStyleBackColor = False
        '
        'ButtonAttributes
        '
        Me.ButtonAttributes.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ButtonAttributes.Location = New System.Drawing.Point(91, 73)
        Me.ButtonAttributes.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ButtonAttributes.Name = "ButtonAttributes"
        Me.ButtonAttributes.Size = New System.Drawing.Size(75, 26)
        Me.ButtonAttributes.TabIndex = 9
        Me.ButtonAttributes.Text = "Attributes"
        Me.ButtonAttributes.UseVisualStyleBackColor = False
        '
        'ButtonAssociaties
        '
        Me.ButtonAssociaties.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ButtonAssociaties.Location = New System.Drawing.Point(176, 73)
        Me.ButtonAssociaties.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ButtonAssociaties.Name = "ButtonAssociaties"
        Me.ButtonAssociaties.Size = New System.Drawing.Size(84, 26)
        Me.ButtonAssociaties.TabIndex = 10
        Me.ButtonAssociaties.Text = "Associations"
        Me.ButtonAssociaties.UseVisualStyleBackColor = False
        '
        'TextBoxTableNo
        '
        Me.TextBoxTableNo.Location = New System.Drawing.Point(557, 0)
        Me.TextBoxTableNo.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.TextBoxTableNo.Name = "TextBoxTableNo"
        Me.TextBoxTableNo.Size = New System.Drawing.Size(44, 20)
        Me.TextBoxTableNo.TabIndex = 11
        Me.TextBoxTableNo.Text = "0"
        '
        'ButtonCleanLinkes
        '
        Me.ButtonCleanLinkes.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ButtonCleanLinkes.Location = New System.Drawing.Point(274, 73)
        Me.ButtonCleanLinkes.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.ButtonCleanLinkes.Name = "ButtonCleanLinkes"
        Me.ButtonCleanLinkes.Size = New System.Drawing.Size(75, 26)
        Me.ButtonCleanLinkes.TabIndex = 12
        Me.ButtonCleanLinkes.Text = "Clean Links"
        Me.ButtonCleanLinkes.UseVisualStyleBackColor = False
        '
        'ButtonImportRequirements
        '
        Me.ButtonImportRequirements.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ButtonImportRequirements.Location = New System.Drawing.Point(521, 73)
        Me.ButtonImportRequirements.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.ButtonImportRequirements.Name = "ButtonImportRequirements"
        Me.ButtonImportRequirements.Size = New System.Drawing.Size(79, 26)
        Me.ButtonImportRequirements.TabIndex = 35
        Me.ButtonImportRequirements.Text = "Requirements"
        Me.ButtonImportRequirements.UseVisualStyleBackColor = False
        '
        'ButtonKilMan
        '
        Me.ButtonKilMan.Location = New System.Drawing.Point(461, 77)
        Me.ButtonKilMan.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.ButtonKilMan.Name = "ButtonKilMan"
        Me.ButtonKilMan.Size = New System.Drawing.Size(56, 19)
        Me.ButtonKilMan.TabIndex = 36
        Me.ButtonKilMan.Text = "LVNL"
        Me.ButtonKilMan.UseVisualStyleBackColor = True
        '
        'ProgressBar
        '
        Me.ProgressBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar.Location = New System.Drawing.Point(0, 105)
        Me.ProgressBar.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(607, 19)
        Me.ProgressBar.TabIndex = 37
        '
        'FrmImportExcel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(607, 413)
        Me.Controls.Add(Me.ProgressBar)
        Me.Controls.Add(Me.ButtonKilMan)
        Me.Controls.Add(Me.ButtonImportRequirements)
        Me.Controls.Add(Me.ButtonCleanLinkes)
        Me.Controls.Add(Me.TextBoxTableNo)
        Me.Controls.Add(Me.ButtonAssociaties)
        Me.Controls.Add(Me.ButtonAttributes)
        Me.Controls.Add(Me.ButtonEntiteiten)
        Me.Controls.Add(Me.DataGridViewExcel)
        Me.Controls.Add(Me.ButtonLoad)
        Me.Controls.Add(Me.ButtonSelect)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxExcelFile)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxConnection)
        Me.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Name = "FrmImportExcel"
        Me.Text = "BizzDesign ERD importer"
        CType(Me.DataGridViewExcel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBoxConnection As System.Windows.Forms.TextBox
    Friend WithEvents OpenExcelFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxExcelFile As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ButtonSelect As System.Windows.Forms.Button
    Friend WithEvents ButtonLoad As System.Windows.Forms.Button
    Friend WithEvents DataGridViewExcel As System.Windows.Forms.DataGridView
    Friend WithEvents ButtonEntiteiten As System.Windows.Forms.Button
    Friend WithEvents ButtonAttributes As System.Windows.Forms.Button
    Friend WithEvents ButtonAssociaties As System.Windows.Forms.Button
    Friend WithEvents TextBoxTableNo As System.Windows.Forms.TextBox
    Friend WithEvents ButtonCleanLinkes As System.Windows.Forms.Button
    Friend WithEvents ButtonImportRequirements As System.Windows.Forms.Button
    Friend WithEvents ButtonKilMan As System.Windows.Forms.Button
    Friend WithEvents ProgressBar As System.Windows.Forms.ProgressBar
End Class
