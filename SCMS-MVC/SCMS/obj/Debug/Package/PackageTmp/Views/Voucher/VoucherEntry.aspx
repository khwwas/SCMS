<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SCMSMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SCMS - Voucher Entry
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContenct" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            document.getElementById("ddl_VoucherType").focus();
        });

        function SaveVoucher() {
            var MessageBox = document.getElementById('MessageBox');
            var txt_SelectedMasterCode = document.getElementById("txt_SelectedMasterCode");
            var txt_SelectedDetailCode = document.getElementById("txt_SelectedDetailCode");
            var txt_Date = document.getElementById('txt_Date');
            var ddl_Status = document.getElementById('ddl_Status');
            var txt_Remarks = document.getElementById('txt_Remarks');
            var ddl_VoucherType = document.getElementById('ddl_VoucherType');
            var ddl_Account = document.getElementById('ddl_Account');
            var txt_Debit = document.getElementById('txt_Debit');
            var txt_Credit = document.getElementById('txt_Credit');
            var txt_Details = document.getElementById('txt_Details');
            var txt_Difference = document.getElementById('txt_Difference');
            var ddl_Location = document.getElementById('ddl_Location');

            if (txt_Date.value == "") {
                FadeIn(MessageBox);
                MessageBox.innerHTML = "<h5>Error!</h5><p>Please select date.</p>";
                MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(MessageBox);
                txt_CompanyName.focus();
                return;
            }
            else if (ddl_Status.value == "0") {
                FadeIn(MessageBox);
                MessageBox.innerHTML = "<h5>Error!</h5><p>Please select status.</p>";
                MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(MessageBox);
                txt_CompanyName.focus();
                return;
            }
            else if (ddl_VoucherType.value == "0") {
                FadeIn(MessageBox);
                MessageBox.innerHTML = "<h5>Error!</h5><p>Please select voucher type.</p>";
                MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(MessageBox);
                txt_CompanyName.focus();
                return;
            }
            else if (ddl_Status.value == "2" && (txt_Difference.value == "" || txt_Difference.value != "0")) {
                FadeIn(MessageBox);
                MessageBox.innerHTML = "<h5>Error!</h5><p>Debit and Credit values should be equal.</p>";
                MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(MessageBox);
                txt_CompanyName.focus();
                return;
            }
            else {
                var DivArr = $("[id^=DetailRow]");
                var RowCount = DivArr.length;
                var VoucherDetailRows = new Array(RowCount);

                if (ddl_Account != null) {
                    VoucherDetailRows[0] = ddl_Account.value + "~";
                }
                else {
                    VoucherDetailRows[0] = "NULL" + "~";
                }

                if (txt_Debit != null) {
                    VoucherDetailRows[0] += txt_Debit.value + "~";
                }
                else {
                    VoucherDetailRows[0] += "NULL" + "~";
                }

                if (txt_Credit != null) {
                    VoucherDetailRows[0] += txt_Credit.value + "~";
                }
                else {
                    VoucherDetailRows[0] += "NULL" + "~";
                }

                if (txt_Details != null) {
                    VoucherDetailRows[0] += txt_Details.value;
                }
                else {
                    VoucherDetailRows[0] += "NULL";
                }

                for (var index = 1; index < RowCount; index++) {

                    var ddl_Account = document.getElementById('ddl_Account' + index);
                    var txt_Debit = document.getElementById('txt_Debit' + index);
                    var txt_Credit = document.getElementById('txt_Credit' + index);
                    var txt_Details = document.getElementById('txt_Details' + index);

                    if (ddl_Account != null) {
                        VoucherDetailRows[index] = ddl_Account.value + "~";
                    }
                    else {
                        VoucherDetailRows[index] = "NULL" + "~";
                    }

                    if (txt_Debit != null) {
                        VoucherDetailRows[index] += txt_Debit.value + "~";
                    }
                    else {
                        VoucherDetailRows[index] += "NULL" + "~";
                    }

                    if (txt_Credit != null) {
                        VoucherDetailRows[index] += txt_Credit.value + "~";
                    }
                    else {
                        VoucherDetailRows[index] += "NULL" + "~";
                    }

                    if (txt_Details != null) {
                        VoucherDetailRows[index] += txt_Details.value;
                    }
                    else {
                        VoucherDetailRows[index] += "NULL";
                    }
                }

                var Url = document.getElementById('frm_VoucherEntry').action;
                Url += "Voucher/SaveVoucher?VoucherMasterCode=" + txt_SelectedMasterCode.value + "&VoucherDate=" + txt_Date.value + "&Status=" + ddl_Status.value + "&VoucherType=" + ddl_VoucherType.value + "&LocationId=" + ddl_Location.value + "&Remarks=" + txt_Remarks.value + "&VoucherDetailRows=" + VoucherDetailRows;
                $.ajax({
                    type: "GET",
                    url: Url,
                    success: function (response) {
                        html = response;
                        //$("#GridContainer").html(response);
                        //SetGrid();
                        //ResetForm();
                        var Div = document.createElement("div");
                        var VoucherId = "";
                        var VoucherCode = "[Auto]";
                        var ReturnValue = 0;
                        Div.innerHTML = html;
                        var InputArray = Div.getElementsByTagName("input");
                        for (var index = 0; index < InputArray.length; index++) {
                            if (InputArray[index].id == "VoucherId") {
                                VoucherId = InputArray[index].value;
                            }
                            if (InputArray[index].id == "VoucherCode") {
                                VoucherCode = InputArray[index].value;
                            }
                            if (InputArray[index].id == "ReturnValue") {
                                ReturnValue = InputArray[index].value;
                            }
                        }
                        FadeIn(MessageBox);
                        if (ReturnValue != "0") {
                            MessageBox.innerHTML = "<h5>Success!</h5><p>Record saved successfully.</p>";
                            MessageBox.setAttribute("class", "message success");
                        }
                        else {
                            MessageBox.innerHTML = "<h5>Error!</h5><p>Unable to save record.</p>";
                            MessageBox.setAttribute("class", "message error");
                        }
                        document.getElementById("Waiting_Image").style.display = "none";
                        document.getElementById("btn_Save").style.display = "block";
                        scroll(0, 0);
                        FadeOut(MessageBox);
                        document.getElementById("txt_Code").value = VoucherCode;
                        document.getElementById("txt_SelectedMasterCode").value = VoucherId;
                    },
                    error: function (rs, e) {
                        document.getElementById("Waiting_Image").style.display = "none";
                        document.getElementById("btn_Save").style.display = "block";
                    }
                });
            }
        }
        function ResetForm() {
            window.location = "../Voucher/VoucherEntry";
        }

        $(function () {
            $("#ddl_Account").combobox();
            $("#ddl_Account1").combobox();
            $("#ddl_Account2").combobox();
            $("#ddl_Account3").combobox();
        });

        function AddDetailRow(RowNo, AccountId, Detail, Debit, Credit) {
            var DivArr = $("[id^=DetailRow]");
            if (RowNo == null || RowNo == "") {
                RowNo = DivArr.length;
            }
            var Url = document.getElementById('frm_VoucherEntry').action;
            Url += "Voucher/NewVoucherDetailEntryRow?RowNo=" + RowNo + "&AccountCode=" + AccountId + "&Narration=" + Detail + "&Debit=" + Debit + "&Credit=" + Credit;
            $.ajax({
                type: "GET",
                url: Url,
                success: function (response) {
                    html = response;
                    var Div = document.createElement('div');
                    var IdString = "DetailRow" + RowNo.toString();
                    Div.setAttribute("id", IdString);
                    Div.setAttribute("style", "float:left;width:auto;");
                    Div.innerHTML += response;
                    document.getElementById("DetailContainer").appendChild(Div);
                    $("#ddl_Account" + RowNo).combobox();
                },
                error: function (rs, e) {

                }
            });
        }
        function SetTotals(Id) {
            var txt_TotalDebit = document.getElementById('txt_TotalDebit');
            var txt_TotalCredit = document.getElementById('txt_TotalCredit');
            var txt_Difference = document.getElementById('txt_Difference');
            var DivArr = $("[id^=DetailRow]");
            var RowNo = DivArr.length;
            var TotalDebits = 0;
            var TotalCredits = 0;
            var Difference = 0;
            if (document.getElementById("txt_Debit").value != "") {
                TotalDebits += parseFloat(document.getElementById("txt_Debit").value);
            }
            if (document.getElementById("txt_Credit").value != "") {
                TotalCredits += parseFloat(document.getElementById("txt_Credit").value);
            }
            for (var index = 1; index < RowNo; index++) {
                if (document.getElementById("txt_Debit" + index).value != "") {
                    TotalDebits += parseFloat(document.getElementById("txt_Debit" + index).value);
                }
                if (document.getElementById("txt_Credit" + index).value != "") {
                    TotalCredits += parseFloat(document.getElementById("txt_Credit" + index).value);
                }
            }
            txt_TotalDebit.style.color = "green";
            txt_TotalCredit.style.color = "green";
            txt_TotalDebit.value = TotalDebits;
            txt_TotalCredit.value = TotalCredits;
            if (TotalDebits < TotalCredits) {
                txt_TotalDebit.style.color = "red";
            }
            else {
                txt_TotalDebit.style.color = "green";
            }
            if (TotalCredits < TotalDebits) {
                txt_TotalCredit.style.color = "red";
            }
            else {
                txt_TotalCredit.style.color = "green";
            }
            Difference = TotalDebits - TotalCredits;
            txt_Difference.style.color = "green";
            if (Difference < 0) {
                Difference = TotalCredits - TotalDebits;
            }
            if (Difference > 0) {
                txt_Difference.style.color = "red";
            }
            txt_Difference.value = Difference;

            if (document.getElementById(Id).value != "" && Id.match(/Debit/) != null) {
                document.getElementById(Id.replace("Debit", "Credit")).disabled = "disabled";
            }
            else {
                document.getElementById(Id.replace("Debit", "Credit")).disabled = "";
            }
            if (document.getElementById(Id).value != "" && Id.match(/Credit/) != null) {
                document.getElementById(Id.replace("Credit", "Debit")).disabled = "disabled";
            }
            else {
                document.getElementById(Id.replace("Credit", "Debit")).disabled = "";
            }
        }
    </script>
    <form id="frm_VoucherEntry" action='<%=Url.Content("~/") %>'>
    <input type="hidden" id="txt_SelectedMasterCode" name="txt_SelectedMasterCode" value='<%=ViewData["VoucherId"] %>' />
    <input type="hidden" id="txt_SelectedDetailCode" name="txt_SelectedDetailCode" value="" />
    <div class="box round first fullpage grid">
        <h2>
            Voucher Entry</h2>
        <div class="block">
            <div id="MessageBox">
            </div>
            <div class="CustomCell" style="width: 97px; height: 30px;">
                Code</div>
            <div class="CustomCell" style="width: 320px; height: 30px;">
                <input type="text" class="CustomText" style="width: 250px;" id="txt_Code" name="txt_Code"
                    maxlength="15" readonly="readonly" value='<%=ViewData["VoucherCode"] %>' />
            </div>
            <div class="CustomCell" style="width: 40px; height: 30px;">
                Date</div>
            <div class="CustomCell" style="width: 282px; height: 30px;">
                <input type="text" class="CustomText" style="width: 220px;" id="txt_Date" name="txt_Date"
                    value="<%=ViewData["CurrentDate"]%>" maxlength="50" />
            </div>
            <script type="text/javascript">
                $('#txt_Date').Zebra_DatePicker({
                    format: 'm/d/Y'
                });
            </script>
        </div>
        <div class="CustomCell" style="width: 50px; height: 30px;">
            Status</div>
        <div class="CustomCell" style="width: 320px; height: 30px;">
            <select id="ddl_Status" name="ddl_Status" class="CustomText" style="width: 251px;">
                <option value="1">Pending </option>
                <option value="2">Approved </option>
            </select>
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
        <div class="CustomCell" style="width: 97px; height: 30px;">
            Location</div>
        <div class="CustomCell" style="width: 270px; height: 30px;">
            <%= Html.DropDownList("ddl_Location", null, new { style = "width:955px;" })%>
        </div>
        <div class="Clear">
        </div>
        <div class="CustomCell" style="width: 97px; height: 30px;">
            Remarks</div>
        <div class="CustomCell" style="width: 960px; height: 30px;">
            <input type="text" value="<%=ViewData["Remarks"] %>" class="CustomText" style="width: 940px;"
                id="txt_Remarks" name="txt_Remarks" maxlength="50" />
        </div>
        <hr style="padding: 0; margin-bottom: 5px;" />
        <div class="CustomCell" style="width: 252px;">
            Account Title
        </div>
        <div class="CustomCell" style="width: 567px;">
            Narration</div>
        <div class="CustomCell" style="width: 120px;">
            Dr.</div>
        <div class="CustomCell" style="width: 118px;">
            Cr.</div>
        <div class="Clear">
        </div>
        <div id="DetailContainer">
            <div id="DetailRow" style="float: left; width: auto;">
                <div class="CustomCell" style="width: 250px; height: 30px;">
                    <%= Html.DropDownList("ddl_Account", null, new { style ="width:250px;" })%>
                </div>
                <div class="CustomCell" style="width: 565px; height: 30px;">
                    <input type="text" class="CustomText" style="width: 545px;" id="txt_Details" name="txt_Details"
                        maxlength="50" />
                </div>
                <div class="CustomCell" style="width: 118px; height: 30px;">
                    <input type="text" class="CustomText" style="width: 100px;" id="txt_Debit" name="txt_Debit"
                        maxlength="50" onblur="SetTotals(this.id)" />
                </div>
                <div class="CustomCell" style="width: 118px; height: 30px;">
                    <input type="text" class="CustomText" style="width: 100px;" id="txt_Credit" name="txt_Credit"
                        maxlength="50" onblur="SetTotals(this.id)" />
                </div>
            </div>
            <div id="DetailRow1" style="float: left; width: auto;">
                <div class="CustomCell" style="width: 250px; height: 30px;">
                    <% ViewData["ddl_Account1"] = new SelectList((List<SCMSDataLayer.DB.SETUP_ChartOfAccount>)ViewData["ChartOfAccounts"], "ChrtAcc_Id", "ChrtAcc_Title", "");%>
                    <%= Html.DropDownList("ddl_Account1", null,new { style = "width:250px;" })%>
                </div>
                <div class="CustomCell" style="width: 565px; height: 30px;">
                    <input type="text" class="CustomText" style="width: 545px;" id="txt_Details1" name="txt_Details"
                        maxlength="50" />
                </div>
                <div class="CustomCell" style="width: 118px; height: 30px;">
                    <input type="text" class="CustomText" style="width: 100px;" id="txt_Debit1" name="txt_Debit"
                        maxlength="50" onblur="SetTotals(this.id)" />
                </div>
                <div class="CustomCell" style="width: 118px; height: 30px;">
                    <input type="text" class="CustomText" style="width: 100px;" id="txt_Credit1" name="txt_Credit"
                        maxlength="50" onblur="SetTotals(this.id)" />
                </div>
            </div>
            <div id="DetailRow2" style="float: left; width: auto;">
                <div class="CustomCell" style="width: 250px; height: 30px;">
                    <% ViewData["ddl_Account2"] = new SelectList((List<SCMSDataLayer.DB.SETUP_ChartOfAccount>)ViewData["ChartOfAccounts"], "ChrtAcc_Id", "ChrtAcc_Title", "");%>
                    <%= Html.DropDownList("ddl_Account2", null,new { style = "width:250px;" })%>
                </div>
                <div class="CustomCell" style="width: 565px; height: 30px;">
                    <input type="text" class="CustomText" style="width: 545px;" id="txt_Details2" name="txt_Details"
                        maxlength="50" />
                </div>
                <div class="CustomCell" style="width: 118px; height: 30px;">
                    <input type="text" class="CustomText" style="width: 100px;" id="txt_Debit2" name="txt_Debit"
                        maxlength="50" onblur="SetTotals(this.id)" />
                </div>
                <div class="CustomCell" style="width: 118px; height: 30px;">
                    <input type="text" class="CustomText" style="width: 100px;" id="txt_Credit2" name="txt_Credit"
                        maxlength="50" onblur="SetTotals(this.id)" />
                </div>
            </div>
            <div id="DetailRow3" style="float: left; width: auto;">
                <div class="CustomCell" style="width: 250px; height: 30px;">
                    <% ViewData["ddl_Account3"] = new SelectList((List<SCMSDataLayer.DB.SETUP_ChartOfAccount>)ViewData["ChartOfAccounts"], "ChrtAcc_Id", "ChrtAcc_Title", "");%>
                    <%= Html.DropDownList("ddl_Account3", null,new { style = "width:250px;" })%>
                </div>
                <div class="CustomCell" style="width: 565px; height: 30px;">
                    <input type="text" class="CustomText" style="width: 545px;" id="txt_Details3" name="txt_Details"
                        maxlength="50" />
                </div>
                <div class="CustomCell" style="width: 118px; height: 30px;">
                    <input type="text" class="CustomText" style="width: 100px;" id="txt_Debit3" name="txt_Debit"
                        maxlength="50" onblur="SetTotals(this.id)" />
                </div>
                <div class="CustomCell" style="width: 118px; height: 30px;">
                    <input type="text" class="CustomText" style="width: 100px;" id="txt_Credit3" name="txt_Credit"
                        maxlength="50" onblur="SetTotals(this.id)" />
                </div>
            </div>
        </div>
        <div style="float: left;">
            <img alt="Add New" src="../../img/add.png" style="width: 30px; cursor: pointer;"
                onclick='javascript:AddDetailRow("");' />
        </div>
        <hr style="padding: 0; margin-bottom: 5px;" />
        <div class="CustomCell" style="width: 820px; height: 30px; text-align: right;">
            Total &nbsp;
        </div>
        <div class="CustomCell" style="width: 118px; height: 30px;">
            <input type="text" class="CustomText" style="width: 100px;" id="txt_TotalDebit" name="txt_TotalDebit"
                maxlength="50" disabled="disabled" />
        </div>
        <div class="CustomCell" style="width: 118px; height: 30px;">
            <input type="text" class="CustomText" style="width: 100px;" id="txt_TotalCredit"
                name="txt_TotalCredit" maxlength="50" disabled="disabled" />
        </div>
        <div class="CustomCell" style="width: 30px; height: 30px; text-align: right;">
            Diff &nbsp;
        </div>
        <div class="CustomCell" style="width: 165px; height: 30px;">
            <input type="text" class="CustomText" style="width: 100px;" id="txt_Difference" disabled="disabled"
                name="txt_Difference" maxlength="50" />
        </div>
        <div class="Clear">
        </div>
        <div style="float: right; margin-bottom: 10px;">
            <div style="float: left; margin-right: 5px;">
                <input id="btn_Save" type="button" value="Post" class="btn btn-blue" onclick="SaveVoucher();"
                    style="width: 90px; height: 35px; padding-top: 5px; color: White; font-weight: bold;
                    font-size: 11pt;" />
                <img alt="" id="Waiting_Image" src="../../img/Ajax_Loading.gif" style="display: none;
                    margin-left: 10" /></div>
            <div style="float: left;">
                <input type="button" value="Reset" class="btn btn-grey" onclick="ResetForm();" style="width: 90px;
                    height: 35px; padding-top: 5px; color: White; font-weight: bold; font-size: 11pt;" />
            </div>
        </div>
        <%-- <hr/> <div id="GridContainer"> <%Html.RenderPartial("GridData");%> </div>--%>
    </div>
    </form>
    <script type="text/javascript">
        document.getElementById("ddl_Status").value = '<%=ViewData["Status"] %>';
        document.getElementById("ddl_VoucherType").value = '<%=ViewData["VoucherType"] %>';
        document.getElementById("ddl_Location").value = '<%=ViewData["LocationId"] %>';
        var TotalRows = '<%=ViewData["RowsCount"] %>';
        var Debit = "";
        var Credit = "";

        document.getElementById("ddl_Account").value = '<%=ViewData["AccountId"] %>';
        document.getElementById("txt_Details").value = '<%=ViewData["txt_Details"] %>';

        Debit = '<%=ViewData["txt_Debit"] %>';
        if (Debit != null && parseFloat(Debit) > 0) {
            document.getElementById("txt_Debit").value = parseFloat(Debit).toFixed(2);
            SetTotals("txt_Debit");
        }
        Credit = '<%=ViewData["txt_Credit"] %>';
        if (Credit != null && parseFloat(Credit) > 0) {
            document.getElementById("txt_Credit").value = parseFloat(Credit).toFixed(2);
            SetTotals("txt_Credit");
        }

        document.getElementById("ddl_Account1").value = '<%=ViewData["AccountId1"] %>';
        document.getElementById("txt_Details1").value = '<%=ViewData["txt_Details1"] %>';

        Debit = '<%=ViewData["txt_Debit1"] %>';
        if (Debit != null && parseFloat(Debit) > 0) {
            document.getElementById("txt_Debit1").value = parseFloat(Debit).toFixed(2);
            SetTotals("txt_Debit1");
        }
        Credit = '<%=ViewData["txt_Credit1"] %>';
        if (Credit != null && parseFloat(Credit) > 0) {
            document.getElementById("txt_Credit1").value = parseFloat(Credit).toFixed(2);
            SetTotals("txt_Credit1");
        }

        document.getElementById("ddl_Account2").value = '<%=ViewData["AccountId2"] %>';
        document.getElementById("txt_Details2").value = '<%=ViewData["txt_Details2"] %>';
        Debit = '<%=ViewData["txt_Debit2"] %>';
        if (Debit != null && parseFloat(Debit) > 0) {
            document.getElementById("txt_Debit2").value = parseFloat(Debit).toFixed(2);
            SetTotals("txt_Debit2");
        }
        Credit = '<%=ViewData["txt_Credit2"] %>';
        if (Credit != null && parseFloat(Credit) > 0) {
            document.getElementById("txt_Credit2").value = parseFloat(Credit).toFixed(2);
            SetTotals("txt_Credit2");
        }
        document.getElementById("ddl_Account3").value = '<%=ViewData["AccountId3"] %>';
        document.getElementById("txt_Details3").value = '<%=ViewData["txt_Details3"] %>';
        Debit = '<%=ViewData["txt_Debit3"] %>';
        if (Debit != null && parseFloat(Debit) > 0) {
            document.getElementById("txt_Debit3").value = parseFloat(Debit).toFixed(2);
            SetTotals("txt_Debit3");
        }
        Credit = '<%=ViewData["txt_Credit3"] %>';
        if (Credit != null && parseFloat(Credit) > 0) {
            document.getElementById("txt_Credit3").value = parseFloat(Credit).toFixed(2);
            SetTotals("txt_Credit3");
        }
    </script>
    <% var TotalRows = ViewData["RowsCount"];
       if (TotalRows != null && Convert.ToInt32(TotalRows) > 0)
       {
           for (int index = 4; index < Convert.ToInt32(TotalRows); index++)
           {
               var Debit = ViewData["txt_Debit" + index.ToString()];
               var Credit = ViewData["txt_Credit" + index.ToString()];
               var Narration = ViewData["txt_Details" + index.ToString()];
               var AccountId = ViewData["AccountId" + index.ToString()];
               string flag = "txt_Debit";
               if (Credit != null && Credit != "" && Convert.ToInt32(Credit) > 0)
               {
                   flag = "txt_Credit";
               }
    %>
    <script type="text/javascript">
        AddDetailRow('<%=index %>', '<%=AccountId %>', '<%=Narration %>', '<%=Debit %>', '<%=Credit %>');
        var txt_TotalDebit = document.getElementById('txt_TotalDebit');
        var txt_TotalCredit = document.getElementById('txt_TotalCredit');
        var txt_Difference = document.getElementById('txt_Difference');
        var flag = '<%=flag%>';
        if (flag == "txt_Debit") {
            txt_TotalDebit.value = parseFloat(txt_TotalDebit.value) + parseFloat('<%=Debit %>');
        }
        else {
            txt_TotalCredit.value = parseFloat(txt_TotalCredit.value) + parseFloat('<%=Credit %>');
        }

        txt_TotalDebit.style.color = "green";
        txt_TotalCredit.style.color = "green";

        if (parseFloat(txt_TotalDebit.value) < parseFloat(txt_TotalCredit.value)) {
            txt_TotalDebit.style.color = "red";
        }
        else {
            txt_TotalDebit.style.color = "green";
        }
        if (parseFloat(txt_TotalCredit.value) < parseFloat(txt_TotalDebit.value)) {
            txt_TotalCredit.style.color = "red";
        }
        else {
            txt_TotalCredit.style.color = "green";
        }
        var Difference = parseFloat(txt_TotalDebit.value) - parseFloat(txt_TotalCredit.value);
        txt_Difference.style.color = "green";
        if (Difference < 0) {
            Difference = parseFloat(txt_TotalCredit.value) - parseFloat(txt_TotalDebit.value);
        }
        if (Difference > 0) {
            txt_Difference.style.color = "red";
        }
        txt_Difference.value = Difference;
    </script>
    <% }
       }
        
    %>
</asp:Content>
