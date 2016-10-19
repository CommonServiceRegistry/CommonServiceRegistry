using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;

namespace CommonServiceRegistry.Unity
{
    public class UnityCommonServiceRegistryAdapter : ICommonServiceRegistry, ICommonServiceResolver
    {
        private readonly IUnityContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityCommonServiceRegistryAdapter"/> class.
        /// </summary>
        /// <param name="container">The existing Unity container to use for registration calls..</param>
        /// <exception cref="ArgumentNullException"></exception>
        public UnityCommonServiceRegistryAdapter(IUnityContainer container)
        {
            if (container == null) { throw new ArgumentNullException(nameof(container)); }

            this.container = container;

            // Register ICommonServiceResolver to point to this instance. Label it as externally
            // controlled so it does not get killed by the container.
            RegisterInstance<ICommonServiceResolver, UnityCommonServiceRegistryAdapter>(this, true);
        }

        /// <inheritdoc />
        public void RegisterTransient<TFrom, TTo>(Func<TFrom> factory = null) where TTo : class, TFrom where TFrom : class
        {
            CheckContainer();

            if (factory == null)
            {
                container.RegisterType<TFrom, TTo>(new TransientLifetimeManager());
            }
            else
            {
                container.RegisterType<TFrom, TTo>(new TransientLifetimeManager(), new InjectionFactory(c => factory()));
            }
        }

        /// <inheritdoc />
        public void RegisterSingleton<TFrom, TTo>(Func<TFrom> factory = null) where TTo : class, TFrom where TFrom : class
        {
            CheckContainer();

            if (factory == null)
            {
                container.RegisterType<TFrom, TTo>(new ContainerControlledLifetimeManager());
            }
            else
            {
                container.RegisterType<TFrom, TTo>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionFactory(c => factory()));
            }
        }

        /// <inheritdoc />
        public void RegisterInstance<TFrom, TTo>(TTo instance, bool isExternalControlled = false) where TTo : class, TFrom where TFrom : class
        {
            CheckContainer();

            var lifetimeManager = isExternalControlled
                ? (LifetimeManager)new ExternallyControlledLifetimeManager()
                : new ContainerControlledLifetimeManager();

            container.RegisterInstance<TFrom>(instance, lifetimeManager);
        }

        public void RegisterCollection<TFrom>(IEnumerable<TFrom> containerControlledCollection)
        {
            throw new NotImplementedException();
        }

        public void RegisterCollection<TFrom>(params TFrom[] singletons)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public T Resolve<T>() where T : class
        {
            CheckContainer();

            return container.Resolve<T>();
        }

        /// <inheritdoc />
        public IEnumerable<T> ResolveAll<T>() where T : class
        {
            CheckContainer();

            return container.ResolveAll<T>();
        }

        private void CheckContainer()
        {
            if (container == null)
            {
                throw new InvalidOperationException("UnityCommonServiceRegistryAdapter: No container instance configured!");
            }
        }
    }
}
