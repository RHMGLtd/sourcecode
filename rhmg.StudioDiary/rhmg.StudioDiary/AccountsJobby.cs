using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Raven.Client.Linq;
using rhmg.StudioDiary.Raven.Indexes;

namespace rhmg.StudioDiary
{
    public class AccountsJobby
    {
        public static List<Booking> BookingsWithOutstandingOwings(IDocumentSession session)
        {
            return session.Query<Booking>().Where(x => x.HasOutstandingOwings && x.IsCancelled).ToList();
        }
        public static List<CustomerArrears.Result> CustomerArrears(IDocumentSession session)
        {
            return session.Query<CustomerArrears.Result>("customerarrears").ToList();
        }
    }
}