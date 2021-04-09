Imports Microsoft.VisualBasic
Imports System.Collections
Imports System.Web

Namespace DLAFormfactory
    ''' <summary>
    ''' class met container van dlaparameters
    ''' wordt gebruikt om een collectie van waarde/naam combinaties door de lagen te transporteren
    ''' </summary>
    ''' <remarks></remarks>
    Public Class DlaParameterContainer
        Private objCol As New Collection()
        ''' <summary>
        ''' voeg een (DLA) parameter toe aan de collectie
        ''' </summary>
        ''' <param name="strName"></param>
        ''' <param name="strValue"></param>
        ''' <remarks></remarks>
        Public Sub AddParameter(ByVal strName As String, ByVal strValue As String)
            Me.AddParameter(strName, strValue, "string")
        End Sub
        Public Sub AddParameter(ByVal strName As String, ByVal strValue As String, ByVal strType As String)
            'strValue = strValue.Replace("'", "''")
            If Not Me.FindKey(strName) Then
                Me.objCol.Add(New DLAParameter(strName, strValue, strType), strName.ToLower())
            End If
        End Sub

        Public Sub UpdateParameter(ByVal strName As String, ByVal strValue As String, ByVal strType As String)
            'strValue = strValue.Replace("'", "''")
            If Me.objCol.Contains(strName) Then
                Me.objCol.Remove(strName)
            End If
            Me.objCol.Add(New DLAParameter(strName, strValue, strType), strName.ToLower())
        End Sub
        ''' <summary>
        ''' Haal een parameterwaarde op op basis van de naam
        ''' en maak  deze leeg als het een NULL waarde is
        ''' </summary>
        ''' <param name="strName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetItemValue4Control(ByVal strName As String) As String
            Dim strRet As String

            strRet = Me.GetItemValue(strName)
            If strRet.ToUpper() = "NULL" Then
                strRet = ""
            End If
            Return strRet
        End Function
        ''' <summary>
        ''' Kijk of een sleutel wel of niet bestaat
        ''' </summary>
        ''' <param name="strName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function FindKey(ByVal strName As String) As Boolean
            Dim blnFound As Boolean
            Dim objPara As DLAParameter
            For Each objPara In Me.objCol
                If objPara.GetName.ToUpper = strName.ToUpper Then
                    blnFound = True
                    Exit For
                End If
            Next
            Return blnFound
        End Function
        ''' <summary>
        ''' geef het aantal items in de collectie terug
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetItemCount() As Integer
            Return Me.objCol.Count
        End Function
        ''' <summary>
        ''' Haal een parameterwaarde op op basis van de naam
        ''' </summary>
        ''' <param name="index"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetItemName(ByVal index As Integer) As String
            Dim objPara As DLAParameter

            objPara = CType(Me.objCol.Item(index), DLAParameter)
            Return objPara.GetName()
        End Function
        ''' <summary>
        ''' Haal een parameterwaarde op op basis van de naam
        ''' </summary>
        ''' <param name="strName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetItemValue(ByVal strName As String) As String
            Dim objPara As DLAParameter
            Dim strValue As String
            Try
                objPara = CType(Me.objCol.Item(strName.ToLower()), DLAParameter)
                strValue = CType(objPara.GetValue(), String)
                strValue = strValue.Replace("''", "'")
                Return strValue
            Catch e As Exception
                DLA2EAHelper.Error2Log(e)
                Return e.Message
            End Try
        End Function
        ''' <summary>
        ''' Verwerk een sql statement naar een statement met waarden
        ''' obv een simpele replace routine
        ''' </summary>
        ''' <param name="strSql"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ProcessStatement(ByVal strSql As String) As String
            Dim objPara As DLAParameter

            strSql = strSql.Replace("@@", "#")
            For Each objPara In Me.objCol
                strSql = strSql.Replace(objPara.GetIdentifier(), objPara.GetSqlValue())
            Next
            strSql = strSql.Replace("is_less", "<=")
            strSql = strSql.Replace("is_more", ">=")
            strSql = strSql.Replace("isless", "<=")
            strSql = strSql.Replace("ismore", ">=")
            '          strSql = strSql.Replace("#weblevelid#", DLASessionCookie.GetLoginLevel())
            '           strSql = strSql.Replace("#activelanguage#", DLASessionCookie.GetLanguage())
            strSql = strSql.Replace("'NULL'", "NULL")
            Return strSql
        End Function
    End Class
End Namespace
