<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SCMSMaster.Master"
    Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home
    <link rel="stylesheet" type="text/css" href="<%=ResolveUrl("~/") %>Content/Slider/slicebox.css" />
    <script type="text/javascript" src="<%=ResolveUrl("~/") %>Scripts/jquery-ui-1.8.11.min.js"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/") %>Scripts/Slider/jquery.slicebox.js"></script>
    <script type="text/javascript" src="<%=ResolveUrl("~/") %>Scripts/Slider/modernizr.custom.46884.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <div class="wrapper">
            <ul id="sb-slider" class="sb-slider">
                <li><a href="" target="_blank">
                    <img src="<%=ResolveUrl("~/") %>img/Home/Home1.png" alt="image1" /></a>
                    <div class="sb-description">
                    </div>
                </li>
                <li><a href="" target="_blank">
                    <img src="<%=ResolveUrl("~/") %>img/Home/Home2.png" alt="image2" /></a>
                    <div class="sb-description">
                    </div>
                </li>
                <%--<li><a href="http://www.flickr.com/photos/strupler/2968114825" target="_blank">
                <img src="images/3.jpg" alt="image1" /></a>
                <div class="sb-description">
                    <h3>
                        Brave Astronaut</h3>
                </div>
            </li>
            <li><a href="http://www.flickr.com/photos/strupler/2968122059" target="_blank">
                <img src="images/4.jpg" alt="image1" /></a>
                <div class="sb-description">
                    <h3>
                        Affectionate Decision Maker</h3>
                </div>
            </li>
            <li><a href="http://www.flickr.com/photos/strupler/2969119944" target="_blank">
                <img src="images/5.jpg" alt="image1" /></a>
                <div class="sb-description">
                    <h3>
                        Faithful Investor</h3>
                </div>
            </li>
            <li><a href="http://www.flickr.com/photos/strupler/2968126177" target="_blank">
                <img src="images/6.jpg" alt="image1" /></a>
                <div class="sb-description">
                    <h3>
                        Groundbreaking Artist</h3>
                </div>
            </li>
            <li><a href="http://www.flickr.com/photos/strupler/2968945158" target="_blank">
                <img src="images/7.jpg" alt="image1" /></a>
                <div class="sb-description">
                    <h3>
                        Selfless Philantropist</h3>
                </div>
            </li>--%>
            </ul>
            <div id="shadow" class="shadow">
            </div>
            <div id="nav-arrows" class="nav-arrows">
                <a href="#">Next</a> <a href="#">Previous</a>
            </div>
            <div id="nav-dots" class="nav-dots">
                <span class="nav-dot-current"></span><span></span><span></span><span></span><span>
                </span><span></span><span></span>
            </div>
        </div>
        <!-- /wrapper -->
    </div>
</asp:Content>
