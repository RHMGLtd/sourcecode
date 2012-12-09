using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;

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

            return store;
        }
    }
}