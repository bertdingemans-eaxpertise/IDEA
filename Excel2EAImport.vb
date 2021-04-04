Imports System.Data.OleDb
Namespace DLAFormfactory

    ''' <summary>
    ''' Routines for integration EA and Excel, this is a class with numerous features
    ''' for importing excel sheets and make transformations for complex (ArchiMate)
    ''' models
    ''' </summary>
    Public Class Excel2EAImport

    Private _Repository As EA.Repository
    Public DTable As DataTable
    Public Property Repository() As EA.Repository
        Get
            Return _Repository
        End Get
        Set(ByVal value As EA.Repository)
            _Repository = value
        End Set
    End Property
    Public Sub HollandCasino()
        'losse import repo
        Dim intTeller As Int16 = 0
        Dim blnFound As Boolean = False
        Dim objProces As New DataSet2Repository()
        objProces.Package_Id = Repository.GetCurrentDiagram().PackageID
        Me.Repository.EnableUIUpdates = False
        Try
            objProces.Repository = Me.Repository
            Dim oRow As DataRow
            For Each oRow In Me.DTable.Rows
                Dim objBron As EA.Element
                Dim objDoel As EA.Element
                Dim objTabel As EA.Element
                If Not IsDBNull(oRow("Bron")) And Not IsDBNull(oRow("Doel")) And Not IsDBNull(oRow("Tabel")) Then
                    objBron = objProces.FindOrAddElement(oRow("Bron"), "ArchiMate_ApplicationComponent", blnFound)
                    objDoel = objProces.FindOrAddElement(oRow("Doel"), "ArchiMate_ApplicationComponent", blnFound)
                    blnFound = False
                    objTabel = objProces.FindOrAddElement(oRow("Tabel"), "", blnFound)
                    objProces.AddConnector(objTabel, objBron, "Trace")
                    objProces.AddConnector(objTabel, objDoel, "Trace")
                    Dim oAttrib As EA.Attribute
                    Dim strFormaat As String
                    If IsDBNull(oRow("Formaat")) Then
                        strFormaat = "?"
                    Else
                        strFormaat = oRow("Formaat")
                    End If
                    Dim strOmschrijving As String
                    If IsDBNull(oRow("Omschrijving")) Then
                        strOmschrijving = ""
                    Else
                        strOmschrijving = oRow("Omschrijving")
                    End If
                    oAttrib = objProces.AddAttribute(objTabel, oRow("Veld"), strFormaat, strOmschrijving)
                    If Not IsNothing(oAttrib) Then
                        oAttrib.Alias = strFormaat
                        If Not IsDBNull(oRow("Primary key")) Then
                            If oRow("Primary key") = "1" Then
                                oAttrib.IsID = True
                            End If
                        End If
                        If Not IsDBNull(oRow("Verplicht")) Then
                            If oRow("Verplicht") = "0" Then
                                oAttrib.LowerBound = "0"
                            End If
                        End If
                        Dim strVoorbeeld As String
                        If Not IsDBNull(oRow("Voorbeeld Data")) Then
                            strVoorbeeld = oRow("Voorbeeld Data")
                            Dim oAT As EA.AttributeTag
                            oAT = oAttrib.TaggedValues.AddNew("Voorbeeld", "")
                            oAT.Value = strVoorbeeld
                            oAT.Update()
                        End If
                        oAttrib.Update()
                    End If
                End If
            Next
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
        Me.Repository.EnableUIUpdates = True

    End Sub

        Public Sub LVNL(oPB As System.Windows.Forms.ProgressBar)
            'losse import repo
            Dim intTeller As Int16 = 0
            Dim blnFound As Boolean = False
            Dim objProces As New DataSet2Repository()
            objProces.Package_Id = Repository.GetTreeSelectedPackage().PackageID
            Me.Repository.EnableUIUpdates = False
            Try
                oPB.Value = 0
                oPB.Minimum = 0
                oPB.Maximum = Me.DTable.Rows.Count
                objProces.Repository = Me.Repository
                objProces.Package_Id = Repository.GetTreeSelectedPackage().PackageID
                Dim oRow As DataRow
                Dim blnFirst As Boolean = True
                Dim strMetaModel As String = ""
                Dim objLVNL As EA.Element
                For Each oRow In Me.DTable.Rows
                    If blnFirst = True Then
                        If Not IsDBNull(oRow("Metamodel")) Then
                            strMetaModel = oRow("Metamodel").ToUpper()
                            blnFirst = False
                        End If
                    End If
                    Dim sDate As String
                    Dim dDate As Date

                    Select Case strMetaModel
                        Case "WORKPACKAGE"
                            If Not IsDBNull(oRow("Name")) Then
                                objLVNL = objProces.FindOrAddElement(oRow("Name"), "ArchiMate_WorkPackage", blnFound)
                                If Not IsDBNull(oRow("Notes")) Then
                                    Dim strNotes As String
                                    strNotes = oRow("Notes")
                                    objLVNL.Notes = strNotes
                                End If
                                objProces.AddOrUpdateTaggedValue(objLVNL, "LVNL: activity type", oRow, False)
                                ' objProces.AddOrUpdateTaggedValue(objLVNL, "LVNL: owner", oRow, False)
                                objProces.AddOrUpdateTaggedValue(objLVNL, "LVNL: category portfolio", oRow, False)
                                objProces.AddOrUpdateTaggedValue(objLVNL, "LVNL: project ID", oRow, False)
                                'objProces.AddOrUpdateTaggedValue(objLVNL, "LVNL: proflow ID", oRow, False)
                                sDate = oRow("LVNL: start date")
                                dDate = Convert.ToDateTime(sDate)
                                sDate = dDate.ToString("dd-MM-yyyy")
                                objProces.AddOrUpdateTaggedValue(objLVNL, "LVNL: start date", sDate, False)
                                sDate = oRow("LVNL: end date")
                                dDate = Convert.ToDateTime(sDate)
                                sDate = dDate.ToString("dd-MM-yyyy")
                                objProces.AddOrUpdateTaggedValue(objLVNL, "LVNL: end date", sDate, False)
                                objProces.AddOrUpdateTaggedValue(objLVNL, "LVNL: project/activity leader", oRow, False)
                                objProces.AddElementAndConnector(objLVNL, oRow, "Programma", "ArchiMate_WorkPackage", "ArchiMate_Association")
                                objLVNL.Update()
                            End If
                        Case "STAKEHOLDER"
                            If Not IsDBNull(oRow("Name")) Then
                                objLVNL = objProces.FindOrAddElement(oRow("Name"), "ArchiMate_Stakeholder", blnFound)
                                If Not IsDBNull(oRow("Notes")) Then
                                    objLVNL.Notes = oRow("Notes")
                                End If
                                'objProces.AddOrUpdateTaggedValue(objLVNL, "LVNL: stakeholder type", oRow, False)
                                objProces.AddOrUpdateTaggedValue(objLVNL, "LVNL: Users", oRow, False)
                                objProces.AddOrUpdateTaggedValue(objLVNL, "LVNL: location", oRow, False)
                                objLVNL.Update()
                            End If
                        Case "DRIVER"
                            If Not IsDBNull(oRow("Name")) Then
                                objLVNL = objProces.FindOrAddElement(oRow("Name"), "ArchiMate_Driver", blnFound)
                                If Not IsDBNull(oRow("Notes")) Then
                                    objLVNL.Notes = oRow("Notes")
                                End If
                                'objProces.AddOrUpdateTaggedValue(objLVNL, "LVNL: driver type", oRow, False)
                                'objProces.AddOrUpdateTaggedValue(objLVNL, "LVNL: reference", oRow, False)
                                objLVNL.Update()
                            End If
                        Case "PROGRAMMA"
                            If Not IsDBNull(oRow("Name")) Then
                                objLVNL = objProces.FindOrAddElement(oRow("Name"), "ArchiMate_WorkPackage", blnFound)
                                If Not IsDBNull(oRow("Notes")) Then
                                    objLVNL.Notes = oRow("Notes")
                                End If
                                objLVNL.Update()
                            End If
                        Case "DELIVERABLE"
                            If Not IsDBNull(oRow("Name")) Then
                                objLVNL = objProces.FindOrAddElement(oRow("Name"), "ArchiMate_Deliverable", blnFound)
                                If Not IsDBNull(oRow("Notes")) Then
                                    objLVNL.Notes = oRow("Notes")
                                End If
                                Dim objResource As EA.Resource
                                objResource = objLVNL.Resources.AddNew(oRow("Name"), "")
                                objResource.Role = "EATMA"
                                objProces.AddOrUpdateTaggedValue(objLVNL, "LVNL: proflow ID", oRow, False)
                                sDate = oRow("LVNL: start date").ToString()
                                If sDate.Length > 0 Then
                                    dDate = Convert.ToDateTime(sDate)
                                    sDate = dDate.ToString("dd-MM-yyyy").Replace("-1-", "-01-").Replace("-1-", "-01-").Replace("-2-", "-02-").Replace("-3-", "-03-").
                                    Replace("-4-", "-04-").Replace("-5-", "-05-").Replace("-6-", "-06-").Replace("-7-", "-08-").Replace("-9-", "-09-")
                                    objResource.DateStart = dDate

                                End If
                                objProces.AddOrUpdateTaggedValue(objLVNL, "LVNL: start date", sDate, False)
                                sDate = oRow("LVNL: end date").ToString()
                                If sDate.Length > 0 Then
                                    dDate = Convert.ToDateTime(sDate)
                                    sDate = dDate.ToString("dd-MM-yyyy").Replace("-1-", "-01-").Replace("-1-", "-01-").Replace("-2-", "-02-").Replace("-3-", "-03-").
                                    Replace("-4-", "-04-").Replace("-5-", "-05-").Replace("-6-", "-06-").Replace("-7-", "-08-").Replace("-9-", "-09-")
                                    objResource.DateEnd = dDate
                                End If

                                objProces.AddOrUpdateTaggedValue(objLVNL, "LVNL: end date", sDate, False)
                                objProces.AddOrUpdateTaggedValue(objLVNL, "LVNL: project/activity leader", oRow, False)
                                objLVNL.Update()
                                Try
                                    objResource.Update()
                                Catch ex As Exception
                                    MsgBox(ex.ToString())
                                End Try
                            End If
                        Case "CAPABILITYCLUSTER"
                            If Not IsDBNull(oRow("Name")) Then
                                objLVNL = objProces.FindOrAddElement(oRow("Name"), "ArchiMate_Capability", blnFound)
                                If Not IsDBNull(oRow("Notes")) Then
                                    objLVNL.Notes = oRow("Notes")
                                End If
                                objLVNL.Update()
                                If Not IsDBNull(oRow("Group")) Then
                                    Dim objPack As EA.Package
                                    objPack = objProces.FindPackage(oRow("Group"))
                                    If Not IsNothing(objPack) Then
                                        Dim strSql As String
                                        strSql = String.Format("UPDATE t_object SET package_id = {1} WHERE object_id = {0} ", objLVNL.ElementID.ToString(), objPack.PackageID.ToString())
                                        Repository.Execute(strSql)
                                    End If
                                End If
                                'objProces.AddOrUpdateTaggedValue(objLVNL, "LVNL: type capability domain", oRow, False)
                                objLVNL.Update()
                            End If
                        Case "VALUE"
                            If Not IsDBNull(oRow("Name")) Then
                                objLVNL = objProces.FindOrAddElement(oRow("Name"), "ArchiMate_Value", blnFound)
                                If Not IsDBNull(oRow("Notes")) Then
                                    objLVNL.Notes = oRow("Notes")
                                End If
                                objLVNL.Update()
                                objProces.AddElementAndConnector(objLVNL, oRow, "Cluster", "ArchiMate_Value", "ArchiMate_Aggregation")
                            End If
                        Case "PLATEAU"
                            If Not IsDBNull(oRow("Name")) Then
                                objLVNL = objProces.FindOrAddElement(oRow("Name"), "ArchiMate_Plateau", blnFound)
                                If Not IsDBNull(oRow("Notes")) Then
                                    objLVNL.Notes = oRow("Notes")
                                End If
                                objProces.AddOrUpdateTaggedValue(objLVNL, "LVNL: Realisation data", oRow, False)
                                objProces.AddOrUpdateTaggedValue(objLVNL, "LVNL: Year", oRow, False)
                                objLVNL.Update()
                            End If
                        Case "BUSINESS FUNCTION"
                            If Not IsDBNull(oRow("Name")) Then
                                objLVNL = objProces.FindOrAddElement(oRow("Name"), "ArchiMate_BusinessFunction", blnFound)
                                If Not IsDBNull(oRow("Notes")) Then
                                    objLVNL.Notes = oRow("Notes")
                                End If
                                objProces.AddOrUpdateTaggedValue(objLVNL, "LVNL: Location", oRow, False)
                                objLVNL.Update()
                            End If
                        Case "OUTCOME"
                            If Not IsDBNull(oRow("Name")) Then
                                objLVNL = objProces.FindOrAddElement(oRow("Name"), "ArchiMate_Outcome", blnFound)
                                If Not IsDBNull(oRow("Notes")) Then
                                    objLVNL.Notes = oRow("Notes")
                                End If
                                objLVNL.Update()
                            End If
                        Case "GOAL"
                            If Not IsDBNull(oRow("Name")) Then
                                objLVNL = objProces.FindOrAddElement(oRow("Name"), "ArchiMate_Goal", blnFound)
                                If Not IsDBNull(oRow("Notes")) Then
                                    objLVNL.Notes = oRow("Notes")
                                End If
                                objProces.AddOrUpdateTaggedValue(objLVNL, "LVNL: Vision source", oRow, False)
                                objLVNL.Update()
                            End If
                    End Select
                    oPB.Increment(1)
                Next
            Catch ex As Exception
                MsgBox(ex.ToString())
                DLA2EAHelper.Error2Log(ex)
            End Try
            Me.Repository.EnableUIUpdates = True

        End Sub



        Public Sub ARISImport(ByVal Guid As String)
        'losse import repo
        Dim intTeller As Int16 = 0
        Dim blnFound As Boolean = False
        Dim objProces As New DataSet2Repository()
        Dim objDiagram As EA.Diagram
        Try
            'aanmaken object en instellen referenties naar essentiele EA objecten
            objProces.Repository = Me.Repository
            objProces.Package_Id = Repository.GetPackageByGuid(Guid).PackageID

            'vaste zaken regelen voor snelle verwerking zoals de classificaties en de rollen
            Dim objEigenaarRol As EA.Element
            objEigenaarRol = objProces.FindOrAddElement("Domeineigenaar", "ArchiMate_BusinessRole", blnFound, "")
            Dim objStewardRol As EA.Element
            objStewardRol = objProces.FindOrAddElement("Data steward", "ArchiMate_BusinessRole", blnFound, "")
            'Beschikbaarheid
            Dim objBHoog As EA.Element
            objBHoog = objProces.FindOrAddElement("Beschikbaarheid hoog", "ArchiMate_Requirement", blnFound, "")
            Dim objBMiddel As EA.Element
            objBMiddel = objProces.FindOrAddElement("Beschikbaarheid middel", "ArchiMate_Requirement", blnFound, "")
            Dim objBLaag As EA.Element
            objBLaag = objProces.FindOrAddElement("Beschikbaarheid laag", "ArchiMate_Requirement", blnFound, "")
            'Integriteit
            Dim objIHoog As EA.Element
            objIHoog = objProces.FindOrAddElement("Integriteit hoog", "ArchiMate_Requirement", blnFound, "")
            Dim objIMiddel As EA.Element
            objIMiddel = objProces.FindOrAddElement("Integriteit middel", "ArchiMate_Requirement", blnFound, "")
            Dim objILaag As EA.Element
            objILaag = objProces.FindOrAddElement("Integriteit laag", "ArchiMate_Requirement", blnFound)

            'Vertrouwelijkheid
            Dim objVHoog As EA.Element
            objVHoog = objProces.FindOrAddElement("Vertrouwelijkheid hoog", "ArchiMate_Requirement", blnFound, "")
            Dim objVMiddel As EA.Element
            objVMiddel = objProces.FindOrAddElement("Vertrouwelijkheid middel", "ArchiMate_Requirement", blnFound, "")
            Dim objVLaag As EA.Element
            objVLaag = objProces.FindOrAddElement("Vertrouwelijkheid laag", "ArchiMate_Requirement", blnFound)

            Dim strNaam As String

            For Each objRow In Me.DTable.Rows
                'object inlezen
                Dim objEntiteit As EA.Element
                If Not IsDBNull(objRow.Item("Objectnaam")) Then
                    strNaam = objRow.Item("Objectnaam")
                    blnFound = False
                    objEntiteit = objProces.FindOrAddElement(strNaam, "ArchiMate_BusinessObject", blnFound)
                    objEntiteit.Update()
                    'sleutel aanmaken
                    Dim strIdO As String = ""
                    strIdO = "DE" + strIdO.PadRight(5 - intTeller.ToString().Length, "0") + intTeller.ToString()
                    objProces.AddOrUpdateTaggedValue(objEntiteit, "Id", strIdO, False)

                    'aantal tagged values voor korte elementen die waarschijnlijk in de rest van het proces minder relevant zijn
                    'If Not IsDBNull(objRow.Item("Afgeleid")) Then
                    'objProces.AddOrUpdateTaggedValue(objEntiteit, "Afgeleid", objRow.Item("Afgeleid"), False)
                    'End If
                    'If Not IsDBNull(objRow.Item("Type data")) Then
                    '    objProces.AddOrUpdateTaggedValue(objEntiteit, "Type data", objRow.Item("Type data"), False)
                    'End If
                    If Not IsDBNull(objRow.Item("Persoonsgegeven")) Then
                        objProces.AddOrUpdateTaggedValue(objEntiteit, "Persoonsgegeven", objRow.Item("Persoonsgegeven"), False)
                    End If
                    'staat uit omdat ie nog niet in de excel voorkomt
                    If Not IsDBNull(objRow.Item("Objectstatus")) Then
                        objProces.AddOrUpdateTaggedValue(objEntiteit, "Objectstatus", objRow.Item("Objectstatus"), False)
                    End If
                    If Not IsDBNull(objRow.Item("Metadatastatus")) Then
                        objProces.AddOrUpdateTaggedValue(objEntiteit, "Metadatastatus", objRow.Item("Metadatastatus"), False)
                    End If

                    If Not IsDBNull(objRow.Item("Gevoelig")) Then
                        objProces.AddOrUpdateTaggedValue(objEntiteit, "Gevoelig", objRow.Item("Gevoelig"), False)
                    End If
                    If Not IsDBNull(objRow.Item("Bijzonder")) Then
                        Dim strTest As String
                        strTest = objRow.Item("Bijzonder")
                        objProces.AddOrUpdateTaggedValue(objEntiteit, "Bijzonder", objRow.Item("Bijzonder"), False)
                    End If
                    If Not IsDBNull(objRow.Item("Voorbeeld")) Then
                        objProces.AddOrUpdateTaggedValue(objEntiteit, "Voorbeeld", objRow.Item("Voorbeeld"), True)
                    End If
                    If Not IsDBNull(objRow.Item("Business key")) Then
                        objProces.AddOrUpdateTaggedValue(objEntiteit, "Business key", objRow.Item("Business key"), True)
                    End If
                    If Not IsDBNull(objRow.Item("Bewaartermijn")) Then
                        objProces.AddOrUpdateTaggedValue(objEntiteit, "Bewaartermijn", objRow.Item("Bewaartermijn"), False)
                    End If
                    'voor het verwerken van de koppeling een diagram maken
                    objDiagram = Me.CreateDiagramFromElement(objEntiteit)
                    'verwerken koppelingen met requirements
                    'If Not IsDBNull(objRow.Item("Beschikbaarheid")) Then
                    '    Dim strBeschikbaarheid As String = objRow.Item("Beschikbaarheid").ToUpper()
                    '    Select Case strBeschikbaarheid
                    '        Case "HOOG"
                    '            objProces.AddConnector(objEntiteit, objBHoog, "ArchiMate_Association")
                    '            objDiagram = Me.AddElement2Diagram(objDiagram, objBHoog, 10, 650)
                    '        Case "MIDDEL"
                    '            objProces.AddConnector(objEntiteit, objBMiddel, "ArchiMate_Association")
                    '            objDiagram = Me.AddElement2Diagram(objDiagram, objBMiddel, 10, 650)
                    '        Case "LAAG"
                    '            objProces.AddConnector(objEntiteit, objBLaag, "ArchiMate_Association")
                    '            objDiagram = Me.AddElement2Diagram(objDiagram, objBLaag, 10, 650)
                    '    End Select
                    'End If

                    If Not IsDBNull(objRow.Item("Integriteit")) Then
                        Dim strBeschikbaarheid As String = objRow.Item("Integriteit").ToUpper()
                        Select Case strBeschikbaarheid
                            Case "HOOG"
                                objProces.AddConnector(objEntiteit, objIHoog, "ArchiMate_Association")
                                objDiagram = Me.AddElement2Diagram(objDiagram, objIHoog, 100, 650)
                            Case "MIDDEL"
                                objProces.AddConnector(objEntiteit, objIMiddel, "ArchiMate_Association")
                                objDiagram = Me.AddElement2Diagram(objDiagram, objIMiddel, 100, 650)
                            Case "LAAG"
                                objProces.AddConnector(objEntiteit, objILaag, "ArchiMate_Association")
                                objDiagram = Me.AddElement2Diagram(objDiagram, objILaag, 100, 650)
                        End Select
                    End If

                    If Not IsDBNull(objRow.Item("Vertrouwelijkheid")) Then
                        Dim strBeschikbaarheid As String = objRow.Item("Vertrouwelijkheid").ToUpper()
                        Select Case strBeschikbaarheid
                            Case "HOOG"
                                objProces.AddConnector(objEntiteit, objVHoog, "ArchiMate_Association")
                                objDiagram = Me.AddElement2Diagram(objDiagram, objVHoog, 200, 650)
                            Case "MIDDEL"
                                objProces.AddConnector(objEntiteit, objVMiddel, "ArchiMate_Association")
                                objDiagram = Me.AddElement2Diagram(objDiagram, objVMiddel, 200, 650)
                            Case "LAAG"
                                objProces.AddConnector(objEntiteit, objVLaag, "ArchiMate_Association")
                                objDiagram = Me.AddElement2Diagram(objDiagram, objVLaag, 200, 650)
                        End Select
                    End If

                    'If Not IsDBNull(objRow.Item("Privacyclassificatie")) Then
                    '    Dim objPrivacy As EA.Element
                    '    strNaam = "Privacyclassificatie " + objRow.Item("Privacyclassificatie").ToString()
                    '    blnFound = False
                    '    objPrivacy = objProces.FindOrAddElement(strNaam, "ArchiMate_Requirement", blnFound, "")
                    '    objProces.AddConnector(objEntiteit, objPrivacy, "ArchiMate_Association")
                    '    objDiagram = Me.AddElement2Diagram(objDiagram, objPrivacy, 300, 650)
                    'End If

                    If Not IsDBNull(objRow.Item("Type data")) Then
                        Dim objTypeData As EA.Element
                        strNaam = objRow.Item("Type data")

                        objTypeData = objProces.FindOrAddElement(strNaam, "ArchiMate_Requirement", blnFound)
                        objTypeData.Update()
                        objProces.AddConnector(objEntiteit, objTypeData, "ArchiMate_Association")
                        objDiagram = Me.AddElement2Diagram(objDiagram, objTypeData, 500, 350)
                    End If


                    If Not IsDBNull(objRow.Item("Leidende bron")) Then
                        Dim objBron As EA.Element
                        strNaam = objRow.Item("Leidende bron")
                        If strNaam.Length > 0 Then
                            objBron = objProces.FindOrAddElement(strNaam, "ArchiMate_ApplicationComponent", blnFound, "")
                            objProces.AddConnector(objEntiteit, objBron, "ArchiMate_Association", "Leidende bron")
                            objDiagram = Me.AddElement2Diagram(objDiagram, objBron, 500, 650)
                        End If
                    End If

                    If Not IsDBNull(objRow.Item("Bronnen")) Then
                        Dim objBron As EA.Element
                        Dim strBronnen As String()
                        Dim strTemp As String = ""
                        strTemp = objRow.Item("Bronnen")

                        If strTemp.Length > 0 Then
                            Dim strBron As String
                            Dim intPos As Int64 = 650
                            strBronnen = strTemp.Split(",")
                            For Each strBron In strBronnen
                                If strBron.Trim.Length > 0 Then
                                    objBron = objProces.FindOrAddElement(strBron, "ArchiMate_ApplicationComponent", blnFound, "")
                                    objProces.AddConnector(objEntiteit, objBron, "ArchiMate_Association", "")
                                    'objDiagram = Me.AddElement2Diagram(objDiagram, objBron, 600, intPos)
                                    intPos += 100
                                End If
                            Next
                        End If
                    End If

                    intTeller += 1
                    objEntiteit.Update()
                End If


                Dim objCluster As EA.Element
                blnFound = False
                If Not IsDBNull(objRow.Item("Datadomeincluster")) Then
                    strNaam = objRow.Item("Datadomeincluster") + " [Cluster]"
                    blnFound = False
                    objCluster = objProces.FindOrAddElement(strNaam, "ArchiMate_Grouping", blnFound)
                    Dim strId As String = ""
                    strId = "DC" + strId.PadRight(3 - intTeller.ToString().Length, "0") + intTeller.ToString()
                    objProces.AddOrUpdateTaggedValue(objCluster, "Id", strId, False)
                    objDiagram = Me.AddElement2Diagram(objDiagram, objCluster, 10, 400)

                    'Domein inlezen
                    Dim objDomein As EA.Element
                    If Not IsDBNull(objRow.Item("Datadomein")) Then
                        strNaam = objRow.Item("Datadomein") + " [Domein]"
                        blnFound = False
                        objDomein = objProces.FindOrAddElement(strNaam, "ArchiMate_Grouping", blnFound)
                        objProces.AddConnector(objCluster, objDomein, "ArchiMate_Aggregation")
                        objProces.AddConnector(objDomein, objEntiteit, "ArchiMate_Aggregation")
                        Dim strId2 As String = ""
                        strId2 = "DD" + strId2.PadRight(4 - intTeller.ToString().Length, "0") + intTeller.ToString()
                        objProces.AddOrUpdateTaggedValue(objDomein, "Id", strId2, False)

                        objDomein.Update()
                        objDiagram = Me.AddElement2Diagram(objDiagram, objDomein, 150, 400)

                        'Datadomein eigenaar toevoegen
                        'Deze worden gekoppeld aan het domein
                        If Not IsDBNull(objRow.Item("Domein eigenaar")) Then
                            Dim strEigenaar As String = objRow.Item("Domein eigenaar")
                            Dim objEigenaar As EA.Element
                            blnFound = False
                            objEigenaar = objProces.FindOrAddElement(strEigenaar, "ArchiMate_BusinessActor", blnFound, "")
                            'staat uit omdat ie nog niet in de excel voorkomt
                            'If Not IsDBNull(objRow.Item("Email_eigenaar")) Then
                            'objProces.AddOrUpdateTaggedValue(objEigenaar, "Email_eigenaar", objRow.Item("Email_eigenaar"), False)
                            'End If
                            objProces.AddConnector(objDomein, objEigenaar, "ArchiMate_Association")
                            objProces.AddConnector(objEigenaar, objEigenaarRol, "ArchiMate_Assignment")
                            'verwijzing aanmaken naar ARIS sleutel via een tagged value
                            objEigenaar.Update()
                            objDiagram = Me.AddElement2Diagram(objDiagram, objEigenaar, 100, 200)
                            objDiagram = Me.AddElement2Diagram(objDiagram, objEigenaarRol, 100, 10)
                        End If

                        'Data steward toevoegen
                        'Deze worden gekoppeld aan het domein
                        If Not IsDBNull(objRow.Item("Data steward")) Then
                            Dim strSteward As String = objRow.Item("Data steward")
                            Dim objSteward As EA.Element
                            blnFound = False
                            objSteward = objProces.FindOrAddElement(strSteward, "ArchiMate_BusinessActor", blnFound, "")
                            'staat uit omdat ie nog niet in de excel voorkomt
                            'If Not IsDBNull(objRow.Item("Email_steward")) Then
                            'objProces.AddOrUpdateTaggedValue(objSteward, "Email_steward", objRow.Item("Email_steward"), False)
                            'End If
                            objProces.AddConnector(objDomein, objSteward, "ArchiMate_Association")
                            objProces.AddConnector(objSteward, objStewardRol, "ArchiMate_Assignment")
                            'verwijzing aanmaken naar ARIS sleutel via een tagged value
                            objSteward.Update()
                            Try
                                objDiagram = Me.AddElement2Diagram(objDiagram, objSteward, 200, 200)
                                objDiagram = Me.AddElement2Diagram(objDiagram, objStewardRol, 200, 10)
                            Catch ex As Exception
                                DLA2EAHelper.Error2Log(ex)
                            End Try
                        End If
                    End If
                End If
                If Not IsNothing(objDiagram) Then
                    Me.Repository.SaveDiagram(objDiagram.DiagramID)
                End If
                Me.Repository.Execute("delete from t_objectproperties where property= 'actorkind'")

                        GC.Collect()
            Next
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub
    Public Function CreateDiagramFromElement(ByVal Element As EA.Element) As EA.Diagram
        Dim objPack As EA.Package
        objPack = Me.Repository.GetPackageByID(Element.PackageID)
        Dim objDiagram As EA.Diagram
        objDiagram = objPack.Diagrams.AddNew(Element.Name, "Logical")
        objDiagram.StyleEx = "MDGDgm=ArchiMate3::Business"
        objDiagram.Update()
        Me.Repository.OpenDiagram(objDiagram.DiagramID)
        objDiagram = Me.AddElement2Diagram(objDiagram, Element, 300, 400)
        Return objDiagram
    End Function
    Public Function AddElement2Diagram(ByVal diagram As EA.Diagram, ByVal element As EA.Element, ByVal top As Int32, ByVal left As Int32) As EA.Diagram
        Dim objDO As EA.DiagramObject
        Try
            Me.Repository.ReloadDiagram(diagram.DiagramID)
            objDO = diagram.DiagramObjects.AddNew("", "")
            objDO.ElementID = element.ElementID
            objDO.top = top * -1
            objDO.bottom = objDO.top - 75
            objDO.left = left
            objDO.right = objDO.left + 120
            objDO.ShowNotes = False
            objDO.Update()
            diagram.Update()
            Return diagram
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try

    End Function
    Public Shared Function MakeExcelConnectionString(ByVal Connection As String, ByVal File As String) As String
        Return Connection.Replace("[FILE]", File)
    End Function

    Public Shared Function LoadTableName(ByVal Connection As String) As String
        Dim objCon As OleDb.OleDbConnection
        Dim Table As String = ""
        Try

            objCon = New OleDb.OleDbConnection(Connection)
            objCon.Open()
            Dim DT As DataTable
            DT = objCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
            Table = "[" & DT.Rows(Convert.ToInt16("1")).Item("TABLE_NAME").ToString() & "]"
            objCon.Close()

        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
        Return Table
    End Function

    Public Shared Function File2DataTable(ByVal Connection As String) As DataTable
        Dim objCon As OleDb.OleDbConnection
        Dim objDT As New DataTable()
        Try

            Dim sSql As String = "SELECT * FROM " + LoadTableName(Connection)
            objCon = New OleDb.OleDbConnection(Connection)
            objCon.Open()

            Dim objDS As New DataSet
            Dim objDA As New OleDb.OleDbDataAdapter(sSql, objCon)
            objDA.Fill(objDS, "Objecten")
            objDT = objDS.Tables("Objecten")
            objCon.Close()
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
        Return objDT
    End Function
End Class


End Namespace