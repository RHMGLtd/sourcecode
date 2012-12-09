using System;

namespace rhmgWebsite.Web
{
    public partial class PopstarExperience : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Default master = Page.Master as Default;
            master.ConfigurePage(SiteSection.Home,
                Pages.popstarExperience,
                new PageTitle("Rock Hard Music Group - Popstar Experience"),
                new PageSubTitle("Rock Hard Music Group - Providing pro musical services to the musical community"),
                new PageDescription("Pop Star Experience - Record your own professional single - Bring out the star in YOU!"),
                new NavControlPath("CustomControls/HomeSideBar.ascx"),
                new Banner("~/images/Rock Hard Music Group - Running Blackpool's best recording and rehearsal studios.png",
                    "Rock Hard Music Group - Running Blackpool, Lancashire's best recording and rehearsal studios"),
                    false);
        }
    }
}
