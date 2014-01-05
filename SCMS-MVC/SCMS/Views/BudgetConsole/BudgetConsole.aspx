<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SCMSMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Budget Console
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
        });

        //        function EditRecord(Id) {
        //        }

        function BudgetConsole(locationId) {
            window.location = "../BudgetConsole?p_LocationId=" + locationId;
        }

        function DeleteRecord(Id) {
            if (confirm("Do you really want to cancel this record")) {
                var MessageBox = document.getElementById('MessageBox');
                var Url = document.getElementById('frm_BudgetConsole').action;
                Url += "BudgetConsole/DeleteRecordById?ps_Id=" + Id;
                $.ajax({
                    type: "GET",
                    url: Url,
                    success: function (response) {
                        html = response;
                        $("#GridContainer").html(response);

                        //ResetForm();
                        FadeIn(MessageBox);
                        MessageBox.innerHTML = "<h5>Success!</h5><p>Record cancelled successfully.</p>";
                        MessageBox.setAttribute("class", "message success");
                        scroll(0, 0);
                        FadeOut(MessageBox);
                        SetGrid();
                    },
                    error: function (rs, e) {
                        FadeIn(MessageBox);
                        MessageBox.innerHTML = "<h5>Error!</h5><p>An error occured in cancelling this record.</p>";
                        MessageBox.setAttribute("class", "message error");
                        scroll(0, 0);
                        FadeOut(MessageBox);
                        SetUserRights();
                    }
                });
            }
        }

        function CopyBudget(budgetId) {
            $('#popup').lightbox_me({
                centered: true,
                closeClick: false,
                closeEsc: false,
                closeSelector: ".btn-red"
            });
            e.preventDefault();
        }

    </script>
    <form id="frm_BudgetConsole" action='<%=Url.Content("~/") %>'>
    <div class="box round first fullpage grid">
        <h2>
            Budget Console</h2>
        <div class="block">
            <div id="MessageBox">
            </div>
            <div class="Clear">
            </div>
            <div id="GridContainer">
                <%Html.RenderPartial("GridData");%>
            </div>
        </div>
    </div>
    </form>
    <div id="popup" style="display: block; background: #FFF; border-radius: 5px 5px 5px 5px;
        -moz-border-radius: 5px 5px 5px 5px; width: 500px; padding: 10px;">
        <div id="PopUpContent">
            <div style="text-align: center; border-bottom: 1px solid #ccc;">
                <h6>
                    Copy Budget
                </h6>
            </div>
            <form method="post" action="../../budget/CopyBudget">
            <input type="hidden" id="MasterId" name="MasterId" value="" />
            <div class="CustomCell" style="width: 150px; height: 30px;">
                Applicable On</div>
            <div class="CustomCell" style="width: 300px; height: 30px;">
                <span>Budget</span><span><input type="radio" value="Budget" checked="checked" name="rdo_BudgetActual" /></span>
                <span>Actual</span><span><input type="radio" value="Actual" name="rdo_BudgetActual" /></span>
            </div>
            <div class="clear">
            </div>
            <div class="CustomCell" style="width: 150px; height: 30px;">
                Applicable %age</div>
            <div class="CustomCell" style="width: 300px; height: 30px;">
                <input type="text" class="CustomText" style="width: 50px;" name="percentage" />
                <span>Inflate</span><span><input type="radio" value="Inflate" checked="checked" name="rdo_InflateDeflate" /></span>
                <span>Deflate</span><span><input type="radio" value="Deflate" name="rdo_InflateDeflate" /></span>
            </div>
            <div style="width: auto; float: right; margin-right: 5px;">
                <input id="btn_Save" type="submit" value="Save" class="btn btn-blue" style="width: 80px;
                    height: 30px; padding-top: 5px; color: White; font-weight: bold; font-size: 11pt;" />
                <input id="btn_Close" type="button" value="Cancel" class="btn btn-red" style="width: 80px;
                    height: 30px; padding-top: 5px; color: White; font-weight: bold; font-size: 11pt;" />
            </div>
            </form>
        </div>
        <div style="display: none; text-align: center;" id="wait">
            <h4>
                Your request is in progress, Please Wait....</h4>
            <img alt="" src="../../img/ajax-loader.gif" width="120px" />
        </div>
        <div class="clear">
        </div>
    </div>
</asp:Content>
