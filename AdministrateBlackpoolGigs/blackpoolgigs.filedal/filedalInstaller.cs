using blackpoolgigs.common.Interfaces;
using blackpoolgigs.filedal.Providers;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using OpenFileSystem.IO;
using OpenFileSystem.IO.FileSystems.Local.Win32;

namespace blackpoolgigs.filedal
{
    public class filedalInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                  Component.For<IAccessTheFileSystem>().ImplementedBy<FileSystemCache>()
                  .LifeStyle.Singleton
            );

            container.Register(Component.For<IFileSystem>().Instance(new Win32FileSystem()));
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
   
    }
}