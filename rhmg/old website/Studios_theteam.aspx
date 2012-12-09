<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Studios_theteam.aspx.cs"
    Inherits="rhmgWebsite.Web.Studios_theteam" MasterPageFile="~/Default.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Table ID="Table1" runat="server">
        <asp:TableRow>
            <asp:TableCell VerticalAlign="Top" ColumnSpan="4">
                <asp:Label ID="Label1" runat="server" CssClass="title" Text="Rock Hard Studios - Meet the Team"
                    ToolTip="Meet the producers and teachers at Rock Hard Studios and Rock Hard Academy :: professional recording and rehearsals for bands and solo artists in Blackpool, Lancashire" />
                <asp:Panel ID="Panel2" runat="server" CssClass="text">
                    Welcome to a little page just about us, your friendly Rock Hard team.
                </asp:Panel>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Width="200px" HorizontalAlign="Center">
                            <asp:Image runat="server" ImageUrl="~/images/us/andrew producer blackpool.jpg" AlternateText="Andrew Cox - Studio Recording and Production :: highly skilled recording bands and solo artists out of Blackpool, Lancashire"
                            ToolTip="Andrew Cox - Studio Recording and Production :: highly skilled recording bands and solo artists out of Blackpool, Lancashire"/>
            </asp:TableCell>
            <asp:TableCell VerticalAlign="Middle">
                <asp:Panel ID="Panel5" runat="server" CssClass="text" HorizontalAlign="Justify">
                    Andrew has been working in recording studios around the country for the last 14
                    years and has a wealth of experience in producing bands from multiple genres. Outside
                    the studio he likes nothing more than to settle down to watch a game of NFL, or
                    alternatively cheer on England in any sport<asp:HyperLink ID="HyperLink4" CssClass="standardLinkInactive"
                        runat="server" Text="..." NavigateUrl="~/AndrewCox.aspx" />
                </asp:Panel>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Width="200px" HorizontalAlign="Center">
                <asp:Image ID="Image2" runat="server" ImageUrl="~/images/us/dan producer blackpool.jpg"
                    AlternateText="Daniel Atkinson - Studio Recording and Production :: hugely experienced producer and engineer in Blackpool, Lancashire"
                    ToolTip="Daniel Atkinson - Studio Recording and Production :: hugely experienced producer and engineer in Blackpool, Lancashire" />
            </asp:TableCell>
            <asp:TableCell VerticalAlign="Middle">
                <asp:Panel ID="Panel6" runat="server" CssClass="text" HorizontalAlign="Justify">
                    Daniel first joined a band aged 12 and has never looked back, recording his first
                    multi-track demo when he was just 13. Since then he has built up many years of experience
                    working with all sorts of bands. Dan is an ardent Manchester United supporter, but
                    don’t hold this against him<asp:HyperLink ID="HyperLink1" CssClass="standardLinkInactive"
                        runat="server" Text="..." NavigateUrl="~/DanAtkinson.aspx" />
                </asp:Panel>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Width="200px" HorizontalAlign="Center">
                <asp:Image ID="Image3" runat="server" ImageUrl="~/images/us/matt drum tutor teaching drums in blackpool.jpg"
                    AlternateText="Matt Sellers - Drum Tutor teaching drums in Blackpool, Lancashire :: working with Rock Hard Academy in Blackpool, Lancashire"
                    ToolTip="Matt Sellers - Drum Tutor teaching drums in Blackpool, Lancashire :: working with Rock Hard Academy in Blackpool, Lancashire" />
            </asp:TableCell>
            <asp:TableCell VerticalAlign="Middle">
                <asp:Panel ID="Panel1" runat="server" CssClass="text" HorizontalAlign="Justify">
                    Matt is one of the most highly respected drummers in the Fylde coast, having a musical
                    CV containing the names of some of the most respected bands in the area. He has
                    also been teaching drums to people of all abilities for many years, with his enthusiasm
                    and experience shining through every time he sits behind a kit<asp:HyperLink ID="HyperLink3"
                        CssClass="standardLinkInactive" runat="server" Text="..." NavigateUrl="~/MattSellers.aspx" />
                </asp:Panel>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
