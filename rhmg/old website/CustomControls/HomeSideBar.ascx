<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HomeSideBar.ascx.cs"
    Inherits="rhmgWebsite.Web.CustomControls.HomeSideBar" %>
<asp:HyperLink ID="LinksHL" runat="server" Text="Links" ToolTip="Click for links to sites that link to us" CssClass="sidepanelLink"
    NavigateUrl="~/Links.aspx" /><br />
<br />
<asp:HyperLink ID="TheTeamHL" runat="server" Text="The Team" ToolTip="Click to meet the producers and engineers at Rock Hard Studios, Blackpool, Lancashire - rehearsal and recording for bands and solo artists" CssClass="sidepanelLink"
    NavigateUrl="~/Studios_theteam.aspx" /><br />
<br />
<asp:HyperLink ID="ProductsHL" runat="server" Text="Products" ToolTip="Click to see products from Rock Hard Studios, Blackpool, Lancashire - rehearsal and recording for bands and solo artists" CssClass="sidepanelLink"
    NavigateUrl="~/Products.aspx" /><br />
<br />
<asp:HyperLink ID="PressHL" runat="server" Text="Press" ToolTip="Click to see Press and Publicity from Rock Hard Studios, Blackpool, Lancashire - rehearsal and recording for bands and solo artists" CssClass="sidepanelLink"
    NavigateUrl="~/Press_and_Publicity.aspx" /><br />
<br />
