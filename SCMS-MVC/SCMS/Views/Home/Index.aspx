<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SCMSMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContenct" runat="server">
    <link href="<%=ResolveUrl("~/") %>Slider/js-image-slider.css" rel="stylesheet" type="text/css" />
    <script src="<%=ResolveUrl("~/") %>Slider/js-image-slider.js" type="text/javascript"></script>
    <link href="<%=ResolveUrl("~/") %>Slider/generic.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="sliderFrame">
        <div id="slider">
            <img src="<%=ResolveUrl("~/") %>Slider/image-slider-1.png" alt=""/>
            <img src="<%=ResolveUrl("~/") %>Slider/image-slider-2.png" alt=""/>
            <img src="<%=ResolveUrl("~/") %>Slider/image-slider-3.png" alt=""/>
            <img src="<%=ResolveUrl("~/") %>Slider/image-slider-4.png" alt=""/>
            <img src="<%=ResolveUrl("~/") %>Slider/image-slider-5.png" alt=""/>
        </div>
        
    </div>
 </asp:Content>
