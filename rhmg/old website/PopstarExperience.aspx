<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Default.Master" CodeBehind="PopstarExperience.aspx.cs"
    Inherits="rhmgWebsite.Web.PopstarExperience" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Table ID="Table1" runat="server">
        <asp:TableRow>
            <asp:TableCell VerticalAlign="Top">
                <asp:Label ID="Label1" runat="server" CssClass="title" Text="Rock Hard Studios - Pop Star Experience" />
                <asp:Panel runat="server" CssClass="text">
                    Rock Hard Music Group is giving you the opportunity to record your own CD in our
                    brand new, state of the art, recording studio.
                </asp:Panel>
                <asp:Panel runat="server" CssClass="text">
                    Come in and rehearse your chosen track in one of the professional sound proofed
                    rehearsal suites and then spend an hour in the recording studio laying down your
                    vocals.
                </asp:Panel>
                <asp:Panel runat="server" CssClass="text">
                    At the end of the session you will go away with a professionaly recorded single.
                    Choose from our <a href="Studios_recording_backing_tracks.aspx" class="standardLinkInactive">
                        collection of 1000s of tracks</a> or bring your own.
                </asp:Panel>
                <br />
                <asp:Panel runat="server" CssClass="subTitle">
                    All this for just £99!
                </asp:Panel>
                <br />
                <br />
                <asp:Panel runat="server" CssClass="subTitle">
                    Rock Hard Studios - Bringing out the pop star in YOU!
                </asp:Panel>
            </asp:TableCell>
            <asp:TableCell VerticalAlign="Top">
                <asp:Image runat="server" ImageUrl="~/images/products/Pop Star Experience - Be a Popstar for a day.png" 
                AlternateText="Pop Star Experience - Be a Popstar for a day" ToolTip="Pop Star Experience - Be a Popstar for a day" />
               
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="BottomBannerContentPlaceHolder">
</asp:Content>
