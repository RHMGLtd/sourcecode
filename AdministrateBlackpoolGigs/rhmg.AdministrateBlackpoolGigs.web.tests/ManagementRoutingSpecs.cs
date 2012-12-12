using coolbunny.tests.common.contexts;
using coolbunny.tests.common.shared.Behaviours;
using Machine.Specifications;
using rhmg.AdministrateBlackpoolGigs.web.Controllers;
using rhmg.AdministrateBlackpoolGigs.web.Models.ViewModels;

namespace rhmg.AdministrateBlackpoolGigs.web.tests
{
    public class ManagementRoutingSpecs
    {
        public class when_routing_to_export : with_controller<ExportViewModel, ExportController>
        {
            Because of = () => GET("export");
            Behaves_like<ok> get;
        }
        public class when_getting_the_admin_panel : with_controller<AdminPanelViewModel, AdminPanelController>
        {
            Because of = () => GET("adminpanel");
            Behaves_like<ok> get;
        }
        public class when_adding_created_dates : with_controller<AddCreatedDatesViewModel, AdminPanelController>
        {
            Because of = () => GET("adminpanel/addcreateddates");
            Behaves_like<ok> get;
        }
        public class when_datacleansing_spaces : with_controller<AddCreatedDatesViewModel, AdminPanelController>
        {
            Because of = () => GET("adminpanel/datacleanse/spaces");
            Behaves_like<ok> get;
        }
    }
}