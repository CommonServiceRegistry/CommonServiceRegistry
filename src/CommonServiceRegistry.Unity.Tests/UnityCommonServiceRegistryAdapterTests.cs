using CommonServiceRegistry.Tests;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace CommonServiceRegistry.Unity.Tests
{
    /// <summary>
    /// Tests for the <see cref="UnityCommonServiceRegistryAdapter"/>.
    /// </summary>
    /// <seealso cref="CommonServiceRegistry.Tests.AdapterTestsBase{Microsoft.Practices.Unity.IUnityContainer}" />
    [TestFixture]
    public class UnityCommonServiceRegistryAdapterTests : AdapterTestsBase<IUnityContainer>
    {
        /// <summary>
        /// Creates a new IoC and return the matching <see cref="ICommonServiceRegistry"/>
        /// for it.
        /// </summary>
        /// <returns></returns>
        protected override void InitializeContainerAndCommonServiceRegistry()
        {
            Container = new UnityContainer();

            var unityCommonServiceRegistryAdapter = new UnityCommonServiceRegistryAdapter(Container);

            Registry = unityCommonServiceRegistryAdapter;
            Resolver = unityCommonServiceRegistryAdapter;
        }
    }
}
