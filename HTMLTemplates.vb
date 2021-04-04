Namespace DLAFormfactory



    Public Class HTMLTemplates
        Protected oSet As DataSet
        Protected oTemplates As DataTable
        Public Const T_NAME As String = "Template_Name"
        Public Const T_HEADER As String = "Template_Header"
        Public Const T_BODY As String = "Template_Body"
        Public Const T_FOOTER As String = "Template_Footer"
        Public Const T_SQL As String = "Template_SQL"
        Sub New()
            Me.oSet = New DataSet("HTMLtemplates")
            Me.oTemplates = New DataTable("templates")
            oSet.Tables.Add(Me.oTemplates)
            Me.oTemplates.Columns.Add(New DataColumn(T_NAME))
            Me.oTemplates.Columns.Add(New DataColumn("id"))
            Me.oTemplates.Columns.Add(New DataColumn(T_HEADER))
            Me.oTemplates.Columns.Add(New DataColumn(T_BODY))
            Me.oTemplates.Columns.Add(New DataColumn(T_FOOTER))
            Me.oTemplates.Columns.Add(New DataColumn(T_SQL))

        End Sub

        Function AddTemplate(name As String, header As String, body As String, footer As String, sql As String) As Boolean
            Dim oRow As DataRow
            Try
                oRow = oTemplates.NewRow()
                oRow(T_NAME) = name
                oRow(T_HEADER) = header
                oRow(T_BODY) = body
                oRow(T_FOOTER) = footer
                oRow(T_SQL) = sql

                oRow("id") = Guid.NewGuid()
                oTemplates.Rows.Add(oRow)
                oTemplates.AcceptChanges()
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
                Return False
            End Try

            Return True
        End Function
        Function GetRowById(id As String) As DataRow
            Dim oRow As DataRow
            Try
                For Each oRow In Me.oTemplates.Rows
                    If oRow("id") = id Then
                        Return oRow
                        Exit For
                    End If
                Next
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
            Return oTemplates.NewRow

        End Function
        Function GetRowByTemplate(template As String) As DataRow
            Dim oRow As DataRow
            Try
                For Each oRow In Me.oTemplates.Rows
                    If template.Contains("#" + oRow(T_NAME).ToString().ToLower() + "#") Then
                        Return oRow
                        Exit For
                    End If
                Next
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
            Return oTemplates.NewRow

        End Function
        Function GetRowByName(name As String) As DataRow
            Dim oRow As DataRow
            Try
                For Each oRow In Me.oTemplates.Rows
                    If oRow(T_NAME).ToString().ToUpper() = name.ToUpper Then
                        Return oRow
                        Exit For
                    End If
                Next
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
            Return oTemplates.NewRow

        End Function
        Function UpdateTemplate(name As String, header As String, body As String, footer As String, id As String, sql As String) As Boolean
            Dim oRow As DataRow
            Try
                For Each oRow In oTemplates.Rows
                    If oRow("id") = id Then
                        oRow(T_NAME) = name
                        oRow(T_HEADER) = header
                        oRow(T_BODY) = body
                        oRow(T_FOOTER) = footer
                        oRow(T_SQL) = sql
                        oRow.AcceptChanges()
                        Me.oTemplates.AcceptChanges()
                        Exit For
                    End If
                Next
                Return True
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
                Return False
            End Try
        End Function

        Function SaveTemplates(file As String) As Boolean
            Try
                If file.Length = 0 Then
                    MsgBox("No filename defined for templates", MsgBoxStyle.OkOnly)
                    Return False
                End If
                DefaultTemplates()
                oSet.WriteXml(file)
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
                Return False
            End Try
            Return True

        End Function

        Public Function DefaultTemplates() As Boolean
            Try
                If Me.oTemplates.Rows.Count = 0 Then
                    Me.AddTemplate("Page_Template", "", "<html><head><link href=""https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css"" rel=""stylesheet"" ></head><body><nav class='navbar navbar-expand-sm bg-light navbar-light'><ul class='navbar-nav'><li class='nav-item active'><a class='nav-link' href='http://eaxpertise.nl'>IDEA</a></li><li class='nav-item'><a class='nav-link' href='Sitemap.html'>SiteMap</a></li></ul></nav><div class='container'>#content#</div></body></html>", "", "")
                    Me.AddTemplate("Detail_Package", "IDEA by EAxpertise<hr>", "<h1>#name#</h1><p>#notes#</p>#list_packages_package##list_diagrams_package##list_elements_package#<hr>#package_pdf#", "<footer class=''ftco-footer ftco-bg-dark ftco-section''</footer>", "select * from t_package where package_id = #id#")
                    Me.AddTemplate("List_Packages", "<h2>Packages</h2><ul>", "<li><a href=""package#package_id#.html"" >#name#</a></li>", "</ul>", "select * from t_package order by name")
                    Me.AddTemplate("List_Packages_Package", "<h2>Packages</h2><ul>", "<li><a href=""package#package_id#.html"" >#name#</a></li>", "</ul>", "select * from t_package where parent_id = #id#")
                    Me.AddTemplate("Detail_Diagram", "IDEA by EAxpertise<hr>", "<h1>#name#</h1><img src=""diagram#diagram_id#.png"" usemap=#ideamap >#diagram_map#<p>#notes#</p> #list_elements_diagram#", "<footer class=''ftco-footer ftco-bg-dark ftco-section''</footer>", "select * from t_diagram where diagram_id = #id#")
                    Me.AddTemplate("List_Diagrams_Package", "<h3>Diagrams</h3><ul>", "<li><a href=""diagram#diagram_id#.html"" >#name#</a></li>", "</ul>", "select * from t_diagram where package_id = #id#")
                    Me.AddTemplate("Detail_Element", "IDEA by EAxpertise<hr>", "<h1>#name#</h1><p>#note#</p>", "<footer class=''ftco-footer ftco-bg-dark ftco-section''</footer>", "select * from t_object where object_id = #id#")
                    Me.AddTemplate("List_Elements_Package", "<h4>Objects</h4><ul>", "<li><a href=""element#object_id#.html"" >#name#</a></li>", "</ul>", "select * from t_object where package_id = #id#")
                    Me.AddTemplate("List_Elements_Diagram", "<h4>Objects</h4><ul>", "<li><a href=""element#object_id#.html"" >#name#</a></li>", "</ul>", "select t_object.* from t_object, t_diagramobjects  where t_object.Object_ID = t_diagramobjects.Object_ID and t_diagramobjects.Diagram_ID = #id#")
                    Me.AddTemplate("Default_Url", "", "<a href=""#url#.html"" >#title#</a>", "", "")
                    Me.AddTemplate("index.html", "<h1>Index page</h1>", "Welcome at our startpage", "Contact us at info@eaxpertise.nl", "")
                End If
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
                Return False
            End Try
            Return True

        End Function
        Public ReadOnly Property Templates() As DataTable
            Get
                Return Me.oTemplates
            End Get

        End Property

        Function LoadTemplates(file As String) As Boolean
            Try
                If file.Length > 0 And System.IO.File.Exists(file) Then
                    Me.oSet.Clear()
                    Me.oSet.ReadXml(file)
                    Me.oTemplates = oSet.Tables("Templates")
                Else
                    Me.DefaultTemplates()
                End If
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
                Return False
            End Try
            Return True

        End Function
    End Class

End Namespace
