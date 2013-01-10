using System.Collections.Generic;
using rhmg.StudioDiary.Raven.Indexes;

namespace rhmg.StudioDiary
{
    public class AccountsJobby
    {
        public static List<Booking> BookingsWithOutstandingOwings(IRepository<Booking> repository)
        {
            return repository.Get(x => x.HasOutstandingOwings && x.IsCancelled);
        }
        public static List<CustomerArrears.Result> CustomerArrears(IRepository<Booking> repository)
        {
            return repository.IndexGet<CustomerArrears.Result>("customerarrears");
        }
    }
}