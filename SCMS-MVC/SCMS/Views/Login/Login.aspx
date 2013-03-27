<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>Financial Management System - v1.0</title>
    <link rel="stylesheet" href="<%=Url.Content("~/Content/Login.css")%>" type="text/css" />
    <script type="text/javascript" src="<%=Url.Content("~/Scripts/jquery-1.5.1.js")%>"></script>
    <script src="<%=Url.Content("~/Scripts/SCMS.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var lcnt_Username = document.getElementById("txt_UserName");
            lcnt_Username.focus();
            $("#txt_UserName").keydown(function (event) {
                if (event.keyCode == 13) {
                    ValidateUser();
                }
            });
            $("#txt_Password").keydown(function (event) {
                if (event.keyCode == 13) {
                    ValidateUser();
                }
            });
        });

        function ValidateUser() {
            var lcnt_MessageBox = document.getElementById('MessageBox');
            var lcnt_Username = document.getElementById("txt_UserName");
            var lcnt_Password = document.getElementById('txt_Password');
            if (lcnt_Username.value == "") {
                FadeIn(lcnt_MessageBox);
                lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Please enter username</p>";
                lcnt_MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(lcnt_MessageBox);
                lcnt_Username.focus();
                return;
            }

            if (lcnt_Password.value == "") {
                FadeIn(lcnt_MessageBox);
                lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Error! Please enter password</p>";
                lcnt_MessageBox.setAttribute("class", "message error");
                scroll(0, 0);
                FadeOut(lcnt_MessageBox);
                lcnt_Password.focus();
                return;
            }

            var Url = document.getElementById('frm_Login').action;
            Url += "Login/ValidateUser?ps_UserName=" + lcnt_Username.value + "&ps_Password=" + lcnt_Password.value;

            document.getElementById("Waiting_Image").style.display = "block";
            document.getElementById("btn_Login").style.display = "none";
            $.ajax({
                type: "GET",
                url: Url,
                success: function (response) {
                    html = response;
                    if (response == "0") {
                        FadeIn(lcnt_MessageBox);
                        lcnt_MessageBox.innerHTML = "<h5>Error!</h5><p>Please enter corrent user name and password</p>";
                        lcnt_MessageBox.setAttribute("class", "message error");
                        scroll(0, 0);
                        FadeOut(lcnt_MessageBox);
                        lcnt_Password.focus();
                        document.getElementById("Waiting_Image").style.display = "none";
                        document.getElementById("btn_Login").style.display = "block";
                    }
                    else {
                        location.href = "../Dashboard";
                    }
                },
                error: function (rs, e) {
                    document.getElementById("Waiting_Image").style.display = "none";
                    document.getElementById("btn_Login").style.display = "block";
                }
            });

        }
    </script>
</head>
<body class="no-side">
    <form id="frm_Login" action="<%= Url.Content("~/") %>">
    <div style="width: 620px; margin: 0 auto; height: 100px;">
        &nbsp;
        <div id="MessageBox">
        </div>
    </div>
    <div class="login-box" style="margin-top: 0px;">
        <div class="login-border">
            <div class="login-style">
                <div class="login-header">
                    <div class="logo clear">
                        <img src="<%=ResolveUrl("~/img/logo.png") %>" alt="" class="picture" />
                        <span class="title">Punjab Daanish Schools & Centers of Excellence Authority</span>
                    </div>
                </div>
                <form action="" method="post">
                <div class="login-inside">
                    <div class="login-data" style="padding-bottom: 68px;">
                        <div class="row clear">
                            <label for="user">
                                <b>Username:</b>
                            </label>
                            <%--<asp:TextBox ID="txt_UserName" runat="server" Text="" ></asp:TextBox>--%>
                            <input type="text" id="txt_UserName" />
                        </div>
                        <div class="row clear">
                            <label for="password">
                                <b>Password:</b>
                            </label>
                            <%--<asp:TextBox ID="txt_Password" TextMode="Password" runat="server" Text=""></asp:TextBox>--%>
                            <input type="password" id="txt_Password" />
                        </div>
                        <div class="Clear">
                        </div>
                        <div style="float: left; margin-left: 139px;">
                            <input id="btn_Login" type="button" value="Login" class="btn btn-blue" onclick="ValidateUser();"
                                style="width: 90px; height: 35px; padding-top: 5px; color: White; font-weight: bold;
                                font-size: 11pt" />
                            <img alt="" id="Waiting_Image" src="<%=Url.Content("~/img/Ajax_Loading.gif")%>" style="display: none;
                                margin-left: 10" />
                        </div>
                    </div>
                </div>
                </form>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
