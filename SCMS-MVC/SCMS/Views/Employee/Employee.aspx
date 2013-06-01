<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SCMSMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Employee Setup
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        function SaveRecord() {
            
            var lcnt_MessageBox = document.getElementById('MessageBox');
            var lcnt_txtSelectedCode = document.getElementById("txt_SelectedCode");

            var txt_Gender = document.getElementById('ddl_Gender');
            var txt_Religion = document.getElementById('ddl_Religion');
            var txt_MaritalStatus = document.getElementById('ddl_MaritalStatus');
            var txt_Nationality = document.getElementById('ddl_Nationality');

            var txt_Title = document.getElementById('txt_Title');
            var txt_Address = document.getElementById('txt_Address');

            var txt_Email = document.getElementById('txt_Email');
            var txt_Phone = document.getElementById('txt_Phone');
            var txt_DoB = document.getElementById('txt_DoB');

            var txt_CNIC = document.getElementById('txt_CNIC');
            var txt_Mobile = document.getElementById('txt_Mobile');
            var txt_AptmentDate = document.getElementById('txt_AptmentDate');
            var txt_JoiningDate = document.getElementById('txt_JoiningDate');
            var txt_ConfirmDate = document.getElementById('txt_ConfirmDate');
            var txt_Probation = document.getElementById('txt_Probation');
            var txt_NoticePerd = document.getElementById('txt_NoticePerd');


            if (txt_Title.value == "") {
                FadeIn(lcnt_MessageBox);
                lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Please! Enter Title</p>";
                lcnt_MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(lcnt_MessageBox);
                txt_Title.focus();
                return;
            }
            if (txt_DoB.value == "") {
                FadeIn(lcnt_MessageBox);
                lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Please! Enter DoB.</p>";
                lcnt_MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(lcnt_MessageBox);
                txt_DoB.focus();
                return;
            }
            if (txt_AptmentDate.value == "") {
                FadeIn(lcnt_MessageBox);
                lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Please! Enter Appointment Date</p>";
                lcnt_MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(lcnt_MessageBox);
                txt_AptmentDate.focus();
                return;
            }
            if (txt_JoiningDate.value == "") {
                FadeIn(lcnt_MessageBox);
                lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Please! Enter Joining Date</p>";
                lcnt_MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(lcnt_MessageBox);
                txt_JoiningDate.focus();
                return;
            }
            if (txt_Probation.value == "") {
                FadeIn(lcnt_MessageBox);
                lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Please! Enter Probation</p>";
                lcnt_MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(lcnt_MessageBox);
                txt_Probation.focus();
                return;
            }
            else {
                var Url = document.getElementById('frm_EmployeeSetup').action;
                Url += "Employee/SaveRecord?Code=" + lcnt_txtSelectedCode.value + "&Title=" + txt_Title.value + "&Address=" + txt_Address.value + "&DoB=" + txt_DoB.value + "&CNIC=" + txt_CNIC.value + "&Gender=" + txt_Gender.value + "&MeritalStaus=" + txt_MaritalStatus.value + "&Nationality=" + txt_Nationality.value + "&Religion=" + txt_Religion.value + "&Email=" + txt_Email.value + "&Phone=" + txt_Phone.value + "&Mobile=" + txt_Mobile.value + "&AptmentDate=" + txt_AptmentDate.value + "&JoiningDate=" + txt_JoiningDate.value + "&ConfirmDate=" + txt_ConfirmDate.value + "&Probation=" + txt_Probation.value + "&NoticePerd=" + txt_NoticePerd.value;
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
            document.getElementById('ddl_Gender').value = "";
            document.getElementById('ddl_Religion').value = "";
            document.getElementById('ddl_MaritalStatus').value = "";
            document.getElementById('ddl_Nationality').value = "";

            document.getElementById('txt_Title').value = "";
            document.getElementById('txt_Address').value = "";

            document.getElementById('txt_Email').value = "";
            document.getElementById('txt_Phone').value = "";
            document.getElementById('txt_DoB').value = "";

            document.getElementById('txt_CNIC').value = "";
            document.getElementById('txt_Mobile').value = "";
            document.getElementById('txt_AptmentDate').value = "";
            document.getElementById('txt_JoiningDate').value = "";
            document.getElementById('txt_ConfirmDate').value = "";
            document.getElementById('txt_Probation').value = "";
            document.getElementById('txt_NoticePerd').value = "";


        }

        function EditRecord(Id) {
             window.location = "/Employee/Edit/" + Id;
        }

        function DeleteRecord(Id) {
            if (confirm("Do you really want to delete this record")) {

                var lcnt_MessageBox = document.getElementById('MessageBox');
                var Url = document.getElementById('frm_EmployeeSetup').action;

                Url += "Employee/DeleteRecord?EmpId=" + Id;
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

        function MakeConfirmDate() {
             var $Probation =  $("#txt_Probation").val();
             var $JoiningDate = $("#txt_JoiningDate").val();

             if ($JoiningDate.length > 0 && $Probation != "")
             {
                 var confirmDate = new Date($JoiningDate);
                 var year = confirmDate.getFullYear();
                 var months = confirmDate.getMonth();
                 var day = confirmDate.getDate();
                 
                 var d = new Date(year, months, day);
                 d.setMonth(d.getMonth() + parseInt($Probation));

                 $("#txt_ConfirmDate").val($.datepicker.formatDate('mm/dd/yy', d));

               // $("#txt_ConfirmDate").val(confirmDate.getMonth() + "/" + confirmDate.getDate() + "/"+confirmDate.getFullYear());
             }
         }

         $(document).ready(function () {

             $("#txt_Probation").keydown(function (event) {
                 if (event.keyCode < 48 || event.keyCode > 57)
                     event.preventDefault();
             });

             $("#txt_NoticePerd").keydown(function (event) {
                 if (event.keyCode < 48 || event.keyCode > 57)
                     event.preventDefault();
             });
         })

    </script>
    <form id="frm_EmployeeSetup" action='<%=Url.Content("~/") %>'>
    <input type="hidden" id="txt_SelectedCode" name="txt_SelectedCode" value="" />
    <div class="box round first fullpage grid">
        <h2>Employee Setup</h2>
        <div class="block">
            <div id="MessageBox"></div>
            <div class="CustomCell" style="width: 139px; height: 30px;margin-top:5px;">
                Code</div>
                <div class="CustomCell" style="height: 30px;margin-left:0;margin-top:5px;">
                   <input type="text" class="CustomText" style="width: 100px;" id="txt_Code" name="txt_Code"
                    maxlength="100" value="[Auto]" readonly="readonly" /> 
                </div>

                <div class="CustomCell" style="width: 107px; height: 30px;margin-top:5px;">Date of Birth</div>
                <div class="CustomCell" style="width: 215px; height: 30px;margin-left:0;">
                    <input type="text" class="CustomText" id="txt_DoB" name="txt_DoB" maxlength="100" style="width:186px;" readonly />
                    <script type="text/javascript">
                        $('#txt_DoB').datepicker().on('changeDate', function (ev) {
                            $('#txt_DoB').datepicker("hide");
                        });
                    </script>
                </div>

                <div class="Clear"></div>

                <div class="CustomCell" style="width: 139px; height: 30px;margin-top:5px;">
                Title</div>
                <div class="CustomCell" style="height: 30px;margin-left:0;margin-top:5px;">
                   <input type="text" class="CustomText" style="width: 888px;" id="txt_Title" name="txt_Title" maxlength="100" />
                </div>
                <div class="Clear"></div>

                <div class="CustomCell" style="width: 139px; height: 30px; margin-top:5px;">
                Address </div>
                <div class="CustomCell" style="height: 30px;margin-left:0;margin-top:5px;">
                   <input type="text" class="CustomText" style="width: 890px;" id="txt_Address" name="txt_Address" maxlength="150" />
                </div>

                <div class="Clear"></div>


                <div class="CustomCell" style="width: 139px; height: 30px;margin-top:5px;">Gender</div>
                <div class="CustomCell" style="width: 215px; height: 30px;margin-left:0;margin-top:5px;">
                     <%= Html.DropDownList("ddl_Gender", null, new { style = "width:200px;padding: 4px;" })%>
                </div>

                <div class="CustomCell" style="width: 107px; height: 30px;margin-top:8px;margin-left:100px;">Religion</div>
                <div class="CustomCell" style="width: 215px; height: 30px;margin-top:5px;">
                     <%= Html.DropDownList("ddl_Religion", null, new { style = "width:200px;padding: 4px;" })%>    
                </div>
                <div class="Clear"></div>

                <div class="CustomCell" style="width: 139px; height: 30px;margin-top:5px;">Marital Status</div>
                <div class="CustomCell" style="width: 215px; height: 30px;margin-left:0;">
                     <%= Html.DropDownList("ddl_MaritalStatus", null, new { style = "width:200px; padding: 4px;" })%>
                </div>

                <div class="CustomCell" style="width: 107px; height: 30px;margin-top:8px;margin-left:100px;">Nationality</div>
                <div class="CustomCell" style="width: 215px; height: 30px;">
                    <%= Html.DropDownList("ddl_Nationality", null, new { style = "width:200px; padding: 4px;" })%>
                </div>

                <div class="Clear"></div>

                <div class="CustomCell" style="width: 139px; height: 30px;margin-top:5px;">Phone</div>
                <div class="CustomCell" style="width: 215px; height: 30px;margin-left:0;">
                    <input type="text" class="CustomText" id="txt_Phone" name="txt_Phone" maxlength="100" style="width:186px;" />
                </div>

                <div class="CustomCell" style="width: 107px; height: 30px;margin-top:8px;margin-left:100px;">Email</div>
                <div class="CustomCell" style="width: 215px; height: 30px;">
                    <input type="text" class="CustomText" id="txt_Email" name="txt_Email" maxlength="100" style="width: 186px;" />
                </div>


                <div class="Clear"></div>

                <div class="CustomCell" style="width: 139px; height: 30px;margin-top:5px;">Mobile</div>
                <div class="CustomCell" style="width: 215px; height: 30px;margin-left:0;">
                    <input type="text" class="CustomText" id="txt_Mobile" name="txt_Mobile" maxlength="100" style="width:186px;" />
                </div>

                <div class="CustomCell" style="width: 107px; height: 30px;margin-top:8px;margin-left:100px;">CNIC</div>
                <div class="CustomCell" style="width: 215px; height: 30px;">
                    <input type="text" class="CustomText" id="txt_CNIC" name="txt_CNIC" maxlength="100" style="width: 186px;" />
                </div>


                 <div class="Clear"></div>


                <div class="CustomCell" style="width: 139px; height: 30px;margin-top:5px;">Appointment Date</div>
                <div class="CustomCell" style="width: 215px; height: 30px;margin-left:0;">
                    <input type="text" class="CustomText" id="txt_AptmentDate" name="txt_AptmentDate" maxlength="100" style="width:186px;" readonly />
                    <script type="text/javascript">
                        $('#txt_AptmentDate').datepicker().on('changeDate', function (ev) {
                            $('#txt_AptmentDate').datepicker("hide");
                        });
                    </script>
                </div>

                <div class="CustomCell" style="width: 107px; height: 30px;margin-top:8px;margin-left:100px;">Joining Date</div>
                <div class="CustomCell" style="width: 215px; height: 30px;">
                    <input type="text" class="CustomText" id="txt_JoiningDate" name="txt_JoiningDate" maxlength="100" style="width: 186px;"  readonly/>
                     <script type="text/javascript">


                         $('#txt_JoiningDate').datepicker().on('changeDate', function (ev) {
                             MakeConfirmDate();
                             $('#txt_JoiningDate').datepicker("hide");
                         });


                     </script>
                </div>

                <div class="Clear"></div>

                <div class="CustomCell" style="width: 139px; height: 30px;margin-top:5px;">Probation (Months)</div>
                <div class="CustomCell" style="width: 215px; height: 30px;margin-left:0;">
                    <input type="text" class="CustomText" id="txt_Probation" name="txt_Probation" onkeyup="MakeConfirmDate()" onkeydown="MakeConfirmDate()" maxlength="2" style="width:186px;" />
                </div>

                <div class="CustomCell" style="width: 107px; height: 30px;margin-top:8px;margin-left:100px;">Confirmation Date</div>
                <div class="CustomCell" style="width: 215px; height: 30px;">
                    <input type="text" class="CustomText" id="txt_ConfirmDate" name="txt_ConfirmDate" maxlength="100" style="width:186px;" readonly />
                </div>

                <div class="Clear"></div>

                <div class="CustomCell" style="width: 139px; height: 30px;margin-top:5px;">Notice Period (Months)</div>
                <div class="CustomCell" style="width: 215px; height: 30px;margin-left:0;">
                    <input type="text" class="CustomText" id="txt_NoticePerd" name="txt_NoticePerd" maxlength="2" style="width:186px;" />
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



