<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDataSetInspector
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
        Me.ComboBoxDataTable = New System.Windows.Forms.ComboBox()
        Me.ButtonDisplayTable = New System.Windows.Forms.Button()
        Me.DataGridViewDataTable = New System.Windows.Forms.DataGridView()
        CType(Me.DataGridViewDataTable, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ComboBoxDataTable
        '
        Me.ComboBoxDataTable.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxDataTable.FormattingEnabled = True
        Me.ComboBoxDataTable.Location = New System.Drawing.Point(2, 1)
        Me.ComboBoxDataTable.Name = "ComboBoxDataTable"
        Me.ComboBoxDataTable.Size = New System.Drawing.Size(611, 21)
        Me.ComboBoxDataTable.TabIndex = 0
        '
        'ButtonDisplayTable
        '
        Me.ButtonDisplayTable.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonDisplayTable.Location = New System.Drawing.Point(643, 1)
        Me.ButtonDisplayTable.Name = "ButtonDisplayTable"
        Me.ButtonDisplayTable.Size = New System.Drawing.Size(154, 23)
        Me.ButtonDisplayTable.TabIndex = 1
        Me.ButtonDisplayTable.Text = "Display Table"
        Me.ButtonDisplayTable.UseVisualStyleBackColor = True
        '
        'DataGridViewDataTable
        '
        Me.DataGridViewDataTable.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewDataTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewDataTable.Location = New System.Drawing.Point(2, 28)
        Me.DataGridViewDataTable.Name = "DataGridViewDataTable"
        Me.DataGridViewDataTable.Size = New System.Drawing.Size(795, 420)
        Me.DataGridViewDataTable.TabIndex = 2
        '
        'FrmDataSetInspector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.DataGridViewDataTable)
        Me.Controls.Add(Me.ButtonDisplayTable)
        Me.Controls.Add(Me.ComboBoxDataTable)
        Me.Name = "FrmDataSetInspector"
        Me.Text = "DataSet Inspector"
        CType(Me.DataGridViewDataTable, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ComboBoxDataTable As System.Windows.Forms.ComboBox
    Friend WithEvents ButtonDisplayTable As System.Windows.Forms.Button
    Friend WithEvents DataGridViewDataTable As System.Windows.Forms.DataGridView
End Class
