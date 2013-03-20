<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SCMSMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Voucher Type
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            document.getElementById("txt_Prefix").focus();
        });

        function SaveVoucherType() {
            var MessageBox = document.getElementById('MessageBox');

            var txt_SelectedCode = document.getElementById("txt_SelectedCode");
            var txt_Prefix = document.getElementById('txt_Prefix');
            var txt_Title = document.getElementById('txt_Title');
            var rdo_CodeInitialization = document.getElementsByName("rdo_CodeInitialization");
            //  var ddl_Locations = document.getElementById('ddl_Locations');
            var CodeInitilization = 2;
            if (rdo_CodeInitialization[0].checked) {
                CodeInitilization = 1;
            }

            if (txt_Prefix.value == "") {
                FadeIn(MessageBox);
                MessageBox.innerHTML = "<h5>Error!</h5><p>Please enter prefix.</p>";
                MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(MessageBox);
                txt_Prefix.focus();
                return;
            }
            else if (txt_Title.value == "") {
                FadeIn(MessageBox);
                MessageBox.innerHTML = "<h5>Error!</h5><p>Please enter title.</p>";
                MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(MessageBox);
                txt_Title.focus();
                return;
            }
            else {
                var Url = document.getElementById('frm_VoucherType').action;
                Url += "VoucherType/SaveVoucherType?Code=" + txt_SelectedCode.value + "&Prefix=" + txt_Prefix.value + "&Title=" + txt_Title.value + "&CodeInitilization=" + CodeInitilization; //+ "&LocId=" + ddl_Locations.value;
                document.getElementById("Waiting_Image").style.display = "block";
                document.getElementById("btn_Save").style.display = "none";
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
                        document.getElementById("btn_Save").style.display = "block";
                        scroll(0, 0);
                        FadeOut(MessageBox);
                    },
                    error: function (rs, e) {
                        document.getElementById("Waiting_Image").style.display = "none";
                        document.getElementById("btn_Save").style.display = "block";
                    }
                });
            }
        }


        function ResetForm() {
            var MessageBox = document.getElementById('MessageBox');
            MessageBox.removeAttribute("class");
            MessageBox.innerHTML = "";

            document.getElementById('txt_SelectedCode').value = "";
            document.getElementById('txt_Code').value = "[Auto]";
            document.getElementById('txt_Prefix').value = "";
            document.getElementById('txt_Title').value = "";
            document.getElementsByName("rdo_CodeInitialization")[0].checked = "checked";
            // document.getElementById('ddl_Locations').value = "";

        }

        function EditRecord(Id) {
            document.getElementById('txt_SelectedCode').value = Id;
            document.getElementById('txt_Code').value = Id;
            document.getElementById('txt_Title').value = document.getElementById('txt_Title' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            document.getElementById('txt_Prefix').value = document.getElementById('txt_Prefix' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            //document.getElementById('ddl_Locations').value = document.getElementById('ddl_Locations' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            if (document.getElementById('txt_CodeInitialization' + Id).innerHTML.trim().toString().replace("&nbsp", "") == "Month") {
                document.getElementsByName("rdo_CodeInitialization")[1].checked = "checked";
            }
            else {
                document.getElementsByName("rdo_CodeInitialization")[0].checked = "checked";
            }
            scroll(0, 0);
        }

        function DeleteRecord(Id) {
            if (confirm("Do you really want to delete this record")) {
                var MessageBox = document.getElementById('MessageBox');
                var Url = document.getElementById('frm_VoucherType').action;
                Url += "VoucherType/DeleteVoucherType?VoucherTypeId=" + Id;
                $.ajax({
                    type: "GET",
                    url: Url,
                    success: function (response) {
                        html = response;
                        $("#GridContainer").html(response);
                        SetGrid();
                        ResetForm();
                        FadeIn(MessageBox);
                        MessageBox.innerHTML = "<h5>Success!</h5><p>Record deleted successfully.</p>";
                        MessageBox.setAttribute("class", "message success");
                        scroll(0, 0);
                        FadeOut(MessageBox);
                    },
                    error: function (rs, e) {
                        FadeIn(MessageBox);
                        MessageBox.innerHTML = "<h5>Error!</h5><p>An error occured in deleting this record.</p>";
                        MessageBox.setAttribute("class", "message error");
                        scroll(0, 0);
                        FadeOut(MessageBox);
                    }
                });
            }
        }

    </script>
    <form id="frm_VoucherType" action='<%=Url.Content("~/") %>'>
    <input type="hidden" id="txt_SelectedCode" name="txt_SelectedCode" value="" />
    <div class="box round first fullpage grid">
        <h2>
            Voucher Type</h2>
        <div class="block">
            <div id="MessageBox">
            </div>
            <div class="CustomCell" style="width: 150px; height: 30px">
                Code</div>
            <input type="text" class="CustomText" style="width: 100px;" id="txt_Code" name="txt_Code"
                maxlength="100" value="[Auto]" readonly="readonly" />
            <div class="Clear">
            </div>
            <div class="CustomCell" style="width: 150px; height: 30px">
                Prefix</div>
            <input type="text" class="CustomText" style="width: 100px;" id="txt_Prefix" name="txt_Prefix"
                maxlength="3" />
            <div class="Clear">
            </div>
            <div class="CustomCell" style="width: 150px; height: 30px">
                Title</div>
            <input type="text" class="CustomText" style="width: 940px;" id="txt_Title" name="txt_Title"
                maxlength="100" />
            <div class="Clear">
            </div>
            <div class="CustomCell" style="width: 150px; height: 30px">
                Code Initialization on
            </div>
            <div class="CustomCell" style="width: 12px; height: 30px">
                <input type="radio" id="rdo_Year" name="rdo_CodeInitialization" checked="checked"
                    class="CustomText" value="Year End" style="margin: 0px;" />
            </div>
            <div class="CustomCell" style="width: 55px; height: 30px;">
                Year End
            </div>
            <div class="CustomCell" style="width: 12px; height: 30px">
                <input type="radio" id="rdo_Month" name="rdo_CodeInitialization" class="CustomText"
                    value="Month End" style="margin: 0px;" />
            </div>
            <div class="CustomCell" style="width: 85px; height: 30px">
                Month End
            </div>
            <div class="Clear">
            </div>
            <%--<div class="CustomCell" style="width: 100px; height: 30px;">
                Location</div>
            <%= Html.DropDownList("ddl_Locations", null, new { style = "width:955px;" })%>
            <div class="Clear">
            </div>--%>
            <div style="float: right; margin-bottom: 10px;">
                <div style="float: left; margin-right: 5px;">
                    <input id="btn_Save" type="button" value="Save" class="btn btn-blue" onclick="SaveVoucherType();"
                        style="width: 90px; height: 35px; padding-top: 5px; color: White; font-weight: bold;
                        font-size: 11pt;" />
                    <img alt="" id="Waiting_Image" src="../../img/Ajax_Loading.gif" style="display: none;
                        margin-left: 10" /></div>
                <div style="float: left;">
                    <input type="button" value="Reset" class="btn btn-grey" onclick="ResetForm();" style="width: 90px;
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
