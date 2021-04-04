''' <summary>
''' Welcome screen of the IDEA AddOn
''' </summary>
Public Class WndWelcome
    Private _repository As EA.Repository
    Public Property Repository() As EA.Repository
        Get
            Return _repository
        End Get
        Set(ByVal value As EA.Repository)
            _repository = value
        End Set
    End Property
    Private Sub ButtonTest_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.Close()
    End Sub

    Private Sub ButtonArchiAid_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonArchiAid.Click
        Dim Frm As New FrmArchimAID()
        Frm.Repository = Me.Repository
        Frm.Show()
        Me.Close()
    End Sub

    Private Sub ButtonReleaseManager_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonReleaseManager.Click
        Dim oWnd As New frmReleaseManager()
        oWnd.Repository = Me.Repository
        oWnd.LoadPackage()
        oWnd.Show()
        Me.Close()
    End Sub

    Private Sub ButtonBizzDesign_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonExcelImporter.Click
        Dim oWnd As New FrmImportExcel()
        oWnd.SetRepository(Repository)
        oWnd.Show()
        Me.Close()
    End Sub

    Private Sub ButtonDeduplicate_Click(ByVal sender As Object, ByVal e As EventArgs)



    End Sub

    Private Sub ButtonImportNorm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonSimpleQuery.Click
        Dim oWnd As New FrmQueryExport()
        oWnd.SetRepository(Repository)
        oWnd.Show()
        Me.Close()
    End Sub

    Private Sub ButtonFrmDataVault_Click(sender As Object, e As EventArgs) Handles ButtonFrmDataVault.Click
        Dim oFrmDV As New FrmDataVault()
        oFrmDV.Repository = Repository
        oFrmDV.LoadTemplates()
        oFrmDV.Show()
        Me.Close()
    End Sub

    Private Sub ButtonSettings_Click(sender As Object, e As EventArgs) Handles ButtonSettings.Click
        Dim oWnd As New FrmSettings()
        oWnd.Repository = Me.Repository
        oWnd.Show()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub WndWelcome_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Label1.Text = Me.Label1.Text
        Me.TextBoxVersion.Text = "Version " + ProductVersion
    End Sub
End Class