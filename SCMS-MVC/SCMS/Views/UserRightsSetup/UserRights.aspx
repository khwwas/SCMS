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
    %>
    <div class="box round first fullpage grid" style="overflow: auto;">
        <h2>
            User Rights Setup</h2>
        <div class="block">
            <div id="MessageBox">
            </div>
            <input type='hidden' id='HiddenUserId' value='' />
            <div id="LeftContainer" style="width: 25%; float: left;">
                <%=Html.DropDownList("ddl_UserGroups", null,"Select User Group", new { @style = "width:330px;", @onchange = "FilterUsersByGroup(this.value)" })%>
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
                    <table id="MenuGrid" class="display" style="width: 100%; padding: 2px;">
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
                    </table>
                </div>
                <div id="LocationContainer" style="height: 349px; overflow: auto; border: 1px solid #ccc;
                    margin-top: 5px; display: none;">
                    <table id="LocationGrid" class="display" style="width: 100%; padding: 2px;">
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
                    </table>
                </div>
                <div id="VoucherTypeContainer" style="height: 349px; overflow: auto; border: 1px solid #ccc;
                    margin-top: 5px; display: none;">
                    <table id="VoucherTypeGrid" class="display" style="width: 100%; padding: 2px;">
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
                    </table>
                </div>
                <div id="ChartOfAccountContainer" style="height: 349px; overflow: auto; border: 1px solid #ccc;
                    margin-top: 5px; display: none;">
                    <table id="Table1" class="display" style="width: 100%; padding: 2px;">
                        <thead>
                            <tr class='odd gradeX' style='line-height: 15px; cursor: pointer; text-align: left;
                                background-color: #ccc;'>
                                <th style="vertical-align: middle; width: 90%; padding-left: 3px;">
                                    Chart Of Account
                                </th>
                                <th style="vertical-align: middle; width: 10%;">
                                    Is Allowed
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <%foreach (SCMSDataLayer.DB.sp_GetUserChartOfAccountByGroupIdResult coa in UserChartOfAccountTypes)
                              {
                            %>
                            <tr class='odd gradeX' style='line-height: 15px; cursor: pointer;'>
                                <td style='vertical-align: middle; width: 25%;'>
                                    <%if (coa.ChrtAcc_Level == 2)
                                      {
                                          coa.ChrtAcc_Title = "&nbsp; &nbsp; " + coa.ChrtAcc_Title;
                                      }
                                    %>
                                    <%else if (coa.ChrtAcc_Level == 3)
                                      {
                                          coa.ChrtAcc_Title = "&nbsp; &nbsp; &nbsp; &nbsp; " + coa.ChrtAcc_Title;
                                      }
                                    %>
                                    <%else if (coa.ChrtAcc_Level == 4)
                                      {
                                          coa.ChrtAcc_Title = "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; " + coa.ChrtAcc_Title;
                                      }
                                    %>
                                    <%else if (coa.ChrtAcc_Level == 5)
                                      {
                                          coa.ChrtAcc_Title = "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; " + coa.ChrtAcc_Title;
                                      }
                                    %>
                                    <%else if (coa.ChrtAcc_Level == 6)
                                      {
                                          coa.ChrtAcc_Title = "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; " + coa.ChrtAcc_Title;
                                      }
                                    %>
                                    <%else
                                      {
                                          coa.ChrtAcc_Title = "<b>" + coa.ChrtAcc_Title + "</b>";
                                      }
                                    %>
                                    <%=coa.ChrtAcc_Title%>
                                </td>
                                <td style="vertical-align: middle;">
                                    <input type='checkbox' class='allowedCOA' value='Chk_Coa<%=coa.ChrtAcc_Id %>' id='<%=coa.ChrtAcc_Id%>' <%=coa.SelectedChartOfAccount == "0" ? "" : "checked='checked'" %> />
                                </td>
                            </tr>
                            <%} %>
                        </tbody>
                    </table>
                </div>
            </div>
            <div style="clear: both; border-bottom: 1px solid #ccc; margin-bottom: 5px; padding-top: 15px;">
            </div>
            <div style="float: right">
                <div style="float: left; margin-right: 5px;">
                    <input id="btn_Save" type="button" value="Save" class="btn btn-blue" onclick="SaveRecord();"
                        style="width: 90px; height: 35px; padding-top: 5px; color: White; font-weight: bold;
                        font-size: 11pt;" />
                    <img alt="" id="Waiting_Image" src="../../img/Ajax_Loading.gif" style="display: none;
                        margin-left: 10" /></div>
            </div>
        </div>
    </div>
    <script type="text/javascript">

        SetClass();

        function FilterUsersByGroup(value) {
            $("#HiddenUserId").val("");
            var Url = "../UserRightsSetup/GetUsersByGroupId?GroupId=" + value;
            $.ajax({
                type: "GET",
                url: Url,
                success: function (response) {
                    $("#UserContainer").html(response);
                    SetClass();
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
                        GetUserLocations(GroupId, UserId);
                        GetUserVoucherTypes(GroupId, UserId);
                    },
                    error: function (rs, e) {

                    }
                });

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

        $("#InnerTab li").click(function () {
            $($(this).parent().find(".active").find("a").attr("href")).hide();
            $(this).parent().find(".active").removeClass("active");
            $(this).addClass("active");
            $($(this).parent().find(".active").find("a").attr("href")).show();

            $("#btn_Save").removeAttr("onclick");
            if (this.id == "liMenuRights") {
                $("#btn_Save").attr("onclick", "SaveRecord()");
            }
            if (this.id == "liLocationRights") {
                $("#btn_Save").attr("onclick", "SetLocations()");
            }
            if (this.id == "liVoucherTypes") {
                $("#btn_Save").attr("onclick", "SetVoucherTypes()");
            }

        });


    </script>
</asp:Content>
