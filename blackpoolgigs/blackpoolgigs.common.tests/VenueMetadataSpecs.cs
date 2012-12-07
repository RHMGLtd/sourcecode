using blackpoolgigs.common.Models;
using Machine.Specifications;

namespace blackpoolgigs.common.tests
{
    public class when_metadata_is_empty
    {
        static VenueMetadata metadata;
        Because of = () => metadata = new VenueMetadata();
        It knows_it_is_empty = () => metadata.IsEmpty().ShouldBeTrue();
    }

    public class when_metadata_has_only_a_name
    {
        static VenueMetadata metadata;
        Because of = () => metadata = new VenueMetadata
                                          {
                                              VenueName = "a name"
                                          };
        It knows_it_is_empty = () => metadata.IsEmpty().ShouldBeTrue();
    }

    public class when_metadata_has_a_bar_and_club_reviews_url
    {
        static VenueMetadata metadata;
        Because of = () => metadata = new VenueMetadata
        {
            VenueName = "a name",
            BarAndClubReviewsUrl = "a url"
        };
        It knows_it_is_not_empty = () => metadata.IsEmpty().ShouldBeFalse();
    }
    public class when_metadata_has_a_main_image_name
    {
        static VenueMetadata metadata;
        Because of = () => metadata = new VenueMetadata
        {
            VenueName = "a name",
            MainImageName = "an image"
        };
        It knows_it_is_not_empty = () => metadata.IsEmpty().ShouldBeFalse();
    }
    public class when_metadata_has_an_empty_contact_details_object
    {
        static VenueMetadata metadata;
        Because of = () => metadata = new VenueMetadata
        {
            VenueName = "a name",
            ContactDetail = new ContactDetail()
        };
        It knows_it_is_empty = () => metadata.IsEmpty().ShouldBeTrue();
    }

    public class when_metadata_has_a_contact_details_object_with_address_lines
    {
        static VenueMetadata metadata;
        Because of = () => metadata = new VenueMetadata
        {
            VenueName = "a name",
            ContactDetail = new ContactDetail
                                {
                                    AddressLines = new[] { "line 1" }
                                }
        };
        It knows_it_is_not_empty = () => metadata.IsEmpty().ShouldBeFalse();
    }
    public class when_metadata_has_a_contact_details_object_with_a_contact_name
    {
        static VenueMetadata metadata;
        Because of = () => metadata = new VenueMetadata
        {
            VenueName = "a name",
            ContactDetail = new ContactDetail
            {
                ContactName = "a name"
            }
        };
        It knows_it_is_not_empty = () => metadata.IsEmpty().ShouldBeFalse();
    }
    public class when_metadata_has_a_contact_details_object_with_an_email_address
    {
        static VenueMetadata metadata;
        Because of = () => metadata = new VenueMetadata
        {
            VenueName = "a name",
            ContactDetail = new ContactDetail
            {
                EmailAddress = "an email"
            }
        };
        It knows_it_is_not_empty = () => metadata.IsEmpty().ShouldBeFalse();
    }
    public class when_metadata_has_a_contact_details_object_with_a_main_phone_number
    {
        static VenueMetadata metadata;
        Because of = () => metadata = new VenueMetadata
        {
            VenueName = "a name",
            ContactDetail = new ContactDetail
            {
                MainPhoneNumber = "a number"
            }
        };
        It knows_it_is_not_empty = () => metadata.IsEmpty().ShouldBeFalse();
    }
    public class when_metadata_has_a_contact_details_object_with_a_postcode
    {
        static VenueMetadata metadata;
        Because of = () => metadata = new VenueMetadata
        {
            VenueName = "a name",
            ContactDetail = new ContactDetail
            {
                PostCode = "a postcode"
            }
        };
        It knows_it_is_not_empty = () => metadata.IsEmpty().ShouldBeFalse();
    }
    public class when_metadata_has_a_contact_details_object_with_a_secondary_phone_number
    {
        static VenueMetadata metadata;
        Because of = () => metadata = new VenueMetadata
        {
            VenueName = "a name",
            ContactDetail = new ContactDetail
            {
                SecondaryPhoneNumbers = new[] { "a number" }
            }
        };
        It knows_it_is_not_empty = () => metadata.IsEmpty().ShouldBeFalse();
    }
    public class when_metadata_has_a_contact_details_object_with_a_website
    {
        static VenueMetadata metadata;
        Because of = () => metadata = new VenueMetadata
        {
            VenueName = "a name",
            ContactDetail = new ContactDetail
            {
                Website = "a website"
            }
        };
        It knows_it_is_not_empty = () => metadata.IsEmpty().ShouldBeFalse();
    }
    public class when_metadata_has_a_populated_google_map_object
    {
        static VenueMetadata metadata;
        Because of = () => metadata = new VenueMetadata
        {
            VenueName = "a name",
            MapCoords = new GoogleMapCoords
                            {
                                Lat = "a latitude",
                                Long = "a longitude"
                            }
        };
        It knows_it_is_not_empty = () => metadata.IsEmpty().ShouldBeFalse();
    }
}