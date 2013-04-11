<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SCMSMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Leave Types Setup
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        function SaveRecord() {
            
            var lcnt_MessageBox = document.getElementById('MessageBox');
            var lcnt_txtSelectedCode = document.getElementById("txt_SelectedCode");
            var txt_Title = document.getElementById('txt_Title');
            var txt_Abbreviation = document.getElementById('txt_Abbreviation');
            var ddl_location = document.getElementById('ddl_location');
            var txt_Count = document.getElementById('txt_Count');
            
            if (ddl_location.value == 0) {
                FadeIn(lcnt_MessageBox);
                lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Please! select location</p>";
                lcnt_MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(lcnt_MessageBox);
                ddl_location.focus();
                return;
            } else if (txt_Title.value == "") {
                FadeIn(lcnt_MessageBox);
                lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Please! enter title</p>";
                lcnt_MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(lcnt_MessageBox);
                txt_Title.focus();
                return;
            } else if (txt_Abbreviation.value == "") {
                FadeIn(lcnt_MessageBox);
                lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Please! Enter Abbreviation</p>";
                lcnt_MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(lcnt_MessageBox);
                txt_Title.focus();
                return;
            } 
            else {
                var Url = document.getElementById('frm_LeaveTypeSetup').action;
                Url += "LeaveType/SaveRecord?Code=" + lcnt_txtSelectedCode.value + "&Location=" + ddl_location.value + "&Title=" + txt_Title.value + "&Abbreviation=" + txt_Abbreviation.value + "&Count=" + txt_Count.value;
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
            document.getElementById('txt_Abbreviation').value = "";

            document.getElementById('ddl_location').value = "";
            document.getElementById('txt_Count').value = "1";
        }

        function EditRecord(Id) {
            document.getElementById('txt_SelectedCode').value = Id;
            document.getElementById('txt_Code').value = Id;
            document.getElementById('txt_Title').value = document.getElementById('txt_Title' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            document.getElementById('txt_Abbreviation').value = document.getElementById('txt_Abbreviation' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            document.getElementById('ddl_location').value = document.getElementById('ddl_location' + Id).value;

            if (document.getElementById('txt_Count' + Id).value == "") {
                document.getElementById('txt_Count').value = "1";
            }
            else {
                document.getElementById('txt_Count').value = document.getElementById('txt_Count' + Id).value;
            }


            scroll(0, 0);
        }

        function DeleteRecord(Id) {
            if (confirm("Do you really want to delete this record")) {

                var lcnt_MessageBox = document.getElementById('MessageBox');
                var Url = document.getElementById('frm_LeaveTypeSetup').action;

                Url += "LeaveType/DeleteRecord?leaveTypeId=" + Id;
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


        function ValuePlus(id) {
            var val = $('#' + id).val();
            if (parseInt(val) < 100) {
                document.getElementById(id).value = parseInt(val) + 1;
            }
        }

    </script>
    <form id="frm_LeaveTypeSetup" action='<%=Url.Content("~/") %>'>
    <input type="hidden" id="txt_SelectedCode" name="txt_SelectedCode" value="" />
    <div class="box round first fullpage grid">
        <h2>
            Leave Type Setup</h2>
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
            <div class="CustomCell" style="width: 115px; height: 30px;">
                Title</div>
            <input type="text" class="CustomText" style="width: 888px;" id="txt_Title" name="txt_Title"
                maxlength="100" />
            <div class="Clear">
            </div>

            <div class="CustomCell" style="width: 115px; height: 30px">
                Abbreviation</div>
            <input type="text" class="CustomText" style="width: 200px;" id="txt_Abbreviation" name="txt_Abbreviation"
                maxlength="10" />
            <div class="Clear">
            </div>

             <div class="CustomCell" style="width: 113px;">
                Count</div>
            <div class="CustomCell" style="width: 42px;">
                <input type="text" class="CustomText" id="txt_Count" name="txt_Count"
                    maxlength="3" style="width: 35px; border-right: 0px;" value="1" disabled="disabled" />
            </div>
             <div class="CustomCell" style="border: 1px solid #ccc; width: 20px; height: 28px;
                margin-right: 12px;">
                <div style="background-image: url('../../img/ArrowUp.png'); background-position: center;
                    height: 14px; background-repeat: no-repeat; cursor: pointer;" onclick="ValuePlus('txt_Count');">
                    &nbsp;
                </div>
                <div style="background-image: url('../../img/ArrowDown.png'); background-position: center;
                    height: 14px; background-repeat: no-repeat; cursor: pointer;" onclick="ValueMinus('txt_Count');">
                    &nbsp;
                </div>
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
