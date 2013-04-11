<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SCMSMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Chart Of Account Modification
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txt_Code").inputmask({ "mask": "99-999-9999-99999-99999-99999" });
            document.getElementById("txt_Code").focus();
        });

        function SaveRecord() {
            var lcnt_MessageBox = document.getElementById('MessageBox');
            var lcnt_txtSelectedCode = document.getElementById("txt_SelectedCode");
            var lcnt_txtOldCode = document.getElementById("txt_OldCode");
            var lcnt_txtNewCode = document.getElementById("txt_NewCode");

            var Url = document.getElementById('frm_ChartOfAccountModSetup').action;
            Url += "ChartOfAccountMod/SaveRecord?ps_OldCode=" + lcnt_txtOldCode.value + "&ps_NewCode=" + lcnt_txtNewCode.value;

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
                    }
                    else if (document.getElementById("SaveResult").value == "-1") {
                        lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Code already exists.</p>";
                        lcnt_MessageBox.setAttribute("class", "message error");

                    }
                    else {
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
            document.getElementById('txt_OldCode').value = ps_Code;

            scroll(0, 0);
        }
    </script>

    <form id="frm_ChartOfAccountModSetup" action='<%=Url.Content("~/") %>'>
    <input type="hidden" id="txt_SelectedCode" name="txt_SelectedCode" value="" />
    <div class="box round first fullpage grid">
        <h2>
            Chart of Account Modification Setup</h2>
        <div class="block">
            <div id="MessageBox" style="position: fixed; top: 0px; width: 95%;">
            </div>
            <div class="CustomCell" style="width: 97px; height: 30px;">
                Old Code</div>
            <div class="CustomCell" style="width: 270px; height: 30px;">
                <input type="text" class="CustomText" style="width: 240px;" id="txt_OldCode" name="txt_OldCode"
                    maxlength="50" />
                <script type="text/javascript">
                    $("#txt_OldCode").inputmask({ "mask": "99-999-9999-99999-99999-99999" });
                </script>
            </div>
            <div class="CustomCell" style="width: 97px; height: 30px;">
                New Code</div>
            <div class="CustomCell" style="width: 270px; height: 30px;">
                <input type="text" class="CustomText" style="width: 240px;" id="txt_NewCode" name="txt_NewCode"
                    maxlength="50" />
                <script type="text/javascript">
                    $("#txt_NewCode").inputmask({ "mask": "99-999-9999-99999-99999-99999" });
                </script>
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
