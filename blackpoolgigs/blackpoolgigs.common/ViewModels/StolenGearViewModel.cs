using System;
using System.Web;
using blackpoolgigs.common.Models;

namespace blackpoolgigs.common.ViewModels
{
    public class StolenGearViewModel
    {
        public StolenGearViewModel()
        {
            Created = DateTime.Now;
        }

        public StolenGearViewModel(StolenGear input)
        {
            if (input.ImageNames.Length != 5 ||
                input.ImageCaptions.Length != 5)
                throw new ArgumentOutOfRangeException();
            Id = input.Id;
            Headline = input.Headline;
            WhatIsIt = input.WhatIsIt;
            WhereWasItStolenFrom = input.WhereWasItStolenFrom;
            WhoToContact = input.WhoToContact;
            ContactEmail = input.ContactEmail;
            ContactPhone = input.ContactPhone;
            RewardOfferedIs = input.RewardOfferedIs;
            WhenWasItStolen = input.WhenWasItStolen;
            Recovered = input.Recovered;
            Archived = input.Archived;
            Created = input.Created;
            Edited = input.Edited;
            ImageCaption1 = input.ImageCaptions[0];
            ImageCaption2 = input.ImageCaptions[1];
            ImageCaption3 = input.ImageCaptions[2];
            ImageCaption4 = input.ImageCaptions[3];
            ImageCaption5 = input.ImageCaptions[4];
            ImageName1 = input.ImageNames[0];
            ImageName2 = input.ImageNames[1];
            ImageName3 = input.ImageNames[2];
            ImageName4 = input.ImageNames[3];
            ImageName5 = input.ImageNames[4];
        }

        public string ImageName1 { get; set; }
        public string ImageCaption1 { get; set; }
        public HttpPostedFileBase Image1 { get; set; }

        public string ImageName2 { get; set; }
        public string ImageCaption2 { get; set; }
        public HttpPostedFileBase Image2 { get; set; }

        public string ImageName3 { get; set; }
        public string ImageCaption3 { get; set; }
        public HttpPostedFileBase Image3 { get; set; }

        public string ImageName4 { get; set; }
        public string ImageCaption4 { get; set; }
        public HttpPostedFileBase Image4 { get; set; }

        public string ImageName5 { get; set; }
        public string ImageCaption5 { get; set; }
        public HttpPostedFileBase Image5 { get; set; }

        public string Id { get; set; }
        public string Headline { get; set; }
        public string WhatIsIt { get; set; }
        public string WhereWasItStolenFrom { get; set; }
        public string WhoToContact { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string RewardOfferedIs { get; set; }
        public DateTime WhenWasItStolen { get; set; }

        public bool Recovered { get; set; }
        public bool Archived { get; set; }
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }

        public StolenGear AsGear()
        {
            return new StolenGear
                       {
                           Id = Id,
                           Archived = Archived,
                           ContactEmail = ContactEmail,
                           ContactPhone = ContactPhone,
                           Created = Created,
                           Edited = Edited,
                           Headline = Headline,
                           Recovered = Recovered,
                           RewardOfferedIs = RewardOfferedIs,
                           WhatIsIt = WhatIsIt,
                           WhenWasItStolen = WhenWasItStolen,
                           WhereWasItStolenFrom = WhereWasItStolenFrom,
                           WhoToContact = WhoToContact,
                           ImageCaptions = new[]
                                               {
                                                   ImageCaption1,
                                                   ImageCaption2,
                                                   ImageCaption3,
                                                   ImageCaption4,
                                                   ImageCaption5
                                                },
                           ImageNames = new[]
                                               {
                                                    Image1 == null ? ImageName1 : Image1.FileName,
                                                    Image2 == null ? ImageName2 : Image2.FileName,
                                                    Image3 == null ? ImageName3 : Image3.FileName,
                                                    Image4 == null ? ImageName4 : Image4.FileName,
                                                    Image5 == null ? ImageName5 : Image5.FileName
                                               }
                       };
        }
    }
}