Imports TEA.DLAFormfactory
Imports System.Windows.Forms


Public Class FrmIDEAFormFactory
    Protected FFGenerator As FormFactoryGenerator
    Protected FFDataset As SimulatorContainer
    Private Sub SelectDeSelect(ByVal state As Boolean)
        Dim i As Int16 = 0
        While i < ListBoxElements.Items.Count
            ListBoxElements.SetItemChecked(i, state)
            i += 1
        End While
    End Sub


    Private Sub ButtonUnselectAll_Click(ByVal sender As Object, ByVal e As EventArgs)
        SelectDeSelect(False)
    End Sub

    Private Sub ButtonToggleAll_Click(ByVal sender As Object, ByVal e As EventArgs)
        'SelectToggle()
    End Sub

    Private Sub LoadElements()
        Dim objDT As DataTable
        Dim strSql As String
        Try
            '    'doen we nu niets mee
            '    Me.TabPageFormFactory.Visible = False
            '    strSql = String.Format("SELECT object_id, name, object_type FROM t_object WHERE package_id = {0} ", Me.ComboBoxPackage.SelectedValue)
            '    objDT = DLA2EAHelper.SQL2DataTable(strSql, Me.Repository)
            '    Me.ListBoxElements.DataSource = Nothing
            '    If Not IsNothing(objDT) Then
            '        Me.ListBoxElements.DataSource = objDT
            '        Me.ListBoxElements.DisplayMember = "name"
            '        Me.ListBoxElements.ValueMember = "object_id"
            '    End If

            strSql = String.Format("SELECT diagram_id, name FROM t_diagram WHERE package_id = {0} ", Me.ComboBoxPackage.SelectedValue)
            objDT = DLA2EAHelper.SQL2DataTable(strSql, Me.Repository)
            ' Me.ListBoxDiagrams.DataSource = Nothing
            If Not IsNothing(objDT) Then
                Me.ListBoxDiagrams.DataSource = objDT
                Me.ListBoxDiagrams.DisplayMember = "name"
                Me.ListBoxDiagrams.ValueMember = "diagram_id"
                Dim i As Int16 = 0
                While i < ListBoxDiagrams.Items.Count
                    ListBoxDiagrams.SetItemChecked(i, (Not ListBoxDiagrams.GetItemChecked(i)))
                    i += 1
                End While
            End If
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub

    Public Sub LoadPackage()
        Dim objDT As DataTable
        Try
            objDT = DLA2EAHelper.SQL2DataTable("SELECT package_id, name FROM t_package ORDER BY 2 ", Me.Repository)
            Me.ComboBoxPackage.DataSource = objDT
            Me.ComboBoxPackage.DisplayMember = "name"
            Me.ComboBoxPackage.ValueMember = "package_id"
            Me.ComboBoxPackage.SelectedValue = Me.Package.PackageID
            Me.LoadElements()
            LoadFormFactoryDataset()
            Me.LabelDatabaseConnection.Text = My.Settings.ConnectionString
            Me.LabelXMLFile.Text = My.Settings.XMLFile
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try

    End Sub
    Sub LoadFormFactoryDataset()
        Me.FFGenerator = New FormFactoryGenerator()
        Me.FFGenerator.Repository = Me.Repository
        Me.FFDataset = Me.FFGenerator.Package2SimulatorDataset(Me.Package, Me.Package.Name)
    End Sub
    Private _Repository As EA.Repository

    Public Property Repository() As EA.Repository
        Get
            Return _Repository
        End Get
        Set(ByVal value As EA.Repository)
            _Repository = value
        End Set
    End Property

    Private _Package As EA.Package
    Public Property Package As EA.Package
        Get
            Return _Package
        End Get
        Set(ByVal value As EA.Package)
            _Package = value
        End Set
    End Property
    Private Sub ButtonLoad_Click(sender As Object, e As EventArgs) Handles ButtonLoad.Click
        Me.LoadElements()
    End Sub
    Private Sub ButtonGenerate_Click_1(sender As Object, e As EventArgs) Handles ButtonGenerate.Click
        Dim objFFGenerator As New FormFactoryGenerator()
        For Each item In Me.ListBoxElements.CheckedItems
            objFFGenerator.Repository = Me.Repository
            objFFGenerator.Element = Me.Repository.GetElementByID(item("object_id"))
            objFFGenerator.GenerateSQL(Me.CheckBoxDelete.Checked, Me.CheckBoxInsert.Checked, Me.CheckBoxSearchCommand.Checked)
            objFFGenerator.GenerateMenuSQL(Me.CheckBoxMenu.Checked, Me.TextBoxMenuParent.Text)
        Next
        If CheckBoxClose.Checked Then
            Me.Close()
        End If
    End Sub

    Private Sub ButtonGenerateDataSet_Click(sender As Object, e As EventArgs) Handles ButtonGenerateDataSet.Click
        Dim DS2SQL As New Dataset2DDL()
        If CheckBoxSaveDataSet.Checked Then
            Me.FFDataset.SaveDataToXML(XmlWriteMode.WriteSchema)
        End If
        If DS2SQL.Dataset2SQL(Me.FFDataset.ContainerDataSet, CheckBoxSystemTables.Checked, CheckBoxModelTables.Checked, CheckBoxForeignKeys.Checked) Then
            Me.TextBoxDatasetCode.Text = DS2SQL.SQL
            If Me.CheckBoxClipBoard.Checked Then
                System.Windows.Forms.Clipboard.SetText(Me.TextBoxDatasetCode.Text)
            End If
        Else
            MsgBox("SQL generating NOT successful")
        End If
    End Sub

    Private Sub ButtonSimulator_Click(sender As Object, e As EventArgs) Handles ButtonSimulator.Click
        Dim FrmSimulator As New Interactory_Simulator()
        Dim aElementen As New DataTable
        Dim strIn As String = "-999"

        Try
            For Each item In Me.ListBoxDiagrams.CheckedItems
                strIn += "," + item("diagram_id").ToString()
            Next
            FrmSimulator.DataSetContainer = Me.FFDataset
            Dim strSql As String = String.Format("SELECT t_object.name FROM t_object, t_diagramobjects WHERE t_object.object_id = t_diagramobjects.object_id AND t_diagramobjects.diagram_id IN({0}) ", strIn)
            aElementen = DLA2EAHelper.SQL2DataTable(strSql, Me.Repository)
            FrmSimulator.TableNames = aElementen
            FrmSimulator.Show()
            Me.Close()
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub

    Private Sub ButtonSelectAll_Click_1(sender As Object, e As EventArgs) Handles ButtonSelectAll.Click
        SelectDeSelect(True)
    End Sub

    Private Sub ButtonUnselectAll_Click_1(sender As Object, e As EventArgs) Handles ButtonUnselectAll.Click
        SelectDeSelect(True)
    End Sub

    Private Sub ButtonToggleAll_Click_1(sender As Object, e As EventArgs) Handles ButtonToggleAll.Click

        Dim i As Int16 = 0
        While i < ListBoxElements.Items.Count
            ListBoxElements.SetItemChecked(i, (Not ListBoxElements.GetItemChecked(i)))
            i += 1
        End While
    End Sub

    Private Sub ButtonInspector_Click(sender As Object, e As EventArgs) Handles ButtonInspector.Click
        Dim frmDI As New FrmDataSetInspector()
        frmDI.SetDataSet(Me.FFDataset.ContainerDataSet)
        frmDI.Show()

    End Sub
    Sub MakeConnectionString()
        Dim dcd As New Microsoft.Data.ConnectionUI.DataConnectionDialog()

        Microsoft.Data.ConnectionUI.DataSource.AddStandardDataSources(dcd)
        If Microsoft.Data.ConnectionUI.DataConnectionDialog.Show(dcd) = DialogResult.OK Then
            My.Settings.ConnectionString = dcd.ConnectionString
            My.Settings.ConnectionType = "SQL"
            My.Settings.Save()
            MsgBox(My.Settings.ConnectionString)
        End If
    End Sub
    Function MakeXMLFile() As String
        Dim strFileName As String = ""

        Me.FileDialog.Title = "Open XML file"
        Me.FileDialog.InitialDirectory = "C:\idea\"
        Me.FileDialog.Filter = "XML files (*.xml)|*.xml"
        Me.FileDialog.FilterIndex = 2
        Me.FileDialog.RestoreDirectory = True
        If Me.FileDialog.ShowDialog() = DialogResult.OK Then
            strFileName = Me.FileDialog.FileName
            My.Settings.ConnectionType = "XML"
        Else
            My.Settings.ConnectionType = ""
        End If
        My.Settings.XMLFile = strFileName
        My.Settings.Save()
        Return strFileName
    End Function
    Private Sub ButtonDatabaseConnection_Click(sender As Object, e As EventArgs) Handles ButtonDatabaseConnection.Click
        MakeConnectionString()
        Me.ButtonDatabaseConnection.Tag = Me.LabelDatabaseConnection.Text = My.Settings.ConnectionString
    End Sub

    Private Sub ButtonXMLFile_Click(sender As Object, e As EventArgs) Handles ButtonXMLFile.Click
        Me.LabelXMLFile.Text = MakeXMLFile()
    End Sub

    Private Sub FrmIDEAFormFactory_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class