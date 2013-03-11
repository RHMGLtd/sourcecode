using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;

namespace rhmg.StudioDiary.InternalWeb.ViewModels
{
    public class StandardFormBookingModel : BaseFormBookingModel
    {
        public StandardFormBookingModel()
        {
            NumberRequired = new List<string>();
            RatesToPickFromMaybe = new List<Rate>();
        }

        public StandardFormBookingModel(Booking booking)
            : base(booking)
        {
            ContactId = booking.MainContactId;
            PhoneNumber = booking.MainContact.PhoneNumber;
            MainContactName = booking.MainContact.MainContactName;
            BandName = booking.MainContact.Name;
            EmailAddress = booking.MainContact.EmailAddress;
            if (booking.Product.Type == Product.ProductType.CanPickFrom)
                Room = booking.Rooms.FirstOrDefault().Id;
            RateId = booking.Rate == null ? null : booking.Rate.Id;
            RateDescription = booking.Rate == null ? null : booking.Rate.Description;
            RatesToPickFromMaybe = booking.Rate == null
                                       ? new List<Rate>()
                                       : booking.Rooms.FirstOrDefault().Rates.ToList();
            OneOffCharge = booking.OneOffCharge;
        }

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

        public string ContactId { get; set; }
        public string PhoneNumber { get; set; }
        public string MainContactName { get; set; }
        public string BandName { get; set; }
        public string EmailAddress { get; set; }
        public string Room { get; set; }
        public string RateId { get; set; }
        public string RateDescription { get; set; }
        public double OneOffCharge { get; set; }
        public List<string> NumberRequired { get; set; }

        public List<Rate> RatesToPickFromMaybe { get; set; }

        public virtual Booking CreateBooking(Product product, IDocumentSession session)
        {
            var contactId = !string.IsNullOrEmpty(ContactId) ? ContactId : createNewContact().Save(session).Id;
            var eqs = ExplodeAdditionalEquipment(session);
            var rooms = product.RoomsToBookOut(Room, session);
            var rate = GetRateToUse(rooms);
            var newBooking = ActuallyCreateBooking(Date, rate, eqs, contactId, rooms, product, session);
            newBooking.Save(session);
            return newBooking;
        }

        public virtual List<AdditionalEquipment> ExplodeAdditionalEquipment(IDocumentSession session)
        {
            var list = NumberRequired.ToDictionary(eq => "additionalequipment/" + eq.Split('_')[0].Replace("eq", ""), eq => int.Parse(eq.Split('_')[1]));
            var eqs = new List<AdditionalEquipment>();
            foreach (var i in list)
            {
                var eq = session.Load<AdditionalEquipment>(i.Key);
                for (var j = 0; j < i.Value; j++)
                    eqs.Add(eq);
            }
            return eqs;
        }
        internal Contact createNewContact()
        {
            return new Contact
                       {
                           EmailAddress =
                               EmailAddress,
                           Name = BandName,
                           PhoneNumber =
                               PhoneNumber,
                           MainContactName =
                               MainContactName
                       };
        }
        protected Rate GetRateToUse(List<Room> rooms)
        {
            return (OneOffCharge == 0.00)
                       ? rooms.First().Rates.First(x => x.Id == RateId)
                       : null;
        }
        protected virtual Booking ActuallyCreateBooking(DateTime date, Rate rate, List<AdditionalEquipment> eqs, string contactId, List<Room> rooms, Product product, IDocumentSession session)
        {
            var booking = string.IsNullOrEmpty(BookingId) ? new Booking() : session.Load<Booking>(BookingId);
            booking.Date = date == DateTime.MinValue ? Date : date;
            booking.MainContactId = contactId;
            booking.StartTime = TimePart.FromString(StartTime);
            booking.Length = TimePart.Duration(StartTime, EndTime);
            booking.Rooms = rooms;
            booking.OneOffCharge = OneOffCharge;
            booking.Rate = rate;
            booking.Product = product;
            booking.Notes = new List<Note>
                                {
                                    new Note
                                        {
                                            Content = Notes
                                        }
                                };
            booking.AdditionalEquipment = eqs;
            return booking;
        }
    }
}