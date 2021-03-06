﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SCMSMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SCMS - User Setup
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        function SaveRecord() {
            var lcnt_MessageBox = document.getElementById('MessageBox');
            var lcnt_txtSelectedCode = document.getElementById("txt_SelectedCode");
            var lcnt_txtTitle = document.getElementById('txt_Title');
            var lcnt_txtLogin = document.getElementById('txt_Login');
            var lcnt_txtPassword = document.getElementById('txt_Password');
            var lcnt_UserGroup = document.getElementById('ddl_UserGroup');
            
            if (lcnt_txtTitle.value == "") {
                FadeIn(lcnt_MessageBox);
                lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Please! enter title</p>";
                lcnt_MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(lcnt_MessageBox);
                lcnt_txtTitle.focus();
                return;
            }
            else if (lcnt_txtLogin.value == "") {
                FadeIn(lcnt_MessageBox);
                lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Please! enter login</p>";
                lcnt_MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(lcnt_MessageBox);
                lcnt_txtTitle.focus();
                return;
            }
            else {
                //var Url = document.getElementById('frm_UserSetup').action;
                //Url += "User/SaveRecord?ps_Code=" + lcnt_txtSelectedCode.value + "&ps_Title=" + lcnt_txtTitle.value + "&ps_GroupId=" + lcnt_UserGroup.value + "&ps_Password=" + lcnt_txtPassword.value;
                document.getElementById("Waiting_Image").style.display = "block";
                document.getElementById("btn_Save").style.display = "none";
                $.ajax({
                    type: "POST",
                    url: "User/SaveRecord",
                    data: { ps_Code: lcnt_txtSelectedCode.value, ps_Title: txt_Title.value, ps_GroupId: lcnt_UserGroup.value, ps_Login: lcnt_txtLogin.value, ps_Password: lcnt_txtPassword.value },
                    success: function (response) {
                        html = response;
                        $("#GridContainer").html(response);
                        SetGrid();
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
                        scroll(0, 0);
                        FadeOut(lcnt_MessageBox);
                    },
                    error: function (rs, e) {
                        document.getElementById("Waiting_Image").style.display = "none";
                        document.getElementById("btn_Save").style.display = "block";
                    }
                });
            }
        }

        function ResetForm() {
            var lcnt_MessageBox = document.getElementById('MessageBox');

            lcnt_MessageBox.removeAttribute("class");
            lcnt_MessageBox.innerHTML = "";

            document.getElementById('txt_SelectedCode').value = "";
            document.getElementById('txt_Code').value = "[Auto]";
            document.getElementById('txt_Title').value = "";
            document.getElementById('txt_Login').value = "";
            document.getElementById('txt_Password').value = "";
        }

        function EditRecord(Id) {
            document.getElementById('txt_SelectedCode').value = Id;
            document.getElementById('txt_Code').value = Id;
            document.getElementById('txt_Title').value = document.getElementById('txt_Title' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            document.getElementById('txt_Login').value = document.getElementById('txt_Login' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            document.getElementById('txt_Password').value = document.getElementById('txt_Password' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            document.getElementById('ddl_UserGroup').value = document.getElementById('ddl_UserGroup' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            scroll(0, 0);
        }

        function DeleteRecord(Id) {
            if (confirm("Do you really want to delete this record")) {

                var lcnt_MessageBox = document.getElementById('MessageBox');
                var Url = document.getElementById('frm_UserSetup').action;

                Url += "User/DeleteRecord?_pId=" + Id;
                $.ajax({
                    type: "GET",
                    url: Url,
                    success: function (response) {
                        html = response;
                        $("#GridContainer").html(response);
                        SetGrid();
                        ResetForm();
                        FadeIn(lcnt_MessageBox);
                        if (document.getElementById("SaveResult").value == "0") {
                            lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Unable to delete record.</p>";
                            lcnt_MessageBox.setAttribute("class", "message error");

                        } else {
                            lcnt_MessageBox.innerHTML = "<h5>Success!</h5><p>Record deleted successfully.</p>";
                            lcnt_MessageBox.setAttribute("class", "message success");
                        }
                        scroll(0, 0);
                        FadeOut(lcnt_MessageBox);
                    },
                    error: function (rs, e) {
                        FadeIn(lcnt_MessageBox);
                        lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>An error occured in deleting this record.</p>";
                        lcnt_MessageBox.setAttribute("class", "message error");
                        scroll(0, 0);
                        FadeOut(lcnt_MessageBox);
                    }
                });
            }
        }

    </script>
    <form id="frm_UserSetup" action='<%=Url.Content("~/") %>'>
    <input type="hidden" id="txt_SelectedCode" name="txt_SelectedCode" value="" />
    <div class="box round first fullpage grid">
        <h2>
            User Setup</h2>
        <div class="block">
            <div id="MessageBox">
            </div>
            <div class="CustomCell" style="width: 100px; height: 30px">
                Code</div>
            <input type="text" class="CustomText" style="width: 100px; font-weight: bold;" id="txt_Code" name="txt_Code"
                maxlength="100" value="[Auto]" readonly="readonly" />
            <div class="Clear">
            </div>
            <div class="CustomCell" style="width: 100px; height: 30px">
                Title</div>
            <input type="text" class="CustomText" style="width: 940px;" id="txt_Title" name="txt_Title"
                maxlength="100" />
            <div class="Clear">
            </div>
            <div class="CustomCell" style="width: 100px; height: 30px">
                Login</div>
            <input type="text" class="CustomText" style="width: 940px;" id="txt_Login" name="txt_Login"
                maxlength="50" />
            <div class="Clear">
            </div>
            <div class="CustomCell" style="width: 100px; height: 30px">
                Password</div>
            <input type="password" class="CustomText" style="width: 940px;" id="txt_Password"
                name="txt_Password" maxlength="100" />
            <div class="Clear">
            </div>
            <div class="CustomCell" style="width: 100px; height: 30px">
                User Group</div>
            <%= Html.DropDownList("ddl_UserGroup", null, new { style = "width:955px;" })%>
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
