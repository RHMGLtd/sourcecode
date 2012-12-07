using System;
using System.Collections.Generic;
using blackpoolgigs.common.Contracts;
using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.Models;
using blackpoolgigs.web.Controllers;
using blackpoolgigs.web.Models;
using blackpoolgigs.web.Models.ViewModels;
using coolbunny.tests.common.contexts;
using coolbunny.tests.common.shared.Behaviours;
using Machine.Specifications;

namespace blackpoolgigs.tests
{
    public class routing_to_a_venue_map : with_controller<VenueMapViewModel, VenueController>
    {
        Establish context = () =>
            Stub<IGiveYouVenues>()
                    .Setup(s => s.GetMetadata(Moq.It.IsAny<VenueName>()))
                    .Returns(new VenueMetadata
                                 {
                                     MapCoords = new GoogleMapCoords
                                                     {
                                                         Lat = "8.00000",
                                                         Long = "-3.00000"
                                                     }
                                 });
        Because of = () => GET("Venues/The Blue Room/Map");
        Behaves_like<ok> get;
    }
    public class routing_to_a_venue_map_that_isnt_there : with_controller<VenueMapNotFoundViewModel, VenueController>
    {
        Establish context = () =>
            Stub<IGiveYouVenues>()
                    .Setup(s => s.GetMetadata(Moq.It.IsAny<VenueName>()))
                    .Returns(new VenueMetadata());
        Because of = () => GET("Venues/The Blue Room/Map");
        Behaves_like<not_found> get;
        It has_the_correct_failure_reason = () => Resource.Reason.ShouldEqual(FourOhFourReason.NoMapForThisVenue);
    }

    public class routing_to_a_venue : with_controller<VenueViewModel, VenueController>
    {
        Establish context = () =>
        {
            Stub<IGiveYouVenues>()
                .Setup(s => s.Get(Moq.It.IsAny<VenueName>()))
                .Returns(new VenueDiary { Venue = "The Blue Room" });
            Stub<IGiveYouVenues>()
                    .Setup(s => s.GetMetadata(Moq.It.IsAny<VenueName>()))
                    .Returns(new VenueMetadata());
        };
        Because of = () => GET("Venues/The Blue Room");
        Behaves_like<ok> get;
    }
    public class routing_to_a_venue_that_isnt_there : with_controller<VenueViewModel, VenueController>
    {
        Establish context = () => Stub<IGiveYouVenues>()
                                      .Setup(s => s.Get(Moq.It.IsAny<VenueName>()))
                                      .Returns(new EmptyVenueDiary { Venue = "The Blue Room" });
        Because of = () => GET("Venues/The Blue Room");
        Behaves_like<not_found> get;
    }
    public class routing_to_a_list_of_venues : with_controller<VenuesViewModel, VenueController>
    {
        Because of = () => GET("Venues");
        Behaves_like<ok> get;
    }
    public class routing_to_venues_map : with_controller<VenuesMapViewModel, VenueController>
    {
        Because of = () => GET("Venues/Map");
        Behaves_like<ok> get;
    }

    public class routing_to_a_gig : with_controller<GigViewModel, VenueController>
    {
        Establish context = () =>
            {
                Stub<IGiveYouVenues>()
                    .Setup(s => s.Get(Moq.It.IsAny<VenueName>()))
                    .Returns(new VenueDiary
                                 {
                                     Venue = "The Blue Room",
                                     Gigs = new List<Gig>
                                                    {
                                                        new Gig
                                                            {
                                                                Date = new DateTime(2011, 4, 23)
                                                            }
                                                    }
                                 });
                Stub<IGiveYouVenues>()
                    .Setup(s => s.GetMetadata(Moq.It.IsAny<VenueName>()))
                    .Returns(new VenueMetadata { VenueName = "The Blue Room" });
            };
        Because of = () => GET("Venues/The Blue Room/23/April/2011");
        Behaves_like<ok> get;
    }

    public class routing_to_an_invalid_venue_rooted_route : with_controller<FourOhFourViewModel, ErrorController>
    {
        Because of = () => GET("Venues/blah/blah/blah");
        Behaves_like<not_found> get;
    }
}
