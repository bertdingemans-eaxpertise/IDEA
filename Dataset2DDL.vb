﻿Namespace DLAFormfactory
    Public Class Dataset2DDL
        Private _SQL As String
        Private Dataset As DataSet
        Public ReadOnly Property SQL() As String
            Get
                Return _SQL
            End Get

        End Property

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
        Private Function DataColumn(Column As DataColumn, PrimaryKeys As DataColumn()) As String
            Dim ColumnTemplate As String = "#columnname# #datatype# #cardinality# #primarykey# #expression# "
            Try
                ColumnTemplate = ColumnTemplate.Replace("#columnname#", Column.ColumnName.ToLower())
                If Column.Expression.Length > 0 Then
                    'Return ""
                    ColumnTemplate = ColumnTemplate.Replace("#expression#", "AS " + Column.Expression)
                Else
                    ColumnTemplate = ColumnTemplate.Replace("#expression#", "")
                    ColumnTemplate = ColumnTemplate.Replace("#datatype#", Me.DataType2SQL(Column))
                    If Column.AllowDBNull = True Then
                        ColumnTemplate = ColumnTemplate.Replace("#cardinality#", "null")
                    Else
                        ColumnTemplate = ColumnTemplate.Replace("#cardinality#", "NOT null")
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

