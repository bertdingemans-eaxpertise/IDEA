Imports TEA.DLAFormfactory

''' <summary>
''' Screen for doing release management in a DTAP configured environment
''' </summary>
Public Class frmReleaseManager
    Private _Repository As EA.Repository
    Private _Project As EA.Project
    Private PackageContainer As ReleaseContainer
    Private oDef As New IDEADefinitions()

    Public Property Repository() As EA.Repository
        Get
            Return _Repository
        End Get
        Set(ByVal value As EA.Repository)
            _Repository = value
            _Project = _Repository.GetProjectInterface()
        End Set
    End Property
    Private Sub frmReleaseManager_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Me.TextBoxVersion.Text = System.DateTime.Now().ToString("yyyy-MM-dd")
        Me.TextBoxDirectory.Text = oDef.GetSettingValue("ReleaseDirectory")
        Me.PackageContainer = New ReleaseContainer()
        Me.DataGridViewRelease.DataSource = Me.PackageContainer.ReleaseTable
    End Sub

    Private Sub LoadBaseLines()
        Dim objDT As DataTable
        Dim strSql As String
        strSql = String.Format("SELECT DocID, Version FROM t_document WHERE DocType = 'BaseLine' AND ElementID ='{0}' ", Me.ComboBoxPackage.SelectedValue)
        objDT = DLA2EAHelper.SQL2DataTable(strSql, Me.Repository)
        If objDT.Rows.Count > 0 Then
            Me.ComboBoxBaseline.DataSource = objDT
            Me.ComboBoxBaseline.DisplayMember = "version"
            Me.ComboBoxBaseline.ValueMember = "DocID"
        End If

    End Sub

    Public Sub LoadPackage()
        Dim objDT As DataTable
        objDT = DLA2EAHelper.SQL2DataTable("SELECT ea_guid as packageguid, name FROM t_package ORDER BY 2 ", Me.Repository)
        Me.ComboBoxPackage.DataSource = objDT
        Me.ComboBoxPackage.DisplayMember = "name"
        Me.ComboBoxPackage.ValueMember = "packageguid"
    End Sub

    Private Sub ComboBoxPackage_SelectionChangeCommitted(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBoxPackage.SelectionChangeCommitted
        Me.LoadBaseLines()
    End Sub

    Private Sub ButtonAddPackage_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonAddPackage.Click
        Dim BaseLineGUID As String = ""
        Try
            Dim PackageName As String = ""
            PackageName = Me.ComboBoxPackage.Text
            If Not String.IsNullOrEmpty(Me.ComboBoxBaseline.SelectedValue) Then
                BaseLineGUID = Me.ComboBoxBaseline.SelectedValue
                PackageName += "-" + Me.ComboBoxBaseline.Text
            End If
            Me.PackageContainer.AddReleaseItem(Me.ComboBoxPackage.SelectedValue, BaseLineGUID, Me.TextBoxDirectory.Text, PackageName, Me.CheckBoxRestoreBaseline.Checked, Me.TextBoxVersion.Text)
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try

    End Sub

    Private Sub ButtonClearSelection_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonClearSelection.Click
        Me.PackageContainer.ClearContainer()
    End Sub

    Private Sub ButtonMakeRelease_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonMakeRelease.Click
        Dim RM As New ReleaseManager

        'Alleen administrators mogen dit doen
        Try
            If DLA2EAHelper.IsUserGroupMember(Me.Repository, "Administrators") Or Me.Repository.IsSecurityEnabled = False Then
                Me.Repository.EnableUIUpdates = False
                RM.Repository = Me.Repository
                RM.ReleaseContainer = Me.PackageContainer
                'maak eerst een backup van de verschillende objectmappen
                'RM.MakeBackupBaseline(Me.ComboBoxBDVRDV.SelectedValue, Me.TextBoxVersion.Text)
                'RM.MakeBackupBaseline(Me.ComboBoxFBDOM.SelectedValue, Me.TextBoxVersion.Text)
                'Kopieer de elementen naar de teammap
                RM.Elements2Team()
                Me.TextBoxResult.Text = RM.Messages
                RM.MakePackageBaseline()
                Me.TextBoxResult.Text = RM.Messages
                RM.RestorePackageBaseline()
                Me.TextBoxResult.Text = RM.Messages
                RM.ExportPackageXMI()
                Me.TextBoxResult.Text = RM.Messages
                RM.Elements2OriginalPackage()
                Me.TextBoxResult.Text = RM.Messages

                Me.Repository.EnableUIUpdates = True
            Else
                MsgBox("You are not authorized to start this function", MsgBoxStyle.OkOnly)
            End If
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
        Me.Close()

    End Sub
    Private Sub ButtonImportRelease_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonImportRelease.Click
        Dim RM As New ReleaseManager
        Try
            If DLA2EAHelper.IsUserGroupMember(Me.Repository, "Administrators") Or Me.Repository.IsSecurityEnabled = False Then
                Me.Repository.EnableUIUpdates = False
                RM.Repository = Me.Repository
                RM.ReleaseContainer = Me.PackageContainer
                RM.Elements2Team()
                Me.TextBoxResult.Text = RM.Messages
                RM.MakePackageBaseline()

                Me.TextBoxResult.Text += RM.Messages
                'RM.RestorePackageBaseline()
                'Me.TextBoxResult.Text = RM.Messages
                RM.ImportPackageXMI()
                RM.Elements2OriginalPackage()
                Me.TextBoxResult.Text = RM.Messages
                Me.Repository.EnableUIUpdates = True
            Else
                MsgBox("You are not authorized to start this function", MsgBoxStyle.OkOnly)
            End If
            Me.Close()
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)

        End Try

    End Sub
End Class