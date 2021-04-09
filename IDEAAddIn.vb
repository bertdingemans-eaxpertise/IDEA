Imports System.Data
Imports System.IO
Imports System.Xml
Imports System
Imports TEA.DLAFormfactory

Namespace TEA
    ''' <summary>
    ''' Connector class for the menu options etcetera for the AddOn
    ''' Here all the connection points from the EA application are defined
    ''' </summary>
    Public Class IDEAAddIn
        ' define menu constants
        Const menuDeduplicator As String = "Deduplicator "
        Const menuAssistant As String = "Assistant"
        Const menuFormFactory As String = "Form Factory"
        Const menuArchiMAID As String = "ArchimAID"
        Const menuNoDataFault As String = "DatAID"
        Const menuSettings As String = "Settings"
        Const menuElement As String = "Browser Helper "
        Const diagramElement As String = "Diagram Helper "
        Const diagramDeduplicator As String = "Deduplicate on diagram "
        Const menuHelper As String = "-IDEA"

        Public Function EA_Connect(ByVal Repository As EA.Repository) As [String]
            DLA2EAHelper.SuppressWarningDialog = False
            Repository.CreateOutputTab("IDEA")
            Return "a string"
        End Function

        'Public Function EA_OnPostNewElement(ByVal Repository As EA.Repository, ByVal Info As EA.EventProperties) As Boolean
        '    Return True
        'End Function
        ''' <summary>
        ''' When an element is deleted first check for the wastebin function in IDEA
        ''' </summary>
        ''' <param name="Repository"></param>
        ''' <param name="Info"></param>
        ''' <returns></returns>
        Function EA_OnPreDeleteElement(Repository As EA.Repository, Info As EA.EventProperties) As Boolean
            Dim intTeller As Int32 = 0
            Dim blnReturn As Boolean = True
            While intTeller < Info.Count
                blnReturn = blnReturn And WasteBin.WasteBinElement(Repository, Info.Get(intTeller).Value)
                intTeller += 1
            End While
            Repository.EnableUIUpdates = False
            Repository.RefreshModelView(Repository.GetTreeSelectedPackage().PackageID)
            Repository.EnableUIUpdates = True
            Return blnReturn
        End Function
        ''' <summary>
        ''' When a package is deleted first check for the wastebin function in IDEA
        ''' </summary>
        ''' <param name="Repository"></param>
        ''' <param name="Info"></param>
        ''' <returns></returns>
        Function EA_OnPreDeletePackage(Repository As EA.Repository, Info As EA.EventProperties) As Boolean
            Return WasteBin.WasteBinPackage(Repository, Info.Get(0).Value)
        End Function
        'Public Overridable Function EA_OnPostNewConnector(ByVal Repository As EA.Repository, ByVal Info As EA.EventProperties) As Boolean
        '    Return False
        'End Function

        ''' <summary>
        ''' Check for the diagram helpers to see if they need to be opened
        ''' </summary>
        ''' <param name="Repository"></param>
        ''' <param name="DiagramID"></param>
        Public Overridable Sub EA_OnPostOpenDiagram(ByVal Repository As EA.Repository, ByVal DiagramID As Integer)
            Try
                If My.Settings.ShowAidOnDiagramOpen = True Then
                    If Repository.GetCurrentDiagram().StyleEx.ToUpper().Contains("ARCHIMATE") Then
                        Dim FrmAM As New FrmArchimAID()
                        FrmAM.Repository = Repository
                        FrmAM.Show()
                    Else
                        Dim FrmDV As New FrmDataVault()
                        FrmDV.Repository = Repository
                        FrmDV.LoadTemplates()
                        FrmDV.Show()
                    End If
                End If
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Save the diagram direct to a file
        ''' </summary>
        ''' <param name="Repository"></param>
        ''' <param name="DiagramID"></param>
        Public Overridable Sub EA_OnPostCloseDiagram(ByVal Repository As EA.Repository, ByVal DiagramID As Integer)
            Try
                Dim oDef As New IDEADefinitions()

                Dim strFile As String = oDef.GetSettingValue("WPP_Diagram_File")
                If strFile.Length > 1 Then
                    strFile = strFile.Replace("#diagram_id#", DiagramID.ToString())
                    Repository.GetProjectInterface().SaveDiagramImageToFile(strFile)
                End If
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
        End Sub

        ''' true if a project is opened in EA
        Private Function IsProjectOpen(ByVal Repository As EA.Repository) As Boolean
            Try
                Dim c As EA.Collection = Repository.Models
                Return True
            Catch
                Return False
            End Try
        End Function
        ''' <summary>
        ''' check of the IDEA menu needs to be enabled (when a project is opened) or not
        ''' </summary>
        ''' <param name="Repository"></param>
        ''' <param name="Location"></param>
        ''' <param name="MenuName"></param>
        ''' <param name="ItemName"></param>
        ''' <param name="IsEnabled"></param>
        ''' <param name="IsChecked"></param>
        Public Sub EA_GetMenuState(ByVal Repository As EA.Repository, ByVal Location As String, ByVal MenuName As String, ByVal ItemName As String, ByRef IsEnabled As Boolean, ByRef IsChecked As Boolean)
            If Not IsProjectOpen(Repository) Then
                ' If no open project, disable all menu options
                IsEnabled = False
            End If
        End Sub
        ''' <summary>
        ''' Activate the specific helper window based on the stereotype of the active element
        ''' </summary>
        ''' <param name="Repository"></param>
        ''' <param name="Element"></param>
        Private Sub OpenFormForElement(ByVal Repository As EA.Repository, ByVal Element As EA.Element)
            Try
                Select Case Element.Stereotype.ToUpper
                    Case "TABLE"
                        If My.Settings.TableHelper Then
                            Dim objFrmTable As New FrmIDEATable()
                            objFrmTable.Repository = Repository
                            objFrmTable.Element = Element
                            objFrmTable.LoadElement()
                            objFrmTable.Show()
                        End If
                    Case "XSDCOMPLEXTYPE"
                        If My.Settings.XSDHelper Then
                            Dim objFrmTable As New FrmIDEATable()
                            objFrmTable.Text = "IDEA XSD Helper"
                            objFrmTable.Repository = Repository
                            objFrmTable.Element = Element
                            objFrmTable.LoadElement()
                            objFrmTable.Show()
                        End If
                    Case Else
                        If Element.Stereotype.Length = 0 And My.Settings.ClassHelper Then
                            Dim objFrmClass As New FrmIDEAClass()
                            objFrmClass.Repository = Repository
                            objFrmClass.Element = Element
                            objFrmClass.LoadElement()
                            objFrmClass.Show()
                        Else
                            MsgBox("No helper available for this element type", MsgBoxStyle.OkOnly)
                        End If
                End Select
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try

        End Sub
        ''' <summary>
        ''' Create the menu's for the IDEA AddOn menu, the explorer and the diagram canvas
        ''' the array of options is dynamically created based on the user settings
        ''' </summary>
        ''' <param name="Repository"></param>
        ''' <param name="Location"></param>
        ''' <param name="MenuName"></param>
        ''' <returns></returns>
        Public Function EA_GetMenuItems(ByVal Repository As EA.Repository, ByVal Location As String, ByVal MenuName As String) As Object
            Dim menuList As New List(Of String)

            Try
                Select Case Location.ToUpper()
                    Case "MAINMENU"
                        Select Case MenuName
                            Case ""
                                Return menuHelper
                            Case menuHelper
                                If My.Settings.Deduplicator Then
                                    menuList.Add(menuDeduplicator)
                                End If
                                If My.Settings.FormFactory Then
                                    menuList.Add(menuFormFactory)
                                End If
                                If My.Settings.ArchimAid Then
                                    menuList.Add(menuArchiMAID)
                                End If
                                If My.Settings.DatAid Then
                                    menuList.Add(menuNoDataFault)
                                End If
                                If My.Settings.Assistant Then
                                    menuList.Add(menuAssistant)
                                End If
                                menuList.Add(menuSettings)
                                Return menuList.ToArray()
                        End Select
                    Case "TREEVIEW"
                        Select Case MenuName
                            Case ""
                                Return "-IDEA"
                            Case "-IDEA"
                                menuList.Add(menuElement)
                                If My.Settings.Deduplicator Then
                                    menuList.Add(menuDeduplicator)
                                End If
                                Return menuList.ToArray()
                        End Select
                    Case "DIAGRAM"
                        Select Case MenuName
                            Case ""
                                Return "-IDEA"
                            Case "-IDEA"
                                menuList.Add(diagramElement)
                                If My.Settings.Deduplicator Then
                                    menuList.Add(menuDeduplicator)
                                End If
                                If My.Settings.ArchimAid Then
                                    menuList.Add(menuArchiMAID)
                                End If
                                If My.Settings.DatAid Then
                                    menuList.Add(menuNoDataFault)
                                End If
                                Return menuList.ToArray()
                        End Select
                    Case Else
                        Return ""
                End Select
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
            Return ""
        End Function
        ''' <summary>
        ''' Handle the click of a menu item and call the relevant underlying routine
        ''' </summary>
        ''' <param name="Repository"></param>
        ''' <param name="Location"></param>
        ''' <param name="MenuName"></param>
        ''' <param name="ItemName"></param>
        Public Sub EA_MenuClick(ByVal Repository As EA.Repository, ByVal Location As String, ByVal MenuName As String, ByVal ItemName As String)
            Try
                Select Case ItemName
                    Case diagramElement
                        If Repository.GetCurrentDiagram.SelectedObjects.Count = 1 Then
                            Dim objDO As EA.DiagramObject
                            objDO = Repository.GetCurrentDiagram.SelectedObjects(0)
                            Dim objElement As EA.Element = Repository.GetElementByID(objDO.ElementID)
                            OpenFormForElement(Repository, objElement)
                        Else
                            Dim objWndDiagram As New FrmIdeaDiagram()
                            objWndDiagram.Repository = Repository
                            objWndDiagram.Diagram = Repository.GetCurrentDiagram
                            'Repository.CloseDiagram(objWndDiagram.Diagram.DiagramID)
                            objWndDiagram.Show()
                            If Not objWndDiagram.LoadElements() Then
                                objWndDiagram.Close()
                            End If

                        End If
                    Case menuElement
                        If Location.ToUpper() = "TREEVIEW" Then
                            If Repository.GetTreeSelectedItemType() = EA.ObjectType.otElement Then
                                OpenFormForElement(Repository, Repository.GetTreeSelectedObject())
                            End If

                            If Repository.GetTreeSelectedItemType() = EA.ObjectType.otDiagram And My.Settings.DiagramHelper Then
                                Dim objDiagram As EA.Diagram
                                objDiagram = Repository.GetTreeSelectedObject()
                                Dim objFrmDiagram As New FrmIdeaDiagram()
                                objFrmDiagram.Repository = Repository
                                objFrmDiagram.Diagram = objDiagram
                                objFrmDiagram.LoadElements()
                                objFrmDiagram.Show()
                            End If
                            If Repository.GetTreeSelectedItemType() = EA.ObjectType.otPackage And My.Settings.PackageHelper Then
                                Dim objPackage As EA.Package
                                objPackage = Repository.GetTreeSelectedObject()
                                Dim objReporting = New FrmReportingHelper()
                                objReporting.Repository = Repository
                                objReporting.oPackage = objPackage
                                objReporting.Show()
                            End If
                        End If
                    Case menuDeduplicator
                        Select Case Location.ToUpper()
                            Case "DIAGRAM"
                                Try
                                    If Repository.GetCurrentDiagram.SelectedObjects.Count = 1 Then

                                        Dim objFrmED As New FrmElementDeduplicator()
                                        Dim objDO As EA.DiagramObject
                                        objDO = DirectCast(Repository.GetCurrentDiagram.SelectedObjects.GetAt(0), EA.DiagramObject)
                                        objFrmED.Repository = Repository
                                        objFrmED.Element = Repository.GetElementByID(objDO.ElementID)
                                        objFrmED.Show()

                                    Else
                                        Dim objFrmDD As New FrmDiagramDuplicator()
                                        objFrmDD.Repository = Repository
                                        objFrmDD.Diagram = Repository.GetTreeSelectedObject()
                                        objFrmDD.Show()
                                    End If
                                Catch ex As Exception
                                    DLA2EAHelper.Error2Log(ex)
                                End Try
                            Case "TREEVIEW"
                                If Repository.GetTreeSelectedItemType() = EA.ObjectType.otElement Then
                                    Dim objFrmED As New FrmElementDeduplicator()
                                    objFrmED.Repository = Repository
                                    objFrmED.Element = Repository.GetTreeSelectedObject()
                                    objFrmED.Show()
                                End If

                                If Repository.GetTreeSelectedItemType() = EA.ObjectType.otDiagram Then
                                    Dim objFrmDD As New FrmDiagramDuplicator()
                                    objFrmDD.Repository = Repository
                                    objFrmDD.Diagram = Repository.GetTreeSelectedObject()
                                    objFrmDD.Show()
                                End If

                                If Repository.GetTreeSelectedItemType() = EA.ObjectType.otPackage Then
                                    Dim objFrmDD As New FrmPackageDeduplicator()
                                    objFrmDD.Repository = Repository
                                    objFrmDD.Show()
                                End If
                            Case "MAINMENU"
                                Dim objFrmDD As New FrmPackageDeduplicator()
                                objFrmDD.Repository = Repository
                                objFrmDD.Show()
                        End Select
                    Case menuFormFactory
                        Dim objFrmFF As New FrmIDEAFormFactory()
                        objFrmFF.Repository = Repository
                        objFrmFF.Package = Repository.GetTreeSelectedPackage()
                        objFrmFF.LoadPackage()
                        objFrmFF.Show()
                    Case menuArchiMAID
                        Dim Frm As New FrmArchimAID()
                        Frm.Repository = Repository
                        Frm.Show()
                    Case menuNoDataFault
                        Dim oFrmDV As New FrmDataVault()
                        oFrmDV.Repository = Repository
                        oFrmDV.LoadTemplates()
                        oFrmDV.Show()
                    Case menuSettings
                        Dim oWnd As New FrmSettings()
                        oWnd.Repository = Repository
                        oWnd.Show()
                    Case Else
                        Dim oWnd As New WndWelcome()
                        oWnd.Repository = Repository
                        oWnd.Show()
                End Select
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try

        End Sub
        ''' <summary>
        ''' Call the deduplicator check screen when this is activated
        ''' </summary>
        ''' <param name="Repository"></param>
        ''' <param name="sGuid"></param>
        ''' <param name="oType"></param>
        Public Sub EA_OnNotifyContextItemModified(ByVal Repository As EA.Repository, ByVal sGuid As String, ByVal oType As EA.ObjectType)
            Dim oElement As EA.Element
            Try
                If oType = EA.ObjectType.otElement Then
                    oElement = Repository.GetElementByGuid(sGuid)
                    If Not IsNothing(oElement) Then
                        'Alleen bij archimate elementen valideren
                        If oElement.Stereotype.Contains("ArchiMate") Then
                            'Alleen een scherm tonen als de gebruiker dat heeft aangezet in de settings
                            If My.Settings.SuppresWarningDialog = False Then
                                Dim oWnd As New FrmUniqueElement()
                                If oWnd.SetElement(oElement, Repository) Then
                                    oWnd.Show()
                                End If
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
        End Sub
        Public Sub EA_Disconnect()
            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Sub
    End Class
End Namespace

