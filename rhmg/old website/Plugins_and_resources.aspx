<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Plugins_and_resources.aspx.cs"
    Inherits="rhmgWebsite.Web.Plugins_and_resources" MasterPageFile="~/Default.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Table ID="Table1" runat="server">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Panel VerticalAlign="Top">
                    <asp:Label ID="Label1" runat="server" CssClass="title" Text="Plugins and other Resources" />
                    <asp:Panel ID="Panel1" runat="server" CssClass="text">
                        At Rock Hard Studios we are keen to provide all kinds of assistance to you, and
                        here on this page you will find an assortment of useful plugins to help you improve
                        yourself as a musician, whether you are in a band or are a solo artist.</asp:Panel>
                    <asp:Panel ID="Panel3" runat="server" CssClass="text">
                        We are only able to provide these plugins due to the excellent work of other online
                        providers of plugins so please, if you find these useful, follow the links back
                        to their site and express your appreciation.
                    </asp:Panel>
                    <asp:Table runat="server">
                        <asp:TableRow runat="server">
                            <asp:TableCell ID="TableCell1" runat="server" Width="300">
                            <div style="display: block; text-align: center; width: 280px;">
                                <object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0"
                                    width="280" height="155" id="mini-guitar-tuner" align="middle">
                                    <param name="allowScriptAccess" value="sameDomain" />
                                    <param name="allowFullScreen" value="false" />
                                    <param name="movie" value="http://www.howtotuneaguitar.org/images/mini-guitar-tuner.swf" />
                                    <param name="quality" value="high" />
                                    <param name="bgcolor" value="#ffffff" />
                                    <embed src="http://www.howtotuneaguitar.org/images/mini-guitar-tuner.swf" quality="high"
                                        bgcolor="#ffffff" width="280" height="155" name="mini-guitar-tuner" align="middle"
                                        allowscriptaccess="sameDomain" allowfullscreen="false" type="application/x-shockwave-flash"
                                        pluginspage="http://www.macromedia.com/go/getflashplayer" />
                                </object>
                            </div>
                            </asp:TableCell>
                            <asp:TableCell ID="bob" runat="server" VerticalAlign="Top">
                                <asp:Panel ID="bruce" runat="server" CssClass="text">
                                    The Tune-O-Matic comes from our friends over at <a href="http://www.howtotuneaguitar.org"
                                        target="_blank" class="externalLink">www.howtotuneaguitar.org</a>. It a very
                                    simple interface, with one note at a time accoustic guitar samples.
                                </asp:Panel>
                                <asp:Panel ID="tom" runat="server" CssClass="text">
                                    As simple as it is, all you need to do is to click on the note you are tuning and
                                    a repeating sample of that note will be played.
                                </asp:Panel>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="TableRow1" runat="server">
                            <asp:TableCell ID="TableCell3" runat="server">
                            <script type="text/javascript">
                                AC_FL_RunContent('codebase', 'http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0', 'width', '280', 'height', '304', 'src', 'flash/metronome?source=mo', 'quality', 'high', 'pluginspage', 'http://www.macromedia.com/go/getflashplayer', 'movie', 'http://www.metronomeonline.com/flash/metronome', 'flashvars', 'source=mo'); //end AC code
                            </script>
                            <noscript>
                                <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0"
                                    width="280" height="304">
                                    <param name="movie" value="/metronome_app.swf" />
                                    <param name="quality" value="high" />
                                    <param name="FlashVars" value="source=mo" />
                                    <embed src="/metronome_app.swf?source=mo" quality="high"
                                        pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash"
                                        width="280" height="304">
                                    </embed>
                                </object>
                            </noscript>
                            </asp:TableCell>
                            <asp:TableCell ID="TableCell4" runat="server" VerticalAlign="Top">
                                <asp:Panel ID="Panel2" runat="server" CssClass="text">
                                    This fantastic metronome is definitely the best available online, mixing good looks
                                    with ease of use. We can provide this to you thanks to <a href="http://www.metronomeonline.com/"
                                        class="externalLink" target="_blank">the emusic institute</a> and <a href="http://www.metronomeonline.com/"
                                            class="externalLink" target="_blank">metronome online</a>.
                                </asp:Panel>
                                <asp:Panel ID="Panel4" runat="server" CssClass="text">
                                    You will need your speakers turned on first (obviously) but it is as simple as clicking
                                    the "on" button to start the metronome. You can change the tempo using the dial,
                                    and get help with the "?" link.
                                </asp:Panel>
                                <asp:Panel ID="Panel5" runat="server" CssClass="text">
                                    This plugin also contains a simple tuner (just giving you a single note).
                                </asp:Panel>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
