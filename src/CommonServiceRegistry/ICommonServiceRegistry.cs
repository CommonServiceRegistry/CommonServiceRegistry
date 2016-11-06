using System;

namespace CommonServiceRegistry
{
    /// <summary>
    /// Code your service registrations against this interface eg. by using extension
    /// methods for <see cref="ICommonServiceRegistry"/>. Then load your prefered IoC
    /// and the ICommonServiceRegistry implementation for it. Instantiate the container
    /// and the <see cref="ICommonServiceRegistry"/> based on the IoC (see implementation
    /// specific details). Then call you registry methods using the <see cref="ICommonServiceRegistry"/>
    /// you just created. Its recommended that you work with constructor- (or property-) injection.
    ///
    /// This way you can code your IoC registrations independent from a specific IoC product.
    /// For example if you code a library you can provide <c>UseMyLibrary()</c> style methods /
    /// helpers that will work with every IoC that has a <see cref="ICommonServiceRegistry"/>
    /// adapter.
    /// </summary>
    /// <example>
    ///
    /// Application:
    ///
    /// var container = new UnityContainer();
    /// var serviceRegistry = new UnityCommonServiceRegistry(container);
    /// serviceRegistry.UseMyLibrary();
    ///
    /// Library:
    ///
    /// public static class MyLibraryServiceRegistration
    /// {
    ///     public static void UseMyLibrary(this ICommonServiceRegistry registry)
    ///     {
    ///         registry.RegisterTransient&lt;IMyClass, MyClass&gt;();
    ///         registry.RegisterSinelton&lt;IMyClass2, MyClass2&gt;§();
    ///         ...
    ///     }
    /// }
    ///
    /// </example>
    public interface ICommonServiceRegistry
    {
        /// <summary>
        /// Registers the type <typeparamref name="TFrom"/> to be implemented
        /// by type <typeparamref name="TTo"/> with an optional <paramref name="factory"/>.
        /// TRANSIENT: Each time the type is resolved it will create a new instance.
        /// </summary>
        /// <typeparam name="TFrom">The type of from.</typeparam>
        /// <typeparam name="TTo">The type of to.</typeparam>
        /// <param name="factory">The optional factory expression.</param>
        void RegisterTransient<TFrom, TTo>(Func<TFrom> factory = null) where TTo : class, TFrom where TFrom : class;

        /// <summary>
        /// Registers the type <typeparamref name="TFrom"/> to be implemented
        /// by type <typeparamref name="TTo"/> with an optional <paramref name="factory"/>.
        /// Singleton: A new instance is created once and then used as a Singleton.
        /// </summary>
        /// <typeparam name="TFrom">The type of from.</typeparam>
        /// <typeparam name="TTo">The type of to.</typeparam>
        /// <param name="factory">The optional factory expression.</param>
        void RegisterSingleton<TFrom, TTo>(Func<TFrom> factory = null) where TTo : class, TFrom where TFrom : class;

        /// <summary>
        /// Registers the type <typeparamref name="TFrom"/> to be implemented
        /// by type <typeparamref name="TTo"/> with an optional <paramref name="factory"/>.
        /// Scoped: A new instance is created per scrope. See resolving. BeginScope().
        /// </summary>
        /// <typeparam name="TFrom">The type of from.</typeparam>
        /// <typeparam name="TTo">The type of to.</typeparam>
        /// <param name="factory">The factory.</param>
        void RegisterScoped<TFrom, TTo>(Func<TFrom> factory = null) where TTo : class, TFrom where TFrom : class;

        /// <summary>
        /// Registers the type <typeparamref name="TFrom" /> to be implemented
        /// with a pre-instantiated <paramref name="instance" />.
        /// Instance: The given <paramref name="instance" /> will be used. No new instance is created.
        /// </summary>
        /// <typeparam name="TFrom">The type of from.</typeparam>
        /// <param name="instance">The pre-created instance to use when an instance of <typeparamref name="TFrom" /> is requested.</param>
        /// <param name="isExternalControlled">if set to <c>true</c> the lifetime of the instance
        ///     controlled outside of the IoC externally. Otherwise (default) the instance is constructed outside
        ///     of the IoC but the lifetime (ownership) will be controlled from the IoC.</param>
        /// <remarks>
        /// Some containers refer this as "external".
        /// </remarks>
        void RegisterInstance<TFrom, TTo>(TTo instance, bool isExternalControlled = false) where TTo : class, TFrom where TFrom : class;
    }
}
