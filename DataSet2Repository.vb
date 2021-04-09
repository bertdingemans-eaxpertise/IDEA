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
        ''' <summary>
        ''' Return the stored package id
        ''' </summary>
        ''' <returns></returns>
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
        ''' <summary>
        ''' Process a datarow for import to transform to a new element and association in EA
        ''' </summary>
        ''' <param name="Parent">The element to start from</param>
        ''' <param name="objRow">Datarow that is imported</param>
        ''' <param name="Fieldname">The name of the field in the datarow</param>
        ''' <param name="objStereoType">The stereotype of the element to connect to</param>
        ''' <param name="ConStereoType">The stereotype of the column to connect to</param>
        ''' <param name="PostFix">Eventually use a postfix (for a context) for the fieldcontent</param>
        ''' <returns></returns>
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
        ''' Add an attribute to an element
        ''' </summary>
        ''' <param name="objElement">The element that is parent</param>
        ''' <param name="strName">The name of the attribute</param>
        ''' <param name="strType">The datatype of the element</param>
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
        ''' Add a tagged value to an element
        ''' </summary>
        ''' <param name="Element">Parent element</param>
        ''' <param name="name">Name of the tagged value</param>
        ''' <param name="value">Value of the tagged value</param>
        ''' <param name="isMemo">Is the value a memo (to the notes field of the TV)</param>
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
        ''' <summary>
        ''' Add (when not exist) or update a tagged value from a datarow
        ''' </summary>
        ''' <param name="Element">Parent element</param>
        ''' <param name="name">Name of the field in the datarow</param>
        ''' <param name="Row">Datarow with the import data</param>
        ''' <param name="isMemo">Is the value a memo</param>
        Public Sub AddOrUpdateTaggedValue(ByVal Element As EA.Element, ByVal name As String, Row As DataRow, ByVal isMemo As Boolean)
            If Not IsDBNull(Row.Item(name)) Then
                Me.AddOrUpdateTaggedValue(Element, name, Row.Item(name), isMemo)
            End If
        End Sub
        ''' <summary>
        ''' Add (when not exist) or update a tagged value from a datarow with a different tv name
        ''' </summary>
        ''' <param name="Element">Parement element</param>
        ''' <param name="tvname">Name of the tagged value</param>
        ''' <param name="rowname">Name of the field in the datarow</param>
        ''' <param name="Row">datarow with imported data</param>
        ''' <param name="isMemo">Memo field</param>
        Public Sub AddOrUpdateTaggedValue(ByVal Element As EA.Element, ByVal tvname As String, rowname As String, Row As DataRow, ByVal isMemo As Boolean)
            If Not IsDBNull(Row.Item(rowname)) Then
                Me.AddOrUpdateTaggedValue(Element, tvname, Row.Item(rowname).ToString(), isMemo)
            End If
        End Sub
        ''' <summary>
        ''' Add an attribute to an element
        ''' </summary>
        ''' <param name="Element">Parent element</param>
        ''' <param name="name">name of the attribute</param>
        ''' <param name="type">datatype of the attribute</param>
        ''' <param name="Note">Description of the attribute</param>
        ''' <returns>Updated attribute</returns>
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
        ''' <summary>
        ''' Get a tagged value content for an element
        ''' </summary>
        ''' <param name="Element">Parent element</param>
        ''' <param name="name">Tagged value name</param>
        ''' <returns>Tagged value content</returns>
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
        ''' Add or update a tagged value to an element
        ''' </summary>
        ''' <param name="Element">Parent object</param>
        ''' <param name="name">Tagged value name</param>
        ''' <param name="value">Tagged value content</param>
        ''' <param name="isMemo">Is the value a memo value</param>
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
        ''' Conevert a stereotype to an objecttype
        ''' </summary>
        ''' <param name="stereotype">Name of the stereotype</param>
        ''' <returns>Name of the objecttype</returns>
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
        ''' Convert a stereotype of a connector to a connectortype
        ''' </summary>
        ''' <param name="stereotype">Name of the stereotype</param>
        ''' <returns>Name of the connectortype</returns>
        Public Function ConvertAssociationStereotype2Type(ByVal stereotype As String) As String
            Dim strRet As String = "Association"
            Select Case stereotype.ToUpper()
                Case "ARCHIMATE_AGGREGATION"
                    strRet = "Aggregation"
            End Select
            Return strRet
        End Function
        ''' <summary>
        ''' Add an element to the repository
        ''' </summary>
        ''' <param name="stereotype">Stereotype of the element</param>
        ''' <param name="name">Name of the element</param>
        ''' <param name="memo">Note field text</param>
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
        ''' Find an element by name
        ''' </summary>
        ''' <param name="name">The name to search for</param>
        ''' <returns>Return the element when found otherwise return nothing</returns>
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
        ''' <summary>
        ''' Find a package by name
        ''' </summary>
        ''' <param name="name">Name to search for</param>
        ''' <returns>The found package or nothing</returns>
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
        ''' Find an element by name and stereotype
        ''' </summary>
        ''' <param name="name">Name of the element</param>
        ''' <param name="stereotype">Name of the stereotype</param>
        ''' <returns>Found element or package</returns>
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
        ''' Search or add an element by name and stereotype
        ''' </summary>
        ''' <param name="name">Name of the element</param>
        ''' <param name="stereotype">Stereotype</param>
        ''' <param name="Found">Element found (true) or added (false)</param>
        ''' <returns>Element found or added</returns>
        Public Function FindOrAddElement(ByVal name As String, ByVal stereotype As String, ByRef Found As Boolean) As EA.Element
            Return Me.FindOrAddElement(name, stereotype, Found, "")
        End Function
        ''' <summary>
        ''' Find or add an element including a memo
        ''' </summary>
        ''' <param name="name">Name of the element</param>
        ''' <param name="stereotype">Stereotype</param>
        ''' <param name="Found">Element found (true) or added (false)</param>
        ''' <param name="memo">Memo with a description of the element</param>
        ''' <returns>Element found or added</returns>
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
        ''' Search for an element by name and stereotype
        ''' </summary>
        ''' <param name="name">Name of the element</param>
        ''' <param name="stereotype">Stereotye of the element</param>
        ''' <param name="Found">Is the element found</param>
        ''' <returns>The found element or nothing</returns>
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
        ''' Search for an element by tagged value (for keys from an external system
        ''' </summary>
        ''' <param name="name">Tagged value name</param>
        ''' <param name="value">Value to search for</param>
        ''' <returns>The found element or nothing</returns>
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
        ''' Search an element by alias and stereotype
        ''' </summary>
        ''' <param name="aliasname">Aliasname</param>
        ''' <param name="stereotype">Stereotype of the element</param>
        ''' <returns>Element found or nothing</returns>
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
        ''' vAdd a connector to the repository
        ''' </summary>
        ''' <param name="Source">Source object</param>
        ''' <param name="Target">Target object</param>
        ''' <param name="stereotype">Connector stereotype</param>
        ''' <returns>The added connector or nothing</returns>
        Public Function AddConnector(ByVal Source As EA.Element, ByVal Target As EA.Element, ByVal stereotype As String) As EA.Connector
            Return Me.AddConnector(Source, Target, stereotype, "")
        End Function
        ''' <summary>
        ''' Add a connector to the reposity with a connector name
        ''' </summary>
        ''' <param name="Source">Source object</param>
        ''' <param name="Target">Target object</param>
        ''' <param name="stereotype">Connector stereotype</param>
        ''' <param name="Melding">Name of the connector</param>
        ''' <returns>Added connector or nothing</returns>
        Public Function AddConnector(ByVal Source As EA.Element, ByVal Target As EA.Element, ByVal stereotype As String, ByVal Melding As String) As EA.Connector
            Dim objConnector As EA.Connector
            Dim blnFound As Boolean = False
            Try
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




