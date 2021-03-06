﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SCMSMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Company Setup
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            document.getElementById("txt_Name").focus();
        });

        function SaveCompany() {
            var MessageBox = document.getElementById('MessageBox');
            var txt_SelectedCode = document.getElementById("txt_SelectedCode");
            var txt_CompanyName = document.getElementById('txt_Name');
            var txt_Address1 = document.getElementById('txt_Address1');
            var txt_Address2 = document.getElementById('txt_Address2');
            var txt_Email = document.getElementById('txt_Email');
            var txt_Phone = document.getElementById('txt_Phone');
            var txt_Fax = document.getElementById('txt_Fax');

            if (txt_CompanyName.value == "") {
                FadeIn(MessageBox);
                MessageBox.innerHTML = "<h5>Error!</h5><p>Please enter company name.</p>";
                MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(MessageBox);
                txt_CompanyName.focus();
                return;
            }
            else {
                //var Url = document.getElementById('frm_CompanySetup').action;
                //Url += "Company/SaveCompany?Code=" + txt_SelectedCode.value + "&Name=" + txt_CompanyName.value + "&Address1=" + txt_Address1.value + "&Address2=" + txt_Address2.value + "&Email=" + txt_Email.value + "&Phone=" + txt_Phone.value + "&Fax=" + txt_Fax.value;
                document.getElementById("Waiting_Image").style.display = "block";
                document.getElementById("btn_Save").style.display = "none";
                $.ajax({
                    type: "POST",
                    url: "Company/SaveCompany",
                    data: { Code: txt_SelectedCode.value, Name: txt_CompanyName.value, Address1: txt_Address1.value, Address2: txt_Address2.value, Email: txt_Email.value, Phone: txt_Phone.value, Fax: txt_Fax.value },
                    success: function (response) {
                        //alert(response);
                        html = response;
                        $("#GridContainer").html(response);
                        ResetForm();
                        FadeIn(MessageBox);
                        if (document.getElementById("SaveResult").value == "0") {
                            MessageBox.innerHTML = "<h5>Error!</h5><p>" + GetMessage("SaveError") + "</p>";
                            MessageBox.setAttribute("class", "message error");

                        } else {
                            MessageBox.innerHTML = "<h5>Success!</h5><p>" + GetMessage("SaveSuccess") + "</p>";
                            MessageBox.setAttribute("class", "message success");
                        }
                        document.getElementById("Waiting_Image").style.display = "none";
                        document.getElementById("btn_Save").style.display = "block";
                        SetGrid();
                        scroll(0, 0);
                        FadeOut(MessageBox);
                    },
                    error: function (rs, e) {
                        document.getElementById("Waiting_Image").style.display = "none";
                        document.getElementById("btn_Save").style.display = "block";
                        SetUserRights();
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
            var txt_CompanyName = document.getElementById('txt_Name').value = "";
            var txt_Address1 = document.getElementById('txt_Address1').value = "";
            var txt_Address2 = document.getElementById('txt_Address2').value = "";
            var txt_Email = document.getElementById('txt_Email').value = "";
            var txt_Phone = document.getElementById('txt_Phone').value = "";
            var txt_Fax = document.getElementById('txt_Fax').value = "";
        }

        function EditRecord(Id) {
            document.getElementById('txt_SelectedCode').value = Id;
            document.getElementById('txt_Code').value = Id;
            document.getElementById('txt_Name').value = document.getElementById('txt_Title' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            document.getElementById('txt_Address1').value = document.getElementById('txt_Address1' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            document.getElementById('txt_Address2').value = document.getElementById('txt_Address2' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            document.getElementById('txt_Email').value = document.getElementById('txt_Email' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            document.getElementById('txt_Phone').value = document.getElementById('txt_Phone' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            document.getElementById('txt_Fax').value = document.getElementById('txt_Fax' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            ShowHideSaveButton();
            scroll(0, 0);
        }

        function DeleteRecord(Id) {
            if (confirm("Do you really want to delete this record")) {
                var lcnt_MessageBox = document.getElementById('MessageBox');
                var Url = document.getElementById('frm_CompanySetup').action;
                Url += "Company/DeleteCompany?companyId=" + Id;
                $.ajax({
                    type: "GET",
                    url: Url,
                    success: function (response) {
                        html = response;
                        $("#GridContainer").html(response);
                        ResetForm();
                        FadeIn(lcnt_MessageBox);
                        if (document.getElementById("SaveResult").value == "0") {
                            lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Unable to delete record.</p>";
                            lcnt_MessageBox.setAttribute("class", "message error");

                        } else {
                            lcnt_MessageBox.innerHTML = "<h5>Success!</h5><p>Record deleted successfully.</p>";
                            lcnt_MessageBox.setAttribute("class", "message success");
                        }
                        SetGrid();
                        scroll(0, 0);
                        FadeOut(lcnt_MessageBox);
                    },
                    error: function (rs, e) {
                        FadeIn(MessageBox);
                        MessageBox.innerHTML = "<h5>Error!</h5><p>An error occured in deleting this record.</p>";
                        MessageBox.setAttribute("class", "message error");
                        SetUserRights();
                        scroll(0, 0);
                        FadeOut(MessageBox);
                    }
                });
            }
        }

    </script>
    <form id="frm_CompanySetup" action='<%=Url.Content("~/") %>'>
    <input type="hidden" id="txt_SelectedCode" name="txt_SelectedCode" value="" />
    <div class="box round first fullpage grid">
        <h2>
            Company Setup</h2>
        <div class="block">
            <div id="MessageBox">
            </div>
            <div class="CustomCell" style="width: 100px; height: 30px">
                Code</div>
            <input type="text" class="CustomText" style="width: 100px; font-weight: bold;" id="txt_Code"
                name="txt_Code" maxlength="100" value="[Auto]" readonly="readonly" />
            <div class="Clear">
            </div>
            <div class="CustomCell" style="width: 100px; height: 30px">
                Title</div>
            <input type="text" class="CustomText" style="width: 940px;" id="txt_Name" name="txt_Name"
                maxlength="100" />
            <div class="Clear">
            </div>
            <div class="CustomCell" style="width: 100px; height: 30px">
                Address1</div>
            <input type="text" class="CustomText" style="width: 940px;" id="txt_Address1" name="txt_Address1"
                maxlength="100" />
            <div class="Clear">
            </div>
            <div class="CustomCell" style="width: 100px; height: 30px">
                Address2</div>
            <input type="text" class="CustomText" style="width: 940px;" id="txt_Address2" name="txt_Address2"
                maxlength="100" />
            <div class="Clear">
            </div>
            <div class="CustomCell">
                Email</div>
            <div class="CustomCell">
                Phone</div>
            <div class="CustomCell">
                Fax</div>
            <div class="Clear">
            </div>
            <input type="text" class="CustomText" id="txt_Email" name="txt_Email" maxlength="100" />
            <input type="text" class="CustomText" id="txt_Phone" name="txt_Phone" maxlength="100" />
            <input type="text" class="CustomText" id="txt_Fax" name="txt_Fax" maxlength="100" />
            <div class="Clear">
            </div>
            <div style="float: right; margin-bottom: 10px;">
                <div style="float: left; margin-right: 5px;">
                    <input id="btn_Save" type="button" value="Save" class="btn btn-blue" onclick="SaveCompany();"
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
