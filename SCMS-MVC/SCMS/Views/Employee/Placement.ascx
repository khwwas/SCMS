<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

    <script type="text/javascript">

        function SaveRecord() {

            var lcnt_MessageBox = document.getElementById('MessageBoxPlacement');
            var lcnt_txtSelectedCode = document.getElementById("txt_PSelectedCode");
            var EmpSelectedCode = document.getElementById("txt_SelectedCode");
           
            var txt_Department = document.getElementById('ddl_Department');
            var txt_EmployeeType = document.getElementById('ddl_EmployeeType');
            var txt_JobTitle = document.getElementById('ddl_JobTitle');
            var txt_LeaveGroup = document.getElementById('ddl_LeaveGroup');

            var txt_LeaveType = document.getElementById('ddl_LeaveType');
            var txt_Location = document.getElementById('ddl_Location');
            var txt_Shift = document.getElementById('ddl_Shift');

            if (EmpSelectedCode.value == "") {
                FadeIn(lcnt_MessageBox);
                lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Please! Create Employee First!</p>";
                lcnt_MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(lcnt_MessageBox);
                return;
            }
            else
            {
                var Url = document.getElementById('frm_EmployeeSetup').action;
                Url += "Employee/SavePlacement?Code=" + lcnt_txtSelectedCode.value + "&EmpId=" + EmpSelectedCode.value + "&DptId=" + txt_Department.value + "&EmpTypId=" + txt_EmployeeType.value + "&JTId=" + txt_JobTitle.value + "&LevGrpId=" + txt_LeaveGroup.value + "&LevTypId=" + txt_LeaveType.value + "&LocId=" + txt_Location.value + "&ShftId=" + txt_Shift.value;
                document.getElementById("Waiting_Image").style.display = "block";
                document.getElementById("btn_Save").style.display = "none";
                $.ajax({
                    type: "GET",
                    url: Url,
                    success: function (response) {
                        FadeIn(lcnt_MessageBox);
                        if (response == "0") {
                            lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Unable to save record.</p>";
                            lcnt_MessageBox.setAttribute("class", "message error");

                        } else {
                            lcnt_MessageBox.innerHTML = "<h5>Success!</h5><p>Record saved successfully.</p>";
                            lcnt_MessageBox.setAttribute("class", "message success");
                            $("#txt_PCode").val(response);
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
            var lcnt_MessageBox = document.getElementById('MessageBoxPlacement');

            lcnt_MessageBox.removeAttribute("class");
            lcnt_MessageBox.innerHTML = "";

            document.getElementById('txt_PSelectedCode').value = "";
            document.getElementById('txt_PCode').value = "[Auto]";

            document.getElementById('ddl_Location').value = "";
            document.getElementById('ddl_Department').value = "";
            document.getElementById('ddl_EmployeeType').value = "";
            document.getElementById('ddl_JobTitle').value = "";
            document.getElementById('ddl_LeaveGroup').value = "";
            document.getElementById('ddl_LeaveType').value = "";
            document.getElementById('ddl_Shift').value = "";
        }


        function EditRecord(Id) {
            document.getElementById('txt_PSelectedCode').value = Id;
            document.getElementById('txt_PCode').value = Id;

            document.getElementById('ddl_Location').value = document.getElementById('ddl_location' + Id).value;
            document.getElementById('ddl_Department').value = document.getElementById('ddl_Department' + Id).value;
            document.getElementById('ddl_EmployeeType').value = document.getElementById('ddl_EmployeeType' + Id).value;
            document.getElementById('ddl_JobTitle').value = document.getElementById('ddl_JobTitle' + Id).value;
            document.getElementById('ddl_LeaveGroup').value = document.getElementById('ddl_LeaveGroup' + Id).value;
            document.getElementById('ddl_LeaveType').value = document.getElementById('ddl_LeaveType' + Id).value;
            document.getElementById('ddl_Shift').value = document.getElementById('ddl_Shift' + Id).value;
           
            scroll(0, 0);
        }

        function DeleteRecord(Id) {
            if (confirm("Do you really want to delete this record")) {
                var EmpSelectedCode = document.getElementById("txt_SelectedCode");
                var lcnt_MessageBox = document.getElementById('MessageBoxPlacement');
                var Url = document.getElementById('frm_EmployeeSetup').action;

                Url += "Employee/DeletePlacement?EmpId="+EmpSelectedCode.value+"&Id="+Id;
                $.ajax({
                    type: "GET",
                    url: Url,
                    success: function (response) {
                        html = response;
                        $("#GridPlacmentContainer").html(response);
                        SetGrid();
                        ResetForm();
                        FadeIn(lcnt_MessageBox);
                        if (document.getElementById("SavePlacementResult").value == "0") {
                            lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Unable to delete record.</p>";
                            lcnt_MessageBox.setAttribute("class", "message error");

                        } else {
                            lcnt_MessageBox.innerHTML = "<h5>Success!</h5><p>Record deleted successfully.</p>";
                            lcnt_MessageBox.setAttribute("class", "message success");
                            document.getElementById("txt_PCode").value = response;
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

    <% 
        SCMSDataLayer.DB.sp_GetEmployeePlacementsResult Placement = ViewData["LastPlacement"] != null ? (SCMSDataLayer.DB.sp_GetEmployeePlacementsResult)ViewData["LastPlacement"] : null;
    %>       

    <input type="hidden" id="txt_PSelectedCode" name="txt_PSelectedCode" value="" />
    <div class="box round first fullpage grid" style="margin-top:0;">
        <div class="block">
            <div id="MessageBoxPlacement"></div>
            <div class="CustomCell" style="width: 139px; height: 30px;margin-top:-5px;">
                Code</div>

                <div class="CustomCell" style="height: 30px;margin-left:0;margin-top:-5px;">
                   <input type="text" class="CustomText" style="width: 100px;" id="txt_PCode" name="txt_PCode"
                    maxlength="100" value="<%=Placement!=null?Placement.Plcmt_Code:"[Auto]" %>" readonly="readonly" /> 
                </div>
                <div class="Clear"></div>

                <div class="CustomCell" style="width: 139px; height: 30px;margin-top:5px;">Department</div>
                <div class="CustomCell" style="width: 215px; height: 30px;margin-left:0;margin-top:5px;">
                     <%= Html.DropDownList("ddl_Department", null, new { style = "width:200px;padding: 4px;" })%>
                </div>

                <div class="CustomCell" style="width: 107px; height: 30px;margin-top:8px;margin-left:100px;">Employee Type</div>
                <div class="CustomCell" style="width: 215px; height: 30px;margin-top:5px;">
                     <%= Html.DropDownList("ddl_EmployeeType", null, new { style = "width:200px;padding: 4px;" })%>    
                </div>
                <div class="Clear"></div>

                <div class="CustomCell" style="width: 139px; height: 30px;margin-top:5px;">Job Title</div>
                <div class="CustomCell" style="width: 215px; height: 30px;margin-left:0;">
                     <%= Html.DropDownList("ddl_JobTitle", null, new { style = "width:200px; padding: 4px;" })%>
                </div>

                <div class="CustomCell" style="width: 107px; height: 30px;margin-top:8px;margin-left:100px;">Leave Group</div>
                <div class="CustomCell" style="width: 215px; height: 30px;">
                    <%= Html.DropDownList("ddl_LeaveGroup", null, new { style = "width:200px; padding: 4px;" })%>
                </div>

                <div class="Clear"></div>

                <div class="CustomCell" style="width: 139px; height: 30px;margin-top:5px;">Leave Type</div>
                <div class="CustomCell" style="width: 215px; height: 30px;margin-left:0;">
                     <%= Html.DropDownList("ddl_LeaveType", null, new { style = "width:200px; padding: 4px;" })%>
                </div>

                <div class="CustomCell" style="width: 107px; height: 30px;margin-top:8px;margin-left:100px;">Location</div>
                <div class="CustomCell" style="width: 215px; height: 30px;">
                    <%= Html.DropDownList("ddl_Location", null, new { style = "width:200px; padding: 4px;" })%>
                </div>

                <div class="Clear"></div>

                <div class="CustomCell" style="width: 139px; height: 30px;margin-top:5px;">Shift</div>
                <div class="CustomCell" style="width: 215px; height: 30px;margin-left:0;">
                    <%= Html.DropDownList("ddl_Shift", null, new {style = "width:200px; padding: 4px;" })%>
                </div>
             
                <div class="Clear"></div>
           
                <div style="float: right; margin-bottom: 10px;">
                    <div style="float: left; margin-right: 5px;">
                        <input id="btn_Save" type="button" value="Save" class="btn btn-blue" onclick="SaveRecord();"
                            style="width: 90px; height: 35px; padding-top: 5px; color: White; font-weight: bold;
                            font-size: 11pt;" />
                        <img alt="" id="Waiting_Image" src="../../img/Ajax_Loading.gif" style="display: none;
                            margin-left: 10" /></div>
                    <div style="float: left;">
                        <input type="button" value="New" class="btn btn-grey" onclick="ResetForm();" style="width: 90px;
                            height: 35px; padding-top: 5px; color: White; font-weight: bold; font-size: 11pt;" />
                    </div>
                </div>
                <hr />
        </div>
    </div>
  
 



