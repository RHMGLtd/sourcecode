using System;
using coolbunny.tests.common.contexts;
using coolbunny.tests.common.shared.Behaviours;
using Machine.Specifications;
using rhmg.AdministrateBlackpoolGigs.web.Controllers;
using rhmg.AdministrateBlackpoolGigs.web.Models;
using rhmg.AdministrateBlackpoolGigs.web.Models.ViewModels;

namespace rhmg.AdministrateBlackpoolGigs.web.tests
{
    public class LookupRoutingSpecs
    {
        public class when_routing_to_venue_lookup : with_controller<LookupViewModel, LookupController>
        {
            Because of = () => GET("/lookup/Venues");
            Behaves_like<ok> get;
        }
        public class when_routing_to_bandnames_lookup : with_controller<LookupViewModel, LookupController>
        {
            Because of = () => GET("/lookup/BandNames");
            Behaves_like<ok> get;
        }
        public class when_routing_to_check_date : with_controller<DateCheckViewModel, ManageGigsController>
        {
            Because of = () => PUT("CheckDate", new DateCheckModel { checkDate = DateTime.Now });
            Behaves_like<ok> get;
        }
    }
}