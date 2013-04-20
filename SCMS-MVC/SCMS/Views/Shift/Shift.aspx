<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SCMSMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Shift Setup
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        function SaveRecord() {
            var lcnt_MessageBox = document.getElementById('MessageBox');
            var lcnt_txtSelectedCode = document.getElementById("txt_SelectedCode");
            
            var txt_Title = document.getElementById('txt_Title');
            var txt_Abbreviation = document.getElementById('txt_Abbreviation');
            var txt_StTime = document.getElementById('txt_StartTime');
            var txt_EdTime = document.getElementById('txt_EndTime');

            var txt_BreakStTime = document.getElementById('txt_BreakStartTime');
            var txt_BreakEdTime = document.getElementById('txt_BreakEndTime');
            var txt_BreakDuration = document.getElementById('txt_BreakDuration');

            var txt_GraceIn = document.getElementById('txt_GraceIn');
            var txt_GraceEarly = document.getElementById('txt_GraceEarly');


            if (txt_Title.value == "") {
                FadeIn(lcnt_MessageBox);
                lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Please! Enter Title</p>";
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
            } else if (txt_StTime.value == "") {
                FadeIn(lcnt_MessageBox);
                lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Please! Enter Start Time</p>";
                lcnt_MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(lcnt_MessageBox);
                txt_Title.focus();
                return;
            } else if (txt_EdTime.value == "") {
                FadeIn(lcnt_MessageBox);
                lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Please! Enter End Time</p>";
                lcnt_MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(lcnt_MessageBox);
                txt_Title.focus();
                return;
            }
            else if (txt_BreakStTime.value == "") {
                FadeIn(lcnt_MessageBox);
                lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Please! Enter Break Start Time</p>";
                lcnt_MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(lcnt_MessageBox);
                txt_Title.focus();
                return;
            }
            else if (txt_BreakEdTime.value == "") {
                FadeIn(lcnt_MessageBox);
                lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Please! Enter Break End Time</p>";
                lcnt_MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(lcnt_MessageBox);
                txt_Title.focus();
                return;
            }else {
                var Url = document.getElementById('frm_ShiftSetup').action;
                Url += "Shift/SaveRecord?Code=" + lcnt_txtSelectedCode.value + "&Title=" + txt_Title.value + "&Abbreviation=" + txt_Abbreviation.value + "&StartTime=" + txt_StTime.value + "&EndTime=" + txt_EdTime.value + "&BreakStartTime=" + txt_BreakStTime.value + "&BreakEndTime=" + txt_BreakEdTime.value + "&BreakDuration=" + txt_BreakDuration.value + "&GraceIn=" + txt_GraceIn.value + "&GraceEarly=" + txt_GraceEarly.value;
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
            document.getElementById('txt_StartTime').value = "";
            document.getElementById('txt_EndTime').value = "";

            document.getElementById('txt_BreakStartTime').value = "";
            document.getElementById('txt_BreakEndTime').value = "";

            document.getElementById('txt_BreakDuration').value = "";
            document.getElementById('txt_GraceIn').value = 1;
            document.getElementById('txt_GraceEarly').value = 1;
        }

        function EditRecord(Id) {
           document.getElementById('txt_SelectedCode').value = Id;
           document.getElementById('txt_Code').value = Id;
           document.getElementById('txt_Title').value = document.getElementById('txt_Title' + Id).innerHTML.trim().toString().replace("&nbsp", "");
           document.getElementById('txt_Abbreviation').value = document.getElementById('txt_Abbreviation' + Id).innerHTML.trim().toString().replace("&nbsp", "");
           document.getElementById('txt_StartTime').value = document.getElementById('txt_StartTime' + Id).innerHTML.trim().toString().replace("&nbsp", "");
           document.getElementById('txt_EndTime').value = document.getElementById('txt_EndTime' + Id).innerHTML.trim().toString().replace("&nbsp", "");

           document.getElementById('txt_BreakStartTime').value = document.getElementById('txt_BreakStartTime' + Id).innerHTML.trim().toString().replace("&nbsp", "");
           document.getElementById('txt_BreakEndTime').value = document.getElementById('txt_BreakEndTime' + Id).innerHTML.trim().toString().replace("&nbsp", "");
           document.getElementById('txt_BreakDuration').value = document.getElementById('txt_BreakDuration' + Id).innerHTML.trim().toString().replace("&nbsp", "");

           if (document.getElementById('txt_GraceIn' + Id).value == "") {
               document.getElementById('txt_GraceIn').value = "1";
           }
           else {
               document.getElementById('txt_GraceIn').value = document.getElementById('txt_GraceIn' + Id).value;
           }

           if (document.getElementById('txt_GraceEarly' + Id).value == "") {
               document.getElementById('txt_GraceEarly').value = "1";
           }
           else {
               document.getElementById('txt_GraceEarly').value = document.getElementById('txt_GraceEarly' + Id).value;
           }
            scroll(0, 0);
        }

        function DeleteRecord(Id) {
            if (confirm("Do you really want to delete this record")) {

                var lcnt_MessageBox = document.getElementById('MessageBox');
                var Url = document.getElementById('frm_ShiftSetup').action;

                Url += "Shift/DeleteRecord?ShifId=" + Id;
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
    
    <form id="frm_ShiftSetup" action='<%=Url.Content("~/") %>'>
    <input type="hidden" id="txt_SelectedCode" name="txt_SelectedCode" value="" />
    <div class="box round first fullpage grid">
        <h2>
            Shift Setup</h2>
        <div class="block">
            <div id="MessageBox">
            </div>
           <div class="CustomCell" style="width: 115px; height: 30px">
                Code</div>
            <input type="text" class="CustomText" style="width: 100px;" id="txt_Code" name="txt_Code"
                maxlength="100" value="[Auto]" readonly="readonly" />
            <div class="Clear">
            </div>
            <div class="CustomCell" style="width: 115px; height: 30px">
                Title</div>
            <input type="text" class="CustomText" style="width: 940px;" id="txt_Title" name="txt_Title"
                maxlength="100" />
            <div class="Clear">
            </div>
            <div class="CustomCell" style="width: 115px; height: 30px">
                Abbreviation</div>
            <input type="text" class="CustomText" style="width: 200px;" id="txt_Abbreviation" name="txt_Abbreviation"
                maxlength="10" />
            <div class="Clear">
            </div>
           
            <div class="CustomCell" style="width: 115px; height: 30px;">
               Start Time</div>
            <div class="CustomCell" style="width: 215px; height: 30px;margin-left:0;">
                 <div class="input-append bootstrap-timepicker">
                    <input id="txt_StartTime" name="txt_StartTime" type="text" class="input-small">
                    <span class="add-on"><i class="icon-time"></i></span>
                 </div>


            </div>

            <div class="CustomCell" style="width: 107px; height: 30px;">
               End Time</div>
            <div class="CustomCell" style="width: 215px; height: 30px;">
                <div class="input-append bootstrap-timepicker">
                    <input id="txt_EndTime" name="txt_EndTime" type="text" class="input-small">
                    <span class="add-on"><i class="icon-time"></i></span>
                 </div>
            </div>

             <div class="Clear">
            </div>

            <div class="CustomCell" style="width: 115px; height: 30px;">
              Break Start Time</div>
            <div class="CustomCell" style="width: 215px; height: 30px;margin-left:0;">
                 <div class="input-append bootstrap-timepicker">
                    <input id="txt_BreakStartTime" name="txt_BreakStartTime" type="text" class="input-small">
                    <span class="add-on"><i class="icon-time"></i></span>
                 </div>
            </div>

            <div class="CustomCell" style="width: 107px; height: 30px;">
               Break End Time</div>
            <div class="CustomCell" style="width: 215px; height: 30px;">
                <div class="input-append bootstrap-timepicker">
                    <input id="txt_BreakEndTime" name="txt_BreakEndTime" type="text" class="input-small">
                    <span class="add-on"><i class="icon-time"></i></span>
                 </div>
            </div>

            <div class="CustomCell" style="width: 107px; height: 30px;">
               Break Duration</div>
            <div class="CustomCell" style="width: 215px; height: 30px;">
                <div class="input-append">
                    <input id="txt_BreakDuration" name="txt_BreakDuration" type="text" class="input-small" readonly="readonly">
                 </div>
            </div>

             <div class="Clear">
             </div>

             <div class="CustomCell" style="width: 113px;">
                Grace In</div>
            <div class="CustomCell" style="width: 42px;">
                <input type="text" class="CustomText" id="txt_GraceIn" name="txt_GraceIn"
                    maxlength="3" style="width: 35px; border-right: 0px;" value="1" disabled="disabled" />
            </div>
             <div class="CustomCell" style="border: 1px solid #ccc; width: 20px; height: 28px;
                margin-right: 12px;">
                <div style="background-image: url('../../img/ArrowUp.png'); background-position: center;
                    height: 14px; background-repeat: no-repeat; cursor: pointer;" onclick="ValuePlus('txt_GraceIn');">
                    &nbsp;
                </div>
                <div style="background-image: url('../../img/ArrowDown.png'); background-position: center;
                    height: 14px; background-repeat: no-repeat; cursor: pointer;" onclick="ValueMinus('txt_GraceIn');">
                    &nbsp;
                </div>
            </div>


             <div class="CustomCell" style="width: 107px;margin-left:139px;">
                Grace Early</div>
            <div class="CustomCell" style="width: 42px;">
                <input type="text" class="CustomText" id="txt_GraceEarly" name="txt_GraceEarly"
                    maxlength="3" style="width: 35px; border-right: 0px;" value="1" disabled="disabled" />
            </div>
             <div class="CustomCell" style="border: 1px solid #ccc; width: 20px; height: 28px;
                margin-right: 12px;">
                <div style="background-image: url('../../img/ArrowUp.png'); background-position: center;
                    height: 14px; background-repeat: no-repeat; cursor: pointer;" onclick="ValuePlus('txt_GraceEarly');">
                    &nbsp;
                </div>
                <div style="background-image: url('../../img/ArrowDown.png'); background-position: center;
                    height: 14px; background-repeat: no-repeat; cursor: pointer;" onclick="ValueMinus('txt_GraceEarly');">
                    &nbsp;
                </div>
            </div>

            <script type="text/javascript">
                $('#txt_StartTime').timepicker({
                    showSeconds: false,
                    showMeridian: true
                });

                $('#txt_EndTime').timepicker({
                    showSeconds: false,
                    showMeridian: true
                });

                $('#txt_BreakStartTime').timepicker({
                    showSeconds: false,
                    showMeridian: true
                }).on('changeTime.timepicker', function (ev) {
                    var endTime = $('#txt_BreakEndTime').data("timepicker").getTime();
                    var stTime = $('#txt_BreakStartTime').data("timepicker").getTime();
                    if (stTime==endTime) {
                        document.getElementById('txt_BreakDuration').value = "00:00:00";
                    } else {
                        document.getElementById('txt_BreakDuration').value = timespan3(stTime, endTime);
                    }
                });

                $('#txt_BreakEndTime').timepicker({
                    showSeconds: false,
                    showMeridian: true
                }).on('changeTime.timepicker', function (ev) {
                    var endTime = $('#txt_BreakEndTime').data("timepicker").getTime();
                    var stTime = $('#txt_BreakStartTime').data("timepicker").getTime();
                    if (stTime==endTime) {
                        document.getElementById('txt_BreakDuration').value = "00:00:00";
                    } else {
                        document.getElementById('txt_BreakDuration').value = timespan3(stTime, endTime);
                    }
                });


                function timespan3(st, ed) {

                    var timeStart = new Date("01/01/2007 " + st);
                    var timeEnd = new Date("01/01/2007 " + ed);

                    var hh = timeStart.getHours();
                    var mn = timeStart.getMinutes();
                    var ss = timeStart.getSeconds();

                    var hh2 = timeEnd.getHours();
                    var mn2 = timeEnd.getMinutes();
                    var ss2 = timeEnd.getSeconds();

                    var h = hh2-hh;
                    var m = mn2-mn;
                    var s = ss2 - ss;
                        h = h >= 0 ? h : 0;
                        m = (h >= 0 && m >= 0) ? m : 0;
                        s = (h >=0 && m >= 0 && s >= 0) ? s : 0;

                        h = h < 10 ? "0" + h : h; m = m < 10 ? '0' + m : m; s = s < 10 ? '0' + s : s; 
                    return (h + ":" + m + ":" + s);
                
                }

               
               



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
