<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Report.Master" CodeBehind="SummaryByEquipmentType.aspx.vb" Inherits="MDS.App.SummaryByEquipmentType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h3> Summary of total equipment by equipment type </h3>

    <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red" Font-Bold="true" Font-Size="Large"></asp:Label>

    <div class="row">

        <div class="col-md-4">
            BOI Number (Seperate by comma (,))<br />
            <asp:TextBox ID="txtBOINumber" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="col-md-2">
            Equipment Type <br />
            <asp:DropDownList ID="ddlEquipmentType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                <asp:ListItem Text="" Value=""></asp:ListItem>
            </asp:DropDownList>
        </div>

        <div class="col-md-2">
            Status Code <br />
            <asp:DropDownList ID="ddlStatusCode" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                <asp:ListItem Text="" Value=""></asp:ListItem>
            </asp:DropDownList>
        </div>

        <div class="col-md-2">   
            &nbsp; <br />
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="form-control btn btn-primary" Width="80px" Height="33px" />         
        </div>

    </div>

    <div class="row">

        <div class="col-md-12">

            <table id="table-listing">
                <thead></thead>
                <tbody></tbody>
            </table>

        </div>

    </div>


    <script>

        $(document).ready(function () {

            $('#<%=btnSearch.ClientID%>').on('click', function (e) {

                e.preventDefault();

                loadReport();

            });

        });

        function loadReport() {

            //alert('Loading report....');
            $.ajax({
                url: "SummaryByEquipmentType.aspx/GetListing",
                data: "{ 'boiNumber': '', 'equipmentType': '', 'statusCode': ''}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    alert(data.d);
                },
                error: function (a, b, c) {
                    console.log('error: ' + JSON.stringify(a));
                }
            });

        }



    </script>

</asp:Content>
