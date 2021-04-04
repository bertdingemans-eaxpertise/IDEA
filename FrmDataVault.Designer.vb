<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDataVault
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
        Me.TabPageAddItem = New System.Windows.Forms.TabPage()
        Me.TextBoxTemplateReplaceValue = New System.Windows.Forms.TextBox()
        Me.TextBoxName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ButtonViewBrowser = New System.Windows.Forms.Button()
        Me.ButtonAdd2Diagram = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ListBoxTemplates = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPageSearchDE = New System.Windows.Forms.TabPage()
        Me.CheckBoxWildcard = New System.Windows.Forms.CheckBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ListBoxConcepts = New System.Windows.Forms.ListBox()
        Me.GridviewElements = New System.Windows.Forms.DataGridView()
        Me.ButtonSearch = New System.Windows.Forms.Button()
        Me.ButtonAddElement = New System.Windows.Forms.Button()
        Me.TextBoxSearch = New System.Windows.Forms.TextBox()
        Me.TabControlDV = New System.Windows.Forms.TabControl()
        Me.TabPageAddItem.SuspendLayout()
        Me.TabPageSearchDE.SuspendLayout()
        CType(Me.GridviewElements, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControlDV.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabPageAddItem
        '
        Me.TabPageAddItem.BackColor = System.Drawing.Color.LightGray
        Me.TabPageAddItem.Controls.Add(Me.TextBoxTemplateReplaceValue)
        Me.TabPageAddItem.Controls.Add(Me.TextBoxName)
        Me.TabPageAddItem.Controls.Add(Me.Label3)
        Me.TabPageAddItem.Controls.Add(Me.ButtonViewBrowser)
        Me.TabPageAddItem.Controls.Add(Me.ButtonAdd2Diagram)
        Me.TabPageAddItem.Controls.Add(Me.Label4)
        Me.TabPageAddItem.Controls.Add(Me.ListBoxTemplates)
        Me.TabPageAddItem.Controls.Add(Me.Label1)
        Me.TabPageAddItem.Location = New System.Drawing.Point(4, 22)
        Me.TabPageAddItem.Margin = New System.Windows.Forms.Padding(2)
        Me.TabPageAddItem.Name = "TabPageAddItem"
        Me.TabPageAddItem.Padding = New System.Windows.Forms.Padding(2)
        Me.TabPageAddItem.Size = New System.Drawing.Size(312, 366)
        Me.TabPageAddItem.TabIndex = 0
        Me.TabPageAddItem.Text = "Add Item"
        '
        'TextBoxTemplateReplaceValue
        '
        Me.TextBoxTemplateReplaceValue.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTemplateReplaceValue.Location = New System.Drawing.Point(130, 287)
        Me.TextBoxTemplateReplaceValue.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxTemplateReplaceValue.Name = "TextBoxTemplateReplaceValue"
        Me.TextBoxTemplateReplaceValue.Size = New System.Drawing.Size(182, 20)
        Me.TextBoxTemplateReplaceValue.TabIndex = 9
        '
        'TextBoxName
        '
        Me.TextBoxName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxName.Location = New System.Drawing.Point(47, 311)
        Me.TextBoxName.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxName.Name = "TextBoxName"
        Me.TextBoxName.Size = New System.Drawing.Size(265, 20)
        Me.TextBoxName.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 288)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(124, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Template Replace Value"
        '
        'ButtonViewBrowser
        '
        Me.ButtonViewBrowser.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonViewBrowser.Location = New System.Drawing.Point(2, 335)
        Me.ButtonViewBrowser.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonViewBrowser.Name = "ButtonViewBrowser"
        Me.ButtonViewBrowser.Size = New System.Drawing.Size(120, 27)
        Me.ButtonViewBrowser.TabIndex = 7
        Me.ButtonViewBrowser.Text = "View in Browser"
        Me.ButtonViewBrowser.UseVisualStyleBackColor = True
        '
        'ButtonAdd2Diagram
        '
        Me.ButtonAdd2Diagram.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonAdd2Diagram.Location = New System.Drawing.Point(188, 335)
        Me.ButtonAdd2Diagram.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonAdd2Diagram.Name = "ButtonAdd2Diagram"
        Me.ButtonAdd2Diagram.Size = New System.Drawing.Size(120, 27)
        Me.ButtonAdd2Diagram.TabIndex = 6
        Me.ButtonAdd2Diagram.Text = "Add 2 Diagram"
        Me.ButtonAdd2Diagram.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(4, 314)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Name"
        '
        'ListBoxTemplates
        '
        Me.ListBoxTemplates.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBoxTemplates.FormattingEnabled = True
        Me.ListBoxTemplates.Location = New System.Drawing.Point(2, 17)
        Me.ListBoxTemplates.Margin = New System.Windows.Forms.Padding(2)
        Me.ListBoxTemplates.Name = "ListBoxTemplates"
        Me.ListBoxTemplates.Size = New System.Drawing.Size(312, 264)
        Me.ListBoxTemplates.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(2, 2)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Template"
        '
        'TabPageSearchDE
        '
        Me.TabPageSearchDE.BackColor = System.Drawing.Color.LightGray
        Me.TabPageSearchDE.Controls.Add(Me.CheckBoxWildcard)
        Me.TabPageSearchDE.Controls.Add(Me.Button1)
        Me.TabPageSearchDE.Controls.Add(Me.ListBoxConcepts)
        Me.TabPageSearchDE.Controls.Add(Me.GridviewElements)
        Me.TabPageSearchDE.Controls.Add(Me.ButtonSearch)
        Me.TabPageSearchDE.Controls.Add(Me.ButtonAddElement)
        Me.TabPageSearchDE.Controls.Add(Me.TextBoxSearch)
        Me.TabPageSearchDE.Location = New System.Drawing.Point(4, 22)
        Me.TabPageSearchDE.Name = "TabPageSearchDE"
        Me.TabPageSearchDE.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageSearchDE.Size = New System.Drawing.Size(312, 366)
        Me.TabPageSearchDE.TabIndex = 2
        Me.TabPageSearchDE.Text = "Search Data Element"
        '
        'CheckBoxWildcard
        '
        Me.CheckBoxWildcard.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxWildcard.AutoSize = True
        Me.CheckBoxWildcard.Checked = True
        Me.CheckBoxWildcard.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxWildcard.Location = New System.Drawing.Point(239, 89)
        Me.CheckBoxWildcard.Name = "CheckBoxWildcard"
        Me.CheckBoxWildcard.Size = New System.Drawing.Size(68, 17)
        Me.CheckBoxWildcard.TabIndex = 17
        Me.CheckBoxWildcard.Text = "Wildcard"
        Me.CheckBoxWildcard.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(2, 132)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(133, 23)
        Me.Button1.TabIndex = 16
        Me.Button1.Text = "View in Browser"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ListBoxConcepts
        '
        Me.ListBoxConcepts.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBoxConcepts.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBoxConcepts.FormattingEnabled = True
        Me.ListBoxConcepts.ItemHeight = 16
        Me.ListBoxConcepts.Location = New System.Drawing.Point(2, 0)
        Me.ListBoxConcepts.Margin = New System.Windows.Forms.Padding(2)
        Me.ListBoxConcepts.Name = "ListBoxConcepts"
        Me.ListBoxConcepts.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBoxConcepts.Size = New System.Drawing.Size(308, 84)
        Me.ListBoxConcepts.TabIndex = 15
        '
        'GridviewElements
        '
        Me.GridviewElements.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridviewElements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridviewElements.Location = New System.Drawing.Point(2, 159)
        Me.GridviewElements.Margin = New System.Windows.Forms.Padding(2)
        Me.GridviewElements.Name = "GridviewElements"
        Me.GridviewElements.RowHeadersWidth = 70
        Me.GridviewElements.RowTemplate.Height = 24
        Me.GridviewElements.RowTemplate.ReadOnly = True
        Me.GridviewElements.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridviewElements.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GridviewElements.Size = New System.Drawing.Size(310, 207)
        Me.GridviewElements.TabIndex = 14
        '
        'ButtonSearch
        '
        Me.ButtonSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSearch.Location = New System.Drawing.Point(248, 105)
        Me.ButtonSearch.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonSearch.Name = "ButtonSearch"
        Me.ButtonSearch.Size = New System.Drawing.Size(62, 23)
        Me.ButtonSearch.TabIndex = 11
        Me.ButtonSearch.Text = "Search"
        Me.ButtonSearch.UseVisualStyleBackColor = True
        '
        'ButtonAddElement
        '
        Me.ButtonAddElement.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonAddElement.Location = New System.Drawing.Point(183, 132)
        Me.ButtonAddElement.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonAddElement.Name = "ButtonAddElement"
        Me.ButtonAddElement.Size = New System.Drawing.Size(129, 23)
        Me.ButtonAddElement.TabIndex = 13
        Me.ButtonAddElement.Text = "Add 2 Diagram"
        Me.ButtonAddElement.UseVisualStyleBackColor = True
        '
        'TextBoxSearch
        '
        Me.TextBoxSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSearch.BackColor = System.Drawing.SystemColors.Info
        Me.TextBoxSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxSearch.Location = New System.Drawing.Point(2, 91)
        Me.TextBoxSearch.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxSearch.Name = "TextBoxSearch"
        Me.TextBoxSearch.Size = New System.Drawing.Size(232, 26)
        Me.TextBoxSearch.TabIndex = 12
        '
        'TabControlDV
        '
        Me.TabControlDV.Controls.Add(Me.TabPageSearchDE)
        Me.TabControlDV.Controls.Add(Me.TabPageAddItem)
        Me.TabControlDV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlDV.Location = New System.Drawing.Point(0, 0)
        Me.TabControlDV.Margin = New System.Windows.Forms.Padding(2)
        Me.TabControlDV.Name = "TabControlDV"
        Me.TabControlDV.SelectedIndex = 0
        Me.TabControlDV.Size = New System.Drawing.Size(320, 392)
        Me.TabControlDV.TabIndex = 0
        '
        'FrmDataVault
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(320, 392)
        Me.Controls.Add(Me.TabControlDV)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "FrmDataVault"
        Me.Text = "DatAID"
        Me.TopMost = True
        Me.TabPageAddItem.ResumeLayout(False)
        Me.TabPageAddItem.PerformLayout()
        Me.TabPageSearchDE.ResumeLayout(False)
        Me.TabPageSearchDE.PerformLayout()
        CType(Me.GridviewElements, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControlDV.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabPageAddItem As System.Windows.Forms.TabPage
    Friend WithEvents TextBoxTemplateReplaceValue As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ButtonViewBrowser As System.Windows.Forms.Button
    Friend WithEvents ButtonAdd2Diagram As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ListBoxTemplates As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TabPageSearchDE As System.Windows.Forms.TabPage
    Friend WithEvents CheckBoxWildcard As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ListBoxConcepts As System.Windows.Forms.ListBox
    Friend WithEvents GridviewElements As System.Windows.Forms.DataGridView
    Friend WithEvents ButtonSearch As System.Windows.Forms.Button
    Friend WithEvents ButtonAddElement As System.Windows.Forms.Button
    Friend WithEvents TextBoxSearch As System.Windows.Forms.TextBox
    Friend WithEvents TabControlDV As System.Windows.Forms.TabControl
End Class
