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
        ''' <summary>
        ''' Repository element for connection to the EA API
        ''' </summary>
        ''' <returns></returns>
        Public Property Repository() As EA.Repository
            Get
                Return _Repository
            End Get
            Set(ByVal value As EA.Repository)
                _Repository = value
            End Set
        End Property

        'Public Sub HollandCasino()
        '    'losse import repo
        '    Dim intTeller As Int16 = 0
        '    Dim blnFound As Boolean = False
        '    Dim objProces As New DataSet2Repository()
        '    objProces.Package_Id = Repository.GetCurrentDiagram().PackageID
        '    Me.Repository.EnableUIUpdates = False
        '    Try
        '        objProces.Repository = Me.Repository
        '        Dim oRow As DataRow
        '        For Each oRow In Me.DTable.Rows
        '            Dim objBron As EA.Element
        '            Dim objDoel As EA.Element
        '            Dim objTabel As EA.Element
        '            If Not IsDBNull(oRow("Bron")) And Not IsDBNull(oRow("Doel")) And Not IsDBNull(oRow("Tabel")) Then
        '                objBron = objProces.FindOrAddElement(oRow("Bron"), "ArchiMate_ApplicationComponent", blnFound)
        '                objDoel = objProces.FindOrAddElement(oRow("Doel"), "ArchiMate_ApplicationComponent", blnFound)
        '                blnFound = False
        '                objTabel = objProces.FindOrAddElement(oRow("Tabel"), "", blnFound)
        '                objProces.AddConnector(objTabel, objBron, "Trace")
        '                objProces.AddConnector(objTabel, objDoel, "Trace")
        '                Dim oAttrib As EA.Attribute
        '                Dim strFormaat As String
        '                If IsDBNull(oRow("Formaat")) Then
        '                    strFormaat = "?"
        '                Else
        '                    strFormaat = oRow("Formaat")
        '                End If
        '                Dim strOmschrijving As String
        '                If IsDBNull(oRow("Omschrijving")) Then
        '                    strOmschrijving = ""
        '                Else
        '                    strOmschrijving = oRow("Omschrijving")
        '                End If
        '                oAttrib = objProces.AddAttribute(objTabel, oRow("Veld"), strFormaat, strOmschrijving)
        '                If Not IsNothing(oAttrib) Then
        '                    oAttrib.Alias = strFormaat
        '                    If Not IsDBNull(oRow("Primary key")) Then
        '                        If oRow("Primary key") = "1" Then
        '                            oAttrib.IsID = True
        '                        End If
        '                    End If
        '                    If Not IsDBNull(oRow("Verplicht")) Then
        '                        If oRow("Verplicht") = "0" Then
        '                            oAttrib.LowerBound = "0"
        '                        End If
        '                    End If
        '                    Dim strVoorbeeld As String
        '                    If Not IsDBNull(oRow("Voorbeeld Data")) Then
        '                        strVoorbeeld = oRow("Voorbeeld Data")
        '                        Dim oAT As EA.AttributeTag
        '                        oAT = oAttrib.TaggedValues.AddNew("Voorbeeld", "")
        '                        oAT.Value = strVoorbeeld
        '                        oAT.Update()
        '                    End If
        '                    oAttrib.Update()
        '                End If
        '            End If
        '        Next
        '    Catch ex As Exception
        '        DLA2EAHelper.Error2Log(ex)
        '    End Try
        '    Me.Repository.EnableUIUpdates = True

        'End Sub
        ''' <summary>
        ''' Loading the LVNL Excel items
        ''' </summary>
        ''' <param name="oPB"></param>
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
        ''' <summary>
        ''' Load the tablename based on the excel connection based on the ordinal position
        ''' </summary>
        ''' <param name="Connection">Excel connection string</param>
        ''' <returns></returns>
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
    End Class
End Namespace