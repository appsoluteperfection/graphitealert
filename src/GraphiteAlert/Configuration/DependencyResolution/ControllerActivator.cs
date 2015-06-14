using System;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;

namespace GraphiteAlert.Configuration.DependencyResolution
{
    public class ControllerActivator : IControllerActivator
    {
        private readonly IContainer _container;

        public ControllerActivator(IContainer container)
        {
            _container = container;
        }

        public IController Create(RequestContext requestContext, Type controllerType)
        {
            return _container.GetInstance(controllerType) as IController;
        }
    }
}