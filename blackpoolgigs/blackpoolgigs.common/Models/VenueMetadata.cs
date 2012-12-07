using System.Linq;

namespace blackpoolgigs.common.Models
{
    public class VenueMetadata
    {
        public VenueMetadata()
        {
            ContactDetail = new ContactDetail();
            MapCoords = new GoogleMapCoords();
        }
        public string Id { get; set; }
        public string VenueName { get; set; }
        public string MainImageName { get; set; }
        public string BarAndClubReviewsUrl { get; set; }
        public ContactDetail ContactDetail { get; set; }
        public GoogleMapCoords MapCoords { get; set; }

        public bool IsEmpty()
        {
            if (!string.IsNullOrEmpty(MainImageName) ||
                !string.IsNullOrEmpty(BarAndClubReviewsUrl)||
                !ContactDetail.IsEmpty() ||
                !MapCoords.IsEmpty())
                return false;
            return true;
        }
    }

    public class ContactDetail
    {
        public ContactDetail()
        {
            AddressLines = new string[] {};
            SecondaryPhoneNumbers = new string[] {};
        }
        public string MainPhoneNumber { get; set; }
        public string[] SecondaryPhoneNumbers { get; set; }
        public string[] AddressLines { get; set; }
        public string Website { get; set; }
        public string EmailAddress { get; set; }
        public string PostCode { get; set; }
        public string ContactName { get; set; }

        public bool IsEmpty()
        {
            if (AddressLines.Any() ||
                SecondaryPhoneNumbers.Any() ||
                !string.IsNullOrEmpty(ContactName) ||
                !string.IsNullOrEmpty(EmailAddress) ||
                !string.IsNullOrEmpty(MainPhoneNumber) ||
                !string.IsNullOrEmpty(PostCode) ||
                !string.IsNullOrEmpty(Website))
                return false;
            return true;
        }
    }

    public class GoogleMapCoords
    {
        public string Lat { get; set; }
        public string Long { get; set; }

        public bool IsEmpty()
        {
            if (string.IsNullOrEmpty(Lat) ||
                string.IsNullOrEmpty(Long))
                return true;
            return false;
        }
    }
}