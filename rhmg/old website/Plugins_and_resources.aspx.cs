using System;
using System.Web.UI;

namespace rhmgWebsite.Web
{
    public partial class Plugins_and_resources : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Default master = Page.Master as Default;
            master.ConfigurePage(SiteSection.Studio,
                Pages.plugins,
                new PageTitle("Rock Hard Music Group - Useful plugins and other resources for musicians, bands or solo artists provided free in conjunction with Rock Hard Studios, Blackpool, Lancashire"),
                new PageSubTitle("Rock Hard Music Group - Providing pro musical services to the musical community"),
                new PageDescription("Useful plugins and other resources for musicians, bands or solo artists."),
                new NavControlPath("CustomControls/HomeSideBar.ascx"),
                new Banner("~/images/Rock Hard Music Group - Running Blackpool's best recording and rehearsal studios.png",
                    "Rock Hard Music Group - Running Blackpool, Lancashire's best recording and rehearsal studios"),
                    false);
        }
    }
}
