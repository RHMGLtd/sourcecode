using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;

namespace rhmg.StudioDiary.InternalWeb.ViewModels
{
    public class AbbreviatedFormBookingModel : BaseFormBookingModel
    {
        public Booking CreateBooking(Product product, IDocumentSession session)
        {
            var rate = session.Query<Rate>().FirstOrDefault(x => x.Description == "***SYSTEM USE ONLY***") ?? new Rate
            {
                Description = "***SYSTEM USE ONLY***",
                Per = new TimeSpan(0),
                PoundsAmount = 0
            }.Save(session);

            var booking = new Booking
            {
                Product = product,
                Date = Date,
                StartTime = TimePart.FromString(StartTime),
                Length = TimePart.Duration(StartTime, EndTime),
                Rooms = product.RoomsToBookOut(string.Empty, session),
                Notes = new List<Note> { new Note { Content = Notes } },
                Rate = rate
            };
            booking.Save(session);
            return booking;
        }
    }
}