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
            Rooms = new List<Room>();
            NumberRequired = new List<string>();
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
        public string Rate { get; set; }
        public double OneOffCharge { get; set; }
        public List<string> NumberRequired { get; set; }


        public virtual Booking CreateBooking(DateTime date, Product product, IDocumentSession session)
        {
            var contactId = !string.IsNullOrEmpty(ContactId) ? ContactId : createNewContact().Save(session).Id;
            var eqs = ExplodeAdditionalEquipment(session);
            var rooms = product.RoomsToBookOut(Room, session);
            var rate = GetRateToUse(rooms);
            var newBooking = ActuallyCreateBooking(date, rate, eqs, contactId, rooms, product);
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
                       ? rooms.First().Rates.First(x => x.Id == Rate)
                       : null;
        }
        protected virtual Booking ActuallyCreateBooking(DateTime date, Rate rate, List<AdditionalEquipment> eqs, string contactId, List<Room> rooms, Product product)
        {
            return new Booking
                       {
                           Date = date,
                           MainContactId = contactId,
                           StartTime = TimePart.FromString(StartTime),
                           Length = TimePart.Duration(StartTime, EndTime),
                           Rooms = rooms,
                           OneOffCharge = OneOffCharge,
                           Rate = rate,
                           Notes = new List<Note>
                                       {
                                           new Note
                                               {
                                                   Content = Notes
                                               }
                                       },
                           AdditionalEquipment = eqs
                       };
        }
    }
}