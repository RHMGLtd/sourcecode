<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Studios.aspx.cs" Inherits="rhmgWebsite.Web.Studios"
    MasterPageFile="~/Default.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Table runat="server" Width="100%">
        <asp:TableRow>
            <asp:TableCell Width="200px">
                <asp:Label ID="Label1" runat="server" CssClass="title" Text="Rock Hard Studios" ToolTip="Read about Rock Hard Studios, Blackpool Lancashire; quality recording and rehearsals for bands and solo artists." />
            </asp:TableCell>
            <asp:TableCell HorizontalAlign="Right">
                <asp:Label runat="server" Text="Open every day 10am - 11pm" CssClass="bold"/>
            </asp:TableCell>
            <asp:TableCell Width="20px"></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:Panel ID="Panel1" runat="server" CssClass="text">
        Rock Hard Studios is a custom built recording and rehearsal studio in the centre
        of Blackpool, Lancashire. Built from scratch very close to Blackpool North railway
        station, our rooms are designed to enhance every aspect of your experience. Our
        engineers have over thirty years of experience within a studio environment and are
        conversant in many different genres of music</asp:Panel>
    <asp:Panel ID="Panel5" runat="server" CssClass="text">
        The Recording Studio is based around Pro Tools HD2 [<a href="http://www.soundonsound.com/sos/May02/articles/protoolshd.asp"
            class="externalLink" target="_blank">Sound on Sound review</a>] and a Digidesign
        C24 control surface. [<a href="http://www.soundonsound.com/sos/jun08/articles/digidesignc24.htm"
            class="externalLink" target="_blank">Sound on Sound review</a>]. We combine
        these industry-leading technologies with an amazing live room and our love for music.</asp:Panel>
    <asp:Panel ID="Panel6" runat="server" CssClass="text">
        Our Rehearsal Rooms are designed with you in mind and come as standard with a <a
            href="http://www.sonor.com/" target="_blank" class="externalLink">Sonor</a>
        drum kit, <a href="http://www.ashdownmusic.com/" target="_blank" class="externalLink">
            Ashdown</a> bass amps and one of a range of guitar amplifiers. They are ideal
        whether you are in a band or are a solo artist or DJ or are just looking for space
        to practice a loud instrument.
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" CssClass="text">
        Please use the links on the left to <a href="ExploreTheStudio.aspx" class="standardLinkInactive"
            title="Click to see pictures showing you around Rock Hard Studio's recording and rehearsal facility for bands and solo artists in Blackpool, Lancashire">
            explore</a> our facility, <a href="Studios_bookings.aspx" class="standardLinkInactive"
                title="Click to book into Rock Hard Studios, Blackpool, Lancashire - recording and rehearsals for bands and solo artists">
                make bookings</a>, <a href="Studios_theteam.aspx" class="standardLinkInactive" title="Click to meet the producers and engineers at Rock Hard Studios, Blackpool, Lancashire - rehearsal and recording for bands and solo artists">
                    meet our team</a> or <a href="Downloads.aspx" class="standardLinkInactive" title="Click here to listen to songs recorded in Rock Hard Studios, Blackpool, Lancashire's best Recording Studio. Also flyers and promotional material">
                        download</a> fact sheets about Rock Hard Studios.
    </asp:Panel>
    <asp:Panel ID="Panel7" runat="server" CssClass="text">
        We have also taken time to root out the best musical plugins available online. These
        can be found <a href="Plugins_and_resources.aspx" class="standardLinkInactive" title="Click for more Plugings and Resources for bands and solo artists">
            here</a>. We will update this page regularly, and if you find any you think
        are worth adding please <a href="Contact.aspx" class="standardLinkInactive" title="Click here for directions to Rock Hard Studios, Blackpool, Lancashire's best Recording and Rehearsal Studio for bands or solo artists">
            contact us</a> and we'll be happy to consider your recommendations. Additionally
        we have teamed up with Ameritx to provide you with professional quality backing
        tracks. You can find the embedded store by <a href="Studios_recording_backing_tracks.aspx"
            class="standardLinkInactive" title="Click for high quality, low cost backing tracks - ideal for solo artists to practice or record to; Rock Hard Studios, Blackpool, Lancashire">
            clicking through to it here</a>.
    </asp:Panel>
    <asp:Panel ID="Panel3" runat="server" CssClass="text">
        <asp:Label ID="Label2" runat="server" Text="Standard rates:" CssClass="bold" />
    </asp:Panel>
    <asp:Panel ID="Panel4" runat="server" CssClass="text">
        <asp:Table runat="server" Width="100%">
            <asp:TableRow>
                <asp:TableCell Width="50%">
                    Rehearsal :: £7ph. 6-11pm 4 hour minimum booking (£25)
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="BottomBannerContentPlaceHolder"
    runat="server">
    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/banners/Recording Studio Christmas Blackpool Shot 2.jpg"
        AlternateText="Rock Hard Studios - Ready for Christmas" ToolTip="Rock Hard Studios - Ready for Christmas" />
</asp:Content>
