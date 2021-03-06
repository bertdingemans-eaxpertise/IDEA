Imports System.Data.SqlClient
Imports System.Data.OleDb
Namespace DLAFormfactory
    ''' <summary>
    ''' Dataset container for the IDEA simulator
    ''' </summary>
    Public Class DLADataSetContainer
        Protected objDS As DataSet
        Public Repository As EA.Repository
        ''' <summary>
        ''' Get a datatable from the dataset by name
        ''' </summary>
        ''' <param name="Name">Name of the datatable to retrieve</param>
        ''' <returns></returns>
        Public Function GetDataTable(Name As String) As DataTable
            Return objDS.Tables(Name)
        End Function
        ''' <summary>
        ''' Get the dataset with the simulator elements
        ''' </summary>
        ''' <returns>Dataset</returns>
        Public ReadOnly Property ContainerDataSet() As DataSet
            Get
                Return objDS
            End Get
        End Property
        ''' <summary>
        ''' Instantiate the simulator dataset container
        ''' </summary>
        ''' <param name="Name"></param>
        Public Sub New(Name As String)
            Me.objDS = New DataSet(Name)
            Me.CreateControlTable()
            Me.CreateCommandTable()
            Me.CreateEnumerationTable()
        End Sub
        ''' <summary>
        ''' Add an empty datatable to the dataset
        ''' </summary>
        ''' <param name="Name">Name of the new table</param>
        ''' <returns></returns>
        Public Function AddTable(Name As String) As DataTable
            Dim objDT As DataTable

            objDT = Me.objDS.Tables.Add(Name)
            Return objDT

        End Function
        ''' <summary>
        ''' Create a control table and define the columns
        ''' Control table is relevant for the simulator user interface
        ''' </summary>
        ''' <returns></returns>
        Private Function CreateControlTable() As Boolean
            Dim DT As DataTable
            Try
                DT = Me.AddTable("SYS_CONTROLS")
                Me.AddColumn(DT, "tablename", "Table name", "String")
                Me.AddColumn(DT, "controlname", "Control name", "String")
                Me.AddColumn(DT, "controltype", "Control type", "String")
                Me.AddColumn(DT, "controlrequired", "ControlRequired", "Boolean")
                Me.AddColumn(DT, "controlcaption", "Control caption", "String")
                Me.AddColumn(DT, "controllookup", "Control lookup", "String")
                Return True
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
                Return False
            End Try

        End Function
        ''' <summary>
        ''' Transform the datatables in the tables collection to sql statments for insert update etc
        ''' </summary>
        ''' <returns></returns>
        Public Function DataTable2Command() As Boolean
            Try
                For Each Table As DataTable In Me.objDS.Tables
                    Me.AddCommand(Table.TableName, "INSERT", DLADataSetContainer.DataTable2Insert(Table))
                    Me.AddCommand(Table.TableName, "UPDATE", DLADataSetContainer.DataTable2Update(Table))
                    Me.AddCommand(Table.TableName, "DELETE", DLADataSetContainer.DataTable2Delete(Table))
                    Me.AddCommand(Table.TableName, "DETAIL", DLADataSetContainer.DataTable2Detail(Table))
                    Me.AddCommand(Table.TableName, "LIST", DLADataSetContainer.DataTable2List(Table))
                Next
                Return True
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
                Return False
            End Try
        End Function
        ''' <summary>
        ''' Create the structure of the sys_command datatable   
        ''' </summary>
        ''' <returns></returns>
        Private Function CreateCommandTable() As Boolean
            Dim DT As DataTable
            Try
                DT = Me.AddTable("SYS_COMMAND")
                Me.AddColumn(DT, "tablename", "Table name", "String")
                Me.AddColumn(DT, "commandname", "Command name", "String")
                Me.AddColumn(DT, "statement", "Statement", "Memo")
                Return True
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
                Return False
            End Try

        End Function
        ''' <summary>
        ''' Add a command to the command systemtable
        ''' </summary>
        ''' <param name="Tablename">Name of the datatable</param>
        ''' <param name="Commandname">Name of the command (insert/update etc</param>
        ''' <param name="Statement">SQL statement</param>
        ''' <returns></returns>
        Public Function AddCommand(Tablename As String, Commandname As String, Statement As String) As Boolean
            Dim oRow As DataRow
            Try
                oRow = Me.objDS.Tables("SYS_COMMAND").NewRow()
                oRow("tablename") = Tablename
                oRow("commandname") = Commandname
                oRow("statement") = Statement
                Me.objDS.Tables("SYS_COMMAND").Rows.Add(oRow)
                Return True
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
                Return False
            End Try
        End Function
        ''' <summary>
        ''' Transform a columnname to a default parameter template
        ''' </summary>
        ''' <param name="Column">Datacolumn</param>
        ''' <returns>Parameter template</returns>
        Private Shared Function Column2ParaMeter(Column As DataColumn) As String
            Dim strParameter As String
            Dim DataType As String = Column.DataType.ToString().ToUpper()
            Select Case DataType
                Case "SYSTEM.STRING"
                    strParameter = "''@@" + Column.ColumnName.ToLower() + "@@''"
                Case Else
                    strParameter = "@@" + Column.ColumnName.ToLower() + "@@"
            End Select
            Return strParameter
        End Function
        ''' <summary>
        ''' Transform a datatable to an insert statement
        ''' </summary>
        ''' <param name="Table"></param>
        ''' <returns>Insert statement</returns>
        Public Shared Function DataTable2Insert(Table As DataTable) As String
            Dim Template As String = "INSERT INTO #tablename# (#fieldlist#) VALUES (#valuelist#) "
            Template = Template.Replace("#tablename#", Table.TableName)
            Dim strColumns As String = "", strValues As String = ""
            For Each Column As DataColumn In Table.Columns
                If Column.Expression.Length = 0 And Column.AutoIncrement = False Then
                    strColumns = strColumns + IIf(strColumns.Length = 0, "", ", ")
                    strColumns = strColumns + Column.ColumnName.ToLower()
                    strValues = strValues + IIf(strValues.Length = 0, "", ", ")
                    strValues = strValues + Column2ParaMeter(Column)
                End If
            Next
            Template = Template.Replace("#fieldlist#", strColumns)
            Template = Template.Replace("#valuelist#", strValues)
            Return Template
        End Function
        ''' <summary>
        ''' Transform a datatable to an delete statement
        ''' </summary>
        ''' <param name="Table"></param>
        ''' <returns>Delete statement</returns>
        Public Shared Function DataTable2Delete(Table As DataTable) As String
            Dim Template As String = "DELETE FROM #tablename# #where# "
            Template = Template.Replace("#tablename#", Table.TableName)
            Template = Template.Replace("#where#", Table2Where(Table))
            Return Template
        End Function
        ''' <summary>
        ''' Transform a datatable to an update statement
        ''' </summary>
        ''' <param name="Table"></param>
        ''' <returns>Update statement</returns>
        Public Shared Function DataTable2Update(Table As DataTable) As String
            Dim Template As String = "UPDATE #tablename# SET #fieldlist# #where# "
            Template = Template.Replace("#tablename#", Table.TableName)
            Dim strColumns As String = ""
            For Each Column As DataColumn In Table.Columns
                If Column.Expression.Length = 0 And Column.AutoIncrement = False Then
                    strColumns = strColumns + IIf(strColumns.Length = 0, "", ", ")
                    strColumns = strColumns + Column.ColumnName.ToLower() + " = " + Column2ParaMeter(Column)
                End If
            Next
            Template = Template.Replace("#fieldlist#", strColumns)
            Template = Template.Replace("#where#", Table2Where(Table))
            Return Template
        End Function
        ''' <summary>
        ''' Transform a datatable to an one row select statement
        ''' </summary>
        ''' <param name="Table"></param>
        ''' <returns>Select statement</returns>
        Public Shared Function DataTable2Detail(Table As DataTable) As String
            Dim Template As String = "SELECT * FROM #tablename# #where# "
            Template = Template.Replace("#tablename#", Table.TableName)
            'Dim strColumns As String = ""
            'For Each Column As DataColumn In Table.Columns
            '    If Column.Expression.Length = 0 And Column.AutoIncrement = False Then
            '        strColumns = strColumns + IIf(strColumns.Length = 0, "", ", ")
            '        strColumns = strColumns + Column.ColumnName.ToLower() + " = " + Column2ParaMeter(Column)
            '    End If
            'Next
            'Template = Template.Replace("#fieldlist#", strColumns)
            Template = Template.Replace("#where#", Table2Where(Table))
            Return Template
        End Function
        ''' <summary>
        ''' Transform a datatable to a search select statement
        ''' </summary>
        ''' <param name="Table">Table name</param>
        ''' <returns>Select </returns>
        Public Shared Function DataTable2List(Table As DataTable) As String
            Dim Template As String = "SELECT #fieldlist# FROM #tablename# "
            Template = Template.Replace("#tablename#", Table.TableName)
            Dim strColumns As String = ""
            For Each Column As DataColumn In Table.Columns
                If Column.Expression.Length = 0 Then
                    strColumns = strColumns +
                        IIf(strColumns.Length = 0, "", ", ") +
                        Column.ColumnName.ToLower()
                End If
            Next
            Template = Template.Replace("#fieldlist#", strColumns)
            'Template = Template.Replace("#where#", Table2Where(Table))
            Return Template
        End Function
        ''' <summary>
        ''' create a where statement for a datatable
        ''' </summary>
        ''' <param name="Table"></param>
        ''' <returns></returns>
        Shared Function Table2Where(Table As DataTable) As String
            Dim Template As String = "WHERE #keylist#"
            Dim keys As String = ""
            For Each Key As DataColumn In Table.PrimaryKey
                keys += IIf(keys.Length > 0, " And ", "") + Key.ColumnName + " = " + Column2ParaMeter(Key)
            Next
            Return Template.Replace("#keylist#", keys)
        End Function
        ''' <summary>
        ''' transform an enumeration to the content of the enumeration system table
        ''' </summary>
        ''' <returns></returns>
        Private Function CreateEnumerationTable() As Boolean
            Dim DT As DataTable
            Try
                DT = Me.AddTable("SYS_ENUMERATION")
                Me.AddColumn(DT, "enumname", "Enumeration name", "String")
                Me.AddColumn(DT, "enumvalue", "Enumeration value", "String")
                Return True
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
                Return False
            End Try

        End Function
        ''' <summary>
        ''' create a view with a list of controls for a table
        ''' </summary>
        ''' <param name="tablename"></param>
        ''' <returns></returns>
        Public Function GetControlsForTable(tablename As String) As DataView
            Dim oDV As DataView
            oDV = New DataView(Me.objDS.Tables("SYS_CONTROLS"))
            oDV.RowFilter = String.Format(" tablename='{0}'", tablename)
            Return oDV
        End Function
        ''' <summary>
        ''' Get an enumeration from the enumeration system table
        ''' </summary>
        ''' <param name="enumname"></param>
        ''' <returns></returns>
        Public Function GetEnumeration(enumname As String) As DataTable
            Dim oDV As DataView
            Try
                oDV = New DataView(Me.objDS.Tables("SYS_ENUMERATION"))
                oDV.RowFilter = String.Format(" enumname='{0}'", enumname)
                Return oDV.ToTable
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
            Return Nothing
        End Function
        ''' <summary>
        ''' Get a commands from the system table with commands
        ''' </summary>
        ''' <param name="tablename">name of the table</param>
        ''' <param name="commandname"> name of the command</param>
        ''' <returns></returns>
        Public Function GetCommandForTable(tablename As String, commandname As String) As DataTable
            Dim oDV As DataView
            oDV = New DataView(Me.objDS.Tables("SYS_COMMAND"))
            oDV.RowFilter = String.Format(" tablename='{0}' And commandname='{1}' ", tablename, commandname)
            Return oDV.ToTable()
        End Function
        ''' <summary>
        ''' Get an empty dataview for a table
        ''' </summary>
        ''' <param name="Name"></param>
        ''' <returns></returns>
        Public Function GetDataViewFromTable(Name As String) As DataView
            Try
                Return New DataView(Me.objDS.Tables(Name))
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
            Return Nothing
        End Function
        ''' <summary>
        ''' Load the tables from a sql data source
        ''' </summary>
        Public Sub LoadDataFromSQL()
            Dim objDB As DLAFormfactory.DLADatabase
            Try
                Me.objDS.EnforceConstraints = False
                objDB = New DLAFormfactory.DLADatabase()
                objDB.OpenConnection(False)
                Dim Table As DataTable
                For Each Table In Me.objDS.Tables
                    If Not Table.TableName.Contains("SYS_") Then
                        Table.Rows.Clear()
                        Dim strSql As String = DLADataSetContainer.DataTable2List(Table)
                        objDB.SQL2DataTable(strSql, Table)
                    End If
                Next
                objDB.CloseConnection(True)
                Me.objDS.EnforceConstraints = True
            Catch ex As Exception
                MsgBox(ex.ToString(), MsgBoxStyle.OkOnly)
                DLA2EAHelper.Error2Log(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Load the datatable content from a XML file
        ''' </summary>
        Public Sub LoadDataFromXML()
            Try
                Me.objDS.EnforceConstraints = False
                For Each Table In Me.objDS.Tables
                    If Not Table.TableName.Contains("SYS_") Then
                        Table.Rows.Clear()
                    End If
                Next
                If IO.File.Exists(My.Settings.XMLFile) Then
                    Me.objDS.ReadXml(My.Settings.XMLFile)
                End If
                Me.objDS.EnforceConstraints = True
            Catch ex As Exception
                DLA2EAHelper.DebugAssertion(ex.ToString())
                DLA2EAHelper.Error2Log(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Save the dataset to a XML file
        ''' </summary>
        ''' <param name="WriteMode">Use write options for creating the XML file Default is no schema</param>
        Public Sub SaveDataToXML(Optional WriteMode As XmlWriteMode = XmlWriteMode.IgnoreSchema)
            Try
                If My.Settings.XMLFile.Length > 0 Then
                    Dim strFile As String = My.Settings.XMLFile
                    If WriteMode = XmlWriteMode.WriteSchema Then
                        strFile = strFile.ToUpper().Replace(".XML", "SCHEMA.XML")
                    End If

                    For Each Table As DataTable In Me.objDS.Tables
                        If WriteMode = XmlWriteMode.WriteSchema And Not Table.TableName.ToUpper().Contains("SYS_") Then
                            Table.Rows.Clear()
                        End If
                        If Not WriteMode = XmlWriteMode.WriteSchema And Table.TableName.ToUpper().Contains("SYS_") Then
                            Table.Rows.Clear()
                        End If
                    Next
                    Me.objDS.WriteXml(strFile, WriteMode)
                End If
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Add a datacolumn to a datatable in a default format
        ''' </summary>
        ''' <param name="DT"></param>
        ''' <param name="ColumnName"></param>
        ''' <param name="ColumnAlias"></param>
        ''' <param name="ColumnType"></param>
        ''' <returns></returns>
        Public Function AddColumn(DT As DataTable, ColumnName As String, ColumnAlias As String, ColumnType As String) As DataColumn
            Dim objColumn As DataColumn
            Try
                objColumn = DT.Columns.Add(ColumnName.Replace(" ", "_"))
                Select Case ColumnType
                    Case "String"
                        ColumnType = "String"
                        objColumn.DataType = System.Type.GetType("System.String")
                        objColumn.MaxLength = 255
                    Case "Memo"
                        ColumnType = "String"
                        objColumn.DataType = System.Type.GetType("System.String")
                        objColumn.MaxLength = 1000
                    Case "Integer"
                        ColumnType = "Int32"
                        objColumn.DataType = System.Type.GetType("System.Int32")
                    Case "Long"
                        ColumnType = "Int64"
                        objColumn.DataType = System.Type.GetType("System.Int64")
                    Case "DateTime"
                        ColumnType = "Datetime"
                        objColumn.DataType = System.Type.GetType("System.DateTime")
                        objColumn.DateTimeMode = DataSetDateTime.Local
                    Case "Date"
                        ColumnType = "Date"
                        objColumn.DataType = System.Type.GetType("System.DateTime")
                    Case Else
                        If ColumnType.Contains("Lookup") Then
                            objColumn.DataType = System.Type.GetType("System.Int32")
                        End If
                        If ColumnType.Contains("Enum") Then
                            objColumn.DataType = System.Type.GetType("System.String")
                        End If
                End Select
                objColumn.Caption = ColumnAlias
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
            Return objColumn
        End Function
        ''' <summary>
        ''' Add a primary key to the datatable based on a column name
        ''' </summary>
        ''' <param name="DT"></param>
        ''' <param name="ColumnName"></param>
        ''' <returns></returns>
        Public Function AddPrimaryKey(DT As DataTable, ColumnName As String) As Boolean
            Dim objColumn As DataColumn
            Try
                objColumn = Me.AddColumn(DT, ColumnName + "ID", ColumnName, "Integer")
                objColumn.AutoIncrement = True
                objColumn.AutoIncrementSeed = 1
                objColumn.AutoIncrementStep = 1
                objColumn.AllowDBNull = False
                DT.PrimaryKey = {objColumn}
                Return True
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Add a column to a datatable with extra parameters for dbnull
        ''' </summary>
        ''' <param name="DT"></param>
        ''' <param name="ColumnName"></param>
        ''' <param name="ColumnAlias"></param>
        ''' <param name="ColumnType"></param>
        ''' <param name="AllowNull"></param>
        ''' <returns></returns>
        Public Function AddColumn(DT As DataTable, ColumnName As String, ColumnAlias As String, ColumnType As String, AllowNull As Boolean) As DataColumn
            Dim objColumn As DataColumn
            objColumn = Me.AddColumn(DT, ColumnName.Replace(" ", "_"), ColumnAlias, ColumnType)
            objColumn.AllowDBNull = AllowNull

            Return objColumn
        End Function
        ''' <summary>
        ''' Add a calculated column based on an expression
        ''' </summary>
        ''' <param name="DT"></param>
        ''' <param name="ColumnName"></param>
        ''' <param name="ColumnAlias"></param>
        ''' <param name="ColumnType"></param>
        ''' <param name="Expression"></param>
        ''' <returns></returns>
        Public Function AddCalculatedColumn(DT As DataTable, ColumnName As String, ColumnAlias As String, ColumnType As String, Expression As String) As DataColumn
            Dim objColumn As DataColumn
            Try
                objColumn = DT.Columns.Add(ColumnName.Replace(" ", "_"))
                objColumn.Expression = Expression
                objColumn.Caption = ColumnAlias
                DLA2EAHelper.DebugAssertion(DT.TableName + ColumnName + Expression)
                Return objColumn
            Catch ex As Exception
                MsgBox(DT.TableName)
                DLA2EAHelper.Error2Log(ex)
            End Try
            Return Nothing
        End Function
        ''' <summary>
        ''' Add a control to the systemtable with controls
        ''' </summary>
        ''' <param name="Tablename"></param>
        ''' <param name="Controlname"></param>
        ''' <param name="ControlType"></param>
        ''' <param name="ControlRequired"></param>
        ''' <param name="Controlcaption"></param>
        ''' <returns></returns>
        Public Function AddControl(Tablename As String, Controlname As String, ControlType As String, ControlRequired As Boolean, Controlcaption As String) As Boolean
            Dim oRow As DataRow
            Try
                oRow = Me.objDS.Tables("SYS_CONTROLS").NewRow()
                oRow("tablename") = Tablename
                oRow("controlname") = Controlname.Replace(" ", "_")
                oRow("controltype") = ControlType
                oRow("controlrequired") = ControlRequired
                oRow("controlcaption") = Controlcaption + IIf(ControlRequired, " *", "")
                Me.objDS.Tables("SYS_CONTROLS").Rows.Add(oRow)
                Return True
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
                Return False
            End Try
        End Function
        ''' <summary>
        ''' Add a constraint collection from the ea element and transform it to a validation calculated field
        ''' </summary>
        ''' <param name="Element"></param>
        ''' <param name="DT"></param>
        ''' <returns></returns>
        Public Function AddConditions(Element As EA.Element, DT As DataTable) As Boolean
            Dim strExpression As String = ""
            For Each Constraint As EA.Constraint In Element.Constraints
                strExpression += IIf(strExpression.Length > 0, " + ", "") +
                    "Iif(" + System.Net.WebUtility.HtmlDecode(Constraint.Notes) + ", '','" + Constraint.Name + "' )"
            Next
            strExpression = strExpression.Replace("Today()", "#" + System.DateTime.Now.ToString("yyyy-MM-dd") + "#")
            Me.AddCalculatedColumn(DT, "Validation", "Validation", "String", strExpression)
            Return True

        End Function
        ''' <summary>
        ''' Add a control to the controls system table
        ''' </summary>
        ''' <param name="Tablename"></param>
        ''' <param name="Controlname"></param>
        ''' <param name="ControlType"></param>
        ''' <param name="Lookup"></param>
        ''' <param name="ControlRequired"></param>
        ''' <param name="Controlcaption"></param>
        ''' <returns></returns>
        Public Function AddControl(Tablename As String, Controlname As String, ControlType As String, Lookup As String, ControlRequired As Boolean, Controlcaption As String) As Boolean
            Dim oRow As DataRow
            Try
                oRow = Me.objDS.Tables("SYS_CONTROLS").NewRow()
                oRow("tablename") = Tablename
                oRow("controlname") = Controlname.Replace(" ", "_")
                oRow("controltype") = ControlType
                oRow("controlrequired") = ControlRequired
                oRow("controlcaption") = Controlcaption + IIf(ControlRequired, " *", "")
                oRow("controllookup") = Lookup
                Me.objDS.Tables("SYS_CONTROLS").Rows.Add(oRow)
                Return True
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
                Return False
            End Try
        End Function
        ''' <summary>
        ''' Transform the inheritance structure to a list of datacolumns in the datatable
        ''' </summary>
        ''' <param name="element"></param>
        ''' <param name="DT"></param>
        ''' <returns></returns>
        Public Function Inheritance2DataSet(element As EA.Element, DT As DataTable) As Boolean
            Try
                Dim oCon As EA.Connector
                For Each oCon In element.Connectors
                    'verwerken associaties van links naar rechts en rechts naar links
                    If oCon.Type = "Generalization" And oCon.SupplierID <> element.ElementID Then
                        Dim oSupplier As EA.Element
                        oSupplier = Repository.GetElementByID(oCon.SupplierID)
                        'Recursief langs de overerving lopen
                        Inheritance2DataSet(oSupplier, DT)
                        Me.Attributes2Dataset(oSupplier, DT)
                    End If
                Next
                Return True
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
                Return False
            End Try
        End Function
        ''' <summary>
        ''' When there is no lookup defined create a lookup based on the scope public of the attributes
        ''' </summary>
        ''' <param name="Element"></param>
        ''' <returns></returns>
        Public Function DefaultLookup(Element As EA.Element) As Boolean
            Dim Attribute As EA.Attribute
            Dim blnLookup As Boolean = False
            Dim strLookup As String = ""
            If Not Me.objDS.Tables(Element.Name).Columns.Contains("Lookup") Then
                For Each Attribute In Element.Attributes
                    If Attribute.Visibility = "Public" Then
                        strLookup = strLookup + IIf(strLookup.Length > 0, " + ", "") + "[" + Attribute.Name + "]"
                        blnLookup = True
                    End If
                Next
                If strLookup.Length > 0 Then
                    AddCalculatedColumn(Me.objDS.Tables(Element.Name), "Lookup", Element.Name, "String", strLookup)
                End If
            End If
            Return blnLookup
        End Function
        ''' <summary>
        ''' Create connectors in the dataset from the connectors in EA
        ''' </summary>
        ''' <param name="Element"></param>
        ''' <param name="Package_id"></param>
        ''' <returns></returns>
        Public Function Connectors2DataSet(Element As EA.Element, Package_id As Int64) As Boolean
            Dim strPrimaryKey As String
            Try
                If Element.Type.ToUpper() = "CLASS" Then
                    Dim oCon As EA.Connector
                    Dim strConnectorName
                    For Each oCon In Element.Connectors
                        'verwerken associaties van links naar rechts en rechts naar links
                        If oCon.SupplierEnd.Cardinality.Contains("1") And oCon.ClientEnd.Cardinality.Contains("*") And oCon.SupplierID = Element.ElementID And oCon.Type = "Association" Then
                            Dim oClient As EA.Element
                            oClient = Repository.GetElementByID(oCon.ClientID)
                            If oClient.PackageID = Package_id Then
                                strPrimaryKey = Element.Name + "ID"
                                Me.AddColumn(Me.ContainerDataSet.Tables(oClient.Name), Element.Name + "ID", Element.Name, "Integer", False)
                                Me.AddControl(oClient.Name, Element.Name + "ID", "ComboBox", "Lookup_" + Element.Name, True, Element.Name)
                                strConnectorName = DLA2EAHelper.InitCap(Element.Name) + "-" + DLA2EAHelper.InitCap(oClient.Name)
                                Me.ContainerDataSet.Relations.Add(strConnectorName,
                                                                          Me.ContainerDataSet.Tables(Element.Name).Columns(strPrimaryKey),
                                                                          Me.ContainerDataSet.Tables(oClient.Name).Columns(strPrimaryKey))
                                Me.AddCalculatedColumn(Me.objDS.Tables(oClient.Name), strConnectorName, strConnectorName, "String", "Parent([" + strConnectorName + "]).Lookup ")


                            End If
                        End If
                        If oCon.ClientEnd.Cardinality.Contains("1") _
                            And oCon.SupplierEnd.Cardinality.Contains("*") _
                            And oCon.ClientID = Element.ElementID _
                            And oCon.SupplierID <> Element.ElementID _
                            And oCon.Type = "Association" Then
                            Dim oSupplier As EA.Element
                            oSupplier = Repository.GetElementByID(oCon.SupplierID)
                            If oSupplier.PackageID = Package_id Then
                                strPrimaryKey = Element.Name + "ID"
                                Me.AddColumn(Me.ContainerDataSet.Tables(oSupplier.Name), Element.Name + "ID", Element.Name, "Integer", False)
                                Me.AddControl(oSupplier.Name, Element.Name + "ID", "ComboBox", "Lookup_" + Element.Name, True, Element.Name)
                                strConnectorName = DLA2EAHelper.InitCap(Element.Name) + "-" + DLA2EAHelper.InitCap(oSupplier.Name)
                                Me.ContainerDataSet.Relations.Add(strConnectorName,
                                                                    Me.ContainerDataSet.Tables(Element.Name).Columns(strPrimaryKey),
                                                                    Me.ContainerDataSet.Tables(oSupplier.Name).Columns(strPrimaryKey))
                                Me.AddCalculatedColumn(Me.objDS.Tables(oSupplier.Name), strConnectorName, strConnectorName, "String", "Parent([" + strConnectorName + "]).Lookup ")
                            End If
                        End If
                        ' Veel op veel
                        If oCon.SupplierEnd.Cardinality.Contains("*") And oCon.ClientEnd.Cardinality.Contains("*") And oCon.SupplierID = Element.ElementID And oCon.Type = "Association" Then
                            Dim oClient As EA.Element
                            oClient = Repository.GetElementByID(oCon.ClientID)
                            If oClient.PackageID = Package_id Then
                                strPrimaryKey = Element.Name + "ID"
                                Dim strClientKey As String = oClient.Name + "ID"
                                Dim LinkDT As DataTable
                                LinkDT = Me.AddTable(Element.Name + oClient.Name)
                                Me.AddColumn(LinkDT, "Validation", "Validation", "String", True)
                                'kant een verbinden
                                Me.AddPrimaryKey(LinkDT, Element.Name + oClient.Name)
                                Me.AddColumn(LinkDT, strPrimaryKey, Element.Name, "Integer", False)
                                Me.AddControl(LinkDT.TableName, strPrimaryKey, "ComboBox", "Lookup_" + Element.Name, True, Element.Name)
                                strConnectorName = DLA2EAHelper.InitCap(Element.Name) + "-" + DLA2EAHelper.InitCap(LinkDT.TableName)
                                Me.ContainerDataSet.Relations.Add(strConnectorName,
                                                                          Me.ContainerDataSet.Tables(Element.Name).Columns(strPrimaryKey),
                                                                          LinkDT.Columns(strPrimaryKey))
                                Me.AddCalculatedColumn(LinkDT, strConnectorName, strConnectorName, "String", "Parent([" + strConnectorName + "]).Lookup ")

                                'kant twee verbinden
                                Me.AddColumn(LinkDT, strClientKey, oClient.Name, "Integer", False)
                                Me.AddControl(LinkDT.TableName, strClientKey, "ComboBox", "Lookup_" + oClient.Name, True, oClient.Name)
                                strConnectorName = DLA2EAHelper.InitCap(oClient.Name) + "-" + DLA2EAHelper.InitCap(LinkDT.TableName)

                                Me.ContainerDataSet.Relations.Add(strConnectorName,
                                                                          Me.ContainerDataSet.Tables(oClient.Name).Columns(strClientKey),
                                                                          LinkDT.Columns(strClientKey))
                                Me.AddCalculatedColumn(LinkDT, strConnectorName, strConnectorName, "String", "Parent([" + strConnectorName + "]).Lookup ")
                                'extra kolom om zaken aan elkaar te knopen
                                Dim strCalculated As String = " 'Click for details' "
                                Me.AddCalculatedColumn(LinkDT, "Lookup", Element.Name, "String", strCalculated)
                            End If
                        End If
                        ' Associatie naar zichzelf
                        If oCon.ClientEnd.Cardinality.Contains("1") _
                            And oCon.SupplierEnd.Cardinality.Contains("*") _
                            And oCon.ClientID = Element.ElementID _
                            And oCon.SupplierID = Element.ElementID _
                            And oCon.Type = "Association" Then
                            Dim oSupplier As EA.Element
                            oSupplier = Repository.GetElementByID(oCon.SupplierID)
                            If oSupplier.PackageID = Package_id Then
                                strPrimaryKey = "Parent" + Element.Name + "ID"
                                Me.AddColumn(Me.ContainerDataSet.Tables(oSupplier.Name), "Parent" + Element.Name + "ID", "Parent" + Element.Name, "Integer", True)
                                Me.AddControl(oSupplier.Name, "Parent" + Element.Name + "ID", "ComboBox", "Lookup_" + Element.Name, False, Element.Name)
                                strConnectorName = DLA2EAHelper.InitCap(Element.Name) + "-" + DLA2EAHelper.InitCap(oSupplier.Name)
                                Me.ContainerDataSet.Relations.Add(strConnectorName,
                                                                   Me.ContainerDataSet.Tables(Element.Name).Columns(Element.Name + "ID"),
                                                                   Me.ContainerDataSet.Tables(oSupplier.Name).Columns(strPrimaryKey))
                                'Me.AddCalculatedColumn(Me.objDS.Tables(oSupplier.Name), strConnectorName, strConnectorName, "String", "Parent([" + strConnectorName + "]).Lookup ")
                            End If
                        End If

                    Next
                End If
                Return True
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Bring the attributes from an element to a datatable (columns)
        ''' </summary>
        ''' <param name="Element"></param>
        ''' <param name="DT"></param>
        ''' <param name="prefix"></param>
        ''' <returns></returns>
        Public Function Attributes2Dataset(Element As EA.Element, DT As DataTable, Optional prefix As String = "") As Boolean
            Try
                Dim attribute As EA.Attribute
                For Each attribute In Element.Attributes
                    Dim strControlType As String = FormFactoryGenerator.AttributeType2Control(attribute)
                    Select Case strControlType
                        Case "Complex"
                            If attribute.ClassifierID > 0 Then
                                Dim ADT As EA.Element
                                ADT = Repository.GetElementByID(attribute.ClassifierID)
                                Select Case ADT.Type.ToUpper()
                                    Case "ENUMERATION", "INTERFACE"
                                        Me.AddColumn(DT, prefix + attribute.Name.Replace(" ", "_"), IIf(attribute.Alias.Length > 0, attribute.Alias, attribute.Name), attribute.Type, IIf(attribute.LowerBound = "0", True, False))
                                        Me.AddControl(DT.TableName, prefix + attribute.Name, "Combobox", attribute.Type, IIf(attribute.LowerBound = "0", False, True), prefix + IIf(attribute.Alias.Length > 0, attribute.Alias, attribute.Name))
                                    Case Else
                                        Me.Attributes2Dataset(ADT, DT, attribute.Name.Replace(" ", "_"))
                                End Select
                            End If
                        Case Else
                            Me.AddColumn(DT, prefix + attribute.Name, IIf(attribute.Alias.Length > 0, attribute.Alias, attribute.Name), attribute.Type, IIf(attribute.LowerBound = "0", True, False))
                            Me.AddControl(DT.TableName, prefix + attribute.Name, strControlType, IIf(attribute.LowerBound = "0", False, True), prefix + IIf(attribute.Alias.Length > 0, attribute.Alias, attribute.Name))
                    End Select
                Next
                Return True
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
                Return False
            End Try
        End Function
        ''' <summary>
        ''' Add an enumation to the system table in a structured format
        ''' </summary>
        ''' <param name="Enumname"></param>
        ''' <param name="Enumvalue"></param>
        ''' <returns></returns>
        Public Function AddEnumeration(Enumname As String, Enumvalue As String) As Boolean
            Dim oRow As DataRow
            Try
                oRow = Me.objDS.Tables("SYS_ENUMERATION").NewRow()
                oRow("enumname") = Enumname
                oRow("enumvalue") = Enumvalue
                Me.objDS.Tables("SYS_ENUMERATION").Rows.Add(oRow)
                Return True
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
                Return False
            End Try
        End Function
        'Public Function AddAssociation(Name As String, Parent As DataColumn, Child As DataColumn) As DataRelation
        '    Return Me.objDS.Relations.Add(Name, Parent, Child)
        'End Function
    End Class
End Namespace