Namespace DLAFormfactory

    Public Class PDFCreator
        Private _Repository As EA.Repository
        Protected docGenerator As EA.DocumentGenerator
        Dim oDef As New IDEADefinitions()

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
        Private _SuppressEmptyNotes As Boolean = False
        Public Property SuppressEmptyNotes() As Boolean
            Get
                Return _SuppressEmptyNotes
            End Get
            Set(ByVal value As Boolean)
                _SuppressEmptyNotes = value
            End Set
        End Property
        Private _OutputType As String
        Public Property OutputType() As String
            Get
                Return _OutputType
            End Get
            Set(ByVal value As String)
                _OutputType = value
            End Set
        End Property

        Private _IncludeToC As Boolean
        Public Property IncludeToC() As Boolean
            Get
                Return _IncludeToC
            End Get
            Set(ByVal value As Boolean)
                _IncludeToC = value
            End Set
        End Property

        Private _IncludeChildPackages As Boolean

        Public Sub New()
            Me.oDef.LoadFromSettings()
        End Sub

        Public Property IncludeChildPackages() As Boolean
            Get
                Return _IncludeChildPackages
            End Get
            Set(ByVal value As Boolean)
                _IncludeChildPackages = value
            End Set
        End Property
        Function MakePDFReport(FilePath As String)
            Dim saveSuccess As Boolean = False
            Dim generationSuccess As Boolean = False
            Me.docGenerator = Repository.CreateDocumentGenerator()

            ' Create a new document
            Try
                If docGenerator.NewDocument("") = True Then
                    If My.Settings.PDFCoverPage.Length > 0 Then
                        generationSuccess = docGenerator.InsertCoverPageDocument(My.Settings.PDFCoverPage)
                        docGenerator.InsertBreak(EA.DocumentBreak.breakPage)
                    End If
                    If Me.IncludeToC Then
                        generationSuccess = docGenerator.InsertTOCDocument("NSToC")
                        docGenerator.InsertBreak(EA.DocumentBreak.breakPage)
                    End If
                    generationSuccess = Package2Report(Me.Package.PackageID)
                    saveSuccess = docGenerator.SaveDocument(FilePath, IIf(Me.OutputType = "DOCX", 3, 2))
                End If
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
            Return saveSuccess And generationSuccess
        End Function
        Function Package2Report(PackageId As String)
            Dim generationSuccess As Boolean = False
            Try
                generationSuccess = docGenerator.DocumentPackage(PackageId, 0, My.Settings.PDFPackageTemplate)
                Dim oDiagramTable As DataTable
                oDiagramTable = Me.Diagrams4Package(PackageId)
                For Each oDRow In oDiagramTable.Rows
                    generationSuccess = docGenerator.DocumentDiagram(oDRow("diagram_id"), 1, My.Settings.PDFDiagramTemplate)
                    Dim oDT As DataTable
                    oDT = Me.Elements4Diagram(oDRow("diagram_id"))
                    generationSuccess = generationSuccess + Element2Report(oDT, oDRow("name"))
                Next
                Dim oPackDT As DataTable
                oPackDT = Me.Elements4Package(PackageId)
                generationSuccess = generationSuccess + Element2Report(oPackDT, "")
                Dim oPkg As EA.Package
                oPkg = Repository.GetPackageByID(PackageId)
                Dim oSub As EA.Package
                If Me.IncludeChildPackages Then
                    For Each oSub In oPkg.Packages
                        generationSuccess = generationSuccess + Package2Report(oSub.PackageID)
                    Next
                End If
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
            Return generationSuccess
        End Function
        Function Element2Report(oDT As DataTable, diagram_name As String) As Boolean
            Dim generationSuccess As Boolean = True
            Dim oRow As DataRow
            Dim ElementTemplate As String
            Try
                ElementTemplate = My.Settings.PDFElementTemplate
                If diagram_name.Contains("Autorisatie") Then
                    'tweede loop voor de businessroles met de autorisaties
                    If oDT.Rows.Count > 0 Then
                        'generationSuccess = docGenerator.InsertText("Autorisatie", "Heading 1")
                    End If
                    For Each oRow In oDT.Rows
                        Dim ElementId As String
                        ElementId = oRow.Item("object_id").ToString()

                        Dim oElement As EA.Element
                        oElement = Me.Repository.GetElementByID(Convert.ToInt32(ElementId))
                        If oElement.Stereotype = "ArchiMate_BusinessRole" Then
                            generationSuccess = docGenerator.DocumentElement(oRow.Item("object_id"), 2, ElementTemplate + " Autorisation")
                        End If
                    Next
                Else
                    For Each oRow In oDT.Rows
                        Dim ElementId As String
                        ElementId = oRow.Item("object_id").ToString()
                        Dim oElement As EA.Element
                        oElement = Me.Repository.GetElementByID(Convert.ToInt32(ElementId))
                        If (oElement.Notes.Length > 0 Or Me.SuppressEmptyNotes = False) And oElement.Stereotype <> "ArchiMate_BusinessRole" And Not oElement.Tag.ToUpper().Contains("BRON") Then
                            generationSuccess = docGenerator.DocumentElement(oElement.ElementID, 0, ElementTemplate)
                        End If
                    Next
                    'Derde loop voor de bronnen
                    For Each oRow In oDT.Rows
                        Dim ElementId As String
                        ElementId = oRow.Item("object_id").ToString()
                        Dim oElement As EA.Element
                        oElement = Me.Repository.GetElementByID(Convert.ToInt32(ElementId))
                        If oElement.Stereotype = "ArchiMate_ApplicationComponent" And oElement.Tag.ToUpper().Contains("BRON") Then
                            generationSuccess = docGenerator.DocumentElement(oElement.ElementID, 0, ElementTemplate + " Source")
                        End If
                    Next
                End If
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try

            Return generationSuccess
        End Function
        Function Elements4Diagram(Diagram_id As String) As DataTable
            Dim strSql As String
            Try
                If oDef.GetSettingValue("ElementDiagramSQL").Contains("#diagram_id#") Then
                    strSql = oDef.GetSettingValue("ElementDiagramSQL").Replace("#diagram_id#", Diagram_id)
                    Dim oDT As DataTable
                    oDT = DLA2EAHelper.SQL2DataTable(strSql, Me.Repository)
                    Return oDT
                End If
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
            Return New DataTable("Empty")
        End Function

        Function Elements4Package(Package_id As String) As DataTable
            Dim strSql As String
            Try
                If oDef.GetSettingValue("ElementPackageSQL").Contains("#package_id#") Then
                    strSql = oDef.GetSettingValue("ElementPackageSQL").Replace("#package_id#", Package_id)
                    Dim oDT As DataTable
                    oDT = DLA2EAHelper.SQL2DataTable(strSql, Me.Repository)
                    Return oDT
                End If
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
            Return New DataTable("Empty")
        End Function

        Function Diagrams4Package(Package_id As String) As DataTable
            Dim strSql As String
            Try
                If oDef.GetSettingValue("DiagramPackageSQL").Contains("#package_id#") Then
                    strSql = oDef.GetSettingValue("DiagramPackageSQL").Replace("#package_id#", Package_id)
                    Dim oDT As DataTable
                    oDT = DLA2EAHelper.SQL2DataTable(strSql, Me.Repository)
                    Return oDT
                End If
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
            Return New DataTable("Empty")
        End Function
    End Class

End Namespace
