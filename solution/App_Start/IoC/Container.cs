using System.Web.Http;
using System.Web.Mvc;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace solution.IoC
{
    public static class Container
    {
        public static IWindsorContainer Object { get; private set; }

        public static IWindsorContainer Install()
        {
            Object = new WindsorContainer().Install(FromAssembly.InThisApplication());

            var controllerFactory = new WindsorControllerFactory(Object.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
            GlobalConfiguration.Configuration.DependencyResolver = new WindsorDependencyResolver(Object.Kernel);
            return Object;
        }
    }
}