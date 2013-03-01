using System;
using System.Collections.Generic;
using System.Linq;

namespace rhmg.StudioDiary.InternalWeb.ViewModels
{
    public class MakeBookingModel
    {
        public MakeBookingModel()
        {
            Rooms = new List<Room>();
            NumberRequired = new List<string>();
        }
        public DateTime Date { get; set; }
        public DayViewBookingLists CurrentBookings { get; set; }
        List<AdditionalEquipment> _additionalEquipment;
        public List<AdditionalEquipment> AvailableAdditionalEquipment
        {
            get { return _additionalEquipment; }
            set
            {
                _additionalEquipment = value.Select(x => new AdditionalEquipment
                                                             {
                                                                 Description = x.Description,
                                                                 Id = x.Id.Replace("additionalequipment/", "eq"),
                                                                 UnitCost = x.UnitCost
                                                             }).ToList();
            }
        }
        public List<Room> Rooms { get; set; }

        public string ContactId { get; set; }
        public string PhoneNumber { get; set; }
        public string MainContactName { get; set; }
        public string BandName { get; set; }
        public string EmailAddress { get; set; }
        public string Room { get; set; }
        public string Rate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Notes { get; set; }
        public List<string> NumberRequired { get; set; }

        public Dictionary<string, int> ExplodeAdditionalEquipment()
        {
            return NumberRequired.ToDictionary(eq => "additionalequipment/" + eq.Split('_')[0].Replace("eq", ""), eq => int.Parse(eq.Split('_')[1]));
        }
    }
}