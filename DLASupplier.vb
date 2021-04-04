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
        Public Function SetStatement(ByVal strS As String) As String
            Dim objCon As DbConnection

            Select Case Me.strDataBase
                'Case "ORACLE"
                '   objCommand = New OracleClient.OracleCommand()
                Case "SQL"
                    objCon = New SqlClient.SqlConnection(My.Settings.Connectionstring)
                Case Else
                    objCon = New OleDb.OleDbConnection(My.Settings.Connectionstring)
            End Select
            Return SetStatement(strS, objCon)
        End Function
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
        ''' <summary>
        ''' Geef de dataset als result terug
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetDataSet() As DataSet
            Return Me.objDataSet
        End Function
        ''' <summary>
        ''' lees de volgende rij in voor een datareader
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Read() As Boolean
            Try
                Return objReader.Read()
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
        End Function
        ''' <summary>
        ''' wordt gebruikt als een datareader gebruikt wordt
        ''' om de huidige rij op te vragen
        ''' wordt gebruikt in combinatie met Read
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetCurrentRow() As DlaParameterContainer
            Dim objPC As New DlaParameterContainer()
            Dim intCount As Integer
            Dim intTeller As Integer
            Dim strDatum As String
            Dim strType As String
            Dim strBedrag As String

            intCount = objReader.FieldCount - 1
            For intTeller = 0 To intCount
                strType = objReader.GetDataTypeName(intTeller)
                Select Case strType
                    Case "DBTYPE_DATE", "DBTYPE_DBTIMESTAMP"
                        If Not objReader.IsDBNull(intTeller) Then
                            strDatum = objReader.GetDateTime(intTeller).ToString("dd-MM-yyyy")
                            objPC.AddParameter(objReader.GetName(intTeller), strDatum, objReader.GetDataTypeName(intTeller))
                        End If
                    Case "DBTYPE_CY"
                        If Not objReader.IsDBNull(intTeller) Then
                            strBedrag = objReader.GetDecimal(intTeller).ToString("C")
                            strBedrag = strBedrag.Replace("€", "")
                            strBedrag = strBedrag.Replace("$", "")
                            strBedrag = strBedrag.Replace(",", ".")
                objPC.AddParameter(objReader.GetName(intTeller), strBedrag, objReader.GetDataTypeName(intTeller))
            End If
            Case Else
                        objPC.AddParameter(objReader.GetName(intTeller), objReader.GetValue(intTeller).ToString(), objReader.GetDataTypeName(intTeller))
                End Select
            Next
            Return objPC
        End Function
        ''' <summary>
        ''' wordt gebruikt als een datareader gebruikt wordt
        ''' om de huidige rij op te vragen
        ''' wordt gebruikt in combinatie met Read
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetCurrentRowAsLookup() As Collection
            Dim objPC As New Collection()
            Dim intCount As Integer
            Dim intTeller As Integer
            Dim strValue As String
            Dim strDisplay As String

            strDisplay = ""
            intCount = objReader.FieldCount - 1
            For intTeller = 1 To intCount
                If (objReader.GetDataTypeName(intTeller) = "DBTYPE_DATE" Or objReader.GetDataTypeName(intTeller) = "DBTYPE_DBTIMESTAMP") And Not objReader.IsDBNull(intTeller) Then
                    strDisplay = strDisplay + objReader.GetDateTime(intTeller).ToString("dd-MM-yyyy") + " "
                Else
                    strDisplay = strDisplay + objReader.GetValue(intTeller).ToString() + " "
                End If
            Next
            strValue = objReader.GetValue(0).ToString()
            objPC.Add(strDisplay, "Display")
            objPC.Add(strValue, "Value")
            Return objPC
        End Function
        ''' <summary>
        ''' Sluit de datareader als we klaar zijn met loop en
        ''' </summary>
        ''' <remarks></remarks>
        Sub CloseReader()
            Me.objReader.Close()
        End Sub
    End Class
End Namespace

