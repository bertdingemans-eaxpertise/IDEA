Imports System.ComponentModel
Imports System.IO
Imports System.Windows.Forms
Imports TEA.DLAFormfactory

Public Class FrmSettings
    Private _Repository As EA.Repository

    Public Property Repository() As EA.Repository
        Get
            Return _Repository
        End Get
        Set(ByVal value As EA.Repository)
            _Repository = value
        End Set
    End Property
    Private Sub FrmSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim oDefinitions As New IDEADefinitions()
        Me.DataGridViewSettings.DataSource = oDefinitions.GetTable("SETTINGS")
        DataGridViewSettings.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        DataGridViewSettings.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells

        Me.DataGridViewStatement.DataSource = oDefinitions.GetTable("SQL-STATEMENT")
        DataGridViewStatement.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        DataGridViewStatement.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
        LoadUserSettings()
    End Sub

    Private Sub LoadUserSettings()
        Me.CheckBoxAssistant.Checked = My.Settings.Assistant
        Me.CheckBoxArchimAid.Checked = My.Settings.ArchimAid
        Me.CheckBoxDatAid.Checked = My.Settings.DatAid
        Me.CheckBoxDeduplicator.Checked = My.Settings.Deduplicator
        Me.CheckBoxFormFactory.Checked = My.Settings.FormFactory

        Me.CheckBoxPackageHelper.Checked = My.Settings.PackageHelper
        Me.CheckBoxDiagramHelper.Checked = My.Settings.DiagramHelper
        Me.CheckBoxClassHelper.Checked = My.Settings.ClassHelper
        Me.CheckBoxTableHelper.Checked = My.Settings.TableHelper
        Me.CheckBoxArchiMateHelper.Checked = My.Settings.ArchiMateHelper
        Me.CheckBoxXSDHelper.Checked = My.Settings.XSDHelper
        Me.CheckBoxShowAidOnDiagramOpen.Checked = My.Settings.ShowAidOnDiagramOpen
    End Sub
    Private Sub SaveUserSettings()
        My.Settings.Assistant = Me.CheckBoxAssistant.Checked
        My.Settings.ArchimAid = Me.CheckBoxArchimAid.Checked
        My.Settings.DatAid = Me.CheckBoxDatAid.Checked
        My.Settings.Deduplicator = Me.CheckBoxDeduplicator.Checked
        My.Settings.FormFactory = Me.CheckBoxFormFactory.Checked

        My.Settings.PackageHelper = Me.CheckBoxPackageHelper.Checked
        My.Settings.DiagramHelper = Me.CheckBoxDiagramHelper.Checked
        My.Settings.ClassHelper = Me.CheckBoxClassHelper.Checked
        My.Settings.TableHelper = Me.CheckBoxTableHelper.Checked
        My.Settings.ArchiMateHelper = Me.CheckBoxArchiMateHelper.Checked
        My.Settings.XSDHelper = Me.CheckBoxXSDHelper.Checked
        My.Settings.ShowAidOnDiagramOpen = Me.CheckBoxShowAidOnDiagramOpen.Checked
        My.Settings.Save()

    End Sub
    Private Sub FrmSettings_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Dim oElement As EA.Element
        Try
            If MsgBox("Save settings modifications?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim oDT As DataTable
                oDT = DLA2EAHelper.SQL2DataTable("SELECT object_id FROM t_object WHERE object_type = 'Artifact' AND name = 'IDEASettings'", Me.Repository)

                Dim oDefinitions As DataTable
                oDefinitions = Me.DataGridViewSettings.DataSource
                Dim stream As New StringWriter()
                oDefinitions.WriteXml(stream, True)

                Dim oStatements As DataTable
                oStatements = Me.DataGridViewStatement.DataSource
                Dim streamstatement As New StringWriter()
                oStatements.WriteXml(streamstatement, True)

                If oDT.Rows.Count > 0 Then
                    Dim oDR As DataRow
                    oDR = oDT.Rows(0)
                    oElement = Me.Repository.GetElementByID(oDR("object_id"))
                    Dim objDS As New DataSet2Repository()
                    objDS.AddOrUpdateTaggedValue(oElement, "Settings", stream.ToString(), True)
                    objDS.AddOrUpdateTaggedValue(oElement, "SQL-Statement", streamstatement.ToString(), True)
                    oElement.Update()
                Else
                    My.Settings.SettingsTable = stream.ToString()
                    My.Settings.StatementsTable = streamstatement.ToString()
                    My.Settings.Save()
                End If
                SaveUserSettings()
            End If
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub
End Class