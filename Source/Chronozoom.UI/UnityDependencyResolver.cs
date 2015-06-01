using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;

namespace Chronozoom.UI
{
    public class UnityDependencyResolver : IDependencyResolver
    {
        private IUnityContainer unityContainer;

        public UnityDependencyResolver(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
        }

        public IDependencyScope BeginScope()
        {
            return new UnityDependencyResolver(unityContainer.CreateChildContainer());
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return unityContainer.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return unityContainer.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new object[0];
            }
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;
                if (disposing)
                {
                    unityContainer.Dispose();
                }
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

    }
}
