using blackpoolgigs.common.Contracts;
using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.Models;
using coolbunny.tests.common.contexts;
using coolbunny.tests.common.shared.Behaviours;
using Machine.Specifications;
using Raven.Abstractions.Data;
using Raven.Json.Linq;
using rhmg.AdministrateBlackpoolGigs.web.Controllers;
using rhmg.AdministrateBlackpoolGigs.web.Models.ViewModels;

namespace rhmg.AdministrateBlackpoolGigs.web.tests
{
    public class VenueRoutingSpecs
    {
        public class when_routing_to_get_all_venues : with_controller<VenuesViewModel, VenuesController>
        {
            Because of = () => GET("Venues");
            Behaves_like<ok> get;
        }

        public class when_routing_to_a_venue : with_controller<VenueViewModel, VenuesController>
        {
            Establish context = () =>
            {
                Stub<IGiveYouVenues>()
                    .Setup(s => s.GetMetadata(Moq.It.IsAny<VenueName>()))
                    .Returns(new VenueMetadata());
                Stub<IGiveYouVenues>()
                    .Setup(s => s.Get(Moq.It.IsAny<VenueName>()))
                    .Returns(new VenueDiary());
            };
            Because of = () => GET("Venues/The Blue Room");
            Behaves_like<ok> get;
        }
        public class when_routing_to_a_venue_mainimage : with_controller<VenueViewModel, VenuesController>
        {
            Establish context = () =>
            {
                Stub<IGiveYouVenues>()
                    .Setup(s => s.GetMetadata(Moq.It.IsAny<VenueName>()))
                    .Returns(new VenueMetadata
                    {
                        VenueName = "a venue",
                        MainImageName = "something",
                        Id = "venue/1"
                    });
                Stub<IDealWithRavenAttachments>()
                    .Setup(s => s.MainImage(Moq.It.IsAny<string>()))
                    .Returns(new Attachment
                    {
                        Metadata = RavenJObject.FromObject(
                           new
                           {
                               Filename = "something"
                           })
                    });
            };
            Because of = () => GET("Venues/The Blue Room/MainImage");
            Behaves_like<ok> get;
        }
        public class when_posting_updates_to_venue_metadata : with_controller<VenuesViewModel, VenuesController>
        {
            Because of = () => POST("Venues/The Blue Room", new VenueViewModel());
            Behaves_like<see_other> post;
        }
        public class when_deleting_venue_metadata : with_controller<VenuesViewModel, VenuesController>
        {
            Because of = () => DELETE("metadata/1");
            Behaves_like<ok> delete;
        }
    }
}