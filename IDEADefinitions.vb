Imports System.IO
Imports TEA.DLAFormfactory

''' <summary>
''' Class voor het verwerken van definities, denk hierbij aan connecties, queries etc. 
''' Wordt gedaan op basis van een Dataset die via XML opgeslagen wordt in de config file
''' </summary>
Public Class IDEADefinitions
    Protected oIDEADefinitions As DataSet
    Sub New()
        Me.LoadDefinitions()
        Me.LoadFromSettings()
    End Sub
    ''' <summary>
    ''' Laden van het metamodel van de definities in de vorm van een dataset met datatables
    ''' </summary>
    Sub LoadDefinitions()
        Try
            Me.oIDEADefinitions = New DataSet("Definitions")
            Dim objSettings As DataTable
            objSettings = New DataTable("Settings")
            objSettings.Columns.Add(New DataColumn("Name", System.Type.GetType("System.String")))
            objSettings.Columns.Add(New DataColumn("Type", System.Type.GetType("System.String")))
            objSettings.Columns.Add(New DataColumn("Value", System.Type.GetType("System.String")))
            Me.oIDEADefinitions.Tables.Add(objSettings)

            Dim objSQL As DataTable
            objSQL = New DataTable("SQL-Statement")
            objSQL.Columns.Add(New DataColumn("Name", System.Type.GetType("System.String")))
            objSQL.Columns.Add(New DataColumn("Type", System.Type.GetType("System.String")))
            objSQL.Columns.Add(New DataColumn("Statement", System.Type.GetType("System.String")))
            objSQL.Columns.Add(New DataColumn("Template", System.Type.GetType("System.String")))
            Me.oIDEADefinitions.Tables.Add(objSQL)
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub

    Sub LoadFromSettings()
        Dim oDT As DataTable
        Try
            oDT = DLA2EAHelper.SQL2DataTable("SELECT t_objectproperties.notes, t_objectproperties.property as name FROM t_object, t_objectproperties WHERE t_object.object_type = 'Artifact'  AND t_object.name = 'IDEASettings' AND t_object.object_id = t_objectproperties.object_id", GetObject(, "EA.App").Repository)
            For Each oRow As DataRow In oDT.Rows
                Dim strText As String
                strText = oRow("Notes")
                LoadDataSetXML(strText, "", oRow("Name"))
            Next
            If oDT.Rows.Count = 0 Then
                LoadDataSetXML(My.Settings.SettingsTable, "", "Settings")
                LoadDataSetXML(My.Settings.StatementsTable, "", "SQL-Statement")
            End If
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub
    ''' 
    ''' 
    ''' <summary>
    ''' Laden van de dataset met definities vanuit de configuratie file f uit de vulling van het result tabblad.
    ''' Als er geen materiaal gevonden wordt worden er een paar voorbeeld items gemaakt
    ''' </summary>
    ''' <param name="xmlstring">Dataset definities uit de config file</param>
    ''' <param name="importstring">Dataset afkomstig vanuit het result scherm</param>
    Sub LoadDataSetXML(xmlstring As String, importstring As String, Tablename As String)
        Try
            Me.oIDEADefinitions.Tables(Tablename).Rows.Clear()
            If xmlstring.Length > 0 Or importstring.Length > 0 Then
                Dim oSR As New StringReader(IIf(importstring.Length > 10, importstring, xmlstring))
                Me.oIDEADefinitions.ReadXml(oSR, XmlReadMode.IgnoreSchema)
            End If
            System.IO.Directory.CreateDirectory("c:\idea")
            Me.AddInitialSetting("LogFile", "c:\idea\error.log", "FILES")
            Me.AddInitialSetting("ReleaseDirectory", "c:\idea", "FILES")
            Me.AddInitialSetting("HTMLPath", "c:\idea\", "HTML")
            'path for the WPP diagram name for direct export when saving ther diagram
            Me.AddInitialSetting("WPP_Diagram_File", "?", "HTML")
            Me.AddInitialSetting("HTMLTemplate", "c:\idea\templates\basis_template.html", "HTML")
            Me.AddInitialSetting("TemplateReplaceValue", "DEFAULT", "AID")
            Me.AddInitialSetting("ExcelConnection", "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=[FILE];Extended Properties=""Excel 12.0 Xml;HDR=YES"";", "EXCEL")
            Me.AddInitialSetting("ElementPackageSQL", "SELECT * FROM t_object where package_id = #package_id# ORDER BY name", "RTF")
            Me.AddInitialSetting("DiagramPackageSQL", "SELECT * FROM t_diagram where package_id = #package_id# ORDER BY name", "RTF")
            Me.AddInitialSetting("ElementDiagramSQL", "SELECT * FROM t_diagramobjects where diagram_id = #diagram_id# ORDER BY RectTop, RectLeft", "RTF")
            Me.AddInitialSetting("ConnectionString", "Provider=SQLOLEDB;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=DLAdministratie2018;Data Source=DESKTOP-HNMVP79;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;Workstation ID=DESKTOP-HNMVP79;Use Encryption for Data=False;Tag with column collation when possible=False;", "FORMFACTORY")
            Me.AddInitialSetting("DeduplicateElementsPackage", "Select orig.name, orig.stereotype, orig.author, orig.version, dupl.name as duplicate_name, t_package.name as package_name, dupl.author as duplicate_author, orig.object_id as origid, dupl.object_id as duplid From t_object orig, t_package, t_object dupl where dupl.package_id = t_package.package_id and orig.name = dupl.name and orig.object_id <> dupl.object_id and (orig.stereotype = dupl.stereotype or (orig.stereotype IS NULL AND dupl.stereotype IS NULL)) and orig.object_type = dupl.object_type and orig.object_type NOT IN('Package', 'Diagram', 'Note', 'Text', 'ProxyConnector') and orig.name IS NOT NULL and orig.name <> '' and orig.version = dupl.version and orig.package_id IN(#package_id#) order by 1 ", "DEDUPL")
            Me.AddInitialSetting("DeduplicateElementElement", "Select dupl.name as duplicate_name, t_package.name as package_name, dupl.author as duplicate_author, orig.object_id as origid, dupl.object_id as duplid From t_object orig, t_package, t_object dupl where dupl.package_id = t_package.package_id and orig.name = dupl.name and orig.object_id <> dupl.object_id and (orig.stereotype = dupl.stereotype or (orig.stereotype IS NULL AND dupl.stereotype IS NULL)) and orig.object_type = dupl.object_type and orig.object_type NOT IN('Package', 'Diagram', 'Note', 'Text', 'ProxyConnector') and orig.name IS NOT NULL and orig.name <> '' and orig.version = dupl.version and orig.object_id = #object_id# order by 1 ", "DEDUPL")
            Me.AddInitialSetting("DeduplicateConnectorsPackage", "Select distinct startobj.name As object_name, orig.name, orig.connector_type,  dupl.name As dupl_name, destobj.name as dupl_object_name, destobj.author as dupl_author, dupl.direction, orig.connector_id as origid, dupl.connector_id as duplid From t_connector orig, t_connector dupl, t_object startobj, t_object destobj Where ((orig.name = dupl.name) Or (orig.name Is null And dupl.name Is null)) And orig.connector_type = dupl.connector_type And orig.end_object_id = dupl.end_object_id And orig.start_object_id = dupl.start_object_id And orig.start_object_id = startobj.Object_ID And orig.end_object_ID = destobj.object_id And Orig.direction = dupl.direction And (orig.styleex IS NULL OR orig.styleex not like '%LFSP=%') And (orig.stereotype = dupl.stereotype Or orig.stereotype Is NULL) And orig.Connector_ID <> dupl.connector_id And startobj.package_id IN(#package_id#) ", "DEDUPL")
            Me.AddInitialSetting("WasteBinPackage_id", "-999", "WASTEBIN")

            'DMF raamwerk
            Me.AddInitialSQL("MappingColumn", "DMF",
                                   "SELECT DISTINCT CASE WHEN t_connector.name IS NULL THEN t_connector.name ELSE concat('_',t_connector.name) END AS connectorname, t_connector.direction, attrsource.name AS sourcecolumn , attrtarget.name +  CASE WHEN targetprops.value = 'H'  THEN '_BK' +  CASE WHEN t_connector.name IS NULL  THEN ''  ELSE concat('_',t_connector.name)  END  ELSE '' END AS targetcolumn , classsource.name AS sourcetable, classtarget.name AS targettable, t_diagram.name AS diagram_name, targetprops.value AS tableType FROM t_connector JOIN t_diagramlinks ON t_diagramlinks.ConnectorID = t_connector.connector_id JOIN t_diagram ON t_diagram.Diagram_ID = t_diagramlinks.DiagramID JOIN t_object AS classsource ON classsource.object_id = t_connector.start_object_id JOIN t_attribute AS attrsource ON attrsource.object_id = classsource.object_id JOIN t_object AS classtarget ON classtarget.object_id = t_connector.end_object_id JOIN t_attribute AS attrtarget ON attrtarget.object_id = classtarget.object_id LEFT OUTER JOIN t_objectproperties AS targetprops ON targetprops.Object_ID = classtarget.Object_ID WHERE t_diagram.diagram_id = #diagram_id# AND t_connector.styleex LIKE '%LFSP=' + attrsource.ea_guid + '%' AND t_connector.styleex LIKE '%LFEP=' + attrtarget.ea_guid + '%' AND targetprops.Property = 'TableType' ",
                                   "INSERT INTO MGT.DynamicLoadDataVault_AttributeMappingInput (LoadDate,UserName,SourceTable,SourceAttribute,Rol,StageName,TargetTable,TargetAttribute) VALUES (CONVERT(DATETIME2(6),GETDATE(),121),CURRENT_USER,'#sourcetable#','#sourcecolumn#','#connectorname#','#sourcetable#_vw','#targettable#','#targetcolumn#') ")
            Me.AddInitialSQL("MappingTable", "DMF", "SELECT DISTINCT classsource.name  AS sourcetable, dbo.nsda_gettaggedvalue(classsource.Object_ID, 'Owner') AS sourceschema , LEFT(classsource.name, CHARINDEX('_', classsource.name)-1) AS sourcerecordsource , classtarget.name  AS targettable , dbo.nsda_gettaggedvalue(classtarget.Object_ID, 'Owner') AS targetschema , CASE WHEN t_connector.name IS NULL THEN t_connector.name ELSE CONCAT('_',t_connector.name) END  AS connectorname , (SELECT t_connectorconstraint.notes  FROM t_connectorconstraint WHERE connectorid = t_connector.connector_id )  AS connectorfilter FROM t_connector, t_diagramlinks, t_diagram, t_object AS classsource, t_object AS classtarget WHERE t_connector.connector_id = t_diagramlinks.ConnectorID AND t_diagramlinks.DiagramID = t_diagram.Diagram_ID AND t_connector.start_object_id = classsource.object_id AND t_connector.end_object_id = classtarget.object_id AND t_connector.styleex IS NULL AND t_diagram.diagram_id = #diagram_id# ",
                                   "INSERT INTO MGT.DynamicLoadDataVault_MappingInput (LoadDate,UserName,RecordSource,SourceSchema,SourceTableName,StageSchema,StageName,TargetSchema,TargetTableName,Rol,SourceFilter,CreateView) VALUES (CONVERT(DATETIME2(6),GETDATE(),121),CURRENT_USER,'#sourcerecordsource#','#sourceschema#','#sourcetable#','#sourceschema#','#sourcetable#_vw','#targetschema#','#targettable#','#connectorname#','#connectorfilter#','1')")

            'Housekeeping
            Me.AddInitialSQL("HousekeepingLink", "Housekeeping", "select bom_object.bom_id
, bom_object.bom_naam
, bom_object.bewaartermijn
, bom_object.bewaartermijneenheid
, bom_object.bewaartermijnvanafwanneer
, table_object.object_name as [tabelnaam] 
, dbo.nsda_gettaggedvalue(table_object.object_id, 'Owner') as [tableschema]
--, tablediagramobjects.Diagram_ID
from nsda_object as [ldm_object] 
, nsda_bombewaartermijn as [bom_object] 
, nsda_object as [table_object] 
, nsda_connector as [flcon]
, nsda_connector as [lccon]
, t_diagramobjects as tablediagramobjects 
where flcon.end_object_id = ldm_object.object_id and flcon.start_object_id = table_object.object_id
and lccon.start_object_id = ldm_object.object_id 
and lccon.end_object_id = bom_object.bom_id 
and ldm_object.object_type = 'Class' 
and table_object.object_type = 'table' 
and ldm_object.object_name is not null 
and table_object.object_id = tablediagramobjects.object_id 
and tablediagramobjects.diagram_id = #diagram_id#",
            "INSERT INTO [PBD].[META_FBDOM_DV_Relaties] ([FBDOM_id], [FBDOMNaam], [bewaartermijn], [bewaartermijneenheid], [bewaartermijnvanafwanneer], [Tabelowner],  [Tabelnaam] ) 
            VALUES ('#bom_id#', '#bom_naam#', '#bewaartermijn#', '#bewaartermijneenheid#', '#bewaartermijnvanafwanneer#', '#tableschema#',  '#tabelnaam#' );")

            Me.AddInitialSQL("HousekeepingPDM", "Housekeeping",
                             "select distinct dbo.nsda_gettaggedvalue(parent_object.object_id, 'Owner') as [parentowner]
            , parent_object.object_name as [parentname]
            ,  dbo.nsda_gettaggedvalue(child_object.object_id, 'Owner') as [childowner]
            , child_object.object_name as [childname] 
            from [nsda_object] as [parent_object], nsda_object as [child_object], nsda_connector as [con], t_diagramobjects as parentdiagramlinks 
            where con.start_object_id = child_object.object_id and con.end_object_id = parent_object.object_id and parent_object.object_type = 'table' and child_object.object_type = 'table' and con.destcard = '1' and parent_object.object_name is not null and parent_object.object_id = parentdiagramlinks.object_id and parentdiagramlinks.diagram_id = #diagram_id# 
            union 
            select distinct dbo.nsda_gettaggedvalue(parent_object.object_id, 'Owner') as [parentowner], parent_object.object_name as [parentname]
            , dbo.nsda_gettaggedvalue(child_object.object_id, 'Owner') as [childowner]
            , child_object.object_name as [childname] 
            from [nsda_object] as [parent_object], nsda_object as [child_object], nsda_connector as [con] , t_diagramobjects as parentdiagramlinks 
            where con.end_object_id = child_object.object_id and con.start_object_id = parent_object.object_id and parent_object.object_type = 'table' and child_object.object_type = 'table' and con.destcard = '1' and parent_object.object_name is not null and parent_object.object_id = parentdiagramlinks.object_id and parentdiagramlinks.diagram_id = #diagram_id# 
             ",
            "INSERT INTO [PBD].[META_DV_TABELRELATIES] ([parentowner], [parentname], [childowner],  [childname] ) VALUES ('#parentowner#', '#parentname#',  '#childowner#',  '#childname#' );")
        Catch ex As Exception
            MsgBox("Error in setting config", MsgBoxStyle.OkOnly)
        End Try

    End Sub
    ''' <summary>
    ''' Zet de dataset om naar een xml string tekst die opgeslagen wordt in bijvoorbeeld de config file
    ''' </summary>
    ''' <returns></returns>
    'Function SaveDataSetXML(strFile As String) As String
    '    Dim strWriter As New StringWriter()
    '    Try
    '        Me.oIDEADefinitions.WriteXml(strWriter, XmlWriteMode.IgnoreSchema)
    '        If strFile.Length > 0 Then
    '            Me.oIDEADefinitions.WriteXml(strFile, XmlReadMode.IgnoreSchema)
    '        End If
    '        Return strWriter.ToString()
    '    Catch ex As Exception
    '        DLA2EAHelper.Error2Log(ex)
    '    End Try
    '    Return ""
    'End Function
    ''' <summary>
    ''' Zoek in een dataset collectie naar een filter, activeer deze en retourneer de gefilterde subset
    ''' </summary>
    ''' <param name="table">Naam van de datatable die uitgevraagd wordt</param>
    ''' <param name="filter">Filterstring waarop je moet filteren</param>
    ''' <returns></returns>
    Function GetFilteredTable(table As String, filter As String) As DataTable
        Dim oDV As DataView
        Try
            oDV = New DataView(Me.oIDEADefinitions.Tables(table))
            oDV.RowFilter = filter
            Return oDV.ToTable()
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
        Return Nothing
    End Function
    ''' <summary>
    ''' utility routine om de waarde van een configitem op te vragen op basis van de naam
    ''' Indien de naam niet gevonden wordt wordt een lege string geretourneerd
    ''' </summary>
    ''' <param name="name">Naam van het configuratie item</param>
    ''' <returns>Waarde van de configuratie item</returns>
    Function GetSettingValue(name As String) As String
        Dim DT As DataTable
        DT = Me.GetFilteredTable("SETTINGS", String.Format("name = '{0}' ", name))
        If DT.Rows.Count > 0 Then
            Dim Row As DataRow
            Row = DT.Rows(0)
            Return Row("Value")
        End If
        Return ""
    End Function
    ''' <summary>
    ''' Ophalen van een datatable op basis van de naam
    ''' </summary>
    ''' <param name="table"></param>
    ''' <returns></returns>
    Function GetTable(table As String) As DataTable
        Try
            Return Me.oIDEADefinitions.Tables(table)
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
        Return Nothing
    End Function
    Sub AddInitialSetting(Name As String, Value As String, Optional Type As String = "Config")
        If Me.GetSettingValue(Name).Length = 0 Then
            Me.AddSetting(Name, Value)
        End If
    End Sub

    Sub AddInitialSQL(Name As String, Type As String, Statement As String, Template As String)
        If Me.GetFilteredTable("SQL-Statement", String.Format(" Name = '{0}' ", Name)).Rows.Count = 0 Then
            Me.AddStatement(Name, Type, Statement, Template)
        End If
    End Sub
    Sub AddSetting(Name As String, Value As String, Optional Type As String = "Config")
        Try
            Dim oRow As DataRow
            oRow = Me.oIDEADefinitions.Tables("Settings").NewRow()
            oRow("Name") = Name
            oRow("Value") = Value
            oRow("Type") = Type
            Me.oIDEADefinitions.Tables("Settings").Rows.Add(oRow)
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub

    Sub AddStatement(Name As String, Type As String, Statement As String, Template As String)
        Try
            Dim oRow As DataRow
            oRow = Me.oIDEADefinitions.Tables("SQL-Statement").NewRow()
            oRow("Name") = Name
            oRow("Type") = Type
            oRow("Statement") = Statement
            oRow("Template") = Template
            Me.oIDEADefinitions.Tables("SQL-Statement").Rows.Add(oRow)
        Catch ex As Exception
            DLA2EAHelper.Error2Log(ex)
        End Try
    End Sub
End Class
