Imports TEA.DLAFormfactory
''' <summary>
''' Deduplication of an package and all the elements in the package with a number
''' of subscreens in the tabpage to define the merging of the related elements and
''' their features
''' </summary>
Public Class FrmPackageDeduplicator

    Private oRepository As EA.Repository
    Private oPackage As EA.Package
    Private sPackageList As String = ""
    Private oDef As New IDEADefinitions()

    Public Property Repository() As EA.Repository
        Get
            Return oRepository
        End Get
        Set(ByVal value As EA.Repository)
            oRepository = value
            Me.oPackage = Me.Repository.GetTreeSelectedPackage()
            Me.LabelPackage.Text = oPackage.Name & " " & oPackage.Version
            Me.LoadGrid()
        End Set
    End Property

    Function CreatePackageList(Pkg As EA.Package) As Boolean
        Try
            If Me.sPackageList.Length > 0 Then
                Me.sPackageList += ","
            End If
            Me.sPackageList += Pkg.PackageID.ToString()
            Dim oSubPkg As EA.Package
            For Each oSubPkg In Pkg.Packages
                CreatePackageList(oSubPkg)
            Next
            Return True
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
        Return False
    End Function
    Sub LoadGrid()
        Dim strSql As String = ""
        Try
            If Me.CheckBoxRecursion.Checked = True Then
                Me.CreatePackageList(Me.oPackage)
            Else
                Me.sPackageList = oPackage.PackageID.ToString()
            End If
            'load elements grid
            strSql = oDef.GetSettingValue("DeduplicateElementsPackage")
            DataGridViewElements.DataSource = DLA2EAHelper.SQL2DataTable(strSql.Replace("#package_id#", Me.sPackageList), Me.Repository)
            If Me.CheckBoxDeduplicateConnector.Checked Then
                Me.ResultSplitContainer.Panel2Collapsed = False
                'load connectors grid
                strSql = oDef.GetSettingValue("DeduplicateConnectorsPackage")
                DLA2EAHelper.Text2ClipBoard(strSql.Replace("#package_id#", Me.sPackageList))
                DataGridViewConnectors.DataSource = DLA2EAHelper.SQL2DataTable(strSql.Replace("#package_id#", Me.sPackageList), Me.Repository)
            Else
                Me.ResultSplitContainer.Panel2Collapsed = True
                DataGridViewConnectors.DataSource = New DataTable()
            End If
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub
    Sub DeDuplicate()
        Dim oDuplPackage As EA.Package = Nothing
        Dim strDuplPackageId As String = ""
        Try
            Me.AddMessage("Deduplicator started")
            If Me.CheckBoxDuplicateFolder.Checked Then
                oDuplPackage = Me.oPackage.Packages.AddNew("Duplicates", "")
                oDuplPackage.Update()
                Me.oPackage.Update()
                strDuplPackageId = oDuplPackage.PackageID
            End If
            Dim objDeDup As New DeDuplicator()
            'Instellen van de parameters voor de routine obv de checkboxen in het scherm
            objDeDup.Repository = Me.Repository
            objDeDup.AddModuleViaCheckBox(Me.CheckBoxRecursion)
            objDeDup.AddModuleViaCheckBox(Me.CheckBoxNotes)
            objDeDup.AddModuleViaCheckBox(Me.CheckBoxTaggedValues)
            objDeDup.AddModuleViaCheckBox(Me.CheckBoxAttributes)
            objDeDup.AddModuleViaCheckBox(Me.CheckBoxLinkedFiles)
            objDeDup.AddModuleViaCheckBox(Me.CheckBoxMethod)
            objDeDup.AddModuleViaCheckBox(Me.CheckBoxScenario)
            objDeDup.AddModuleViaCheckBox(Me.CheckBoxRequirements)
            objDeDup.AddModuleViaCheckBox(Me.CheckBoxConnectors)
            objDeDup.AddModuleViaCheckBox(Me.CheckBoxDuplicateFolder)
            objDeDup.AddModuleViaCheckBox(Me.CheckBoxRename)
            objDeDup.AddModuleViaCheckBox(Me.CheckBoxCreateTrace)
            objDeDup.AddModuleViaCheckBox(Me.CheckBoxDeduplicateConnector)
            Me.AddMessage("Deduplicator configured")

            'Doe de deduplicatie
            Me.AddMessage("Deduplicating elements started")
            objDeDup.DeDuplicatePackage(oPackage, strDuplPackageId, Me.ProgressBarDedupl, Me.DataGridViewElements.DataSource)
            Me.AddMessage("Deduplicating elements ready")
            If Me.CheckBoxDeduplicateConnector.Checked Then
                Me.AddMessage("Deduplicating connectors started")
                objDeDup.DeDuplicateConnectors(oPackage, strDuplPackageId, Me.ProgressBarDedupl, Me.DataGridViewConnectors.DataSource)
                Me.AddMessage("Deduplicating connectors ready")
            End If
            If Me.CheckBoxDuplicateFolder.Checked Then
                oDuplPackage.Elements.Refresh()
                oDuplPackage.Update()
                Me.AddMessage("Package tree refreshed")
                Me.AddMessage("Duplicate elements available in the package named Duplicates")
            End If
            'Immediately close the window when the checkbox is checked
            If Me.CheckBoxCloseWindow.Checked Then
                Me.Close()
            Else
                Me.oRepository.RefreshModelView(0)
            End If
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub
    Private Sub FrmPackageDeduplicator_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Me.CheckBoxSuppressValidationWarning.Checked = My.Settings.SuppresWarningDialog
    End Sub

    Private Sub AddMessage(strMessage As String)
        Me.TextBoxResult.Text += vbCrLf + strMessage
    End Sub

    Private Sub ButtonDeDuplicate_Click(sender As Object, e As EventArgs) Handles ButtonDeDuplicate.Click
        If Me.Repository.IsSecurityEnabled = False Or DLA2EAHelper.IsUserGroupMember(Me.Repository, "Administrators") Then
            Me.Repository.CreateOutputTab("IDEA")
            Me.Repository.EnsureOutputVisible("IDEA")
            Me.TextBoxResult.Text = ""
            Me.TabControlDeduplicator.SelectTab("TabPageResultText")
            'Me.oRepository.EnableUIUpdates = False
            DeDuplicate()
            ' Me.oRepository.EnableUIUpdates = True
        Else
            MsgBox("You are not authorized to start this function", MsgBoxStyle.OkOnly)
        End If
    End Sub

    Private Sub ButtonDuplicateReport_Click_1(sender As Object, e As EventArgs) Handles ButtonDuplicateReport.Click
        Try
            'When validation will be send to a report create a pdf report and run the report based on the guid of the selected package, this is recursive by the nature of the report
            Dim oProject As EA.Project
            oProject = Repository.GetProjectInterface()
            SaveFileDialogReport.DefaultExt = "*.pdf"
            SaveFileDialogReport.Filter = "Pdf Files|*.pdf"
            SaveFileDialogReport.CreatePrompt = True
            If SaveFileDialogReport.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                oProject.RunReport(Me.oPackage.PackageGUID, "DuplicateReport", SaveFileDialogReport.FileName)
            End If
        Catch ex As Exception
            Repository.WriteOutput("IDEA", ex.Message, 0)
        End Try
    End Sub

    Private Sub ButtonSaveValidation_Click_1(sender As Object, e As EventArgs) Handles ButtonSaveValidation.Click
        My.Settings.SuppresWarningDialog = Me.CheckBoxSuppressValidationWarning.Checked
        My.Settings.Save()
    End Sub
    Private Sub ButtonReloadGrid_Click(sender As Object, e As EventArgs) Handles ButtonReloadGrid.Click
        Me.LoadGrid()
    End Sub

    Private Sub ButtonViewInBrowser_Click(sender As Object, e As EventArgs) Handles ButtonOriginalnBrowser.Click
        Dim oDVRTX As System.Windows.Forms.DataGridViewTextBoxCell
        Try
            If Not IsNothing(Me.DataGridViewElements.CurrentRow.Cells("origid")) Then
                oDVRTX = Me.DataGridViewElements.CurrentRow.Cells("origid")
                Repository.ShowInProjectView(Me.Repository.GetElementByID(oDVRTX.Value))
            End If

        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub

    Private Sub ButtonDuplicateInBrowser_Click(sender As Object, e As EventArgs) Handles ButtonDuplicateInBrowser.Click
        Dim oDVRTX As System.Windows.Forms.DataGridViewTextBoxCell
        Try
            If Not IsNothing(Me.DataGridViewElements.CurrentRow.Cells("duplid")) Then
                oDVRTX = Me.DataGridViewElements.CurrentRow.Cells("duplid")
                Repository.ShowInProjectView(Me.Repository.GetElementByID(oDVRTX.Value))
            End If
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub
End Class