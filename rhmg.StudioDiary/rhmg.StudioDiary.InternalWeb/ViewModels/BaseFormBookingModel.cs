using System;
using System.Collections.Generic;

namespace rhmg.StudioDiary.InternalWeb.ViewModels
{
    public class BaseFormBookingModel
    {
        public BaseFormBookingModel()
        {
            Rooms = new List<Room>();
        }

        public BaseFormBookingModel(Booking booking)
        {
            ProductFriendlyName = booking.Product.Name;
            BookingHint = booking.Product.BookingHint;
            BookingId = booking.Id;
            Date = booking.Date;
            StartTime = booking.StartTime.ToString();
            EndTime = TimePart.EndTime(booking.StartTime, booking.Length).ToString();
            Notes = Notes;
        }

        public string BookingId { get; set; }
        public string ProductFriendlyName { get; set; }
        public string BookingHint { get; set; }

        public DateTime Date { get; set; }
        public DayViewBookingLists CurrentBookings { get; set; }
        /// <summary>
        /// These are the rooms which are AVAILABLE to pick from, not the one which is associated with the booking per se
        /// </summary>
        public List<Room> Rooms { get; set; }

        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Notes { get; set; }
    }
}