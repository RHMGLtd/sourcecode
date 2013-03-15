using Raven.Client;

namespace rhmg.StudioDiary.InternalWeb.ViewModels
{
    public class ExtendedFormBookingModel : StandardFormBookingModel
    {
        public ExtendedFormBookingModel() { }
        public ExtendedFormBookingModel(Booking booking)
            : base(booking)
        {
            Address = booking.MainContact.Address;
            Postcode = booking.MainContact.Postcode;
            SecondaryContactName = booking.MainContact.SecondaryContactName;
            Age = booking.MainContact.Age;

            MakeUpSession = booking.MakeUpSession;
            Biscuits = booking.Biscuits;
            SongChoice = booking.SongChoice;
            NumberInParty = booking.NumberInParty;
            PizzaOnTheDay = booking.PizzaOnTheDay;
        }

        // contact stuff
        public string Address { get; set; }
        public string Postcode { get; set; }
        public string SecondaryContactName { get; set; }
        public string Age { get; set; }
        // booking specific stuff
        public bool MakeUpSession { get; set; }
        public bool Biscuits { get; set; }
        public string SongChoice { get; set; }
        public string NumberInParty { get; set; }
        public bool PizzaOnTheDay { get; set; }

        public override Booking CreateBooking(Product product, IDocumentSession session)
        {
            var contactId = ContactId;
            if (string.IsNullOrEmpty(ContactId))
            {
                var contact = createNewContact();
                contact.Address = Address;
                contact.Postcode = Postcode;
                contact.SecondaryContactName = SecondaryContactName;
                contact.Age = Age;
                contactId = contact.Save(session).Id;
            }
            var eqs = ExplodeAdditionalEquipment(session);
            var rooms = product.RoomsToBookOut(Room, session);
            var rate = GetRateToUse(rooms);
            var newBooking = ActuallyCreateBooking(Date, rate, eqs, contactId, rooms, product, session);
            newBooking.MakeUpSession = MakeUpSession;
            newBooking.Biscuits = Biscuits;
            newBooking.SongChoice = SongChoice;
            newBooking.NumberInParty = NumberInParty;
            newBooking.PizzaOnTheDay = PizzaOnTheDay;
            newBooking.Save(session);
            return newBooking;
        }
    }
}
