Imports Microsoft.VisualBasic
Imports System.Windows.Forms

Namespace DLAFormfactory
    ''' <summary>
    ''' Class for loading a treeview in the explorer windows
    ''' </summary>
    Public Class DLALoadTree

        Protected Filter As String = ""
        ''' <summary>
        ''' Load a node to the treeview
        ''' </summary>
        ''' <param name="Table">Data table with the values and display items</param>
        ''' <param name="strSleutel">Key for reference to the table content for use in an url</param>
        ''' <param name="strDisplay">Name to display in the treeview</param>
        ''' <param name="rootnode">Rootnode where the elements as child are added to</param>
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
        ''' <summary>
        ''' Added elements to a treeview based on datarows
        ''' </summary>
        ''' <param name="Rows">List of rows</param>
        ''' <param name="strSleutel">Key value to use for an url to navigate to an element</param>
        ''' <param name="strDisplay">Name to display in the treeview</param>
        ''' <param name="rootnode">Rootnode where the elements as child are added to</param>
        ''' <param name="childname">Table name for navigation in the url</param>
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
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="Table">Data table with the values and display items</param>
        ''' <param name="strSleutel">Key for reference to the table content for use in an url</param>
        ''' <param name="strDisplay">Name to display in the treeview</param>
        ''' <param name="rootnode">Rootnode where the elements as child are added to</param>
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
        ''' <summary>
        ''' Create a treeview element for adding new elements to the database
        ''' </summary>
        ''' <param name="rootnode"></param>
        ''' <param name="Childname"></param>
        Protected Sub PopulateNewChild(ByVal rootnode As TreeNode, Childname As String)
            Dim KindNode As TreeNode
            KindNode = New TreeNode("New " + Childname)
            KindNode.Name = Childname + "|NEW"
            rootnode.Nodes.Add(KindNode)
        End Sub
        ''' <summary>
        ''' Create a treeview element for adding new elements to the database
        ''' </summary>
        ''' <param name="rootnode"></param>
        Protected Sub PopulateNew(ByVal rootnode As TreeNode)
            Dim KindNode As TreeNode
            KindNode = New TreeNode("New " + rootnode.Text)
            KindNode.Name = rootnode.Name + "|NEW"
            rootnode.Nodes.Add(KindNode)
        End Sub
        ''' <summary>
        ''' Set a filter keyword to the treeview for loading a subset
        ''' </summary>
        ''' <param name="sFlt"></param>
        Public Sub SetFilter(sFlt As String)
            Me.Filter = sFlt.ToUpper()
        End Sub
    End Class
End Namespace