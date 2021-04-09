Imports System.IO
Imports System.Text
Imports TEA.DLAFormfactory
''' <summary>
''' Class for the HTML generator. Various functions to generate HTML pages and
''' snippets from the elements in the repository
''' </summary>
''' 
Public Class HTMLPublicator
    Const ForReading = 1, ForWriting = 2

    Protected sPath As String
    Protected sHome As String
    Protected sTemplate As String
    Protected oSBSiteMap As StringBuilder
    Protected oALDiagrams As New ArrayList()
    Protected oALPackages As New ArrayList()
    Protected oALElements As New ArrayList()
    Protected oDef As New IDEADefinitions()
    Protected blnCreatePDF As Boolean = False
    Protected blnCompositeClickable As Boolean = False
    Private _Rep As EA.Repository
    ''' <summary>
    ''' Create a pdf of the packages that are processed and contain diagrams
    ''' </summary>
    ''' <returns></returns>
    Public Property CreatePDF() As Boolean
        Get
            Return blnCreatePDF
        End Get
        Set(ByVal value As Boolean)
            blnCreatePDF = value
        End Set
    End Property

    Private _Templates As HTMLTemplates
    Public Property Templates() As HTMLTemplates
        Get
            Return _Templates
        End Get
        Set(ByVal value As HTMLTemplates)
            _Templates = value
        End Set
    End Property

    Public Property CompositeClickable() As Boolean
        Get
            Return blnCompositeClickable
        End Get
        Set(ByVal value As Boolean)
            blnCompositeClickable = value
        End Set
    End Property
    Public Property Repository() As EA.Repository
        Get
            Return _Rep
        End Get
        Set(ByVal value As EA.Repository)
            _Rep = value
        End Set
    End Property
    ''' <summary>
    ''' Check if a diagram is already processed by the publisher and if not add it to the list of ready to process diagrams
    ''' </summary>
    ''' <param name="oRow">Row met data</param>
    ''' <returns></returns>
    Private Function CheckAndAddItem(ByVal oRow As DataRow, sTemplateName As String) As Boolean
        If sTemplateName.ToLower.Contains("list_diagrams") Then
            If Me.oALDiagrams.Contains(oRow("diagram_id")) = False Then
                Me.oALDiagrams.Add(oRow("diagram_id"))
                Return True
            End If
        End If
        If sTemplateName.ToLower.Contains("list_packages") Then
            If Me.oALPackages.Contains(oRow("package_id")) = False Then
                Me.oALPackages.Add(oRow("package_id"))
                Return True
            End If

        End If
        If sTemplateName.ToLower.Contains("list_elements") Then
            If Me.oALElements.Contains(oRow("object_id")) = False Then
                Me.oALElements.Add(oRow("object_id"))
                Return True
            End If
        End If
        Return False
    End Function
    ''' <summary>
    ''' Check if a diagram is processed and if so remove it from the list of to be processed diagrams
    ''' </summary>
    ''' <param name="ID"></param>
    ''' <returns></returns>
    Private Function CheckAndRemoveDiagram(ByVal ID As String) As Boolean
        If Me.oALDiagrams.Contains(ID) Then
            Me.oALDiagrams.Remove(ID)
            Me.Repository.WriteOutput("IDEA", "Diagram" + ID + " Removed from Array", 0)
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' Remove a package from the list of packages based on the id
    ''' </summary>
    ''' <param name="ID"></param>
    ''' <returns></returns>
    Private Function CheckAndRemovePackage(ByVal ID As String) As Boolean
        If Me.oALPackages.Contains(ID) Then
            Me.oALPackages.Remove(ID)
            Me.Repository.WriteOutput("IDEA", "Package" + ID + " Removed from Array", 0)
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' Remove a package from the list of elements based on the id
    ''' </summary>
    ''' <param name="ID"></param>
    ''' <returns>Success</returns>
    Private Function CheckAndRemoveElement(ByVal ID As String) As Boolean
        If Me.oALElements.Contains(ID) Then
            Me.oALElements.Remove(ID)
            Me.Repository.WriteOutput("IDEA", "Element" + ID + " Removed from Array", 0)
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub New()
        'Definieer de globale variabelen en objecten
        oDef.LoadFromSettings()
        sPath = oDef.GetSettingValue("HTMLPath")
        Me.oSBSiteMap = New StringBuilder("")
        Me.oSBSiteMap.Append("<UL>")

    End Sub
    ''' <summary>
    ''' Read the template from a template file on the disk
    ''' </summary>
    ''' <param name="sTemplateFile">Name of the template file</param>
    Public Sub ReadHTMLTemplate(ByVal sTemplateFile As String)
        If File.Exists(sTemplateFile) Then
            Me.sTemplate = File.ReadAllText(sTemplateFile)
        End If
    End Sub
    ''' <summary>
    ''' Create a html href tag from the filename and the title
    ''' </summary>
    ''' <param name="sTitel">title of the hyperlink</param>
    ''' <param name="sURL">url to link to</param>
    ''' <returns></returns>
    Public Function MaakURL(ByVal sTitel As String, ByVal sURL As String) As String
        'Maak een url voor een lijst of overzicht 
        Dim oRow As DataRow
        oRow = Me._Templates.GetRowByName("Default_url")
        Dim sText As String = oRow("template_body")
        Return sText.Replace("#url#", sURL).Replace("#title#", sTitel)
    End Function
    ''' <summary>
    ''' Write the processed string to a file and do some processing for the tags
    ''' </summary>
    ''' <param name="sNaam">Name of the file</param>
    ''' <param name="sContent">html content</param>
    Public Sub Export2HTML(ByVal sNaam As String, ByVal sContent As String)
        'Export content naar een htmlfile gebaseerd op een constante in het templatebased on the constant of the path 
        Dim sValue As String
        sValue = sTemplate.Replace("#content#", sContent)
        'when a homepage is defined this is added too
        sValue = sValue.Replace("#home", sHome)
        File.WriteAllText(FullFileName(sPath, sNaam), sValue)
        Repository.WriteOutput("IDEA", FullFileName(sPath, sNaam) + " Is created", 0)
    End Sub
    ''' <summary>
    ''' Make a full file name of the html file including the path and the extension
    ''' </summary>
    ''' <param name="sPath"></param>
    ''' <param name="sNaam"></param>
    ''' <returns></returns>
    Public Shared Function FullFileName(ByVal sPath As String, ByVal sNaam As String) As String
        If Not sNaam.ToLower.Contains(".htm") Then
            Return sPath & sNaam & ".html"
        End If
        Return sPath & sNaam
    End Function
    ''' <summary>
    ''' Publish the package content to Html pages
    ''' </summary>
    ''' <param name="oPkg"></param>
    Public Sub PubliceerRootPackage(ByVal oPkg As EA.Package)
        'Voor de zekerheid als de root anders verwerkt gaat worden
        sHome = "package" + oPkg.PackageID.ToString() + ".html"
        PubliceerPackage(oPkg)
        PubliceerSubPackages()
        PubliceerDiagrams()
        PubliceerElements()
    End Sub
    ''' <summary>
    ''' Publish the subpackages of a package
    ''' </summary>
    Public Sub PubliceerSubPackages()
        Try
            Dim oRow As DataRow = Me._Templates.GetRowByName("Detail_Package")
            Dim sResultaat As String = ""

            Dim strId As String
            While Me.oALPackages.Count > 0
                strId = Me.oALPackages(0)
                Dim oPkg As EA.Package
                oPkg = Me.Repository.GetPackageByID(strId)
                If Not File.Exists(FullFileName(Me.sPath, "element" + strId)) Then
                    MaakSiteMapURL(MaakURL(oPkg.Name, "package" + oPkg.PackageID.ToString()), True)
                    sResultaat = ""
                    sResultaat = Me.ProcessSQL2Template(oRow(HTMLTemplates.T_NAME), oRow(HTMLTemplates.T_SQL), oRow(HTMLTemplates.T_HEADER), oRow(HTMLTemplates.T_BODY), oRow(HTMLTemplates.T_FOOTER), strId)
                    'maak een pdf document aan als er een template voor is
                    If sResultaat.Contains("#package_pdf#") And Me.blnCreatePDF = True And oPkg.Diagrams.Count > 0 Then
                        MaakDocumentVoorPackage(oPkg)
                        sResultaat = sResultaat.Replace("#package_pdf#", "<a href='" + sPath & "packagedocument" & oPkg.PackageID.ToString() & ".PDF' target='_blank' >Open PDF</a>")
                    Else
                        sResultaat = sResultaat.Replace("#package_pdf#", "&nbsp;")
                    End If
                    Export2HTML("package" + strId, sResultaat)
                End If
                Me.CheckAndRemovePackage(strId)
            End While
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Process all the elements etc based on the collection of html templates
    ''' </summary>
    Public Sub PubliceerHTMLPagina()
        Try
            Dim oRow As DataRow
            Dim sResultaat As String = ""
            For Each oRow In Me.Templates.Templates.Rows
                If oRow(HTMLTemplates.T_NAME).ToString().ToLower().Contains(".html") Then
                    sResultaat = Me.ProcessSQL2Template(oRow(HTMLTemplates.T_NAME), oRow(HTMLTemplates.T_SQL), oRow(HTMLTemplates.T_HEADER), oRow(HTMLTemplates.T_BODY), oRow(HTMLTemplates.T_FOOTER), "")
                    Export2HTML(oRow(HTMLTemplates.T_NAME), sResultaat)
                End If
            Next
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try

    End Sub
    ''' <summary>
    ''' Publish  all the diagrams based on an array of found diagrams
    ''' </summary>
    Public Sub PubliceerDiagrams()
        Try
            Dim oRow As DataRow = Me._Templates.GetRowByName("Detail_Diagram")
            Dim sResultaat As String = ""

            Dim strId As String
            While Me.oALDiagrams.Count > 0
                strId = Me.oALDiagrams(0)
                Dim oDgm As EA.Diagram
                oDgm = Me.Repository.GetDiagramByID(strId)
                If Not File.Exists(FullFileName(Me.sPath, "element" + strId)) Then

                    MaakSiteMapURL(MaakURL(oDgm.Name, "diagram" + oDgm.DiagramID.ToString()), False)
                    sResultaat = ""
                    sResultaat = Me.ProcessSQL2Template(oRow(HTMLTemplates.T_NAME), oRow(HTMLTemplates.T_SQL), oRow(HTMLTemplates.T_HEADER), oRow(HTMLTemplates.T_BODY), oRow(HTMLTemplates.T_FOOTER), strId)
                    If sResultaat.Contains("#diagram_map#") Then
                        Dim strMap As String
                        strMap = MakeDiagramMap(oDgm)
                        sResultaat = sResultaat.Replace("#diagram_map#", strMap)
                    End If
                    Export2HTML("diagram" + strId, sResultaat)
                    Me.SaveDiagramImage(strId)
                End If
                Me.CheckAndRemoveDiagram(strId)
            End While
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try

    End Sub
    ''' <summary>
    ''' Publish all the elements found based on an array of found elements
    ''' </summary>
    Public Sub PubliceerElements()
        Try
            Dim oRow As DataRow = Me._Templates.GetRowByName("Detail_Element")
            Dim sResultaat As String = ""
            Dim strId As String
            If oRow(HTMLTemplates.T_BODY).ToString().Length > 0 Then
                While Me.oALElements.Count > 0
                    strId = Me.oALElements(0)
                    If Not File.Exists(FullFileName(Me.sPath, "element" + strId)) Then
                        sResultaat = ""
                        sResultaat = Me.ProcessSQL2Template(oRow(HTMLTemplates.T_NAME), oRow(HTMLTemplates.T_SQL), oRow(HTMLTemplates.T_HEADER), oRow(HTMLTemplates.T_BODY), oRow(HTMLTemplates.T_FOOTER), strId)
                        Export2HTML("element" + strId, sResultaat)
                    End If
                    Me.CheckAndRemoveElement(strId)
                End While
            End If
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try

    End Sub
    ''' <summary>
    ''' Write the diagram to disk as a png file on a standardized location
    ''' </summary>
    Public Sub SaveDiagramImage(ByVal sDiagram As String)
        'Schrijf diagram weg naar een plaatje 
        Dim objProject As EA.Project
        objProject = Repository.GetProjectInterface()
        Repository.OpenDiagram(Convert.ToInt32(sDiagram))
        objProject.SaveDiagramImageToFile(sPath + "diagram" + sDiagram + ".png")
        Repository.CloseDiagram(Convert.ToInt32(sDiagram))
    End Sub
    ''' <summary>
    ''' Create a sitemap url for a package or diagram. With diagram you can define a tree like structure
    ''' </summary>
    ''' <param name="sURL"></param>
    ''' <param name="bNiveau"></param>
    Public Sub MaakSiteMapURL(ByVal sURL As String, ByVal bNiveau As String)
        'Opbouwen van de sitemap met URLs packages krijgen een h3
        If bNiveau = True Then
            oSBSiteMap.Append("<strong>")
        End If
        oSBSiteMap.Append("<li>" + sURL + "</li>")
        If bNiveau = True Then
            oSBSiteMap.Append("</strong>")
        End If
    End Sub
    ''' <summary>
    ''' Process sql statement for various templates for an element
    ''' </summary>
    ''' <param name="sNaam"></param>
    ''' <param name="sSql"></param>
    ''' <param name="sHeader"></param>
    ''' <param name="sBody"></param>
    ''' <param name="sFooter"></param>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function ProcessSQL2Template(ByRef sNaam As String, sSql As String, sHeader As String, sBody As String, sFooter As String, id As String) As String
        Dim oDataTable As DataTable
        Dim sResultaat As String = ""
        Try
            'eerst gaan we het element zelf verwerken
            If sSql.Length > 0 Then
                sSql = sSql.Replace("#id#", id)
                oDataTable = DLA2EAHelper.SQL2DataTable(sSql, Me.Repository)
                Dim oColumn As DataColumn
                Dim oRow As DataRow
                For Each oRow In oDataTable.Rows
                    Dim sRegel = sBody
                    For Each oColumn In oDataTable.Columns
                        If sRegel.Contains("#" + oColumn.ColumnName.ToLower() + "#") Then
                            sRegel = sRegel.Replace("#" + oColumn.ColumnName.ToLower() + "#", oRow(oColumn.ColumnName))
                        End If
                    Next
                    Me.CheckAndAddItem(oRow, sNaam)
                    sResultaat += sRegel
                Next
            Else
                sResultaat = sBody
            End If
            'daarna gaan we kijken of er nog andere html blokken zitten
            While sResultaat.Contains("#list") Or sResultaat.Contains("#detail")
                Repository.WriteOutput("IDEA", sResultaat, 0)
                Dim oTmplRow As DataRow
                oTmplRow = Me.Templates.GetRowByTemplate(sResultaat)
                sResultaat = sResultaat.Replace("#" + oTmplRow(HTMLTemplates.T_NAME).ToLower() + "#", Me.ProcessSQL2Template(oTmplRow(HTMLTemplates.T_NAME), oTmplRow(HTMLTemplates.T_SQL), oTmplRow(HTMLTemplates.T_HEADER), oTmplRow(HTMLTemplates.T_BODY), oTmplRow(HTMLTemplates.T_FOOTER), id))
            End While

            If sResultaat.Length > 0 Then
                Return sHeader + sResultaat + sFooter
            Else
                Return ""
            End If
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
        Return ""
    End Function
    ''' <summary>
    ''' Publish a package to html
    ''' </summary>
    ''' <param name="oPkg">Object of the package to process</param>
    Public Sub PubliceerPackage(ByVal oPkg As EA.Package)
        Try
            Dim oRow As DataRow = Me._Templates.GetRowByName("Detail_Package")
            Dim sResultaat As String = ""
            MaakSiteMapURL(MaakURL(oPkg.Name, "package" + oPkg.PackageID.ToString()), True)

            sResultaat = Me.ProcessSQL2Template(oRow(HTMLTemplates.T_NAME), oRow(HTMLTemplates.T_SQL), oRow(HTMLTemplates.T_HEADER), oRow(HTMLTemplates.T_BODY), oRow(HTMLTemplates.T_FOOTER), oPkg.PackageID)

            If sResultaat.Contains("#package_pdf#") And oPkg.Diagrams.Count > 0 And Me.blnCreatePDF = True Then
                MaakDocumentVoorPackage(oPkg)
                sResultaat = sResultaat.Replace("#package_pdf#", "<a href='" + sPath & "packagedocument" & oPkg.PackageID.ToString() & ".PDF' target='_blank' >Open PDF</a>")
            Else
                sResultaat = sResultaat.Replace("#package_pdf#", "#")
            End If
            Export2HTML("package" + oPkg.PackageID.ToString(), sResultaat)
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try

    End Sub
    ''' <summary>
    ''' Write the constructed sitemap to a file 
    ''' </summary>
    Sub PublishSiteMap()
        Export2HTML("SiteMap", oSBSiteMap.ToString())
    End Sub
    ''' <summary>
    ''' Convert an element and a diagramobject in a diagram to a map tag in a html construction. In here a number of calculations and transformations are needed
    ''' </summary>
    ''' <param name="oElement">Element to process</param>
    ''' <param name="oDO">Data object to process (read the coordinates)</param>
    ''' <param name="MinX">Min X position of alle the elements</param>
    ''' <param name="MinY">Min Y position of alle the elements</param>
    ''' <param name="MaxX">Max X position of alle the elements when resizing is needed</param>
    ''' <returns></returns>
    Public Function Element2Map(ByVal oElement As EA.Element, ByVal oDO As EA.DiagramObject, ByVal MinX As Double, ByVal MinY As Double, ByVal MaxX As Double) As String
        Dim sCoord
        Dim dFactor As Double
        Try
            sCoord = ""

            If ((oElement.IsComposite = True And Me.blnCompositeClickable = True) Or Me.blnCompositeClickable = False) Then
                'Calculate the coordinates for each part of an element
                dFactor = 1

                sCoord += "<area shape='rect' coords='"
                sCoord += Int(Convert.ToDouble(oDO.left - 10) * dFactor).ToString() + ","
                sCoord += Int(Convert.ToDouble(Math.Abs(oDO.top) - 10) * dFactor).ToString() + ","
                sCoord += Int(Convert.ToDouble(oDO.right + MinX) + 10).ToString() + ","
                sCoord += Int(Convert.ToDouble(Math.Abs(oDO.bottom) + 10) * dFactor).ToString() + "' target='_self'  "
                If oElement.IsComposite = True Then
                    sCoord += " href='diagram" + oElement.CompositeDiagram.diagramID.ToString() + ".html' >"
                Else
                    sCoord += " href='element" + oElement.ElementID.ToString() + ".html' >"
                End If
            End If
            Return sCoord
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
        Return ""
    End Function
    ''' <summary>
    ''' Publish a diagram and all the elements to a html page
    ''' </summary>
    ''' <param name="oDgm"></param>
    Public Function MakeDiagramMap(ByVal oDgm As EA.Diagram) As String
        '    Dim oSBInhoud As New StringBuilder()
        '    Dim sFNaam As String
        Dim oElement As EA.Element
        Dim oDiagramObject As EA.DiagramObject
        '    Dim sOpsommingElement As String
        Dim sMap As String
        Dim iMinX As Integer
        Dim iMinY As Integer
        Dim iMaxX As Integer

        'Aanmaken map elementen
        Try
            sMap = ""
            'calculate the min and max coordinates for all elements in the diagram
            'necessary for positioning the rect elements
            iMaxX = 0
            iMinX = 1000
            iMinY = 1000
            For Each oDiagramObject In oDgm.DiagramObjects
                If Math.Abs(oDiagramObject.top) < iMinY Then
                    iMinY = Math.Abs(oDiagramObject.top)
                End If
                If oDiagramObject.left < iMinX Then
                    iMinX = oDiagramObject.left
                End If
                If oDiagramObject.right > iMaxX Then
                    iMaxX = oDiagramObject.right
                End If
            Next
            'create all the elements based on the option of a combined diagram element page or not
            For Each oDiagramObject In oDgm.DiagramObjects
                oElement = Repository.GetElementByID(oDiagramObject.ElementID)
                sMap += Element2Map(oElement, oDiagramObject, iMinX, iMinY, iMaxX)
            Next
            Return "<map name='ideamap' >" + sMap + "</map>"
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
        Return ""
    End Function

    ''' <summary>
    ''' Create a document for the package content
    ''' </summary>
    ''' <param name="oPkg">Package object</param>
    Public Sub MaakDocumentVoorPackage(ByVal oPkg As EA.Package)

        Dim oPDF As New PDFCreator()
        oPDF.Repository = Me.Repository
        oPDF.Package = oPkg
        oPDF.MakePDFReport(sPath & "packagedocument" & oPkg.PackageID.ToString() & ".PDF")


    End Sub
    ''' <summary>
    ''' delete all the files available in the publication folder
    ''' </summary>
    Sub DeleteFilesInFolder()
        If Directory.Exists(sPath) Then
            For Each _file As String In Directory.GetFiles(sPath)
                File.Delete(_file)
            Next
        End If
    End Sub

    ''' <summary>
    ''' Generate the documentation based on the first package
    ''' </summary>
    ''' <param name="oPackage">The rootpackage to start processing</param>
    Public Sub Generate(ByVal oPackage As EA.Package)
        Try
            Repository.CreateOutputTab("IDEA")
            Repository.EnableUIUpdates = False
            sTemplate = Me.Templates.GetRowByName("Page_Template")(HTMLTemplates.T_BODY)
            ReadHTMLTemplate(sPath + "\" + oDef.GetSettingValue("HTMLTemplate"))
            DeleteFilesInFolder()
            Repository.WriteOutput("IDEA", "Generator is started", 0)
            PubliceerRootPackage(oPackage)
            PubliceerHTMLPagina()
            PublishSiteMap()
            Repository.WriteOutput("IDEA", "Generator is ready", 0)
            Repository.EnableUIUpdates = True
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub
End Class
