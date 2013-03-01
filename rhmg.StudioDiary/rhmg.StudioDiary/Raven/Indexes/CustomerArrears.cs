using System.Linq;
using Raven.Client.Indexes;

namespace rhmg.StudioDiary.Raven.Indexes
{
    public class CustomerArrears : AbstractIndexCreationTask<Booking, CustomerArrears.Result>
    {
        public class Result
        {
            public string ContactId { get; set; }
            public double AmountOwed { get; set; }
            public Contact Contact { get; set; }
            public string[] BookingIds { get; set; }
        }

        public CustomerArrears()
        {
            Map = docs => from booking in docs
                          where booking.IsCancelled && booking.HasOutstandingOwings
                          select new
                                     {
                                         ContactId = booking.MainContactId,
                                         AmountOwed = booking.CurrentlyOwed,
                                         BookingIds = new[] { booking.Id }
                                     };
            Reduce = results => from result in results
                                group result by result.ContactId
                                    into g
                                    select new
                                           {
                                               ContactId = g.Key,
                                               AmountOwed = g.Sum(x => x.AmountOwed),
                                               BookingIds = from b in g.SelectMany(x => x.BookingIds)
                                                            group b by b
                                                                into b2
                                                                select b2.Key
                                           };
            TransformResults =
            (database, results) => from result in results
                                   let contact = database.Load<Contact>(result.ContactId)
                                     select new
                                     {
                                         result.ContactId,
                                         result.AmountOwed,
                                         Contact = contact,
                                         result.BookingIds
                                     };
        }
    }
}