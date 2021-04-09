Namespace DLAFormfactory


    ''' <summary>
    ''' Helper for working with datatables and datasets in import and export routines
    ''' </summary>
    Public Class DLADataSetHelper
        Protected DataSet As DataSet
        Public Sub New()
            Me.DataSet = New DataSet("Conversion")
        End Sub
        Private _DataRow As DataRow
        ''' <summary>
        ''' Active datarow of the datatable
        ''' </summary>
        Public Property DataRow() As DataRow
            Get
                Return _DataRow
            End Get
            Set(ByVal value As DataRow)
                _DataRow = value
            End Set
        End Property
        ''' <summary>
        ''' Active datatable in a dataset
        ''' </summary>
        Private _DataTable As DataTable
        ''' <summary>
        ''' Set or get a datatable
        ''' </summary>
        ''' <returns></returns>
        Public Property DataTable() As DataTable
            Get
                Return _DataTable
            End Get
            Set(ByVal value As DataTable)
                _DataTable = value
            End Set
        End Property
        ''' <summary>
        ''' Create a new data table with a table name and a list of column names
        ''' </summary>
        ''' <param name="tablename"></param>
        ''' <param name="columns"></param>
        Public Function NewDataTable(ByVal tablename As String, ByVal columns As String()) As Boolean
            Try
                Dim oColumn As DataColumn
                Dim sColumn As String
                Me.DataTable = Me.DataSet.Tables.Add(tablename)
                For Each sColumn In columns
                    oColumn = Me.DataTable.Columns.Add(sColumn, System.Type.GetType("System.String"))
                Next
                Return True
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
                Return False
            End Try

        End Function
        ''' <summary>
        ''' Create a new row and process it as a property so that we can easily change
        ''' values with the helper
        ''' </summary>
        Public Function NewDataRow() As Boolean
            Try
                Me.DataRow = Me.DataTable.NewRow()
                Return True
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
                Return False
            End Try

        End Function
        ''' <summary>
        ''' Add a modified datarow to to the datatable
        ''' </summary>
        Public Function AddDataRow() As Boolean
            Try
                Me.DataTable.Rows.Add(Me.DataRow)
                Return True
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
                Return False
            End Try

        End Function
        ''' <summary>
        ''' Add data to a column in the active datatable and datarow
        ''' </summary>
        ''' <param name="column"></param>
        ''' <param name="data"></param>
        Public Function AddData(ByVal column As String, ByVal data As String) As Boolean
            Try
                Me.DataRow.Item(column) = data
                Return True
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
                Return False
            End Try

        End Function

        ''' <summary>
        ''' Convert a datatable content to a string based on a template string
        ''' </summary>
        ''' <param name="template"></param>
        Public Function DataTable2String(ByVal template As String) As String
            Dim objRow As DataRow
            Dim objColumn As DataColumn
            Dim strTotal As String = ""

            For Each objRow In Me.DataTable.Rows
                Dim strRegel As String = template
                For Each objColumn In DataTable.Columns
                    strRegel = strRegel.Replace("#" + objColumn.ColumnName.ToLower + "#", objRow.Item(objColumn))
                Next
                strTotal += strRegel + vbCrLf + vbCrLf
            Next
            Return strTotal
        End Function
    End Class
End Namespace
