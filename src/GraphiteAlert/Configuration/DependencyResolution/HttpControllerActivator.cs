using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using StructureMap;

namespace GraphiteAlert.Configuration.DependencyResolution
{
    public class HttpControllerActivator : IHttpControllerActivator
    {
        private readonly IContainer _container;

        public HttpControllerActivator(IContainer container)
        {
            _container = container;
        }

        public IHttpController Create(HttpRequestMessage request
            , HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controller = _container.GetInstance(controllerType) as IHttpController;
            return controller;
        }
    }
}