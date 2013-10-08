<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SCMSMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Bank Reconciliation
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            //            document.getElementById("txt_Title").focus();

            $('#txt_Date').Zebra_DatePicker({
                format: 'm/d/Y'
            });
        });

        function SaveRecord() {
            var lcnt_MessageBox = document.getElementById('MessageBox');
            var lcnt_txtSelectedCode = document.getElementById("txt_SelectedCode");
            var lcnt_Reconciled = document.getElementById("chk_Reconcilied");
            var lcnt_ReconDate = document.getElementById("txt_Date");

            document.getElementById("Waiting_Image").style.display = "block";
            document.getElementById("btn_Save").style.display = "none";
            $.ajax({
                type: "POST",
                url: "BankReconciliation/SaveRecord",
                data: { ps_Code: lcnt_txtSelectedCode.value, ps_Reconciled: lcnt_Reconciled.value, ps_ReconciliationDate: lcnt_ReconDate.value },
                success: function (response) {
                    html = response;
                    $("#GridContainer").html(response);
                    ResetForm();
                    FadeIn(lcnt_MessageBox);
                    if (document.getElementById("SaveResult").value == "0") {
                        lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Unable to save record.</p>";
                        lcnt_MessageBox.setAttribute("class", "message error");

                    } else {
                        lcnt_MessageBox.innerHTML = "<h5>Success!</h5><p>Record saved successfully.</p>";
                        lcnt_MessageBox.setAttribute("class", "message success");
                    }
                    document.getElementById("Waiting_Image").style.display = "none";
                    document.getElementById("btn_Save").style.display = "block";
                    SetGrid();
                    scroll(0, 0);
                    FadeOut(lcnt_MessageBox);
                },
                error: function (rs, e) {
                    document.getElementById("Waiting_Image").style.display = "none";
                    document.getElementById("btn_Save").style.display = "block";
                    SetUserRights();
                }
            });

        }

        //        function ResetForm() {
        //            var lcnt_MessageBox = document.getElementById('MessageBox');

        //            lcnt_MessageBox.removeAttribute("class");
        //            lcnt_MessageBox.innerHTML = "";

        //            document.getElementById('txt_SelectedCode').value = "";
        //            document.getElementById('txt_Code').value = "[Auto]";
        //            document.getElementById('txt_Title').value = "";
        //        }

        function ResetForm() {
            window.location = "../BankReconciliation";
        }

        function EditRecord(Id) {
            document.getElementById('txt_SelectedCode').value = Id;
            document.getElementById('txt_Code').value = document.getElementById('txt_Code' + Id).innerHTML.trim().toString();
            document.getElementById('txt_VoucherDate').value = document.getElementById('txt_VoucherDate' + Id).innerHTML.trim().toString();
            if (document.getElementById('txt_Reconciliation' + Id).innerHTML.trim().toString() == "1") {
                document.getElementById('chk_Reconcilied').checked = true;
            }
            else {
                document.getElementById('chk_Reconcilied').checked = false;
            }
            document.getElementById('txt_Date').value = document.getElementById('txt_ReconciliationDate' + Id).innerHTML.trim().toString();
            ShowHideSaveButton();
            scroll(0, 0);
        }

    </script>
    <form id="frm_BankReconciliationSetup" action='<%=Url.Content("~/") %>'>
    <input type="hidden" id="txt_SelectedCode" name="txt_SelectedCode" value="" />
    <div class="box round first fullpage grid">
        <h2>
            Bank Reconciliation</h2>
        <div class="block">
            <div id="MessageBox">
            </div>
            <div class="CustomCell" style="width: 150px; height: 30px">
                Voucher #</div>
            <div class="CustomCell" style="width: 320px; height: 30px;">
                <input type="text" class="CustomText" style="width: 100px; font-weight: bold;" id="txt_Code"
                    name="txt_Code" maxlength="100" value="[Auto]" readonly="readonly" />
            </div>
            <div class="CustomCell" style="width: 150px; height: 30px;">
                Voucher Date</div>
            <div class="CustomCell" style="width: 200px; height: 30px;">
                <input type="text" class="CustomText" style="width: 120px;" id="txt_VoucherDate"
                    name="txt_VoucherDate" value="" readonly="readonly" maxlength="180" />
            </div>
            <div class="Clear">
            </div>
            <div class="CustomCell" style="width: 150px; height: 30px">
                Reconciled</div>
            <div class="CustomCell" style="width: 320px; height: 30px;">
                <input type="checkbox" class="Checkbox" id="chk_Reconcilied" name="chk_Reconcilied" />
            </div>
            <%--<div class="Clear">
            </div>--%>
            <div class="CustomCell" style="width: 150px; height: 30px">
                Reconciliation Date</div>
            <div class="CustomCell" style="width: 200px; height: 30px;">
                <input type="text" class="CustomText" style="width: 120px;" id="txt_Date" name="txt_Date"
                    value="<%=ViewData["CurrentDate"]%>" maxlength="180" />
            </div>
            <div class="Clear">
            </div>
            <div style="float: right; margin-bottom: 10px;">
                <div style="float: left; margin-right: 5px;">
                    <input id="btn_Save" type="button" value="Save" class="btn btn-blue" onclick="SaveRecord();"
                        style="width: 90px; height: 35px; padding-top: 5px; color: White; font-weight: bold;
                        font-size: 11pt;" />
                    <img alt="" id="Waiting_Image" src="../../img/Ajax_Loading.gif" style="display: none;
                        margin-left: 10" /></div>
                <div style="float: left;">
                    <input type="button" value="Cancel" class="btn btn-grey" onclick="ResetForm();" style="width: 90px;
                        height: 35px; padding-top: 5px; color: White; font-weight: bold; font-size: 11pt;" />
                </div>
            </div>
            <hr />
            <div id="GridContainer">
                <%Html.RenderPartial("GridData");%>
            </div>
        </div>
    </div>
    </form>
</asp:Content>
