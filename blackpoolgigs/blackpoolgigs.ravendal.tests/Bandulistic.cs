using System.Collections.Generic;
using System.Linq;
using blackpoolgigs.common.Models;
using blackpoolgigs.ravendal.Indexes;
using blackpoolgigs.ravendal.tests.contexts;
using coolbunny.common.Helpers;
using Machine.Specifications;
using Bands = blackpoolgigs.ravendal.Providers.Bands;

namespace blackpoolgigs.ravendal.tests
{
    public class when_retrieving_a_band_metadata : with_raven_integration<Bands, BandMetadata>
    {
        Establish context = () =>
        {
            session.Store(new BandMetadata
            {
                BandName = "a test band"
            });
            session.SaveChanges();
        };

        It returns_for_matching_band_name = () => Service.GetMetadata(new BandName { Value = "a test band" }).BandName.ShouldEqual("a test band");
        It returns_for_non_matching_band_name = () => Service.GetMetadata(new BandName { Value = "another test band" }).BandName.ShouldEqual("another test band");
    }
    public class when_retrieving_a_band_metadata_with_info : with_raven_integration<Bands, BandMetadata>
    {
        static BandMetadata result;
        Establish context = () =>
        {
            session.Store(new BandMetadata
            {
                BandName = "a test band",
                MainContactName = "a person"
            });
            session.SaveChanges();
        };

        Because of = () => result = Service.GetMetadata(new BandName { Value = "a test band" });

        It returns_for_matching_band_name = () => result.BandName.ShouldEqual("a test band");
        It returns_for_non_matching_band_name = () => result.MainContactName.ShouldEqual("a person");
    }
    public class when_saving_a_new_band_metadata : with_raven_integration<Bands, BandMetadata>
    {
        static BandMetadata result;
        Because of = () => result = Service.Save(new BandMetadata
                                            {
                                                BandName = "a band",
                                                MainContactName = "a person"
                                            });

        It has_given_an_id = () => result.Id.ShouldEqual("band/1");
    }

    public class when_saving_an_edit_to_a_band_metadata : with_raven_integration<Bands, BandMetadata>
    {
        static BandMetadata result;

        Establish context = () =>
            {
                result = new BandMetadata();
                session.Store(result);
                session.SaveChanges();
            };

        Because of = () =>
            {
                result.MainContactName = "a person";
                result = Service.Save(result);
            };

        It has_given_an_id = () => result.Id.ShouldEqual("band/1");
        It has_saved_the_contact_name = () => result.MainContactName.ShouldEqual("a person");
    }

    public class when_retrieving_band_urls : with_raven_client_index<common.Models.BandUrls>
    {
        static BandMetadata input;
        static List<common.Models.BandUrls> result;

        Establish context = () =>
        {
            input = new BandMetadata
                         {
                             FacebookLink = "http://www.bob.com",
                             MySpaceLink = "http://www.bruce.com",
                             OtherLinks = new[] { "http://www.jenny.com", "http://www.june.com" }
                         };
            session.Store(input);
            session.SaveChanges();
        };

        Because of = () =>
            {
                result = session.Advanced.LuceneQuery<common.Models.BandUrls>("BandUrls").WaitForNonStaleResults().ToList();
            };

        It has_one_result = () => result.Count().ShouldEqual(1);
    }

    public class when_retrieving_band_urls_through_service : with_raven_integration<Bands, common.Models.BandUrls>
    {
        static BandMetadata input;
        static PageOfResults<common.Models.BandUrls> result;

        Establish context = () =>
        {
            input = new BandMetadata
            {
                FacebookLink = "http://www.bob.com",
                MySpaceLink = "http://www.bruce.com",
                OtherLinks = new[] { "http://www.jenny.com", "http://www.june.com" }
            };
            session.Store(input);
            session.SaveChanges();
            
        };

        Because of = () =>
            {
                result = Service.GetBandUrls(new PagingParams(1, 10, "", ""));
            };

        It has_one_record = () => result.TotalNumberOfRecords.ShouldEqual(1);
    }
}