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
        //        $(document).ready(function () {
        //            
        //        });

        function GetUserMenu(obj) {
            var Url = document.getElementById('frm_UserMenuSetup').action;
            Url += "UserRightsSetup/GetUserMenus?GroupId=" + obj.value;
            $.ajax({
                type: "GET",
                url: Url,
                success: function (response) {
                    html = response;
                    $("#jqxTree").html(response);
                    SetTree();
                },
                error: function (rs, e) {

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
            $("#jqxTree").jqxTree('selectItem', $("#home")[0]);
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
                <div style='float: left;'>
                    <div style='margin-left: 0px;'>
                        <div style='margin-top: 0px;'>
                            <div class="CustomCell" style="width: 70px; height: 30px">
                                User Group</div>
                            <%=Html.DropDownList("ddl_UserGroups", null, new { @style = "width:259px;", @onchange = "GetUserMenu(this)" })%>
                            <script type="text/javascript">
                                GetUserMenu(document.getElementById('ddl_UserGroups'));
                            </script>
                        </div>
                    </div>
                    <div id='jqxTree' style='margin-left: 0px;'>
                    </div>
                </div>
            </div>
            <hr />
            <div style="float: right">
                <div style="float: left; margin-right: 5px;">
                    <input id="btn_Save" type="button" value="Save" class="btn btn-blue" onclick="SaveRecord();"
                        style="width: 90px; height: 35px; padding-top: 5px; color: White; font-weight: bold;
                        font-size: 11pt;" />
                    <img alt="" id="Waiting_Image" src="../../img/Ajax_Loading.gif" style="display: none;
                        margin-left: 10" /></div>
                <div style="float: left;">
                    <input type="button" value="Reset" class="btn btn-grey" onclick="ResetForm();" style="width: 90px;
                        height: 35px; padding-top: 5px; color: White; font-weight: bold; font-size: 11pt;" />
                </div>
            </div>
        </div>
    </div>
    </form>
</asp:Content>
