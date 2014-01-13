<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SCMSMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- embedding SWF -->
    <script src="../../Slider/swfobject.js" type="text/javascript"></script>
    <script type="text/javascript">
        var flashvars = {};
        flashvars.xml_file = "../../Slider/flashmo_225_photo_list.xml";
        var params = {};
        params.wmode = "transparent";
        var attributes = {};
        attributes.id = "slider";
        swfobject.embedSWF("../../Slider/flashmo_225_bar_slider.swf", "flashmo_slider", "940", "400", "9.0.0", false, flashvars, params, attributes);
    </script>
    <!-- embedding SWF -->
    <div align="center">
        <div id="flashmo_slider">
            <a href="http://www.adobe.com/go/getflashplayer">
                <img src="http://www.adobe.com/images/shared/download_buttons/get_flash_player.gif"
                    alt="Get Adobe Flash player" />
            </a>
            <br />
            <br />
        </div>
    </div>
</asp:Content>
