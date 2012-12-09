using System;

namespace rhmgWebsite.Web
{
    public partial class Studios_recording_backing_tracks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Default master = Page.Master as Default;
            master.ConfigurePage(SiteSection.Studio,
                Pages.recording,
                new PageTitle("Rock Hard Studios - Blackpool, Lancashire Recording Studio - backing tracks"),
                new PageSubTitle("Rock Hard Studios - Pro level recording and rehearsal for amazing prices"),
                new PageDescription("Our partnership with Ameritz to provide high quality backing tracks for your recording session in Blackpool, Lancashire"),
                new NavControlPath("CustomControls/StudiosSideBar.ascx"),
                new Banner("~/images/Rock Hard Studios - Blackpool's best recording and rehearsal studios.png",
                    "Rock Hard Studios - Blackpool, Lancashire's best recording and rehearsal studios"),
                    false);
        }
    }
}
