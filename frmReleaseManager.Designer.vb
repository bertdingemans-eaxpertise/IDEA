''' <summary>
''' Screen for doing release management in a DTAP configured environment
''' </summary>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmReleaseManager
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboBoxPackage = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ComboBoxBaseline = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.CheckBoxRestoreBaseline = New System.Windows.Forms.CheckBox()
        Me.ButtonAddPackage = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ButtonClearSelection = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.TextBoxResult = New System.Windows.Forms.TextBox()
        Me.TextBoxDirectory = New System.Windows.Forms.TextBox()
        Me.TextBoxVersion = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ButtonMakeRelease = New System.Windows.Forms.Button()
        Me.ButtonImportRelease = New System.Windows.Forms.Button()
        Me.DataGridViewRelease = New System.Windows.Forms.DataGridView()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DataGridViewRelease, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(76, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(850, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Release manager for baselines in develop and production environments"
        '
        'ComboBoxPackage
        '
        Me.ComboBoxPackage.FormattingEnabled = True
        Me.ComboBoxPackage.Location = New System.Drawing.Point(144, 28)
        Me.ComboBoxPackage.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ComboBoxPackage.Name = "ComboBoxPackage"
        Me.ComboBoxPackage.Size = New System.Drawing.Size(484, 24)
        Me.ComboBoxPackage.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 66)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(102, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Team package"
        '
        'ComboBoxBaseline
        '
        Me.ComboBoxBaseline.FormattingEnabled = True
        Me.ComboBoxBaseline.Location = New System.Drawing.Point(328, 60)
        Me.ComboBoxBaseline.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ComboBoxBaseline.Name = "ComboBoxBaseline"
        Me.ComboBoxBaseline.Size = New System.Drawing.Size(300, 24)
        Me.ComboBoxBaseline.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(217, 63)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 17)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Select baseline"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CheckBoxRestoreBaseline)
        Me.GroupBox1.Controls.Add(Me.ButtonAddPackage)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.ComboBoxBaseline)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.ComboBoxPackage)
        Me.GroupBox1.Location = New System.Drawing.Point(11, 34)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox1.Size = New System.Drawing.Size(635, 146)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Package"
        '
        'CheckBoxRestoreBaseline
        '
        Me.CheckBoxRestoreBaseline.AutoSize = True
        Me.CheckBoxRestoreBaseline.Location = New System.Drawing.Point(5, 63)
        Me.CheckBoxRestoreBaseline.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.CheckBoxRestoreBaseline.Name = "CheckBoxRestoreBaseline"
        Me.CheckBoxRestoreBaseline.Size = New System.Drawing.Size(137, 21)
        Me.CheckBoxRestoreBaseline.TabIndex = 2
        Me.CheckBoxRestoreBaseline.Text = "Restore baseline"
        Me.CheckBoxRestoreBaseline.UseVisualStyleBackColor = True
        '
        'ButtonAddPackage
        '
        Me.ButtonAddPackage.Location = New System.Drawing.Point(472, 103)
        Me.ButtonAddPackage.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ButtonAddPackage.Name = "ButtonAddPackage"
        Me.ButtonAddPackage.Size = New System.Drawing.Size(156, 36)
        Me.ButtonAddPackage.TabIndex = 9
        Me.ButtonAddPackage.Text = "Add package"
        Me.ButtonAddPackage.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(715, 0)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox2.Size = New System.Drawing.Size(309, 202)
        Me.GroupBox2.TabIndex = 6
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Repository"
        '
        'ButtonClearSelection
        '
        Me.ButtonClearSelection.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonClearSelection.Location = New System.Drawing.Point(658, 308)
        Me.ButtonClearSelection.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ButtonClearSelection.Name = "ButtonClearSelection"
        Me.ButtonClearSelection.Size = New System.Drawing.Size(156, 48)
        Me.ButtonClearSelection.TabIndex = 10
        Me.ButtonClearSelection.Text = "Clear Selection"
        Me.ButtonClearSelection.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.TextBoxResult)
        Me.GroupBox3.Controls.Add(Me.TextBoxDirectory)
        Me.GroupBox3.Controls.Add(Me.TextBoxVersion)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Location = New System.Drawing.Point(651, 34)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox3.Size = New System.Drawing.Size(517, 252)
        Me.GroupBox3.TabIndex = 6
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Repository"
        '
        'TextBoxResult
        '
        Me.TextBoxResult.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxResult.Location = New System.Drawing.Point(11, 110)
        Me.TextBoxResult.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TextBoxResult.Multiline = True
        Me.TextBoxResult.Name = "TextBoxResult"
        Me.TextBoxResult.ReadOnly = True
        Me.TextBoxResult.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBoxResult.Size = New System.Drawing.Size(500, 131)
        Me.TextBoxResult.TabIndex = 10
        '
        'TextBoxDirectory
        '
        Me.TextBoxDirectory.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDirectory.Location = New System.Drawing.Point(140, 69)
        Me.TextBoxDirectory.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TextBoxDirectory.Name = "TextBoxDirectory"
        Me.TextBoxDirectory.Size = New System.Drawing.Size(309, 22)
        Me.TextBoxDirectory.TabIndex = 7
        '
        'TextBoxVersion
        '
        Me.TextBoxVersion.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxVersion.Location = New System.Drawing.Point(140, 37)
        Me.TextBoxVersion.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TextBoxVersion.Name = "TextBoxVersion"
        Me.TextBoxVersion.Size = New System.Drawing.Size(309, 22)
        Me.TextBoxVersion.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(5, 73)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(129, 17)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Exchange directory"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(5, 42)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 17)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Version"
        '
        'ButtonMakeRelease
        '
        Me.ButtonMakeRelease.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonMakeRelease.Location = New System.Drawing.Point(841, 308)
        Me.ButtonMakeRelease.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ButtonMakeRelease.Name = "ButtonMakeRelease"
        Me.ButtonMakeRelease.Size = New System.Drawing.Size(156, 48)
        Me.ButtonMakeRelease.TabIndex = 11
        Me.ButtonMakeRelease.Text = "Export Release (development)"
        Me.ButtonMakeRelease.UseVisualStyleBackColor = True
        '
        'ButtonImportRelease
        '
        Me.ButtonImportRelease.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonImportRelease.Location = New System.Drawing.Point(1003, 308)
        Me.ButtonImportRelease.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ButtonImportRelease.Name = "ButtonImportRelease"
        Me.ButtonImportRelease.Size = New System.Drawing.Size(156, 48)
        Me.ButtonImportRelease.TabIndex = 12
        Me.ButtonImportRelease.Text = "Import Release (production)"
        Me.ButtonImportRelease.UseVisualStyleBackColor = True
        '
        'DataGridViewRelease
        '
        Me.DataGridViewRelease.AllowUserToAddRows = False
        Me.DataGridViewRelease.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewRelease.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewRelease.Location = New System.Drawing.Point(11, 185)
        Me.DataGridViewRelease.Name = "DataGridViewRelease"
        Me.DataGridViewRelease.ReadOnly = True
        Me.DataGridViewRelease.RowHeadersWidth = 51
        Me.DataGridViewRelease.RowTemplate.Height = 24
        Me.DataGridViewRelease.Size = New System.Drawing.Size(634, 178)
        Me.DataGridViewRelease.TabIndex = 13
        '
        'frmReleaseManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1179, 358)
        Me.Controls.Add(Me.DataGridViewRelease)
        Me.Controls.Add(Me.ButtonImportRelease)
        Me.Controls.Add(Me.ButtonMakeRelease)
        Me.Controls.Add(Me.ButtonClearSelection)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "frmReleaseManager"
        Me.Text = "NS ReleaseManager for EA"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.DataGridViewRelease, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxPackage As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxBaseline As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonAddPackage As System.Windows.Forms.Button
    Friend WithEvents TextBoxDirectory As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxVersion As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ButtonClearSelection As System.Windows.Forms.Button
    Friend WithEvents ButtonMakeRelease As System.Windows.Forms.Button
    Friend WithEvents TextBoxResult As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxRestoreBaseline As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonImportRelease As System.Windows.Forms.Button
    Friend WithEvents DataGridViewRelease As System.Windows.Forms.DataGridView
End Class
