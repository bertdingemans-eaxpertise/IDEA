Imports System.Data.OleDb
Imports TEA.DLAFormfactory

''' <summary>
''' Import data from excel sheets with helper routines for to make advanced import
''' routines with associations and other entities
''' </summary>
Public Class FrmImportExcel
    Protected strTable As String
    Protected objRepo As EA.Repository
    Protected objDT As DataTable

    Public Sub SetRepository(ByVal oRep As EA.Repository)
        Me.objRepo = oRep
    End Sub
    Private Sub ButtonSelect_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonSelect.Click
        Me.OpenExcelFileDialog.Filter = "xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
        If OpenExcelFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Me.TextBoxExcelFile.Text = OpenExcelFileDialog.FileName
            Dim strConnect As String = Me.TextBoxConnection.Text
            Me.TextBoxConnection.Text = strConnect.Replace("[FILE]", Me.TextBoxExcelFile.Text)
            Me.ButtonLoad.Enabled = True
        End If
    End Sub

    Private Sub FrmImportExcel_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Me.OpenExcelFileDialog.FileName = ""
        Me.TextBoxConnection.Text = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=[FILE];Extended Properties=""Excel 12.0 Xml;HDR=YES"";"
    End Sub

    Private Sub LoadTableName()
        Dim objCon As OleDb.OleDbConnection

        Try
            objCon = New OleDb.OleDbConnection(Me.TextBoxConnection.Text)
            objCon.Open()

            Dim DT As DataTable
            DT = objCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
            Me.strTable = "[" & DT.Rows(Convert.ToInt16(Me.TextBoxTableNo.Text)).Item("TABLE_NAME").ToString() & "]"
            objCon.Close()
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub

    Private Sub LoadData(ByVal sSql As String)
        Dim objCon As OleDb.OleDbConnection

        Try
            objCon = New OleDb.OleDbConnection(Me.TextBoxConnection.Text)
            objCon.Open()
            Dim objDS As New DataSet
            Dim objDA As New OleDb.OleDbDataAdapter(sSql, objCon)
            objDA.Fill(objDS, "Objecten")
            Me.objDT = objDS.Tables("Objecten")
            DataGridViewExcel.DataSource = objDS
            DataGridViewExcel.DataMember = "Objecten"
            objCon.Close()
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub
    Private Sub LoadData()
        LoadData("SELECT * FROM " & strTable)
    End Sub

    'Private Sub ButtonEntiteiten_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonEntiteiten.Click
    '    Dim objProces As New TEADataSet2Repository()
    '    Dim objRow As DataRow
    '    objProces.Repository = Me.objRepo
    '    For Each objRow In Me.objDT.Rows
    '        Dim objElement As EA.Element
    '        If Not IsDBNull(objRow.Item("Entity")) Then
    '            Dim strDescr As String = ""
    '            If Not IsDBNull(objRow.Item("Documentation")) Then
    '                strDescr = objRow.Item("Documentation")
    '            End If

    '            If DLA2EAHelper.CheckUniqueElement(objRow.Item("Entity"), Me.objRepo) Then
    '                objElement = objProces.AddElement("", objRow.Item("Entity"), strDescr)
    '                objElement.Update()
    '                If Not IsDBNull(objRow.Item("Constraint")) Then
    '                    Dim objCon As EA.Constraint
    '                    objCon = objElement.Constraints.AddNew(objElement.Name & " Conditie", "Invariant")
    '                    objCon.Notes = objRow.Item("Constraint")
    '                    objCon.Update()
    '                End If
    '                objElement.Update()
    '            End If

    '        End If
    '    Next
    'End Sub

    'Private Sub ButtonAttributes_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonAttributes.Click
    '    Dim objProces As New TEADataSet2Repository()
    '    Dim objRow As DataRow
    '    Dim strFouten As String = ""
    '    Try
    '        objProces.Repository = Me.objRepo
    '        For Each objRow In Me.objDT.Rows
    '            Dim objElement As EA.Element
    '            If Not IsDBNull(objRow.Item("Entity")) Then
    '                If DLA2EAHelper.CheckUniqueElement(objRow.Item("Entity"), Me.objRepo) Then
    '                    objElement = objProces.AddElement("", objRow.Item("Entity"), "")
    '                    objElement.Update()
    '                Else
    '                    objElement = objProces.FindElement(objRow.Item("Entity"))
    '                End If
    '                Dim strDocum As String
    '                strDocum = ""
    '                If Not IsDBNull(objRow.Item("documentation")) Then
    '                    strDocum += objRow.Item("documentation")
    '                End If

    '                If Not IsNothing(objElement) And Not IsDBNull(objRow.Item("Attribute")) Then
    '                    Dim objAttribute As EA.Attribute
    '                    objAttribute = objElement.Attributes.AddNew(objRow.Item("Attribute"), "Variant")
    '                    objAttribute.Notes = strDocum
    '                    objAttribute.Visibility = "Private"
    '                    If Not IsDBNull(objRow.Item("attribute type")) Then
    '                        objAttribute.Type = objRow.Item("attribute type")
    '                    End If
    '                    If Not IsDBNull(objRow.Item("participation")) Then
    '                        If objRow.Item("participation") = "optional" Then
    '                            objAttribute.LowerBound = "0"
    '                        Else
    '                            objAttribute.LowerBound = "1"

    '                        End If
    '                        objAttribute.UpperBound = "1"
    '                    End If
    '                    objAttribute.Update()
    '                    Dim objCon As EA.AttributeConstraint
    '                    If Not IsDBNull(objRow.Item("constraint")) Then
    '                        objCon = objAttribute.Constraints.AddNew(objAttribute.Name & " Conditie", "Invariant")
    '                        objCon.AttributeID = objAttribute.AttributeID
    '                        objCon.Notes = objRow.Item("constraint")
    '                        objCon.Update()
    '                    End If
    '                    If Not IsDBNull(objRow.Item("enumeration")) Then
    '                        objCon = objAttribute.Constraints.AddNew(objAttribute.Name & " Enumeratie", "Invariant")
    '                        objCon.AttributeID = objAttribute.AttributeID
    '                        objCon.Notes = objRow.Item("enumeration")
    '                        objCon.Update()
    '                    End If
    '                    objAttribute.Update()
    '                    objElement.Update()
    '                    strDocum = ""
    '                End If
    '            End If
    '        Next
    '    Catch ex As Exception
    '        DLA2EAHelper.Error2Log(ex)
    '    End Try
    '    MsgBox(strFouten)
    'End Sub

    'Private Sub ButtonRelaties_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonAssociaties.Click
    '    Dim strFouten As String = ""
    '    Try
    '        Dim objProces As New TEADataSet2Repository()
    '        Dim objRow As DataRow

    '        objProces.Repository = Me.objRepo

    '        For Each objRow In Me.objDT.Rows
    '            Dim objElement As EA.Element
    '            Dim objLinkElement As EA.Element

    '            If Not IsDBNull(objRow.Item("Entity")) And Not IsDBNull(objRow.Item("Entity2")) Then
    '                If DLA2EAHelper.CheckUniqueElement(objRow.Item("Entity"), Me.objRepo) Then
    '                    objElement = objProces.AddElement("", objRow.Item("Entity"), "")
    '                    objElement.Update()
    '                Else
    '                    objElement = objProces.FindElement(objRow.Item("Entity"))
    '                End If

    '                If DLA2EAHelper.CheckUniqueElement(objRow.Item("Entity2"), Me.objRepo) Then
    '                    objLinkElement = objProces.AddElement("", objRow.Item("Entity2"), "")
    '                    objLinkElement.Update()
    '                Else
    '                    objLinkElement = objProces.FindElement(objRow.Item("Entity2"))
    '                End If
    '                Dim objConnector As EA.Connector
    '                objConnector = objProces.AddConnector(objElement, objLinkElement, "Association")
    '                If Not IsDBNull(objRow.Item("From role")) Then
    '                    objConnector.SupplierEnd.Role = objRow.Item("From role")
    '                End If
    '                If Not IsDBNull(objRow.Item("From role")) Then
    '                    objConnector.SupplierEnd.Role = objRow.Item("From role")
    '                End If
    '                If Not IsDBNull(objRow.Item("to role")) Then
    '                    objConnector.ClientEnd.Role = objRow.Item("to role")
    '                End If
    '                If Not IsDBNull(objRow.Item("Relation")) Then
    '                    objConnector.Name = objRow.Item("Relation")
    '                End If
    '                If Not IsDBNull(objRow.Item("from multiplicity")) And Not IsDBNull(objRow.Item("from participation")) Then
    '                    Dim strCardinality As String = objRow.Item("from multiplicity") & objRow.Item("from participation")
    '                    Select Case strCardinality
    '                        Case "manyoptional"
    '                            objConnector.SupplierEnd.Cardinality = "0..*"
    '                        Case "oneoptional"
    '                            objConnector.SupplierEnd.Cardinality = "0..1"
    '                        Case "manymandatory"
    '                            objConnector.SupplierEnd.Cardinality = "1..*"
    '                        Case Else
    '                            objConnector.SupplierEnd.Cardinality = "1"
    '                    End Select
    '                End If
    '                If Not IsDBNull(objRow.Item("to multiplicity")) And Not IsDBNull(objRow.Item("to participation")) Then
    '                    Dim strCardinality As String = objRow.Item("to multiplicity") & objRow.Item("to participation")
    '                    Select Case strCardinality
    '                        Case "manyoptional"
    '                            objConnector.ClientEnd.Cardinality = "0..*"
    '                        Case "oneoptional"
    '                            objConnector.ClientEnd.Cardinality = "0..1"
    '                        Case "manymandatory"
    '                            objConnector.ClientEnd.Cardinality = "1..*"
    '                        Case Else
    '                            objConnector.ClientEnd.Cardinality = "1"
    '                    End Select
    '                End If

    '                objConnector.Update()
    '            End If
    '        Next
    '    Catch ex As Exception
    '        DLA2EAHelper.Error2Log(ex)
    '    End Try
    'End Sub

    'Private Sub ButtonCleanLinkes_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonCleanLinkes.Click

    '    Dim oElement As EA.Element
    '    Dim oPack As EA.Package
    '    oPack = objRepo.GetTreeSelectedPackage()
    '    For Each oElement In oPack.Elements

    '        oElement.Notes = VerwerkLinks(oPack, oElement.Notes)
    '        Dim oAttribute As EA.Attribute
    '        For Each oAttribute In oElement.Attributes
    '            oAttribute.Notes = VerwerkLinks(oPack, oAttribute.Notes)
    '            Dim objCon As EA.AttributeConstraint
    '            For Each objCon In oAttribute.Constraints
    '                objCon.Notes = VerwerkLinks(oPack, objCon.Notes)
    '                objCon.Update()
    '            Next
    '            oAttribute.Update()
    '        Next
    '        oElement.Update()
    '    Next
    'End Sub

    Public Function VerwerkLinks(ByVal Package As EA.Package, ByVal tekst As String) As String

        tekst = tekst.Replace("0", "")
        tekst = tekst.Replace("1", "")
        tekst = tekst.Replace("2", "")
        tekst = tekst.Replace("3", "")
        tekst = tekst.Replace("4", "")
        tekst = tekst.Replace("5", "")
        tekst = tekst.Replace("6", "")
        tekst = tekst.Replace("7", "")
        tekst = tekst.Replace("8", "")
        tekst = tekst.Replace("9", "")
        tekst = tekst.Replace(">", "")

        If tekst.Contains("link:") Then
            Dim oKoppel As EA.Element
            For Each oKoppel In Package.Elements
                If oKoppel.Name.Length > 5 Then
                    tekst = tekst.Replace("<link: " & oKoppel.Name & " ", "<a href=""$element://" & oKoppel.ElementGUID & """ ><b>" & oKoppel.Name & "</b></a>")
                    'tekst = tekst.Replace(">", "")
                    tekst = tekst.Replace("|", "")
                End If
            Next
        End If
        Return tekst
    End Function

    Private Sub ButtonImportNora_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim objProces As New TEADataSet2Repository()
        Dim objRow As DataRow
        objProces.Repository = Me.objRepo

        Dim strNoraHandreiking As String = ""
        Try
            For Each objRow In Me.objDT.Rows
                Dim objElement As EA.Element

                If Not IsDBNull(objRow.Item("Criterianr")) Then
                    Dim strNr As String

                    strNr = "Digid " + objRow.Item("Norm2-0") + "/" + objRow.Item("Criterianr")

                    If DLA2EAHelper.CheckUniqueElement(strNr, "ArchiMate_Requirement", Me.objRepo) Then
                        Dim strNotes As String = ""
                        If Not IsDBNull(objRow.Item("Beschrijving_beveiligingsrichtlijn")) Then
                            strNotes = objRow.Item("Beschrijving_beveiligingsrichtlijn")
                        End If

                        objElement = objProces.AddElement("ArchiMate_Requirement", strNr, strNotes)
                        'objElement.Alias = objRow.Item("SubID")
                        objElement.Update()
                        objProces.AddTaggedValue(objElement, "Beheersdoelstelling", objRow.Item("Beheersdoelstelling"), True)
                        objProces.AddTaggedValue(objElement, "Auditee_evidence", objRow.Item("Auditee_evidence"), True)

                        If Not IsDBNull(objRow.Item("Toelichting_NORA_Handreiking")) Then
                            strNoraHandreiking = objRow.Item("Toelichting_NORA_Handreiking")
                        End If
                        objProces.AddTaggedValue(objElement, "Toelichting_NORA_Handreiking", strNoraHandreiking, True)
                        objProces.AddTaggedValue(objElement, "Opzet_bestaan?", objRow.Item("Opzet_bestaan?"), False)

                        objElement.Update()
                        Me.objRepo.GetTreeSelectedPackage().Update()
                    End If
                End If

            Next

        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub

    Private Sub ButtonImportBIO_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim objProces As New TEADataSet2Repository()
        Dim objRow As DataRow
        objProces.Repository = Me.objRepo

        Dim strNoraHandreiking As String = ""
        Try
            For Each objRow In Me.objDT.Rows
                Dim objElement As EA.Element
                Dim strNr As String
                If Not IsDBNull(objRow.Item("BIO_Controlnummer_overheid")) Then
                    strNr = "BIO " + objRow.Item("BIO_Controlnummer_ISO27002") + "/" + objRow.Item("BIO_Controlnummer_overheid")
                Else
                    strNr = "BIO " + objRow.Item("BIO_Controlnummer_ISO27002")
                End If
                If DLA2EAHelper.CheckUniqueElement(strNr, "ArchiMate_Requirement", Me.objRepo) Then
                    Dim strNotes As String = ""
                    If Not IsDBNull(objRow.Item("BIO_Controltekst_ISO27002")) Then
                        strNotes = objRow.Item("BIO_Controltekst_ISO27002")
                    End If

                    If Not IsDBNull(objRow.Item("BIO_Controltekst_Overheid")) Then
                        strNotes += vbCrLf & objRow.Item("BIO_Controltekst_Overheid")
                    End If
                    objElement = objProces.AddElement("ArchiMate_Requirement", strNr, strNotes)
                    objElement.Update()
                    objProces.AddTaggedValue(objElement, "BIO_Verantwoordelijke_Secretaris_algemeen_directeur", IIf(IsDBNull(objRow.Item("BIO_Verantwoordelijke_Secretaris_algemeen_directeur")), "", objRow.Item("BIO_Verantwoordelijke_Secretaris_algemeen_directeur")), False)
                    objProces.AddTaggedValue(objElement, "BIO_Verantwoordelijke_Proceseigenaar", IIf(IsDBNull(objRow.Item("BIO_Verantwoordelijke_Proceseigenaar")), "", objRow.Item("BIO_Verantwoordelijke_Proceseigenaar")), False)
                    objProces.AddTaggedValue(objElement, "BIO_BBN_1", IIf(IsDBNull(objRow.Item("BIO_BBN_1")), "", objRow.Item("BIO_BBN_1")), False)
                    objProces.AddTaggedValue(objElement, "BIO_BBN_2", IIf(IsDBNull(objRow.Item("BIO_BBN_2")), "", objRow.Item("BIO_BBN_2")), False)
                    objProces.AddTaggedValue(objElement, "BIO_BBN_3", IIf(IsDBNull(objRow.Item("BIO_BBN_3")), "", objRow.Item("BIO_BBN_3")), False)
                    objProces.AddTaggedValue(objElement, "BIO_Handreikingen", IIf(IsDBNull(objRow.Item("BIO_Handreikingen")), "", objRow.Item("BIO_Handreikingen")), False)
                    objProces.AddTaggedValue(objElement, "BIO_addendum_Maatregel", IIf(IsDBNull(objRow.Item("BIO_addendum_Maatregel")), "", objRow.Item("BIO_addendum_Maatregel")), False)
                    objProces.AddTaggedValue(objElement, "BIO_addendum_Organisatie", IIf(IsDBNull(objRow.Item("BIO_addendum_Organisatie")), "", objRow.Item("BIO_addendum_Organisatie")), False)
                    objProces.AddTaggedValue(objElement, "BIO_addendum_ BBN", IIf(IsDBNull(objRow.Item("BIO_addendum_ BBN")), "", objRow.Item("BIO_addendum_ BBN")), False)
                    objProces.AddTaggedValue(objElement, "BIO_addendum_Richtlijn", IIf(IsDBNull(objRow.Item("BIO_addendum_Richtlijn")), "", objRow.Item("BIO_addendum_Richtlijn")), False)
                    objProces.AddTaggedValue(objElement, "BIO_addendum_Verantwoordelijken", IIf(IsDBNull(objRow.Item("BIO_addendum_Verantwoordelijken")), "", objRow.Item("BIO_addendum_Verantwoordelijken")), False)

                    Me.objRepo.GetTreeSelectedPackage().Update()
                End If
            Next
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub

    Private Sub ButtonImportRequirements_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonImportRequirements.Click
        Dim objProces As New TEADataSet2Repository()
        Dim objRow As DataRow
        Dim objElement As EA.Element
        Dim objDomein As EA.Element

        For Each objRow In Me.objDT.Rows
            If Not IsDBNull(objRow.Item("Requirement")) Then
                Dim strType As String
                If Not IsDBNull(objRow.Item("Type")) Then
                    strType = objRow.Item("Type")

                    If DLA2EAHelper.CheckUniqueElement(objRow.Item("Requirement"), "ArchiMate_" & strType, Me.objRepo) Then
                        Dim strNotes As String = ""
                        If Not IsDBNull(objRow.Item("Opmerking")) Then
                            strNotes = objRow.Item("Opmerking")
                        End If

                        objElement = objProces.AddElement("ArchiMate_" & strType, objRow.Item("Requirement"), strNotes)
                        objElement.Alias = objRow.Item("SubID")
                        objElement.Update()
                        If Not IsDBNull(objRow.Item("MoSCoW")) Then
                            Dim objTV As EA.TaggedValue
                            objTV = objElement.TaggedValues.AddNew("MoSCoW", "")
                            objTV.Value = objRow.Item("MoSCoW")
                            objTV.Update()
                        End If
                        objElement.Update()
                    End If

                    If Not IsDBNull(objRow.Item("Domein")) Then
                        If DLA2EAHelper.CheckUniqueElement(objRow.Item("Domein"), "ArchiMate_ApplicationFunction", Me.objRepo) Then
                            objDomein = objProces.AddElement("ArchiMate_ApplicationFunction", objRow.Item("Domein"), "")
                            objDomein.Update()
                        Else
                            objDomein = objProces.FindElement(objRow.Item("Domein"), "ArchiMate_ApplicationFunction")
                        End If
                        Dim objConnector As EA.Connector
                        objConnector = objProces.AddConnector(objElement, objDomein, "Association")
                        objConnector.Update()
                    End If

                End If
            End If
        Next
    End Sub
    Private Sub ButtonKilMan_Click(sender As Object, e As EventArgs) Handles ButtonKilMan.Click
        Dim Excel2EA As New Excel2EAImport()
        Excel2EA.Repository = Me.objRepo
        Excel2EA.DTable = Me.objDT
        Excel2EA.LVNL(ProgressBar)
    End Sub

    Private Sub ButtonLoad_Click(sender As Object, e As EventArgs) Handles ButtonLoad.Click
        LoadTableName()
        LoadData()
    End Sub
End Class