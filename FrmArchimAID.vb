Imports TEA.DLAFormfactory

''' <summary>
''' Form for ArchiMate modeling that combines searching and a toolbox to prevent
''' creating duplicates See also the <a href="$diagram://{0B41C36A-B116-4ff7-BABF-
''' 3EF374CBBB16}"><font color="#0000ff"><u>screen description</u></font></a>
''' </summary>
Public Class FrmArchimAID
    Private oRepository As EA.Repository
    Public Property Repository() As EA.Repository
        Get
            Return oRepository
        End Get
        Set(ByVal value As EA.Repository)
            oRepository = value

        End Set
    End Property
    ''' <summary>
    ''' Clear the combobox etc when you select a new stereotype
    ''' </summary>
    Private Sub ClearListBoxConcepts()
        Me.ListBoxConcepts.Items.Clear()
        Me.ComboBoxConcept.Items.Clear()
        Me.GridviewElements.DataSource = Nothing
        Me.ButtonAddElement.Enabled = False
        Me.ButtonCreateOnDiagram.Enabled = False
        Me.ButtonViewBrowser.Enabled = False
    End Sub
    ''' <summary>
    ''' Add an array of implementation stereotypes
    ''' </summary>
    Private Sub AddImplementation()
        ListBoxConcepts.Items.AddRange({"Deliverable", "Gap", "ImplementationEvent", "Plateau", "WorkPackage"})
    End Sub
    Sub AddMotivation()
        ListBoxConcepts.Items.AddRange({"Assessment", "Driver", "Goal", "Meaning", "Outcome", "Stakeholder", "Requirement", "Principle", "Constraint", "Value", "Resource", "Capability", "Course Of Action"})

    End Sub
    Private Sub ButtonImplementation_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonImplementation.Click
        Me.ClearListBoxConcepts()
        Me.AddImplementation()
        Me.TabArchiMAID.SelectTab(1)
    End Sub
    Private Sub ButtonMotivation_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonMotivation.Click
        Me.ClearListBoxConcepts()
        Me.AddMotivation()
        Me.TabArchiMAID.SelectTab(1)
    End Sub
    ''' <summary>
    ''' Add an array of business structure stereotypes
    ''' </summary>
    Sub AddBusinessStructure()
        ListBoxConcepts.Items.AddRange({"BusinessActor", "BusinessRole", "BusinessCollaboration", "BusinessInterface"})
    End Sub
    ''' <summary>
    ''' Add an array of business behaviour stereotypes
    ''' </summary>
    Sub AddBusinessBehaviour()
        ListBoxConcepts.Items.AddRange({"BusinessProcess", "BusinessFunction", "BusinessInteraction", "BusinessService", "BusinessEvent"})
    End Sub
    ''' <summary>
    ''' Add an array of business passive stereotypes
    ''' </summary>
    Sub AddBusinessPassive()
        ListBoxConcepts.Items.AddRange({"BusinessObject", "Contract", "Representation", "Product"})
    End Sub
    ''' <summary>
    ''' Add an array of application structure stereotypes
    ''' </summary>
    Sub AddApplicationStructure()
        ListBoxConcepts.Items.AddRange({"ApplicationComponent", "ApplicationCollaboration", "ApplicationInterface"})
    End Sub
    ''' <summary>
    ''' Add an array of application behaviour stereotypes
    ''' </summary>

    Sub AddApplicationBehaviour()
        ListBoxConcepts.Items.AddRange({"ApplicationProcess", "ApplicationFunction", "ApplicationInteraction", "ApplicationService", "ApplicationEvent"})
    End Sub
    ''' <summary>
    ''' Add an array of application passive stereotypes
    ''' </summary>
    Sub AddApplicationPassive()
        ListBoxConcepts.Items.AddRange({"DataObject"})
    End Sub
    ''' <summary>
    ''' Add an array of technology structure stereotypes
    ''' </summary>
    Sub AddTechnologyStructure()
        ListBoxConcepts.Items.AddRange({"Node", "Device", "SystemSoftware", "TechnologyCollaboration", "TechnologyInterface", "Path", "CommunicationNetwork", "Facility", "Equipment", "DistributionNetwork", "Material", "Location"})
    End Sub
    ''' <summary>
    ''' Add an array of technology behaviour stereotypes
    ''' </summary>

    Sub AddTechnologyBehaviour()
        ListBoxConcepts.Items.AddRange({"TechnologyProcess", "TechnologyFunction", "TechnologyInteraction", "TechnologyService", "TechnologyEvent"})
    End Sub
    ''' <summary>
    ''' Add an array of technology passive stereotypes
    ''' </summary>
    Sub AddTechnologyPassive()
        ListBoxConcepts.Items.AddRange({"Artifact", "TechnologyObject"})
    End Sub
    Private Sub ButtonBusinessStructure_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonBusinessStructure.Click
        Me.ClearListBoxConcepts()
        Me.AddBusinessStructure()
        Me.TabArchiMAID.SelectTab(1)
    End Sub
    Private Sub ButtonBusinessBehaviour_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonBusinessBehaviour.Click
        Me.ClearListBoxConcepts()
        Me.AddBusinessBehaviour()
        Me.TabArchiMAID.SelectTab(1)
    End Sub
    Private Sub ButtonBusinessPassive_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonBusinessPassive.Click
        Me.ClearListBoxConcepts()
        Me.AddBusinessPassive()
        Me.TabArchiMAID.SelectTab(1)
    End Sub
    Private Sub ButtonBusinessLayer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonBusinessLayer.Click
        Me.ClearListBoxConcepts()
        Me.AddBusinessStructure()
        Me.AddBusinessBehaviour()
        Me.AddBusinessPassive()
        Me.TabArchiMAID.SelectTab(1)
    End Sub
    Private Sub ButtonApplicationStructure_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonApplicationStructure.Click
        Me.ClearListBoxConcepts()
        Me.AddApplicationStructure()
        Me.TabArchiMAID.SelectTab(1)
    End Sub
    Private Sub ButtonApplicationBehaviour_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonApplicationBehaviour.Click
        Me.ClearListBoxConcepts()
        Me.AddApplicationBehaviour()
        Me.TabArchiMAID.SelectTab(1)
        Me.ListBoxConcepts.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
    End Sub
    Private Sub ButtonApplicationPassive_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonApplicationPassive.Click
        Me.ClearListBoxConcepts()
        Me.AddApplicationPassive()
        Me.TabArchiMAID.SelectTab(1)
    End Sub
    Private Sub ButtonApplication_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonApplication.Click
        Me.ClearListBoxConcepts()
        Me.AddApplicationStructure()
        Me.AddApplicationBehaviour()
        Me.AddApplicationPassive()
        Me.TabArchiMAID.SelectTab(1)
    End Sub

    Private Sub ButtonSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonSearch.Click
        DoSearch
    End Sub
    ''' <summary>
    ''' Search for the selected elements based on the keyword and the selected archimate concepts
    ''' </summary>
    Private Sub DoSearch()
        Dim strSql As String
        'Repository.CreateOutputTab("IDEA")
        strSql = "Select t_object.name as object_name, t_object.alias, t_package.name as package, t_object.stereotype, t_object.status, t_object.author, t_object.object_id " +
            "FROM t_object, t_package " +
            "WHERE t_object.package_id = t_package.package_id " +
            "AND t_object.stereotype IN(#stereotypes#) "
        If Me.TextBoxSearch.Text.Length > 0 Then
            strSql += "AND ( t_object.name Like '%#search#%' OR  t_object.alias Like '%#search#%' ) "
        End If
        strSql += "ORDER BY t_object.stereotype, t_object.name "
        If Me.Repository.RepositoryType.ToUpper() = "JET" Then
            strSql = strSql.Replace("%", "*")
        End If
        Dim strStereo As String = "'-999'"
        Me.ComboBoxConcept.Items.Clear()
        Me.ButtonCreateOnDiagram.Enabled = True
        LabelConcept.Text = ""
        For Each iTeller In ListBoxConcepts.SelectedIndices
            strStereo += ", 'ArchiMate_" + ListBoxConcepts.Items(iTeller).ToString() + "'"
            LabelConcept.Text += ListBoxConcepts.Items(iTeller).ToString() + " "
            Me.ComboBoxConcept.Items.Add(ListBoxConcepts.Items(iTeller).ToString())
        Next
        strSql = strSql.Replace("#stereotypes#", strStereo).Replace("#search#", Me.TextBoxSearch.Text)
        Repository.WriteOutput("IDEA", strSql, 0)

        Dim oDataTable As DataTable
        Me.GridviewElements.DataSource = Nothing
        oDataTable = DLA2EAHelper.EAString2DataTable(Repository.SQLQuery(strSql))
        Me.GridviewElements.DataSource = oDataTable
        Me.ButtonAddElement.Enabled = True
        Me.ButtonViewBrowser.Enabled = True
    End Sub
    Private Sub ButtonAddElement_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonAddElement.Click
        Dim oDVRTX As System.Windows.Forms.DataGridViewTextBoxCell
        Dim objDiagram As EA.Diagram
        Dim intTop As Integer = -100
        Dim intLeft As Integer = 100
        Dim Generator As System.Random = New System.Random()
        Try
            objDiagram = Me.oRepository.GetCurrentDiagram()
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

    Private Sub ButtonCreateOnDiagram_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonCreateOnDiagram.Click
        Dim objDiagram As EA.Diagram
        Dim objPackage As EA.Package

        Try
            If Me.ComboBoxConcept.Text <> "" And Not IsNothing(Me.oRepository.GetCurrentDiagram()) Then
                objDiagram = Me.oRepository.GetCurrentDiagram()
                objPackage = Me.oRepository.GetPackageByID(objDiagram.PackageID)
                Dim objElement As EA.Element
                objElement = objPackage.Elements.AddNew(Me.TextBoxName.Text, "ArchiMate_" + ComboBoxConcept.Text)
                objElement.Notes = Me.TextBoxNotes.Text
                objElement.Update()
                objPackage.Update()
                Dim objDON As EA.DiagramObject
                objDON = objDiagram.DiagramObjects.AddNew("", "")
                objDON.ElementID = objElement.ElementID
                objDON.top = -200
                objDON.bottom = -200 - 50
                objDON.left = 200
                objDON.right = 200 + 120
                objDON.ShowNotes = False
                objDON.Update()
                objDiagram.Update()
                Repository.ReloadDiagram(objDiagram.DiagramID)
            Else
                MsgBox("Please select a concept and have a diagram open", MsgBoxStyle.OkOnly)
            End If
        Catch ex As Exception
            Repository.WriteOutput("IDEA", ex.Message, 0)
        End Try
    End Sub
    Private Sub ButtonTechnologyPassive_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonTechnologyPassive.Click
        Me.ClearListBoxConcepts()
        Me.AddTechnologyPassive()
        Me.TabArchiMAID.SelectTab(1)
    End Sub
    Private Sub ButtonTechnologyBehaviour_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonTechnologyBehaviour.Click
        Me.ClearListBoxConcepts()
        Me.AddTechnologyBehaviour()
        Me.TabArchiMAID.SelectTab(1)

    End Sub
    Private Sub ButtonTechnologyStructure_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonTechnologyStructure.Click
        Me.ClearListBoxConcepts()
        Me.AddTechnologyStructure()
        Me.TabArchiMAID.SelectTab(1)

    End Sub
    Private Sub ButtonTechnology_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonTechnology.Click
        Me.ClearListBoxConcepts()
        Me.AddTechnologyStructure()
        Me.AddTechnologyPassive()
        Me.AddTechnologyBehaviour()
        Me.TabArchiMAID.SelectTab(1)
    End Sub

    Private Sub ButtonViewBrowser_Click(sender As Object, e As EventArgs) Handles ButtonViewBrowser.Click
        Dim oDVRTX As System.Windows.Forms.DataGridViewTextBoxCell
        Try

            Me.Repository.EnableUIUpdates = False
            For Each oDVR In Me.GridviewElements.SelectedRows
                oDVRTX = oDVR.Cells("object_id")
                Repository.ShowInProjectView(Me.Repository.GetElementByID(oDVRTX.Value))
            Next
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub
    Private Sub TextBoxSearch_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSearch.TextChanged
        DoSearch()
    End Sub
End Class