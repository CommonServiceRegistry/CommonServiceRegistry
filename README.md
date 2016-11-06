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
