using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace GraphiteAlert.Configuration.DependencyResolution
{
    public class ServiceLocator : ServiceLocatorImplBase
    {
        private readonly IContainer _container;

        public ServiceLocator(IContainer container)
        {
            _container = container;
        }

        public IContainer Container { get { return _container; } }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return string.IsNullOrEmpty(key)
                       ? _container.GetInstance(serviceType)
                       : _container.GetInstance(serviceType, key);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return _container.GetAllInstances(serviceType).Cast<object>().AsEnumerable();
        }

        public override TService GetInstance<TService>()
        {
            return _container.GetInstance<TService>();
        }

        public override TService GetInstance<TService>(string key)
        {
            return _container.GetInstance<TService>(key);
        }

        public override IEnumerable<TService> GetAllInstances<TService>()
        {
            return _container.GetAllInstances<TService>();
        }
    }
}