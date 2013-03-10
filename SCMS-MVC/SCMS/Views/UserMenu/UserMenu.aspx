<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SCMSMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SCMS - User Menu
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        //window.location = "../../JsTree/index.html";
        function SaveUserMenu() {
            var ddl_UserGroup = document.getElementById('ddl_UserGroup');

            if (ddl_UserGroup.value == 0) {
                FadeIn(MessageBox);
                MessageBox.innerHTML = "<h5>Error!</h5><p>Please select user group name.</p>";
                MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(MessageBox);
                ddl_UserGroup.focus();
                return;
            }
            else {
                var Url = document.getElementById('frm_UserSetup').action;
                Url += "User/SaveUser?Code=" + txt_SelectedCode.value + "&Company=" + ddl_Company.value + "&Location=" + ddl_Location.value + "&UserGroup=" + ddl_UserGroup.value + "&User=" + txt_User.value + "&Password=" + txt_Password.value;
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
                        FadeIn(MessageBox);
                        MessageBox.innerHTML = "<h5>Success!</h5><p>Record saved successfully.</p>";
                        MessageBox.setAttribute("class", "message success");
                        document.getElementById("Waiting_Image").style.display = "none";
                        document.getElementById("btn_Save").style.display = "block";
                        scroll(0, 0);
                        FadeOut(MessageBox);
                    },
                    error: function (rs, e) {
                        document.getElementById("Waiting_Image").style.display = "none";
                        document.getElementById("btn_Save").style.display = "block";
                    }
                });
            }
        }

        function SaveUserMenu(id) {

            var ddl_UserGroup = document.getElementById('ddl_UserGroup').value;
            var chk_box_menu = document.getElementById(id).value;
            alert(ddl_UserGroup);

            if (ddl_UserGroup.value == 0) {
                FadeIn(MessageBox);
                MessageBox.innerHTML = "<h5>Error!</h5><p>Please select user group name.</p>";
                MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(MessageBox);
                ddl_UserGroup.focus();
                return;
            } else {
                var Url = document.getElementById('frm_UserMenu').action;
                Url += "UserMenu/SaveUserMenu?UserMenu=" + chk_box_menu + "&UserGroup=" + ddl_UserGroup;
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
                        FadeIn(MessageBox);
                        MessageBox.innerHTML = "<h5>Success!</h5><p>Record saved successfully.</p>";
                        MessageBox.setAttribute("class", "message success");
                        document.getElementById("Waiting_Image").style.display = "none";
                        document.getElementById("btn_Save").style.display = "block";
                        scroll(0, 0);
                        FadeOut(MessageBox);
                    },
                    error: function (rs, e) {
                        document.getElementById("Waiting_Image").style.display = "none";
                        document.getElementById("btn_Save").style.display = "block";
                    }
                });
            }
        
        
        }

        function ResetForm() {
            var MessageBox = document.getElementById('MessageBox');
            MessageBox.removeAttribute("class");
            MessageBox.innerHTML = "";
            document.getElementById('txt_SelectedCode').value = "";
            var txt_User = document.getElementById('txt_User').value = "";
            var txt_Password = document.getElementById('txt_Password').value = "";
            var ddl_Location = document.getElementById('ddl_Location').value = "";
            var ddl_Company = document.getElementById('ddl_Company').value = "";
            var ddl_UserGroup = document.getElementById('ddl_UserGroup').value = "";

        }

        function EditRecord(Id) {
            document.getElementById('txt_SelectedCode').value = Id;
            document.getElementById('txt_User').value = document.getElementById('txt_User' + Id).innerHTML.trim().toString().replace("&nbsp", "");
            document.getElementById('ddl_Location').value = document.getElementById('ddl_Location' + Id).childNodes[1].value.trim().toString().replace("&nbsp", "");
            document.getElementById('ddl_Company').value = document.getElementById('ddl_Company' + Id).childNodes[1].value.trim().toString().replace("&nbsp", "");
            document.getElementById('ddl_UserGroup').value = document.getElementById('ddl_UserGroup' + Id).childNodes[1].value.trim().toString().replace("&nbsp", "");
            scroll(0, 0);
        }

        

    </script>
    <script type="text/javascript">
        //<!--
        $(document).ready(function () {
            $('#tabs').tabs({
                cookie: { expires: 30 }
            });
            $('.jquery').each(function () {
                eval($(this).html());
            });
            $('.button').button();
        });
        //-->
    </script>
    <form id="frm_UserMenu" action='<%=Url.Content("~/") %>'>
    <div class="box round first fullpage grid">
        <h2>
            User Menu</h2>
        <div class="block">
            <div id="MessageBox">
            </div>
            <div class="CustomCell">
                User Group Name</div>
            <div class="Clear">
            </div>
            <%List<SCMSDataLayer.DB.SECURITY_UserGroup> ListUserGroups = new List<SCMSDataLayer.DB.SECURITY_UserGroup>();
              ListUserGroups = new SCMSDataLayer.DALUserGroup().GetAllUserGroup().ToList();
            %>
            <select id="ddl_UserGroup" name="ddl_UserGroup" style="width: 314px;">
                <option value="">Select User Group</option>
                <%foreach (SCMSDataLayer.DB.SECURITY_UserGroup UserGroupRow in ListUserGroups)
                  { %>
                <option value='<%=UserGroupRow.UsrGrp_Id %>'>
                    <%=UserGroupRow.UsrGrp_Title%></option>
                <%} %>
            </select>
            <div class="Clear">
            </div>
            <div style="width: 500px;" id="tabs">
                <ul id="tree2">
                    <li>
                         <ul>
                            <li>
                                <% List<SCMSDataLayer.DB.Security_MenuOption> ListUserMenu = new List<SCMSDataLayer.DB.Security_MenuOption>();
                                   ListUserMenu = new SCMSDataLayer.DalUserMenu().GetAllUserMenu().ToList();
                                   List<SCMSDataLayer.DB.Security_UserRight> ListUserRight = new List<SCMSDataLayer.DB.Security_UserRight>();
                                   ListUserRight = new SCMSDataLayer.DalUserMenu().GetAllUserRights().ToList();
                                   if (ListUserMenu != null && ListUserMenu.Count > 0)
                                   {
                                       int CountVar = 1;
                                       foreach (SCMSDataLayer.DB.Security_MenuOption UserMenu in ListUserMenu)
                                       {
                                           var RootMenu = ListUserMenu.Where(m => m.Mnu_Id.Equals(UserMenu.Mnu_Level)).ToList();
                                           
                                           if(RootMenu != null)
                                           {
                                               Response.Write("<input type='checkbox' id=" + UserMenu.Mnu_Id + " value=" + UserMenu.Mnu_Id + " onclick='return SaveUserMenu(this.id);' /><label>" + UserMenu.Mnu_Description + "</label>");     
                                           }
                                           var FilteredUserRights = ListUserRight.Where(c => c.Mnu_Id.Equals(UserMenu.Mnu_Id)).SingleOrDefault();
                                           
                                           if (FilteredUserRights != null)
                                           {
                                               //Response.Write("<input type='checkbox' name=" + UserMenu.Mnu_Id + " id=" + UserMenu.Mnu_Id + " checked='checked' /><label>" + UserMenu.Mnu_Description + "</label>");
                                               var Child = UserMenu.Mnu_Id + '.' + CountVar;
                                               var ChildUserMenu = ListUserMenu.Where(c => c.Mnu_Level.Equals(Child)).SingleOrDefault();
                                               
                                               if (ChildUserMenu != null)
                                               { %>
                                <!-- <ul>
                                    <li>
                                        <input type="checkbox" /><label><%=ChildUserMenu.Mnu_Description %></label>
                                    </li>
                                </ul> -->
                                <%          }
                                           }
                                           else
                                           {
                                               //Response.Write("<input type='checkbox' name=" + UserMenu.Mnu_Id + " id=" + UserMenu.Mnu_Id + " /><label>" + UserMenu.Mnu_Description + "</label>");
                                           }  
                                %>
                            </li>
                            <% 
                                           CountVar++;
                                } 

                                   } %>
                            <!-- <li>
                                <input type="checkbox" /><label>Node 2</label>
                                <ul>
                                    <li>
                                        <input type="checkbox" /><label>Node 2.1</label></li>
                                    <li>
                                        <input type="checkbox" /><label>Node 2.2</label></li>
                                    <li>
                                        <input type="checkbox" /><label>Node 2.3</label></li>
                                </ul>
                            </li>-->
                        </ul>
                    </li>
                </ul>
                <script type="text/javascript">
                    $('#tree2').checkboxTree({
                        collapseImage: 'img/downArrow.gif',
                        expandImage: 'img/rightArrow.gif'
                    });
                </script>
            </div>
            <div style="float: right; margin-bottom: 10px;">
                <div style="float: left; margin-right: 5px;">
                    <input id="btn_Save" type="button" value="Save" class="btn btn-blue" onclick="SaveUser();"
                        style="width: 90px; height: 35px; padding-top: 5px; color: White; font-weight: bold;
                        font-size: 11pt;" />
                    <img alt="" id="Waiting_Image" src="../../img/Ajax_Loading.gif" style="display: none;
                        margin-left: 10" /></div>
                <div style="float: left;">
                    <input type="button" value="Reset" class="btn btn-grey" onclick="ResetForm();" style="width: 90px;
                        height: 35px; padding-top: 5px; color: White; font-weight: bold; font-size: 11pt;" />
                </div>
            </div>
            <hr />
           <%-- <div id="GridContainer">
                <%Html.RenderPartial("GridData");%>
            </div>--%>
        </div>
    </div>
    </form>
</asp:Content>
