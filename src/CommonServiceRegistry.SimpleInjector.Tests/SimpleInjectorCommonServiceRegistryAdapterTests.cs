using SimpleInjector;
using CommonServiceRegistry.Tests;
using NUnit.Framework;
using SimpleInjector.Extensions.LifetimeScoping;

namespace CommonServiceRegistry.SimpleInjector.Tests
{
    [TestFixture]
    public class SimpleInjectorCommonServiceRegistryAdapterTests : AdapterTestsBase<Container>
    {
        /// <summary>
        /// Creates a new IoC and return the matching <see cref="ICommonServiceRegistry"/>
        /// for it.
        /// </summary>
        /// <returns></returns>
        protected override void InitializeContainerAndCommonServiceRegistry()
        {
            Container = new Container();
            Container.Options.DefaultScopedLifestyle = new LifetimeScopeLifestyle();

            var simpleInjectorCommonServiceRegistryAdapter = new SimpleInjectorCommonServiceRegistryAdapter(Container);

            Registry = simpleInjectorCommonServiceRegistryAdapter;
            Resolver = simpleInjectorCommonServiceRegistryAdapter;
        }
    }
}
