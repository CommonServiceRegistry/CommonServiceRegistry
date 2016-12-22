# CommonServiceRegistry
Master: ![CI Build Status for master Branch](https://marcduerst.visualstudio.com/_apis/public/build/definitions/7c6411c0-584f-43dd-bb94-134baed219a2/4/badge)

## What is the Common Service Registry

The Common Service Registry is a set of Nuget packages which allows register services with a IoC/DI container indemendant of the
individual container. This means that one can specifiy registrations in a generic way and later change the IoC/DI container 
without chaning the registry code. This is especially usefull for library- or framework authors who don't know what IoC/DI
container gets used by the application using the library.

Further more there are a few out of the box implementations (adapters) for implementing the Common Service Registry.
Currently supported are:

* SimpleInjector
* Unity (partially, scoped Lifetime not yet supported)

..more to follow as needed.

There is also a small testing baselibrary which allows to test new adapters with very little code (a few lines are enought
to get the entire testsuite for a new adapter implementation.

## Open issues / Todo

- Finish package building und deployment
- Samples
- Support scoped lifetime for Unity
- Add support for collection registration / resolving
- Add more IoC/DI containers

See [here](https://github.com/mduu/CommonServiceRegistry/issues).

## Current Work in Progress

* The current registration interface can be found [here ...](https://github.com/mduu/CommonServiceRegistry/blob/master/src/CommonServiceRegistry/ICommonServiceRegistry.cs)
* The current resolving interface (more an added value) can be found [here ...](https://github.com/mduu/CommonServiceRegistry/blob/master/src/CommonServiceRegistry/ICommonServiceResolver.cs)

## How to install

Easiest is to get them from Nuget.org:

- Core: https://www.nuget.org/packages/CommonServiceRegistry.Core/
- SimpleInjector Adapter: https://www.nuget.org/packages/CommonServiceRegistry.SimpleInjector/
- Unity Adapter: https://www.nuget.org/packages/CommonServiceRegistry.Unity/

**Note:** The Nuget packages are "prerelease" state and therefore need to -pre switch (or checkbox set in case of the Nuget UI).

Another option is clone the repo und build it yourself.

## How to use

Code your service registrations against ``ICommonServiceRegistry`` eg. by using extension methods for 
``ICommonServiceRegistry``. Then load your prefered IoC and the ``ICommonServiceRegistry`` adapter for it.
Instantiate the container and the ``ICommonServiceRegistry`` based on the IoC (see implementation
specific details). Then call you registry methods using the ``ICommonServiceRegistry`` you just created. 
Its recommended that you work with constructor- (or property-) injection.

This way you can code your IoC registrations independent from a specific IoC product.
For example if you code a library you can provide ``UseMyLibrary()`` style methods /
helpers that will work with every IoC that has a ``ICommonServiceRegistry`` adapter.

**Application:**
```csharp
var container = new UnityContainer();
var serviceRegistry = new UnityCommonServiceRegistry(container);
serviceRegistry.UseMyLibrary();
```

**Library:**
```csharp
public static class MyLibraryServiceRegistration
{
    public static void UseMyLibrary(this ICommonServiceRegistry registry)
    {
        registry.RegisterTransient&lt;IMyClass, MyClass&gt;();
        registry.RegisterSinelton&lt;IMyClass2, MyClass2&gt;§();
        ...
    }
}
```
