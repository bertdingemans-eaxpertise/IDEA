<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Interactory_Simulator
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Interactory_Simulator))
        Me.TabPageDisplay = New System.Windows.Forms.TabPage()
        Me.DataGridViewDetail = New System.Windows.Forms.DataGridView()
        Me.TabPageDetail = New System.Windows.Forms.TabPage()
        Me.TabControlDetail = New System.Windows.Forms.TabControl()
        Me.TreeViewInteractory = New System.Windows.Forms.TreeView()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.ToolStripButtonSave = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.FilterBut = New System.Windows.Forms.ToolStripButton()
        Me.LoadChildrenButton = New System.Windows.Forms.ToolStripButton()
        Me.DispCon = New System.Windows.Forms.ToolStripButton()
        Me.ComboBoxModelType = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripButtonLoadMetaModel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripFilter = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripButtonReload = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripButtonDelete = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.TabPageDisplay.SuspendLayout()
        CType(Me.DataGridViewDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControlDetail.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabPageDisplay
        '
        Me.TabPageDisplay.Controls.Add(Me.DataGridViewDetail)
        Me.TabPageDisplay.Location = New System.Drawing.Point(4, 22)
        Me.TabPageDisplay.Name = "TabPageDisplay"
        Me.TabPageDisplay.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageDisplay.Size = New System.Drawing.Size(567, 478)
        Me.TabPageDisplay.TabIndex = 0
        Me.TabPageDisplay.Text = "Display"
        Me.TabPageDisplay.ToolTipText = "Display the element or collection of elements in  a grid"
        Me.TabPageDisplay.UseVisualStyleBackColor = True
        '
        'DataGridViewDetail
        '
        Me.DataGridViewDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridViewDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewDetail.Location = New System.Drawing.Point(3, 3)
        Me.DataGridViewDetail.Margin = New System.Windows.Forms.Padding(2)
        Me.DataGridViewDetail.Name = "DataGridViewDetail"
        Me.DataGridViewDetail.RowHeadersWidth = 51
        Me.DataGridViewDetail.RowTemplate.Height = 24
        Me.DataGridViewDetail.Size = New System.Drawing.Size(561, 472)
        Me.DataGridViewDetail.TabIndex = 4
        '
        'TabPageDetail
        '
        Me.TabPageDetail.AutoScroll = True
        Me.TabPageDetail.BackColor = System.Drawing.Color.LightGray
        Me.TabPageDetail.Location = New System.Drawing.Point(4, 22)
        Me.TabPageDetail.Name = "TabPageDetail"
        Me.TabPageDetail.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageDetail.Size = New System.Drawing.Size(567, 478)
        Me.TabPageDetail.TabIndex = 1
        Me.TabPageDetail.Text = "Modify"
        Me.TabPageDetail.ToolTipText = "Display the element in edit mode"
        '
        'TabControlDetail
        '
        Me.TabControlDetail.Controls.Add(Me.TabPageDisplay)
        Me.TabControlDetail.Controls.Add(Me.TabPageDetail)
        Me.TabControlDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlDetail.Location = New System.Drawing.Point(0, 0)
        Me.TabControlDetail.Name = "TabControlDetail"
        Me.TabControlDetail.SelectedIndex = 0
        Me.TabControlDetail.Size = New System.Drawing.Size(575, 504)
        Me.TabControlDetail.TabIndex = 1
        '
        'TreeViewInteractory
        '
        Me.TreeViewInteractory.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.TreeViewInteractory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeViewInteractory.Location = New System.Drawing.Point(0, 0)
        Me.TreeViewInteractory.Margin = New System.Windows.Forms.Padding(2)
        Me.TreeViewInteractory.Name = "TreeViewInteractory"
        Me.TreeViewInteractory.Size = New System.Drawing.Size(264, 504)
        Me.TreeViewInteractory.TabIndex = 0
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 27)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(2)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TreeViewInteractory)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TabControlDetail)
        Me.SplitContainer1.Size = New System.Drawing.Size(842, 504)
        Me.SplitContainer1.SplitterDistance = 264
        Me.SplitContainer1.SplitterWidth = 3
        Me.SplitContainer1.TabIndex = 3
        '
        'ToolStripButtonSave
        '
        Me.ToolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonSave.Image = CType(resources.GetObject("ToolStripButtonSave.Image"), System.Drawing.Image)
        Me.ToolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonSave.Name = "ToolStripButtonSave"
        Me.ToolStripButtonSave.Size = New System.Drawing.Size(24, 24)
        Me.ToolStripButtonSave.Text = "Save"
        Me.ToolStripButtonSave.ToolTipText = "Save the modifications in the screen"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 27)
        '
        'FilterBut
        '
        Me.FilterBut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.FilterBut.Image = CType(resources.GetObject("FilterBut.Image"), System.Drawing.Image)
        Me.FilterBut.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.FilterBut.Name = "FilterBut"
        Me.FilterBut.Size = New System.Drawing.Size(24, 24)
        Me.FilterBut.Text = "Filter"
        '
        'LoadChildrenButton
        '
        Me.LoadChildrenButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.LoadChildrenButton.Image = CType(resources.GetObject("LoadChildrenButton.Image"), System.Drawing.Image)
        Me.LoadChildrenButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.LoadChildrenButton.Name = "LoadChildrenButton"
        Me.LoadChildrenButton.Size = New System.Drawing.Size(24, 24)
        Me.LoadChildrenButton.Text = "Load Children"
        Me.LoadChildrenButton.ToolTipText = "Load Children for this item"
        '
        'DispCon
        '
        Me.DispCon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.DispCon.Image = CType(resources.GetObject("DispCon.Image"), System.Drawing.Image)
        Me.DispCon.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.DispCon.Name = "DispCon"
        Me.DispCon.Size = New System.Drawing.Size(24, 24)
        Me.DispCon.Text = "Display connection"
        Me.DispCon.ToolTipText = "Display connection information"
        '
        'ComboBoxModelType
        '
        Me.ComboBoxModelType.Items.AddRange(New Object() {"SQL", "XML"})
        Me.ComboBoxModelType.Name = "ComboBoxModelType"
        Me.ComboBoxModelType.Size = New System.Drawing.Size(92, 27)
        Me.ComboBoxModelType.Text = "SQL"
        Me.ComboBoxModelType.ToolTipText = "Select the storage type of the model"
        '
        'ToolStripButtonLoadMetaModel
        '
        Me.ToolStripButtonLoadMetaModel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonLoadMetaModel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonLoadMetaModel.Name = "ToolStripButtonLoadMetaModel"
        Me.ToolStripButtonLoadMetaModel.Size = New System.Drawing.Size(23, 24)
        Me.ToolStripButtonLoadMetaModel.Text = "Load metamodel"
        '
        'ToolStripFilter
        '
        Me.ToolStripFilter.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ToolStripFilter.Name = "ToolStripFilter"
        Me.ToolStripFilter.Size = New System.Drawing.Size(100, 27)
        Me.ToolStripFilter.ToolTipText = "Enter filtertext and press filter button"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1, Me.ToolStripButtonLoadMetaModel, Me.ComboBoxModelType, Me.DispCon, Me.ToolStripButtonReload, Me.ToolStripFilter, Me.FilterBut, Me.ToolStripSeparator4, Me.ToolStripLabel2, Me.ToolStripSeparator1, Me.LoadChildrenButton, Me.ToolStripButtonSave, Me.ToolStripButtonDelete, Me.ToolStripButton1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(842, 27)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(34, 24)
        Me.ToolStripLabel1.Text = "Repo"
        '
        'ToolStripButtonReload
        '
        Me.ToolStripButtonReload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonReload.Image = CType(resources.GetObject("ToolStripButtonReload.Image"), System.Drawing.Image)
        Me.ToolStripButtonReload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonReload.Name = "ToolStripButtonReload"
        Me.ToolStripButtonReload.Size = New System.Drawing.Size(24, 24)
        Me.ToolStripButtonReload.Text = "Reload"
        Me.ToolStripButtonReload.ToolTipText = "Reload the data"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 27)
        '
        'ToolStripLabel2
        '
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        Me.ToolStripLabel2.Size = New System.Drawing.Size(50, 24)
        Me.ToolStripLabel2.Text = "Element"
        '
        'ToolStripButtonDelete
        '
        Me.ToolStripButtonDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonDelete.Image = CType(resources.GetObject("ToolStripButtonDelete.Image"), System.Drawing.Image)
        Me.ToolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonDelete.Name = "ToolStripButtonDelete"
        Me.ToolStripButtonDelete.Size = New System.Drawing.Size(24, 24)
        Me.ToolStripButtonDelete.Text = "Delete"
        Me.ToolStripButtonDelete.ToolTipText = "Delete selected tree element"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(31, 24)
        Me.ToolStripButton1.Text = "Test"
        '
        'Interactory_Simulator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(842, 531)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "Interactory_Simulator"
        Me.Text = "Object Model Simulator"
        Me.TabPageDisplay.ResumeLayout(False)
        CType(Me.DataGridViewDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControlDetail.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TabPageDisplay As System.Windows.Forms.TabPage
    Friend WithEvents DataGridViewDetail As System.Windows.Forms.DataGridView
    Friend WithEvents TabPageDetail As System.Windows.Forms.TabPage
    Friend WithEvents TabControlDetail As System.Windows.Forms.TabControl
    Friend WithEvents TreeViewInteractory As System.Windows.Forms.TreeView
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents ToolStripButtonSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents FilterBut As System.Windows.Forms.ToolStripButton
    Friend WithEvents LoadChildrenButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents DispCon As System.Windows.Forms.ToolStripButton
    Friend WithEvents ComboBoxModelType As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripButtonLoadMetaModel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripFilter As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButtonReload As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripLabel2 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripButtonDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
End Class
