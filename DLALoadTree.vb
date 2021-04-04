Imports Microsoft.VisualBasic
Imports System.Windows.Forms

Namespace DLAFormfactory
    Public Class DLALoadTree

        Protected Filter As String = ""
        Sub LoadTreeViewNode(Table As DataTable, strSleutel As String, strDisplay As String(), rootnode As TreeNode)
            Try
                Dim Row As DataRow
                For Each Row In Table.Rows
                    Dim KindNode As TreeNode
                    Dim Totaal As String = ""
                    Dim regel As String = ""
                    For Each regel In strDisplay
                        Totaal += Row(regel) + " "
                    Next
                    KindNode = New TreeNode(Totaal)
                    KindNode.Name = rootnode.Name + "|" + Row(strSleutel)
                    If Totaal.ToUpper().Contains(Filter) Or Filter.Length = 0 Then
                        rootnode.Nodes.Add(KindNode)
                    End If
                Next
                Me.PopulateNew(rootnode)
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
        End Sub
        Sub LoadTreeViewChildNodes(Rows As DataRow(), strSleutel As String, strDisplay As String, rootnode As TreeNode, childname As String)
            Try
                Dim Row As DataRow
                For Each Row In Rows
                    Dim KindNode As TreeNode
                    If Not IsDBNull(Row(strDisplay)) Then
                        KindNode = New TreeNode(Row(DLA2EAHelper.InitCap(strDisplay)))
                        KindNode.Name = childname + "|" + Row(strSleutel).ToString()
                        If Row(strDisplay).ToUpper().Contains(Filter) Or Filter.Length = 0 Then
                            rootnode.Nodes.Add(KindNode)
                        End If
                    End If
                Next
                Me.PopulateNewChild(rootnode, childname)
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
        End Sub

        Sub LoadTreeViewNode(Table As DataTable, strSleutel As String, strDisplay As String, rootnode As TreeNode)
            Try
                Dim Row As DataRow
                For Each Row In Table.Rows
                    Dim KindNode As TreeNode
                    If Not IsDBNull(Row(strDisplay)) Then
                        KindNode = New TreeNode(DLA2EAHelper.InitCap(Row(strDisplay)))
                        KindNode.Name = rootnode.Name + "|" + Row(strSleutel).ToString()
                        If Row(strDisplay).ToUpper().Contains(Filter) Or Filter.Length = 0 Then
                            rootnode.Nodes.Add(KindNode)
                        End If
                    End If
                Next
                Me.PopulateNew(rootnode)
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
        End Sub
        Protected Sub PopulateNewChild(ByVal rootnode As TreeNode, Childname As String)

            Dim KindNode As TreeNode
            KindNode = New TreeNode("New " + Childname)
            KindNode.Name = Childname + "|NEW"
            rootnode.Nodes.Add(KindNode)

        End Sub
        Protected Sub PopulateNew(ByVal rootnode As TreeNode)

            Dim KindNode As TreeNode
            KindNode = New TreeNode("New " + rootnode.Text)
            KindNode.Name = rootnode.Name + "|NEW"
            rootnode.Nodes.Add(KindNode)

        End Sub
        Public Sub SetFilter(sFlt As String)
            Me.Filter = sFlt.ToUpper()
        End Sub
    End Class
End Namespace