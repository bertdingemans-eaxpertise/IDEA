Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common
Imports System.Configuration

Namespace DLAFormfactory
    ''' <summary>
    ''' Klasse voor het inkapselen van de navigatie door een recordset
    ''' kan bijvoorbeeld gebruikt worden voor het vullen van listboxen e.d. 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class DLASupplier
        Private objReader As DbDataReader
        Private objDataSet As DataSet
        Private strSql As String
        Private blnReader As Boolean
        Private strDataBase As String = ""
        Public Sub New()
            blnReader = True
        End Sub
        ''' <summary>
        ''' Constructor geeft aan dat een datareader gebruikt wordt ipv een dataset
        ''' </summary>
        ''' <param name="blnR"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal blnR As Boolean)
            blnReader = blnR
        End Sub

        ''' <summary>
        ''' Zet het sql statement in de vorm van een dataadapter of datareader
        ''' met een aardige error handling
        ''' </summary>
        ''' <param name="strS"></param>
        ''' <param name="objCon"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SetStatement(ByVal strS As String, ByVal objCon As DbConnection) As String
            Dim objCommand As DbCommand
            Dim objDA As DbDataAdapter
            Dim strError As String

            If objCon.ConnectionString.ToUpper().Contains("OLEDB") Then
                objCommand = New OleDb.OleDbCommand()
            Else
                objCommand = New SqlClient.SqlCommand()
            End If
            strError = ""
            Me.strSql = strS
            If Not objCon.ConnectionString.ToUpper.Contains("MICROSOFT.JET.OLEDB") Then
                strSql = "Set quoted_identifier OFF;" & strSql
            End If
            Try
                If blnReader = True Then
                    'vullen van de datareader
                    objCommand.CommandText = strSql
                    objCommand.Connection = objCon
                    objCommand.CommandType = CommandType.Text
                    Me.objReader = objCommand.ExecuteReader()
                Else
                    ' vullen van de dataset
                    Select Case Me.strDataBase
                        Case "SQL"
                            objDA = New SqlClient.SqlDataAdapter(Me.strSql, objCon)
                            'Case "Oracle"
                            '    objDA = New OracleClient.OracleDataAdapter(Me.strSql, objCon)
                        Case Else
                            objDA = New OleDb.OleDbDataAdapter(Me.strSql, objCon)
                    End Select
                    Me.objDataSet = New Data.DataSet()
                    objDA.Fill(Me.objDataSet)
                End If
            Catch e As DbException
                DLA2EAHelper.Error2Log(e)
                strError = e.Message
            End Try
            Return strError
        End Function
    End Class
End Namespace

