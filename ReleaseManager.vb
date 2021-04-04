Imports TEA.DLAFormfactory
''' <summary>
''' Bring the data of a release from one environment to the other in a DTAP
''' implementation
''' </summary>
Public Class ReleaseManager
    Private _Messages As String = ""
    Private _Repository As EA.Repository
    Private _Project As EA.Project
    Private _Message2Output As Boolean = True
    Private _ReleaseContainer As ReleaseContainer
    Public Property ReleaseContainer() As ReleaseContainer
        Get
            Return _ReleaseContainer
        End Get
        Set(ByVal value As ReleaseContainer)
            _ReleaseContainer = value
        End Set
    End Property
    ''' <summary>
    ''' Reference to the repository class of the running EA instance
    ''' </summary>
    ''' <returns></returns>
    Public Property Repository() As EA.Repository
        Get
            Return _Repository
        End Get
        Set(ByVal value As EA.Repository)
            _Repository = value
            _Project = _Repository.GetProjectInterface()
            If Me._Message2Output = True Then
                Me.Repository.CreateOutputTab("Release manager")
            End If
        End Set
    End Property
    Public ReadOnly Property Messages As String
        Get
            Return _Messages
        End Get
    End Property
    Sub New()

    End Sub
    Public Sub CreateRelease()
        MakePackageBaseline()
        RestorePackageBaseline()
        ExportPackageXMI()
    End Sub

    Public Sub MakePackageBaseline()
        Dim Container As DataRow
        Try
            For Each Container In Me.ReleaseContainer.ReleaseItems
                If Container("PackageGUID").Length > 0 Then
                    If Me._Project.CreateBaseline(Container("PackageGUID"), Container("Version"), "") Then
                        Me.AddMessage("Baseline created for " + Container("PackageName"))
                    Else
                        Me.AddMessage("!!!Error in creating a baseline for " + Container("PackageName"))
                    End If
                End If
            Next
        Catch ex As Exception
            Me.AddMessage(ex.ToString())
        End Try

    End Sub
    Public Sub MakeBackupBaseline(ByVal guid As String, ByVal version As String)
        Try
            If Me._Project.CreateBaseline(guid, version, "") Then
                Me.AddMessage("Baseline created for backup ")
            Else
                Me.AddMessage("!!!Error in creating a baseline for  backup ")
            End If
        Catch ex As Exception
            Me.AddMessage(ex.ToString())
        End Try

    End Sub

    Public Sub AddMessage(ByVal strMessage As String)
        _Messages += strMessage + vbCrLf

        If Me._Message2Output = True Then
            Me.Repository.WriteOutput("Release manager", strMessage, 0)
        End If
    End Sub
    Public Sub RestorePackageBaseline()
        Dim Container As DataRow
        Try
            For Each Container In Me.ReleaseContainer.ReleaseItems
                If Container("RestoreBaseLine") = True And Container("BaseLineGUID").Length > 0 Then
                    Dim strMerge As String = "<Merge><MergeItem guid=""RestoreAll"" baselineOnly=""true"" modelOnly=""true"" moved=""true""  changed=""true"" fullRestore=""true"" /></Merge>"

                    Me._Project.DoBaselineMerge(Container("PackageGUID"), Container("BaseLineGUID"), strMerge, "")
                    Me.AddMessage("Baseline restored for " + Container("PackageName"))
                End If

            Next
        Catch ex As Exception
            Me.AddMessage(ex.ToString())
        End Try

    End Sub
    Public Sub ExportPackageXMI()
        Dim Container As DataRow
        Try
            For Each Container In Me.ReleaseContainer.ReleaseItems
                Me.AddMessage(Me._Project.ExportPackageXMI(Container("PackageGUID"), EA.EnumXMIType.xmiNative, 1, 3, True, True, Container("FolderName") + "\" & Container("PackageName") + ".xml"))
                Me.AddMessage("Package exported for " + Container("PackageName"))
            Next
        Catch ex As Exception
            Me.AddMessage(ex.ToString())
        End Try

    End Sub
    Public Sub ImportPackageXMI()
        Dim Container As DataRow
        Try
            For Each Container In Me.ReleaseContainer.ReleaseItems
                Me.AddMessage(Me._Project.ImportPackageXMI(Container("PackageGUID"), Container("FolderName") + "\" & Container("PackageName") + ".xml", 1, 0))
                Me.AddMessage("Package imported for " + Container("PackageName"))
            Next
        Catch ex As Exception
            Me.AddMessage(ex.ToString())
        End Try

    End Sub

    Public Sub Elements2Team()
        Dim objDiagram As EA.Diagram
        Dim objPackage As EA.Package
        Dim objElement As EA.Element
        Dim objDO As EA.DiagramObject
        Dim Container As DataRow
        Dim objHelper As New DataSet2Repository()

        Try
            objHelper.Repository = Me.Repository
            For Each Container In Me.ReleaseContainer.ReleaseItems
                objPackage = Me.Repository.GetPackageByGuid(Container("PackageGUID"))
                For Each objDiagram In objPackage.Diagrams
                    For Each objDO In objDiagram.DiagramObjects
                        objElement = Me.Repository.GetElementByID(objDO.ElementID)
                        'bewaar waar dit element origineel stond
                        objHelper.AddOrUpdateTaggedValue(objElement, "originalpackage", objElement.PackageID, False)
                        'verplaats het naar het juiste package
                        objElement.PackageID = objDiagram.PackageID
                        objElement.Update()
                    Next
                    Me.AddMessage(objDiagram.Name + " Elements collected to package")

                Next
                Me.Repository.RefreshModelView(objPackage.PackageID)
            Next

        Catch ex As Exception
            Me.AddMessage(ex.ToString())
        End Try

    End Sub
    Public Sub Elements2OriginalPackage()
        Dim objDiagram As EA.Diagram
        Dim objPackage As EA.Package
        Dim objElement As EA.Element
        Dim objDO As EA.DiagramObject
        Dim Container As DataRow

        Try
            For Each Container In Me.ReleaseContainer.ReleaseItems
                objPackage = Me.Repository.GetPackageByGuid(Container("PackageGUID"))
                For Each objDiagram In objPackage.Diagrams
                    For Each objDO In objDiagram.DiagramObjects
                        objElement = Me.Repository.GetElementByID(objDO.ElementID)
                        Dim objTV As EA.TaggedValue
                        For Each objTV In objElement.TaggedValues
                            If objTV.Name = "originalpackage" Then
                                objElement.PackageID = objTV.Value
                                objElement.Update()
                                Exit For
                            End If
                        Next

                    Next
                Next
                Me.Repository.RefreshModelView(0)
            Next
        Catch ex As Exception
            Me.AddMessage(ex.ToString())
        End Try

    End Sub
End Class
