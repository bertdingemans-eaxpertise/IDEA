Imports System.Web
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Data.Common
Imports System.Configuration
Namespace DLAFormfactory
    ''' <summary>
    ''' Communication with a database
    ''' </summary>
    ''' <remarks></remarks>
    Public Class DLADatabase
        Private strConnection As String = My.Settings.Connectionstring
        Private strDataBase As String = ""
        'ConfigurationManager.AppSettings("database").ToUpper()
        Public objCon As DbConnection
        Private strErrorMessage As String
        Protected objTrans As DbTransaction
        Protected strStatement As String
        Private blnTransaction As Boolean
        Private blnSupplierIsReader As Boolean
        ''' <summary>
        ''' Is the sql connection an oledb connection
        ''' </summary>
        ''' <returns>True when an oledb connection</returns>
        Public Function IsOleDB() As Boolean
            Return Me.strConnection.ToUpper().Contains("OLEDB")
        End Function
        ''' <summary>
        ''' Get the active connectionstring
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetConnectionString() As String
            Return Me.strConnection
        End Function
        ''' <summary>
        ''' Is it an access database that is connected
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsMSAccess() As Boolean
            Return strConnection.ToUpper.Contains("JET.OLEDB")
        End Function

        ''' <summary>
        ''' Make connection to a database
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub MakeConnection()
            If Me.IsOleDB() Then
                Me.objCon = New OleDb.OleDbConnection()
            Else
                Me.objCon = New SqlClient.SqlConnection()
            End If
        End Sub
        ''' <summary>
        ''' ZSet the connection string for the database
        ''' <param name="strConnect">Connectiestring</param>
        ''' <remarks></remarks>
        Private Sub SetConnection(strConnect As String)
            Me.objCon.ConnectionString = strConnect
            Me.ClearError()
            Me.blnTransaction = True
            Me.blnSupplierIsReader = True
        End Sub
        ''' <summary>
        ''' Instantiate database object
        ''' </summary>
        ''' <remarks></remarks>
        Sub New()
            MakeConnection()
            SetConnection(Me.strConnection)
        End Sub
        ''' <summary>
        ''' Geeft aan of de DLASupplier een dataset is ipv een datareader
        ''' </summary>
        ''' <remarks></remarks>
        Sub SupplierIsDataSet()
            blnSupplierIsReader = False
        End Sub
        ''' <summary>
        ''' Geeft aan dat supplier een datareader is ipv een dataset
        ''' </summary>
        ''' <remarks></remarks>
        Sub SupplierIsReader()
            blnSupplierIsReader = True
        End Sub
        ''' <summary>
        ''' Opnieuw een statement uitvoeren dus eerst de foutenlog clearen
        ''' </summary>
        ''' <remarks></remarks>
        Sub ClearError()
            strErrorMessage = ""
        End Sub
        ''' <summary>
        ''' Ophalen van de eventueel gestapelde foutmeldingen
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetErrorMessage() As String
            GetErrorMessage = Me.strErrorMessage
        End Function
        ''' <summary>
        ''' Voer een gegevensbewerkend SQL statement uit
        ''' </summary>
        ''' <param name="Sql"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SQL2DataTable(Sql As String, Table As DataTable) As DataTable
            Dim adapter As DbDataAdapter
            If Me.IsOleDB() = False Then
                adapter = New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(Sql, objCon)
            Else
                adapter = New OleDbDataAdapter()
                adapter.SelectCommand = New OleDbCommand(Sql, objCon)
            End If
            adapter.Fill(Table)
            Return Table
        End Function
        ''' <summary>
        ''' execute a modify (insert/update/delete statement
        ''' </summary>
        ''' <param name="strSql">SQL statement to execute</param>
        ''' <returns></returns>
        Public Function ExecuteModify(ByVal strSql As String) As Boolean
            Dim objCommand As DbCommand
            Dim blnOk As Boolean
            Dim strError As String = ""

            If Me.IsOleDB() Then
                objCommand = New OleDb.OleDbCommand()
            Else
                objCommand = New SqlClient.SqlCommand()
            End If
            Me.strStatement = Me.DefaultSql(strSql)
            Try
                If Not Me.IsOleDB() Then
                    objCommand.CommandText = "SET quoted_identifier off;" & strStatement
                Else
                    objCommand.CommandText = strStatement
                End If
                objCommand.Connection = Me.objCon
                If Me.blnTransaction = True Then
                    objCommand.Transaction = Me.objTrans
                End If
                objCommand.ExecuteNonQuery()
                blnOk = True
            Catch ex As DbException
                DLA2EAHelper.Error2Log(ex)
                blnOk = False
            Finally
                DLA2EAHelper.SQL2Log(strStatement, strError)
            End Try
            ExecuteModify = blnOk
        End Function
        ''' <summary>
        ''' Open een connectie met de database obv de connectiestring
        ''' </summary>
        ''' <param name="blnTrans">Use a transaction</param>
        ''' <remarks></remarks>
        Public Sub OpenConnection(ByVal blnTrans As Boolean)
            Me.objCon.Open()
            Me.blnTransaction = blnTrans
            If Me.blnTransaction = True Then
                Me.objTrans = Me.objCon.BeginTransaction()
            End If
        End Sub
        ''' <summary>
        ''' Sluit een open databaseconnectie
        ''' </summary>
        ''' <param name="blnOk"></param>
        ''' <remarks></remarks>
        Public Sub CloseConnection(ByVal blnOk As Boolean)
            If Me.blnTransaction = True Then
                If blnOk = True Then
                    Me.objTrans.Commit()
                Else
                    Me.objTrans.Rollback()
                End If
            End If
            Me.objCon.Close()
        End Sub
        ''' <summary>
        ''' Verwerk het sql statement naar een standaard zodat de NULL's correct worden afgehandeld
        ''' </summary>
        ''' <param name="strSql"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function DefaultSql(ByVal strSql As String) As String
            strSql = strSql.Replace("@@", "#")

            'strSql = strSql.Replace(Chr(34), "'"
            strSql = strSql.Replace("'NULL'", "NULL")
            strSql = strSql.Replace(Chr(34) & "NULL" & Chr(34), "NULL")
            strSql = strSql.Replace("#NULL#", "NULL")
            'strSql = strSql.Replace("#language#", DLASessionCookie.GetLanguage())
            'strSql = strSql.Replace("#weblevelid#", DLASessionCookie.GetLoginLevel())

            If Me.IsMSAccess Then
                strSql = strSql.Replace("+", "&")
                strSql = strSql.Replace("GETDATE()", "Date()")
                strSql = strSql.Replace("getDate()", "Date()")
                strSql = strSql.Replace("getdate()", "Date()")
                strSql = strSql.Replace("GetDate()", "Date()")
            Else
                strSql = strSql.Replace("&", "+")
            End If
            Return strSql
        End Function
        ''' <summary>Execute an sql statement and transform to a supplier
        ''' </summary>
        ''' <param name="strSql"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ExecuteSupply(ByVal strSql As String) As DLASupplier
            Dim objSP As New DLASupplier(Me.blnSupplierIsReader)

            Me.strStatement = Me.DefaultSql(strSql)
            objSP.SetStatement(strStatement, Me.objCon)
            Return objSP
        End Function
        ''' <summary>
        ''' Execute a sql as a selectstatement and return a data supplier
        ''' </summary>
        ''' <param name="strSql">SQL statement</param>
        ''' <param name="blnSupplierAsReader">Define the supplier type</param>
        ''' <returns></returns>
        Public Function ExecuteSupply(ByVal strSql As String, ByVal blnSupplierAsReader As Boolean) As DLASupplier
            Dim objSP As New DLASupplier(blnSupplierAsReader)
            Me.strStatement = Me.DefaultSql(strSql)
            objSP.SetStatement(strStatement, Me.objCon)
            Return objSP
        End Function
    End Class
End Namespace
