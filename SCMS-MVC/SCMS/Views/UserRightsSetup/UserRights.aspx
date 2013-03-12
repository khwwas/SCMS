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
        });
    </script>
    <form id="frm_UserMenuSetup" action='<%=Url.Content("~/") %>'>
    <div class="box round first fullpage grid" style="overflow: auto;">
        <h2>
            User Setup</h2>
        <div class="block">
            <div id="MessageBox">
            </div>
            <div id='jqxWidget'>
                <div style='float: left;'>
                    <div id='jqxTree' style='float: left; margin-left: 20px;'>
                        <ul>
                            <li id='home'>Home</li>
                            <li item-checked='true' item-expanded='true'>Solutions
                                <ul>
                                    <li>Education</li>
                                    <li>Financial services</li>
                                    <li>Government</li>
                                    <li item-checked='false'>Manufacturing</li>
                                    <li>Solutions
                                        <ul>
                                            <li>Consumer photo and video</li>
                                            <li>Mobile</li>
                                            <li>Rich Internet applications</li>
                                            <li>Technical communication</li>
                                            <li>Training and eLearning</li>
                                            <li>Web conferencing</li>
                                        </ul>
                                    </li>
                                    <li>All industries and solutions</li>
                                </ul>
                            </li>
                            <li>Products
                                <ul>
                                    <li>PC products</li>
                                    <li>Mobile products</li>
                                    <li>All products</li>
                                </ul>
                            </li>
                            <li>Support
                                <ul>
                                    <li>Support home</li>
                                    <li>Customer Service</li>
                                    <li>Knowledge base</li>
                                    <li>Books</li>
                                    <li>Training and certification</li>
                                    <li>Support programs</li>
                                    <li>Forums</li>
                                    <li>Documentation</li>
                                    <li>Updates</li>
                                </ul>
                            </li>
                            <li>Communities
                                <ul>
                                    <li>Designers</li>
                                    <li>Developers</li>
                                    <li>Educators and students</li>
                                    <li>Partners</li>
                                    <li>By resource
                                        <ul>
                                            <li>Labs</li>
                                            <li>TV</li>
                                            <li>Forums</li>
                                            <li>Exchange</li>
                                            <li>Blogs</li>
                                            <li>Experience Design</li>
                                        </ul>
                                    </li>
                                </ul>
                            </li>
                            <li>Company
                                <ul>
                                    <li>About Us</li>
                                    <li>Press</li>
                                    <li>Investor Relations</li>
                                    <li>Corporate Affairs</li>
                                    <li>Careers</li>
                                    <li>Showcase</li>
                                    <li>Events</li>
                                    <li>Contact Us</li>
                                    <li>Become an affiliate</li>
                                </ul>
                            </li>
                        </ul>
                    </div>
                    <div style='margin-left: 60px; float: left;'>
                        <div style='margin-top: 10px;'>
                            <select>
                                <option>1</option>
                            </select>
                            <div class="Clear">
                            </div>
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
            </div>
        </div>
    </div>
    </form>
</asp:Content>
