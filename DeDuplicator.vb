Namespace DLAFormfactory

    ''' <summary>
    ''' Class for doing the generic routines of deduplication. This has various
    ''' routines for deduplicating elements.
    ''' </summary>
    Public Class DeDuplicator
	''' <summary>
	''' Reference to the EArepository
	''' </summary>
    Private _Repository As EA.Repository
	''' <summary>
	''' String concatenation of the various properties of the original
	''' </summary>
    Private strOriginals As String = ""
        ''' <summary>
        ''' String concatenation of the various properties of the duplicate
        ''' </summary>
        Private strModules As String = ""
        Public AllModules As Boolean = False
        ''' <summary>
        ''' Helper routine to bring the values of the checkboxes in the deduplicator screen
        ''' to the deduplicator functions
        ''' </summary>
        ''' <param name="objControl"></param>
        Public Sub AddModuleViaCheckBox(ByVal objControl As System.Windows.Forms.CheckBox)
        If objControl.Checked Then
            strModules += objControl.Name.Replace("CheckBox", "") + ";"
        End If
    End Sub
	''' <summary>
	''' Method to check for running a deduplication routine
	''' </summary>
	''' <param name="sModule"></param>
    Public Function HasModule(ByVal sModule As String) As Boolean
        Dim blnRet As Boolean
            blnRet = (AllModules Or strModules.ToUpper().Contains(sModule.ToUpper()))
            Return blnRet
    End Function
    Public Property Repository() As EA.Repository
        Get
            Return _Repository
        End Get
        Set(ByVal value As EA.Repository)
            _Repository = value
        End Set
    End Property
        ''' <summary>
        ''' Get duplicates based on the selected package
        ''' </summary>
        ''' <param name="oPackage"></param>
        ''' <param name="oRepo"></param>
        ''' <returns></returns>
        Public Sub DeDuplicatePackage(ByVal oPackage As EA.Package, ByVal DuplicatePackageId As String, ProgressBar As System.Windows.Forms.ProgressBar, oDT As DataTable)
            ProgressBar.Value = 0
            ProgressBar.Minimum = 0
            ProgressBar.Maximum = oDT.Rows.Count
            ProgressBar.Step = 1
            For Each oDR In oDT.Rows
                DeduplicateElement(oDR.Item("origid"), oDR.Item("duplid"), oPackage.PackageID, DuplicatePackageId)
                ProgressBar.PerformStep()
            Next
        End Sub
        ''' <summary>
        ''' Deduplicate connectors in the package
        ''' </summary>
        ''' <param name="oPackage">Root package</param>
        ''' <param name="DuplicatePackageId">Move the duplicates to a duplicates packages</param>
        ''' <param name="ProgressBar">Progress Bar</param>
        ''' <param name="oConDT">Datatable with duplicate connectors</param>
        Public Sub DeDuplicateConnectors(ByVal oPackage As EA.Package, ByVal DuplicatePackageId As String, ProgressBar As System.Windows.Forms.ProgressBar, oConDT As DataTable)
            ProgressBar.Value = 0
            ProgressBar.Minimum = 0
            ProgressBar.Maximum = oConDT.Rows.Count
            ProgressBar.Step = 1
            If Me.HasModule("DeduplicateConnector") Then
                Dim strOrigIds As String = ""
                For Each oDR In oConDT.Rows
                    strOrigIds += " " + oDR.Item("origid")
                    If Not strOrigIds.Contains(oDR.Item("duplid")) Then
                        DeduplicateConnector(oDR.Item("origid"), oDR.Item("duplid"), oPackage.PackageID, DuplicatePackageId)
                    End If
                    ProgressBar.PerformStep()
                Next
            End If
            'process the sub packages too when checked based on recursion
        End Sub

        ''' <summary>
        ''' Deduplicate two elements in which you merge a number of child elements from the duplicate to the original
        ''' </summary>
        ''' <param name="strOriginalID">Original element (to maintain)</param>
        ''' <param name="strDuplicateID">Duplicate element (to move to the duplicate folder)</param>
        ''' <param name="strPackageID">Package id for the duplicates to move to</param>
        Public Sub DeduplicateElement(ByVal strOriginalID As String, ByVal strDuplicateID As String, ByVal strPackageID As String, ByVal strDuplPackageId As String)
            Dim objElement As EA.Element
            Dim objElementLeft As EA.Element
            Dim objTaggedValue As EA.TaggedValue
            Try
                ' suppress validation warnings for convenience of batch process
                My.Settings.SuppresWarningDialog = True
                My.Settings.Save()
                objElement = Me.Repository.GetElementByID(Convert.ToInt32(strDuplicateID))
                objElementLeft = Me.Repository.GetElementByID(Convert.ToInt32(strOriginalID))
                If objElementLeft.Name.Substring(0, 1) = "_" Then
                    Repository.WriteOutput("IDEA", "Duduplication not executed name starts with _", 0)
                Else
                    If Me.HasModule("Notes") And objElementLeft.Notes <> objElement.Notes Then
                        objElementLeft.Notes += vbCrLf + vbCrLf + objElement.Notes
                        objElementLeft.Update()
                    End If
                    If Me.HasModule("DuplicateFolder") Then
                        objElement.PackageID = strDuplPackageId
                    End If
                    If Me.HasModule("Rename") Then
                        objElement.Name = "_" & objElement.Name
                    End If
                    objElement.Update()

                    Dim strDiagramSQL As String()
                    strDiagramSQL = {"DELETE FROM t_diagramobjects WHERE object_id = #dupl_id# AND EXISTS(SELECT 1 FROM t_diagramobjects AS DO WHERE DO.object_id = #orig_id# AND DO.diagram_id = t_diagramobjects.diagram_id)  ",
                       "UPDATE t_diagramobjects SET object_id = #orig_id# WHERE object_id = #dupl_id# AND NOT EXISTS(SELECT 1 FROM t_diagramobjects AS DO WHERE DO.object_id = #orig_id# AND DO.diagram_id = t_diagramobjects.diagram_id)  "}
                    For Each Sql As String In strDiagramSQL
                        Sql = Sql.Replace("#orig_id#", strOriginalID).Replace("#dupl_id#", strDuplicateID)
                        Me.Repository.Execute(Sql)
                    Next
                    If Me.HasModule("TaggedValues") Then
                        For Each objTaggedValue In objElement.TaggedValues
                            objTaggedValue.ElementID = strOriginalID
                            objTaggedValue.Update()
                        Next
                    End If

                    If Me.HasModule("Attributes") Then
                        Me.Repository.Execute(String.Format("UPDATE t_attribute SET object_id ={0} WHERE object_id = {1} AND t_attribute.name NOT IN(SELECT name FROM t_attribute WHERE object_id = {0}) ", strOriginalID, strDuplicateID))
                    End If
                    If Me.HasModule("LinkedFiles") Then
                        ' Repository.WriteOutput("IDEA", String.Format("UPDATE t_objectfiles SET object_id = {0} WHERE object_id = {1} AND filename NOT IN(SELECT filename FROM t_objectfiles WHERE object_id = {0} )", strOriginalID, strDuplicateID), 0)
                        Me.Repository.Execute(String.Format("UPDATE t_objectfiles SET object_id = {0} WHERE object_id = {1} AND filename NOT IN(SELECT filename FROM t_objectfiles WHERE object_id = {0} )", strOriginalID, strDuplicateID))
                    End If
                    If Me.HasModule("Method") Then
                        Me.Repository.Execute(String.Format("UPDATE t_operation SET object_id ={0} WHERE object_id = {1} AND t_operation.name NOT IN(SELECT name FROM t_operation WHERE object_id ={0})", strOriginalID, strDuplicateID))
                    End If
                    If Me.HasModule("Scenario") Then
                        Me.Repository.Execute("UPDATE t_objectscenarios SET object_id =" + strOriginalID + " WHERE object_id = " + strDuplicateID)
                    End If
                    If Me.HasModule("Requirements") Then
                        Me.Repository.Execute("UPDATE t_objectrequires SET object_id =" + strOriginalID + " WHERE object_id = " + strDuplicateID)
                    End If
                    If Me.HasModule("Connectors") Then
                        Me.Repository.Execute(String.Format("UPDATE t_connector SET start_object_id ={0} WHERE start_object_id = {1} AND NOT Exists(SELECT 1 FROM t_connector T2 WHERE t2.start_object_id = {0} AND t_connector.end_object_id = t2.end_object_id AND t_connector.stereotype = T2.stereotype AND t_connector.connector_type = T2.connector_type) ", strOriginalID, strDuplicateID))
                        Me.Repository.Execute(String.Format("UPDATE t_connector SET end_object_id ={0} WHERE end_object_id = {1} AND NOT Exists(SELECT 1 FROM t_connector T2 WHERE t2.end_object_id = {0}AND t_connector.start_object_id = t2.start_object_id AND t_connector.stereotype = T2.stereotype AND t_connector.connector_type = T2.connector_type) ", strOriginalID, strDuplicateID))
                    End If
                    'when checked move the element to the duplicate folder for fast delete or archive later by hand
                    objElement.Update()
                    If Me.HasModule("CreateTrace") Then
                        Dim objCon As EA.Connector
                        objCon = objElement.Connectors.AddNew("Duplicate", "trace")
                        objCon.SupplierID = objElement.ElementID
                        objCon.ClientID = objElementLeft.ElementID
                        objCon.Update()
                        objElement.Update()
                    End If
                End If
                Repository.WriteOutput("IDEA", objElementLeft.Name + " is deduplicated", 0)
                'turn back on the warnings
                My.Settings.SuppresWarningDialog = False
                My.Settings.Save()
            Catch ex As Exception
                Repository.WriteOutput("IDEA", ex.ToString(), 0)
            End Try
        End Sub

        ''' <summary>
        ''' Deduplicate a connector, often there are more connectors between two elements
        ''' because users hide associations and create new ones
        ''' </summary>
        ''' <param name="strOriginalID"></param>
        ''' <param name="strDuplicateID"></param>
        ''' <param name="strPackageID"></param>
        ''' <param name="strDuplPackageId"></param>
        Public Sub DeduplicateConnector(ByVal strOriginalID As String, ByVal strDuplicateID As String, ByVal strPackageID As String, ByVal strDuplPackageId As String)
        Dim objConnector As EA.Connector
        Dim objConnectorLeft As EA.Connector
        Dim objTaggedValue As EA.ConnectorTag
            Try
                ' suppress validation warnings for convenience of batch process
                My.Settings.SuppresWarningDialog = True
                My.Settings.Save()
                objConnector = Me.Repository.GetConnectorByID(strDuplicateID)
                objConnectorLeft = Me.Repository.GetConnectorByID(strOriginalID)
                If Me.HasModule("Notes") And objConnectorLeft.Notes <> objConnector.Notes Then
                    objConnectorLeft.Notes += vbCrLf + vbCrLf + objConnector.Notes
                    objConnectorLeft.Update()
                End If
                Me.Repository.Execute("UPDATE t_diagramlinks SET connectorid =" + strOriginalID + " WHERE connectorid = " + strDuplicateID)
                If Me.HasModule("TaggedValues") Then
                    For Each objTaggedValue In objConnector.TaggedValues
                        objTaggedValue.ConnectorID = strOriginalID
                        objTaggedValue.Update()
                    Next
                End If
                Me.Repository.Execute("DELETE FROM t_connector WHERE connector_id = " + strDuplicateID)
                'turn back on the warnings
                My.Settings.SuppresWarningDialog = False
                My.Settings.Save()
                Me.Repository.RefreshModelView(Convert.ToInt32(strPackageID))
            Catch ex As Exception
                Repository.WriteOutput("IDEA", ex.ToString(), 0)
        End Try
    End Sub
End Class

End Namespace