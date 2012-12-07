using System;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Client.Indexes;

namespace rhmgOnline.Raven
{
    public class DocumentStoreFactory
    {
        public static IDocumentStore GetDocumentStore()
        {
            var store = new EmbeddableDocumentStore
            {
                RunInMemory = false,
                DataDirectory = AppDomain.CurrentDomain.BaseDirectory + "App_Data\\Raven\\",
                Conventions = DocumentConventionFactory.getConvention(),

                Configuration =
                {
                    Port = 3140/*,
                    Catalog =
                    {
                        Catalogs =
                        {
                            new AssemblyCatalog(
                                Assembly.GetAssembly(typeof (VersioningPutTrigger)))
                        }
                    }*/

                },
                UseEmbeddedHttpServer = true
            };
            //var generator = new MultiTypeHiLoKeyGenerator(store, 1);
            //store.Conventions.DocumentKeyGenerator = entity => generator.GenerateDocumentKey(store.Conventions, entity);
            store.Initialize();
            IndexCreation.CreateIndexes(Assembly.GetAssembly(typeof(RouteRegistration)), store);
            return store;
        }
    }
}