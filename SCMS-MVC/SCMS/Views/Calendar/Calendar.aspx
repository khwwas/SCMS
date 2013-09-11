<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SCMSMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Calendar Setup
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

        });

        function SaveRecord() {

            var lcnt_MessageBox = document.getElementById('MessageBox');
            var lcnt_txtSelectedCode = document.getElementById("txt_SelectedCode");
            //            var ddl_Company = document.getElementById('ddl_Company');
            //            var ddl_location = document.getElementById('ddl_location');
            var ddl_CalenderType = document.getElementById('ddl_CalenderType');
            var txt_Prefix = document.getElementById('txt_Prefix');
            var txt_Title = document.getElementById('txt_Title');
            var txt_SratrtDate = document.getElementById('txt_SratrtDate');
            var txt_EndDate = document.getElementById('txt_EndDate');

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
            if (ddl_CalenderType.value == 0) {
                FadeIn(lcnt_MessageBox);
                lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Please! select calendar type</p>";
                lcnt_MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(lcnt_MessageBox);
                ddl_CalenderType.focus();
                return;
            } else if (txt_Prefix.value == "") {
                FadeIn(lcnt_MessageBox);
                lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Please! enter prefix</p>";
                lcnt_MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(lcnt_MessageBox);
                txt_Prefix.focus();
                return;
            }
            else if (txt_Title.value == "") {
                FadeIn(lcnt_MessageBox);
                lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Please! enter title</p>";
                lcnt_MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(lcnt_MessageBox);
                txt_Title.focus();
                return;
            }
            else {
                var Url = document.getElementById('frm_CalendarSetup').action;
                Url += "Calendar/SaveRecord?ps_Code=" + lcnt_txtSelectedCode.value + "&CalenderType=" + ddl_CalenderType.value + "&Prefix=" + txt_Prefix.value + "&Title=" + txt_Title.value + "&SratrtDate=" + txt_SratrtDate.value + "&EndDate=" + txt_EndDate.value;
                document.getElementById("Waiting_Image").style.display = "block";
                document.getElementById("btn_Save").style.display = "none";
                $.ajax({
                    type: "GET",
                    url: Url,
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
            document.getElementById('txt_Title').value = "";
            //            document.getElementById('ddl_Company').value = "";
            //            document.getElementById('ddl_location').value = "";
            document.getElementById('ddl_CalenderType').value = "";
            document.getElementById('txt_Prefix').value = "";
            document.getElementById('txt_SratrtDate').value = "";
            document.getElementById('txt_EndDate').value = "";
        }

        function EditRecord(Id) {
            document.getElementById('txt_SelectedCode').value = Id;
            document.getElementById('txt_Title').value = document.getElementById('txt_Title' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            //            document.getElementById('ddl_Company').value = document.getElementById('ddl_Company' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            //            document.getElementById('ddl_location').value = document.getElementById('ddl_location' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            document.getElementById('ddl_CalenderType').value = document.getElementById('ddl_CalenderType' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            document.getElementById('txt_Prefix').value = document.getElementById('txt_Prefix' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            document.getElementById('txt_SratrtDate').value = document.getElementById('txt_SratrtDate' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            document.getElementById('txt_EndDate').value = document.getElementById('txt_EndDate' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            //document.getElementById('ddl_level').value = document.getElementById('txt_level' + Id).value;
            ShowHideSaveButton();
            scroll(0, 0);
        }

        function DeleteRecord(Id) {
            if (confirm("Do you really want to delete this record")) {

                var lcnt_MessageBox = document.getElementById('MessageBox');
                var Url = document.getElementById('frm_CalendarSetup').action;

                Url += "Calendar/DeleteRecord?_pId=" + Id;
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
    <form id="frm_CalendarSetup" action='<%=Url.Content("~/") %>'>
    <input type="hidden" id="txt_SelectedCode" name="txt_SelectedCode" value="" />
    <div class="box round first fullpage grid">
        <h2>
            Calendar Setup</h2>
        <div class="block">
            <div id="MessageBox">
            </div>
            <%--<div class="CustomCell" style="width: 105px; height: 30px">
                Company</div>
            <div style="width: 270px; height: 30px;" class="CustomCell">
                <%= Html.DropDownList("ddl_Company", null, new { style = "width:950px; padding: 4px;" })%>
            </div>
            <div class="Clear">
            </div>
            <div class="CustomCell" style="width: 105px; height: 30px;">
                Location</div>
            <div style="width: 270px; height: 30px;" class="CustomCell">
                <%= Html.DropDownList("ddl_location", null, new { style = "width:950px; padding: 4px;" })%>
            </div>
            <div class="Clear">
            </div>--%>
            <div class="CustomCell" style="width: 115px; height: 30px;">
                Title</div>
            <input type="text" class="CustomText" style="width: 940px;" id="txt_Title" name="txt_Title"
                maxlength="100" />
            <div class="Clear">
            </div>
            <div class="CustomCell" style="width: 115px; height: 30px;">
                Calender Type</div>
            <%= Html.DropDownList("ddl_CalenderType", null, new { style = "width:950px; padding: 4px;" })%>
            <div class="Clear">
            </div>
            <div class="CustomCell" style="width: 115px; height: 30px;">
                Prefix</div>
            <input type="text" class="CustomText" style="width: 219px;" id="txt_Prefix" name="txt_Prefix"
                maxlength="2" />
            <div class="Clear">
            </div>
            <div class="CustomCell" style="width: 115px; height: 30px;">
                Start Date</div>
            <div class="CustomCell" style="width: 282px; height: 30px;">
                <input type="text" class="CustomText" style="width: 220px;" id="txt_SratrtDate" name="txt_SratrtDate"
                    value="<%=ViewData["CurrentDate"]%>" maxlength="50" />
            </div>
            <div class="CustomCell" style="width: 115px; height: 30px;">
                End Date</div>
            <div class="CustomCell" style="width: 282px; height: 30px;">
                <input type="text" class="CustomText" style="width: 220px;" id="txt_EndDate" name="txt_EndDate"
                    value="<%=ViewData["CurrentDate"]%>" maxlength="50" />
            </div>
            <script type="text/javascript">
                $('#txt_SratrtDate').Zebra_DatePicker({
                    format: 'm/d/Y'
                });
                $('#txt_EndDate').Zebra_DatePicker({
                    format: 'm/d/Y'
                });
            </script>
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
