using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using blackpoolgigs.filedal;
using blackpoolgigs.ravendal;
using Castle.Facilities.Startable;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using coolbunny.common;
using coolbunny.common.Interfaces;
using coolbunny.ravendal;
using rhmg.AdministrateBlackpoolGigs.web.Services;
using Snooze;
using Snooze.Routing;
using Spark;
using Spark.Web.Mvc;

namespace rhmg.AdministrateBlackpoolGigs.web
{
    public class MvcApplication : HttpApplication, IWindsorApplication
    {
        static IWindsorContainer container;

        protected void Application_Start()
        {
            CreateContainer();
            var environment = EnvironmentConfigurationHelper.GetApplicationEnvironment();
            DoInstallation(environment);
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));
            RegisterViewEngines(ViewEngines.Engines);
            RegisterRoutes(RouteTable.Routes);
            if (environment == ApplicationEnvironment.Development)
            {
                //container.Resolve<ICreateTestData>().Run("from administrate program");
            }
            ConfigureSpark();
            container.Resolve<IConfigureAutomapper>().Do();
        }
        public void ConfigureSpark()
        {
            var settings = new SparkSettings();
            settings.SetAutomaticEncoding(true);

            SparkEngineStarter.RegisterViewEngine(settings);

            container.Register(AllTypes.FromThisAssembly()
                 .Pick()
                 .If(t => typeof(ResourceController).IsAssignableFrom(t))
                 .Configure(c => c.LifeStyle.HybridPerWebRequestTransient())
                 .WithService.Self());
        }
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.FromAssemblyWithType<RouteRegistration>();
        }
        public void RegisterViewEngines(ICollection<IViewEngine> engines)
        {
            if (engines == null) throw new ArgumentNullException("engines");

            SparkEngineStarter.RegisterViewEngine(engines);
        }

        public IWindsorContainer GetContainer()
        {
            return container;
        }

        public IWindsorContainer CreateContainer()
        {
            return container = new WindsorContainer();
        }

        public void DoInstallation(ApplicationEnvironment environment)
        {
            container.Register(Configuration(environment));
            container.AddFacility<StartableFacility>();
            container.Install(FromAssembly.This());
            container.Install(FromAssembly.Containing<ravendalInstaller>());
            container.Install(FromAssembly.Containing<filedalInstaller>());
            container.Register(Component.For<IConfigureAutomapper>().Instance(new AutoMapperConfiguration()).LifeStyle.Singleton);
        }
        private static BasedOnDescriptor Configuration(ApplicationEnvironment environment)
        {
            return AllTypes.FromThisAssembly().Pick().If(t => t.Namespace != null && t.Namespace.EndsWith(".Configuration") && t.Name == environment.ToString()).Configure(c => c.LifeStyle.Singleton).WithService.AllInterfaces();
        }
    }
}