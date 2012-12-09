<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Default.Master" CodeBehind="Index.aspx.cs"
    Inherits="rhmgWebsite.Web.Index" %>

<%@ Register Src="CustomControls/CustomerQuoteDisplay.ascx" TagName="CustomerQuoteDisplay"
    TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="Label1" runat="server" CssClass="title" Text="Welcome to the Rock Hard Music Group"
        ToolTip="Welcome to Rock Hard Music Group, Rock Hard Studios and Rock Hard Academy in Blackpool, Lancashire" />
    <asp:Table runat="server">
        <asp:TableRow runat="server">
            <asp:TableCell runat="server">
                <asp:Panel ID="Panel1" runat="server" CssClass="text">
                    Welcome to Rock Hard Studios Here you can find information about all aspects of
                    our business including our central Blackpool, Lancashire based recording and rehearsal
                    studio; the best in the North West of England. In addition to our recording and
                    rehearsal studio, we run an Academy of Excellence in Music, and also have various
                    custom Live Music Services.</asp:Panel>
                <a href="http://www.blackpoolgigs.co.uk" target="_bg">
                    <asp:Image ID="Image5" runat="server" ImageUrl="~/images/Blackpool Gigs Logo - The place to find out about gigs in Blackpool Lancashire.png"
                        AlternateText="Blackpool Gigs - The place to find out about gigs in Blackpool, Lancashire"
                        ToolTip="Blackpool Gigs - The place to find out about gigs in Blackpool, Lancashire" />
                </a>
                <asp:Panel ID="Panel4" runat="server" CssClass="text">
                    A free service that we are operating is blackpoolgigs.co.uk; <a href="http://www.blackpoolgigs.co.uk"
                        target="_bg" class="externalLink">the best place to find out about gigs in Blackpool</a>.
                    We aim to put, for free, a listing of every gig and musical event that is occuring
                    within Blackpool. This is only one of many ways in which we are showing our determination
                    to improve the musical community in Blackpool.
                </asp:Panel>
                <a href="http://www.northwestacademy.co.uk/" target="_bg">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/products/north west academy dj production and tuition.png"
                        AlternateText="North West Academy - DJ Production and Tuition" ToolTip="North West Academy - DJ Production and Tuition" />
                </a>
                <asp:Panel ID="Panel6" runat="server" CssClass="text">
                    North West Academy is Blackpool and the Fylde's first specialist DJ and Music Production
                    Academy and is run right here from Rock Hard Studios. Click above to visit their
                    website and find out more about the courses they offer.
                </asp:Panel>
                <asp:Panel ID="Panel2" runat="server" CssClass="text">
                    The Rock Hard Music Group has over the last several years built up a reputation
                    for excellence and refusal to settle for second best. You will find this demonstrated
                    through our determination to provide the best possible service to our customers,
                    a fact reflected by
                    <asp:HyperLink ID="HyperLink5" runat="server" CssClass="externalLink" Text="what they say about us"
                        Target="_blank" NavigateUrl="http://www.freeindex.co.uk/more-reviews.htm?id=226367" />.
                </asp:Panel>
                <asp:Panel ID="Panel3" runat="server" CssClass="text">
                    We would love to hear from you so if you have any questions or specific requirements
                    regarding anything at all please do get in contact and we will be happy to help
                    you. Our studio, in central Blackpool, Lancashire, is open daily from 10am - 11pm
                    if you want to visit; we would be happy to get the kettle on and welcome you.
                </asp:Panel>
            </asp:TableCell>
            <asp:TableCell runat="server" Width="190px" VerticalAlign="Top">
                <div style="margin-bottom: 10px; font-size:large">
                    Plug-in of the moment
                </div>
                <div style="display:block; text-align: center; width: 190px;" id="pluginofthemoment">
                    <object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0" width="190" height="105" id="mini-guitar-tuner" align="middle">
                        <param name="allowScriptAccess" value="sameDomain" /><param name="allowFullScreen" value="false" />
                        <param name="movie" value="http://www.howtotuneaguitar.org/images/mini-guitar-tuner.swf" />
                        <param name="quality" value="high" /><param name="bgcolor" value="#ffffff" />
                        <embed src="http://www.howtotuneaguitar.org/images/mini-guitar-tuner.swf" quality="high" bgcolor="#ffffff" width="190" height="105" name="mini-guitar-tuner" align="middle" allowScriptAccess="sameDomain" allowFullScreen="false" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />
                    </object>
                    <span style="font-size: 9px; font-family:verdana, arial sans-serif">
                        <a href="http://www.howtotuneaguitar.org" target="_blank">www.howtotuneaguitar.org</a>
                     </span>
                 </div>
                 <a href="Plugins_and_resources.aspx" class="standardLinkInactive" title="Click for more Plugings and Resources for bands and solo artists">click for more plugins...</a>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:Table runat="server" CellPadding="0" CellSpacing="0" Width="100%">
        <asp:TableRow>
            <asp:TableCell VerticalAlign="Top">
                <asp:Label runat="server" Text="Andrew, Dan, Matt and Chris" CssClass="text"></asp:Label>
            </asp:TableCell>
            <asp:TableCell HorizontalAlign="Right">
                <uc1:CustomerQuoteDisplay runat="server" ID="CustomerQuoteDisplay1" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:Panel ID="bob" runat="server" CssClass="text">
        For your convenience, we have teamed up with a <b>highly reputable UK backing track
            supplier</b>, so you can quickly purchase a backing track to <b>rehearse at home</b>,
        before coming to the studio for your recording session or party. Please click <a
            href="Studios_recording_backing_tracks.aspx" class="standardLinkInactive" title="Click for high quality, low cost backing tracks - ideal for solo artists to practice or record to; Rock Hard Studios, Blackpool, Lancashire">
            here</a>.</asp:Panel>
    <asp:Panel ID="Panel5" runat="server" CssClass="text" Direction="LeftToRight" HorizontalAlign="Center">
        <a href="PopStarParties.aspx">
            <asp:Image runat="server" ImageUrl="~/images/products/Pop Star Parties - click here for more information.png"
                AlternateText="Pop Star Parties - click here for more information" ToolTip="Pop Star Parties - click here for more information :: record for fun at Rock Hard Studios, Blackpool, Lancashire" /></a>
        <a href="/Academy/index.html" target="_blank">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/products/Drum Lessons - click here for more information.png"
                AlternateText="Drum Lessons - click here for more information" ToolTip="Drum Lessons - click here for more information :: learn to drum with Rock Hard Academy, Blackpool, Lancashire" /></a>
        <a href="Studios_recording.aspx">
            <asp:Image ID="Image3" runat="server" ImageUrl="~/images/products/Pro Audio Recording - click here for more information.png"
                AlternateText="Pro Audio Recording - click here for more information" ToolTip="Pro Audio Recording - click here for more information :: bands or solo artists get professional quality recording at Rock Hard Studios, Blackpool, Lancashire" /></a>
        <a href="Studios_rehearsals.aspx">
            <asp:Image ID="Image4" runat="server" ImageUrl="~/images/products/Rehearsal Rooms - click here for more information.png"
                AlternateText="Rehearsal Rooms - click here for more information" ToolTip="Rehearsal Rooms - click here for more information :: bands or solo artists practice and rehearse in the best rooms in Blackpool, Lancashire" /></a>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="BottomBannerContentPlaceHolder"
    runat="server">
    <asp:Image runat="server" ImageUrl="~/images/banners/Recording Studio Control Surface Blackpool.jpg"
        AlternateText="Rock Hard Studios - Digidesion C24 Control Surface, Protools HD2 and KRK V8 Studio Monitors :: professional recording and rehearsals for bands and solo artists in Blackpool, Lancashire"
        ToolTip="Rock Hard Studios - Digidesion C24 Control Surface, Protools HD2 and KRK V8 Studio Monitors :: professional recording and rehearsals for bands and solo artists in Blackpool, Lancashire" />
</asp:Content>
<asp:Content ContentPlaceHolderID="AdContentPlaceHolder" runat="server">
    <script type="text/javascript"><!--
        google_ad_client = "pub-4956915789383422";
        /* rhmg home page 2 */
        google_ad_slot = "0549395024";
        google_ad_width = 728;
        google_ad_height = 90;
//-->
    </script>
    <script type="text/javascript" src="http://pagead2.googlesyndication.com/pagead/show_ads.js">
    </script>
</asp:Content>
