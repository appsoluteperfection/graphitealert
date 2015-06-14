using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using GraphiteAlert.Configuration;
using GraphiteAlert.Configuration.DependencyResolution;
using StructureMap;
using DependencyResolver = GraphiteAlert.Configuration.DependencyResolution.DependencyResolver;

namespace GraphiteAlert
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            StructureMapSetup.SetUpIoC();
            
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

    }
}
