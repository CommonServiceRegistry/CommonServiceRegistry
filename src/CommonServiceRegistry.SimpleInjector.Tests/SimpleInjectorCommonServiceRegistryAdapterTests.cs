using SimpleInjector;
using CommonServiceRegistry.Tests;
using NUnit.Framework;
using SimpleInjector.Extensions.LifetimeScoping;

namespace CommonServiceRegistry.SimpleInjector.Tests
{
    /// <summary>
    /// Implements unit-tests for <see cref="SimpleInjectorCommonServiceRegistryAdapter"/> based
    /// on the tests from <see cref="AdapterTestsBase{TContainer}"/>.
    /// </summary>
    /// <seealso cref="CommonServiceRegistry.Tests.AdapterTestsBase{SimpleInjector.Container}" />
    /// <seealso cref="SimpleInjectorCommonServiceRegistryAdapter"/>
    [TestFixture]
    public class SimpleInjectorCommonServiceRegistryAdapterTests : AdapterTestsBase<Container>
    {
        /// <summary>
        /// Creates a new IoC and return the matching <see cref="ICommonServiceRegistry"/> for it.
        /// </summary>
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
