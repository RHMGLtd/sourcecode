using System.Collections.Generic;
using System.Linq;
using coolbunny.ravendal.Extensions;
using Machine.Specifications;
using Raven.Client;
using Raven.Client.Embedded;

namespace blackpoolgigs.ravendal.tests.contexts
{
    public abstract class with_raven_client_index<TIndexResult>
    {
        protected static IDocumentSession session;
        protected static TIndexResult[] index_results;
        static EmbeddableDocumentStore store;

        Establish ravendb = () =>
        {
            store = (EmbeddableDocumentStore)DocumentStoreFactory.GetInMemoryDocumentStoreForUnitTest();
            session = store.OpenSession();
        };

        protected static IEnumerable<TIndexResult> reading_index(string name)
        {
            session.WaitForQueryToComplete(name);
            return index_results = session.Advanced.LuceneQuery<TIndexResult>(name).ToArray();
        }
    }
}