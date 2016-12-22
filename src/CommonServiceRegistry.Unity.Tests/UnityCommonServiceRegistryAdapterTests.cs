using CommonServiceRegistry.Tests;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace CommonServiceRegistry.Unity.Tests
{
    /// <summary>
    /// Tests for the <see cref="UnityCommonServiceRegistryAdapter"/> based on <see cref="AdapterTestsBase{TContainer}"/>.
    /// </summary>
    /// <seealso cref="CommonServiceRegistry.Tests.AdapterTestsBase{IUnityContainer}" />
    /// <seealso cref="UnityCommonServiceRegistryAdapter"/>
    [TestFixture]
    public class UnityCommonServiceRegistryAdapterTests : AdapterTestsBase<IUnityContainer>
    {
        [Test]
        [Ignore("Not yet supported by the Unity adapter")]
        public override void Test_Scoped()
        {
        }

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
