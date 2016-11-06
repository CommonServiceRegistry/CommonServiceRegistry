using System;

namespace CommonServiceRegistry.Tests.TestTypes
{
    public class MyOtherClass: IMyOtherClass
    {
        public Guid InstanceId { get; } = Guid.NewGuid();
    }
}
