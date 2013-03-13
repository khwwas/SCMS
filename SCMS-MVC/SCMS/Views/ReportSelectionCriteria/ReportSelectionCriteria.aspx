<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SCMSMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Report Selection Criteria
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        var ps_ReportName;
        ps_ReportName = '<%= ViewData["ReportName"] %>';

        $(document).ready(function () {
            SetCriteria();
        });

        function CheckAllCode() {
            if (document.getElementById('chk_AllAccCode').checked == true) {
                document.getElementById('ddl_AccCodeFrom').disabled = true;
                document.getElementById('ddl_AccCodeTo').disabled = true;
            }
            else {
                document.getElementById('ddl_AccCodeFrom').disabled = false;
                document.getElementById('ddl_AccCodeTo').disabled = false;
            }
        };

        function CheckAllDate() {
            if (document.getElementById('chk_AllDate').checked == true) {
                document.getElementById('txt_DateFrom').disabled = true;
                document.getElementById('txt_DateTo').disabled = true;
            }
            else {
                document.getElementById('txt_DateFrom').disabled = false;
                document.getElementById('txt_DateTo').disabled = false;
            }
        };

        function SetCriteria() {
            if (ps_ReportName == "ChartOfAccount") {
                document.getElementById('div_Location').style.display = "block";
            }
            else if (ps_ReportName == "LedgerDetail") {
                document.getElementById('div_Location').style.display = "block";
                document.getElementById('div_AccCodeFrom').style.display = "block";
                document.getElementById('div_AccCodeTo').style.display = "block";
                document.getElementById('div_DateRange').style.display = "block";

                document.getElementById('ddl_AccCodeFrom').disabled = true;
                document.getElementById('ddl_AccCodeTo').disabled = true;

                document.getElementById('txt_DateFrom').disabled = true;
                document.getElementById('txt_DateTo').disabled = true;
            }
        };

        function ViewReport() {
            if (ps_ReportName == "Company") {
                var Url = "../ReportSelectionCriteria/SetParameter?ps_ReportName=" + ps_ReportName + "&ps_Location=''";
            }
            else if (ps_ReportName == "Location") {
                var Url = "../ReportSelectionCriteria/SetParameter?ps_ReportName=" + ps_ReportName + "&ps_Location=''";
            }
            else if (ps_ReportName == "City") {
                var Url = "../ReportSelectionCriteria/SetParameter?ps_ReportName=" + ps_ReportName + "&ps_Location=''";
            }
            else if (ps_ReportName == "VoucherTypes") {
                var Url = "../ReportSelectionCriteria/SetParameter?ps_ReportName=" + ps_ReportName + "&ps_Location=''";
            }
            else if (ps_ReportName == "ChartOfAccount") {
                var Url = "../ReportSelectionCriteria/SetParameter?ps_ReportName=" + ps_ReportName + "&ps_Location=''";
            }
            else if (ps_ReportName == "LedgerDetail") {
                var pcnt_Location = document.getElementById('ddl_location');
                var Url = "../ReportSelectionCriteria/SetParameter?ps_ReportName=" + ps_ReportName + "&ps_Location=" + pcnt_Location.value;
            }
            else if (ps_ReportName == "TrialBalance") {
                var pcnt_Location = document.getElementById('ddl_location');
                var Url = "../ReportSelectionCriteria/SetParameter?ps_ReportName=" + ps_ReportName + "&ps_Location=" + pcnt_Location.value;
            };

            $.ajax({
                type: "GET",
                url: Url,
                success: function (response) {
                    html = response;
                },
                error: function (rs, e) {
                }
            });
            var Url = "../Reports/ViewReport.aspx";
            window.open(Url);
            return false;
        }
    </script>
    <form id="frm_ReportSelectionCriteria" action='<%=Url.Content("~/") %>'>
    <div class="box round first fullpage grid">
        <h2>
            Report Selection Criteria</h2>
        <div class="block">
            <div style="float: right; margin-bottom: 10px;">
                <div style="float: left; margin-right: 5px;">
                    <input id="btn_ViewReport" type="button" value="View Report" class="btn btn-blue"
                        onclick="ViewReport();" style="width: 100px; height: 35px; padding-top: 5px;
                        color: White; font-weight: bold; font-size: 11pt;" />
                    <img alt="" id="Waiting_Image" src="../../img/Ajax_Loading.gif" style="display: none;
                        margin-left: 10" /></div>
            </div>
            <div class="Clear">
            </div>
            <div id="div_Location" style="display: none;">
                <div class="CustomCell" style="width: 150px; height: 30px;">
                    Location</div>
                <%= Html.DropDownList("ddl_location", null, new { style = "width:955px;" })%>
            </div>
            <div id="div_AccCodeFrom" style="display: none;">
                <div class="CustomCell" style="width: 150px; height: 30px;">
                    Account Code From</div>
                <%= Html.DropDownList("ddl_AccCodeFrom", null, new { style = "width:955px;" })%>
            </div>
            <div id="div_AccCodeTo" style="display: none;">
                <div class="CustomCell" style="width: 150px; height: 30px;">
                    Account Code To</div>
                <%= Html.DropDownList("ddl_AccCodeTo", null, new { style = "width:955px;" })%>
                <input type="checkbox" class="checkbox" id="chk_AllAccCode" name="chk_AllAccCode"
                    onclick="CheckAllCode()" checked="checked" />
            </div>
            <div id="div_DateRange" style="display: none;">
                <div class="CustomCell" style="width: 150px; height: 30px;">
                    Date From</div>
                <div class="CustomCell" style="width: 282px; height: 30px;">
                    <input type="text" class="CustomText" style="width: 220px;" id="txt_DateFrom" name="txt_DateFrom"
                        value="<%=ViewData["CurrentDate"]%>" maxlength="50" />
                </div>
                <script type="text/javascript">
                    $('#txt_DateFrom').Zebra_DatePicker({
                        format: 'd/m/Y'
                    });
                </script>
                &nbsp;
                <div class="CustomCell" style="width: 150px; height: 30px;">
                    Date To</div>
                <div class="CustomCell" style="width: 282px; height: 30px;">
                    <input type="text" class="CustomText" style="width: 220px;" id="txt_DateTo" name="txt_DateTo"
                        value="<%=ViewData["CurrentDate"]%>" maxlength="50" />
                </div>
                <script type="text/javascript">
                    $('#txt_DateTo').Zebra_DatePicker({
                        format: 'd/m/Y'
                    });
                </script>
                <input type="checkbox" class="checkbox" id="chk_AllDate" name="chk_AllDate" onclick="CheckAllDate()"
                    checked="checked" />
            </div>
        </div>
    </div>
    </form>
</asp:Content>
