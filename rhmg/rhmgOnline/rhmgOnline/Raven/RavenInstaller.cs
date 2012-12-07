using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Raven.Client;

namespace rhmgOnline.Raven
{
    public class RavenInstaller
    {
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
                container.Register(Component.For<IDocumentStore>().Instance(DocumentStoreFactory.GetDocumentStore()));

            //each request gets its own session
            container.Register(Component.For<IDocumentSession>().LifeStyle.Transient.UsingFactoryMethod(c => c.Resolve<IDocumentStore>().OpenSession()));


            var session = container.Resolve<IDocumentSession>();
            session.SaveChanges();
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            RegisterRaven(container);
        }
    }
}