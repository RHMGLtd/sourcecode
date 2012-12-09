<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Customers.aspx.cs" Inherits="rhmgWebsite.Web.Customers"
    MasterPageFile="~/Default.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Table ID="Table1" runat="server">
        <asp:TableRow>
            <asp:TableCell VerticalAlign="Top">
                <asp:Label ID="Label1" runat="server" CssClass="title" Text="Customer List" />
                <asp:Panel ID="Panel1" runat="server" CssClass="text">
                    At Rock Hard Studios we consider all our customers to be part of the larger family.
                    We do everything in our power to provide a service that matches and exceeds expectations.
                    We truly believe that our studio is the best for Recording and Rehearsals that there
                    is in Blackpool, Lancashire and the surrounding area.</asp:Panel>
                <asp:Panel ID="Panel2" runat="server" CssClass="text">
                    Below is a (kind of) exhaustive list of who has been through our studio since we
                    opened in 2007. We are proud to have been associated with each of these artists,
                    and to have a continuing relationship with them into the future.</asp:Panel>
                <asp:Panel ID="Panel3" runat="server" CssClass="text">
                    If you want to find out what they found so amazing about us and our facility please
                    just <a href="Contact.aspx">contact us</a>, or make yourself welcome by turning
                    up and introducing yourselves.</asp:Panel>
                <asp:Table ID="CustomerTable" runat="server">
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
