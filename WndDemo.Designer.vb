<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WndDemo
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
        Me.ButtonTest = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ButtonTest
        '
        Me.ButtonTest.Location = New System.Drawing.Point(203, 300)
        Me.ButtonTest.Name = "ButtonTest"
        Me.ButtonTest.Size = New System.Drawing.Size(103, 40)
        Me.ButtonTest.TabIndex = 0
        Me.ButtonTest.Text = "Test"
        Me.ButtonTest.UseVisualStyleBackColor = True
        '
        'WndDemo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(498, 352)
        Me.Controls.Add(Me.ButtonTest)
        Me.Name = "WndDemo"
        Me.Text = "WndDemo"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ButtonTest As System.Windows.Forms.Button
End Class
