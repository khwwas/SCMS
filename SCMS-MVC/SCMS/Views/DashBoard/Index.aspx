<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DashBoardMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SCMS - DashBoard
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box round first fullpage grid">
        <h2>
            Dashboard</h2>
        <div class="block" style="min-height: 410px;">
            <div style="width: 100px; height: 100px; float: left; border: 4spx inset #ccc; border-radius: 5px;
                -moz-border-radius: 5px; padding: 5px; background: #cccfff;">
                <div>
                    General Ledger</div>
            </div>
        </div>
    </div>
</asp:Content>
