Namespace DLAFormfactory
    ''' <summary>
    ''' Transform the dataset content to DDL commands
    ''' </summary>
    Public Class Dataset2DDL
        Private _SQL As String
        Private Dataset As DataSet
        Public ReadOnly Property SQL() As String
            Get
                Return _SQL
            End Get

        End Property
        ''' <summary>
        ''' Routine to execute the transformation f'rom a datatable to DDL sql statements
        ''' </summary>
        ''' <param name="DS">dataset with datatables and systemtables</param>
        ''' <param name="Systemtables">Process the systemtables</param>
        ''' <param name="modeltables">process the modeltables</param>
        ''' <param name="foreignkeys">create ddle for the foreign keys</param>
        ''' <returns></returns>
        Public Function Dataset2SQL(DS As DataSet, Systemtables As Boolean, modeltables As Boolean, foreignkeys As Boolean) As Boolean
            Dim Result As Boolean = True
            Me.Dataset = DS
            For Each Table As DataTable In DS.Tables
                Result = Result + DataTable2SQL(Table, Systemtables, modeltables)
            Next
            If foreignkeys = True Then
                For Each Relation As DataRelation In DS.Relations
                    Result = Result + DataRelation2SQL(Relation)
                Next
            End If
            Return Result
        End Function
        ''' <summary>
        ''' Transform a relation element in the datatable to an alter table statement in ddl
        ''' </summary>
        ''' <param name="Relation">Relation to process</param>
        ''' <returns></returns>
        Function DataRelation2SQL(Relation As DataRelation)
            Dim ForeignKeyTemplate As String = "ALTER TABLE #childtable# ADD FOREIGN KEY (#childcolumns#) REFERENCES #parenttable#(#parentcolumns#);"
            Try
                ForeignKeyTemplate = ForeignKeyTemplate.Replace("#childtable#", Relation.ChildTable.TableName.ToUpper())
                ForeignKeyTemplate = ForeignKeyTemplate.Replace("#parenttable#", Relation.ParentTable.TableName.ToUpper())
                Dim childColumns As String = ""
                For Each Column As DataColumn In Relation.ChildColumns()
                    childColumns += IIf(childColumns.Length > 0, ", ", "")
                    childColumns += Column.ColumnName.ToLower()
                Next
                Dim parentColumns As String = ""
                For Each Column As DataColumn In Relation.ParentColumns()
                    parentColumns += IIf(parentColumns.Length > 0, ", ", "")
                    parentColumns += Column.ColumnName.ToLower()
                Next
                ForeignKeyTemplate = ForeignKeyTemplate.Replace("#childcolumns#", childColumns)
                ForeignKeyTemplate = ForeignKeyTemplate.Replace("#parentcolumns#", parentColumns)
                Me._SQL += ForeignKeyTemplate + vbCrLf + vbCrLf
                Return True
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Transform a datatable to a ddl sql statement
        ''' </summary>
        ''' <param name="Table">Data table to process</param>
        ''' <returns>True or false for the processing of this function</returns>
        Private Function DataTable2SQL(Table As DataTable, systemtables As Boolean, modeltables As Boolean)
            Dim TableTemplate As String = "CREATE TABLE #tablename# (#columnnames#);"
            Try
                If (Table.TableName.ToUpper().Contains("SYS_") = True And systemtables) Or
                        (Table.TableName.ToUpper().Contains("SYS_") = False And modeltables) Then
                    TableTemplate = TableTemplate.Replace("#tablename#", Table.TableName.ToUpper())
                    Dim strColumns As String = ""
                    For Each Column As DataColumn In Table.Columns
                        If Column.Expression.Length = 0 Then
                            Dim strCol As String = Me.DataColumn(Column, Table.PrimaryKey)
                            If strCol.Length > 0 Then
                                strColumns += IIf(strColumns.Length > 0, ", ", "")
                                strColumns += strCol
                            End If
                        End If
                    Next
                    TableTemplate = TableTemplate.Replace("#columnnames#", strColumns)
                    Me._SQL += TableTemplate + vbCrLf + vbCrLf
                End If
                Return True
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
                Return False
            End Try
        End Function
        ''' <summary>
        ''' Process a datacolumn in a datatable to a part of a DDL sql statement
        ''' </summary>
        ''' <param name="Column">Column to process</param>
        ''' <param name="PrimaryKeys">Process the collection of primary keys</param>
        ''' <returns>Result of ths function</returns>
        Private Function DataColumn(Column As DataColumn, PrimaryKeys As DataColumn()) As String
            Dim ColumnTemplate As String = "#columnname# #datatype# #cardinality# #primarykey# #expression# "
            Try
                ColumnTemplate = ColumnTemplate.Replace("#columnname#", Column.ColumnName.ToLower())

                If Column.Expression.Length > 0 Then
                    'process an expression to a calculated column in SQL
                    ColumnTemplate = ColumnTemplate.Replace("#expression#", "AS " + Column.Expression)
                Else
                    'process the column as DDL of a column
                    ColumnTemplate = ColumnTemplate.Replace("#expression#", "")
                    ColumnTemplate = ColumnTemplate.Replace("#datatype#", Me.DataType2SQL(Column))
                    If Column.AllowDBNull = True Then
                        ColumnTemplate = ColumnTemplate.Replace("#cardinality#", "null")
                    Else
                        ColumnTemplate = ColumnTemplate.Replace("#cardinality#", "NOT null")
                        'when the column is in the primary key list create the promary key statement
                        For Each pkey As DataColumn In PrimaryKeys
                            If pkey.ColumnName = Column.ColumnName Then
                                ColumnTemplate = ColumnTemplate.Replace("#primarykey#", "primary key")
                                Exit For
                            End If
                        Next
                    End If
                End If
                'opruimen als het er nu nog staat
                ColumnTemplate = ColumnTemplate.Replace("#primarykey#", "")
                ColumnTemplate = ColumnTemplate.Replace("#cardinality#", "")
                ColumnTemplate = ColumnTemplate.Replace("#datatype#", "")
                Return ColumnTemplate + vbCrLf
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
                Return ""
            End Try
        End Function
        ''' <summary>
        ''' Process the datatype in the datatable to a relevant SQL server datatype
        ''' </summary>
        ''' <param name="DataColumn">The column to process</param>
        ''' <returns>SQL datatype</returns>
        Private Function DataType2SQL(DataColumn As DataColumn) As String
            Dim DataType As String = DataColumn.DataType.ToString().ToUpper()
            Dim strRet As String = "nvarchar(250)"
            If DataColumn.MaxLength > 300 And DataType = "SYSTEM.STRING" Then
                strRet = "text"
            Else
                Select Case DataType
                    Case "SYSTEM.DATE", "SYSTEM.DATETIME"
                        strRet = "datetime"
                    Case "SYSTEM.INT32"
                        strRet = "integer"
                        If DataColumn.AutoIncrement = True Then
                            strRet += " identity(1,1) "
                        End If
                End Select
            End If
            Return strRet
        End Function
    End Class
End Namespace

