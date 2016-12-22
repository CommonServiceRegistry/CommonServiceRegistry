using System;
using System.Linq;
using CommonServiceRegistry.Tests.TestTypes;
using FluentAssertions;
using NUnit.Framework;

namespace CommonServiceRegistry.Tests
{
    /// <summary>
    /// Base class for testing IoC adapters. Adapter implementations just need
    /// to inherit from this base class, implement <see cref="InitializeContainerAndCommonServiceRegistry"/>
    /// and get all the tests.
    /// </summary>
    public abstract class AdapterTestsBase<TContainer>
        where TContainer : class
    {
        /// <summary>
        /// Sets up the tests by calling the inheritors <see cref="InitializeContainerAndCommonServiceRegistry"/>
        /// and then ensure the container and the registry are set up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            InitializeContainerAndCommonServiceRegistry();

            Container.Should().NotBeNull("CommonServiceRegistry 'Container' not initialized properly within InitializeContainerAndCommonServiceRegistry()!");
            Registry.Should().NotBeNull("CommonServiceRegistry 'Registry' not initialized properly within InitializeContainerAndCommonServiceRegistry()!");
            Resolver.Should().NotBeNull("CommonServiceRegistry 'Resolver' not initialized properly within InitializeContainerAndCommonServiceRegistry()!");
        }

        /// <summary>
        /// Tears down the tests.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            CleanUpTest();
        }

        [Test(Description = "Tests that transient registrations return a new instance every time solved.")]
        public virtual void Test_Transient()
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

        [Test(Description = "Tests that singelton registrations return a the same instance every time solved.")]
        public virtual void Test_Singelton()
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

        [Test(Description = "Tests that scoped registrations return a the same instance per scope.")]
        public virtual void Test_Scoped()
        {
            // Arrange
            Registry.RegisterScoped<IMyClass, MyClass>();

            IMyClass obj1, obj2, obj3, obj4;

            // Act (Getting two times to object)
            using (Resolver.BeginScope())
            {
                obj1 = Resolver.Resolve<IMyClass>();
                obj2 = Resolver.Resolve<IMyClass>();

            }

            using (Resolver.BeginScope())
            {
                obj3 = Resolver.Resolve<IMyClass>();
                obj4 = Resolver.Resolve<IMyClass>();
            }

            // Assert
            obj1.Should().NotBeNull();
            obj2.Should().NotBeNull();
            obj1.InstanceId.Should().NotBeEmpty();
            obj2.InstanceId.Should().NotBeEmpty();
            obj1.InstanceId.Should().Be(obj2.InstanceId);

            obj3.Should().NotBeNull();
            obj4.Should().NotBeNull();
            obj3.InstanceId.Should().NotBeEmpty();
            obj4.InstanceId.Should().NotBeEmpty();
            obj3.InstanceId.Should().Be(obj3.InstanceId);

            obj1.InstanceId.Should().NotBe(obj3.InstanceId);
        }

        /// <summary>
        /// Creates a new IoC and return the matching <see cref="ICommonServiceRegistry"/>
        /// for it.
        /// </summary>
        /// <returns></returns>
        protected abstract void InitializeContainerAndCommonServiceRegistry();

        /// <summary>
        /// Cleans up after a test.
        /// </summary>
        protected virtual void CleanUpTest()
        {
            Registry = null;
            (Container as IDisposable)?.Dispose();
        }

        protected TContainer Container;
        protected ICommonServiceRegistry Registry;
        protected ICommonServiceResolver Resolver;
    }
}
