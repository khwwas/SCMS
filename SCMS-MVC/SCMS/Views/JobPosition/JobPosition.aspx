﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SCMSMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Job Position
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        function SaveJobPosition() {
            var MessageBox = document.getElementById('MessageBox');
            
            var txt_SelectedCode = document.getElementById("txt_SelectedCode");
            var txt_Title = document.getElementById('txt_Title');
            var txt_Remarks = document.getElementById('txt_Remarks');
            var ddl_location = document.getElementById('ddl_location');
            var ddl_functionalarea = document.getElementById('ddl_functionalarea');
            //var ddl_jobtitle = document.getElementById('ddl_jobtitle');
            //var ddl_department = document.getElementById('ddl_department');

           
            if (txt_Title.value == "") {
                FadeIn(MessageBox);
                MessageBox.innerHTML = "<h5>Error!</h5><p>Please enter title.</p>";
                MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(MessageBox);
                txt_Title.focus();
                return;
            }
            else if (ddl_location.value == 0)
            {
                FadeIn(lcnt_MessageBox);
                lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Please! Select Location</p>";
                lcnt_MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(lcnt_MessageBox);
                ddl_location.focus();
                return;
            }
            else
            {
                var Url = document.getElementById('frm_JobPosition').action;
                Url += "JobPosition/SaveJobPosition?Code=" + txt_SelectedCode.value + "&Title=" + txt_Title.value + "&Remarks=" + txt_Remarks.value + "&location=" + ddl_location.value + "&Department=&Job=&functionalarea=" + ddl_functionalarea.value;
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
            document.getElementById('txt_Title').value = "";
            document.getElementById('txt_Remarks').value = "";
            document.getElementById('ddl_location').value = "";
            document.getElementById('ddl_functionalarea').value = "";

        }

        function EditRecord(Id) {
            document.getElementById('txt_SelectedCode').value = Id;
            document.getElementById('txt_Code').value = Id;
            document.getElementById('txt_Title').value = document.getElementById('txt_Title' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            document.getElementById('txt_Remarks').value = document.getElementById('txt_Remarks' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            document.getElementById('ddl_location').value = document.getElementById('ddl_location' + Id).value;
            document.getElementById('ddl_functionalarea').value = document.getElementById('ddl_functionalarea' + Id).value;
            scroll(0, 0);
        }

        function DeleteRecord(Id) {
            if (confirm("Do you really want to delete this record")) {
                var MessageBox = document.getElementById('MessageBox');
                var Url = document.getElementById('frm_JobPosition').action;
                Url += "JobPosition/DeleteJobPosition?JobPositionId=" + Id;
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
                            lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Unable to delete record.</p>";
                            lcnt_MessageBox.setAttribute("class", "message error");

                        } else {
                            lcnt_MessageBox.innerHTML = "<h5>Success!</h5><p>Record deleted successfully.</p>";
                            lcnt_MessageBox.setAttribute("class", "message success");
                        }
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
    <form id="frm_JobPosition" action='<%=Url.Content("~/") %>'>
    <input type="hidden" id="txt_SelectedCode" name="txt_SelectedCode" value="" />
    <div class="box round first fullpage grid">
        <h2>
            Job Position</h2>
        <div class="block">
            <div id="MessageBox">
            </div>
            <div class="CustomCell" style="width: 115px; height: 30px">
                Code</div>
            <input type="text" class="CustomText" style="width: 100px;" id="txt_Code" name="txt_Code"
                maxlength="100" value="[Auto]" readonly="readonly" />
            <div class="Clear">
            </div>
              <div class="CustomCell" style="width: 115px; height: 30px;">
                Location</div>
                <%= Html.DropDownList("ddl_location", null, new { style = "width:900px; padding: 4px;" })%>
              <div class="Clear">
            </div>
            
            <%--<div class="CustomCell" style="width: 115px; height: 30px;">
                Departement</div>
                <%= Html.DropDownList("ddl_departement", null, new { style = "width:900px; padding: 4px;" })%>
              <div class="Clear"></div>
            --%>  
            <div class="CustomCell" style="width: 115px; height: 30px;">
                Functional Area</div>
                <%= Html.DropDownList("ddl_functionalarea", null, new { style = "width:900px; padding: 4px;" })%>
              <div class="Clear"></div> 

            <%--<div class="CustomCell" style="width: 115px; height: 30px;">
                Job Title</div>
                <%= Html.DropDownList("ddl_jobtitle", null, new { style = "width:900px; padding: 4px;" })%>
              <div class="Clear"></div>
            --%>

            <div class="CustomCell" style="width: 115px; height: 30px">
                Title</div>
            <input type="text" class="CustomText" style="width: 940px;" id="txt_Title" name="txt_Title"
                maxlength="100" />
            <div class="Clear">
            </div>
            
            <div class="CustomCell" style="width: 115px; height: 30px">
                Remarks</div>
            <input type="text" class="CustomText" style="width: 940px;" id="txt_Remarks" name="txt_Remarks"
                maxlength="100" />
            
            <div class="Clear">
            </div>
                       
            <div class="Clear">
            </div>
         
            <div style="float: right; margin-bottom: 10px;">
                <div style="float: left; margin-right: 5px;">
                    <input id="btn_Save" type="button" value="Save" class="btn btn-blue" onclick="SaveJobPosition();"
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
