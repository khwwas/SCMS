﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SCMSMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Budget Type
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            document.getElementById("txt_Prefix").focus();
        });

        function Save() {
            var MessageBox = document.getElementById('MessageBox');

            var txt_SelectedCode = document.getElementById("txt_SelectedCode");
            var txt_Prefix = document.getElementById('txt_Prefix');
            var txt_Title = document.getElementById('txt_Title');

            if (txt_Title.value == "") {
                FadeIn(MessageBox);
                MessageBox.innerHTML = "<h5>Error!</h5><p>Please enter title.</p>";
                MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(MessageBox);
                txt_Title.focus();
                return;
            }
            else {
                //var Url = document.getElementById('frm_BudgetType').action;
                //Url += "BudgetType/Save?Code=" + txt_SelectedCode.value + "&Prefix=" + txt_Prefix.value + "&Title=" + txt_Title.value + "&CodeInitilization=" + CodeInitilization; //+ "&LocId=" + ddl_Locations.value;
                document.getElementById("Waiting_Image").style.display = "block";
                document.getElementById("btn_Save").style.display = "none";
                $.ajax({
                    type: "POST",
                    url: "BudgetType/Save",
                    data: { Code: txt_SelectedCode.value, Prefix: txt_Prefix.value, Title: txt_Title.value },
                    success: function (response) {
                        html = response;
                        $("#GridContainer").html(response);
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
            document.getElementById('txt_Prefix').value = "";
            document.getElementById('txt_Title').value = "";
        }

        function EditRecord(Id) {
            document.getElementById('txt_SelectedCode').value = Id;
            document.getElementById('txt_Code').value = Id;
            document.getElementById('txt_Title').value = document.getElementById('txt_Title' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            document.getElementById('txt_Prefix').value = document.getElementById('txt_Prefix' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            ShowHideSaveButton();
            scroll(0, 0);
        }

        function DeleteRecord(Id) {
            if (confirm("Do you really want to delete this record")) {
                var MessageBox = document.getElementById('MessageBox');
                var Url = document.getElementById('frm_BudgetType').action;
                Url += "BudgetType/Delete?Id=" + Id;
                $.ajax({
                    type: "GET",
                    url: Url,
                    success: function (response) {
                        html = response;
                        $("#GridContainer").html(response);
                        ResetForm();
                        FadeIn(MessageBox);
                        if (document.getElementById("SaveResult").value == "0") {
                            lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Unable to delete record.</p>";
                            lcnt_MessageBox.setAttribute("class", "message error");

                        } else {
                            lcnt_MessageBox.innerHTML = "<h5>Success!</h5><p>Record deleted successfully.</p>";
                            lcnt_MessageBox.setAttribute("class", "message success");
                        }
                        SetGrid();
                        scroll(0, 0);
                        FadeOut(MessageBox);
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
    <form id="frm_BudgetType" action='<%=Url.Content("~/") %>'>
    <input type="hidden" id="txt_SelectedCode" name="txt_SelectedCode" value="" />
    <div class="box round first fullpage grid">
        <h2>
            Budget Type</h2>
        <div class="block">
            <div id="MessageBox">
            </div>
            <div class="CustomCell" style="width: 150px; height: 30px">
                Code</div>
            <input type="text" class="CustomText" style="width: 100px; font-weight: bold;" id="txt_Code" name="txt_Code"
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
            <div style="float: right; margin-bottom: 10px;">
                <div style="float: left; margin-right: 5px;">
                    <input id="btn_Save" type="button" value="Save" class="btn btn-blue" onclick="Save();"
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
