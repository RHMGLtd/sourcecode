using System.Linq;
using Raven.Client.Indexes;

namespace rhmg.StudioDiary.Raven.Indexes
{
    public class CustomerArrears : AbstractIndexCreationTask<Booking, CustomerArrears.Result>
    {
        public class Result
        {
            public string Contact { get; set; }
            public double AmountOwed { get; set; }
        }

        public CustomerArrears()
        {
            Map = docs => from booking in docs
                          where booking.IsCancelled && booking.HasOutstandingOwings
                          from contact in booking.Contacts
                          select new
                                     {
                                         Contact = contact.Id,
                                         AmountOwed = booking.CurrentlyOwed
                                     };
            Reduce = results => from result in results
                                group result by
                                new
                                {
                                    result.Contact
                                }
                                    into g
                                    select new
                                           {
                                               g.Key.Contact,
                                               AmountOwed = g.Sum(x => x.AmountOwed)
                                           };
        }
    }
}