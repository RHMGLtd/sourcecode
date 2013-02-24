using System.Linq;
using Raven.Client.Indexes;

namespace rhmg.StudioDiary.Raven.Indexes
{
    public class Bookings_ByContact : AbstractIndexCreationTask<Booking, Bookings_ByContact.Result>
    {
        public class Result
        {
            public string ContactId { get; set; }
            public string[] BookingIds { get; set; }
        }
        public Bookings_ByContact()
        {
            Map = docs => from booking in docs
                          where !booking.IsCancelled
                          from contact in booking.Contacts
                          select new
                          {
                              ContactId = contact.Id,
                              BookingIds = new[] { booking.Id }
                          };
            Reduce = results => from result in results
                                group result by result.ContactId
                                    into g
                                    select new
                                    {
                                        ContactId = g.Key,
                                        BookingIds = from b in g.SelectMany(x => x.BookingIds)
                                                   group b by b
                                                   into b2
                                                   select b2.Key
                                    };
        }
    }
}