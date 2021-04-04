Imports System.Web
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Data.Common
Imports System.Configuration
Namespace DLAFormfactory
    ''' <summary>
    ''' Klasse voor communicatie met de database
    ''' later kunnen we hier de variaties in opnemen
    ''' nu alleen voor SQL server en access
    ''' kan later een interface worden met instantiaties voor andere platformen
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


        Public Function IsOleDB() As Boolean
            Return Me.strConnection.ToUpper().Contains("OLEDB")
        End Function
        ''' <summary>
        ''' Geef de connectiestring als returnwaarde
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetConnectionString() As String
            Return Me.strConnection
        End Function
        ''' <summary>
        ''' Is het een MS-Access database dan de SQL server specifieke zaken omzetten naar access items
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsMSAccess() As Boolean
            Return strConnection.ToUpper.Contains("JET.OLEDB")
        End Function

        ''' <summary>
        ''' Maak een connectie met de database
        ''' Dit op basis van de verschillende Database servers
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
        ''' Zet alle instellingen voor de connectie en kies eventueel een repository uit de cookies
        ''' </summary>
        ''' <param name="strConnect">Connectiestring</param>
        ''' <remarks></remarks>
        Private Sub SetConnection(strConnect As String)
            Me.objCon.ConnectionString = strConnect
            Me.ClearError()
            Me.blnTransaction = True
            Me.blnSupplierIsReader = True
        End Sub
        ''' <summary>
        ''' maak een object aan en zorg dat de connectie geopend wordt
        ''' </summary>
        ''' <remarks></remarks>
        Sub New()
            MakeConnection()
            SetConnection(Me.strConnection)
        End Sub
        ''' <summary>
        ''' maak een object aan en zorg dat de connectie geopend wordt
        ''' </summary>
        ''' <param name="strConnect">Connectiestring wordt als init parameter meegegeven</param>
        ''' <remarks></remarks>
        Sub New(ByVal strConnect As String)
            MakeConnection()
            SetConnection(strConnect)
            Me.ClearError()
            Me.blnTransaction = True
            Me.blnSupplierIsReader = True
        End Sub
        ''' <summary>
        ''' maak een object aan en zorg dat de connectie geopend wordt
        ''' en werk wel of niet met een transactie
        ''' </summary>
        ''' <param name="blnTrans"></param>
        ''' <remarks></remarks>
        Sub New(ByVal blnTrans As Boolean)
            MakeConnection()
            Me.objCon.ConnectionString = strConnection
            Me.ClearError()
            Me.blnTransaction = blnTrans
            Me.blnSupplierIsReader = True
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
        ''' <param name="blnTrans"></param>
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
        ''' <summary>
        ''' Voer een gegevensverstrekkend SQL statement uit en geef het terug als een DLAsupplier
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
        ''' Voer een gegevensverstrekkend SQL statement uit en geef het terug als een DLAsupplier
        ''' </summary>
        ''' <param name="strSql">Sql statement</param>
        ''' <param name="blnSupplierAsReader">Geef aan of het een reader of een dataset moet zijn als resultset</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ExecuteSupply(ByVal strSql As String, ByVal blnSupplierAsReader As Boolean) As DLASupplier
            Dim objSP As New DLASupplier(blnSupplierAsReader)

            Me.strStatement = Me.DefaultSql(strSql)
            objSP.SetStatement(strStatement, Me.objCon)
            Return objSP
        End Function
    End Class
End Namespace
