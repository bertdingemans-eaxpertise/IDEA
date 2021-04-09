Namespace DLAFormfactory

    ''' <summary>
    ''' Class for generating source code for the forfactory ASP.Net application.
    ''' (Specific routines not further documented)
    ''' </summary>
    Public Class FormFactoryGenerator
    Protected InsertForm As String
    Protected DeleteForm As String
    Protected InsertControl As String
    Protected DeleteControl As String
    Protected InsertCodeList As String
    Protected DeleteCodeList As String
    Protected DeleteMenu As String
    Protected InsertMenu
    Private _Element As EA.Element
    Public Property Element() As EA.Element
        Get
            Return _Element
        End Get
        Set(ByVal value As EA.Element)
            _Element = value
        End Set
    End Property

    Private _Repository As EA.Repository
    Public Property Repository() As EA.Repository
        Get
            Return _Repository
        End Get
        Set(ByVal value As EA.Repository)
            _Repository = value
        End Set
    End Property

        Public Function Package2SimulatorDataset(Package As EA.Package, name As String) As SimulatorContainer
            Dim strPrimaryKey As String = ""
            Dim SimDataSet As New SimulatorContainer()
            Dim DT As DataTable
            Dim Row As DataRow
            Dim Element As EA.Element
            Dim Attribute As EA.Attribute
            Try
                SimDataSet.Repository = Me.Repository
                For Each Element In Package.Elements
                    Select Case Element.Type.ToUpper()
                        Case "CLASS"
                            If Element.Stereotype.Length = 0 And Element.Abstract = False Then
                                DT = SimDataSet.AddTable(Element.Name)
                                'aanmaken velden en controls voor table
                                SimDataSet.Inheritance2DataSet(Element, DT)
                                SimDataSet.Attributes2Dataset(Element, DT)
                                ' aanmaken primaire sleutels
                                SimDataSet.AddPrimaryKey(DT, Element.Name)
                                SimDataSet.AddControl(Element.Name, Element.Name + "ID", "HiddenSLE", "", True, Element.Name)
                                SimDataSet.AddConditions(Element, DT)
                                'commandos aanmaken
                            End If
                        Case "INTERFACE"
                            If Element.Name.ToUpper().Contains("LOOKUP") Then
                                Dim strLookup As String = ""
                                For Each Attribute In Element.Attributes
                                    If Not Attribute.IsID Then
                                        If strLookup.Length > 0 Then
                                            strLookup += " + ' ' + "
                                        End If
                                        strLookup += Attribute.Name.Replace(" ", "_")
                                    End If
                                Next
                                'Nog niet af datatable name moet anders
                                SimDataSet.AddCalculatedColumn(SimDataSet.ContainerDataSet.Tables(Element.Name.Replace("Lookup_", "")), "Lookup", Element.Name, "String", strLookup)
                            End If
                        Case "ENUMERATION"
                            If Element.Name.ToUpper().Contains("ENUM") Then
                                For Each Attribute In Element.Attributes
                                    SimDataSet.AddEnumeration(Element.Name, Attribute.Name)
                                Next
                            End If
                    End Select
                Next
                For Each Element In Package.Elements
                    SimDataSet.DefaultLookup(Element)
                    SimDataSet.Connectors2DataSet(Element, Element.PackageID)
                Next
                SimDataSet.DataTable2Command()
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
            Return SimDataSet
        End Function
        Public Shared Function AttributeType2Control(Attribute As EA.Attribute) As String
            Dim strRet As String = ""
            Select Case Attribute.Type.ToUpper()
                Case "STRING"
                    strRet = "SingleLineEdit"
                Case "MEMO"
                    strRet = "MultiLineEdit"
                Case "BOOLEAN"
                    strRet = "CheckBox"
                Case "INTEGER", "LONG"
                    strRet = "NumberSLE"
                Case "DATE", "DATETIME"
                    strRet = "CalendarSLE"
                Case Else
                    strRet = "Complex"
            End Select
            Return strRet
        End Function
        Public Shared Function Attribute2ControlType(Attribute As EA.Attribute) As String
            Dim strRet As String
            If Attribute.Visibility = "Private" Then
                strRet = "Hidden"
            Else
                Select Case Attribute.Type.ToUpper()
                    Case "STRING"
                        strRet = "SingleLineEdit"
                    Case "MEMO"
                        strRet = "MultiLineEdit"
                    Case "DATE", "DATETIME"
                        strRet = "CalendarSLE"
                    Case "LONG", "INTEGER", "DOUBLE", "DECIMAL"
                        strRet = "NumberSLE"
                    Case "BOOLEAN"
                        strRet = "CheckBox"
                    Case Else
                        strRet = "Combobox"
                End Select
            End If
            Return strRet
        End Function
        Public Sub SetSQLTemplates()
        Me.InsertForm = "INSERT INTO WEBFORM (webformid, formtitle, formtype, formhelptext) Select MAX(webformid) + 1, '@@elementname@@' as formtitle, 'Entry form' as formtype, '@@elementnotes@@' as formhelptext FROM WEBFORM"
        Me.DeleteForm = "DELETE FROM WEBFORM WHERE formtitle = '@@elementname@@' "
        Me.DeleteCodeList = "DELETE FROM WEBCODELIST WHERE section = '@@elementname@@'  "
        Me.InsertCodeList = "INSERT INTO WEBCODELIST (searchcode, description, section, blocked, languagecode) VALUES ('@@attributename@@', '@@attributename@@', '@@elementname@@', 0, 'NL' ) "
        Me.DeleteControl = "DELETE FROM WEBCONTROL WHERE webformid IN(SELECT webformid FROM WEBFORM WHERE formtitle = '@@elementname@@') "
        Me.InsertControl = "INSERT INTO WEBCONTROL ( [controlname] , [controltype] , [controlsql] , [webformid], [controllabel] , [controlwidth] , [controlheight] , [controlrequired] , [controlorder] , [weblevelid] , [colspan] , [languagecode] , [controlconnection]) " +
            "SELECT '@@attributename@@', '@@controltype@@', '@@controlsql@@', webformid , '@@attributename@@', @@controlwidth@@ , @@controlheight@@, @@controlrequired@@, @@controlorder@@, @@weblevelid@@, 1, '@@languagecode@@', 'connectionstring' FROM WEBFORM WHERE formtitle='@@elementname@@' "
        Me.DeleteMenu = "DELETE FROM WEBMENU WHERE menu_caption = '@@menuname@@' and menu_name = '@@parentmenu@@' "
        Me.InsertMenu = "INSERT INTO WEBMENU (menu_caption, menu_name, linkname, weblevelid, status) VALUES ('@@menuname@@', '@@parentmenu@@', 'frmCombi.aspx?webformid=@@menuname@@', 1, 'EA gen' ) "

    End Sub
    Public Sub GenerateMenuSQL(ByVal menu As Boolean, ByVal parentname As String)
        Dim strSql As String

        If menu = True Then
            'eerst deleten van afhankelijkheden dan van de master zelf
            strSql = Me.DeleteMenu.Replace("@@menuname@@", Me.Element.Name).Replace("@@parentmenu@@", parentname)
            DLA2EAHelper.ExecuteModifySQL(strSql)
            Repository.WriteOutput("IDEA", strSql, 0)
            strSql = Me.InsertMenu.Replace("@@menuname@@", Me.Element.Name).Replace("@@parentmenu@@", parentname)
            DLA2EAHelper.ExecuteModifySQL(strSql)
            Repository.WriteOutput("IDEA", strSql, 0)
        End If

    End Sub

        Public Sub GenerateSQL(ByVal delete As Boolean, ByVal insert As Boolean, ByVal searchcommand As Boolean)
        Dim strSql As String
        Dim strSelect As String = ""

        Me.SetSQLTemplates()
        Select Case Element.Type.ToUpper()
            Case "CLASS", "INTERFACE"
                If delete = True Then
                    'eerst deleten van afhankelijkheden dan van de master zelf
                    strSql = Me.DeleteControl.Replace("@@elementname@@", Me.Element.Name).Replace("@@elementnotes@@", Me.Element.Notes)
                    DLA2EAHelper.ExecuteModifySQL(strSql)
                    strSql = Me.DeleteForm.Replace("@@elementname@@", Me.Element.Name).Replace("@@elementnotes@@", Me.Element.Notes)
                    DLA2EAHelper.ExecuteModifySQL(strSql)
                End If
                If insert = True Then
                    strSql = Me.InsertForm.Replace("@@elementname@@", Me.Element.Name).Replace("@@elementnotes@@", Me.Element.Notes)
                    DLA2EAHelper.ExecuteModifySQL(strSql)
                    Dim Attribute As EA.Attribute
                    Dim ControlOrder As Int16 = 100
                    For Each Attribute In Me.Element.Attributes
                        Dim strControlType As String
                        strControlType = FormFactoryGenerator.Attribute2ControlType(Attribute)
                        strSql = Me.InsertControl.Replace("@@elementname@@", Me.Element.Name)
                        strSelect += IIf(strSelect.Length > 0, ", ", "") + Attribute.Name
                        strSql = strSql.Replace("@@elementnotes@@", Me.Element.Notes)
                        strSql = strSql.Replace("@@attributename@@", Attribute.Name)
                        strSql = strSql.Replace("@@controltype@@", strControlType)
                        Select Case strControlType
                            Case "Hidden"
                                strSql = strSql.Replace("@@controlwidth@@", "0").Replace("@@controlheight@@", "0").Replace("@@controlsql@@", "NULL")
                            Case "SingleLineEdit"
                                strSql = strSql.Replace("@@controlwidth@@", "400").Replace("@@controlheight@@", "25").Replace("@@controlsql@@", "NULL")
                            Case "MultiLineEdit"
                                strSql = strSql.Replace("@@controlwidth@@", "400").Replace("@@controlheight@@", "100").Replace("@@controlsql@@", "NULL")
                            Case "NumberSLE", "CalendarSLE"
                                strSql = strSql.Replace("@@controlwidth@@", "200").Replace("@@controlheight@@", "25").Replace("@@controlsql@@", "NULL")
                            Case "CheckBox"
                                strSql = strSql.Replace("@@controlwidth@@", "25").Replace("@@controlheight@@", "25").Replace("@@controlsql@@", "NULL")
                            Case "ComboBox"
                                strSql = strSql.Replace("@@controlwidth@@", "400").Replace("@@controlheight@@", "25")
                                Dim strElementId As String
                                strElementId = SearchTypeElement(Attribute.Type)
                                If strElementId = "-999" Then
                                        MsgBox("Error in SQL definition for the attribute " + Attribute.Name)
                                    Else
                                    strSql = strSql.Replace("@@controlsql@@", CreateSelectStatement(strElementId))
                                End If
                        End Select
                        strSql = strSql.Replace("@@controlrequired@@", Attribute.LowerBound)
                        strSql = strSql.Replace("@@controlorder@@", ControlOrder)
                        strSql = strSql.Replace("@@weblevelid@@", 1)
                        strSql = strSql.Replace("@@languagecode@@", "NL")
                        Repository.WriteOutput("IDEA", strSql, 0)
                        ControlOrder += 10
                        DLA2EAHelper.ExecuteModifySQL(strSql)
                    Next
                    'aanmaken command controls
                    'select command
                    strSql = Me.InsertControl.Replace("@@elementname@@", Me.Element.Name)
                    strSql = strSql.Replace("@@elementnotes@@", Me.Element.Notes)
                    strSql = strSql.Replace("@@attributename@@", "SelectCommand")
                    strSql = strSql.Replace("@@controltype@@", "SelectCommand")
                    strSql = strSql.Replace("@@controlwidth@@", "NULL")
                    strSql = strSql.Replace("@@controlheight@@", "NULL")
                    strSql = strSql.Replace("@@controlsql@@", "SELECT " + strSelect + " FROM " + Element.Name + " WHERE " + Me.WhereStatement(Element, False))
                    strSql = strSql.Replace("@@controlrequired@@", "0")
                    strSql = strSql.Replace("@@controlorder@@", "0")
                    strSql = strSql.Replace("@@weblevelid@@", 1)
                    strSql = strSql.Replace("@@languagecode@@", "NL")
                    DLA2EAHelper.ExecuteModifySQL(strSql)
                    If searchcommand = True Then
                        'search command
                        strSql = Me.InsertControl.Replace("@@elementname@@", Me.Element.Name)
                        strSql = strSql.Replace("@@elementnotes@@", Me.Element.Notes)
                        strSql = strSql.Replace("@@attributename@@", "SearchCommand")
                        strSql = strSql.Replace("@@controltype@@", "SearchCommand")
                        strSql = strSql.Replace("@@controlwidth@@", "NULL")
                        strSql = strSql.Replace("@@controlheight@@", "NULL")
                        strSql = strSql.Replace("@@controlsql@@", Me.SearchStatement(Element))
                        strSql = strSql.Replace("@@controlrequired@@", "0")
                        strSql = strSql.Replace("@@controlorder@@", "0")
                        strSql = strSql.Replace("@@weblevelid@@", 1)
                        strSql = strSql.Replace("@@languagecode@@", "NL")
                        DLA2EAHelper.ExecuteModifySQL(strSql)
                    End If
                    'delete command
                    strSql = Me.InsertControl.Replace("@@elementname@@", Me.Element.Name)
                    strSql = strSql.Replace("@@elementnotes@@", Me.Element.Notes)
                    strSql = strSql.Replace("@@attributename@@", "DeleteCommand")
                    strSql = strSql.Replace("@@controltype@@", "DeleteCommand")
                    strSql = strSql.Replace("@@controlwidth@@", "NULL")
                    strSql = strSql.Replace("@@controlheight@@", "NULL")
                    strSql = strSql.Replace("@@controlsql@@", "DELETE FROM " & Element.Name & " WHERE " & Me.WhereStatement(Element, False))
                    strSql = strSql.Replace("@@controlrequired@@", "0")
                    strSql = strSql.Replace("@@controlorder@@", "0")
                    strSql = strSql.Replace("@@weblevelid@@", 1)
                    strSql = strSql.Replace("@@languagecode@@", "NL")

                    DLA2EAHelper.ExecuteModifySQL(strSql)
                    'insert
                    strSql = Me.InsertControl.Replace("@@elementname@@", Me.Element.Name)
                    strSql = Me.InsertControl.Replace("@@elementname@@", Me.Element.Name)
                    strSql = strSql.Replace("@@elementnotes@@", Me.Element.Notes)
                    strSql = strSql.Replace("@@attributename@@", "InsertCommand")
                    strSql = strSql.Replace("@@controltype@@", "InsertCommand")
                    strSql = strSql.Replace("@@controlwidth@@", "NULL")
                    strSql = strSql.Replace("@@controlheight@@", "NULL")
                    strSql = strSql.Replace("@@controlsql@@", InsertStatement(Element))
                    strSql = strSql.Replace("@@controlrequired@@", "0")
                    strSql = strSql.Replace("@@controlorder@@", "0")
                    strSql = strSql.Replace("@@weblevelid@@", 1)
                    strSql = strSql.Replace("@@languagecode@@", "NL")
                    DLA2EAHelper.ExecuteModifySQL(strSql)
                    'update
                    strSql = Me.InsertControl.Replace("@@elementname@@", Me.Element.Name)
                    strSql = Me.InsertControl.Replace("@@elementname@@", Me.Element.Name)
                    strSql = strSql.Replace("@@elementnotes@@", Me.Element.Notes)
                    strSql = strSql.Replace("@@attributename@@", "UpdateCommand")
                    strSql = strSql.Replace("@@controltype@@", "UpdateCommand")
                    strSql = strSql.Replace("@@controlwidth@@", "NULL")
                    strSql = strSql.Replace("@@controlheight@@", "NULL")
                    strSql = strSql.Replace("@@controlsql@@", UpdateStatement(Element))
                    strSql = strSql.Replace("@@controlrequired@@", "0")
                    strSql = strSql.Replace("@@controlorder@@", "0")
                    strSql = strSql.Replace("@@weblevelid@@", 1)
                    strSql = strSql.Replace("@@languagecode@@", "NL")
                    DLA2EAHelper.ExecuteModifySQL(strSql)
                End If
            Case "ENUMERATION"
                If delete = True Then
                    strSql = Me.DeleteCodeList.Replace("@@elementname@@", Me.Element.Name)
                    DLA2EAHelper.ExecuteModifySQL(strSql)
                End If
                If insert = True Then
                    Dim Attribute As EA.Attribute
                    For Each Attribute In Me.Element.Attributes
                        strSql = Me.InsertCodeList.Replace("@@elementname@@", Me.Element.Name).Replace("@@elementnotes@@", Me.Element.Notes).Replace("@@attributename@@", Attribute.Name)
                        DLA2EAHelper.ExecuteModifySQL(strSql)
                    Next
                End If

        End Select
    End Sub

    Public Function WhereStatement(ByVal Element As EA.Element, ByVal IsName As Boolean) As String
        Dim strWhere As String = ""

            strWhere += Element.Name & " = @@" & Element.Name & "ID" & "@@"
            Return strWhere
    End Function
    Public Function SearchStatement(ByVal Element As EA.Element) As String
        Dim strSearch As String = ""
        Dim strFields As String = ""
        Dim strKeys As String = ""
        Dim strWhere As String = ""
        Dim arrNamen As String() = {"NAAM", "NAME", "TITEL", "TITLE", "ONDERWERP"}
        Dim blnName As Boolean = False

        Dim Attribute As EA.Attribute
        For Each Attribute In Element.Attributes
            If Attribute.IsID Then
                strKeys += Attribute.Name
            End If
            For Each strNaam As String In arrNamen
                If Attribute.Name.ToUpper.Contains(strNaam) Then
                    If strFields.Length > 0 Then
                        strFields += " + "
                    End If
                        strFields += Attribute.Name.ToLower()
                        Exit For
                End If
            Next
        Next
        strSearch = "SELECT " + strKeys + " as id, " + strFields + " as display FROM " + Element.Name + " ORDER BY 2 "

        Return strSearch
    End Function


    Public Function InsertStatement(ByVal Element As EA.Element) As String
        Dim strInsert As String = ""
        Dim strFields As String = ""
        Dim strValues As String = ""

        Dim Attribute As EA.Attribute
        For Each Attribute In Element.Attributes
            If Not Attribute.IsID Then
                If strFields.Length > 0 Then
                    strFields += ", "
                    strValues += ", "
                End If
                    strFields += Attribute.Name.ToLower()
                    strValues += AttributeParameter(Attribute)
            End If
        Next
        strInsert = "INSERT INTO " + Element.Name + "( " + strFields + " ) VALUES ( " + strValues + " )"

        Return strInsert
    End Function

    Private Shared Function AttributeParameter(ByVal Attribute As EA.Attribute) As String
        Dim strParameter As String
        If Attribute.Type.ToUpper().Contains("LOOKUP") Then
                strParameter = "@@" + Attribute.Name.ToLower() + "@@"
            Else
            Select Case Attribute.Type.ToUpper()
                Case "INTEGER", "BOOLEAN", "DECIMAL"
                        strParameter = "@@" + Attribute.Name.ToLower() + "@@"
                    Case Else
                        strParameter = "''@@" + Attribute.Name.ToLower() + "@@''"
                End Select
        End If
        Return strParameter
    End Function

    Public Function SearchTypeElement(ByVal type As String) As String
        Dim strElementId As String = "-999"
        Dim strSql As String
        Try
            strSql = String.Format("SELECT object_id FROM T_OBJECT WHERE stereotype IS NULL AND name = '{0}' ", type)
                Dim objDT As DataTable
                objDT = DLA2EAHelper.SQL2DataTable(strSql, Me.Repository)
                If objDT.Rows.Count > 0 Then
                    strElementId = objDT.Rows(0).Item("object_id").ToString()
                End If
            Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
        Return strElementId
    End Function
        Public Function UpdateStatement(ByVal Element As EA.Element) As String
            Dim strUpdate As String = ""
            Dim strFields As String = ""

            Dim Attribute As EA.Attribute
            For Each Attribute In Element.Attributes
                If Not Attribute.IsID Then
                    If strFields.Length > 0 Then
                        strFields += ", "
                    End If
                    strFields += Attribute.Name.ToLower() + " = "
                    strFields += AttributeParameter(Attribute)
                End If
            Next
            strUpdate = "UPDATE " + Element.Name + " SET " + strFields + " WHERE " + WhereStatement(Element, True)

            Return strUpdate
        End Function

        Private Function CreateSelectStatement(ByVal id As String) As String
        Dim strSql As String = "NULL"
        Dim Element As EA.Element
        Element = Repository.GetElementByID(id)
        Select Case Element.Type.ToUpper
            Case "ENUMERATION"
                strSql = String.Format("SELECT searchcode, description FROM WEBCODELIST WHERE section = ""{0}"" ", Element.Name)
            Case "INTERFACE"
                strSql = String.Format("SELECT @@value@@, @@display@@ FROM {0} ORDER BY 2 ", Element.Name.ToUpper.Replace("LOOKUP_", ""))
                Dim strValue As String = ""
                Dim strDisplay As String = ""
                Dim objAttr As EA.Attribute
                For Each objAttr In Element.Attributes
                    If objAttr.IsID Then
                        strValue += IIf(strValue.Length > 0, ", ", "")
                        strValue += objAttr.Name
                    Else
                        strDisplay += IIf(strDisplay.Length > 0, ", ", "")
                        strDisplay += objAttr.Name
                    End If
                Next
                strSql = strSql.Replace("@@value@@", strValue)
                strSql = strSql.Replace("@@display@@", strDisplay)

        End Select
        Return strSql
    End Function
End Class

End Namespace