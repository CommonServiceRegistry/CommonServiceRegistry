using System;
using System.Collections.Generic;

namespace CommonServiceRegistry
{
    /// <summary>
    /// Basic service resolver interface (aka Service Locator pattern).
    /// </summary>
    /// <remarks>
    /// Please note that we recommend using the Service Locator pattern (=anti pattern)
    /// for special situations only. We strongly recommend to use the Constructor parameter injection
    /// pattern as described in <see cref="ICommonServiceRegistry"/>. For more information about
    /// this topic read the following blog-post from Mark Seemann:
    /// http://blog.ploeh.dk/2010/02/03/ServiceLocatorisanAnti-Pattern/
    /// </remarks>
    public interface ICommonServiceResolver
    {
        /// <summary>
        /// Resolves an instance of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to resolve (eg. an Interface).</typeparam>
        /// <returns>An instance of <typeparamref name="T"/>.</returns>
        T Resolve<T>() where T: class;

        /// <summary>
        /// Resolves all registred intances for type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to resolve (eg. an Interface).</typeparam>
        /// <returns>All registred intances for type <typeparamref name="T"/>.</returns>
        IEnumerable<T> ResolveAll<T>() where T : class;

        /// <summary>
        /// Begins a new scope and return the scope's <see cref="IDisposable"/>.
        /// </summary>
        /// <remarks>
        /// To end the scope dispose the returned IDisposable.
        /// </remarks>
        /// <returns>The scope's <see cref="IDisposable"/>. If this gets disposed the scope ends.</returns>
        IDisposable BeginScope();
    }
}
