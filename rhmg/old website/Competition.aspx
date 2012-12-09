<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Competition.aspx.cs" Inherits="rhmgWebsite.Web.Competition"
    MasterPageFile="~/Default.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Table ID="Table1" runat="server">
        <asp:TableRow>
            <asp:TableCell VerticalAlign="Top" ColumnSpan="2">
                <asp:Label ID="Label1" runat="server" CssClass="title" Text="Contact" />
                <asp:Panel ID="Panel2" runat="server" CssClass="text">
                    Rock Hard Music Group and 97.4 Rock FM are giving you the chance to sing in front
                    of a sell out crowd at the Blackpool Christmas Lights Switch On.
                </asp:Panel>
                <asp:Panel ID="bob" runat="server" CssClass="text">
                    Simply upload a video of you singing a song to YouTube and fill out the form below
                    to be entered into the competition. Entrants must be aged 16 and over and available
                    all day on the 19th November. Entries close Tuesday 15th and winner will be chosen
                    Wednesday 16th.</asp:Panel>
                <asp:Panel ID="Panel1" runat="server" CssClass="text">
                    All fields marked (<asp:Label ID="Label2" runat="server" CssClass="highlight">*</asp:Label>)
                    are mandatory, and we will log your IP address with your query.
                </asp:Panel>
            </asp:TableCell>
            <asp:TableCell Width="10px" RowSpan="4">
                &nbsp;
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Width="500px" RowSpan="2" VerticalAlign="Top">
                <asp:Table ID="Table3" runat="server" CellPadding="4" CellSpacing="4">
                    <asp:TableRow ID="TableRow1" runat="server">
                        <asp:TableCell Width="120px" HorizontalAlign="Right" VerticalAlign="Middle">
                            <asp:Label ID="Label3" runat="server" CssClass="highlight">*</asp:Label>Your Name:
                        </asp:TableCell>
                        <asp:TableCell ID="TableCell1" Width="200px" HorizontalAlign="Left" runat="server">
                            <asp:TextBox ID="name" runat="server" Width="190px"></asp:TextBox></asp:TableCell>
                        <asp:TableCell>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="name"
                                ValidationGroup="query_form" Font-Size="7pt" Text="this field is required" Display="Dynamic" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow5" runat="server">
                        <asp:TableCell HorizontalAlign="Right" VerticalAlign="Middle">
                            <asp:Label ID="Label6" runat="server" CssClass="highlight">*</asp:Label>Email Address:</asp:TableCell>
                        <asp:TableCell ID="TableCell5" HorizontalAlign="Left" runat="server">
                            <asp:TextBox ID="email" runat="server" Width="190px"></asp:TextBox></asp:TableCell>
                        <asp:TableCell>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="email"
                                ValidationGroup="query_form" ValidationExpression="^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$"
                                Text="email in incorrect format" Font-Size="7pt" Display="Dynamic" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="email"
                                ValidationGroup="query_form" Font-Size="7pt" Text="email in incorrect format"
                                Display="Dynamic" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow6" runat="server">
                        <asp:TableCell HorizontalAlign="Right" VerticalAlign="Middle">
                            <asp:Label ID="Label7" runat="server" CssClass="highlight">*</asp:Label>Confirm
                            Email:</asp:TableCell>
                        <asp:TableCell ID="TableCell6" HorizontalAlign="Left" runat="server">
                            <asp:TextBox ID="email2" runat="server" Width="190px"></asp:TextBox></asp:TableCell>
                        <asp:TableCell>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="email2"
                                ControlToCompare="email" Operator="Equal" Type="String" ValidationGroup="query_form"
                                Text="email addresses not identical" Font-Size="7pt" Display="Dynamic" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="email2"
                                ValidationGroup="query_form" Font-Size="7pt" Text="email addresses not identical"
                                Display="Dynamic" /></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow7" runat="server">
                        <asp:TableCell HorizontalAlign="Right" VerticalAlign="Middle">
                            <asp:Label ID="Label8" runat="server" CssClass="highlight">*</asp:Label>Contact
                            Number:</asp:TableCell>
                        <asp:TableCell ID="TableCell7" HorizontalAlign="Left" runat="server">
                            <asp:TextBox ID="contact_number" runat="server" Width="190px"></asp:TextBox></asp:TableCell>
                        <asp:TableCell>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="contact_number"
                                ValidationGroup="query_form" ValidationExpression="^(((\+44\s?\d{4}|\(?0\d{4}\)?)\s?\d{3}\s?\d{3})|((\+44\s?\d{3}|\(?0\d{3}\)?)\s?\d{3}\s?\d{4})|((\+44\s?\d{2}|\(?0\d{2}\)?)\s?\d{4}\s?\d{4}))(\s?\#(\d{4}|\d{3}))?$"
                                Text="number in incorrect format" Font-Size="7pt" Display="Dynamic" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="contact_number"
                                ValidationGroup="query_form" Font-Size="7pt" Text="this field is required" Display="Dynamic" /></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow2" runat="server">
                        <asp:TableCell Width="120px" HorizontalAlign="Right" VerticalAlign="Middle">
                            <asp:Label ID="Label5" runat="server" CssClass="highlight">*</asp:Label>Url to your video:
                        </asp:TableCell><asp:TableCell ID="TableCell2" HorizontalAlign="Left" runat="server">
                            <asp:TextBox ID="url_input" runat="server" Width="190px"></asp:TextBox></asp:TableCell>
                        <asp:TableCell>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="contact_number"
                                ValidationGroup="query_form" Font-Size="7pt" Text="this field is required" Display="Dynamic" /></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow4" runat="server">
                        <asp:TableCell HorizontalAlign="Right" VerticalAlign="Middle"></asp:TableCell>
                        <asp:TableCell ID="TableCell4" HorizontalAlign="Left" runat="server">
                            <asp:Button ID="submit" OnClick="Submit_click" runat="server" ValidationGroup="query_form"
                                Text="send us your entry" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2">
                            <asp:Label ID="ErrorLabel" runat="server" Visible="false" CssClass="errorSubTitle"
                                Text="**An error occurred. Please try later.**" /><asp:Label ID="Label4" runat="server"
                                    CssClass="errorSubTitle" Text="&nbsp;" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
