using System.Web.Http;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Web.Mvc;

namespace solution.IoC
{
    public class DefaultInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            RegisterControllers(container);

            // singleton lifestyle shares instances between web requests.
            // all dependent projects with the "WorkSnap" prefix are loaded into Castle Windsor, so keep this in mind with project naming. 
            container.Register(Classes.FromAssemblyInThisApplication().Pick().WithServiceDefaultInterfaces().LifestyleSingleton());
        }

        public void RegisterControllers(IWindsorContainer container)
        {
            container.Register(Classes.FromThisAssembly()
                .BasedOn<Controller>()
                .LifestyleTransient());

            container.Register(Classes.FromThisAssembly()
                .BasedOn<ApiController>()
                .LifestylePerWebRequest());
        }
    }
}