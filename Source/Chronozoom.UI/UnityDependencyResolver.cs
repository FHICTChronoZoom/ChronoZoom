using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace Chronozoom.UI
{
    /// <summary>
    /// A <see cref="System.Web.Http.Dependencies.IDependencyResolver"/> implementation for Microsoft's Unity dependency container.
    /// </summary>
    public class UnityDependencyResolver : IDependencyResolver
    {
        private IUnityContainer unityContainer;

        /// <summary>
        /// Create a new instance of <see cref="Chronozoom.UI.UnityDependencyResolver"/>.
        /// </summary>
        /// <param name="unityContainer">The <see cref="Microsoft.Practices.Unity.IUnityContainer"/> that will be used to resolve types on runtime.</param>
        public UnityDependencyResolver(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
        }

        /// <summary>
        /// Starts a resolution scope.
        /// </summary>
        /// <returns>The dependency scope.</returns>
        public IDependencyScope BeginScope()
        {
            return new UnityDependencyResolver(unityContainer.CreateChildContainer());
        }

        /// <summary>
        /// Retrieves a service from the scope.
        /// </summary>
        /// <param name="serviceType">The service to be retrieved.</param>
        /// <returns>The retrieved service.</returns>
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

        /// <summary>
        /// Retrieves a collection of services from the scope.
        /// </summary>
        /// <param name="serviceType">The collection of services to be retrieved.</param>
        /// <returns>The retrieved collection of services.</returns>
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

        /// <summary>
        /// Override this method to dispose of additional resources.
        /// </summary>
        /// <param name="managed">True if Dispose is called, false if called by deconstructor.</param>
        protected virtual void Dispose(bool managed)
        {
            if (!disposedValue)
            {
                disposedValue = true;
                if (managed)
                {
                    unityContainer.Dispose();
                }
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

    }
}
