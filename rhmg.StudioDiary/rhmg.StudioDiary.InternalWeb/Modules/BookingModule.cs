using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.ModelBinding;
using rhmg.StudioDiary.InternalWeb.ViewModels;
using rhmg.StudioDiary.Raven;

namespace rhmg.StudioDiary.InternalWeb.Modules
{
    public class BookingModule : NancyModule
    {
        public BookingModule(IRavenSessionProvider store)
        {
            using (var session = store.GetSession())
            {
                Get["/rehearsal/{day}/{month}/{year}/newbooking/"] = parameters =>
                                                              {
                                                                  var date = new DateTime(parameters.year, parameters.month, parameters.day);
                                                                  return View[new MakeBookingModel
                                                                                            {
                                                                                                Date =
                                                                                                    date,
                                                                                                CurrentBookings = DiaryManager.DayCheck(date, session),
                                                                                                Rooms = Room.All(session),
                                                                                                AvailableAdditionalEquipment = AdditionalEquipment.All(session)
                                                                                            }];
                                                              };
                Post["/rehearsal/{day}/{month}/{year}/newbooking"] = parameters =>
                                                               {
                                                                   var booking = this.Bind<MakeBookingModel>();
                                                                   string contactId;
                                                                   if (string.IsNullOrEmpty(booking.ContactId))
                                                                   {
                                                                       // create a new contact
                                                                       contactId = new Contact
                                                                       {
                                                                           EmailAddress =
                                                                               booking.EmailAddress,
                                                                           Name = booking.BandName,
                                                                           PhoneNumber =
                                                                               booking.PhoneNumber,
                                                                           MainContactName =
                                                                               booking.MainContactName
                                                                       }.Save(session).Id;
                                                                   }
                                                                   else
                                                                   {
                                                                       contactId = booking.ContactId;
                                                                   }
                                                                   var eqs = new List<AdditionalEquipment>();
                                                                   foreach (var i in booking.ExplodeAdditionalEquipment())
                                                                   {
                                                                       var eq = session.Load<AdditionalEquipment>(i.Key);
                                                                       for (var j = 0; j < i.Value; j++)
                                                                           eqs.Add(eq);
                                                                   }
                                                                   // create a new booking
                                                                   var room = session.Load<Room>(booking.Room);
                                                                   var newBooking = new Booking
                                                                                        {
                                                                                            Date = new DateTime(parameters.year, parameters.month, parameters.day),
                                                                                            MainContactId = contactId,
                                                                                            StartTime = TimePart.FromString(booking.StartTime),
                                                                                            Length = TimePart.Duration(booking.StartTime, booking.EndTime),
                                                                                            Room = room,
                                                                                            Rate = room.Rates.First(x => x.Id == booking.Rate),
                                                                                            Notes = new List<Note>
                                                                                                        {
                                                                                                            new Note
                                                                                                                {
                                                                                                                    Content = booking.Notes
                                                                                                                }
                                                                                                        },
                                                                                            AdditionalEquipment = eqs
                                                                                        };
                                                                   newBooking.Save(session);
                                                                   return Response.AsRedirect("/");
                                                               };
            }
        }
    }
}