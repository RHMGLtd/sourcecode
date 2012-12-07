using System.Linq;

namespace blackpoolgigs.common.Models
{
    public class BandMetadata
    {
        public string Id { get; set; }

        public string BandName { get; set; }
        public string MainContactName { get; set; }
        public string[] OtherContacts { get; set; }
        public string ContactEmail { get; set; }
        public string AverageAge { get; set; }
        public string Bio { get; set; }
        public string Style { get; set; }
        public int CoverPercentage { get; set; }
        public int OriginalPercentage { get; set; }
        public string MySpaceLink { get; set; }
        public string FacebookLink { get; set; }
        public string[] OtherLinks { get; set; }
        public string NumberOfBandMembers { get; set; }
        public string HomeTown { get; set; }

        public BandMetadata()
        {
            OtherContacts = new string[] { };
            OtherLinks = new string[] { };
        }

        public BandMetadata(string bandName)
        {
            BandName = bandName;
        }

        public bool IsEmpty()
        {
            if (!string.IsNullOrEmpty(MainContactName) ||
                !string.IsNullOrEmpty(ContactEmail) ||
                OtherContacts != null ||
                !string.IsNullOrEmpty(AverageAge) ||
                !string.IsNullOrEmpty(Bio) ||
                !string.IsNullOrEmpty(Style) ||
                !string.IsNullOrEmpty(MySpaceLink) ||
                !string.IsNullOrEmpty(FacebookLink) ||
                !string.IsNullOrEmpty(NumberOfBandMembers) ||
                !string.IsNullOrEmpty(HomeTown) ||
                OtherLinks != null)
                return false;
            return true;
        }
    }
}