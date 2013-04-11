<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DashBoardMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Dashboard
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .ModuleBox
        {
            width: 140px;
            height: 145px;
            float: left;
            border: 0px solid grey;
            border-radius: 8px;
            -moz-border-radius: 8px;
            background-repeat: no-repeat;
            background-position: top;
            padding-bottom: 8px;
            -webkit-box-shadow: 0 0 10px silver;
            -moz-box-shadow: 0 0 10px silver;
            box-shadow: 0 0 10px silver;
            cursor: pointer;
            margin-left: 20px;
        }
        .ModuleTitle
        {
            margin-top: 129px;
            font-weight: bold;
            font-size: 12px;
            text-align: center;
            border-top: 1px solid #ccc;
        }
        .ModuleTitle:hover, .ModuleBox:hover
        {
            color: grey;
        }
    </style>
    <div class="box round first fullpage grid">
        <h2>
            Application Dashboard</h2>
        <div class="block" style="min-height: 410px; overflow: auto;">
            <% 
                var modules = (List<SCMSDataLayer.DB.SETUP_Module>)ViewData["Modules"];
                if (modules != null && modules.Count > 0)
                {
                    if (modules.Count > 1)
                    {
                        foreach (SCMSDataLayer.DB.SETUP_Module mod in modules)
                        { 
            %>
            <div style="background-image: url('<%=mod.Mod_ImagePath%>');" class="ModuleBox" onclick="javascript:window.location='<%=mod.Mod_Url %>?ModId=<%=mod.Mod_Id %>&ModDesc=<%=modules[0].Mod_Desc %>&ModAbbr=<%=modules[0].Mod_Abbreviation %>'">
                <div class="ModuleTitle">
                    <%=mod.Mod_Abbreviation%></div>
            </div>
            <%}
                    }
                    else
                    {
                       
            %>
            <script type="text/javascript">
                window.location = '../Home?ModId=<%=modules[0].Mod_Id %>&ModDesc=<%=modules[0].Mod_Desc %>&ModAbbr=<%=modules[0].Mod_Abbreviation %>';
            </script>
            <%}
                }%>
        </div>
    </div>
</asp:Content>
