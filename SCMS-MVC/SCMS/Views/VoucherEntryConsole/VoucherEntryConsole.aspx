<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SCMSMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Voucher Entry Console
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {

        });
        function Search() {
            var MessageBox = document.getElementById('MessageBox');
            var pcnt_Location = document.getElementById("ddl_Location");
            var pcnt_VoucherType = document.getElementById('ddl_VoucherType');
            var pcnt_DateFrom = document.getElementById('txt_DateFrom');
            var pcnt_DateTo = document.getElementById('txt_DateTo');

            var Url = document.getElementById('frm_VoucherEntryConsole').action;
            Url += "VoucherEntryConsole/Search?ps_Location=" + pcnt_Location.value + "&ps_VoucherType=" + pcnt_VoucherType.value + "&ps_DateFrom=" + pcnt_DateFrom.value + "&ps_DateTo=" + pcnt_DateTo.value;
            document.getElementById("Waiting_Image").style.display = "block";
            document.getElementById("btn_Search").style.display = "none";
            $.ajax({
                type: "GET",
                url: Url,
                success: function (response) {
                    html = response;
                    $("#GridContainer").html(response);
                    SetGrid();
                    ResetForm();
                    FadeIn(MessageBox);
                    if (document.getElementById("SaveResult").value == "0") {
                        MessageBox.innerHTML = "<h5>Error!</h5><p>Unable to save record.</p>";
                        MessageBox.setAttribute("class", "message error");

                    } else {
                        MessageBox.innerHTML = "<h5>Success!</h5><p>Record saved successfully.</p>";
                        MessageBox.setAttribute("class", "message success");
                    }
                    document.getElementById("Waiting_Image").style.display = "none";
                    document.getElementById("btn_Search").style.display = "block";
                    scroll(0, 0);
                    FadeOut(MessageBox);
                },
                error: function (rs, e) {
                    document.getElementById("Waiting_Image").style.display = "none";
                    document.getElementById("btn_Search").style.display = "block";
                }
            });
        }


        function EditRecord(Id) {

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
            <%-- <div class="CustomCell" style="width: 97px; height: 30px;">
                Location</div>
            <div class="CustomCell" style="width: 270px; height: 30px;">
                <%= Html.DropDownList("ddl_Location", null, new { style = "width:955px;" })%>
            </div>
            <div class="Clear">
            </div>
            <div class="CustomCell" style="width: 97px; height: 30px;">
                Voucher Type</div>
            <div class="CustomCell" style="width: 270px; height: 30px;">
                <%= Html.DropDownList("ddl_VoucherType", null, new { style = "width:955px;" })%>
            </div>
            <div class="Clear">
            </div>
            <div class="CustomCell" style="width: 97px; height: 30px; vertical-align: middle;">
                Date From</div>
            <div class="CustomCell" style="width: 282px; height: 30px;">
                <input type="text" class="CustomText" style="width: 220px;" id="txt_DateFrom" name="txt_DateFrom"
                    maxlength="50" />
            </div>
            <script type="text/javascript">
                $('#txt_DateFrom').Zebra_DatePicker({
                    format: 'd/M/Y'
                });
            </script>
            <div class="CustomCell" style="width: 97px; height: 30px; vertical-align: middle;">
                Date To</div>
            <div class="CustomCell" style="width: 282px; height: 30px;">
                <input type="text" class="CustomText" style="width: 220px;" id="txt_DateTo" name="txt_DateTo"
                    maxlength="50" />
            </div>
            <script type="text/javascript">
                $('#txt_DateTo').Zebra_DatePicker({
                    format: 'd/M/Y'
                });
            </script>
            <div class="Clear">
            </div>
            <div style="float: right; margin-bottom: 10px;">
                <div style="float: left; margin-right: 5px;">
                    <input id="btn_Search" type="button" value="Search" class="btn btn-blue" onclick="Search();"
                        style="width: 90px; height: 35px; padding-top: 5px; color: White; font-weight: bold;
                        font-size: 11pt;" />
                    <img alt="" id="Waiting_Image" src="../../img/Ajax_Loading.gif" style="display: none;
                        margin-left: 10" /></div>
            </div>
            <hr />--%>
            <div id="GridContainer">
                <%Html.RenderPartial("GridData");%>
            </div>
        </div>
    </div>
    </form>
</asp:Content>
