<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SCMSMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Budget Console
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
        });

        function EditRecord(Id) {
        }

        function VoucherEntryConsole(locationId, vchrtypId) {
            window.location = "../VoucherEntryConsole?p_LocationId=" + locationId + "&p_VoucherTypeId=" + vchrtypId;
        }

        function DeleteRecord(Id) {
            if (confirm("Do you really want to cancel this record")) {
                var MessageBox = document.getElementById('MessageBox');
                var Url = document.getElementById('frm_BudgetConsole').action;
                Url += "VoucherEntryConsole/DeleteRecordById?ps_Id=" + Id;
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
</asp:Content>
