using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace GraphiteAlert.Configuration.DependencyResolution
{
    public class DependencyScope : IDependencyScope
    {
        private readonly IContainer _container;
        public DependencyScope(IContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            return serviceType.IsAbstract || serviceType.IsInterface ? _container.TryGetInstance(serviceType)
                : _container.GetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances<object>()
               .Where(s => s.GetType() == serviceType);
        }
    

        public void Dispose()
        {
            if (_container != null)
                _container.Dispose();
        }
    }

    public class DependencyResolver : DependencyScope, IDependencyResolver, IServiceLocator
    {
        private readonly IContainer _container;
        public DependencyResolver(IContainer container) :
            base(container.GetNestedContainer())
        {
            _container = container;
        }

        public IDependencyScope BeginScope()
        {
            // Create a Nested Container.So that we can dispose the container once the request is completed.
            var nestedContainer = _container.GetNestedContainer();
            return new DependencyScope(nestedContainer);
        }

        public object GetInstance(Type serviceType)
        {
            try
            {
                return _container.GetInstance(serviceType);
            }
            catch(StructureMapConfigurationException)
            {
                if (serviceType.FullName.Contains("System."))
                    return null;
                throw;
            }
        }

        public object GetInstance(Type serviceType, string key)
        {
            return _container.GetInstance(serviceType, key);
        }

        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.GetAllInstances(serviceType).Cast<object>();
        }

        public TService GetInstance<TService>()
        {
            return _container.GetInstance<TService>();
        }

        public TService GetInstance<TService>(string key)
        {
            return _container.GetInstance<TService>(key);
        }

        public IEnumerable<TService> GetAllInstances<TService>()
        {
            return _container.GetAllInstances<TService>();
        }
    }
}