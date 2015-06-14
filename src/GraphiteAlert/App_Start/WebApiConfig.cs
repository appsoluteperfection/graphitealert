using System.Net.Http.Headers;
using GraphiteAlert.Configuration.DependencyResolution;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http;

namespace GraphiteAlert
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            StructureMapSetup.SetUpIoC(config);

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Default to json
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }

        
        
    }
}
