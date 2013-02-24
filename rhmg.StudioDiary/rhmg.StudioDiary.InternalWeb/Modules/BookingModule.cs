using System;
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
                                                                                                Rooms = session.Query<Room>().ToList(),
                                                                                                AvailableAdditionalEquipment = session.Query<AdditionalEquipment>().ToList()
                                                                                            }];
                                                              };
                Post["/rehearsal/{day}/{month}/{year}/newbooking"] = parameters =>
                                                               {
                                                                   var booking = this.Bind<MakeBookingModel>();
                                                                  /*Contact contact;
                                                                   if (string.IsNullOrEmpty(booking.ContactId))
                                                                   {
                                                                       // create a new contact
                                                                       contact = new Contact
                                                                                         {
                                                                                             EmailAddress =
                                                                                                 booking.EmailAddress,
                                                                                             Name = booking.BandName,
                                                                                             PhoneNumber =
                                                                                                 booking.PhoneNumber,
                                                                                             MainContactName =
                                                                                                 booking.MainContactName
                                                                                         };
                                                                       contact.Save(session);
                                                                   }
                                                                   else
                                                                   {
                                                                       contact = Contact.Get(booking.ContactId, session);
                                                                   }
                                                                   // create a new booking
                                                                   var newBooking = new Booking
                                                                                        {
                                                                                            Contacts =
                                                                                                new List<Contact>
                                                                                                    {contact},
                                                                                            StartTime = TimePart.FromString(booking.StartTime),
                                                                                            Length = TimePart.Duration(booking.StartTime, booking.EndTime)
                                                                                            
                                                                                        };*/
                                                                   return "success";
                                                               };
            }
        }
    }
}