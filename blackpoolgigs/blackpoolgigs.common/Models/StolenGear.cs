using System;

namespace blackpoolgigs.common.Models
{
    public class StolenGear
    {
        public StolenGear()
        {
            Created = DateTime.Now;
            ImageCaptions = new string[5];
            ImageNames = new string[5];
        }
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

        /*public StolenGearImage Image1 { get; set; }
        public StolenGearImage Image2 { get; set; }
        public StolenGearImage Image3 { get; set; }
        public StolenGearImage Image4 { get; set; }
        public StolenGearImage Image5 { get; set; }*/

        public string[] ImageNames { get; set; }
        public string[] ImageCaptions { get; set; }
    }
    public class StolenGearImage
    {
        public string ImageName { get; set; }
        public string ImageCaption { get; set; }
    }
}