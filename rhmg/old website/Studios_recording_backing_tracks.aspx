<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Studios_recording_backing_tracks.aspx.cs"
    Inherits="rhmgWebsite.Web.Studios_recording_backing_tracks" MasterPageFile="~/Default.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="Label1" runat="server" CssClass="title" Text="Rock Hard Studios - Backing Tracks" />
    <asp:Panel ID="bruce" runat="server" CssClass="text">
        Rock Hard Studios has teamed up with Ameritz to provide you with professional quality
        backing tracks. Below is a link directly to their store where you can try before
        you buy, and then download the files directly to your home computer.
    </asp:Panel>
    <asp:Panel ID="Panel1" runat="server" CssClass="text">
        This will allow you to practice prior to your <a href="PopStarParties.aspx" class="standardLinkInactive">
            Pop Star Party</a> or Recording Studio Experience Day. You can also use these
        tracks if you perform live.
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" CssClass="text">
        All tracks are <b>licensed</b> and <b>royalty</b> free so long as you do not intend
        selling your finished recording. If you wish to sell your recordings please just
        ask us how to get your licence to do this - don't worry, it's quite easy.
    </asp:Panel>
    <iframe frameborder="0" width="98%" height="670" src="http://www.ameritz.co.uk/affiliate_top100.asp?affid=109922"
        align="center" scrolling="Auto"></iframe>
</asp:Content>
