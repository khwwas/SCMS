<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SCMSMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Voucher Entry
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContenct" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .ui-autocomplete
        {
            max-height: 200px;
            overflow-y: auto;
            width: auto;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#txt_Date').Zebra_DatePicker({
                format: 'm/d/Y'
            });
            $("[name=ddl_Account]").combobox();
            var Titles = document.getElementById('NarrationTitles').value;
            var data = "";
            if (Titles != null && Titles != "") {
                data = JSON.parse(Titles);
            }
            $("[name=txt_Details]").autocomplete({ source: data });
            $("#btn_AddNewRow").click(function () {

                var comboData = $("#AccountCodesList").val().split(',');
                var htmlStr = "<div class='detailRow' style='float: left; width: auto;'>";
                htmlStr += "<div class='CustomCell' style='width: 250px; height: 30px;'>";
                htmlStr += "<select style='width: 250px;' name='ddl_Account'>";
                htmlStr += "<option value='0'></option>";
                for (var index = 0; index < comboData.length; index++) {
                    var comboArr = comboData[index].split(':');
                    htmlStr += "<option value='" + comboArr[0] + "'>" + comboArr[1] + "</option>";
                }
                htmlStr += "</select>";
                htmlStr += "</div>";
                htmlStr += "<div class='CustomCell' style='width: 565px; height: 30px;'>";
                htmlStr += "<input type='text' class='CustomText' style='width: 545px;' name='txt_Details' maxlength='200' />";
                htmlStr += "</div>";
                htmlStr += "<div class='CustomCell' style='width: 118px; height: 30px;'>";
                htmlStr += "<input type='text' class='CustomText' style='width: 100px;' name='txt_Debit' maxlength='50' />";
                htmlStr += "</div>";
                htmlStr += "<div class='CustomCell' style='width: 118px; height: 30px;'>";
                htmlStr += "<input type='text' class='CustomText' style='width: 100px;' name='txt_Credit' maxlength='50' />";
                htmlStr += "</div>";
                htmlStr += "</div>";
                $(".detailRow").last().after(htmlStr);
                $(".detailRow").last().find("[name=ddl_Account]").combobox();
                $(".detailRow").last().find("[name=txt_Details]").autocomplete({ source: data });
                $(".detailRow").last().find("[name=txt_Debit],[name=txt_Credit]").blur(function () {
                    if ($(this).attr("name") == "txt_Debit" && $(this).val() != "") {
                        $(this).parent().next().find("input").attr("disabled", "disabled");
                    }
                    else {
                        $(this).parent().next().find("input").removeAttr("disabled");
                    }

                    if ($(this).attr("name") == "txt_Credit" && $(this).val() != "") {
                        $(this).parent().prev().find("input").attr("disabled", "disabled");
                    }
                    else {
                        $(this).parent().prev().find("input").removeAttr("disabled");
                    }

                    var sum = 0;
                    $("input[name=txt_Debit]").each(function () {
                        if ($(this).val() != "") {
                            sum += parseFloat($(this).val());
                        }
                    });
                    $("#txt_TotalDebit").val(sum);

                    sum = 0;
                    $("input[name=txt_Credit]").each(function () {
                        if ($(this).val() != "") {
                            sum += parseFloat($(this).val());
                        }
                    });
                    $("#txt_TotalCredit").val(sum);

                    $("#txt_TotalDebit").css("color", "green");
                    $("#txt_TotalCredit").css("color", "green");
                    $("#txt_Difference").css("color", "green");
                    if ($("#txt_TotalDebit").val() < $("#txt_TotalCredit").val()) {
                        $("#txt_TotalDebit").css("color", "red");
                        $("#txt_TotalCredit").css("color", "green");
                    }
                    if ($("#txt_TotalCredit").val() < $("#txt_TotalDebit").val()) {
                        $("#txt_TotalDebit").css("color", "green");
                        $("#txt_TotalCredit").css("color", "red");
                    }

                    $("#txt_Difference").val(parseFloat($("#txt_TotalDebit").val()) - parseFloat($("#txt_TotalCredit").val()));
                    if ($("#txt_Difference").val() < 0) {
                        $("#txt_Difference").val(parseFloat($("#txt_TotalCredit").val()) - parseFloat($("#txt_TotalDebit").val()));
                    }
                    if ($("#txt_Difference").val() > 0) {
                        $("#txt_Difference").css("color", "red");
                    }

                });
            });

            $("input[name=txt_Debit],input[name=txt_Credit]").blur(function () {
                if ($(this).attr("name") == "txt_Debit" && $(this).val() != "") {
                    $(this).parent().next().find("input").attr("disabled", "disabled");
                }
                else {
                    $(this).parent().next().find("input").removeAttr("disabled");
                }

                if ($(this).attr("name") == "txt_Credit" && $(this).val() != "") {
                    $(this).parent().prev().find("input").attr("disabled", "disabled");
                }
                else {
                    $(this).parent().prev().find("input").removeAttr("disabled");
                }

                if ($("#txt_TotalDebit").val() == "") {
                    $("#txt_TotalDebit").val("0");
                }
                if ($("#txt_TotalCredit").val() == "") {
                    $("#txt_TotalCredit").val("0");
                }

                var sum = 0;
                $("input[name=txt_Debit]").each(function () {
                    if ($(this).val() != "") {
                        sum += parseFloat($(this).val());
                    }
                });
                $("#txt_TotalDebit").val(sum);

                sum = 0;
                $("input[name=txt_Credit]").each(function () {
                    if ($(this).val() != "") {
                        sum += parseFloat($(this).val());
                    }
                });
                $("#txt_TotalCredit").val(sum);

                $("#txt_TotalDebit").css("color", "green");
                $("#txt_TotalCredit").css("color", "green");
                $("#txt_Difference").css("color", "green");
                if ($("#txt_TotalDebit").val() < $("#txt_TotalCredit").val()) {
                    $("#txt_TotalDebit").css("color", "red");
                    $("#txt_TotalCredit").css("color", "green");
                }
                if ($("#txt_TotalCredit").val() < $("#txt_TotalDebit").val()) {
                    $("#txt_TotalDebit").css("color", "green");
                    $("#txt_TotalCredit").css("color", "red");
                }

                $("#txt_Difference").val(parseFloat($("#txt_TotalDebit").val()) - parseFloat($("#txt_TotalCredit").val()));
                if ($("#txt_Difference").val() < 0) {
                    $("#txt_Difference").val(parseFloat($("#txt_TotalCredit").val()) - parseFloat($("#txt_TotalDebit").val()));
                }
                if ($("#txt_Difference").val() > 0) {
                    $("#txt_Difference").css("color", "red");
                }

            });

        });

        function SaveVoucher() {
            var MessageBox = document.getElementById('MessageBox');
            var txt_SelectedMasterCode = document.getElementById("txt_SelectedMasterCode");
            var txt_SelectedDetailCode = document.getElementById("txt_SelectedDetailCode");
            var txt_Date = document.getElementById('txt_Date');
            var ddl_Status = document.getElementById('ddl_Status');
            var txt_Remarks = document.getElementById('txt_Remarks');
            var ddl_VoucherType = document.getElementById('ddl_VoucherType');
            var txt_Difference = document.getElementById('txt_Difference');
            var ddl_Location = document.getElementById('ddl_Location');

            if (txt_Date.value == "") {
                FadeIn(MessageBox);
                MessageBox.innerHTML = "<h5>Error!</h5><p>Please select date.</p>";
                MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(MessageBox);
                txt_Date.focus();
                return;
            }
            else if (ddl_Status.value == "0") {
                FadeIn(MessageBox);
                MessageBox.innerHTML = "<h5>Error!</h5><p>Please select status.</p>";
                MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(MessageBox);
                ddl_Status.focus();
                return;
            }
            else if (ddl_VoucherType.value == "0") {
                FadeIn(MessageBox);
                MessageBox.innerHTML = "<h5>Error!</h5><p>Please select voucher type.</p>";
                MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(MessageBox);
                ddl_VoucherType.focus();
                return;
            }
            else if (ddl_Status.value == "2" && (txt_Difference.value == "" || txt_Difference.value != "0")) {
                FadeIn(MessageBox);
                MessageBox.innerHTML = "<h5>Error!</h5><p>Debit and Credit values should be equal.</p>";
                MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(MessageBox);
                ddl_Status.focus();
                return;
            }
            else {
                var VoucherDetailRows = [];
                var index = 0;

                $(".detailRow").each(function () {
                    var ddl_Account = $(this).find("[name=ddl_Account]");
                    var txt_Debit = $(this).find("input[name=txt_Debit]");
                    var txt_Credit = $(this).find("input[name=txt_Credit]");
                    var txt_Details = $(this).find("input[name=txt_Details]");

                    if (ddl_Account != null) {
                        VoucherDetailRows[index] = ddl_Account.val() + "║";
                    }
                    else {
                        VoucherDetailRows[index] = "NULL" + "║";
                    }

                    if (txt_Debit != null) {
                        VoucherDetailRows[index] += txt_Debit.val() + "║";
                    }
                    else {
                        VoucherDetailRows[index] += "NULL" + "║";
                    }

                    if (txt_Credit != null) {
                        VoucherDetailRows[index] += txt_Credit.val() + "║";
                    }
                    else {
                        VoucherDetailRows[index] += "NULL" + "║";
                    }

                    if (txt_Details != null) {
                        VoucherDetailRows[index] += txt_Details.val();
                    }
                    else {
                        VoucherDetailRows[index] += "NULL";
                    }
                    index++;
                });

                VoucherDetailRows[$(".detailRow").length] = txt_SelectedMasterCode.value + "║" + txt_Date.value + "║" + ddl_Status.value + "║" + ddl_VoucherType.value + "║" + ddl_Location.value + "║" + txt_Remarks.value;
                document.getElementById("Waiting_Image").style.display = "block";
                document.getElementById("btn_Save").style.display = "none";
                $.ajax({
                    type: "POST",
                    url: "../Voucher/SaveVoucher",
                    data: JSON.stringify(VoucherDetailRows),
                    dataType: 'json',
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        FadeIn(MessageBox);
                        var Arr = response.toString().split(",");
                        if (Arr[0] != null) {
                            document.getElementById("txt_SelectedMasterCode").value = Arr[0];
                        }
                        if (Arr[1] != null) {
                            document.getElementById("txt_Code").value = Arr[1];
                        }
                        else {
                            document.getElementById("txt_Code").value = "[Auto]";
                        }
                        if (Arr[2] != null && Arr[2] != "0") {
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
                    }
                });
            }
        }

        function ResetForm() {
            window.location = "../Voucher/VoucherEntry";
        }

    </script>
    <form id="frm_VoucherEntry" action='<%=Url.Content("~/") %>'>
    <input type="hidden" id="txt_SelectedMasterCode" name="txt_SelectedMasterCode" value='<%=ViewData["VoucherId"] %>' />
    <input type="hidden" id="txt_SelectedDetailCode" name="txt_SelectedDetailCode" value="" />
    <input type="hidden" id="NarrationTitles" name="NarrationTitles" value='<%=ViewData["Narrations"] %>' />
    <input type="hidden" id="AccountCodesList" name="AccountCodesList" value="<%=ViewData["ChartOfAccountCodesWithTitles"] %>" />
    <div class="box round first fullpage grid">
        <h2>
            Voucher Entry</h2>
        <div class="block">
            <div id="MessageBox">
            </div>
            <%if (ViewData["Code"] != null && ViewData["Code"] != "")
              { %>
            <div class="CustomCell" style="width: 85px; height: 30px;">
                Last Voucher</div>
            <div class="CustomCell" style="width: 600px; height: 30px; font-family: Tahoma;">
                <b>Voucher # :</b>
                <%=ViewData["Code"]%>
                <b>, Date : </b>
                <%=ViewData["Date"]%>
                <b>, Status : </b>
                <%=ViewData["Status"]%>
            </div>
            <div class="Clear" style="border-bottom: 1px solid #ccc; margin-bottom: 5px;">
            </div>
            <%} %>
            <div class="CustomCell" style="width: 97px; height: 30px;">
                Code</div>
            <div class="CustomCell" style="width: 320px; height: 30px;">
                <input type="text" class="CustomText" style="width: 250px; font-weight: bold;" id="txt_Code"
                    name="txt_Code" maxlength="15" readonly="readonly" value='<%=ViewData["VoucherCode"] %>' />
            </div>
            <div class="CustomCell" style="width: 40px; height: 30px;">
                Date</div>
            <div class="CustomCell" style="width: 282px; height: 30px;">
                <input type="text" class="CustomText" style="width: 220px;" id="txt_Date" name="txt_Date"
                    value="<%=ViewData["CurrentDate"]%>" maxlength="180" />
            </div>
        </div>
        <div class="CustomCell" style="width: 50px; height: 30px;">
            Status</div>
        <div class="CustomCell" style="width: 320px; height: 30px;">
            <select id="ddl_Status" name="ddl_Status" class="CustomText" style="width: 251px;">
                <option value="Pending">Pending </option>
                <option value="Approved">Approved </option>
            </select>
        </div>
        <div class="Clear">
        </div>
        <div class="CustomCell" style="width: 97px; height: 30px;">
            Voucher Type</div>
        <div class="CustomCell" style="width: 270px; height: 30px;">
            <%= Html.DropDownList("ddl_VoucherType", null, new { style = "width:955px;"})%>
        </div>
        <div class="Clear">
        </div>
        <div class="CustomCell" style="width: 97px; height: 30px;">
            Location</div>
        <div class="CustomCell" style="width: 270px; height: 30px;">
            <%= Html.DropDownList("ddl_Location", null, new { style = "width:955px;"})%>
        </div>
        <div class="Clear">
        </div>
        <div class="CustomCell" style="width: 97px; height: 30px;">
            Remarks</div>
        <div class="CustomCell" style="width: 960px; height: 30px;">
            <input type="text" value="<%=ViewData["Remarks"] %>" class="CustomText" style="width: 940px;"
                id="txt_Remarks" name="txt_Remarks" maxlength="180" />
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
            <%if (ViewData["Edit"] != null && ViewData["Edit"] != "")
              {
                  List<SCMSDataLayer.DB.GL_VchrDetail> voucherDetails = new List<SCMSDataLayer.DB.GL_VchrDetail>();
                  if (ViewData["DetailRecords"] != null)
                  {
                      voucherDetails = (List<SCMSDataLayer.DB.GL_VchrDetail>)ViewData["DetailRecords"];
                  }
                  if (voucherDetails != null && voucherDetails.Count > 0)
                  {
                      foreach (SCMSDataLayer.DB.GL_VchrDetail row in voucherDetails)
                      {
            %>
            <div class="detailRow" style="float: left; width: auto;">
                <div class="CustomCell" style="width: 250px; height: 30px;">
                    <%ViewData["ddl_Account"] = new SelectList((List<SCMSDataLayer.DB.SETUP_ChartOfAccount>)ViewData["ChartOfAccounts"], "ChrtAcc_Id", "ChrtAcc_Title", row.ChrtAcc_Id);%>
                    <%= Html.DropDownList("ddl_Account", null, new { style = "width:250px;" })%>
                </div>
                <div class="CustomCell" style="width: 565px; height: 30px;">
                    <input type="text" class="CustomText" style="width: 545px;" name="txt_Details" value='<%=row.VchDet_Remarks %>'
                        maxlength="200" />
                </div>
                <div class="CustomCell" style="width: 118px; height: 30px;">
                    <%if (row.VchMas_CrAmount > 0)
                      {
                    %>
                    <input type="text" class="CustomText" style="width: 100px;" name="txt_Debit" disabled="disabled"
                        maxlength="50" />
                    <%}
                      else
                      {%>
                    <input type="text" class="CustomText" style="width: 100px;" name="txt_Debit" value="<%=row.VchMas_DrAmount %>"
                        maxlength="50" />
                    <%} %>
                </div>
                <div class="CustomCell" style="width: 118px; height: 30px;">
                    <%if (row.VchMas_DrAmount > 0)
                      { %>
                    <input type="text" class="CustomText" style="width: 100px;" name="txt_Credit" disabled="disabled"
                        maxlength="50" />
                    <%}
                      else
                      { %>
                    <input type="text" class="CustomText" style="width: 100px;" name="txt_Credit" value="<%=row.VchMas_CrAmount %>"
                        maxlength="50" />
                    <%} %>
                </div>
            </div>
            <%}
                  }
            %>
            <script type="text/javascript">
                $(document).ready(function () {
                    if ($("#txt_TotalDebit").val() == "") {
                        $("#txt_TotalDebit").val("0");
                    }
                    if ($("#txt_TotalCredit").val() == "") {
                        $("#txt_TotalCredit").val("0");
                    }

                    var sum = 0;
                    $("input[name=txt_Debit]").each(function () {
                        if ($(this).val() != "") {
                            sum += parseFloat($(this).val());
                        }
                    });
                    $("#txt_TotalDebit").val(sum);

                    sum = 0;
                    $("input[name=txt_Credit]").each(function () {
                        if ($(this).val() != "") {
                            sum += parseFloat($(this).val());
                        }
                    });
                    $("#txt_TotalCredit").val(sum);

                    $("#txt_TotalDebit").css("color", "green");
                    $("#txt_TotalCredit").css("color", "green");
                    $("#txt_Difference").css("color", "green");
                    if ($("#txt_TotalDebit").val() < $("#txt_TotalCredit").val()) {
                        $("#txt_TotalDebit").css("color", "red");
                        $("#txt_TotalCredit").css("color", "green");
                    }
                    if ($("#txt_TotalCredit").val() < $("#txt_TotalDebit").val()) {
                        $("#txt_TotalDebit").css("color", "green");
                        $("#txt_TotalCredit").css("color", "red");
                    }

                    $("#txt_Difference").val(parseFloat($("#txt_TotalDebit").val()) - parseFloat($("#txt_TotalCredit").val()));
                    if ($("#txt_Difference").val() < 0) {
                        $("#txt_Difference").val(parseFloat($("#txt_TotalCredit").val()) - parseFloat($("#txt_TotalDebit").val()));
                    }
                    if ($("#txt_Difference").val() > 0) {
                        $("#txt_Difference").css("color", "red");
                    }
                });

            </script>
            <%}
              else
              {%>
            <div class="detailRow" style="float: left; width: auto;">
                <div class="CustomCell" style="width: 250px; height: 30px;">
                    <%= Html.DropDownList("ddl_Account", null, new { style = "width:250px;" })%>
                </div>
                <div class="CustomCell" style="width: 565px; height: 30px;">
                    <input type="text" class="CustomText" style="width: 545px;" name="txt_Details" maxlength="200" />
                </div>
                <div class="CustomCell" style="width: 118px; height: 30px;">
                    <input type="text" class="CustomText" style="width: 100px;" name="txt_Debit" maxlength="50" />
                </div>
                <div class="CustomCell" style="width: 118px; height: 30px;">
                    <input type="text" class="CustomText" style="width: 100px;" name="txt_Credit" maxlength="50" />
                </div>
            </div>
            <div class="detailRow" style="float: left; width: auto;">
                <div class="CustomCell" style="width: 250px; height: 30px;">
                    <%= Html.DropDownList("ddl_Account", null, new { style = "width:250px;" })%>
                </div>
                <div class="CustomCell" style="width: 565px; height: 30px;">
                    <input type="text" class="CustomText" style="width: 545px;" name="txt_Details" maxlength="200" />
                </div>
                <div class="CustomCell" style="width: 118px; height: 30px;">
                    <input type="text" class="CustomText" style="width: 100px;" name="txt_Debit" maxlength="50" />
                </div>
                <div class="CustomCell" style="width: 118px; height: 30px;">
                    <input type="text" class="CustomText" style="width: 100px;" name="txt_Credit" maxlength="50" />
                </div>
            </div>
            <div class="detailRow" style="float: left; width: auto;">
                <div class="CustomCell" style="width: 250px; height: 30px;">
                    <%= Html.DropDownList("ddl_Account", null, new { style = "width:250px;" })%>
                </div>
                <div class="CustomCell" style="width: 565px; height: 30px;">
                    <input type="text" class="CustomText" style="width: 545px;" name="txt_Details" maxlength="200" />
                </div>
                <div class="CustomCell" style="width: 118px; height: 30px;">
                    <input type="text" class="CustomText" style="width: 100px;" name="txt_Debit" maxlength="50" />
                </div>
                <div class="CustomCell" style="width: 118px; height: 30px;">
                    <input type="text" class="CustomText" style="width: 100px;" name="txt_Credit" maxlength="50" />
                </div>
            </div>
            <div class="detailRow" style="float: left; width: auto;">
                <div class="CustomCell" style="width: 250px; height: 30px;">
                    <%= Html.DropDownList("ddl_Account", null, new { style = "width:250px;" })%>
                </div>
                <div class="CustomCell" style="width: 565px; height: 30px;">
                    <input type="text" class="CustomText" style="width: 545px;" name="txt_Details" maxlength="200" />
                </div>
                <div class="CustomCell" style="width: 118px; height: 30px;">
                    <input type="text" class="CustomText" style="width: 100px;" name="txt_Debit" maxlength="50" />
                </div>
                <div class="CustomCell" style="width: 118px; height: 30px;">
                    <input type="text" class="CustomText" style="width: 100px;" name="txt_Credit" maxlength="50" />
                </div>
            </div>
            <%} %>
            <div style="float: left;">
                <img id="btn_AddNewRow" alt="Add New" src="../../img/add.png" style="width: 30px;
                    cursor: pointer;" />
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
                    <input type="button" value="Cancel" class="btn btn-grey" onclick="ResetForm();" style="width: 90px;
                        height: 35px; padding-top: 5px; color: White; font-weight: bold; font-size: 11pt;" />
                </div>
            </div>
        </div>
    </div>
    </form>
</asp:Content>
