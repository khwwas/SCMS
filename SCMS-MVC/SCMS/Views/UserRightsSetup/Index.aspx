<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SCMS - User Menu Rights
</asp:Content>--%>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">--%>
<!-- start checkboxTree configuration -->
<html>
<head>
    <script type="text/javascript" src="library/jquery-1.4.4.js"></script>
    <script type="text/javascript" src="library/jquery-ui-1.8.12.custom/js/jquery-ui-1.8.12.custom.min.js"></script>
    <link rel="stylesheet" type="text/css" href="library/jquery-ui-1.8.12.custom/css/smoothness/jquery-ui-1.8.12.custom.css" />
    <script type="text/javascript" src="jquery.checkboxtree.js"></script>
    <link rel="stylesheet" type="text/css" href="jquery.checkboxtree.css" />
    <!-- end checkboxTree configuration -->
</head>
<body>
    <div id="tabs">
        <div id="tabs-1">
            <ul id="tree1">
                <li>
                    <input type="checkbox"><label>Node 1</label>
                    <ul>
                        <li>
                            <input type="checkbox"><label>Node 1.1</label></li>
                    </ul>
                </li>
                <li>
                    <input type="checkbox"><label>Node 2</label>
                    <ul>
                        <li>
                            <input type="checkbox"><label>Node 2.1</label></li>
                        <li>
                            <input type="checkbox"><label>Node 2.2</label></li>
                    </ul>
                </li>
            </ul>
            <script type="text/javascript">
                $('#tree1').checkboxTree(); 
            </script>
        </div>
    </div>
</body>
</html>
<%--</asp:Content>--%>
