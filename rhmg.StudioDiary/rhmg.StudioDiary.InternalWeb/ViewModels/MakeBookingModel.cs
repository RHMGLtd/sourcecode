using System;
using System.Collections.Generic;

namespace rhmg.StudioDiary.InternalWeb.ViewModels
{
    public class MakeBookingModel
    {
        public MakeBookingModel()
        {
            Rooms = new List<Room>();
        }
        public DateTime Date { get; set; }
        public DayViewBookingLists CurrentBookings { get; set; }
        public List<AdditionalEquipment> AvailableAdditionalEquipment { get; set; } 
        public List<Room> Rooms { get; set; }

        public string ContactId { get; set; }
        public string PhoneNumber { get; set; }
        public string MainContactName { get; set; }
        public string BandName { get; set; }
        public string EmailAddress { get; set; }
        public string Room { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public List<Note> Notes { get; set; }
    }
}