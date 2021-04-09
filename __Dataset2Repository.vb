Namespace DLAFormfactory


    ''' <summary>
    ''' Helper class for transforming output from the Sparx API in a more usable format
    ''' within a DotNet (windows) application.
    ''' </summary>
    ''' 
    Public Class __DataSet2Repository
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
        Public Property Repository() As EA.Repository
            Get
                Return _Repository
            End Get
            Set(ByVal value As EA.Repository)
                _Repository = value
            End Set
        End Property
        ''' <summary>
        ''' Voeg een attribuut toe aan een element
        ''' </summary>
        ''' <param name="objElement"></param>
        ''' <param name="strName"></param>
        ''' <param name="strType"></param>
        ''' <returns></returns>
        Public Shared Function AddAttribute(ByVal objElement As EA.Element, ByVal strName As String, ByVal strType As String) As EA.Element

            Dim objAttribute As EA.Attribute
            If Not IsDBNull(strName) Then
                objAttribute = objElement.Attributes.AddNew(strName, strType)
                objElement.Update()
            End If
            Return objElement
        End Function
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
        ''' Voeg een tagged value toe aan een element
        ''' </summary>
        ''' <param name="Element"></param>
        ''' <param name="name"></param>
        ''' <param name="value"></param>
        ''' <param name="isMemo"></param>
        Public Shared Sub AddTaggedValue(ByVal Element As EA.Element, ByVal name As String, ByVal value As String, ByVal isMemo As Boolean)
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
        Public Shared Sub AddOrUpdateTaggedValue(ByVal Element As EA.Element, ByVal name As String, ByVal value As String, ByVal isMemo As Boolean)
            Try
                Dim objTV As EA.TaggedValue
                Dim Found As Boolean = False
                If Not String.IsNullOrEmpty(value) And value <> ChrW(129) Then
                    For Each objTV In Element.TaggedValues
                        If objTV.Name = name Then
                            If isMemo Then
                                objTV.Notes = value
                            Else
                                objTV.Value = value
                            End If
                            objTV.Update()
                            Found = True
                            Exit For
                        End If
                    Next

                    If Found = False Then
                        objTV = Element.TaggedValues.AddNew(name, "<memo>")
                        If isMemo Then
                            objTV.Notes = value
                        Else
                            objTV.Value = value
                        End If
                        objTV.Update()
                    End If
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
        Public Shared Function ConvertStereotype2Type(ByVal stereotype As String) As String
            Dim strRet As String = "Class"
            Select Case stereotype.ToUpper()
                Case "ARCHIMATE_APPLICATIONCOMPONENT"
                    strRet = "Component"
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
        Public Shared Function ConvertAssociationStereotype2Type(ByVal stereotype As String) As String
            Dim strRet As String = "Association"
            Select Case stereotype.ToUpper()
                Case "ARCHIMATE_REALIZATION", "ARCHIMATE_ACCESS"
                    strRet = "Dependency"
                Case "ARCHIMATE_AGGREGATION"
                    strRet = "Aggregation"
                Case "TRACE"
                    strRet = "Abstraction"
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
                objPack = _Repository.GetPackageByID(Me.Package_Id)
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
            Dim objElement As EA.Element

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

                Dim objDS As DataSet
                objDS = DLA2EAHelper.SQL2DataSet(strSql, Me.Repository)
                If objDS.Tables.Count > 0 Then
                    If objDS.Tables(1).Rows.Count = 1 Then
                        objElement = Me.Repository.GetElementByID(objDS.Tables(1).Rows(0).Item("object_id"))
                    End If
                End If
            Catch ex As Exception
                fouten += "Fout in FindElement" & ex.Message & vbCrLf
            End Try
            Return objElement
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
        Public Function FindElement(ByVal name As String, ByVal stereotype As String, ByRef Found As Boolean) As EA.Element
            Dim objPack As EA.Package
            Dim objElement As EA.Element

            Try
                objPack = _Repository.GetPackageByID(Me.Package_Id)
                Dim strSql As String
                If stereotype.Length > 0 Then
                    strSql = String.Format("SELECT object_id FROM t_object WHERE (alias = '{0}' or name = '{0}')  AND stereotype = '{1}'", name, stereotype)
                Else
                    strSql = String.Format("SELECT object_id FROM t_object WHERE (alias = '{0}' or name = '{0}')  AND object_type = 'Class'", name, stereotype)
                End If
                Dim objDT As DataTable
                objDT = DLA2EAHelper.SQL2DataTable(strSql, Me.Repository)
                Found = False
                If Not IsNothing(objDT) Then
                    If objDT.Rows.Count = 1 Then
                        objElement = Me.Repository.GetElementByID(objDT.Rows(0).Item("object_id"))
                        Found = True
                    End If
                End If
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
            Return objElement
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
                If objDT.Rows.Count = 1 Then
                    objElement = Me.Repository.GetElementByID(objDT.Rows(0).Item("object_id"))
                End If
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
            Return objElement
        End Function
        Public Function FindElementByAlias(ByVal name As String, ByVal stereotype As String) As EA.Element
            Dim objPack As EA.Package
            Dim objElement As EA.Element

            Try
                objPack = _Repository.GetPackageByID(Me.Package_Id)
                Dim strSql As String
                strSql = String.Format("SELECT object_id FROM t_object WHERE alias = '{0}' AND stereotype = '{1}'", name, stereotype)
                Dim objDT As DataTable
                objDT = DLA2EAHelper.SQL2DataTable(strSql, Me.Repository)

                If objDT.Rows.Count = 1 Then
                    objElement = Me.Repository.GetElementByID(objDT.Rows(0).Item("object_id"))
                End If
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
            Return objElement
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
                'eerst controleren of de conector niet al bestaat
                For Each objConnector In Source.Connectors
                    If objConnector.Stereotype = stereotype And objConnector.ClientID = Target.ElementID Then
                        blnFound = True
                        Exit For
                    End If
                Next
                If blnFound = False Then
                    objConnector = Source.Connectors.AddNew(Melding, ConvertAssociationStereotype2Type(stereotype))
                    objConnector.SupplierID = Source.ElementID
                    objConnector.ClientID = Target.ElementID
                    objConnector.Stereotype = stereotype
                    objConnector.Direction = "Unspecified"
                    objConnector.Update()
                End If

                Source.Update()
                objPack.Update()
            Catch ex As Exception
                DLA2EAHelper.Error2Log(ex)
            End Try
            Return objConnector
        End Function
    End Class

End Namespace