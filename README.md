# DI-Lite

DI-Lite is a small size, high performance tool for storing and retrieving objects for dependency injection. Library is written in C# and has no external dependencies.

# Table of contents
- [Features](#Features)
- [Examples](#Examples)
- [Todos](#Todos)

## Features

  - Registering singletons
  - Registering factories
  - Registering scoped dependencies
  - Getting dependencies
  - Scoping dependencies
  - Auto creating objects base on their constructor

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

// already instantiated objects can be used
Container.Single<DependencyType>(dependencyInstance);

// we can already use our container for creating new dependencies
Container.Single<DependencyType>("tag2", () => new DependencyImp(Container.Get<DependencyOfOurDependencyType>()));

// when no creation function is provided the object will be created using it's  constructor, but we have to provide its concrete class. Dependency class has to have exactly 1 constructor
Container.Single<DependencyType, DependencyImp>("tag3");

// if for some reason we don't want an abstraction then call can be simplified
Container.Single<DependencyImp>("tag4");
```

 ```CSharp
// register factory
// new object will be created with every Get call
Container.Factory<DependencyType>();
// most overloads working with Single works with Factory with minor exceptions
```

 ```CSharp
// register scoped dependency
// same object will be returned for a given scope
// different scopes will return different objects
Container.Scoped<DependencyType>();
// most overloads working with Single works with Scoped with minor exceptions
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
// regular containers are unable to Get scoped dependencies
var dependency = Container.Get<DependencyType>();
// we can also get dependencies registered with tag
var dependency = Container.Get<DependencyType>("tag");
```

 ```CSharp
// to Get scoped dependencies we first have to create a scope
var scope = Container.CreateScope();
// scope is able to Get all kinds of dependencies
// it is performed the same way as with Container
var dependency = scope.Get<DependencyType>();
// or
var dependency = scope.Get<DependencyType>("tag");
```

### Todos

 - Write MORE Tests