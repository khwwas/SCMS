<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SCMSMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Report Selection Criteria
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        var ps_ReportName, ps_Url;
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

        function CheckAllVchrDoc() {
            if (document.getElementById('chk_AllVchrDoc').checked == true) {
                document.getElementById('ddl_VchrDocFrom').disabled = true;
                document.getElementById('ddl_VchrDocTo').disabled = true;
            }
            else {
                document.getElementById('ddl_VchrDocFrom').disabled = false;
                document.getElementById('ddl_VchrDocTo').disabled = false;
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
            if (ps_ReportName.toLowerCase() == "ChartOfAccount".toLowerCase()) {
                document.getElementById('div_Level').style.display = "block";
            }
            else if (ps_ReportName.toLowerCase() == "VoucherDocument".toLowerCase()) {
                document.getElementById('div_Location').style.display = "block";
                document.getElementById('div_VoucherTypes').style.display = "block";

                document.getElementById('div_VchrDocFrom').style.display = "block";
                document.getElementById('div_VchrDocTo').style.display = "block";
                document.getElementById('ddl_VchrDocFrom').disabled = true;
                document.getElementById('ddl_VchrDocTo').disabled = true;

                document.getElementById('div_DateRange').style.display = "block";
                document.getElementById('txt_DateFrom').disabled = true;
                document.getElementById('txt_DateTo').disabled = true;
            }
            else if (ps_ReportName.toLowerCase() == "LedgerDtLocWise".toLowerCase()) {
                document.getElementById('div_Location').style.display = "block";
                document.getElementById('div_AccCodeFrom').style.display = "block";
                document.getElementById('div_AccCodeTo').style.display = "block";
                document.getElementById('div_DateRange').style.display = "block";

                document.getElementById('ddl_AccCodeFrom').disabled = true;
                document.getElementById('ddl_AccCodeTo').disabled = true;

                document.getElementById('txt_DateFrom').disabled = true;
                document.getElementById('txt_DateTo').disabled = true;
            }
            else if (ps_ReportName.toLowerCase() == "LedgerDtAccWise".toLowerCase()) {
                document.getElementById('div_Location').style.display = "block";
                document.getElementById('div_AccCodeFrom').style.display = "block";
                document.getElementById('div_AccCodeTo').style.display = "block";
                document.getElementById('div_DateRange').style.display = "block";

                document.getElementById('ddl_AccCodeFrom').disabled = true;
                document.getElementById('ddl_AccCodeTo').disabled = true;

                document.getElementById('txt_DateFrom').disabled = true;
                document.getElementById('txt_DateTo').disabled = true;
            }
            else if (ps_ReportName.toLowerCase() == "TrialBalance".toLowerCase()) {
                document.getElementById('div_Location').style.display = "block";
                document.getElementById('div_AccCodeFrom').style.display = "block";
                document.getElementById('div_AccCodeTo').style.display = "block";
                document.getElementById('div_DateRange').style.display = "block";
                document.getElementById('div_TrialActivity').style.display = "block";

                document.getElementById('ddl_AccCodeFrom').disabled = true;
                document.getElementById('ddl_AccCodeTo').disabled = true;

                document.getElementById('txt_DateFrom').disabled = true;
                document.getElementById('txt_DateTo').disabled = true;
            }
            else if (ps_ReportName.toLowerCase() == "IncomeStatement".toLowerCase()) {
                document.getElementById('div_Location').style.display = "block";
                document.getElementById('div_Level').style.display = "block";
                document.getElementById('div_Year').style.display = "block";
            }
        };

        function ViewReport() {
            if (ps_ReportName.toLowerCase() == "Company".toLowerCase() || ps_ReportName.toLowerCase() == "Location".toLowerCase() ||
                ps_ReportName.toLowerCase() == "City".toLowerCase() || ps_ReportName.toLowerCase() == "VoucherTypes".toLowerCase()) {
                ps_Url = "../ReportSelectionCriteria/SetParam_Setups?ps_ReportName=" + ps_ReportName;
            }
            else if (ps_ReportName.toLowerCase() == "ChartOfAccount".toLowerCase()) {
                var pcnt_ChartOfAccount = document.getElementById('txt_Level');
                ps_Url = "../ReportSelectionCriteria/SetParam_ChartOfAccount?ps_ReportName=" + ps_ReportName + "&pi_Level=" + pcnt_ChartOfAccount.value;
            }
            else if (ps_ReportName.toLowerCase() == "VoucherDocument".toLowerCase()) {
                var pcnt_Location = document.getElementById('ddl_location');
                var pcnt_VoucherTypes = document.getElementById('ddl_VoucherTypes');
                var pcnt_AllDate = document.getElementById('chk_AllDate');
                var pcnt_DateFrom = document.getElementById('txt_DateFrom');
                var pcnt_DateTo = document.getElementById('txt_DateTo');
                var pcnt_AllDoc = document.getElementById('chk_AllVchrDoc');
                var pcnt_DocFrom = document.getElementById('ddl_VchrDocFrom');
                var pcnt_DocTo = document.getElementById('ddl_VchrDocTo');

                var li_AllDoc, li_AllDate;

                if (pcnt_AllDoc.checked == true) {
                    li_AllDoc = 1;
                }
                else {
                    li_AllDoc = 0;
                }

                if (pcnt_AllDate.checked == true) {
                    li_AllDate = 1;
                }
                else {
                    li_AllDate = 0;
                }

                ps_Url = "../ReportSelectionCriteria/SetParam_VoucherDocument?ps_ReportName=" + ps_ReportName + "&ps_Location=" + pcnt_Location.value +
                         "&ps_VoucherTypes=" + pcnt_VoucherTypes.value + "&pi_AllDoc=" + li_AllDoc.toString() + "&ps_DocFrom=" + pcnt_DocFrom.value + "&ps_DocTo=" + pcnt_DocTo.value +
                         "&pi_AllDate=" + li_AllDate.toString() + "&pdt_DateFrom=" + pcnt_DateFrom.value + "&pdt_DateTo=" + pcnt_DateTo.value + "";
            }
            else if (ps_ReportName.toLowerCase() == "LedgerDtLocWise".toLowerCase() ||
                     ps_ReportName.toLowerCase() == "LedgerDtAccWise".toLowerCase()) {
                var pcnt_Location = document.getElementById('ddl_location');

                var pcnt_AllAccCode = document.getElementById('chk_AllAccCode').checked;
                var pcnt_AccCodeFrom = document.getElementById('ddl_AccCodeFrom');
                var pcnt_AccCodeTo = document.getElementById('ddl_AccCodeTo');

                var pcnt_AllDate = document.getElementById('chk_AllDate').checked;
                var pcnt_DateFrom = document.getElementById('txt_DateFrom');
                var pcnt_DateTo = document.getElementById('txt_DateTo');

                var li_AllCode, li_AllDate;

                if (pcnt_AllAccCode.toString() == "true") {
                    li_AllCode = 1;
                }
                else {
                    li_AllCode = 0;
                }

                if (pcnt_AllDate.toString() == "true") {
                    li_AllDate = 1;
                }
                else {
                    li_AllDate = 0;
                }

                ps_Url = "../ReportSelectionCriteria/SetParam_LedgerDetail?ps_ReportName=" + ps_ReportName + "&ps_Location=" + pcnt_Location.value +
                         "&pi_AllAccCode=" + li_AllCode.toString() + "&ps_AccCodeFrom=" + pcnt_AccCodeFrom.value + "&ps_AccCodeTo=" + pcnt_AccCodeTo.value +
                         "&pi_AllDate=" + li_AllDate.toString() + "&pdt_DateFrom=" + pcnt_DateFrom.value + "&pdt_DateTo=" + pcnt_DateTo.value + "";
            }
            else if (ps_ReportName.toLowerCase() == "TrialBalance".toLowerCase()) {
                var pcnt_Location = document.getElementById('ddl_location');

                var pcnt_AllAccCode = document.getElementById('chk_AllAccCode').checked;
                var pcnt_AccCodeFrom = document.getElementById('ddl_AccCodeFrom');
                var pcnt_AccCodeTo = document.getElementById('ddl_AccCodeTo');

                var pcnt_AllDate = document.getElementById('chk_AllDate').checked;
                var pcnt_DateFrom = document.getElementById('txt_DateFrom');
                var pcnt_DateTo = document.getElementById('txt_DateTo');

                var pcnt_Acticity = document.getElementById('rdo_Activity').checked;
                var pcnt_ActivityAll = document.getElementById('rdo_ActivityAll').checked;

                var li_AllCode, li_AllDate, ls_TrialActivity;

                if (pcnt_AllAccCode.toString() == "true") {
                    li_AllCode = 1;
                }
                else {
                    li_AllCode = 0;
                }

                if (pcnt_AllDate.toString() == "true") {
                    li_AllDate = 1;
                }
                else {
                    li_AllDate = 0;
                }

                if (pcnt_Acticity.toString() == "true") {
                    ls_TrialActivity = "Activity";
                }
                else if (pcnt_ActivityAll.toString() == "true") {
                    ls_TrialActivity = "ActivityAll";
                }

                ps_Url = "../ReportSelectionCriteria/SetParam_TrialBalance?ps_ReportName=" + ps_ReportName + "&ps_Location=" + pcnt_Location.value +
                         "&pi_AllAccCode=" + li_AllCode.toString() + "&ps_AccCodeFrom=" + pcnt_AccCodeFrom.value + "&ps_AccCodeTo=" + pcnt_AccCodeTo.value +
                         "&pi_AllDate=" + li_AllDate.toString() + "&pdt_DateFrom=" + pcnt_DateFrom.value + "&pdt_DateTo=" + pcnt_DateTo.value +
                         "&ps_TrialActivity=" + ls_TrialActivity + "";
            }
            else if (ps_ReportName.toLowerCase() == "IncomeStatement".toLowerCase()) {
                var pcnt_Location = document.getElementById('ddl_location');
                var pcnt_Level = document.getElementById('txt_Level');
                var pcnt_Year = document.getElementById('ddl_Year');

                ps_Url = "../ReportSelectionCriteria/SetParam_IncomeStatement?ps_ReportName=" + ps_ReportName + "&ps_Location=" + pcnt_Location.value +
                         "&pi_Level=" + pcnt_Level.value + "&pi_Year=" + pcnt_Year.value;
            };

            $.ajax({
                type: "GET",
                url: ps_Url,
                success: function (response) {
                    html = response;
                    if (response == "OK") {
                        window.open("../Reports/ViewReport.aspx");
                    }
                },
                error: function (rs, e) {
                    alert("Parameters not set");
                }
            });

        }
    </script>
    <form id="frm_ReportSelectionCriteria" action='<%=Url.Content("~/") %>'>
    <div class="box round first fullpage grid">
        <h2>
            Report Selection Criteria for
            <%= ViewData["ReportName"] %></h2>
        <div class="block">
            <div style="float: right; margin-bottom: 10px;">
                <div style="float: left; margin-right: 5px;">
                    <input id="btn_ViewReport" type="button" value="View Report" class="btn btn-blue"
                        onclick="ViewReport();" style="width: 120px; height: 35px; padding-top: 5px;
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
            <div class="Clear">
            </div>
            <div id="div_VoucherTypes" style="display: none;">
                <div class="CustomCell" style="width: 150px; height: 30px;">
                    Voucher Type</div>
                <%= Html.DropDownList("ddl_VoucherTypes", null, new { style = "width:955px;" })%>
            </div>
            <div class="Clear">
            </div>
            <div id="div_AccCodeFrom" style="display: none;">
                <div class="CustomCell" style="width: 150px; height: 30px;">
                    Account Code From</div>
                <%= Html.DropDownList("ddl_AccCodeFrom", null, new { style = "width:955px;" })%>
            </div>
            <div class="Clear">
            </div>
            <div id="div_AccCodeTo" style="display: none;">
                <div class="CustomCell" style="width: 150px; height: 30px;">
                    Account Code To</div>
                <%= Html.DropDownList("ddl_AccCodeTo", null, new { style = "width:955px;" })%>
                <input type="checkbox" class="checkbox" id="chk_AllAccCode" name="chk_AllAccCode"
                    onclick="CheckAllCode()" checked="checked" />
            </div>
            <div class="Clear">
            </div>
            <div id="div_VchrDocFrom" style="display: none;">
                <div class="CustomCell" style="width: 150px; height: 30px;">
                    Document From</div>
                <%= Html.DropDownList("ddl_VchrDocFrom", null, new { style = "width:955px;" })%>
            </div>
            <div class="Clear">
            </div>
            <div id="div_VchrDocTo" style="display: none;">
                <div class="CustomCell" style="width: 150px; height: 30px;">
                    Document To</div>
                <%= Html.DropDownList("ddl_VchrDocTo", null, new { style = "width:955px;" })%>
                <input type="checkbox" class="checkbox" id="chk_AllVchrDoc" name="chk_AllVchrDoc"
                    onclick="CheckAllVchrDoc()" checked="checked" />
            </div>
            <div class="Clear">
            </div>
            <div id="div_DateRange" style="display: none;">
                <div class="CustomCell" style="width: 150px; height: 30px;">
                    Date From</div>
                <div class="CustomCell" style="width: 282px; height: 30px;">
                    <input type="text" class="CustomText" style="width: 220px;" id="txt_DateFrom" name="txt_DateFrom"
                        value="<%=ViewData["CurrentDate"]%>" maxlength="50" />
                </div>
                <script type="text/javascript">
                    $('#txt_DateFrom').datepicker().on('changeDate', function (ev) {
                        $('#txt_DateFrom').datepicker("hide");
                    });
                </script>
                &nbsp;
                <div class="CustomCell" style="width: 140px; height: 30px;">
                    Date To</div>
                <div class="CustomCell" style="width: 282px; height: 30px;">
                    <input type="text" class="CustomText" style="width: 220px;" id="txt_DateTo" name="txt_DateTo"
                        value="<%=ViewData["CurrentDate"]%>" maxlength="50" />
                </div>
                <script type="text/javascript">
                    $('#txt_DateTo').datepicker().on('changeDate', function (ev) {
                        $('#txt_DateTo').datepicker("hide");
                    });
                </script>
                <input type="checkbox" class="checkbox" id="chk_AllDate" name="chk_AllDate" onclick="CheckAllDate()"
                    checked="checked" />
            </div>
            <div class="Clear">
            </div>
            <div id="div_Year" style="display: none;">
                <div class="CustomCell" style="width: 150px; height: 30px;">
                    Year</div>
                <select name="ddl_Year" id="ddl_Year" style="width: 955px;">
                    <option value="2008">2008</option>
                    <option value="2009">2009</option>
                    <option value="2010">2010</option>
                    <option value="2011">2011</option>
                    <option value="2012">2012</option>
                    <option value="2013">2013</option>
                    <option value="2014">2014</option>
                </select>
            </div>
            <div class="Clear">
            </div>
            <div id="div_Level" style="display: none;">
                <div class="CustomCell" style="width: 150px;">
                    Level</div>
                <div class="CustomCell" style="width: 42px;">
                    <input type="text" class="CustomText" id="txt_Level" name="txt_Level" maxlength="1"
                        style="width: 35px; border-right: 0px;" value="1" disabled="disabled" />
                </div>
                <div class="CustomCell" style="border: 1px solid #ccc; width: 20px; height: 28px;
                    margin-right: 12px;">
                    <div style="background-image: url('../../img/ArrowUp.png'); background-position: center;
                        height: 14px; background-repeat: no-repeat; cursor: pointer;" onclick="ValuePlus('txt_Level');">
                        &nbsp;
                    </div>
                    <div style="background-image: url('../../img/ArrowDown.png'); background-position: center;
                        height: 14px; background-repeat: no-repeat; cursor: pointer;" onclick="ValueMinus('txt_Level');">
                        &nbsp;
                    </div>
                </div>
            </div>
            <div class="Clear">
            </div>
            <div id="div_TrialActivity" style="display: none;">
                <div class="CustomCell" style="width: 150px; height: 30px;">
                    Trial Activity
                </div>
                <div class="CustomCell" style="width: 650px;">
                    Activity&nbsp;<input type="radio" class="radio" id="rdo_Activity" name="TrialActivity"
                        value="Activity" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Activity/Opening/Closing&nbsp;<input
                        type="radio" class="radio" id="rdo_ActivityAll" name="TrialActivity" checked="checked"
                        value="All" />
                </div>
            </div>
        </div>
    </div>
    </form>
</asp:Content>
