<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SCMSMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Calendar Type Setup
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {

        });
        function SaveRecord() {

            var lcnt_MessageBox = document.getElementById('MessageBox');
            var lcnt_txtSelectedCode = document.getElementById("txt_SelectedCode");
            var txt_Title = document.getElementById('txt_Title');
            var ddl_Company = document.getElementById('ddl_Company');
            var ddl_location = document.getElementById('ddl_location');
            var ddl_level = document.getElementById('ddl_level');

            //            if (ddl_Company.value == 0) {
            //                FadeIn(lcnt_MessageBox);
            //                lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Please! select company</p>";
            //                lcnt_MessageBox.setAttribute("class", "message error");
            //                scroll(0, 0);
            //                FadeOut(lcnt_MessageBox);
            //                ddl_Company.focus();
            //                return;
            //            } else if (ddl_location.value == 0) {
            //                FadeIn(lcnt_MessageBox);
            //                lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Please! select location</p>";
            //                lcnt_MessageBox.setAttribute("class", "message error");
            //                scroll(0, 0);
            //                FadeOut(lcnt_MessageBox);
            //                ddl_location.focus();
            //                return;
            //            } else if (ddl_level.value == 0) {
            //                FadeIn(lcnt_MessageBox);
            //                lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Please! select level</p>";
            //                lcnt_MessageBox.setAttribute("class", "message error");
            //                scroll(0, 0);
            //                FadeOut(lcnt_MessageBox);
            //                ddl_level.focus();
            //                return;
            //            } else 
            if (txt_Title.value == "") {
                FadeIn(lcnt_MessageBox);
                lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Please! enter title</p>";
                lcnt_MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(lcnt_MessageBox);
                txt_Title.focus();
                return;
            }
            else {
                //var Url = document.getElementById('frm_CalendarTypeSetup').action;
                //Url += "CalendarType/SaveRecord?ps_Code=" + lcnt_txtSelectedCode.value + "&Comapany=" + ddl_Company.value + "&Location=" + ddl_location.value + "&Title=" + txt_Title.value + "&Level=" + ddl_level.value;
                //Url += "CalendarType/SaveRecord?ps_Code=" + lcnt_txtSelectedCode.value + "&Title=" + txt_Title.value ;
                document.getElementById("Waiting_Image").style.display = "block";
                document.getElementById("btn_Save").style.display = "none";
                //alert("Before AJEX ps_Code: "+ lcnt_txtSelectedCode.value + ", Title: "+ txt_Title.value);
                $.ajax({
                    type: "POST",
                    url: "CalendarType/SaveRecord",
                    data: { ps_Code: lcnt_txtSelectedCode.value, Title: txt_Title.value },
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
        }

        function ResetForm() {
            var lcnt_MessageBox = document.getElementById('MessageBox');

            lcnt_MessageBox.removeAttribute("class");
            lcnt_MessageBox.innerHTML = "";

            document.getElementById('txt_SelectedCode').value = "";
            document.getElementById('txt_Code').value = "[Auto]";
            document.getElementById('txt_Title').value = "";
            //document.getElementById('ddl_Company').value = "";
            //document.getElementById('ddl_location').value = "";
            //document.getElementById('ddl_level').value = "";
        }

        function EditRecord(Id) {
            document.getElementById('txt_SelectedCode').value = Id;
            document.getElementById('txt_Code').value = Id;
            document.getElementById('txt_Title').value = document.getElementById('txt_Title' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            //document.getElementById('ddl_Company').value = document.getElementById('ddl_Company' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            //document.getElementById('ddl_location').value = document.getElementById('ddl_location' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            //document.getElementById('ddl_level').value = document.getElementById('txt_level' + Id).value;
            ShowHideSaveButton();
            scroll(0, 0);
        }

        function DeleteRecord(Id) {
            if (confirm("Do you really want to delete this record")) {

                var lcnt_MessageBox = document.getElementById('MessageBox');
                var Url = document.getElementById('frm_CalendarTypeSetup').action;

                Url += "CalendarType/DeleteRecord?_pId=" + Id;
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
                        FadeIn(lcnt_MessageBox);
                        lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>An error occured in deleting this record.</p>";
                        lcnt_MessageBox.setAttribute("class", "message error");
                        SetUserRights();
                        scroll(0, 0);
                        FadeOut(lcnt_MessageBox);
                    }
                });
            }
        }

    </script>
    <form id="frm_CalendarTypeSetup" action='<%=Url.Content("~/") %>'>
    <input type="hidden" id="txt_SelectedCode" name="txt_SelectedCode" value="" />
    <div class="box round first fullpage grid">
        <h2>
            CalendarType Setup</h2>
        <div class="block">
            <div id="MessageBox">
            </div>
            <!--<div class="CustomCell" style="width: 115px; height: 30px">
                Company</div>
            < %= Html.DropDownList("ddl_Company", null, new { style = "width:900px; padding: 4px;" })%>
            <div class="Clear">
            </div>
            <div class="CustomCell" style="width: 115px; height: 30px;">
                Location</div>
            < %= Html.DropDownList("ddl_location", null, new { style = "width:900px; padding: 4px;" })%>
            <div class="Clear">
            </div>
            <div class="CustomCell" style="width: 115px; height: 30px;">
                Level</div>
            <select name="ddl_level" id="ddl_level" style="width: 900px;">
                <option value="">Select Level</option>
                <option value="1">Yearly</option>
                <option value="2">Monthly</option>
                <option value="3">Fortnightly</option>
                <option value="4">Weekly</option>
                <option value="5">Daily</option>
            </select>
            <div class="Clear">
            </div>-->
            <div class="CustomCell" style="width: 115px; height: 30px">
                Code</div>
            <input type="text" class="CustomText" style="width: 100px; font-weight: bold;" id="txt_Code"
                name="txt_Code" maxlength="100" value="[Auto]" readonly="readonly" />
            <div class="Clear">
            </div>
            <div class="CustomCell" style="width: 115px; height: 30px;">
                Title</div>
            <input type="text" class="CustomText" style="width: 888px;" id="txt_Title" name="txt_Title"
                maxlength="100" />
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
