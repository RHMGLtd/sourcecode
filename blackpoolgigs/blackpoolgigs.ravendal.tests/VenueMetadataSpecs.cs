using System.Collections.Generic;
using System.Linq;
using blackpoolgigs.common.Contracts;
using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.Models;
using blackpoolgigs.ravendal.Providers;
using blackpoolgigs.ravendal.tests.contexts;
using coolbunny.common.Helpers;
using coolbunny.ravendal.Extensions;
using Machine.Specifications;

namespace blackpoolgigs.ravendal.tests
{
    public class when_getting_diaryplusmetadata : with_raven_integration<VenueProvider, VenueDiaryPlusMetaData>
    {
        static PageOfResults<VenueDiaryPlusMetaData> result;
        Establish context = () =>
            {
                Stub<ISummariseVenues>()
                    .Setup(x => x.Metadata())
                    .Returns(new List<VenueMetadata>
                                 {
                                     new VenueMetadata
                                         {
                                             VenueName = "a venue",
                                             ContactDetail = new ContactDetail
                                                                 {
                                                                     EmailAddress = "an email address",
                                                                     AddressLines = new[] {"line 1", "line 2"},
                                                                     MainPhoneNumber = "a phone number",
                                                                     PostCode = "postcode",
                                                                     Website = "website link"
                                                                 },
                                             MainImageName = "something.jpg",
                                             MapCoords = new GoogleMapCoords
                                                             {
                                                                 Lat = "lat",
                                                                 Long = "long"
                                                             }
                                         }
                                 });
                session.Store(new Gig
                                  {
                                      Venue = "a venue"
                                  });
                session.SaveChanges();
                session.WaitForQueryToComplete("Venues");
            };

        Because of = () => result = Service.DiaryPlusMetadata(new PagingParams(1, 10, "", ""));
        It has_returned_one_result = () => result.Results.Count().ShouldEqual(1);
        It has_retuned_a_gig_in_that_result = () => result.Results.FirstOrDefault().Diary.Venue.ShouldEqual("a venue");

        It has_returned_metadata_in_that_result =
            () => result.Results.FirstOrDefault().Metadata.IsEmpty().ShouldBeFalse();
    }
    public class when_getting_diaryplusmetadata_when_no_matching_metadata : with_raven_integration<VenueProvider, VenueDiaryPlusMetaData>
    {
        static PageOfResults<VenueDiaryPlusMetaData> result;
        Establish context = () =>
        {
            session.Store(new Gig
            {
                Venue = "a venue"
            });
            session.SaveChanges();
            session.WaitForQueryToComplete("Venues/Metadata");
            session.WaitForQueryToComplete("Venues");
        };

        Because of = () => result = Service.DiaryPlusMetadata(new PagingParams(1, 10, "", ""));
        It has_returned_one_result = () => result.Results.Count().ShouldEqual(1);
        It has_retuned_a_gig_in_that_result = () => result.Results.FirstOrDefault().Diary.Venue.ShouldEqual("a venue");

        It has_returned_metadata_in_that_result =
            () => result.Results.FirstOrDefault().Metadata.IsEmpty().ShouldBeTrue();
    }
    public class VenueMetadataSpecs : with_raven_client_index<VenueMetadata>
    {
        static VenueMetadata metadata;
        Establish context = () =>
            {
                session.Store(new VenueMetadata
                                  {
                                      ContactDetail = new ContactDetail
                                                          {
                                                              EmailAddress = "an email address",
                                                              AddressLines = new[] { "line 1", "line 2" },
                                                              MainPhoneNumber = "a phone number",
                                                              PostCode = "postcode",
                                                              Website = "website link"
                                                          },
                                      MainImageName = "something.jpg",
                                      MapCoords = new GoogleMapCoords
                                                      {
                                                          Lat = "lat",
                                                          Long = "long"
                                                      }
                                  });
                session.SaveChanges();
            };

        Because of = () => metadata = reading_index("Venues/Metadata").FirstOrDefault();
        It should_have_returned_a_metadata = () => metadata.ShouldNotBeNull();
        It should_have_an_id = () => metadata.Id.ShouldEqual("metadata/1");

        It should_have_the_correct_email_address =
            () => metadata.ContactDetail.EmailAddress.ShouldEqual("an email address");
    }
    public class when_loading_metadata_that_is_not_stored : with_raven_integration<VenueProvider, VenueMetadata>
    {
        static VenueMetadata metadata;
        Because of = () => metadata = Service.GetMetadata(new VenueName { Value = "something" });
        It is_not_null = () => metadata.ShouldNotBeNull();
        It has_the_correct_venue_name = () => metadata.VenueName.ShouldEqual("something");
    }
    public class when_loading_metadata_that_is_stored : with_raven_integration<VenueProvider, VenueMetadata>
    {
        static VenueMetadata metadata;

        Establish context = () =>
        {
            session.Store(new VenueMetadata
            {
                VenueName = "something",
                ContactDetail = new ContactDetail
                {
                    EmailAddress = "an email address",
                    AddressLines = new[] { "line 1", "line 2" },
                    MainPhoneNumber = "a phone number",
                    PostCode = "postcode",
                    Website = "website link"
                },
                MainImageName = "something.jpg",
                MapCoords = new GoogleMapCoords
                {
                    Lat = "lat",
                    Long = "long"
                }
            });
            session.SaveChanges();
            session.WaitForQueryToComplete("Venues/Metadata");
        };
        Because of = () => metadata = Service.GetMetadata(new VenueName { Value = "something" });
        It is_not_null = () => metadata.ShouldNotBeNull();
        It has_the_correct_venue_name = () => metadata.VenueName.ShouldEqual("something");
        It has_the_correct_id = () => metadata.Id.ShouldEqual("metadata/1");
    }
}