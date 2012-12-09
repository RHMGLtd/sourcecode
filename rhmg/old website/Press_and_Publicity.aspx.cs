using System;
using System.Web.UI;

namespace rhmgWebsite.Web
{
    public partial class Press_and_Publicity : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Default master = Page.Master as Default;
            master.ConfigurePage(SiteSection.Home,
                Pages.press,
                new PageTitle("Rock Hard Music Group - Press and Publicity for Rock Hard Studios, providing professional recording and rehearsals in Blackpool, Lancashire"),
                new PageSubTitle("Rock Hard Music Group - Providing pro musical services to the musical community"),
                new PageDescription("Links to Rock Hard Music Group and Rock Hard Studios in the press, and other publicity."),
                new NavControlPath("CustomControls/HomeSideBar.ascx"),
                new Banner("~/images/Rock Hard Music Group - Running Blackpool's best recording and rehearsal studios.png",
                    "Rock Hard Music Group - Running Blackpool, Lancashire's best recording and rehearsal studios"),
                    false);
        }
    }
}
