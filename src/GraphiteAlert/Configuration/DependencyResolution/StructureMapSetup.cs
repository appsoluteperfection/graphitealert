using StructureMap;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace GraphiteAlert.Configuration.DependencyResolution
{
    public static class StructureMapSetup
    {
        public static void SetUpIoC(HttpConfiguration config = null)
        {
            var container = new Container();
            container.Configure(x =>
            {
                x.AddRegistry<DefaultRegistry>();
                x.AddRegistry<ControllerRegistry>();
            });

            // Register Service Locator
            Microsoft.Practices.ServiceLocation.ServiceLocator.SetLocatorProvider(() => new ServiceLocator(container));

            // Register Dependency Resolver
            var resolver = new DependencyResolver(container);
            System.Web.Mvc.DependencyResolver.SetResolver(resolver);

            
            if (null != config)
            {
                config.DependencyResolver = resolver;
                config.Services.Replace(typeof(IHttpControllerActivator), new HttpControllerActivator(container));
            }
        }
    }
}