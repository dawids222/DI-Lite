# DI-Lite

DI-Lite is a small size, high performance tool for storing and retrieving objects for dependency injection. Library is written in C# and has no external dependencies.

# Table of contents
- [Features](#Features)
- [Examples](#Examples)
- [Todos](#Todos)

## Features

  - Registering singletons
  - Registering factories
  - Getting dependencies
  - Auto creating objects base on their most arguments constructor

## Examples

 ```CSharp
// creating a DI container
var Container = new Container();
```

 ```CSharp
// register singleton 
// object will be created once and then every Get call will return same object
Container.Single<DependencyType>(() => new DependencyImp());

// dependency can have a tag to distinguish dependencies of the same type
Container.Single<DependencyType>("tag", () => new DependencyImp());

// we can already use our container for creating new dependencies
Container.Single<DependencyType>("tag2", () => new DependencyImp(Container.Get<DependencyOfOurDependencyType>()));

// when no creation callback is provided the object will be created using it's most arguments constructor, but we have to provide its concrete class
Container.Single<DependencyType, DependencyImp>("tag3");

// if we for some reason don't want an abstraction then call can be simplified
Container.Single<DependencyImp>("tag4");
```

 ```CSharp
// register factory
// new object will be created with every Get call
Container.Factory<DependencyType>(() => new DependencyImp());
// every overload working with Single works with Factory
```

 ```CSharp
// register variants
// every register method has Try and Force variant
Container.Factory(() => "1");
Container.Factory(() => "2"); // this will throw an exception
/*******/
Container.Factory(() => "1");
Container.TryFactory(() => "2"); // this will not override the dependency
/*******/
Container.Factory(() => "1"));
Container.ForceFactory(() => "2"); // this will override the dependency
```

 ```CSharp
// getting dependency object from container
var dependency = Container.Get<DependencyType>();
// we can also get dependencies registered with tag
var dependency = Container.Get<DependencyType>("tag");
```

### Todos

 - Write MORE Tests