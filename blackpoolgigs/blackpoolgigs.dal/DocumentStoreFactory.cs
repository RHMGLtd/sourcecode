using System.Reflection;
using blackpoolgigs.ravendal.Indexes;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using Raven.Client.Extensions;
using Raven.Client.Indexes;

namespace blackpoolgigs.ravendal
{
    public class DocumentStoreFactory
    {
        public static IDocumentStore GetDocumentStore(int portNumber)
        {
            return GetOutOfProcessDocumentStore();
        }

        private static IDocumentStore GetOutOfProcessDocumentStore()
        {
            var store = new DocumentStore()
            {
                Url = "http://rhmg.raven:8080",
                DefaultDatabase = "blackpoolgigs",
                Conventions = DocumentConventionFactory.getConvention()
            };
            store.Initialize();
            store.DatabaseCommands.EnsureDatabaseExists("blackpoolgigs");
            IndexCreation.CreateIndexes(Assembly.GetAssembly(typeof(Gigs_ByDate)), store);
            return store;
        }
        private static IDocumentStore GetEmbeddedDocumentStore(int portNumber)
        {
            var store = new EmbeddableDocumentStore()
                            {
                                DataDirectory = "Data",
                                Conventions = DocumentConventionFactory.getConvention(),
                                Configuration = { Port = portNumber },
                                UseEmbeddedHttpServer = true
                            };
            store.Initialize();
            IndexCreation.CreateIndexes(Assembly.GetAssembly(typeof(Gigs_ByDate)), store);
            return store;
        }
        private static IDocumentStore GetInMemoryDocumentStore()
        {
            var store = new EmbeddableDocumentStore
                            {
                                RunInMemory = true,
                                Conventions = DocumentConventionFactory.getConvention(),
                                Configuration = { Port = 3137 },
                                UseEmbeddedHttpServer = true
                            };
            store.Initialize();

            IndexCreation.CreateIndexes(Assembly.GetAssembly(typeof(Gigs_ByDate)), store);

            return store;
        }

        public static IDocumentStore GetInMemoryDocumentStoreForUnitTest()
        {
            var store = new EmbeddableDocumentStore
            {
                RunInMemory = true,
                Conventions = DocumentConventionFactory.getConvention(),
                Configuration = { Port = 3137 },
                UseEmbeddedHttpServer = false
            };
            store.Initialize();

            IndexCreation.CreateIndexes(Assembly.GetAssembly(typeof(Gigs_ByDate)), store);

            return store;
        }
    }
}