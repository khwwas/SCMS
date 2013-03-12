<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SCMSMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Chart Of Account Setup
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txt_Code").inputmask({ "mask": "99-999-999-9999-9999-9999" });
            document.getElementById("txt_Code").focus();
        });

        function SaveRecord() {
            var lcnt_MessageBox = document.getElementById('MessageBox');
            var lcnt_txtSelectedCode = document.getElementById("txt_SelectedCode");
            var lcnt_txtCode = document.getElementById("txt_Code");
            var lcnt_Level = document.getElementById('txt_Level');
            var lcnt_BudgetLevel = document.getElementById('txt_BudgetLevel');
            var lcnt_Active = document.getElementById('chk_Active');
            var lcnt_txtTitle = document.getElementById('txt_Title');
            var lcnt_TypeGroup = document.getElementById('rdo_Group');
            var lcnt_TypeDetail = document.getElementById('rdo_Detail');
            var lcnt_Nature = document.getElementById('ddl_Nature');
            var lcnt_AccountNature = document.getElementById('ddl_AccNature');
            var li_Type;

            if (lcnt_txtTitle.value == "") {
                FadeIn(lcnt_MessageBox);
                lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Please! enter title</p>";
                lcnt_MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(lcnt_MessageBox);
                lcnt_txtTitle.focus();
                return;
            }
            else {
                var Url = document.getElementById('frm_ChartOfAccountSetup').action;

                if (lcnt_TypeGroup.checked == true) {
                    li_Type = 1;
                }
                else {
                    li_Type = 2;
                }
                var Active = 0;
                if (lcnt_Active.checked = true) {
                    Active = 1;
                }
                Url += "ChartOfAccount/SaveRecord?ps_Id=" + lcnt_txtSelectedCode.value + "&ps_Code=" + lcnt_txtCode.value + "&ps_Title=" + lcnt_txtTitle.value + "&pi_Level=" + lcnt_Level.value +
                                       "&pi_BudgetLevel=" + lcnt_BudgetLevel.value + "&pi_Active=" + Active + "&pi_Type=" + li_Type + "&ps_Nature=" + lcnt_Nature.value +
                                       "&ps_AccountNature=" + lcnt_AccountNature.value;

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
                        lcnt_MessageBox.innerHTML = "<h5>Success!</h5><p>Record saved successfully.</p>";
                        lcnt_MessageBox.setAttribute("class", "message success");
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
            document.getElementById('txt_Code').value = "";
            document.getElementById('txt_Title').value = "";
        }
        function EditRecord(Id, ps_Code) {
            document.getElementById('txt_SelectedCode').value = Id;
            document.getElementById('txt_Code').value = ps_Code
            document.getElementById('txt_Title').value = document.getElementById('txt_Title' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            if (document.getElementById('ChrtAcc_Level' + Id).value == "") {
                document.getElementById('txt_Level').value = "1";
            }
            else {
                document.getElementById('txt_Level').value = document.getElementById('ChrtAcc_Level' + Id).value;
            }
            if (document.getElementById('ChrtAcc_BudgetLevel' + Id).value == "") {
                document.getElementById('txt_BudgetLevel').value = "1";
            }
            else {
                document.getElementById('txt_BudgetLevel').value = document.getElementById('ChrtAcc_BudgetLevel' + Id).value;
            }
            if (document.getElementById('ChrtAcc_Active' + Id).value == "1") {
                document.getElementById('chk_Active').checked = true;
            }
            else {
                document.getElementById('chk_Active').checked = false;
            }
            if (document.getElementById('ChrtAcc_Type' + Id).value == "1") {
                document.getElementById('rdo_Group').checked = true;
                document.getElementById('rdo_Detail').checked = false;
            }
            else {
                document.getElementById('rdo_Detail').checked = true;
                document.getElementById('rdo_Group').checked = false;
            }
            //document.getElementById('chk_Active').value = document.getElementById('ChrtAcc_Active' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            //document.getElementById('rdo_Group').value = document.getElementById('ChrtAcc_Type' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            if (document.getElementById('Natr_Id' + Id).value == "") {
                document.getElementById('ddl_Nature').value = "00001";
            }
            else {
                document.getElementById('ddl_Nature').value = document.getElementById('Natr_Id' + Id).value;
            }
            if (document.getElementById('AccNatr_Id' + Id).value == "") {
                document.getElementById('ddl_AccNature').value = "00001";
            }
            else {
                document.getElementById('ddl_AccNature').value = document.getElementById('AccNatr_Id' + Id).value;
            }
            document.getElementById('txt_Title').value = document.getElementById('txt_Title' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            scroll(0, 0);
        }
        function DeleteRecord(Id) {
            if (confirm("Do you really want to delete this record")) {
                var lcnt_MessageBox = document.getElementById('MessageBox');
                var Url = document.getElementById('frm_ChartOfAccountSetup').action;

                Url += "ChartOfAccount/DeleteRecord?_pId=" + Id;
                $.ajax({
                    type: "GET",
                    url: Url,
                    success: function (response) {
                        html = response;
                        $("#GridContainer").html(response);
                        SetGrid();
                        ResetForm();
                        FadeIn(lcnt_MessageBox);
                        lcnt_MessageBox.innerHTML = "<h5>Success!</h5><p>Record deleted successfully.</p>";
                        lcnt_MessageBox.setAttribute("class", "message success");
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

        function Open() {
            $('#popup').lightbox_me({
                centered: true,
                closeClick: false,
                closeEsc: false,
                closeSelector: ".btn-red"
            });
            e.preventDefault();
        }

        function SetLevel() {
            var Level = document.getElementById("txt_Level");
            var Code = document.getElementById("txt_Code");
        }

    </script>
    <form id="frm_ChartOfAccountSetup" action='<%=Url.Content("~/") %>'>
    <input type="hidden" id="txt_SelectedCode" name="txt_SelectedCode" value="" />
    <div class="box round first fullpage grid">
        <h2>
            Chart of Account Setup</h2>
        <div class="block">
            <div id="MessageBox">
            </div>
            <div class="CustomCell" style="width: 97px; height: 30px;">
                Code</div>
            <div class="CustomCell" style="width: 270px; height: 30px;">
                <input type="text" class="CustomText" style="width: 240px;" id="txt_Code" name="txt_Code"
                    maxlength="50" onblur="SetLevel()" />
                <script type="text/javascript">
                    $("#txt_Code").inputmask({ "mask": "99-999-999-9999-9999-9999" });
                </script>
            </div>
            <div class="CustomCell" style="width: 40px;">
                Level</div>
            <div class="CustomCell" style="width: 42px;">
                <input type="text" class="CustomText" id="txt_Level" name="txt_Level" maxlength="1" 
                    style="width: 35px; border-right: 0px;" value="1" disabled="disabled" />
            </div>
            <div class="CustomCell" style="border: 1px solid #ccc; width: 20px; height: 28px;
                margin-right: 12px;">
                <div style="background-image: url('../../img/ArrowUp.png'); background-position: center;
                    height: 14px; background-repeat: no-repeat; cursor: pointer;" onclick="ValueMinus('txt_Level');">
                    &nbsp;
                </div>
                <div style="background-image: url('../../img/ArrowDown.png'); background-position: center;
                    height: 14px; background-repeat: no-repeat; cursor: pointer;" onclick="ValuePlus('txt_Level');">
                    &nbsp;
                </div>
            </div>
            <div class="CustomCell" style="width: 90px;">
                Budget Level</div>
            <div class="CustomCell" style="width: 42px;">
                <input type="text" class="CustomText" id="txt_BudgetLevel" name="txt_BudgetLevel"
                    maxlength="2" style="width: 35px; border-right: 0px;" value="1" disabled="disabled" />
            </div>
            <div class="CustomCell" style="border: 1px solid #ccc; width: 20px; height: 28px;
                margin-right: 12px;">
                <div style="background-image: url('../../img/ArrowUp.png'); background-position: center;
                    height: 14px; background-repeat: no-repeat; cursor: pointer;" onclick="ValueMinus('txt_BudgetLevel');">
                    &nbsp;
                </div>
                <div style="background-image: url('../../img/ArrowDown.png'); background-position: center;
                    height: 14px; background-repeat: no-repeat; cursor: pointer;" onclick="ValuePlus('txt_BudgetLevel');">
                    &nbsp;
                </div>
            </div>
            <div class="CustomCell" style="width: 50px;">
                Active</div>
            <div class="CustomCell">
                <input type="checkbox" class="checkbox" checked="checked" id="chk_Active" name="chk_Active" />
            </div>
            <div class="Clear">
            </div>
            <div class="CustomCell" style="width: 100px; height: 30px">
                Title</div>
            <input type="text" class="CustomText" style="width: 940px;" id="txt_Title" name="txt_Title"
                maxlength="100" />
            <div class="Clear">
            </div>
            <div class="CustomCell" style="width: 100px; height: 30px">
                Type</div>
            <div class="CustomCell">
                <input type="radio" class="radio" style="width: 20px;" id="rdo_Group" name="rdo_Type"
                    value="1" checked="checked" />Group
                <input type="radio" class="radio" style="width: 20px;" id="rdo_Detail" name="rdo_Type"
                    value="2" />Detail
            </div>
            <div class="Clear">
            </div>
            <div class="CustomCell" style="width: 100px; height: 30px">
                Nature</div>
            <%= Html.DropDownList("ddl_Nature", null, new { style = "width:955px;" })%>
            <div class="Clear">
            </div>
            <div class="CustomCell" style="width: 100px; height: 30px">
                Account Nature</div>
            <%= Html.DropDownList("ddl_AccNature", null, new { style = "width:955px;" })%>
            <div class="Clear">
            </div>
            <div style="float: right; margin-bottom: 10px;">
                <div style="float: left; margin-right: 5px;">
                    <input id="btn_Save" type="button" value="Save" class="btn btn-blue" onclick="SaveRecord();"
                        style="width: 90px; height: 35px; padding-top: 5px; color: White; font-weight: bold;
                        font-size: 11pt;" />
                    <img alt="" id="Waiting_Image" src="../../img/Ajax_Loading.gif" style="display: none;
                        margin-left: 10" /></div>
                <div style="float: left; margin-right: 5px;">
                    <input id="btn_Import" type="button" value="Import" class="btn btn-blue" onclick="Open();"
                        style="width: 90px; height: 35px; padding-top: 5px; color: White; font-weight: bold;
                        font-size: 11pt;" />
                    <img alt="" id="Img1" src="../../img/Ajax_Loading.gif" style="display: none; margin-left: 10" /></div>
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
    <div id="popup" style="display: none; background: #FFF; border-radius: 5px 5px 5px 5px;
        -moz-border-radius: 5px 5px 5px 5px; width: auto; padding: 10px;">
        <form id="frm_FileUpload" name="frm_FileUpload" enctype="multipart/form-data" method="post"
        action="<%=Url.Content("~/ChartOfAccount/ImportData") %>">
        <div class="block">
            <div class="CustomCell" style="padding-top: 5px; width: 65px;">
                Select File</div>
            <div class="CustomCell" style="width: 223px;">
                <input type="file" class="CustomText" id="txt_File" name="txt_File" style="border-right: 0px;" />
            </div>
            <div class="Clear">
            </div>
            <div class="CustomCell" style="width: 286px;">
                <div style="width: auto; float: right; margin-left: 3px;">
                    <input id="btn_Cancel" type="button" value="Cancel" class="btn btn-red btn-small"
                        onclick="javascript:txt_File.value='';" style="width: 80px; height: 30px; padding-top: 5px;
                        color: White; font-weight: bold; font-size: 11pt;" />
                </div>
                <div style="width: auto; float: right;">
                    <input id="btn_Upload" type="button" value="Save" class="btn btn-blue" onclick="javascript:btn_Upload.style.display='none';Img2.style.display=''; frm_FileUpload.submit();"
                        style="width: 80px; height: 30px; padding-top: 5px; color: White; font-weight: bold;
                        font-size: 11pt;" />
                </div>
                <div style="width: auto; float: right;">
                    <img alt="" id="Img2" src="../../img/Ajax_Loading.gif" style="display: none; margin-left: 10px;" />
                </div>
            </div>
        </div>
        </form>
    </div>
</asp:Content>
