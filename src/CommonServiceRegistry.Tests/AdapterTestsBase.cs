using System;
using System.Linq;
using CommonServiceRegistry.Tests.TestTypes;
using FluentAssertions;
using NUnit.Framework;

namespace CommonServiceRegistry.Tests
{
    /// <summary>
    /// Base class for testing IoC adapters. Adapter implementations just need
    /// to inherit from this base class, implement <see cref="CreateCommonServiceRegistry"/>
    /// and get all the tests.
    /// </summary>
    public abstract class AdapterTestsBase<TContainer>
        where TContainer : class
    {
        [SetUp]
        public void SetUp()
        {
            InitializeContainerAndCommonServiceRegistry();

            Container.Should().NotBeNull("Container not initialized properly within InitializeContainerAndCommonServiceRegistry()!");
            Registry.Should().NotBeNull("CommonServiceRegistry 'registry' not initialized properly within InitializeContainerAndCommonServiceRegistry()!");
        }

        [TearDown]
        public void TearDown()
        {
            CleanUpTest();
        }

        [Test(Description = "Tests that transient registrations return a new instance every time solved.")]
        public void Test_Transient_One()
        {
            // Arrange
            Registry.RegisterTransient<IMyClass, MyClass>();

            // Act
            var obj1 = Resolver.Resolve<IMyClass>();
            var obj2 = Resolver.Resolve<IMyClass>();

            // Assert
            obj1.Should().NotBeNull();
            obj2.Should().NotBeNull();
            obj1.InstanceId.Should().NotBeEmpty();
            obj2.InstanceId.Should().NotBeEmpty();
            obj1.InstanceId.Should().NotBe(obj2.InstanceId);
        }

        [Test(Description = "Tests that transient registrations return a new instance every time solved.")]
        public void Test_Transient_Many()
        {
            // Arrange
            Registry.RegisterTransient<IBaseClass, MyClass>(null);
            Registry.RegisterTransient<IBaseClass, MyOtherClass>(null);

            // Act
            var objs1 = Resolver.ResolveAll<IBaseClass>();
            var objs2 = Resolver.ResolveAll<IBaseClass>();

            // Assert
            objs1.Should().HaveCount(2);
            objs2.Should().HaveCount(2);

            var ids = objs1.Select(o => o.InstanceId).Union(objs2.Select(o => o.InstanceId)).Distinct();
            ids.Should().HaveCount(4);
        }

        [Test(Description = "Tests that singelton registrations return a the same instance every time solved.")]
        public void Test_Singelton_One()
        {
            // Arrange
            Registry.RegisterSingleton<IMyClass, MyClass>();

            // Act
            var obj1 = Resolver.Resolve<IMyClass>();
            var obj2 = Resolver.Resolve<IMyClass>();

            // Assert
            obj1.Should().NotBeNull();
            obj2.Should().NotBeNull();
            obj1.InstanceId.Should().NotBeEmpty();
            obj2.InstanceId.Should().NotBeEmpty();
            obj1.InstanceId.Should().Be(obj2.InstanceId);
        }

        [Test(Description = "Tests that singelton registrations return a the same instance every time solved.")]
        public void Test_Singelton_Many()
        {
            // Arrange
            Registry.RegisterSingleton<IBaseClass, MyClass>(null);
            Registry.RegisterSingleton<IBaseClass, MyOtherClass>(null);

            // Act
            var objs1 = Resolver.ResolveAll<IBaseClass>();
            var objs2 = Resolver.ResolveAll<IBaseClass>();

            // Assert
            objs1.Should().HaveCount(2);
            objs2.Should().HaveCount(2);

            var ids = objs1.Select(o => o.InstanceId).Union(objs2.Select(o => o.InstanceId)).Distinct();
            ids.Should().HaveCount(2);
        }

        /// <summary>
        /// Creates a new IoC and return the matching <see cref="ICommonServiceRegistry"/>
        /// for it.
        /// </summary>
        /// <returns></returns>
        protected abstract void InitializeContainerAndCommonServiceRegistry();

        protected virtual void CleanUpTest()
        {
            Registry = null;
            var disposableContainer = Container as IDisposable;
            disposableContainer?.Dispose();
        }

        protected TContainer Container;
        protected ICommonServiceRegistry Registry;
        protected ICommonServiceResolver Resolver;
    }
}
