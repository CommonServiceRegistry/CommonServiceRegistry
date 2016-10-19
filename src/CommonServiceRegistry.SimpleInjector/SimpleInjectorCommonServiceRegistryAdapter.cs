using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void RegisterInstance<TFrom, TTo>(TTo instance, bool isExternalControlled = false) where TTo : class, TFrom where TFrom : class
        {
            CheckContainer();

            container.RegisterSingleton<TFrom>(instance);
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

            if (string.IsNullOrEmpty(name))
            {
                container.Register<TFrom, TTo>();
            }
            else
            {
                container.RegisterCollection<TFrom>();
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

        private void CheckContainer()
        {
            if (container == null)
            {
                throw new InvalidOperationException("Container not yet set!");
            }
        }
    }
}
