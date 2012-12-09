using System.Collections.Generic;
using System.Linq;

namespace rhmg.StudioDiary
{
    public class Contact : Entity
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public List<Booking> Bookings { get; set; }



        public Contact Save(IRepository<Contact> contacts)
        {
            if (contacts.Exists(x => x.PhoneNumber == PhoneNumber))
                return this;
            return contacts.Put(this);
        }

        public static Contact Get(string id, IRepository<Contact> contacts, IRepository<Booking> bookings)
        {
            var contact = contacts.Get(id);
            contact.Bookings = bookings.Get(x => x.Contacts.Any(y => y.Id == id));
            return contact;
        }

        public double CurrentlyOwed(IRepository<Booking> bookings)
        {
            return bookings.Get(x => x.Contacts.Any(y => y.Id == Id) && x.HasOutstandingOwings).Sum(z => z.Outstanding());
        }
    }
}