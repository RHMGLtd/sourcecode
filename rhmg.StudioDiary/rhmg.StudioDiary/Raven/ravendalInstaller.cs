using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Raven.Client;

namespace rhmg.StudioDiary.Raven
{
    public class ravendalInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            RegisterRaven(container);
            container.Register(ServicesInNamespaceEndingWith("Providers"));
        }

        static BasedOnDescriptor ServicesInNamespaceEndingWith(string ns)
        {
            return AllTypes.FromThisAssembly()
                .Pick()
                .If(t => t.Namespace != null && t.Namespace.EndsWith(ns))
                .Configure(c => c.LifeStyle.HybridPerWebRequestTransient())
                .WithService.AllInterfaces();
        }

        private static void RegisterRaven(IWindsorContainer container)
        {
            //frig for injecting a store from tests
            var havestore = false;
            try
            {
                var store = container.Resolve<IDocumentStore>();
                if (store != null)
                    havestore = true;
            }
            catch
            {

            }
            //only need one raven store per app domain
            if (!havestore)
                container.Register(Component.For<IDocumentStore>().Instance(DocumentStoreFactory.GetInMemoryDocumentStoreForUnitTest()));

            //each request gets its own session
            container.Register(Component.For<IDocumentSession>().LifeStyle.HybridPerWebRequestTransient().UsingFactoryMethod(c => c.Resolve<IDocumentStore>().OpenSession()));
        }
    }
}