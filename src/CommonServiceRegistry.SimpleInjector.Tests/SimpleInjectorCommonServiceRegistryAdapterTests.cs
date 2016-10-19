using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;
using CommonServiceRegistry.Tests;
using NUnit.Framework;

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

            var simpleInjectorCommonServiceRegistryAdapter = new SimpleInjectorCommonServiceRegistryAdapter(Container);

            Registry = simpleInjectorCommonServiceRegistryAdapter;
            Resolver = simpleInjectorCommonServiceRegistryAdapter;
        }
    }
}
