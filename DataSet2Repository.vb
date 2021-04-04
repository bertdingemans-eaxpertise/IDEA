Namespace DLAFormfactory


    ''' <summary>
    ''' Helper class for transforming output from the Sparx API in a more usable format
    ''' within a DotNet (windows) application.
    ''' </summary>
    Public Class DataSet2Repository
        Private _Repository As EA.Repository
        Public fouten As String = ""

        ''' <summary>
        ''' Bewaar de package property voor het aanmaken van elementen in dit package
        ''' </summary>
        Private _Package_id As String
        Public Property Package_Id() As String
            Get
                Return _Package_id
            End Get
            Set(ByVal value As String)
                _Package_id = value
            End Set
        End Property
        ''' <summary>
        ''' neem een verwijzing op naar de repository omdat dat tikwerk bespaart
        ''' </summary>
        ''' <returns></returns>
        Public Property Repository() As EA.Repository
            Get
                Return _Repository
            End Get
            Set(ByVal value As EA.Repository)
                _Repository = value
            End Set
        End Property

        Public Function AddElementAndConnector(Parent As EA.Element, objRow As DataRow, Fieldname As String, objStereoType As String, ConStereoType As String, Optional PostFix As String = "") As EA.Element
            Try
                Dim objChild As EA.Element
                If Not IsDBNull(objRow.Item(Fieldname)) Then
                    Dim strChild As String = objRow.Item(Fieldname) + PostFix
                    objChild = Me.FindOrAddElement(strChild, objStereoType, False)
                    objChild.Status = "Import4Hub"
                    objChild.Update()
                    Dim objCon As EA.Connector
                    objCon = Me.AddConnector(objChild, Parent, ConStereoType)
                    objCon.Update()
                    Return objChild
                End If
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
            Return Nothing
        End Function

        ''' <summary>
        ''' Voeg een attribuut toe aan een element
        ''' </summary>
        ''' <param name="objElement"></param>
        ''' <param name="strName"></param>
        ''' <param name="strType"></param>
        ''' <returns></returns>
        Public Function AddAttribute(ByVal objElement As EA.Element, ByVal strName As String, ByVal strType As String) As EA.Element

            Dim objAttribute As EA.Attribute
            If Not IsDBNull(strName) Then
                objAttribute = objElement.Attributes.AddNew(strName, strType)
                objElement.Update()
            End If
            Return objElement
        End Function
        ''' <summary>
        ''' Voeg een tagged value toe aan een element
        ''' </summary>
        ''' <param name="Element"></param>
        ''' <param name="name"></param>
        ''' <param name="value"></param>
        ''' <param name="isMemo"></param>
        Public Sub AddTaggedValue(ByVal Element As EA.Element, ByVal name As String, ByVal value As String, ByVal isMemo As Boolean)
            Try
                Dim objTV As EA.TaggedValue
                If Not String.IsNullOrEmpty(value) And value <> ChrW(129) Then
                    If isMemo Then
                        objTV = Element.TaggedValues.AddNew(name, "<memo>")
                        objTV.Notes = value
                    Else
                        objTV = Element.TaggedValues.AddNew(name, value)
                    End If
                    objTV.Update()
                End If
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
        End Sub

        Public Sub AddOrUpdateTaggedValue(ByVal Element As EA.Element, ByVal name As String, Row As DataRow, ByVal isMemo As Boolean)
            If Not IsDBNull(Row.Item(name)) Then
                Me.AddOrUpdateTaggedValue(Element, name, Row.Item(name), isMemo)
            End If
        End Sub
        Public Sub AddOrUpdateTaggedValue(ByVal Element As EA.Element, ByVal tvname As String, rowname As String, Row As DataRow, ByVal isMemo As Boolean)
            If Not IsDBNull(Row.Item(rowname)) Then
                Me.AddOrUpdateTaggedValue(Element, tvname, Row.Item(rowname).ToString(), isMemo)
            End If
        End Sub
        Public Shared Function AddAttribute(ByVal Element As EA.Element, ByVal name As String, ByVal type As String, Note As String) As EA.Attribute
            Try
                Dim objAttribute As EA.Attribute
                objAttribute = Element.Attributes.AddNew(name, type)
                objAttribute.Notes = Note
                objAttribute.Update()
                Element.Update()
                Return objAttribute
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
            Return Nothing
        End Function
        Public Shared Function GetTaggedValue(ByVal Element As EA.Element, ByVal name As String) As String
            Dim objTV As EA.TaggedValue
            Dim Found As Boolean = False
            Dim strRet As String = ""
            Try

                For Each objTV In Element.TaggedValues
                    If objTV.Name = name Then
                        If objTV.Value = "<memo>" Then
                            strRet = objTV.Notes
                            Found = True
                        Else
                            strRet = objTV.Value
                        End If
                        Exit For
                    End If
                Next
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
            Return strRet
        End Function
        ''' <summary>
        ''' Voeg een tagged value toe aan een element en als deze al bestaat update dan de inhoud van deze tagged value
        ''' </summary>
        ''' <param name="Element"></param>
        ''' <param name="name"></param>
        ''' <param name="value"></param>
        ''' <param name="isMemo"></param>
        ''' 
        Public Sub AddOrUpdateTaggedValue(ByVal Element As EA.Element, ByVal name As String, ByVal value As String, ByVal isMemo As Boolean)
            Try
                Dim objTV As EA.TaggedValue
                Dim Found As Boolean = False
                If Not String.IsNullOrEmpty(value) And value <> ChrW(129) Then
                    For Each objTV In Element.TaggedValues
                        If objTV.Name = name Then
                            Found = True
                            Exit For
                        End If
                    Next
                    If isMemo Then
                        If Found = False Then
                            objTV = Element.TaggedValues.AddNew(name, "<memo>")
                        End If
                        objTV.Notes = value
                    Else
                        If Found = False Then
                            objTV = Element.TaggedValues.AddNew(name, value)
                        Else
                            objTV.Value = value
                        End If
                    End If
                    objTV.Update()
                End If
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Op basis van een stereotype moet een type bepaald worden voor het aanmaken van nieuwe elementen. Deze routine handelt dat af, zou mooier zijn met een array van items
        ''' </summary>
        ''' <param name="stereotype"></param>
        ''' <returns></returns>
        Public Function ConvertStereotype2Type(ByVal stereotype As String) As String
            Dim strRet As String = "Class"
            Select Case stereotype.ToUpper()
                Case "ARCHIMATE_APPLICATIONCOMPONENT"
                    strRet = "Component"
                Case "ARCHIMATE_BUSINESSROLE"
                    strRet = "BusinessRole"
                Case "ARCHIMATE_GROUPING"
                    strRet = "Grouping"
                Case "ARCHIMATE_APPLICATIONINTERFACE"
                    strRet = "Interface"
                Case "ARCHIMATE_BUSINESSFUNCTION"
                    strRet = "Activity"
                Case "ARCHIMATE_APPLICATIONFUNCTION"
                    strRet = "ApplicationFunction"
                Case "ARCHIMATE_LOCATION"
                    strRet = "Class"
                Case "ARCHIMATE_WORKPACKAGE"
                    strRet = "WorkPackage"
                    'Case "ARCHIMATE_DELIVERABLE"
                    '    strRet = "Deliverable"
                Case "ARCHIMATE_CAPABILIY"
                    strRet = "Capability"
                Case "ARCHIMATE_VALUE"
                    strRet = "Class"
                Case "ARCHIMATE_PLATEAU"
                    strRet = "Plateau"
                Case "ARCHIMATE_OUTCOME"
                    strRet = "Outcome"
                Case "ARCHIMATE_GOAL"
                    strRet = "Goal"
                Case "ARCHIMATE_DRIVER"
                    strRet = "Driver"
                Case "ARCHIMATE_STAKEHOLDER"
                    strRet = "Stakeholder"
                Case Else
                    strRet = "Class"
            End Select
            Return strRet
        End Function
        ''' <summary>
        ''' Converteren van een connector stereotype naar een type hebben we nodig voor het aanmaken van nieuwe connectoren
        ''' </summary>
        ''' <param name="stereotype"></param>
        ''' <returns></returns>
        Public Function ConvertAssociationStereotype2Type(ByVal stereotype As String) As String
            Dim strRet As String = "Association"
            Select Case stereotype.ToUpper()
                Case "ARCHIMATE_AGGREGATION"
                    strRet = "Aggregation"
            End Select
            Return strRet
        End Function
        ''' <summary>
        ''' Toevoegen van een element aan de repository
        ''' </summary>
        ''' <param name="stereotype"></param>
        ''' <param name="name"></param>
        ''' <param name="memo"></param>
        ''' <returns></returns>
        Public Function AddElement(ByVal stereotype As String, ByVal name As String, ByVal memo As String) As EA.Element
            Dim objPack As EA.Package
            Dim objElement As EA.Element
            Try
                If Me.Package_Id.Contains("{") Then
                    objPack = _Repository.GetPackageByGuid(Me.Package_Id)
                Else
                    objPack = _Repository.GetPackageByID(Convert.ToInt32(Me.Package_Id))
                End If
                objElement = objPack.Elements.AddNew(name, ConvertStereotype2Type(stereotype))
                If Not String.IsNullOrEmpty(stereotype) Then
                    objElement.Stereotype = stereotype
                End If
                If Not String.IsNullOrEmpty(memo) Then
                    objElement.Notes = memo
                End If
                objElement.Update()
                objPack.Update()
            Catch ex As Exception
                MsgBox(ex.ToString())
            End Try
            Return objElement
        End Function
        ''' <summary>
        ''' Kijk op basis van de name van een element of deze al bestaat in de repository
        ''' </summary>
        ''' <param name="name"></param>
        ''' <returns></returns>
        Public Function FindElement(ByVal name As String) As EA.Element
            Dim objPack As EA.Package
            Dim objElement As New EA.Element()
            Try
                objPack = _Repository.GetPackageByID(Me.Package_Id)
                Dim strSql As String
                strSql = String.Format("SELECT object_id FROM t_object WHERE name = '{0}' ", name)
                Dim objDT As DataTable
                objDT = DLA2EAHelper.SQL2DataTable(strSql, Me.Repository)
                If objDT.Rows.Count = 1 Then
                    objElement = Me.Repository.GetElementByID(objDT.Rows(0).Item("object_id"))
                End If
            Catch ex As Exception
                fouten += "Fout in FindElement" & ex.Message & vbCrLf
            End Try
            Return objElement
        End Function

        Public Function FindPackage(ByVal name As String) As EA.Package
            Dim objPack As EA.Package
            Try
                Dim strSql As String
                strSql = String.Format("SELECT package_id FROM t_package WHERE name = '{0}' ", name)
                Dim objDT As DataTable
                objDT = DLA2EAHelper.SQL2DataTable(strSql, Me.Repository)
                If objDT.Rows.Count = 1 Then
                    objPack = Me.Repository.GetPackageByID(objDT.Rows(0).Item("package_id"))
                Else
                    objPack = Nothing
                End If
            Catch ex As Exception
                fouten += "Fout in FindPackage" & ex.Message & vbCrLf
            End Try
            Return objPack
        End Function
        ''' <summary>
        ''' Kijk op basis van de name en stereotype van een element of deze al bestaat in de repository
        ''' </summary>
        ''' <param name="name"></param>
        ''' <param name="stereotype"></param>
        ''' <returns></returns>
        Public Function FindElement(ByVal name As String, ByVal stereotype As String) As EA.Element
            Dim objPack As EA.Package
            Dim objElement As EA.Element

            Try
                objPack = _Repository.GetPackageByID(Me.Package_Id)
                Dim strSql As String
                strSql = String.Format("SELECT object_id FROM t_object WHERE (alias = '{0}' or name = '{0}')  AND stereotype = '{1}'", name, stereotype)
                Dim objDT As DataTable
                objDT = DLA2EAHelper.SQL2DataTable(strSql, Me.Repository)
                If objDT.Rows.Count = 1 Then
                    objElement = Me.Repository.GetElementByID(objDT.Rows(0).Item("object_id"))
                End If
            Catch ex As Exception
                fouten += "Fout in FindElement" & ex.Message & vbCrLf
            End Try
            Return Nothing
        End Function
        ''' <summary>
        ''' Zoek naar een element in de repo en als deze niet bestaat maak dan een nieuwe aan
        ''' </summary>
        ''' <param name="name"></param>
        ''' <param name="stereotype"></param>
        ''' <param name="Found"></param>
        ''' <returns></returns>
        Public Function FindOrAddElement(ByVal name As String, ByVal stereotype As String, ByRef Found As Boolean) As EA.Element
            Return Me.FindOrAddElement(name, stereotype, Found, "")
        End Function
        ''' <summary>
        ''' Zoek naar een element in de repo en als deze niet bestaat maak dan een nieuwe aan
        ''' Extra is hier dat een memo voor de beschrijving kan worden toegevoegd
        ''' </summary>
        ''' <param name="name"></param>
        ''' <param name="stereotype"></param>
        ''' <param name="Found"></param>
        ''' <param name="memo"></param>
        ''' <returns></returns>
        Public Function FindOrAddElement(ByVal name As String, ByVal stereotype As String, ByRef Found As Boolean, ByVal memo As String) As EA.Element
            Dim objElement As EA.Element

            objElement = Me.FindElement(name, stereotype, Found)
            If Found = False And name.Length > 0 Then
                objElement = Me.AddElement(stereotype, name, memo)
                Found = True
            End If
            Return objElement
        End Function
        ''' <summary>
        ''' Zoek een element op basis van naam en stereotype binnen een package collection dat ingesteld is als instance variable
        ''' </summary>
        ''' <param name="name">Naam van het element</param>
        ''' <param name="stereotype">Stereotye van het element</param>
        ''' <param name="Found"></param>
        ''' <returns></returns>
        Public Function FindElement(ByVal name As String, ByVal stereotype As String, ByRef Found As Boolean) As EA.Element
            Dim objPack As EA.Package
            Dim objElement As EA.Element

            Try
                objPack = _Repository.GetPackageByID(Me.Package_Id)
                Dim strSql As String
                strSql = String.Format("SELECT object_id FROM t_object WHERE (alias = '{0}' or name = '{0}')  AND stereotype = '{1}'", name.Replace("'", "''"), stereotype)
                Dim objDT As DataTable
                objDT = DLA2EAHelper.SQL2DataTable(strSql, Me.Repository)

                If Not IsNothing(objDT) Then
                    If objDT.Rows.Count > 0 Then
                        objElement = Me.Repository.GetElementByID(objDT.Rows(0).Item("object_id"))
                        Found = True
                        Return objElement
                    Else
                        Found = False
                    End If
                Else
                    Found = False
                End If
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
            Return Nothing
        End Function
        ''' <summary>
        ''' zoek een element op basis van een tagged value naam en waarde
        ''' wordt gebruikt als er een tagged value is voor bijvoorbeeld een externe sleutel
        ''' </summary>
        ''' <param name="name"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Function FindElementByTaggedValue(ByVal name As String, ByVal value As String) As EA.Element
            Dim objPack As EA.Package
            Dim objElement As EA.Element

            Try
                objPack = _Repository.GetPackageByID(Me.Package_Id)
                Dim strSql As String
                strSql = String.Format("select t_object.object_id from t_object, t_objectproperties where t_object.Object_ID = t_objectproperties.Object_ID and t_objectproperties.Property = '{0}' and t_objectproperties.value = '{1}'", name, value)
                Dim objDT As DataTable
                objDT = DLA2EAHelper.SQL2DataTable(strSql, Me.Repository)
                If Not IsNothing(objDT) Then
                    '            And objDT.Rows.Count = 1 Then
                    objElement = Me.Repository.GetElementByID(objDT.Rows(0).Item("object_id"))
                    Return objElement
                End If
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
            Return Nothing
        End Function
        ''' <summary>
        ''' Zoek een element in de repository op basis van een alias en een stereotype combinatie
        ''' </summary>
        ''' <param name="aliasname">gebruikte aliasnaam</param>
        ''' <param name="stereotype">gebruikte stereotype</param>
        ''' <returns></returns>
        Public Function FindElementByAlias(ByVal aliasname As String, ByVal stereotype As String) As EA.Element
            Dim objPack As EA.Package
            Dim objElement As EA.Element

            Try
                objPack = _Repository.GetPackageByID(Me.Package_Id)
                Dim strSql As String
                strSql = String.Format("SELECT object_id FROM t_object WHERE alias = '{0}' AND stereotype = '{1}'", aliasname, stereotype)
                Dim objDT As DataTable
                objDT = DLA2EAHelper.SQL2DataTable(strSql, Me.Repository)

                If objDT.Rows.Count = 1 Then
                    objElement = Me.Repository.GetElementByID(objDT.Rows(0).Item("object_id"))
                    Return objElement
                End If
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
            Return Nothing
        End Function
        ''' <summary>
        ''' voeg een connector toe aan de repository obv een dataset
        ''' </summary>
        ''' <param name="Source"></param>
        ''' <param name="Target"></param>
        ''' <param name="stereotype"></param>
        ''' <returns></returns>
        Public Function AddConnector(ByVal Source As EA.Element, ByVal Target As EA.Element, ByVal stereotype As String) As EA.Connector
            Return Me.AddConnector(Source, Target, stereotype, "")
        End Function

        ''' <summary>
        ''' voeg een connector toe aan de repository obv een dataset
        ''' </summary>
        ''' <param name="Source"></param>
        ''' <param name="Target"></param>
        ''' <param name="stereotype"></param>
        ''' <returns></returns>
        Public Function AddConnector(ByVal Source As EA.Element, ByVal Target As EA.Element, ByVal stereotype As String, ByVal Melding As String) As EA.Connector
            Dim objPack As EA.Package
            Dim objConnector As EA.Connector
            Dim blnFound As Boolean = False

            Try
                objPack = _Repository.GetPackageByID(Me.Package_Id)
                'eerst controleren of de connector niet al bestaat
                'For Each objConnector In Source.Connectors.
                '    If objConnector.Stereotype = stereotype And objConnector.ClientID = Target.ElementID Then
                '        blnFound = True
                '    Exit For
                '    End If
                'Next
                Dim sql As String

                sql = String.Format("select t_connector.connector_id from t_connector where t_connector.stereotype = '{0}' and t_connector.end_object_id = {1} and t_connector.start_object_id = {2} ", stereotype, Source.ElementID, Target.ElementID)
                Dim objDT As DataTable
                objDT = DLA2EAHelper.SQL2DataTable(sql, Me.Repository)
                If objDT.TableName = "ERROR" Then
                    objConnector = Source.Connectors.AddNew(Melding, ConvertAssociationStereotype2Type(stereotype))
                    objConnector.SupplierID = Source.ElementID
                    objConnector.ClientID = Target.ElementID
                    objConnector.Stereotype = stereotype
                    objConnector.Direction = "Unspecified"
                    objConnector.Update()
                    Source.Update()
                    objPack.Update()
                Else
                    Dim Row As DataRow = objDT.Rows(0)
                    objConnector = Repository.GetConnectorByID(Convert.ToInt32(Row("connector_id")))
                End If
                Return objConnector
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
            Return Nothing
        End Function
    End Class
End Namespace




