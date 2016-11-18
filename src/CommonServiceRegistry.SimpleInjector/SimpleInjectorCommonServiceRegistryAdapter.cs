using System;
using System.Collections.Generic;
using SimpleInjector;

namespace CommonServiceRegistry.SimpleInjector
{
    public class SimpleInjectorCommonServiceRegistryAdapter : ICommonServiceRegistry, ICommonServiceResolver
    {
        private readonly Container container;

        public SimpleInjectorCommonServiceRegistryAdapter(Container container)
        {
            if (container == null) { throw new ArgumentNullException(nameof(container)); }

            this.container = container;
        }

        /// <inheritdoc />
        public void RegisterInstance<TFrom>(TFrom instance, bool isExternalControlled = false) where TFrom : class
        {
            CheckContainer();

            container.RegisterSingleton(instance);
        }

        /// <inheritdoc />
        public void RegisterScoped<TFrom, TTo>(Func<TFrom> factory = null) where TFrom : class where TTo : class, TFrom
        {
            CheckContainer();

            if (factory == null)
            {
                container.Register<TFrom, TTo>(Lifestyle.Scoped);
            }
            else
            {
                container.Register(factory, Lifestyle.Scoped);
            }
        }

        /// <inheritdoc />
        public void RegisterSingleton<TFrom, TTo>(Func<TFrom> factory = null) where TTo : class, TFrom where TFrom : class
        {
            CheckContainer();

            if (factory == null)
            {
                container.RegisterSingleton<TFrom, TTo>();
            }
            else
            {
                container.RegisterSingleton(factory);
            }
        }

        /// <inheritdoc />
        public void RegisterTransient<TFrom, TTo>(Func<TFrom> factory = null) where TTo : class, TFrom where TFrom : class
        {
            CheckContainer();

            if (factory == null)
            {
                container.Register<TFrom, TTo>();
            }
            else
            {
                container.Register(factory);
            }
        }

        /// <inheritdoc />
        public T Resolve<T>() where T: class
        {
            CheckContainer();

            return container.GetInstance<T>();
        }

        /// <inheritdoc />
        public IEnumerable<T> ResolveAll<T>() where T: class
        {
            CheckContainer();

            return container.GetAllInstances<T>();
        }

        public IDisposable BeginScope()
        {
            CheckContainer();

            return container.BeginLifetimeScope();
        }

        private void CheckContainer()
        {
            if (container == null)
            {
                throw new InvalidOperationException("Container not yet set!");
            }
        }
    }
}
