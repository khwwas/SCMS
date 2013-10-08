<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SCMSMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Voucher Entry Console
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            //            document.getElementById('ddl_location').disabled = true;
        });

        //        function CheckAllLocations() {
        //            if (document.getElementById('chk_AllLocations').checked == true) {
        //                document.getElementById('ddl_location').disabled = true;
        //            }
        //            else {
        //                document.getElementById('ddl_location').disabled = false;
        //            }
        //        };

        function EditRecord(Id) {
        }

        function VoucherEntryConsole(locationId) {
            window.location = '../VoucherEntryConsole?locationId=' + locationId;
        }

        function DeleteRecord(Id) {
            if (confirm("Do you really want to delete this record")) {
                var MessageBox = document.getElementById('MessageBox');
                var Url = document.getElementById('frm_VoucherEntryConsole').action;
                Url += "VoucherEntryConsole/DeleteRecordById?ps_Id=" + Id;
                $.ajax({
                    type: "GET",
                    url: Url,
                    success: function (response) {
                        html = response;
                        $("#GridContainer").html(response);

                        //ResetForm();
                        FadeIn(MessageBox);
                        MessageBox.innerHTML = "<h5>Success!</h5><p>Record deleted successfully.</p>";
                        MessageBox.setAttribute("class", "message success");
                        scroll(0, 0);
                        FadeOut(MessageBox);
                        SetGrid();
                    },
                    error: function (rs, e) {
                        FadeIn(MessageBox);
                        MessageBox.innerHTML = "<h5>Error!</h5><p>An error occured in deleting this record.</p>";
                        MessageBox.setAttribute("class", "message error");
                        scroll(0, 0);
                        FadeOut(MessageBox);
                        SetUserRights();
                    }
                });
            }
        }

    </script>
    <form id="frm_VoucherEntryConsole" action='<%=Url.Content("~/") %>'>
    <div class="box round first fullpage grid">
        <h2>
            Voucher Entry Console</h2>
        <div class="block">
            <div id="MessageBox">
            </div>
            <%--<div id="div_Location" style="vertical-align: middle">
                <div class="CustomCell" style="width: 200px; height: 20px; text-align: right;">
                    Location&nbsp;&nbsp;</div>
                <%= Html.DropDownList("ddl_location", null, new { style = "width:500px; height:20px;" })%>
                <input type="checkbox" class="checkbox" id="chk_AllLocations" name="chk_AllLocations"
                    onclick="CheckAllLocations()" checked="checked" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <input id="btn_Search" type="button" value="Search" class="btn btn-blue" onclick="FilterLocations();"
                    style="width: 120px; height: 35px; padding-top: 5px; color: White; font-weight: bold;
                    font-size: 11pt;" />
                <img alt="" id="Waiting_Image" src="../../img/Ajax_Loading.gif" style="display: none;
                    margin-left: 10" /></div>--%>
            <div class="Clear">
            </div>
            <div id="GridContainer">
                <%Html.RenderPartial("GridData");%>
            </div>
        </div>
    </div>
    </form>
</asp:Content>
