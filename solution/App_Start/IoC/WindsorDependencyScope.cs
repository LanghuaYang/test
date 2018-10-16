using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Castle.MicroKernel;
using Castle.MicroKernel.Lifestyle;

namespace solution.IoC
{
    public class WindsorDependencyScope : IDependencyScope
    {
        private readonly IKernel _container;
        private readonly IDisposable _scope;
        private static object lockObject = new Object();

        public WindsorDependencyScope(IKernel container)
        {
            _container = container;
            _scope = _container.BeginScope();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }

        public object GetService(Type serviceType)
        {
            lock (lockObject)
            {
                return _container.HasComponent(serviceType) ? _container.Resolve(serviceType) : null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.ResolveAll(serviceType).Cast<object>().ToArray();
        }
    }
}