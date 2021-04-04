Imports System.Xml
Imports System.IO
Imports TEA.DLAFormfactory
Imports System.ComponentModel

Partial Public Class FrmReportingHelper
    Public oPackage As EA.Package
    Private oTemplates As New HTMLTemplates()
    Private oDef As New IDEADefinitions()
    Private oSearchReplace As New DataTable("SEARCHREPLACE")

    Public Property Package() As EA.Package
        Get
            Return oPackage
        End Get
        Set(ByVal value As EA.Package)
            oPackage = value
            Me.LabelPackage.Text = oPackage.Name
        End Set
    End Property

    Private oRepository As EA.Repository
    Public Property Repository() As EA.Repository
        Get
            Return oRepository
        End Get
        Set(ByVal value As EA.Repository)
            oRepository = value
            Me.oPackage = Me.Repository.GetTreeSelectedPackage()
            Me.LabelPackage.Text = oPackage.Name

        End Set
    End Property
    ''' <summary>
    ''' Press the HTML publish button and generate a HTML site based on the diagrams and packages
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonPublish_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonPublish.Click
        Dim oPublisher As New HTMLPublicator()
        'store the current settings for reuse
        Try
            'set the properties for the publisher
            oPublisher.Templates = Me.oTemplates
            oPublisher.CreatePDF = Me.CheckBoxCreatePDF.Checked
            oPublisher.CompositeClickable = Me.CheckBoxCompositeClickable.Checked
            oPublisher.Repository = Me.Repository
            oPublisher.Generate(Me.oPackage)
            If Me.CheckBoxDispayInBrowser.Checked = True Then
                Me.WebBrowserResult.Navigate(Me.TextBoxStartURL.Text)
                Me.DublicateTabControl.SelectTab("TabPageResult")
            End If

        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try

    End Sub


    Private Sub LoadTemplates()
        oTemplates.LoadTemplates(Me.TextBoxTemplatesFile.Text)
        Me.ListBoxTemplates.DataSource = oTemplates.Templates
        Me.ListBoxTemplates.ValueMember = "id"
        Me.ListBoxTemplates.DisplayMember = HTMLTemplates.T_NAME
    End Sub
    Private Sub FrmReportingHelper_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.oSearchReplace.Columns.Add(New DataColumn("Search"))
            Me.oSearchReplace.Columns.Add(New DataColumn("Replace"))
            Me.DataGridViewSearchReplace.DataSource = Me.oSearchReplace

            Me.TextBoxDiagramPackageSQL.Text = oDef.GetSettingValue("DiagramPackageSQL")
            Me.TextBoxElementPackageSQL.Text = oDef.GetSettingValue("ElementPackageSQL")
            Me.TextBoxElementDiagramSQL.Text = oDef.GetSettingValue("ElementDiagramSQL")
            Me.ComboBoxCoverPageTemplate.SelectedValue = My.Settings.PDFCoverPage
            Me.ComboBoxPackageTemplate.SelectedValue = My.Settings.PDFPackageTemplate
            Me.ComboBoxDiagramTemplate.SelectedValue = My.Settings.PDFDiagramTemplate
            Me.ComboBoxElementTemplate.SelectedValue = My.Settings.PDFElementTemplate

            Me.TextBoxHTMLPath.Text = oDef.GetSettingValue("HTMLPath")
            Me.TextBoxTemplatesFile.Text = oDef.GetSettingValue("HTMLTemplate")
            Me.TextBoxStartURL.Text = Me.TextBoxHTMLPath.Text & "SiteMap.html"
            Me.OpenFileDialogReport.InitialDirectory = oDef.GetSettingValue("HTMLPath")
            Me.ComboBoxCoverPageTemplate.DataSource = DLA2EAHelper.SQL2DataTable("select docname from t_document where doctype =  'SSDOCSTYLE' order by 1 ", Me.Repository)
            Me.ComboBoxPackageTemplate.DataSource = DLA2EAHelper.SQL2DataTable("select docname from t_document where doctype =  'SSDOCSTYLE' order by 1 ", Me.Repository)
            Me.ComboBoxDiagramTemplate.DataSource = DLA2EAHelper.SQL2DataTable("select docname from t_document where doctype =  'SSDOCSTYLE' order by 1 ", Me.Repository)
            Me.ComboBoxElementTemplate.DataSource = DLA2EAHelper.SQL2DataTable("select docname from t_document where doctype =  'SSDOCSTYLE' order by 1 ", Me.Repository)

            If Me.ComboBoxCoverPageTemplate.DataSource.Rows.Count > 0 Then
                Me.ComboBoxCoverPageTemplate.ValueMember = "docname"
                Me.ComboBoxCoverPageTemplate.DisplayMember = "docname"
            End If
            If Me.ComboBoxPackageTemplate.DataSource.Rows.Count > 0 Then
                Me.ComboBoxPackageTemplate.DisplayMember = "docname"
                Me.ComboBoxPackageTemplate.ValueMember = "docname"
            End If
            If Me.ComboBoxDiagramTemplate.DataSource.Rows.Count > 0 Then
                Me.ComboBoxDiagramTemplate.DisplayMember = "docname"
                Me.ComboBoxDiagramTemplate.ValueMember = "docname"
            End If
            If Me.ComboBoxElementTemplate.DataSource.rows.count > 0 Then
                Me.ComboBoxElementTemplate.DisplayMember = "docname"
                Me.ComboBoxElementTemplate.ValueMember = "docname"
            End If


        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
        Me.LoadTemplates()
    End Sub

    Private Sub ListBoxTemplates_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxTemplates.SelectedIndexChanged
        Dim oRow As DataRow
        oRow = Me.oTemplates.GetRowById(Me.ListBoxTemplates.SelectedValue.ToString())
        Me.TextBoxHeader.Text = oRow(HTMLTemplates.T_HEADER).ToString()
        Me.TextBoxBody.Text = oRow(HTMLTemplates.T_BODY).ToString()
        Me.TextBoxSQL.Text = oRow(HTMLTemplates.T_SQL).ToString()
        Me.TextBoxFooter.Text = oRow(HTMLTemplates.T_FOOTER).ToString()
        Me.TextBoxTemplateName.Text = oRow(HTMLTemplates.T_NAME).ToString()
    End Sub

    Private Sub ButtonUpdateTemplate_Click(sender As Object, e As EventArgs) Handles ButtonUpdateTemplate.Click
        Me.oTemplates.UpdateTemplate(Me.TextBoxTemplateName.Text, Me.TextBoxHeader.Text, Me.TextBoxBody.Text, Me.TextBoxFooter.Text, Me.ListBoxTemplates.SelectedValue.ToString, Me.TextBoxSQL.Text)
    End Sub

    Private Sub ButtonAddTemplate_Click(sender As Object, e As EventArgs) Handles ButtonAddTemplate.Click
        Me.oTemplates.AddTemplate(Me.TextBoxTemplateName.Text, Me.TextBoxHeader.Text, Me.TextBoxBody.Text, Me.TextBoxFooter.Text, Me.TextBoxSQL.Text)
    End Sub

    Private Sub ButtonReadTermplates_Click(sender As Object, e As EventArgs) Handles ButtonReadTermplates.Click
        Me.OpenFileDialogReport.RestoreDirectory = True
        If Me.OpenFileDialogReport.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Me.TextBoxTemplatesFile.Text = Me.OpenFileDialogReport.FileName
            Me.LoadTemplates()
        End If
    End Sub

    Private Sub ButtonCreateDocument_Click_1(sender As Object, e As EventArgs) Handles ButtonCreateDocument.Click
        Dim oPDF As New PDFCreator()
        oPDF.Package = Me.oPackage
        If Me.RadioButtonDocx.Checked Then
            oPDF.OutputType = "DOCX"
        Else
            oPDF.OutputType = "PDF"
        End If
        oPDF.SuppressEmptyNotes = Me.CheckBoxSuppressEmptyNotes.Checked
        oPDF.IncludeToC = Me.CheckBoxIncludeToC.Checked
        oPDF.IncludeChildPackages = Me.CheckBoxIncludeChildPackages.Checked
        oPDF.Repository = Me.Repository

        oPDF.MakePDFReport(oDef.GetSettingValue("HTMLPath") & Me.oPackage.Name & "." & oPDF.OutputType)
        Me.Close()
    End Sub

    Private Sub ButtonSelectDir_Click(sender As Object, e As EventArgs) Handles ButtonSelectDir.Click
        If Me.FolderBrowserDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Me.TextBoxHTMLPath.Text = Me.FolderBrowserDialog.SelectedPath + "\"
        End If
    End Sub

    Private Sub ButtonSaveTemplates_Click(sender As Object, e As EventArgs) Handles ButtonSaveTemplates.Click
        Try
            Me.oTemplates.UpdateTemplate(Me.TextBoxTemplateName.Text, Me.TextBoxHeader.Text, Me.TextBoxBody.Text, Me.TextBoxFooter.Text, Me.ListBoxTemplates.SelectedValue.ToString, Me.TextBoxSQL.Text)
            Me.oTemplates.SaveTemplates(Me.TextBoxTemplatesFile.Text)
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub

    Private Sub ButtonSortElements_Click(sender As Object, e As EventArgs) Handles ButtonSortElements.Click
        SortPackage(oPackage)
    End Sub
    Private Sub SortPackage(Package As EA.Package)
        Dim strSql As String = "select object_id FROM t_object where t_object.package_id = {0} order by {1} "
        Try
            oRepository.EnableUIUpdates = False
            If RadioButtonStereotype.Checked Then
                strSql = String.Format(strSql, Package.PackageID, "stereotype, name")
            End If
            If RadioButtonName.Checked Then
                strSql = String.Format(strSql, Package.PackageID, "name")
            End If
            If RadioButtonAlias.Checked Then
                strSql = String.Format(strSql, oPackage.PackageID, "alias")
            End If
            Dim oDT As DataTable
            oDT = DLA2EAHelper.SQL2DataTable(strSql, Me.Repository)
            Dim intTeller As Int32 = 1
            Dim oRow As DataRow
            Dim oElement As EA.Element
            DLA2EAHelper.SetProgressBarInit(Me.ProgressBar1, oDT.Rows.Count)
            For Each oRow In oDT.Rows
                oElement = oRepository.GetElementByID(oRow("object_id"))
                oElement.TreePos = intTeller
                intTeller += 1
                oElement.Update()
                Me.ProgressBar1.PerformStep()
            Next
            oRepository.RefreshModelView(oPackage.PackageID)
            oRepository.EnableUIUpdates = True
            If Me.CheckBoxRecursive.Checked Then
                For Each oSubPack As EA.Package In Package.Packages
                    SortPackage(oSubPack)
                Next
            End If
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub
    Private Sub ButtonRemoveNesting_Click(sender As Object, e As EventArgs) Handles ButtonRemoveNesting.Click
        RemoveNesting(oPackage)
        Me.Close()
    End Sub
    Private Sub RemoveNesting(Package As EA.Package)
        Dim strSql As String = "UPDATE t_object SET parentid = 0 WHERE parentid > 0 AND package_id = " + Package.PackageID.ToString()
        Me.Repository.Execute(strSql)
        Me.Repository.RefreshModelView(Me.oPackage.PackageID)
        If Me.CheckBoxRecursive.Checked Then
            For Each oSubPack As EA.Package In Package.Packages
                SortPackage(oSubPack)
            Next
        End If
    End Sub
    Private Sub ButtonReplace_Click(sender As Object, e As EventArgs) Handles ButtonReplace.Click
        Try
            oRepository.EnableUIUpdates = False
            SearchandReplace(oPackage)
            Me.Repository.RefreshModelView(oPackage.PackageID)
            oRepository.EnableUIUpdates = True
            Me.Close()
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try

    End Sub
    Private Sub SearchandReplace(Package As EA.Package)
        Dim intCount As Int32 = 1
        For Each oRow As DataRow In Me.oSearchReplace.Rows
            If CheckBoxReplaceName.Checked Then
                Package.Name = Package.Name.Replace(oRow("Search"), oRow("Replace"))
            End If
            If CheckBoxReplaceAlias.Checked Then
                Package.Alias = Package.Alias.Replace(oRow("Search"), oRow("Replace"))
            End If
            If CheckBoxReplaceNotes.Checked Then
                Package.Notes = Package.Notes.Replace(oRow("Search"), oRow("Replace"))
            End If
            intCount += Package.Elements.Count + Package.Diagrams.Count
            DLA2EAHelper.SetProgressBarInit(Me.ProgressBar1, intCount)
            ProgressBar1.PerformStep()
            For Each Element As EA.Element In Package.Elements
                If CheckBoxReplaceName.Checked Then
                    Element.Name = Element.Name.Replace(oRow("Search"), oRow("Replace"))
                End If
                If CheckBoxReplaceAlias.Checked Then
                    Element.Alias = Element.Alias.Replace(oRow("Search"), oRow("Replace"))
                End If
                If CheckBoxReplaceNotes.Checked Then
                    Element.Notes = Element.Notes.Replace(oRow("Search"), oRow("Replace"))
                End If
                Element.Update()
                ProgressBar1.PerformStep()
            Next
            For Each Diagram As EA.Diagram In Package.Diagrams
                If CheckBoxReplaceName.Checked Then
                    Diagram.Name = Diagram.Name.Replace(oRow("Search"), oRow("Replace"))
                End If

                If CheckBoxReplaceNotes.Checked Then
                    Diagram.Notes = Diagram.Notes.Replace(oRow("Search"), oRow("Replace"))
                End If
                Diagram.Update()
                ProgressBar1.PerformStep()
            Next
        Next
        Package.Update()
        If Me.CheckBoxRecursive.Checked Then
            For Each SubPack As EA.Package In Package.Packages
                SearchandReplace(SubPack)
            Next
        End If
    End Sub

    Private Sub FrmReportingHelper_HelpButtonClicked(sender As Object, e As CancelEventArgs) Handles Me.HelpButtonClicked
        MsgBox("Under construction")
    End Sub
End Class