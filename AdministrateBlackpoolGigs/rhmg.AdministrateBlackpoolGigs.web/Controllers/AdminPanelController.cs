using blackpoolgigs.common.Interfaces;
using rhmg.AdministrateBlackpoolGigs.web.Models.ViewModels;
using rhmg.AdministrateBlackpoolGigs.web.Urls;
using Snooze;

namespace rhmg.AdministrateBlackpoolGigs.web.Controllers
{
    public class AdminPanelController : ResourceController
    {
        protected readonly IOneOffAdminStuff admin;

        public AdminPanelController(IOneOffAdminStuff admin)
        {
            this.admin = admin;
        }

        public ResourceResult Get(AdminPanelUrl url)
        {
            return OK(new AdminPanelViewModel()).AsHtml();
        }
        public ResourceResult Get(AddCreatedDatesUrl url)
        {
            return OK(new AddCreatedDatesViewModel
                          {
                              Count = admin.AddCreatedDates()
                          }).AsHtml();
        }
        public ResourceResult Get(DataCleanseSpacesUrl url)
        {
            admin.DataCleanseSpaces();
            return OK(new DataCleanseSpacesViewModel()).AsHtml();
        }
    }
}