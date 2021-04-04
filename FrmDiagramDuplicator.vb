Imports TEA.DLAFormfactory

''' <summary>
''' Form for the deduplication of diagrams. This is actually a duplicator since it
''' duplicates all the elements in the diagram
''' </summary>
Public Class FrmDiagramDuplicator
    Private oRepository As EA.Repository
    Private oDiagram As EA.Diagram
    Public Property Repository() As EA.Repository
        Get
            Return oRepository
        End Get
        Set(ByVal value As EA.Repository)
            oRepository = value
        End Set
    End Property
    Public Property Diagram() As EA.Diagram
        Get
            Return oDiagram
        End Get
        Set(ByVal value As EA.Diagram)
            oDiagram = value
            Me.LabelDiagram.Text = oDiagram.Name + " " + oDiagram.Version + " " + oDiagram.Author
        End Set
    End Property
    Private Sub ButtonClone_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonClone.Click
        Dim objClone As EA.Package
        Dim objPack As EA.Package
        Dim objRoot As EA.Package
        Dim oElement As EA.Element
        Dim strUser As String = ""
        Dim objHelper As New TEADataSet2Repository()

        Try
            Me.Repository.SuppressEADialogs = True
            Me.ProgressBarClone.Minimum = 0
            Me.ProgressBarClone.Maximum = 11
            If Me.Repository.IsSecurityEnabled Then
                strUser = Me.Repository.GetCurrentLoginUser()
            End If
            objRoot = Me.oRepository.GetPackageByID(Me.oDiagram.PackageID)
            objPack = objRoot.Packages.AddNew(oDiagram.Name + " original", "")
            objPack.Update()
            Me.ProgressBarClone.Increment(1)
            If Me.CheckBoxCloneDiagram.Checked = True Then
                oDiagram.PackageID = objPack.PackageID
                oDiagram.Update()
            End If
            Me.ProgressBarClone.Increment(1)
            Dim oDO As EA.DiagramObject
            For Each oDO In Me.Diagram.DiagramObjects
                oElement = Me.Repository.GetElementByID(oDO.ElementID)
                objHelper.AddOrUpdateTaggedValue(oElement, "originalpackage", oElement.PackageID, False)
                oElement.PackageID = objPack.PackageID
                oElement.Update()
            Next
            Me.ProgressBarClone.Increment(1)
            'Als de baseline check is aangevinkt een baseline aanmaken voor deze originele entiteiten
            If Me.CheckBoxKeepOriginal.Checked And Me.CheckBoxBaseline.Checked Then
                Dim project As EA.Project
                project = Me.Repository.GetProjectInterface()
                project.CreateBaseline(objPack.PackageGUID, System.DateTime.Now.ToString("yy-MM-dd:HH-mm") & " " & strUser, "Baseline for deduplicator")
            End If
            Me.ProgressBarClone.Increment(1)
            objClone = objPack.Clone()
            objClone.Name = oDiagram.Name + " Clone " + System.DateTime.Now.ToString("yy-MM-dd:HH-mm") & " " & strUser
            objClone.Update()
            Me.ProgressBarClone.Increment(1)

            For Each oElement In objClone.Elements
                oElement.Version = System.DateTime.Now.ToString("yyyy-MM-dd:HH-mm")
                oElement.Update()
            Next
            Me.ProgressBarClone.Increment(1)

            If Me.CheckBoxTrace.Checked Then
                For Each oElement In objClone.Elements
                    Dim oOrig As EA.Element
                    For Each oOrig In objPack.Elements
                        If oElement.Name = oOrig.Name Then
                            Dim oCon As EA.Connector
                            oCon = oElement.Connectors.AddNew("Duplicate", "")
                            oCon.Stereotype = "trace"
                            oCon.Direction = "Unspecified"
                            oCon.SupplierID = oOrig.ElementID
                            oCon.ClientID = oElement.ElementID
                            oCon.Update()
                            oOrig.Update()
                            oElement.Update()
                            Exit For
                        End If
                    Next
                Next
                Me.ProgressBarClone.Increment(1)
                Dim oCloneDiagram As EA.Diagram
                For Each oCloneDiagram In objClone.Diagrams
                    oCloneDiagram.Name = oCloneDiagram.Name + " Clone " + System.DateTime.Now.ToString("yy-MM-dd:HH-mm")
                    For Each oDO In oCloneDiagram.DiagramObjects
                        oDO.BackgroundColor = 900000
                        oDO.Update()
                    Next
                    oCloneDiagram.Update()
                Next
                Me.ProgressBarClone.Increment(1)
            End If
            'zet de items terug als er geen original folder hoeft te blijven
            If Me.CheckBoxKeepOriginal.Checked = False Then
                For Each oElement In objPack.Elements
                    Dim objTV As EA.TaggedValue
                    For Each objTV In oElement.TaggedValues
                        If objTV.Name = "originalpackage" Then
                            oElement.PackageID = objTV.Value
                            oElement.Update()
                            Exit For
                        End If
                    Next
                Next
                Me.ProgressBarClone.Increment(1)
                If Me.CheckBoxCloneDiagram.Checked = True Then
                    oDiagram.PackageID = objRoot.PackageID
                    oDiagram.Update()
                End If
                Me.ProgressBarClone.Increment(1)
                Dim strDeletePackage As String = String.Format("DELETE FROM t_package WHERE package_id = {0} ", objPack.PackageID)
                Me.Repository.Execute(strDeletePackage)
                Me.Repository.RefreshModelView(oDiagram.PackageID)
                Me.Repository.RefreshModelView(objPack.PackageID)

                Me.ProgressBarClone.Increment(1)
            End If
            Me.Close()
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
        Me.Repository.SuppressEADialogs = False


    End Sub

    Private Sub CheckBoxOriginal_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CheckBoxKeepOriginal.CheckedChanged

    End Sub
End Class