using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace blackpoolgigs.web
{
    public class installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //container.Register(Component.For<IProvideGigs>().ImplementedBy<GigProvider>()
            //         .LifeStyle.HybridPerWebRequestTransient());
            container.Register(ServicesInNamespaceEndingWith("Services"));
        }

        static BasedOnDescriptor ServicesInNamespaceEndingWith(string ns)
        {
            return AllTypes.FromThisAssembly()
                .Pick()
                .If(t => t.Namespace != null && t.Namespace.EndsWith(ns))
                .Configure(c => c.LifeStyle.HybridPerWebRequestTransient())
                .WithService.AllInterfaces();
        }
    }
}