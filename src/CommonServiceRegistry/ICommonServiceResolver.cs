using System;
using System.Collections.Generic;

namespace CommonServiceRegistry
{
    public interface ICommonServiceResolver
    {
        T Resolve<T>() where T: class;
        IEnumerable<T> ResolveAll<T>() where T : class;
        IDisposable BeginScope();
    }
}
