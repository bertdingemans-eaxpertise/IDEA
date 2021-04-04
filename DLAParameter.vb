Imports Microsoft.VisualBasic

Namespace DLAFormfactory
    ''' <summary>
    ''' klasse op basis van een combinatie naam - waarde - type
    ''' wordt gebruikt om gegevens uit controls transporteerbaar te maken door lagen
    ''' </summary>
    ''' <remarks></remarks>
    Public Class DLAParameter
        Private strName As String
        Private strValue As String
        Private strType As String
        Private blnIsNull As Boolean
        ''' <summary>
        ''' Hier initialiseer ik de klasse
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            strName = ""
            strValue = ""
            strType = ""
            blnIsNull = True
        End Sub
        ''' <summary>
        ''' constructor welke gelijk de waarden opslaat in de variabelen
        ''' let op dat alles van het type string is
        ''' toevoeging null waarde gezet
        ''' </summary>
        ''' <param name="strN"></param>
        ''' <param name="strV"></param>
        ''' <param name="strT"></param>
        ''' <param name="blnN"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal strN As String, ByVal strV As String, ByVal strT As String, ByVal blnN As Boolean)
            strName = strN.ToLower()
            Me.SetValue(strV)
            strType = strT.ToUpper()
            blnIsNull = blnN
        End Sub
        ''' <summary>
        ''' constructor welke gelijk de waarden opslaat in de variabelen
        ''' let op dat alles van het type string is
        ''' </summary>
        ''' <param name="strN"></param>
        ''' <param name="strV"></param>
        ''' <param name="strT"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal strN As String, ByVal strV As String, ByVal strT As String)
            strName = strN.ToLower()
            Me.SetValue(strV)
            strType = strT.ToUpper()
        End Sub
        ''' <summary>
        ''' Haal de waarde weer op
        ''' hier kan ook een conversie plaatsvinden bijvoorbeeld datum waarden
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetValue() As String
            Return Me.strValue
        End Function
        ''' <summary>
        ''' haal de waarde weer op
        ''' hier vindt de conversie plaats van bijvoorbeeld datum waarden
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetSqlValue() As String
            Dim blnFound As Boolean
            blnFound = False

            'als het een currency waarde is
            If strValue.IndexOf("€") >= 0 Then
                'strValue = strValue.Replace(",", ".")
                strValue = strValue.Replace("€", "")
            End If
            ' als het een boolean waarde is converteren naar int
            If Me.strValue.ToLower = "false" Then
                strValue = "0"
            End If
            If Me.strValue.ToLower = "true" Then
                strValue = "-1"
            End If
            Dim dDatum As Date
            If Date.TryParse(strValue, dDatum) Then
                strValue = dDatum.ToString("yyyy-MM-dd")
            End If
            strValue = strValue.Replace(Chr(34), "'")
            blnFound = True
            'End If
            Return Me.strValue
        End Function
        ''' <summary>
        ''' Zetten van de waarden als lengte 0 dan NULL als string
        ''' </summary>
        ''' <param name="strV"></param>
        ''' <remarks></remarks>
        Public Sub SetValue(ByVal strV As String)
            strV = Trim(strV)
            If strV.Length = 0 Then
                strValue = "NULL"
            Else
                Me.strValue = strV
            End If
        End Sub
        ''' <summary>
        ''' Geef de naam terug van de parameter
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetName() As String
            Return Me.strName
        End Function
        ''' <summary>
        ''' Haal de naam op als een DLA SQL identifier
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetIdentifier() As String
            Return "#" & Me.strName & "#"
        End Function
    End Class
End Namespace
