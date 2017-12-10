Imports System.IO
Imports Oracle.ManagedDataAccess.Client

Public Class BOICommencement
    Inherits System.Web.UI.Page

    'Oracle
    Private oOra As New Oracle
    Private cnnOra As OracleConnection = Nothing
    Private cnnOraString As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            GetBOINumberList()
            GetGoodsTypeList()
            GetXmlTypeList()
            GetEquipmentTypeList()

        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        'GetListing(ddlBOINumber.SelectedValue, txtImportDateFrom.Text, txtInvoiceNo.Text, txtDescription.Text, txtEquipmentId.Text, txtAssetTag.Text, TextBoxM.Text)
        GetListing(ddlBOINumber.SelectedValue, ddlGoodsType.SelectedValue, ddlXmlType.SelectedValue, ddlEquipmentType.SelectedValue, txtImportDateFrom.Text, txtImportDateTo.Text, txtMultiFilter.Text)
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        ExportToExcel()
    End Sub

#Region "get App.Config"
    Private Sub GetAppConfig()
        cnnOraString = ConfigurationManager.ConnectionStrings("ORA_EQBConnString").ConnectionString
    End Sub
#End Region

#Region "Open Connection"
    Private Sub OpenConnection()
        oOra.OpenOraConnection(cnnOra, cnnOraString)
    End Sub

    Private Sub CloseConnection()
        oOra.CloseOraConnection(cnnOra)
    End Sub

#End Region

#Region "Functions"

    Private Sub GetBOINumberList()

        Try

            GetAppConfig()
            OpenConnection()

            Dim sSQL = "select distinct BOI_NUMBER from TBL_BOINUMBER order by BOI_NUMBER"
            Dim dsResult As DataSet = oOra.OraExecuteQuery(sSQL, cnnOra)

            ddlBOINumber.DataSource = dsResult.Tables(0)
            ddlBOINumber.DataTextField = "BOI_NUMBER"
            ddlBOINumber.DataValueField = "BOI_NUMBER"
            ddlBOINumber.DataBind()

        Catch ex As Exception

            Dim errorMsg As String = ex.Message
            lblError.Text = errorMsg

        Finally

            CloseConnection()

        End Try

    End Sub

    Private Sub GetGoodsTypeList()

        Try

            GetAppConfig()
            OpenConnection()

            Dim sSQL = "select GOOD_TYPE_CODE, GOOD_TYPE_DESC from TBL_BOIGOODTYPESCODE order by GOOD_TYPE_DESC"
            Dim dsResult As DataSet = oOra.OraExecuteQuery(sSQL, cnnOra)

            ddlGoodsType.DataSource = dsResult.Tables(0)
            ddlGoodsType.DataTextField = "GOOD_TYPE_DESC"
            ddlGoodsType.DataValueField = "GOOD_TYPE_CODE"
            ddlGoodsType.DataBind()

        Catch ex As Exception

            Dim errorMsg As String = ex.Message
            lblError.Text = errorMsg

        Finally

            CloseConnection()

        End Try

    End Sub

    Private Sub GetXmlTypeList()

        Try

            GetAppConfig()
            OpenConnection()

            Dim sSQL = "select distinct XMLTYPE from TBL_BOIINFO order by XMLTYPE"
            Dim dsResult As DataSet = oOra.OraExecuteQuery(sSQL, cnnOra)

            ddlXmlType.DataSource = dsResult.Tables(0)
            ddlXmlType.DataTextField = "XMLTYPE"
            ddlXmlType.DataValueField = "XMLTYPE"
            ddlXmlType.DataBind()

        Catch ex As Exception

            Dim errorMsg As String = ex.Message
            lblError.Text = errorMsg

        Finally

            CloseConnection()

        End Try

    End Sub

    Private Sub GetEquipmentTypeList()

        Try

            GetAppConfig()
            OpenConnection()

            Dim sSQL = "select distinct EQUIPMENT_TYPE from v_equipment order by EQUIPMENT_TYPE"
            Dim dsResult As DataSet = oOra.OraExecuteQuery(sSQL, cnnOra)

            ddlEquipmentType.DataSource = dsResult.Tables(0)
            ddlEquipmentType.DataTextField = "EQUIPMENT_TYPE"
            ddlEquipmentType.DataValueField = "EQUIPMENT_TYPE"
            ddlEquipmentType.DataBind()

        Catch ex As Exception

            Dim errorMsg As String = ex.Message
            lblError.Text = errorMsg

        Finally

            CloseConnection()

        End Try

    End Sub

    'Private Sub GetListing(boiNo As String, importDate As String, invoiceNo As String, desc As String, equipmentId As String, assetTag As String, year As String)

    '    Try

    '        GetAppConfig()
    '        OpenConnection()

    '        'Dim sSQL = "select a.BOI_NUMBER, a.INVOICE_ITEM, a.DESCRIPTION, ' ' AS TAG_NUMBER, ' ' AS SERIAL_NUMBER, ' ' AS MACHINE_NUMBER, "
    '        'sSQL = sSQL & "a.QUANTITY, a.UNIT_CODE, b.good_type_desc AS GOOD_TYPE, ' ' AS YEAR_OF_MANU, c.SHIP_FROM, c.IMPORT_DATE, "
    '        'sSQL = sSQL & "a.INVOICE_NUMBER, a.INVOICE_DATE, a.DOCUMENT_NUMBER, a.DOCUMENT_DATE, '0' AS AMOUNT, c.JOB_NUMBER, "
    '        'sSQL = sSQL & "a.XMLTYPE AS XML_TYPE, c.RAW_FILEPATH "
    '        'sSQL = sSQL & "from tbl_boiinfo a "
    '        'sSQL = sSQL & "left join tbl_boigoodtypescode b ON a.good_type_code = b.good_type_code "
    '        'sSQL = sSQL & "left join tbl_boiimportentry c ON a.import_declaration_number = c.import_entry_number "

    '        Dim sSQL = "SELECT c.BOI_NUMBER, concat(c.BOI_NUMBER, c.certificate_number) as BOI_NUMBER2, c.certificate_number, a.invoice_item,a.description, d.asset_tag, d.serial_no, "
    '        sSQL = sSQL & "d.equipment_id, 1 as QUANTITY, a.unit_code, e.good_type_desc, d.manufacturer_year, b.ship_from, "
    '        sSQL = sSQL & "b.import_date, a.invoice_number, a.invoice_date, a.document_number, a.document_date, "
    '        sSQL = sSQL & "b.amount, b.job_number, a.xmltype, "
    '        sSQL = sSQL & "replace(f.raw_filepath, 'D:\Application\AspNet\mds\Files\Invoice\', '') AS download_inv, replace(b.raw_filepath, 'D:\Application\AspNet\mds\Files\Entry\', '') AS download_import "

    '        ' Added by Sharizan on 10Dec2017 1PM
    '        sSQL = sSQL & ", d.EQUIPMENT_TYPE, f.PO_NUMBER, CAR_NUMBER, b.H_S_CODE "

    '        sSQL = sSQL & "FROM TBL_BOIINFO a LEFT OUTER JOIN "
    '        sSQL = sSQL & "(SELECT t1.*,t2.ITEM_NUMBER,t2.ORIGIN_CONTRY , t2.AMOUNT, "
    '        sSQL = sSQL & "t2.TAX_VALUE, t2.VAT_VALUE, t2.TAX_RAT_PERCENT,t2.H_S_CODE ,t2.INVOICE_NUMBER,t2.INVOICE_ITEM "
    '        sSQL = sSQL & "FROM TBL_BOIIMPORTENTRY t1, TBL_BOIIMPORTSUBENTRY t2 "
    '        sSQL = sSQL & "WHERE t2.IMPORT_ENTRY_NUMBER = t1.IMPORT_ENTRY_NUMBER) b on a.INVOICE_NUMBER = b.INVOICE_NUMBER "
    '        sSQL = sSQL & "And a.INVOICE_ITEM = b.INVOICE_ITEM "
    '        sSQL = sSQL & "LEFT OUTER JOIN TBL_BOINUMBER c ON a.boi_number = c.boi_number "
    '        sSQL = sSQL & "LEFT OUTER JOIN v_equipment d ON a.invoice_item = d.invoice_no_item And a.invoice_number = d.invoice_no "
    '        sSQL = sSQL & "LEFT OUTER JOIN TBL_BOIGOODTYPESCODE e ON a.good_type_code = e.good_type_code "
    '        sSQL = sSQL & "LEFT OUTER JOIN TBL_BOIINVOICEINFO f ON a.invoice_number = f.invoice_number "

    '        sSQL = sSQL & "WHERE 1=1 And ROWNUM <= 100 "

    '        'Filtering
    '        If boiNo.Trim <> "" Then
    '            sSQL = sSQL & "And a.BOI_NUMBER = '" & boiNo & "' "
    '        End If

    '        If importDate.Trim <> "" Then
    '            sSQL = sSQL & "AND to_char(b.IMPORT_DATE, 'yyyy-MM-dd') = '" & DateTime.Parse(importDate).ToString("yyyy-MM-dd") & "' "
    '        End If

    '        If invoiceNo.Trim <> "" Then
    '            sSQL = sSQL & "AND a.INVOICE_NUMBER = '" & invoiceNo & "' "
    '        End If

    '        If desc.Trim <> "" Then
    '            sSQL = sSQL & "AND UPPER(a.DESCRIPTION) LIKE '%" & desc.ToUpper & "%' "
    '        End If

    '        If equipmentId.Trim <> "" Then
    '            sSQL = sSQL & "AND d.equipment_id = '" & equipmentId & "' "
    '        End If

    '        If assetTag.Trim <> "" Then
    '            sSQL = sSQL & "AND d.asset_tag = '" & assetTag & "' "
    '        End If

    '        If year.Trim <> "" Then
    '            sSQL = sSQL & "AND d.manufacturer_year = '" & year & "' "
    '        End If

    '        Dim dsResult As DataSet = oOra.OraExecuteQuery(sSQL, cnnOra)

    '        gvListing.DataSource = dsResult
    '        gvListing.DataBind()

    '        If dsResult.Tables(0).Rows.Count > 0 Then
    '            btnExport.Visible = True
    '        Else
    '            btnExport.Visible = False
    '        End If

    '    Catch ex As Exception

    '        Dim errorMsg As String = ex.Message
    '        lblError.Text = errorMsg

    '    Finally

    '        CloseConnection()

    '    End Try

    'End Sub

    Private Sub GetListing(boiNo As String, goodsType As String, xmlType As String, equipmentType As String, importDateFrom As String, ImportDateTo As String, multiFilter As String)

        Try

            GetAppConfig()
            OpenConnection()

            Dim sSQL = "SELECT c.BOI_NUMBER, concat(c.BOI_NUMBER, c.certificate_number) as BOI_NUMBER2, c.certificate_number, a.invoice_item,a.description, d.asset_tag, d.serial_no, "
            sSQL = sSQL & "d.equipment_id, 1 as QUANTITY, a.unit_code, e.good_type_desc, d.manufacturer_year, b.ship_from, "
            sSQL = sSQL & "b.import_date, a.invoice_number, a.invoice_date, a.document_number, a.document_date, "
            sSQL = sSQL & "b.amount, b.job_number, a.xmltype, "
            sSQL = sSQL & "replace(f.raw_filepath, 'D:\Application\AspNet\mds\Files\Invoice\', '') AS download_inv, replace(b.raw_filepath, 'D:\Application\AspNet\mds\Files\Entry\', '') AS download_import "

            ' Added by Sharizan on 10Dec2017 1PM
            sSQL = sSQL & ", d.EQUIPMENT_TYPE, f.PO_NUMBER, f.CAR_NUMBER, b.H_S_CODE "

            sSQL = sSQL & "FROM TBL_BOIINFO a LEFT OUTER JOIN "
            sSQL = sSQL & "(SELECT t1.*,t2.ITEM_NUMBER,t2.ORIGIN_CONTRY , t2.AMOUNT, "
            sSQL = sSQL & "t2.TAX_VALUE, t2.VAT_VALUE, t2.TAX_RAT_PERCENT,t2.H_S_CODE ,t2.INVOICE_NUMBER,t2.INVOICE_ITEM "
            sSQL = sSQL & "FROM TBL_BOIIMPORTENTRY t1, TBL_BOIIMPORTSUBENTRY t2 "
            sSQL = sSQL & "WHERE t2.IMPORT_ENTRY_NUMBER = t1.IMPORT_ENTRY_NUMBER) b on a.INVOICE_NUMBER = b.INVOICE_NUMBER "
            sSQL = sSQL & "And a.INVOICE_ITEM = b.INVOICE_ITEM "
            sSQL = sSQL & "LEFT OUTER JOIN TBL_BOINUMBER c ON a.boi_number = c.boi_number "
            sSQL = sSQL & "LEFT OUTER JOIN v_equipment d ON a.invoice_item = d.invoice_no_item And a.invoice_number = d.invoice_no "
            sSQL = sSQL & "LEFT OUTER JOIN TBL_BOIGOODTYPESCODE e ON a.good_type_code = e.good_type_code "
            sSQL = sSQL & "LEFT OUTER JOIN TBL_BOIINVOICEINFO f ON a.invoice_number = f.invoice_number "

            sSQL = sSQL & "WHERE 1=1 And ROWNUM <= 150 "

            'Filtering
            If boiNo.Trim <> "" Then
                sSQL = sSQL & "And a.BOI_NUMBER = '" & boiNo & "' "
            End If

            If importDateFrom.Trim <> "" And ImportDateTo.Trim <> "" Then
                sSQL = sSQL & "And to_char(b.IMPORT_DATE, 'yyyy-MM-dd') between '" & DateTime.Parse(importDateFrom).ToString("yyyy-MM-dd") & "' And '" & DateTime.Parse(ImportDateTo).ToString("yyyy-MM-dd") & "' "
            End If

            If goodsType.Trim <> "" Then
                sSQL = sSQL & "And a.good_type_code = '" & goodsType & "' "
            End If

            If xmlType.Trim <> "" Then
                sSQL = sSQL & "And a.xmltype = '" & xmlType & "' "
            End If

            If equipmentType.Trim <> "" Then
                sSQL = sSQL & "And d.EQUIPMENT_TYPE = '" & equipmentType & "' "
            End If

            If multiFilter.Trim <> "" Then

                sSQL = sSQL & "And ( "
                sSQL = sSQL & "f.CAR_NUMBER Like '%" & multiFilter & "%' "
                sSQL = sSQL & "Or f.PO_NUMBER Like '%" & multiFilter & "%' "
                sSQL = sSQL & "Or a.description Like '%" & multiFilter & "%' "
                sSQL = sSQL & "Or d.asset_tag Like '%" & multiFilter & "%' "
                sSQL = sSQL & "Or d.serial_no Like '%" & multiFilter & "%' "
                sSQL = sSQL & "Or d.equipment_id Like '%" & multiFilter & "%' "
                sSQL = sSQL & "Or b.ship_from Like '%" & multiFilter & "%' "
                sSQL = sSQL & "Or a.invoice_number Like '%" & multiFilter & "%' "
                sSQL = sSQL & "Or a.document_number Like '%" & multiFilter & "%' "
                sSQL = sSQL & "Or b.job_number Like '%" & multiFilter & "%' "
                sSQL = sSQL & ") "


            End If

            'If invoiceNo.Trim <> "" Then
            '    sSQL = sSQL & "And a.INVOICE_NUMBER = '" & invoiceNo & "' "
            'End If

            'If desc.Trim <> "" Then
            '    sSQL = sSQL & "AND UPPER(a.DESCRIPTION) LIKE '%" & desc.ToUpper & "%' "
            'End If

            'If equipmentId.Trim <> "" Then
            '    sSQL = sSQL & "AND d.equipment_id = '" & equipmentId & "' "
            'End If

            'If assetTag.Trim <> "" Then
            '    sSQL = sSQL & "AND d.asset_tag = '" & assetTag & "' "
            'End If

            'If Year.Trim <> "" Then
            '    sSQL = sSQL & "AND d.manufacturer_year = '" & Year() & "' "
            'End If

            Dim dsResult As DataSet = oOra.OraExecuteQuery(sSQL, cnnOra)

            gvListing.DataSource = dsResult
            gvListing.DataBind()

            If dsResult.Tables(0).Rows.Count > 0 Then
                btnExport.Visible = True
            Else
                btnExport.Visible = False
            End If

        Catch ex As Exception

            Dim errorMsg As String = ex.Message
            lblError.Text = errorMsg

        Finally

            CloseConnection()

        End Try

    End Sub

    Protected Sub ExportToExcel()
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=BOICommencement-" & DateTime.Now.ToString("yyyyMMddHHmmss") & ".xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"
        'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)

            'To Export all pages
            gvListing.AllowPaging = False
            'Me.GetListing(ddlBOINumber.SelectedValue, txtImportDateFrom.Text, txtInvoiceNo.Text, txtDescription.Text, txtEquipmentId.Text, txtAssetTag.Text, TextBoxM.Text)

            gvListing.HeaderRow.BackColor = System.Drawing.Color.White
            For Each cell As TableCell In gvListing.HeaderRow.Cells
                cell.BackColor = gvListing.HeaderStyle.BackColor
            Next
            For Each row As GridViewRow In gvListing.Rows
                row.BackColor = System.Drawing.Color.White
                For Each cell As TableCell In row.Cells
                    If row.RowIndex Mod 2 = 0 Then
                        cell.BackColor = gvListing.AlternatingRowStyle.BackColor
                    Else
                        cell.BackColor = gvListing.RowStyle.BackColor
                    End If
                    cell.CssClass = "textmode"
                    'cell.CssClass = "text"
                Next
            Next

            gvListing.RenderControl(hw)
            'style to format numbers to string
            Dim style As String = "<style> .textmode { } </style>"
            'Dim style As String = "<style> .text { mso-number-format:\@; } </style>"
            Response.Write(style)
            Response.Output.Write(sw.ToString())
            Response.Flush()
            Response.[End]()
        End Using
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        'This function required for excel export
    End Sub

    Protected Sub gvListing_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvListing.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim hidImport As HiddenField = e.Row.FindControl("hidRawFileImportEntry")
                'Dim imgImportEntry As ImageButton = e.Row.FindControl("imgViewDetails2")
                Dim hlViewRawImportEntry As HyperLink = e.Row.FindControl("hlViewRawImportEntry")
                If hidImport.Value <> Nothing Then

                    hlViewRawImportEntry.NavigateUrl = "~/Files/Entry/" & HttpUtility.UrlEncode(hidImport.Value)
                    hlViewRawImportEntry.Visible = True
                Else
                    hlViewRawImportEntry.Visible = False

                End If

                Dim hidInvoice As HiddenField = e.Row.FindControl("hidRawFileInvoice")
                'Dim imgInvoice As ImageButton = e.Row.FindControl("imgViewDetails1")
                Dim hlViewRawInvoice As HyperLink = e.Row.FindControl("hlViewRawInvoice")
                If hidInvoice.Value <> Nothing Then

                    hlViewRawInvoice.NavigateUrl = "~/Files/Invoice/" & HttpUtility.UrlEncode(hidInvoice.Value)
                    hlViewRawInvoice.Visible = True
                Else
                    hlViewRawInvoice.Visible = False

                End If

            End If

        Catch ex As Exception

        End Try



    End Sub

#End Region

End Class