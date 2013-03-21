<%@ Page Language="C#" MasterPageFile="~/Views/Shared/SCMSMaster.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SCMS - User Menu Rights
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContenct" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="../../Widgets/jqwidgets/styles/jqx.base.css" type="text/css" />
    <script type="text/javascript" src="../../Widgets/scripts/gettheme.js"></script>
    <script type="text/javascript" src="../../Widgets/scripts/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" src="../../Widgets/jqwidgets/jqxcore.js"></script>
    <script type="text/javascript" src="../../Widgets/jqwidgets/jqxbuttons.js"></script>
    <script type="text/javascript" src="../../Widgets/jqwidgets/jqxscrollbar.js"></script>
    <script type="text/javascript" src="../../Widgets/jqwidgets/jqxpanel.js"></script>
    <script type="text/javascript" src="../../Widgets/jqwidgets/jqxtree.js"></script>
    <script type="text/javascript" src="../../Widgets/jqwidgets/jqxcheckbox.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            SetTree();
        });
        function GetUserMenu(value) {
            var Url = document.getElementById('frm_UserMenuSetup').action;
            Url += "UserRightsSetup/GetUserMenus?GroupId=" + value;
            $.ajax({
                type: "GET",
                url: Url,
                success: function (response) {
                    html = response;
                    $("#TreeContainer").html(response);
                    SetTree();
                },
                error: function (rs, e) {

                }
            });
        }
        function SaveRecord() {
            var lcnt_MessageBox = document.getElementById('MessageBox');
            var UserMenuIds = "";
            var spanElements = document.getElementsByClassName('jqx-checkbox-check-checked');
            for (var index = 0; index < spanElements.length; index++) {
                if (UserMenuIds.length > 0) {
                    UserMenuIds += "," + spanElements[index].parentNode.parentNode.parentNode.parentNode.id;
                }
                else {
                    UserMenuIds += spanElements[index].parentNode.parentNode.parentNode.parentNode.id;
                }
            }
            var Url = document.getElementById('frm_UserMenuSetup').action;
            Url += "UserRightsSetup/SaveUserRights?GroupId=" + document.getElementById('ddl_UserGroups').value + "&UserMenus=" + UserMenuIds;
            document.getElementById("Waiting_Image").style.display = "block";
            document.getElementById("btn_Save").style.display = "none";
            $.ajax({
                type: "GET",
                url: Url,
                success: function (response) {
                    html = response;
                    FadeIn(lcnt_MessageBox);
                    if (response == "1") {
                        lcnt_MessageBox.innerHTML = "<h5>Success!</h5><p>Record saved successfully.</p>";
                        lcnt_MessageBox.setAttribute("class", "message success");
                    }
                    else {
                        lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Unable to save record.</p>";
                        lcnt_MessageBox.setAttribute("class", "message error");
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
        function SetTree() {
            // Create jqxTree 
            var theme = getDemoTheme();
            // create jqxTree
            $('#jqxTree').jqxTree({ height: '400px', hasThreeStates: true, checkboxes: true, width: '330px', theme: theme });
            $('#jqxCheckBox').jqxCheckBox({ width: '200px', height: '25px', checked: true, theme: theme });
            $('#jqxCheckBox').on('change', function (event) {
                var checked = event.args.checked;
                $('#jqxTree').jqxTree({ hasThreeStates: checked });
            });
        }
    </script>
    <form id="frm_UserMenuSetup" action='<%=Url.Content("~/") %>'>
    <div class="box round first fullpage grid" style="overflow: auto;">
        <h2>
            User Menu Rights Setup</h2>
        <div class="block">
            <div id="MessageBox">
            </div>
            <div id='jqxWidget' style="overflow: auto; clear: both; margin-bottom: 5px;">
                <div class="CustomCell" style="width: 260px; height: 0px">
                    Select User Group</div>
                <div class="CustomCell" style="width: 720px; height: 0px">
                    Select Menu Options</div>
                <div style="clear: both">
                </div>
                <div style="margin-top: 15px;">
                    <div style='float: left;'>
                        <%=Html.DropDownList("ddl_UserGroups", null, new { @style = "width:259px;", @onchange = "GetUserMenu(this.value)" })%>
                    </div>
                    <div id="TreeContainer" style="float: left; margin-left: 5px;">
                        <%Html.RenderPartial("TreeView"); %>
                    </div>
                </div>
            </div>
            <div style="clear: both; border-bottom: 1px solid #ccc; margin-bottom: 5px;">
            </div>
            <div style="float: right">
                <div style="float: left; margin-right: 5px;">
                    <input id="btn_Save" type="button" value="Save" class="btn btn-blue" onclick="SaveRecord();"
                        style="width: 90px; height: 35px; padding-top: 5px; color: White; font-weight: bold;
                        font-size: 11pt;" />
                    <img alt="" id="Waiting_Image" src="../../img/Ajax_Loading.gif" style="display: none;
                        margin-left: 10" /></div>
                <%--<div style="float: left;">
                    <input type="button" value="Reset" class="btn btn-grey" onclick="ResetForm();" style="width: 90px;
                        height: 35px; padding-top: 5px; color: White; font-weight: bold; font-size: 11pt;" />
                </div>--%>
            </div>
        </div>
    </div>
    </form>
</asp:Content>
