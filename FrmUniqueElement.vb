Imports System.Windows.Forms
Imports System.Drawing
Imports TEA.DLAFormfactory

''' <summary>
''' Form for displaying the elements that are duplicate in the repository. Give a
''' warning and eventually add the duplicate element to the diagram.
''' </summary>
Public Class FrmUniqueElement
    Protected Repository As EA.Repository
    Protected Element As EA.Element


    Public Function SetElement(ByVal oElement As EA.Element, ByVal oRepo As EA.Repository) As Boolean
        Dim sSql As String
        Dim HasData As Boolean = False
        Dim oDef As New IDEADefinitions()

        Me.Repository = oRepo
        Me.Element = oElement
        If My.Settings.SuppresWarningDialog = False And oElement.Stereotype.Length > 0 Then
            sSql = oDef.GetSettingValue("DeduplicateElementElement")
            sSql = Replace(sSql, "#object_id#", oElement.ElementID)
            Dim oDT As DataTable
            oDT = DLA2EAHelper.EAString2DataTable(oRepo.SQLQuery(sSql))
            If oDT.Rows.Count < 1 Then
                HasData = False
            Else
                Me.UniqueDataGridView.DataSource = oDT
                Me.UniqueDataGridView.AutoResizeColumns()
                HasData = True
                Dim column As New DataGridViewCheckBoxColumn()
                With column
                    .HeaderText = "Add to diagram"
                    .Name = "Add2Diagram"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                    .FlatStyle = FlatStyle.Standard
                    .CellTemplate = New DataGridViewCheckBoxCell()
                End With
                Me.UniqueDataGridView.Columns.Insert(0, column)
            End If
        End If
        Return HasData
    End Function

    Private Sub ButtonAdd2Diagram_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonAdd2Diagram.Click
        Dim objDiagram As EA.Diagram
        Dim oDVRTX As System.Windows.Forms.DataGridViewTextBoxCell
        Dim intTop As Integer = -100
        Dim intLeft As Integer = 100
        Try
            objDiagram = Me.Repository.GetCurrentDiagram()
            If Not IsNothing(objDiagram) Then
                Dim objDO As EA.DiagramObject
                For Each objDO In objDiagram.DiagramObjects
                    If objDO.ElementID = Me.Element.ElementID Then
                        intTop = objDO.top
                        intLeft = objDO.left
                        objDO.BackgroundColor = 500
                        'For Each oDVR In Me.UniqueDataGridView.Rows
                        '    If oDVR.Cells("Add2Diagram").Value = True Then
                        '        objDO.ElementID = Convert.ToInt32(oDVR.Cells("duplid").Value)
                        '    End If
                        'Next
                    End If
                    objDO.Update()
                Next

                For Each oDVR In Me.UniqueDataGridView.Rows
                    If oDVR.Cells("Add2Diagram").Value = True Then
                        intLeft += 150
                        intTop -= 70
                        oDVRTX = oDVR.Cells("duplid")
                        Dim objDON As EA.DiagramObject
                        objDON = objDiagram.DiagramObjects.AddNew("", "")
                        objDON.ElementID = oDVRTX.Value
                        objDON.top = intTop
                        objDON.bottom = intTop - 50
                        objDON.left = intLeft - 50
                        objDON.right = intLeft + 120
                        objDON.ShowNotes = False
                        objDON.Update()
                        objDiagram.Update()

                    End If
                Next
                Repository.SaveDiagram(objDiagram.DiagramID)
                Repository.ReloadDiagram(objDiagram.DiagramID)
            End If
            Me.Close()
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex.Message)
        End Try
    End Sub

    Private Sub ButtonDeduplicate_Click(sender As Object, e As EventArgs) Handles ButtonDeduplicate.Click
        Dim oDVRTX As System.Windows.Forms.DataGridViewTextBoxCell

        For Each oDVR In Me.UniqueDataGridView.Rows
            Dim oWnd As New FrmElementDeduplicator()
            oWnd.Repository = Me.Repository
            If oDVR.Cells("Add2Diagram").Value = True Then
                oDVRTX = oDVR.Cells("origid")
                oWnd.Element = Me.Element
                oWnd.SetDuplicateValue(oDVR.Cells("duplid").ToString())
                oWnd.Show()
                Me.Close()
            End If
        Next
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub ButtonFindOriginal_Click(sender As Object, e As EventArgs) Handles ButtonFindOriginal.Click
        Dim oDVRTX As System.Windows.Forms.DataGridViewTextBoxCell
        Try
            oDVRTX = Me.UniqueDataGridView.CurrentRow.Cells("origid")
            Repository.ShowInProjectView(Me.Repository.GetElementByID(oDVRTX.Value))
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub

    Private Sub ButtonFindDuplicate_Click(sender As Object, e As EventArgs) Handles ButtonFindDuplicate.Click
        Dim oDVRTX As System.Windows.Forms.DataGridViewTextBoxCell
        Try
            oDVRTX = Me.UniqueDataGridView.CurrentRow.Cells("duplid")
            Repository.ShowInProjectView(Me.Repository.GetElementByID(oDVRTX.Value))
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try

    End Sub
End Class