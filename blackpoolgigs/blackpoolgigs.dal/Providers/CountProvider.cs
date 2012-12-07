using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.Models;
using Raven.Client;

namespace blackpoolgigs.ravendal.Providers
{
    public class CountProvider : IJustDoCounts
    {
        protected readonly IDocumentSession session;

        public CountProvider(IDocumentSession session)
        {
            this.session = session;
        }

        public int Gigs
        {
            get { return session.Advanced.LuceneQuery<Gig>().WaitForNonStaleResults().QueryResult.TotalResults; }
        }

        public int RepeatingGigs
        {
            get { return session.Advanced.LuceneQuery<RepeatingGig>().WaitForNonStaleResults().QueryResult.TotalResults; }
        }

        public int StolenGear
        {
            get { return session.Advanced.LuceneQuery<StolenGear>().WaitForNonStaleResults().QueryResult.TotalResults; }

        }
    }
}