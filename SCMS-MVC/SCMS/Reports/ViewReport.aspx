<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewReport.aspx.cs" Inherits="SCMS.Reports.ViewReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="~/Scripts/jquery-1.5.1.js"></script>
    <script src="~/Scripts/SCMS.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="MessageBox" runat="server">
        </div>
        <table border="0" style="width: 100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <CR:CrystalReportViewer ID="crvReports" runat="server" AutoDataBind="true" EnableDatabaseLogonPrompt="False"
                        EnableParameterPrompt="False" ToolPanelView="None" HasCrystalLogo="False" HasDrilldownTabs="False"
                        HasDrillUpButton="False" HasSearchButton="False" HasToggleGroupTreeButton="False"
                        HasToggleParameterPanelButton="False" PrintMode="ActiveX" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
