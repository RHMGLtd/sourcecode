using System;

namespace rhmgWebsite.Web
{
    public partial class CompetitionThankyou : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Default master = Page.Master as Default;
            master.ConfigurePage(SiteSection.Studio,
                Pages.contact,
                new PageTitle("Rock Hard Studios - Competition entry confirmation"),
                new PageSubTitle("Rock Hard Studios - Pro level recording and rehearsal for amazing prices"),
                new PageDescription("Confirmation page having entered the Rock FM competition"),
                new NavControlPath("CustomControls/StudiosSideBar.ascx"),
                new Banner("~/images/Rock Hard Studios - Blackpool's best recording and rehearsal studios.png",
                    "Rock Hard Studios - Blackpool, Lancashire's best recording and rehearsal studios"),
                    false);
        }
    }
}
