using System;

namespace CommonServiceRegistry.Tests.TestTypes
{
    public class MyClass : IMyClass
    {
        public Guid InstanceId { get; } = Guid.NewGuid();
    }
}
