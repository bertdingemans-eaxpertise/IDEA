Imports TEA.DLAFormfactory

''' <summary>
''' Container class for storing release manager steps for transferring XML
''' specifications from one environment to the other.
''' </summary>
Public Class ReleaseContainer
    Public ReleaseTable As DataTable
    Sub New()
        ReleaseTable = New DataTable("Release")
        ReleaseTable.Columns.Add(New DataColumn("PackageName", System.Type.GetType("System.String")))
        ReleaseTable.Columns.Add(New DataColumn("FolderName", System.Type.GetType("System.String")))
        ReleaseTable.Columns.Add(New DataColumn("RestoreBaseLine", System.Type.GetType("System.Boolean")))
        ReleaseTable.Columns.Add(New DataColumn("Version", System.Type.GetType("System.String")))
        ReleaseTable.Columns.Add(New DataColumn("PackageGUID", System.Type.GetType("System.String")))
        ReleaseTable.Columns.Add(New DataColumn("BaselineGUID", System.Type.GetType("System.String")))
    End Sub

    Public ReadOnly Property ReleaseItems() As DataRowCollection
        Get
            Return Me.ReleaseTable.Rows
        End Get

    End Property
    Function AddReleaseItem(PackageGUID As String, BaselineGuid As String, FolderName As String, PackageName As String, RestoreBaseline As Boolean, Version As String) As Boolean
        Try
            Dim oRow As DataRow

            oRow = Me.ReleaseTable.NewRow()
            oRow("PackageGUID") = PackageGUID
            oRow("BaselineGuid") = BaselineGuid
            oRow("FolderName") = FolderName
            oRow("PackageName") = PackageName
            oRow("RestoreBaseline") = RestoreBaseline
            oRow("Version") = Version
            Me.ReleaseTable.Rows.Add(oRow)
            Return True
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
            Return False
        End Try
    End Function

    Sub ClearContainer()
        Me.ReleaseTable.Rows.Clear()
    End Sub
End Class
