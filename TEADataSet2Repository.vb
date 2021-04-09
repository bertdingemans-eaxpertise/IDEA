Namespace DLAFormfactory

    ''' <summary>
    ''' Bring elements from a datatable to the repository with different
    ''' functionalities like:
    ''' Adding or Updating elements
    ''' Adding or updating tagged values
    ''' Adding or updating connectors
    ''' Etc
    ''' </summary>
    Public Class TEADataSet2Repository

    Private _Repository As EA.Repository
    Public fouten As String = ""
    Public Property Repository() As EA.Repository
        Get
            Return _Repository
        End Get
        Set(ByVal value As EA.Repository)
            _Repository = value
        End Set
    End Property
    Public Function AddAttribute(ByVal objElement As EA.Element, ByVal strName As String, ByVal strType As String) As EA.Element

        Dim objAttribute As EA.Attribute
        If Not IsDBNull(strName) Then
            objAttribute = objElement.Attributes.AddNew(strName, strType)
            objElement.Update()
        End If
        Return objElement
    End Function
    Public Sub AddTaggedValue(ByVal Element As EA.Element, ByVal name As String, ByVal value As String, ByVal isMemo As Boolean)
        Try
            Dim objTV As EA.TaggedValue
            If Not String.IsNullOrEmpty(value) Then
                If isMemo Then
                    objTV = Element.TaggedValues.AddNew(name, "<memo>")
                    objTV.Notes = value
                Else
                    objTV = Element.TaggedValues.AddNew(name, value)
                End If
                objTV.Update()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub AddOrUpdateTaggedValue(ByVal Element As EA.Element, ByVal name As String, ByVal value As String, ByVal isMemo As Boolean)
        Try
            Dim objTV As EA.TaggedValue
            Dim blnUpdate As Boolean = False

            For Each objTV In Element.TaggedValues
                If objTV.Name = name Then
                    blnUpdate = True
                    If isMemo Then
                        objTV.Notes = value
                    Else
                        objTV.Value = value
                    End If
                End If
            Next
            If blnUpdate = False Then
                Me.AddTaggedValue(Element, name, value, isMemo)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function ConvertStereotype2Type(ByVal stereotype As String) As String
        Dim strRet As String = "Class"
        Select Case stereotype.ToUpper()
            Case "ARCHIMATE_APPLICATIONCOMPONENT"
                strRet = "Component"
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

    Public Function ConvertAssociationStereotype2Type(ByVal stereotype As String) As String
        Dim strRet As String = "Association"
        Select Case stereotype.ToUpper()
            Case "ARCHIMATE_AGGREGATION"
                strRet = "Aggregation"
        End Select
        Return strRet
    End Function

    Public Function AddElement(ByVal stereotype As String, ByVal name As String, ByVal memo As String) As EA.Element
        Dim objPack As EA.Package
        Dim objElement As EA.Element
        Try
            objPack = _Repository.GetTreeSelectedPackage()
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
    Public Function FindElement(ByVal name As String) As EA.Element
        Dim objPack As EA.Package
        Dim objElement As EA.Element
            Try
                objPack = _Repository.GetTreeSelectedPackage()
                Dim strSql As String
                strSql = String.Format("SELECT object_id FROM t_object WHERE name = '{0}' ", name)
                Dim objDT As DataTable
                objDT = DLA2EAHelper.SQL2DataTable(strSql, Me.Repository)
                If objDT.Rows.Count > 0 Then
                    objElement = Me.Repository.GetElementByID(objDT.Rows(0).Item("object_id"))
                Else
                    objElement = Nothing
                End If
            Catch ex As Exception
                fouten += "Fout in FindElement" & ex.Message & vbCrLf
            End Try
            Return objElement
    End Function

    Public Function FindElement(ByVal name As String, ByVal stereotype As String) As EA.Element
        Dim objPack As EA.Package
        Dim objElement As EA.Element

            Try
                objPack = _Repository.GetTreeSelectedPackage()
                Dim strSql As String
                strSql = String.Format("SELECT object_id FROM t_object WHERE name = '{0}' AND stereotype = '{1}'", name, stereotype)
                Dim objDT As DataTable
                objDT = DLA2EAHelper.SQL2DataTable(strSql, Me.Repository)
                If objDT.Rows.Count > 0 Then
                    objElement = Me.Repository.GetElementByID(objDT.Rows(0).Item("object_id"))
                Else
                    objElement = Nothing
                End If
            Catch ex As Exception
                fouten += "Fout in FindElement" & ex.Message & vbCrLf
            End Try
            Return objElement
    End Function

    Public Function FindElementByAlias(ByVal name As String, ByVal stereotype As String) As EA.Element
        Dim objPack As EA.Package
        Dim objElement As EA.Element

            Try
                objPack = _Repository.GetTreeSelectedPackage()
                Dim strSql As String
                strSql = String.Format("SELECT object_id FROM t_object WHERE alias = '{0}' AND stereotype = '{1}'", name, stereotype)
                Dim objDT As DataTable
                objDT = DLA2EAHelper.SQL2DataTable(strSql, Me.Repository)
                If objDT.Rows.Count = 1 Then
                    objElement = Me.Repository.GetElementByID(objDT.Rows(0).Item("object_id"))
                End If
            Catch ex As Exception
                fouten += "Fout in FindElementByAlias" & ex.Message & vbCrLf
        End Try
            Return objElement
        End Function
    Public Function AddConnector(ByVal Source As EA.Element, ByVal Target As EA.Element, ByVal stereotype As String) As EA.Connector
        Return Me.AddConnector(Source, Target, stereotype, "--")
    End Function
        Public Function AddConnector(ByVal Source As EA.Element, ByVal Target As EA.Element, ByVal stereotype As String, ByVal Melding As String) As EA.Connector
            Dim objPack As EA.Package
            Dim objConnector As EA.Connector
            Try
                objPack = _Repository.GetTreeSelectedPackage()
                objConnector = Source.Connectors.AddNew("", ConvertAssociationStereotype2Type(stereotype))
                objConnector.SupplierID = Source.ElementID
                objConnector.ClientID = Target.ElementID
                objConnector.Stereotype = stereotype
                objConnector.Direction = "Unspecified"
                objConnector.Update()
                Source.Update()
                objPack.Update()
            Catch ex As Exception
                fouten += "Fout in AddConnector" & stereotype & ex.Message & vbCrLf

            End Try
            Return objConnector
        End Function
    End Class

End Namespace