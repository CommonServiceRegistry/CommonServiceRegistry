using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;

namespace CommonServiceRegistry.Unity
{
    /// <summary>
    /// Implements <see cref="ICommonServiceRegistry"/> and <see cref="ICommonServiceResolver"/>
    /// using the Unity IoC.
    /// </summary>
    /// <seealso cref="CommonServiceRegistry.ICommonServiceRegistry" />
    /// <seealso cref="CommonServiceRegistry.ICommonServiceResolver" />
    /// <seealso cref="UnityContainer"/>
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
            RegisterInstance<ICommonServiceResolver>(this, true);
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

        /// <summary>
        /// Registers the type <typeparamref name="TFrom"/> to be implemented
        /// by type <typeparamref name="TTo"/> with an optional <paramref name="factory"/>.
        /// Scoped: A new instance is created per scrope. See resolving. BeginScope().
        /// </summary>
        /// <typeparam name="TFrom">The type of from.</typeparam>
        /// <typeparam name="TTo">The type of to.</typeparam>
        /// <param name="factory">The factory.</param>
        public void RegisterScoped<TFrom, TTo>(Func<TFrom> factory = null) where TFrom : class where TTo : class, TFrom
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void RegisterInstance<TFrom>(TFrom instance, bool isExternalControlled = false) where TFrom : class
        {
            CheckContainer();

            var lifetimeManager = isExternalControlled
                ? (LifetimeManager)new ExternallyControlledLifetimeManager()
                : new ContainerControlledLifetimeManager();

            container.RegisterInstance(instance, lifetimeManager);
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

        /// <inheritdoc />
        public IDisposable BeginScope()
        {
            throw new NotImplementedException();
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
