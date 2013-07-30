<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SCMSMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 99%; text-align: center; padding-top: 100px;">
        <img alt="" src="<%=ResolveUrl("~/") %>img/Home/Home2.png" />
    </div>
</asp:Content>
