﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Studios_recording.aspx.cs"
    Inherits="rhmgWebsite.Web.Studios_recording" MasterPageFile="~/Default.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Table runat="server">
        <asp:TableRow>
            <asp:TableCell Width="500px" VerticalAlign="Top">
                <asp:Label ID="Label1" runat="server" CssClass="title" Text="Rock Hard Studios - Recording"
                    ToolTip="Recording at Rock Hard Studios, Blackpool, Lancashire - THE best recording studio for bands or solo artists." />
                <asp:Panel ID="Panel2" runat="server" CssClass="text">
                    Rock Hard Studios boasts a 400 sq foot live room, linked via a sextuple-glazed window
                    to our cutting-edge custom designed 200 sq foot control room.
                </asp:Panel>
                <asp:Panel ID="Panel3" runat="server" CssClass="text">
                    Customers have exclusive access to our chill out room / games room upstairs, with
                    exceptionally comfortable sofas, a widescreen television with games consoles, a
                    pool table available for all customers at 50p a play, and another window looking
                    down into the live room.
                </asp:Panel>
                <asp:Panel ID="Panel5" runat="server" CssClass="text">
                    Our comprehensive range of microphones will be enough to satisfy any purpose, and
                    the experience that we have using them means that no application is out of range.
                </asp:Panel>
                <asp:Panel ID="Panel4" runat="server" CssClass="text">
                    We also have an extensive range of items available for use during your session including
                    drum kits, guitar and bass amps and a classic Farfisa organ.
                </asp:Panel>
                <asp:Panel ID="bob" runat="server" CssClass="text">
                    For your convenience, we have teamed up with a <b>highly reputable UK backing track
                        supplier</b>, so you can quickly purchase a backing track to <b>rehearse at home</b>,
                    before coming to the studio for your recording session or party. Please click <a
                        href="Studios_recording_backing_tracks.aspx" class="standardLinkInactive" title="Click for high quality, low cost backing tracks - ideal for solo artists to practice or record to; Rock Hard Studios, Blackpool, Lancashire">
                        here</a>.</asp:Panel>
                <br />
                <br />
                <asp:Table ID="Table1" runat="server" CellPadding="0" CellSpacing="0">
                    <asp:TableRow>
                        <asp:TableCell Width="25px"></asp:TableCell>
                        <asp:TableCell>
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/photos/explore_the_studio/Rock Hard Studios Blackpool Control Surface Digidesign Protools HD2 C24_.jpg"
                                AlternateText="Rock Hard Studios Control Surface - Digidesign Protools HD2/C24 :: Record with your band or as a solo artist at Rock Hard Studios, Blackpool"
                                ToolTip="Rock Hard Studios Control Surface - Digidesign Protools HD2/C24 :: Record with your band or as a solo artist at Rock Hard Studios, Blackpool" /></asp:TableCell>
                        <asp:TableCell Width="25px"></asp:TableCell>
                        <asp:TableCell>
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/images/photos/explore_the_studio/Rock Hard Studios Blackpool The view from the Drum Kit_.jpg"
                                AlternateText="Rock Hard Studios - The view from the Drum Kit :: Bands and solo artists recording in Blackpool, Lancashire"
                                ToolTip="Rock Hard Studios - The view from the Drum Kit :: Bands and solo artists recording in Blackpool, Lancashire" /></asp:TableCell>
                        <asp:TableCell Width="25px"></asp:TableCell>
                        <asp:TableCell>
                            <asp:Image ID="Image3" runat="server" ImageUrl="~/images/photos/explore_the_studio/Rock Hard Studios Blackpool The view from the control room into the Live Room_.jpg"
                                AlternateText="Rock Hard Studios - The view from the control room into the Live Room :: Professional recording for bands and solo artists in Blackpool, Lancashire"
                                ToolTip="Rock Hard Studios - The view from the control room into the Live Room :: Professional recording for bands and solo artists in Blackpool, Lancashire" /></asp:TableCell>
                        <asp:TableCell Width="25px"></asp:TableCell>
                        <asp:TableCell>
                            <asp:Image ID="Image4" runat="server" ImageUrl="~/images/photos/explore_the_studio/Rock Hard Studios Blackpool The Live room viewed from the Chill Out Room_.jpg"
                                AlternateText="Rock Hard Studios - The Live room viewed from the Chill Out Room :: An amazing recording experience for bands and solo artists at Rock Hard Studios, Blackpool, Lancashire"
                                ToolTip="Rock Hard Studios - The Live room viewed from the Chill Out Room :: An amazing recording experience for bands and solo artists at Rock Hard Studios, Blackpool, Lancashire" /></asp:TableCell>
                        <asp:TableCell Width="25px"></asp:TableCell>
                        <asp:TableCell>
                            <asp:Image ID="Image5" runat="server" ImageUrl="~/images/photos/explore_the_studio/Rock Hard Studios Blackpool The Live room with our Drum World Custom Maple Drumkit_.jpg"
                                AlternateText="Rock Hard Studios - The Live room with our Drum World Custom Maple Drumkit :: record with the best kit at Rock Hard Studios, Blackpool, Lancashire"
                                ToolTip="Rock Hard Studios - The Live room with our Drum World Custom Maple Drumkit :: record with the best kit at Rock Hard Studios, Blackpool, Lancashire" /></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Panel ID="Panel1" runat="server" CssClass="text">
                    <asp:BulletedList ID="BulletedList1" runat="server">
                        <asp:ListItem>Custom Designed Recording Suite</asp:ListItem>
                        <asp:ListItem>Digidesign Protools HD2</asp:ListItem>
                        <asp:ListItem>Digidesign C24 Control Surface</asp:ListItem>
                        <asp:ListItem>Pro tools 8 / Logic 7 / Cubase 4</asp:ListItem>
                        <asp:ListItem>Apple MacPro 2x2.8gig quad core (OS X 10.5)</asp:ListItem>
                        <asp:ListItem>AppleiMac 3.06gig 27" (OS X 10.6)</asp:ListItem>
                        <asp:ListItem>MOTU 2408 Mk 3</asp:ListItem>
                        <asp:ListItem>192 interface (8 x analog input + 8 x AES i/o)</asp:ListItem>
                        <asp:ListItem>96i interface</asp:ListItem>
                        <asp:ListItem>Yamaha NS10 + KRK V8 monitoring system</asp:ListItem>
                    </asp:BulletedList>
                </asp:Panel>
                <asp:Panel ID="Panel6" runat="server" CssClass="text">
                    <asp:BulletedList ID="BulletedList2" runat="server">
                        <asp:ListItem>Neumann TLM103</asp:ListItem>
                        <asp:ListItem>Beyerdynamic Opus 87</asp:ListItem>
                        <asp:ListItem>Shure SM58</asp:ListItem>
                        <asp:ListItem>Shure SM57</asp:ListItem>
                        <asp:ListItem>Shure Beta 91</asp:ListItem>
                        <asp:ListItem>Shure Beta 98</asp:ListItem>
                        <asp:ListItem>AKG C1000</asp:ListItem>
                        <asp:ListItem>AKG D300</asp:ListItem>
                    </asp:BulletedList>
                </asp:Panel>
                <center>
                    <a href="/Downloads/Full Equipment List.pdf" target="_blank" class="standardLink" title="A list of the equipment at Rock Hard Studios, Blackpool, Lancashire - for bands or solo artists">
                        <asp:Image ID="Image6" runat="server" ImageUrl="~/images/pdf.png" /><br />
                        <asp:Label ID="Label2" runat="server" Text="Download Full Equipment List.pdf" />
                    </a>
                </center>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
