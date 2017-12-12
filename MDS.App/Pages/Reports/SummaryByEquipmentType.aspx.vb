Imports System.Web.Services
Imports Newtonsoft.Json

Public Class SummaryByEquipmentType
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

#Region "Web methods"

    <WebMethod>
    Public Shared Function GetListing(boiNumber As String, equipmentType As String, statusCode As String) As Object

        Dim dbConn As String = ""

        Return JsonConvert.SerializeObject("[]")

    End Function

#End Region


End Class