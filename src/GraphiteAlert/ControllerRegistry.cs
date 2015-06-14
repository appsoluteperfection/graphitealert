using System.Web.Http;
using System.Web.Http.Controllers;
using GraphiteAlert.Configuration.DependencyResolution;
using StructureMap.Configuration.DSL;
using System.Web.Mvc;
using StructureMap.Graph;

namespace GraphiteAlert
{
    public class ControllerRegistry : Registry
    {
        public ControllerRegistry()
        {
            For<IControllerActivator>().Use<ControllerActivator>();
            
            Scan(p =>
            {
                p.TheCallingAssembly();
                p.AddAllTypesOf<IHttpController>();
                p.AddAllTypesOf<ApiController>();
            });
        }
    }
}