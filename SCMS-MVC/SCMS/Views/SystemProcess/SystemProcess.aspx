<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SCMSMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    System Processes
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">


        function SaveRecord() {
            var lcnt_MessageBox = document.getElementById('MessageBox');

            document.getElementById("Waiting_Image").style.display = "block";
            document.getElementById("btn_Save").style.display = "none";

            $.ajax({
                type: "POST",
                url: "SystemProcess/SaveRecord",
                //                data: { ps_Code: lcnt_txtSelectedCode.value, ps_Title: lcnt_txtTitle.value },
                success: function (response) {
                    html = response;
                    //                    $("#GridContainer").html(response);
                    //                    ResetForm();
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
                    //                    SetGrid();
                    scroll(0, 0);
                    FadeOut(lcnt_MessageBox);
                },
                error: function (rs, e) {
                    document.getElementById("Waiting_Image").style.display = "none";
                    document.getElementById("btn_Save").style.display = "block";
                    //                    SetUserRights();
                }
            });
        }
       
       
    </script>
    <form id="frm_SystemProcesses" action='<%=Url.Content("~/") %>'>
    <input type="hidden" id="SaveResult" value='<%=ViewData["SaveResult"] %>' />
    <div class="box round first fullpage grid">
        <h2>
            System Processes</h2>
        <div class="block">
            <div id="MessageBox">
            </div>
            <div class="CustomCell" style="width: 400px; height: 30px">
                <b>Press the button to correct all voucher numbers: </b>
            </div>
            <div style="float: right; margin-bottom: 10px;">
                <div style="float: left; margin-right: 5px;">
                    <input id="btn_Save" type="button" value="Voucher Code Correction" class="btn btn-blue"
                        onclick="SaveRecord();" style="width: 190px; height: 35px; padding-top: 5px;
                        color: White; font-weight: bold; font-size: 11pt;" />
                    <img alt="" id="Waiting_Image" src="../../img/Ajax_Loading.gif" style="display: none;
                        margin-left: 10" /></div>
            </div>
            <%-- <div class="Clear">
            </div>--%>
            <hr />
        </div>
    </div>
    </form>
</asp:Content>
