''' <summary>
''' Form for displaying the elements that are duplicate in the repository. Give a
''' warning and eventually add the duplicate element to the diagram.
''' </summary>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class FrmUniqueElement
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
        Me.UniqueDataGridView = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ButtonAdd2Diagram = New System.Windows.Forms.Button()
        Me.ButtonDeduplicate = New System.Windows.Forms.Button()
        Me.ButtonFindOriginal = New System.Windows.Forms.Button()
        Me.ButtonFindDuplicate = New System.Windows.Forms.Button()
        CType(Me.UniqueDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'UniqueDataGridView
        '
        Me.UniqueDataGridView.AllowUserToAddRows = False
        Me.UniqueDataGridView.AllowUserToDeleteRows = False
        Me.UniqueDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UniqueDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.UniqueDataGridView.Location = New System.Drawing.Point(2, 46)
        Me.UniqueDataGridView.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.UniqueDataGridView.Name = "UniqueDataGridView"
        Me.UniqueDataGridView.RowHeadersWidth = 51
        Me.UniqueDataGridView.RowTemplate.Height = 24
        Me.UniqueDataGridView.Size = New System.Drawing.Size(596, 206)
        Me.UniqueDataGridView.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 7)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 18)
        Me.Label1.TabIndex = 1
        '
        'ButtonAdd2Diagram
        '
        Me.ButtonAdd2Diagram.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonAdd2Diagram.Location = New System.Drawing.Point(513, 7)
        Me.ButtonAdd2Diagram.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.ButtonAdd2Diagram.Name = "ButtonAdd2Diagram"
        Me.ButtonAdd2Diagram.Size = New System.Drawing.Size(86, 35)
        Me.ButtonAdd2Diagram.TabIndex = 2
        Me.ButtonAdd2Diagram.Text = "Add2Diagram"
        Me.ButtonAdd2Diagram.UseVisualStyleBackColor = True
        '
        'ButtonDeduplicate
        '
        Me.ButtonDeduplicate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonDeduplicate.Location = New System.Drawing.Point(431, 7)
        Me.ButtonDeduplicate.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.ButtonDeduplicate.Name = "ButtonDeduplicate"
        Me.ButtonDeduplicate.Size = New System.Drawing.Size(78, 35)
        Me.ButtonDeduplicate.TabIndex = 3
        Me.ButtonDeduplicate.Text = "Deduplicate"
        Me.ButtonDeduplicate.UseVisualStyleBackColor = True
        '
        'ButtonFindOriginal
        '
        Me.ButtonFindOriginal.Location = New System.Drawing.Point(2, 7)
        Me.ButtonFindOriginal.Name = "ButtonFindOriginal"
        Me.ButtonFindOriginal.Size = New System.Drawing.Size(75, 32)
        Me.ButtonFindOriginal.TabIndex = 4
        Me.ButtonFindOriginal.Text = "Find Original"
        Me.ButtonFindOriginal.UseVisualStyleBackColor = True
        '
        'ButtonFindDuplicate
        '
        Me.ButtonFindDuplicate.Location = New System.Drawing.Point(83, 7)
        Me.ButtonFindDuplicate.Name = "ButtonFindDuplicate"
        Me.ButtonFindDuplicate.Size = New System.Drawing.Size(87, 32)
        Me.ButtonFindDuplicate.TabIndex = 5
        Me.ButtonFindDuplicate.Text = "Find Duplicate"
        Me.ButtonFindDuplicate.UseVisualStyleBackColor = True
        '
        'FrmUniqueElement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(598, 254)
        Me.Controls.Add(Me.ButtonFindDuplicate)
        Me.Controls.Add(Me.ButtonFindOriginal)
        Me.Controls.Add(Me.ButtonDeduplicate)
        Me.Controls.Add(Me.ButtonAdd2Diagram)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.UniqueDataGridView)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "FrmUniqueElement"
        Me.Text = "Validate Unique Element"
        Me.TopMost = True
        CType(Me.UniqueDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents UniqueDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ButtonAdd2Diagram As System.Windows.Forms.Button
    Friend WithEvents ButtonDeduplicate As System.Windows.Forms.Button
    Friend WithEvents ButtonFindOriginal As System.Windows.Forms.Button
    Friend WithEvents ButtonFindDuplicate As System.Windows.Forms.Button
End Class
