using System.Threading;
using Raven.Abstractions.Data;
using Raven.Client;

namespace coolbunny.ravendal.Extensions
{
    public static class ravenExtensions
    {
        public static void WaitForQueryToComplete(this IDocumentSession session, string indexName)
        {
            session.WaitForQueryToComplete(indexName, 500);
        }
        public static void WaitForQueryToComplete(this IDocumentSession session, string indexName, int sleepLength)
        {
            QueryResult results;
            do
            {
                //doesn't matter what the query is here, just want to see if it's stale or not
                results = session.Advanced.LuceneQuery<object>(indexName)
                              .Where("")
                              .QueryResult;

                if (results.IsStale)
                    Thread.Sleep(sleepLength);
            } while (results.IsStale);
        }
    }
}