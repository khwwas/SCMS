<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DashBoardMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SCMS - DashBoard
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
            background-position: center;
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
            Dashboard</h2>
        <div class="block" style="min-height: 410px; overflow: auto;">
            <div style="background-image: url('../img/GL.png');" class="ModuleBox" onclick="javascript:window.location='../Home'">
                <div class="ModuleTitle">
                    General Ledger</div>
            </div>
            <div class="ModuleBox" style="background-image: url('../img/TM.png'); background-position: top;">
                <div class="ModuleTitle">
                    Time Management</div>
            </div>
            <div class="ModuleBox" style="background-image: url('../img/MG.png'); background-position: top;">
                <div class="ModuleTitle">
                    Campus Management</div>
            </div>
            <div class="ModuleBox" style="background-image: url('../img/MG.png'); background-position: top;">
                <div class="ModuleTitle">
                    Module 4</div>
            </div>
            <div class="ModuleBox" style="background-image: url('../img/MG.png'); background-position: top;">
                <div class="ModuleTitle">
                    Module 5</div>
            </div>
            <div class="ModuleBox" style="background-image: url('../img/MG.png'); background-position: top;">
                <div class="ModuleTitle">
                    Module 6</div>
            </div>
            <div class="ModuleBox" style="background-image: url('../img/MG.png'); background-position: top;">
                <div class="ModuleTitle">
                    Module 7</div>
            </div>
            <div class="ModuleBox" style="background-image: url('../img/MG.png'); background-position: top;">
                <div class="ModuleTitle">
                    Module 8</div>
            </div>
            <div style="clear: both; height: 20px;">
            </div>
            <div class="ModuleBox" style="background-image: url('../img/MG.png'); background-position: top;">
                <div class="ModuleTitle">
                    Module 9</div>
            </div>
            <div class="ModuleBox" style="background-image: url('../img/MG.png'); background-position: top;">
                <div class="ModuleTitle">
                    Module 10</div>
            </div>
            <div class="ModuleBox" style="background-image: url('../img/MG.png'); background-position: top;">
                <div class="ModuleTitle">
                    Module 11</div>
            </div>
            <div class="ModuleBox" style="background-image: url('../img/MG.png'); background-position: top;">
                <div class="ModuleTitle">
                    Module 12</div>
            </div>
            <div class="ModuleBox" style="background-image: url('../img/MG.png'); background-position: top;">
                <div class="ModuleTitle">
                    Module 13</div>
            </div>
            <div class="ModuleBox" style="background-image: url('../img/MG.png'); background-position: top;">
                <div class="ModuleTitle">
                    Module 14</div>
            </div>
            <div class="ModuleBox" style="background-image: url('../img/MG.png'); background-position: top;">
                <div class="ModuleTitle">
                    Module 15</div>
            </div>
            <div class="ModuleBox" style="background-image: url('../img/MG.png'); background-position: top;">
                <div class="ModuleTitle">
                    Module 16</div>
            </div>
        </div>
    </div>
</asp:Content>
