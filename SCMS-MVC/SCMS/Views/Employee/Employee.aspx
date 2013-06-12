<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SCMSMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Employee Setup
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        function SaveEmployeeRecord() {

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

                var file_data = $("#profileImage").prop("files")[0];
                var form_data = new FormData();
                form_data.append("Code", lcnt_txtSelectedCode.value)
                form_data.append("Title", txt_Title.value)
                form_data.append("Address", txt_Address.value)
                form_data.append("DoB", txt_DoB.value)
                form_data.append("CNIC", txt_CNIC.value)
                form_data.append("Gender", txt_Gender.value)
                form_data.append("MeritalStaus", txt_MaritalStatus.value)
                form_data.append("Nationality", txt_Nationality.value)
                form_data.append("Religion", txt_Religion.value)
                form_data.append("Email", txt_Email.value)
                form_data.append("Phone", txt_Phone.value)
                form_data.append("Mobile", txt_Mobile.value)
                form_data.append("AptmentDate", txt_AptmentDate.value)

                form_data.append("JoiningDate", txt_JoiningDate.value)
                form_data.append("ConfirmDate", txt_ConfirmDate.value)
                form_data.append("Probation", txt_Probation.value)
                form_data.append("NoticePerd", txt_NoticePerd.value)
                if (file_data != null) {
                   form_data.append("ProfileImg", file_data)
                }             

//                var Url = document.getElementById('frm_EmployeeSetup').action;
//                Url += "Employee/SaveEmployee?Code=" + lcnt_txtSelectedCode.value + "&Title=" + txt_Title.value + "&Address=" + txt_Address.value + "&DoB=" + txt_DoB.value + "&CNIC=" + txt_CNIC.value + "&Gender=" + txt_Gender.value + "&MeritalStaus=" + txt_MaritalStatus.value + "&Nationality=" + txt_Nationality.value + "&Religion=" + txt_Religion.value + "&Email=" + txt_Email.value + "&Phone=" + txt_Phone.value + "&Mobile=" + txt_Mobile.value + "&AptmentDate=" + txt_AptmentDate.value + "&JoiningDate=" + txt_JoiningDate.value + "&ConfirmDate=" + txt_ConfirmDate.value + "&Probation=" + txt_Probation.value + "&NoticePerd=" + txt_NoticePerd.value;
                
                document.getElementById("Waiting_Image").style.display = "block";
                document.getElementById("btn_Save").style.display = "none";
                $.ajax({
                    //                    type: "GET",
                    //                    url: Url,
                    url: "/Employee/SaveEmployee",
                    cache: false,
                    contentType: false,
                    dataType: 'json',
                    processData: false,
                    data: form_data,
                    type: 'post',
                    success: function (response) {
                        FadeIn(lcnt_MessageBox);
                        var Arr = response.toString().split(",");
                        if (Arr[0] != null && Arr[0] == "0") {
                            lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Unable to update record.</p>";
                            lcnt_MessageBox.setAttribute("class", "message error");
                        } else {
                            lcnt_MessageBox.innerHTML = "<h5>Success!</h5><p>Record updated successfully.</p>";
                            lcnt_MessageBox.setAttribute("class", "message success");
                            document.getElementById("txt_Code").value = Arr[1].toString();
                            document.getElementById("txt_SelectedCode").value = Arr[1].toString();

                            if (Arr[2] != null && Arr[2] != "0") {
                                $("#prPic").attr("src", "../../UploadedDocuments/" + Arr[2]);
                                $("#txt_ChangeImage").css("display", "block");
                                $("#ImageContainer").css("display", "none")
                                $("#UploadBtn").removeClass("hide");
                                $('#PicField').val("");
                                $('#profileImage').val("");
                            }

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

        function MakeConfirmDate() {
            var $Probation = $("#txt_Probation").val();
            var $JoiningDate = $("#txt_JoiningDate").val();

            if ($JoiningDate.length > 0 && $Probation != "") {
                var confirmDate = new Date($JoiningDate);
                var year = confirmDate.getFullYear();
                var months = confirmDate.getMonth();
                var day = confirmDate.getDate();
                var d = new Date(year, months, day);
                d.setMonth(d.getMonth() + parseInt($Probation));
                $("#txt_ConfirmDate").val($.datepicker.formatDate('mm/dd/yy', d));
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


            $('#tabs').tab();

            $("#changImg").click(function (event) {
                $("#txt_ChangeImage").css("display", "none");
                $("#ImageContainer").css("display", "block")
            });

            $("#changCancel").click(function (event) {
                $("#txt_ChangeImage").css("display", "block");
                $("#ImageContainer").css("display", "none")
                $('#PicField').val("");
                $('#profileImage').val("");
            });

            $('input[id=profileImage]').change(function () {
                if (ValidateProfileImg()) {
                    $('#PicField').val($(this).val());
                } else {
                    $('#PicField').val("");
                    $('#profileImage').val("");
                }
            });


        })


        function UploadImage() {
            var lcnt_MessageBox = document.getElementById('ImgMessageBox');
            if (ValidateProfileImg()) {
                var EmpId = $("#txt_SelectedCode").val();
                var file_data = $("#profileImage").prop("files")[0];   
                var form_data = new FormData();                  
                form_data.append("profileImage", file_data)
                form_data.append("EmployeeId", "" + EmpId)                 
                $.ajax({
                    url: "/Employee/ChangeProfileImg",
                    cache: false,
                    contentType: false,
                    processData: false,
                    data: form_data,                         
                    type: 'post',
                    success: function (response) {
                        if (response == "0") {
                            lcnt_MessageBox.innerHTML = "<p>Unable to upload picture!</p>";
                            lcnt_MessageBox.setAttribute("class", "message error");

                        } else {
                            lcnt_MessageBox.innerHTML = "<p>Picture uploaded successfully.</p>";
                            lcnt_MessageBox.setAttribute("class", "message success");
                            $("#prPic").attr("src", "../../UploadedDocuments/" + response);
                            $("#txt_ChangeImage").css("display", "block");
                            $("#ImageContainer").css("display", "none")
                            $('#PicField').val("");
                            $('#profileImage').val("");

                        }
                        
                    },
                    error: function (rs, e) {
                        lcnt_MessageBox.innerHTML = "<p>Unable to upload picture!</p>";
                        lcnt_MessageBox.setAttribute("class", "message error");
                    }
                })
            }
            else
            {
                lcnt_MessageBox.innerHTML = "<p>Select an image!</p>";
                lcnt_MessageBox.setAttribute("class", "message error");
            }
            FadeOut(lcnt_MessageBox);
        }

        function AddNew() {
            window.location = "/Employee/EmployeeEntry";
        }


        function ValidateProfileImg() {
             var lcnt_MessageBox = document.getElementById('ImgMessageBox');
             if (document.getElementById('profileImage').files.item(0) != null) {
                var fSize = document.getElementById('profileImage').files.item(0).size;
                if (fSize <= 2097152.0) {
                    var fileName = document.getElementById('profileImage').value;
                    var ext = fileName.substring(fileName.lastIndexOf('.') + 1, fileName.length);
                    if (ext == "png" || ext == "jpeg" || ext == "gif" || ext == "jpg") {
                       return true;
                    }else {
                       lcnt_MessageBox.innerHTML = "<p>Only images can be uploaded!</p>";
                       lcnt_MessageBox.setAttribute("class", "message error");
                       lcnt_MessageBox.setAttribute("style", "display:block");
                       FadeOut(lcnt_MessageBox);
                       return false;
                    }
                }
                else {
                    lcnt_MessageBox.innerHTML = "<p>Maximum file size should be 2MB!</p>";
                    lcnt_MessageBox.setAttribute("class", "message error");
                   
                    FadeOut(lcnt_MessageBox);
                    return false;
                }
            }
           
        }


    </script>

    <% 
        SCMSDataLayer.DB.SETUP_Employee EmployeeData = ViewData["Employee"] != null ? (SCMSDataLayer.DB.SETUP_Employee)ViewData["Employee"] : null;
        string dob = EmployeeData != null && EmployeeData.Emp_DoB != null ? Convert.ToDateTime(EmployeeData.Emp_DoB).ToShortDateString() : "";
        string aptDate = EmployeeData != null && EmployeeData.Emp_AptmentDate != null ? Convert.ToDateTime(EmployeeData.Emp_AptmentDate).ToShortDateString() : "";
        string joinDate = EmployeeData != null && EmployeeData.Emp_JoiningDate != null ? Convert.ToDateTime(EmployeeData.Emp_JoiningDate).ToShortDateString() : "";
        string confirmDate = EmployeeData != null && EmployeeData.Emp_ConfirmDate != null ? Convert.ToDateTime(EmployeeData.Emp_ConfirmDate).ToShortDateString() : "";
        string photoSRC = "../../img/person_blank.gif";
        if (EmployeeData != null)
        {
            String RealPath = Server.MapPath("..//UploadedDocuments/" + EmployeeData.Emp_ImagePath);
            String EMPPhoto = "../../UploadedDocuments/" + EmployeeData.Emp_ImagePath;
            String DefaultPhoto = "../../img/person_blank.gif";
            photoSRC = (!string.IsNullOrEmpty(EmployeeData.Emp_ImagePath) && System.IO.File.Exists(RealPath)) ? EMPPhoto : DefaultPhoto;
        }
    %>       
    
    <form id="frm_EmployeeSetup" action='<%=Url.Content("~/") %>'>
    <input type="hidden" id="txt_SelectedCode" name="txt_SelectedCode" value="<%=EmployeeData!=null?EmployeeData.Emp_Code:"" %>" />
    <div class="box round first fullpage grid">
        <h2>Employee Setup</h2>
        <div class="block">
            <div id="MessageBox"></div>
            <div style="clear:both;"></div>
            <div id="InnerTab" class="tabbable">
            
                 <ul id="tabs" class="nav nav-tabs" data-tabs="tabs">
                    <li class="active"><a href="#Employee" data-toggle="tab">Employee</a></li>
                    <li><a href="#placmnt" data-toggle="tab">Employee Placement</a></li>
                 </ul>

                  <div id="tab-content" class="tab-content" style="overflow:visible;">
                    <div class="tab-pane active" id="Employee" style="margin-left:10px;">
                       <!---Employe Section---->
                            <div style="margin-top:20px;"></div>
                                  
                            <div style="width:800px;float:left;">     
                                       
                                <div class="CustomCell" style="width: 139px; height: 30px;margin-top:5px;">
                                Code</div>
                                <div class="CustomCell" style="height: 30px;margin-left:0;margin-top:5px;">
                                    <input type="text" class="CustomText" style="width: 100px;" id="txt_Code" name="txt_Code"
                                    maxlength="100" value="<%=EmployeeData!=null?EmployeeData.Emp_Code:"[Auto]" %>" readonly="readonly" /> 
                                </div>
                                <div class="CustomCell" style="width: 107px; height: 30px;margin-top:5px;">Date of Birth</div>
                                <div class="CustomCell" style="width: 215px; height: 30px;margin-left:0;">
                                    <input type="text" class="CustomText" id="txt_DoB" name="txt_DoB" maxlength="100" style="width:186px;" value="<%=dob %>" readonly />
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
                                    <input type="text" class="CustomText" style="width: 611px;" id="txt_Title" name="txt_Title" value="<%=EmployeeData!=null?EmployeeData.Emp_Title:"" %>" maxlength="100" />
                                </div>
                                <div class="Clear"></div>

                                <div class="CustomCell" style="width: 139px; height: 30px; margin-top:5px;">
                                Address </div>
                                <div class="CustomCell" style="height: 30px;margin-left:0;margin-top:5px;">
                                    <input type="text" class="CustomText" style="width: 611px;" id="txt_Address" name="txt_Address" value="<%=EmployeeData!=null?EmployeeData.Emp_Address:"" %>" maxlength="150" />
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
                                    <input type="text" class="CustomText" id="txt_Phone" name="txt_Phone" maxlength="100" value="<%=EmployeeData!=null?EmployeeData.Emp_Phone:"" %>" style="width:186px;" />
                                </div>

                                <div class="CustomCell" style="width: 107px; height: 30px;margin-top:8px;margin-left:100px;">Email</div>
                                <div class="CustomCell" style="width: 215px; height: 30px;">
                                    <input type="text" class="CustomText" id="txt_Email" name="txt_Email" maxlength="100" value="<%=EmployeeData!=null?EmployeeData.Emp_Email:"" %>" style="width: 186px;" />
                                </div>


                                <div class="Clear"></div>

                                <div class="CustomCell" style="width: 139px; height: 30px;margin-top:5px;">Mobile</div>
                                <div class="CustomCell" style="width: 215px; height: 30px;margin-left:0;">
                                    <input type="text" class="CustomText" id="txt_Mobile" name="txt_Mobile" maxlength="100" value="<%=EmployeeData!=null?EmployeeData.Emp_Mobile:"" %>" style="width:186px;" />
                                </div>

                                <div class="CustomCell" style="width: 107px; height: 30px;margin-top:8px;margin-left:100px;">CNIC</div>
                                <div class="CustomCell" style="width: 215px; height: 30px;">
                                    <input type="text" class="CustomText" id="txt_CNIC" name="txt_CNIC" maxlength="100" value="<%=EmployeeData!=null?EmployeeData.Emp_CNIC:"" %>" style="width: 186px;" />
                                </div>


                                    <div class="Clear"></div>


                                <div class="CustomCell" style="width: 139px; height: 30px;margin-top:5px;">Appointment Date</div>
                                <div class="CustomCell" style="width: 215px; height: 30px;margin-left:0;">
                                    <input type="text" class="CustomText" id="txt_AptmentDate" name="txt_AptmentDate" value="<%=aptDate %>" maxlength="100" style="width:186px;" readonly />
                                    <script type="text/javascript">
                                        $('#txt_AptmentDate').datepicker().on('changeDate', function (ev) {
                                            $('#txt_AptmentDate').datepicker("hide");
                                        });
                                    </script>
                                </div>

                                <div class="CustomCell" style="width: 107px; height: 30px;margin-top:8px;margin-left:100px;">Joining Date</div>
                                <div class="CustomCell" style="width: 215px; height: 30px;">
                                    <input type="text" class="CustomText" id="txt_JoiningDate" name="txt_JoiningDate" value="<%=joinDate %>" maxlength="100" style="width: 186px;"  readonly/>
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
                                    <input type="text" class="CustomText" id="txt_Probation" name="txt_Probation" value="<%=EmployeeData!=null?EmployeeData.Emp_Months_Probation:"" %>" onkeyup="MakeConfirmDate()" onkeydown="MakeConfirmDate()" maxlength="2" style="width:186px;" />
                                </div>

                                <div class="CustomCell" style="width: 107px; height: 30px;margin-top:8px;margin-left:100px;">Confirmation Date</div>
                                <div class="CustomCell" style="width: 215px; height: 30px;">
                                    <input type="text" class="CustomText" id="txt_ConfirmDate" name="txt_ConfirmDate" value="<%=confirmDate%>" maxlength="100" style="width:186px;" readonly />
                                </div>

                                <div class="Clear"></div>

                                <div class="CustomCell" style="width: 139px; height: 30px;margin-top:5px;">Notice Period (Months)</div>
                                <div class="CustomCell" style="width: 215px; height: 30px;margin-left:0;">
                                    <input type="text" class="CustomText" id="txt_NoticePerd" name="txt_NoticePerd" value="<%=EmployeeData!=null?EmployeeData.Emp_Months_NoticePerd:"" %>" maxlength="2" style="width:186px;" />
                                </div>
                            </div>
                            <div class="CustomCell" style="width:auto;height:300px;margin-top:20px;">
                                <div style="padding: 0px; overflow: auto;margin:10px;">
                                    <div style="float: left;" id="PicEdit">
                                        <div id="ImgMessageBox"></div>
                                        <div style="clear:both;"></div>
                                        <div style="float: left">
                                            <img id="prPic" class="img-rounded img-polaroid" style="width:90px; height:90px;" src="<%=photoSRC %>" width="100" height="100">
                                        </div>
                        
                                        <div id="txt_ChangeImage" style="clear: both;">
                                            <small style="margin-left: 10px;"><a id="changImg" style="text-decoration: none;cursor:pointer;">Change Photo</a></small>
                                        </div>
                                        <div id="ImageContainer" class="form-horizontal control-group" style="clear: both; display: none; padding-top: 3px;">
                                                <input name="EmployeeId" type="hidden" value="<%=EmployeeData!=null?EmployeeData.Emp_Code:"" %>" />
                                                <input id="profileImage" name="profileImage" class="file" type="file" style="display:none;" />
                                                <div class="input-append" style="margin-right:10px;">
                                                    <input id="PicField" name="PicField" type="text" style="width:87px;height:18px;" />
                                                    <a class="btn btn-grey btn-small" onclick="$('input[id=profileImage]').click()">Browse</a>
                                                </div>
                                                <a id="UploadBtn" type="button" onclick="UploadImage();" class="btn btn-blue btn-small hide">Upload</a>
                                                <a id="changCancel" class="btn btn-grey btn-small">Cancel</a>
                                                <script type="text/javascript">
                                                  <%if (EmployeeData != null){%>
                                                    $("#UploadBtn").removeClass("hide");
                                                  <%}%>
                                                </script>
                                        </div>
                                    </div>
                                </div>
                            </div>
                                  
                            <div class="Clear"></div>

                            <div style="float: right; margin-bottom: 10px;">
                                <div style="float: left; margin-right: 5px;">
                                    <input id="btn_Save" type="button" value="Save" class="btn btn-blue" onclick="SaveEmployeeRecord();"
                                        style="width: 90px; height: 35px; padding-top: 5px; color: White; font-weight: bold;
                                        font-size: 11pt;" />
                                    <img alt="" id="Waiting_Image" src="../../img/Ajax_Loading.gif" style="display: none;
                                        margin-left: 10" /></div>
                                <div style="float: left;">
                                    <input type="button" value="New" class="btn btn-grey" onclick="AddNew();" style="width: 90px;
                                        height: 35px; padding-top: 5px; color: White; font-weight: bold; font-size: 11pt;" />
                                </div>
                            </div>
                
                       <!------End Employee Section---->
                    </div>
                    <div class="tab-pane" id="placmnt">
                       <div id="PlacementForm">
                          <%Html.RenderPartial("Placement");%>
                       </div>
                    </div>
                </div>
            </div>
          </div>
       </div>
              
    </form>
 </asp:Content>



