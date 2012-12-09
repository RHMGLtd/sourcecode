using System;

namespace rhmgWebsite.Web.CustomControls
{
    public partial class HomeSideBar : SidebarNavBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public override void Navigate(Pages page)
        {
            LinksHL.CssClass = TheTeamHL.CssClass = ProductsHL.CssClass = "sidepanelLink";

            switch (page)
            {
                case Pages.links:
                    LinksHL.CssClass = "sidepanelLinkInactive";
                    break;
                case Pages.theTeam:
                    TheTeamHL.CssClass = "sidepanelLinkInactive";
                    break;
                case Pages.products:
                case Pages.popStarParties:
                case Pages.cabaretArtists:
                case Pages.karaoke:
                case Pages.popstarExperience:
                    ProductsHL.CssClass = "sidepanelLinkInactive";
                    break;
                case Pages.press:
                    PressHL.CssClass = "sidepanelLinkInactive";
                    break;
                default:
                    break;
            }
        }
    }
}