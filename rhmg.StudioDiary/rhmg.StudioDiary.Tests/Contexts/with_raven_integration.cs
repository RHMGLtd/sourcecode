using System.Collections.Generic;
using Machine.Specifications;
using Raven.Client;
using Raven.Client.Embedded;
using rhmg.StudioDiary.Raven;

namespace rhmg.StudioDiary.Tests.Contexts
{
    public abstract class with_raven_integration<TService, TDocument> : with_service<TService>
        where TService : class
        where TDocument : new()
    {
        protected static EmbeddableDocumentStore store;
        protected static IDocumentSession session;
        Cleanup after_each = () => store.Dispose();

        Establish ravendb = Make;

        public static void Make()
        {
            with_automocking.Make();
            store = (EmbeddableDocumentStore)DocumentStoreFactory.GetInMemoryDocumentStoreForUnitTest();
            autoMocker.Inject<IDocumentStore>(store);
            autoMocker.Inject(session = store.OpenSession());
        }

        protected static IEnumerable<object> index_results;

        protected static string metadata(string identity, string metadata)
        {
            return session.Advanced.GetMetadataFor(session.Load<TDocument>(identity))
                .Value<string>(metadata);
        }

        protected static IEnumerable<object> reading_index(string name)
        {
            return index_results = reading_index<object>(name);
        }

        public static IEnumerable<T> reading_index<T>(string name)
        {
            var result = session.Advanced.LuceneQuery<T>(name).WaitForNonStaleResults();
            return result;
        }

        protected static void clear_session()
        {
            //Clears this instance.  Remove all entities from the delete queue and stops tracking changes for all entities.
            session.Advanced.Clear();
        }

        protected static void EvictExistingFromMemoryToAvoidNonUniqueObjectException(TDocument existing)
        {
            if (existing != null)
                session.Advanced.Evict(existing);
        }
    }
}