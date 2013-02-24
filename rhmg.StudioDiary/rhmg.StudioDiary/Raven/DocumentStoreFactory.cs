using System;
using System.Reflection;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using Raven.Client.Indexes;
using rhmg.StudioDiary.Raven.Indexes;

namespace rhmg.StudioDiary.Raven
{
    public class DocumentStoreFactory
    {
        public static IDocumentStore GetOutOfProcessDocumentStore()
        {
            var store = new DocumentStore()
                            {
                                Url = "http://rhmg.raven:8080",
                                Conventions = DocumentConventionFactory.getConvention()
                            };
            store.Initialize();
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

            return store;
        }


        public static DocumentStore GetInMemoryPersistedDocumentStore()
        {
            var store = new EmbeddableDocumentStore
            {
                RunInMemory = false,
                DataDirectory = AppDomain.CurrentDomain.BaseDirectory + "App_Data\\Raven\\",
                Conventions = DocumentConventionFactory.getConvention(),
                Configuration = { Port = 3136 },
                UseEmbeddedHttpServer = true

            };
            store.Initialize();
            IndexCreation.CreateIndexes(Assembly.GetAssembly(typeof(CustomerArrears)), store);
            return store;
        }

        public static EmbeddableDocumentStore GetInMemoryDocumentStoreForUnitTest()
        {
            var store = new EmbeddableDocumentStore
                            {
                                RunInMemory = true,
                                Conventions = DocumentConventionFactory.getConvention(),
                                Configuration = { Port = 3137 },
                                UseEmbeddedHttpServer = false
                            };
            store.Initialize();
            IndexCreation.CreateIndexes(Assembly.GetAssembly(typeof(CustomerArrears)), store);

            return store;
        }
    }
    public interface IRavenSessionProvider
    {
        IDocumentSession GetSession();
    }

    public class RavenSessionProvider : IRavenSessionProvider
    {
        private static DocumentStore _documentStore;

        public static DocumentStore DocumentStore
        {
            get { return (_documentStore ?? (_documentStore = CreateDocumentStore())); }
        }

        private static DocumentStore CreateDocumentStore()
        {

            return DocumentStoreFactory.GetInMemoryPersistedDocumentStore();
        }

        public IDocumentSession GetSession()
        {
            var session = DocumentStore.OpenSession();
            return session;
        }


    }
}