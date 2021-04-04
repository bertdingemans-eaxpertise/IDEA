Imports TEA.DLAFormfactory

Public Class FrmDataVault
    Private _Repository As EA.Repository
    Private oDef As IDEADefinitions

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Property Repository() As EA.Repository
        Get
            Return _Repository
        End Get
        Set(ByVal value As EA.Repository)
            _Repository = value
        End Set
    End Property
    Private Sub FrmDataVault_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.oDef = New IDEADefinitions()
        Me.oDef.LoadFromSettings()
        Me.TextBoxTemplateReplaceValue.Text = oDef.GetSettingValue("TemplateReplaceValue")
        Me.ListBoxConcepts.DataSource = LoadSearchStatement()
        Me.ListBoxConcepts.ValueMember = "sql"
        Me.ListBoxConcepts.DisplayMember = "name"
    End Sub


    Function LoadSearchStatement() As DataTable
        Dim oDT As New DataTable("Searches")
        oDT.Columns.Add(New DataColumn("Name"))
        oDT.Columns.Add(New DataColumn("SQL"))
        Dim oRow As DataRow

        oRow = oDT.NewRow()
        oRow("Name") = "Data Entity"
        oRow("SQL") = "SELECT name, author, version, status, stereotype, object_id FROM t_object WHERE (stereotype IS NULL OR stereotype = 'table') AND object_type='Class' AND name LIKE '<wc><search>%' ORDER BY name "
        oDT.Rows.Add(oRow)


        oRow = oDT.NewRow()
        oRow("Name") = "LDM Entity"
        oRow("SQL") = "SELECT name, author, version, status, object_id FROM t_object WHERE stereotype IS NULL AND object_type='Class' AND name LIKE '<wc><search>%' ORDER BY name "
        oDT.Rows.Add(oRow)

        oRow = oDT.NewRow()
        oRow("Name") = "Table"
        oRow("SQL") = "SELECT name, author, version, status, object_id FROM t_object WHERE stereotype = 'table' AND name LIKE '<wc><search>%' ORDER BY name "
        oDT.Rows.Add(oRow)

        oRow = oDT.NewRow()
        oRow("Name") = "Hub"
        oRow("SQL") = "SELECT name, author, version, status, object_id FROM t_object WHERE name LIKE '<wc><search>%' AND stereotype = 'table' AND name LIKE '%_H%'  ORDER BY name "
        oDT.Rows.Add(oRow)

        oRow = oDT.NewRow()
        oRow("Name") = "Link"
        oRow("SQL") = "SELECT name, author, version, status, object_id FROM t_object WHERE name LIKE '<wc><search>%' AND stereotype = 'table' AND name LIKE '%_L%'  ORDER BY name "
        oDT.Rows.Add(oRow)

        oRow = oDT.NewRow()
        oRow("Name") = "Sat"
        oRow("SQL") = "SELECT name, author, version, status, object_id FROM t_object WHERE name LIKE '<wc><search>%' AND stereotype = 'table' AND name LIKE '%_S%'  ORDER BY name "
        oDT.Rows.Add(oRow)

        oRow = oDT.NewRow()
        oRow("Name") = "BDV Table"
        oRow("SQL") = "SELECT name, author, version, status, object_id FROM t_object WHERE stereotype = 'table' AND name LIKE '%_B%'  AND name LIKE '<wc><search>%' ORDER BY name "
        oDT.Rows.Add(oRow)


        oRow = oDT.NewRow()
        oRow("Name") = "Business Hub"
        oRow("SQL") = "SELECT name, author, version, status, object_id FROM t_object WHERE stereotype = 'table' AND name LIKE '%_BH'  AND name LIKE '<wc><search>%' ORDER BY name "
        oDT.Rows.Add(oRow)

        oRow = oDT.NewRow()
        oRow("Name") = "Business Link"
        oRow("SQL") = "SELECT name, author, version, status, object_id FROM t_object WHERE stereotype = 'table' AND name LIKE '%_BL'  AND name LIKE '<wc><search>%' ORDER BY name "
        oDT.Rows.Add(oRow)

        oRow = oDT.NewRow()
        oRow("Name") = "Business Sat"
        oRow("SQL") = "SELECT name, author, version, status, object_id FROM t_object WHERE stereotype = 'table' AND name LIKE '%_BS'  AND name LIKE '<wc><search>%' ORDER BY name "
        oDT.Rows.Add(oRow)
        Return oDT
    End Function
    Public Sub LoadTemplates()
        Dim objDT As DataTable
        Try
            objDT = DLA2EAHelper.SQL2DataTable("select t_object.object_id, t_object.name from t_object, t_objectproperties where t_object.Object_ID = t_objectproperties.Object_ID and t_objectproperties.Property = 'IDEA_isTemplate' and t_objectproperties.value = 'True'", Me.Repository)
            If Not IsNothing(objDT) Then
                Me.ListBoxTemplates.DataSource = objDT
                Me.ListBoxTemplates.ValueMember = "object_id"
                Me.ListBoxTemplates.DisplayMember = "name"
                ' Me.ListBoxTemplates.SelectedValue = objDT.Rows(0).Item("object_id")
            End If
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub
    Private Sub ButtonAdd2Diagram_Click(sender As Object, e As EventArgs) Handles ButtonAdd2Diagram.Click
        Dim objDiagram As EA.Diagram
        Dim intTop As Integer = -100
        Dim intLeft As Integer = 100
        Dim Generator As System.Random = New System.Random()
        Try
            objDiagram = Me.Repository.GetCurrentDiagram()
            If Me.TextBoxName.Text.Length = 0 Then
                MsgBox("Enter a name before generating a new element")
                Return
            End If
            If Me.ListBoxTemplates.SelectedIndex < 0 Then
                MsgBox("Select a template before generating a new element")
                Return
            End If
            intTop = Generator.Next(-300, 0)
            intLeft = Generator.Next(0, 300)
            Dim objElement As EA.Element
            Dim objDON As EA.DiagramObject

            objElement = Me.Repository.GetElementByID(Me.ListBoxTemplates.SelectedValue).Clone()
            Me.Repository.Execute(String.Format("DELETE FROM t_seclocks WHERE EntityID = '{0}' ", objElement.ElementGUID))
            Me.Repository.Execute(String.Format("UPDATE t_object SET package_id = {1}, name = '{2}', style='{3}' WHERE object_id = {0} ", objElement.ElementID, objDiagram.PackageID, objElement.Name.Replace(Me.TextBoxTemplateReplaceValue.Text, Me.TextBoxName.Text), objElement.StyleEx.Replace("Locked=true;", "")))
            Dim objAttribute As EA.Attribute
            For Each objAttribute In objElement.Attributes
                objAttribute.Name = objAttribute.Name.Replace(Me.TextBoxTemplateReplaceValue.Text, Me.TextBoxName.Text)
                objAttribute.Update()
            Next
            Dim objOperation As EA.Method
            For Each objOperation In objElement.Methods
                objOperation.Name = objOperation.Name.Replace(Me.TextBoxTemplateReplaceValue.Text, Me.TextBoxName.Text)
                objOperation.Update()
            Next
            Me.Repository.Execute(String.Format("DELETE FROM t_objectproperties WHERE object_id = {0} AND property LIKE 'IDEA{1}' ", objElement.ElementID, IIf(Me.Repository.RepositoryType = "JET", "*", "%")))
            objDON = objDiagram.DiagramObjects.AddNew("", "")
            objDON.ElementID = objElement.ElementID
            objDON.top = intTop
            objDON.bottom = intTop - 50
            objDON.left = intLeft
            objDON.right = intLeft + 120
            objDON.ShowNotes = False
            objDON.Update()
            objDiagram.Update()
            Repository.EnableUIUpdates = False
            Repository.RefreshOpenDiagrams(True)
            Repository.RefreshModelView(0)
            Repository.EnableUIUpdates = True
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub
    Private Sub ButtonViewBrowser_Click(sender As Object, e As EventArgs) Handles ButtonViewBrowser.Click
        If IsNothing(Me.ListBoxTemplates.SelectedValue) Then
            MsgBox("No item selected, nothing to view for")
        Else
            Me.Repository.ShowInProjectView(Me.Repository.GetElementByID(Me.ListBoxTemplates.SelectedValue))

        End If
    End Sub

    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click
        DoSearch
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim oDVRTX As System.Windows.Forms.DataGridViewTextBoxCell
        Try
            Me.Repository.EnableUIUpdates = False
            oDVRTX = Me.GridviewElements.CurrentRow.Cells("object_id")
            Repository.ShowInProjectView(Me.Repository.GetElementByID(oDVRTX.Value))
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub

    Private Sub ButtonAddElement_Click(sender As Object, e As EventArgs) Handles ButtonAddElement.Click
        Dim oDVRTX As System.Windows.Forms.DataGridViewTextBoxCell
        Dim objDiagram As EA.Diagram
        Dim intTop As Integer = -100
        Dim intLeft As Integer = 100
        Dim Generator As System.Random = New System.Random()
        Try
            objDiagram = Me.Repository.GetCurrentDiagram()
            Me.Repository.EnableUIUpdates = False
            For Each oDVR In Me.GridviewElements.SelectedRows
                intTop = Generator.Next(-200, 0)
                intLeft = Generator.Next(0, 200)

                oDVRTX = oDVR.Cells("object_id")
                Dim objDON As EA.DiagramObject
                Repository.WriteOutput("IDEA", oDVRTX.Value, 0)
                objDON = objDiagram.DiagramObjects.AddNew("", "")
                objDON.ElementID = oDVRTX.Value
                objDON.top = intTop
                objDON.bottom = intTop - 50
                objDON.left = intLeft
                objDON.right = intLeft + 120
                objDON.ShowNotes = False
                objDON.Update()
                objDiagram.Update()
                intLeft += 20
                intTop -= 20
            Next
            Repository.SaveDiagram(objDiagram.DiagramID)
            Me.Repository.EnableUIUpdates = True
            Repository.ReloadDiagram(objDiagram.DiagramID)

        Catch ex As Exception
            Repository.WriteOutput("IDEA", ex.Message, 0)
        End Try
    End Sub
    Private Sub DoSearch()
        Dim strSql As String
        If Me.TextBoxSearch.Text.Length > 0 Then
            strSql = Me.ListBoxConcepts.SelectedValue
            strSql = strSql.Replace("<search>", Me.TextBoxSearch.Text).Replace("<wc>", IIf(CheckBoxWildcard.Checked, "%", ""))
            '            MsgBox(strSql)
            Me.GridviewElements.DataSource = DLA2EAHelper.SQL2DataTable(strSql, Repository)
        End If
    End Sub
    Private Sub TextBoxSearch_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSearch.TextChanged
        DoSearch()
    End Sub
End Class