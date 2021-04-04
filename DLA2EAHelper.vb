Imports System.Windows.Forms
Imports System.Xml
Imports System.IO
Imports System.Reflection
Imports System.Text

Namespace DLAFormfactory


    ''' <summary>
    ''' Class for all kinds of helper routines, it is the base for all the other
    ''' routines and classes in the library
    ''' </summary>
    Public Class DLA2EAHelper
        Const AssertionOn As Boolean = False
        Shared Function InitCap(ByVal val As String) As String
            ' Test for nothing or empty.
            If String.IsNullOrEmpty(val) Then
                Return val
            Else
                Return val.Substring(0, 1).ToUpper() + val.Substring(1).ToLower()
            End If
        End Function
        Public Shared Property SuppressWarningDialog As Boolean
        ''' <summary>
        ''' Initialize the progressbar with a min and max value
        ''' </summary>
        ''' <param name="Bar"></param>
        ''' <param name="Count">Max value</param>
        Public Shared Sub SetProgressBarInit(Bar As System.Windows.Forms.ProgressBar, Count As Int32)
            Bar.Minimum = 0
            Bar.Maximum = Count
            Bar.Value = 0
        End Sub

        Public Shared Sub Text2ClipBoard(Text As String)
            System.Windows.Forms.Clipboard.SetText(Text)
        End Sub
        Public Shared Sub SQL2Log(ByVal strComments As String, strError As String)
            Dim fileStream As FileStream
            Dim streamWriter As StreamWriter
            Dim strPath As String
            If "SQL1" = "SQL" Then
                strPath = "c:\werkmap\sql.log"
                If System.IO.File.Exists(strPath) Then
                    fileStream = New FileStream(strPath, FileMode.Append, FileAccess.Write)
                Else
                    fileStream = New FileStream(strPath, FileMode.Create, FileAccess.Write)
                End If
                streamWriter = New StreamWriter(fileStream)
                streamWriter.WriteLine(System.DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss"))
                streamWriter.WriteLine("SQL: " & strComments)
                streamWriter.WriteLine("Error: " & strError)
                streamWriter.WriteLine("User: " & "")
                streamWriter.WriteLine("")
                streamWriter.Close()
                fileStream.Close()
            End If
        End Sub
        Public Shared Sub Error2Log(ByVal ex As Exception)
            Error2Log(ex.ToString())
        End Sub

        Public Shared Sub DebugAssertion(Message As String)
            If AssertionOn Then
                Text2ClipBoard(Message)
                MsgBox(Message, MsgBoxStyle.Question)
            End If
        End Sub
        Public Shared Function SQL2DataTable(ByVal strStatement As String, ByVal objRepo As EA.Repository) As DataTable
            Dim strVal As String
            strStatement = DLA2EAHelper.SQLForEAP(strStatement, objRepo)
            DLA2EAHelper.DebugAssertion(strStatement)
            strVal = objRepo.SQLQuery(strStatement)
            Return EAString2DataTable(strVal)
        End Function

        Public Shared Sub Error2Log(ByVal msg As String)
            Dim oDef As New IDEADefinitions()
            Dim strPath As String
            strPath = oDef.GetSettingValue("LogFile")
            String2File(System.DateTime.Now.ToString() + msg + vbCrLf, strPath)
            If AssertionOn Then
                MsgBox(msg)
            End If
        End Sub
        Public Shared Function String2File(ByVal text As String, ByVal strPath As String) As Boolean
            Dim fileStream As FileStream
            Dim streamWriter As StreamWriter
            If System.IO.File.Exists(strPath) Then
                fileStream = New FileStream(strPath, FileMode.Append, FileAccess.Write)
            Else
                fileStream = New FileStream(strPath, FileMode.Create, FileAccess.Write)
            End If
            streamWriter = New StreamWriter(fileStream)
            streamWriter.WriteLine(text)
            streamWriter.Close()
            fileStream.Close()
            Return True

        End Function
        ''' <summary>
        ''' Omzetten van een datatable naar een csv string
        ''' </summary>
        ''' <param name="dTable"></param>
        ''' <returns></returns>
        Public Shared Function Table2CSV(ByVal dTable As DataTable) As String
            Dim sb As StringBuilder = New StringBuilder()
            Dim intClmn As Integer = dTable.Columns.Count

            Dim i As Integer = 0
            Dim objColumn As DataColumn
            Dim blnFirst As Boolean = True
            For Each objColumn In dTable.Columns
                If objColumn.ColumnName.ToString().ToUpper() <> "DATA_ID" Then
                    If blnFirst = True Then
                        blnFirst = False
                    Else
                        sb.Append(",")
                    End If
                    sb.Append("""" + objColumn.ColumnName.ToString().Replace("-", " ").Trim() + """")
                End If
            Next
            sb.Append(vbNewLine)
            Dim row As DataRow
            For Each row In dTable.Rows
                Dim ir As Integer = 0
                blnFirst = True

                For Each objColumn In dTable.Columns
                    If objColumn.ColumnName.ToString().ToUpper() <> "DATA_ID" Then
                        If blnFirst = True Then
                            blnFirst = False
                        Else
                            sb.Append(",")
                        End If
                        sb.Append("""" + row.Item(objColumn.ColumnName).ToString().Replace("""", """""") + """")
                    End If
                Next
                sb.Append(vbNewLine)
            Next
            Return sb.ToString()
        End Function
        Public Shared Function ExecuteModifySQL(ByVal SQL As String)
            Dim objDB As DLADatabase
            Dim blnOk As Boolean = False
            objDB = New DLADatabase()
            Try
                objDB.OpenConnection(True)
                DLA2EAHelper.DebugAssertion(SQL)
                blnOk = objDB.ExecuteModify(SQL)
                objDB.CloseConnection(True)
            Catch ex As Exception
                DLA2EAHelper.Error2Log(SQL)
                DLA2EAHelper.Error2Log(ex)
                blnOk = False
            Finally
                'objDB.CloseConnection(False)
            End Try
            Return blnOk
        End Function
        Public Shared Function IsUserGroupMember(ByVal Repo As EA.Repository, ByVal Group As String) As Boolean

            If Repo.IsSecurityEnabled Then
                Dim strUserId As String
                strUserId = Repo.GetCurrentLoginUser(True)
                Dim strSql As String = "SELECT t_secgroup.groupname FROM t_secgroup, t_secusergroup WHERE t_secgroup.groupid = t_secusergroup.groupid AND t_secusergroup.userid = '" + strUserId + "'"
                Return Repo.SQLQuery(strSql).IndexOf(Group) > 0
            Else
                Return False
            End If

        End Function


        ''' <summary>
        ''' Check if an element is unique or not based on the name and stereotype
        ''' </summary>
        ''' <param name="strName">Name of the element</param>
        ''' <param name="strType">Stereotype of the element</param>
        ''' <param name="oRepo">Repository to run the query in</param>
        ''' <returns>True for unique</returns>
        Public Shared Function CheckUniqueElement(ByVal strName As String, ByVal strType As String, ByVal oRepo As EA.Repository) As Boolean
            Dim sSql As String
            sSql = "SELECT Count(*) as aantal FROM t_object WHERE t_object.name = '#name#' And t_object.Stereotype='#stereotype#' "
            sSql = Replace(sSql, "#name#", strName.Replace("'", "''"))
            sSql = Replace(sSql, "#stereotype#", strType)

            Dim strVal As String
            strVal = oRepo.SQLQuery(sSql)
            Return strVal.Contains("<aantal>0</aantal>")
        End Function


        Public Shared Function SQL2DataSet(ByVal strStatement As String, ByVal objRepo As EA.Repository) As DataSet
            Dim strVal As String
            If objRepo.RepositoryType = "JET" Then
                strStatement = strStatement.Replace("+", "&")
                strStatement = strStatement.Replace("%", "*")
            End If
            DLA2EAHelper.DebugAssertion(strStatement)
            strVal = objRepo.SQLQuery(strStatement)
            Return EAString2DataSet(strVal)
        End Function

        Public Shared Function SQLForEAP(ByVal strStatement As String, ByVal objRepo As EA.Repository) As String
            If objRepo.RepositoryType().ToUpper() = "JET" Then
                strStatement = strStatement.Replace("+", "&")
                strStatement = strStatement.Replace("%", "*")
            End If
            Return strStatement
        End Function
        ''' <summary>
        ''' Transform a Sparx dataset string to a usable dataset in VB
        ''' </summary>
        ''' <param name="strVal"></param>
        ''' <returns></returns>
        Public Shared Function EAString2DataSet(ByVal strVal As String) As DataSet
            Dim oDataset As New DataSet()
            Try
                If strVal.IndexOf("Dataset_0") > 0 Then
                    strVal = strVal.Replace("<EADATA version=""1.0"" exporter=""Enterprise Architect"">", "")
                    strVal = strVal.Replace("</EADATA>", "")
                    oDataset.ReadXml(New StringReader(strVal))
                Else
                    oDataset.Tables.Add(New DataTable())
                End If
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
            Return oDataset
        End Function

        Public Shared Function EAString2DataTable(ByVal strVal As String) As DataTable
            Dim oDataset As New DataSet()
            Try
                oDataset = DLA2EAHelper.EAString2DataSet(strVal)
                If oDataset.Tables.Count = 2 Then
                    Return oDataset.Tables(1)
                End If
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
            Return New DataTable("ERROR")
        End Function

        'Public Shared Function CheckUniqueElement(ByVal oElement As EA.Element, ByVal oRepo As EA.Repository) As Boolean
        '    Dim sSql As String
        '    If oElement.Stereotype.Length > 0 Then
        '        sSql = "SELECT Count(*) as aantal FROM t_object WHERE t_object.name = '#name#' And t_object.version = '#version#' And t_object.Stereotype='#stereotype#' AND t_object.ea_guid<>'#guid#' "
        '        sSql = Replace(sSql, "#name#", oElement.Name.Replace("'", "''"))
        '        sSql = Replace(sSql, "#stereotype#", oElement.Stereotype)
        '        sSql = Replace(sSql, "#version#", oElement.Version)

        '        sSql = Replace(sSql, "#guid#", oElement.ElementGUID)
        '        Dim strVal As String
        '        strVal = oRepo.SQLQuery(sSql)
        '        Return strVal.Contains("<aantal>0</aantal>")
        '    End If
        '    Return True
        'End Function
        'Public Shared Function CheckUniqueElement(ByVal strName As String, ByVal oRepo As EA.Repository) As Boolean
        '    Dim sSql As String
        '    sSql = "SELECT Count(*) as aantal FROM t_object WHERE t_object.name = '#name#' "
        '    sSql = Replace(sSql, "#name#", strName.Replace("'", "''"))

        '    Dim strVal As String
        '    strVal = oRepo.SQLQuery(sSql)
        '    Return strVal.Contains("<aantal>0</aantal>")
        'End Function
    End Class

End Namespace