<%@ Page Language="C#" MasterPageFile="~/Views/Shared/SCMSMaster.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SCMS - User Rights Setup
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .Background
        {
            background-color: rgb(230,230,230);
        }
        #InnerTab a:hover
        {
            background: #F8F8F8 !important;
            color: Black !important;
        }
    </style>
    <% 
        List<SCMSDataLayer.DB.sp_GetUserListResult> users = new List<SCMSDataLayer.DB.sp_GetUserListResult>();
        users = new SCMSDataLayer.DALUser().GetAllData();
        List<SCMSDataLayer.DB.sp_GetUserMenuRightsResult> MenuRights = (List<SCMSDataLayer.DB.sp_GetUserMenuRightsResult>)ViewData["UserMenuRights"];
        List<SCMSDataLayer.DB.sp_GetUserLocationsByGroupIdResult> UserLocations = (List<SCMSDataLayer.DB.sp_GetUserLocationsByGroupIdResult>)ViewData["UserLocations"];
        List<SCMSDataLayer.DB.sp_GetUserVoucherTypesByGroupIdResult> UserVoucherTypes = (List<SCMSDataLayer.DB.sp_GetUserVoucherTypesByGroupIdResult>)ViewData["UserVoucherTypes"];
        List<SCMSDataLayer.DB.sp_GetUserChartOfAccountByGroupIdResult> UserChartOfAccountTypes = (List<SCMSDataLayer.DB.sp_GetUserChartOfAccountByGroupIdResult>)ViewData["UserChartOfAccount"];
        List<SCMSDataLayer.DB.sp_GetUserListResult> allUsers = (List<SCMSDataLayer.DB.sp_GetUserListResult>)ViewData["AllUsers"];
    %>
    <div class="box round first fullpage grid" style="overflow: auto;">
        <h2>
            User Rights Setup</h2>
        <div class="block">
            <div id="MessageBox">
            </div>
            <input type='hidden' id='HiddenUserId' value='' />
            <div id="LeftContainer" style="width: 25%; float: left;">
                <%=Html.DropDownList("ddl_UserGroups", null, new { @style = "width:330px;", @onchange = "FilterUsersByGroup(this.value,'1')" })%>
                <div style="clear: both;">
                </div>
                <div id="UserContainer" style="overflow: auto; height: 350px; width: 100%; border: 1px solid #ccc;">
                    <table id="UserGrid" class=" display" style="width: 100%; padding: 2px;">
                        <tbody>
                            <%foreach (SCMSDataLayer.DB.sp_GetUserListResult user in users)
                              { %>
                            <tr id='<%=user.User_Id%>|<%=user.UsrGrp_Id%>' class='odd gradeX' style='line-height: 15px;
                                cursor: pointer;'>
                                <td style="vertical-align: middle; width: 25%;">
                                    <%=user.User_Code %>
                                </td>
                                <td style="vertical-align: middle; width: 35%;">
                                    <%=user.User_Title %>
                                </td>
                                <td style="vertical-align: middle; width: 30%;">
                                    <%=user.UsrGrp_Title %>
                                </td>
                                <td style="vertical-align: middle; text-align: right; padding-right: 3px;">
                                </td>
                            </tr>
                            <%} %>
                        </tbody>
                    </table>
                </div>
            </div>
            <div id='RightContainer' style="float: left; border: 0px solid #ccc; margin-left: 10px;
                width: 74%;">
                <div id="InnerTab" class="tabbable">
                    <ul id="tabs" class="nav nav-tabs" data-tabs="tabs">
                        <li id="liMenuRights" class="active"><a href="#MenuContainer">Menu Rights</a></li>
                        <li id="liLocationRights"><a href="#LocationContainer">Locations</a></li>
                        <li id="liVoucherTypes"><a href="#VoucherTypeContainer">Voucher Types</a></li>
                        <li id="liChartOfAccount"><a href="#ChartOfAccountContainer">Chart Of Account</a></li>
                    </ul>
                </div>
                <div id="MenuContainer" style="height: 349px; overflow: auto; border: 1px solid #ccc;
                    margin-top: 5px;">
                    <%--<table id="MenuGrid" class="display" style="width: 100%; padding: 2px;">
                        <thead>
                            <tr class='odd gradeX' style='line-height: 15px; cursor: pointer; text-align: left;
                                background-color: #ccc;'>
                                <th style="vertical-align: middle; width: 30%; padding-left: 3px;">
                                    Menu Description
                                </th>
                                <th style="vertical-align: middle; width: 10%;">
                                    Is Allowed
                                </th>
                                <th style="vertical-align: middle; width: 10%;">
                                    Can Add
                                </th>
                                <th style="vertical-align: middle; width: 10%;">
                                    Can Edit
                                </th>
                                <th style="vertical-align: middle; width: 10%;">
                                    Can Delete
                                </th>
                                <th style="vertical-align: middle; width: 10%;">
                                    Can Print
                                </th>
                                <th style="vertical-align: middle; width: 10%;">
                                    Can Import
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <%int count = 0;
                              foreach (SCMSDataLayer.DB.sp_GetUserMenuRightsResult menuRight in MenuRights)
                              {
                                  count++;
                                  string node = menuRight.Mnu_Level.Contains(".") ? "Child" : "Parent";
                            %>
                            <tr class='odd gradeX' style='line-height: 15px; cursor: pointer;'>
                                <%if (node == "Parent")
                                  { %>
                                <th style='vertical-align: middle; text-align: left; padding-left: 3px;'>
                                    <%=menuRight.Mnu_Description%>
                                </th>
                                <%}
                                  else
                                  { %>
                                <td style='vertical-align: middle; padding-left: 50px;'>
                                    <%=menuRight.Mnu_Description%>
                                </td>
                                <%} %>
                                <td style='vertical-align: middle;'>
                                    <input type='checkbox' class='allowed' value='ChkDesc<%=menuRight.Mnu_Level %>' id='ChkDesc<%=menuRight.Mnu_Id %>'
                                        <%=menuRight.SelectedMenu == 0 ? "" : "checked='checked'" %> />
                                </td>
                                <%if (menuRight.Mnu_Level == "3" || menuRight.Mnu_Level.Contains("3."))
                                  { %>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <%}
                                  else
                                  { %>
                                <td style='vertical-align: middle;'>
                                    <input type='checkbox' class='add' value='ChkAdd<%=menuRight.Mnu_Level %>' id='ChkAdd<%=menuRight.Mnu_Id %>'
                                        <%=menuRight.CanAdd == false ? "" : "checked='checked'" %> />
                                </td>
                                <td style='vertical-align: middle;'>
                                    <input type='checkbox' class='edit' value='ChkEdit<%=menuRight.Mnu_Level %>' id='ChkEdit<%=menuRight.Mnu_Id %>'
                                        <%=menuRight.CanEdit == false ? "" : "checked='checked'" %> />
                                </td>
                                <td style='vertical-align: middle;'>
                                    <input type='checkbox' class='delete' value='ChkDelete<%=menuRight.Mnu_Level %>'
                                        id='ChkDelete<%=menuRight.Mnu_Id %>' <%=menuRight.CanDelete == false ? "" : "checked='checked'" %> />
                                </td>
                                <td style='vertical-align: middle;'>
                                    <input type='checkbox' class='print' value='ChkPrint<%=menuRight.Mnu_Level %>' id='ChkPrint<%=menuRight.Mnu_Id %>'
                                        <%=menuRight.CanPrint == false ? "" : "checked='checked'" %> />
                                </td>
                                <td style='vertical-align: middle;'>
                                    <input type='checkbox' class='import' value='ChkImport<%=menuRight.Mnu_Level %>'
                                        id='ChkImport<%=menuRight.Mnu_Id %>' <%=menuRight.CanImport == false ? "" : "checked='checked'" %> />
                                </td>
                                <%} %>
                            </tr>
                            <%} %>
                        </tbody>
                    </table>--%>
                </div>
                <div id="LocationContainer" style="height: 349px; overflow: auto; border: 1px solid #ccc;
                    margin-top: 5px; display: none;">
                    <%--<table id="LocationGrid" class="display" style="width: 100%; padding: 2px;">
                        <thead>
                            <tr class='odd gradeX' style='line-height: 15px; cursor: pointer; text-align: left;
                                background-color: #ccc;'>
                                <th style="vertical-align: middle; width: 90%; padding-left: 3px;">
                                    Location
                                </th>
                                <th style="vertical-align: middle; width: 10%;">
                                    Is Allowed
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <%foreach (SCMSDataLayer.DB.sp_GetUserLocationsByGroupIdResult location in UserLocations)
                              {
                            %>
                            <tr class='odd gradeX' style='line-height: 15px; cursor: pointer;'>
                                <td style='vertical-align: middle; width: 25%;'>
                                    <%=location.Loc_Title %>
                                </td>
                                <td style="vertical-align: middle;">
                                    <input type='checkbox' class='allowedloc' id='<%=location.Loc_Id %>' <%=location.SelectedLocation == "0" ? "" : "checked='checked'" %> />
                                </td>
                            </tr>
                            <%} %>
                        </tbody>
                    </table>--%>
                </div>
                <div id="VoucherTypeContainer" style="height: 349px; overflow: auto; border: 1px solid #ccc;
                    margin-top: 5px; display: none;">
                    <%-- <table id="VoucherTypeGrid" class="display" style="width: 100%; padding: 2px;">
                        <thead>
                            <tr class='odd gradeX' style='line-height: 15px; cursor: pointer; text-align: left;
                                background-color: #ccc;'>
                                <th style="vertical-align: middle; width: 90%; padding-left: 3px;">
                                    Voucher Type
                                </th>
                                <th style="vertical-align: middle; width: 10%;">
                                    Is Allowed
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <%foreach (SCMSDataLayer.DB.sp_GetUserVoucherTypesByGroupIdResult voucherType in UserVoucherTypes)
                              {
                            %>
                            <tr class='odd gradeX' style='line-height: 15px; cursor: pointer;'>
                                <td style='vertical-align: middle; width: 25%;'>
                                    <%=voucherType.VchrType_Title%>
                                </td>
                                <td style="vertical-align: middle;">
                                    <input type='checkbox' class='allowedVtype' id='<%=voucherType.VchrType_Id%>' <%=voucherType.SelectedVoucherType == "0" ? "" : "checked='checked'" %> />
                                </td>
                            </tr>
                            <%} %>
                        </tbody>
                    </table>--%>
                </div>
                <div id="ChartOfAccountContainer" style="height: 349px; overflow: auto; border: 1px solid #ccc;
                    margin-top: 5px; display: none;">
                    <%--<table id="ChartOfAccountGrid" class="display" style="width: 100%; padding: 2px;">
                        <thead>
                            <tr class='odd gradeX' style='line-height: 15px; cursor: pointer; text-align: left;
                                background-color: #ccc;'>
                                <th style="vertical-align: middle; width: 25%; padding-left: 3px;">
                                    Code
                                </th>
                                <th style="vertical-align: middle; width: 25%; padding-left: 3px;">
                                    Chart Of Account
                                </th>
                                <th style="vertical-align: middle; width: 25%; padding-left: 3px;">
                                    Type
                                </th>
                                <th style="vertical-align: middle; width: 25%;">
                                    <input type='checkbox' id='chk_AllCOA' style='margin-bottom: 5px; margin-right: 1px;
                                        margin-left: 4px;' />Is Allowed
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <%foreach (SCMSDataLayer.DB.sp_GetUserChartOfAccountByGroupIdResult coa in UserChartOfAccountTypes)
                              {
                            %>
                            <tr class='odd gradeX' style='line-height: 15px; cursor: pointer;'>
                                <td style='vertical-align: middle; width: 25%;'>
                                    <%=coa.ChrtAcc_CodeDisplay%>
                                </td>
                                <td style='vertical-align: middle; width: 25%;'>
                                    <%=coa.ChrtAcc_Title%>
                                </td>
                                <td style='vertical-align: middle; width: 25%;'>
                                    <%if (coa.ChrtAcc_Type == 1)
                                      {
                                          Response.Write("Group");
                                      }
                                      else
                                      {
                                          Response.Write("Detail");
                                      } %>
                                </td>
                                <td style="vertical-align: middle;">
                                    <input type='checkbox' class='allowedCOA' value='Chk_Coa<%=coa.ChrtAcc_Id %>' id='<%=coa.ChrtAcc_Id%>'
                                        <%=coa.SelectedChartOfAccount == "0" ? "" : "checked='checked'" %> />
                                </td>
                            </tr>
                            <%} %>
                        </tbody>
                    </table>--%>
                </div>
            </div>
            <div style="clear: both; border-bottom: 1px solid #ccc; margin-bottom: 5px; padding-top: 15px;">
            </div>
            <div style="float: right">
                <div style="float: left; margin-right: 5px;">
                    <input id="btn_Copy" type="button" value="Copy" class="btn btn-blue" onclick="CopyUserRights();"
                        style="width: 90px; height: 35px; padding-top: 5px; color: White; font-weight: bold;
                        font-size: 11pt;" />
                    <input id="btn_Save" type="button" value="Save" class="btn btn-blue" onclick="SaveRecord();"
                        style="width: 90px; height: 35px; padding-top: 5px; color: White; font-weight: bold;
                        font-size: 11pt;" />
                    <img alt="" id="Waiting_Image" src="../../img/Ajax_Loading.gif" style="display: none;
                        margin-left: 10" /></div>
            </div>
        </div>
    </div>
    <div id="popup" style="display: none; background: #FFF; border-radius: 5px 5px 5px 5px;
        -moz-border-radius: 5px 5px 5px 5px; width: 500px; padding: 10px;">
        <div id="PopUpContent">
            <div style="text-align: center; border-bottom: 1px solid #ccc;">
                <h6>
                    Select user to copy rights from the previous selected user</h6>
            </div>
            <table id="CopyUsers" class=" display" style="width: 100%; padding: 2px; margin-bottom: 5px;">
                <tbody>
                    <%foreach (SCMSDataLayer.DB.sp_GetUserListResult user in allUsers)
                      { %>
                    <tr id='<%=user.User_Id%>' class='odd gradeX' style='line-height: 15px; cursor: pointer;'
                        onclick='SaveCopyRights(this.id);'>
                        <td style="vertical-align: middle; width: 25%;">
                            <%=user.User_Code %>
                        </td>
                        <td style="vertical-align: middle; width: 35%;">
                            <%=user.User_Title %>
                        </td>
                        <td style="vertical-align: middle; width: 30%;">
                            <%=user.UsrGrp_Title %>
                        </td>
                        <td style="vertical-align: middle; text-align: right; padding-right: 3px;">
                        </td>
                    </tr>
                    <%} %>
                </tbody>
            </table>
            <input type="hidden" id="txt_CurrentTab" value="Menus" />
            <div style="width: auto; float: right; margin-right: 5px;">
                <input id="btn_Close" type="button" value="Cancel" class="btn btn-red" style="width: 80px;
                    height: 30px; padding-top: 5px; color: White; font-weight: bold; font-size: 11pt;" />
            </div>
        </div>
        <div style="display: none; text-align: center;" id="wait">
            <h4>
                Your request is in progress, Please Wait....</h4>
            <img src="../../img/ajax-loader.gif" width="120px" />
        </div>
    </div>
    <script type="text/javascript">
        function CopyUserRights() {
            $('#popup').lightbox_me({
                centered: true,
                closeClick: false,
                closeEsc: false,
                closeSelector: ".btn-red"
            });
            e.preventDefault();
        }
        //SetClass();
        $(document).ready(function () {
            FilterUsersByGroup($("#ddl_UserGroups").val(), '1');
            //SetDefault();
        });

        function FilterUsersByGroup(value, selected) {
            $("#HiddenUserId").val("");
            var Url = "../UserRightsSetup/GetUsersByGroupId?GroupId=" + value + "&selected=" + selected;
            $.ajax({
                type: "GET",
                url: Url,
                success: function (response) {
                    $("#UserContainer").html(response);
                    SetClass();
                    SetDefault();
                },
                error: function (rs, e) {

                }
            });
        }

        function SetClass() {
            $("#UserGrid tr").click(function () {
                $("#UserGrid tr").removeClass("Background");
                $(this).addClass("Background");
                $("#HiddenUserId").val(this.id);

                var Arr = $("#HiddenUserId").val().split('|');
                UserId = Arr[0];
                GroupId = Arr[1];

                var Url = "../UserRightsSetup/GetUserMenus?GroupId=" + GroupId + "&UserId=" + UserId;
                $.ajax({
                    type: "GET",
                    url: Url,
                    success: function (response) {
                        $("#MenuContainer").html(response);
                        SelectAll();
                        //SelectAllCOA();
                        GetUserLocations(GroupId, UserId);
                        GetUserVoucherTypes(GroupId, UserId);
                        GetChartOfAccounts(GroupId, UserId);
                    },
                    error: function (rs, e) {

                    }
                });

            });
        }

        function SetDefault() {

            $("#HiddenUserId").val($("#UserGrid tr")[0].id);

            var Arr = $("#HiddenUserId").val().split('|');
            UserId = Arr[0];
            GroupId = Arr[1];

            var Url = "../UserRightsSetup/GetUserMenus?GroupId=" + GroupId + "&UserId=" + UserId;
            $.ajax({
                type: "GET",
                url: Url,
                success: function (response) {
                    $("#MenuContainer").html(response);
                    SelectAll();
                    //SelectAllCOA();
                    GetUserLocations(GroupId, UserId);
                    GetUserVoucherTypes(GroupId, UserId);
                    GetChartOfAccounts(GroupId, UserId);
                },
                error: function (rs, e) {

                }
            });

        }

        function GetUserLocations(GroupId, UserId) {
            var Url = "../UserRightsSetup/GetUserLocations?GroupId=" + GroupId + "&UserId=" + UserId;
            $.ajax({
                type: "GET",
                url: Url,
                success: function (response) {
                    $("#LocationContainer").html(response);
                },
                error: function (rs, e) {

                }
            });
        }

        function GetUserVoucherTypes(GroupId, UserId) {
            var Url = "../UserRightsSetup/GetUserVoucherTypes?GroupId=" + GroupId + "&UserId=" + UserId;
            $.ajax({
                type: "GET",
                url: Url,
                success: function (response) {
                    $("#VoucherTypeContainer").html(response);
                },
                error: function (rs, e) {

                }
            });
        }

        function GetChartOfAccounts(GroupId, UserId) {
            var Url = "../UserRightsSetup/GetUserChartOfAccounts?GroupId=" + GroupId + "&UserId=" + UserId;
            $.ajax({
                type: "GET",
                url: Url,
                success: function (response) {
                    $("#ChartOfAccountContainer").html(response);
                },
                error: function (rs, e) {

                }
            });
        }

        function SelectAll() {
            $("input[type=checkbox]").each(function () {
                $(this).click(function () {
                    if ($(this).val().indexOf('.') == -1) {
                        var val = 'input[value^="' + $(this).val() + '."]';
                        $(val).prop('checked', this.checked);
                    }
                });
            });
        }
        SelectAll();

        function SelectAllCOA(obj) {
            // alert(obj.checked);
            $(".allowedCOA").prop('checked', obj.checked);
        }
        //SelectAllCOA();

        function SaveRecord() {

            var lcnt_MessageBox = document.getElementById('MessageBox');
            var UserMenuIds = "";
            var GroupId = "";
            var UserId = "";
            $(".allowed:checked").each(function () {

                var Id = this.id.replace("ChkDesc", "");
                var CanAdd = $("#ChkAdd" + Id).is(":checked");
                var CanEdit = $("#ChkEdit" + Id).is(":checked");
                var CanDelete = $("#ChkDelete" + Id).is(":checked");
                var CanImport = $("#ChkImport" + Id).is(":checked");
                var CanPrint = $("#ChkPrint" + Id).is(":checked");

                if (UserMenuIds != "") {
                    UserMenuIds += "," + Id + "~" + CanAdd + "~" + CanEdit + "~" + CanDelete + "~" + CanPrint + "~" + CanImport;
                }
                else {
                    UserMenuIds += Id + "~" + CanAdd + "~" + CanEdit + "~" + CanDelete + "~" + CanPrint + "~" + CanImport;
                }

            });
            if ($("#HiddenUserId").val() != "") {
                var Arr = $("#HiddenUserId").val().split('|');
                UserId = Arr[0];
                GroupId = Arr[1];
            }
            var Url = "../UserRightsSetup/SaveUserRights?GroupId=" + GroupId + "&UserId=" + UserId + "&isGroup=false&UserMenus=" + UserMenuIds;
            //alert(Url);
            //return false;
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

        function SetLocations() {

            var lcnt_MessageBox = document.getElementById('MessageBox');
            var UserLocationsIds = "";
            var GroupId = "";
            var UserId = "";
            $(".allowedloc:checked").each(function () {

                var Id = this.id;

                if (UserLocationsIds != "") {
                    UserLocationsIds += "," + Id;
                }
                else {
                    UserLocationsIds += Id;
                }

            });
            if ($("#HiddenUserId").val() != "") {
                var Arr = $("#HiddenUserId").val().split('|');
                UserId = Arr[0];
                GroupId = Arr[1];
            }
            var Url = "../UserRightsSetup/SetUserLocations?GroupId=" + GroupId + "&UserId=" + UserId + "&isGroup=false&UserLocations=" + UserLocationsIds;
            //alert(Url);
            //return false;
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

        function SetVoucherTypes() {

            var lcnt_MessageBox = document.getElementById('MessageBox');
            var VoucherTypeIds = "";
            var GroupId = "";
            var UserId = "";
            $(".allowedVtype:checked").each(function () {

                var Id = this.id;

                if (VoucherTypeIds != "") {
                    VoucherTypeIds += "," + Id;
                }
                else {
                    VoucherTypeIds += Id;
                }

            });
            if ($("#HiddenUserId").val() != "") {
                var Arr = $("#HiddenUserId").val().split('|');
                UserId = Arr[0];
                GroupId = Arr[1];
            }
            var Url = "../UserRightsSetup/SetUserVoucherTypes?GroupId=" + GroupId + "&UserId=" + UserId + "&isGroup=false&UserVoucherTypes=" + VoucherTypeIds;
            //alert(Url);
            //return false;
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

        function SetChartOfAccounts() {

            var lcnt_MessageBox = document.getElementById('MessageBox');
            var ChartOfAccountIds = "";
            var GroupId = "";
            var UserId = "";
            $(".allowedCOA:checked").each(function () {

                var Id = this.id;

                if (ChartOfAccountIds != "") {
                    ChartOfAccountIds += "," + Id;
                }
                else {
                    ChartOfAccountIds += Id;
                }

            });
            if ($("#HiddenUserId").val() != "") {
                var Arr = $("#HiddenUserId").val().split('|');
                UserId = Arr[0];
                GroupId = Arr[1];
            }
            //var Url = "../UserRightsSetup/SetUserChartOfAccount?GroupId=" + GroupId + "&UserId=" + UserId + "&isGroup=false&UserChartOfAccounts=" + ChartOfAccountIds;
            //alert(Url);
            //return false;
            document.getElementById("Waiting_Image").style.display = "block";
            document.getElementById("btn_Save").style.display = "none";
            $.ajax({
                type: "POST",
                url: "UserRightsSetup/SetUserChartOfAccount",
                data: { GroupId: GroupId, UserId: UserId, isGroup: false, UserChartOfAccounts: ChartOfAccountIds },
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

        $("#InnerTab li").click(function () {
            $($(this).parent().find(".active").find("a").attr("href")).hide();
            $(this).parent().find(".active").removeClass("active");
            $(this).addClass("active");
            $($(this).parent().find(".active").find("a").attr("href")).show();

            $("#btn_Save").removeAttr("onclick");
            if (this.id == "liMenuRights") {
                $("#btn_Save").attr("onclick", "SaveRecord()");
                $("#txt_CurrentTab").val("Menus");
            }
            if (this.id == "liLocationRights") {
                $("#btn_Save").attr("onclick", "SetLocations()");
                $("#txt_CurrentTab").val("Locations");
            }
            if (this.id == "liVoucherTypes") {
                $("#btn_Save").attr("onclick", "SetVoucherTypes()");
                $("#txt_CurrentTab").val("VoucherTypes");
            }
            if (this.id == "liChartOfAccount") {
                $("#btn_Save").attr("onclick", "SetChartOfAccounts()");
                $("#txt_CurrentTab").val("ChartOfAccounts");
            }

        });

        function SaveCopyRights(NewUserId) {
            var lcnt_MessageBox = document.getElementById('MessageBox');
            var Arr = $("#HiddenUserId").val().split('|');
            var CurrentUserId = Arr[0];
            var GroupId = Arr[1];
            var SelectedTab = $("#txt_CurrentTab").val();

            var Url = "../UserRightsSetup/CopyUserRights?CurrentUserId=" + CurrentUserId + "&NewUserId=" + NewUserId + "&SelectedTab=" + SelectedTab;
            //return false;
            $("#PopUpContent").hide();
            $("#wait").show();

            $.ajax({
                type: "GET",
                url: Url,
                success: function (response) {
                    //html = response;
                    if (response == "1") {
                        $("#PopUpContent").show();
                        $("#wait").hide();
                        $("#popup").trigger('close');
                        FadeIn(lcnt_MessageBox);
                        //alert("OK");
                        lcnt_MessageBox.innerHTML = "<h5>Success!</h5><p>Your request has been processed successfully.</p>";
                        lcnt_MessageBox.setAttribute("class", "message success");
                    }
                    else {
                        $("#PopUpContent").show();
                        $("#wait").hide();
                        $("#popup").trigger('close');
                        FadeIn(lcnt_MessageBox);
                        lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Sorry, the system was unable to process your request.</p>";
                        lcnt_MessageBox.setAttribute("class", "message error");
                    }
                    scroll(0, 0);
                    FadeOut(lcnt_MessageBox);
                },
                error: function (rs, e) {
                    $("#PopUpContent").show();
                    $("#wait").hide();
                    $("#popup").trigger('close');
                    FadeIn(lcnt_MessageBox);
                    lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Sorry, the system was unable to process your request.</p>";
                    lcnt_MessageBox.setAttribute("class", "message error");
                    scroll(0, 0);
                    FadeOut(lcnt_MessageBox);
                }
            });
        }

    </script>
</asp:Content>
