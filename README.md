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
  - Removing registered dependencies
  - Getting dependencies
  - Scoping dependencies
  - Auto creating objects based on their constructor
  - Checking if all registered dependencies can be constructed
  - Invoking delegates via Reflection

## Examples

 ```CSharp
// creating a DI container
// container implements IDisposable interface in order to clean
// after any resolved dependency that also implements IDisposable
// that's why it is advised to instantiate it with 'using' keyword
// or rememeber to call .Dispose() when it is no longed needed
using var Container = new Container();
```

 ```CSharp
// register singleton 

// object will be created once and then every Get call will return same object
Container.Single<DependencyType>(() => new DependencyImp());
// dependency can have a tag to distinguish dependencies of the same type
Container.Single<DependencyType>("tag0", () => new DependencyImp());

// parameter of type 'IDependencyProvider' can be passed. This is instance on which we invoke 'Get' method.
Container.Single<DependencyType>(provider => new DependencyImp());
// we can specify tag
Container.Single<DependencyType>("tag1", provider => new DependencyImp());

// already instantiated objects can be used
Container.Single<DependencyType>(dependencyInstance);

// we can already use our container for creating new dependencies
Container.Single<DependencyType>("tag2", () => new DependencyImp(Container.Get<DependencyOfOurDependencyType>()));

// when no creation function is provided the object will be created using it's  constructor, but we have to provide its concrete class. Dependency class has to have exactly 1 constructor
Container.Single<DependencyType, DependencyImp>();
// we can specify tag
Container.Single<DependencyType, DependencyImp>("tag3");

// if for some reason we don't want an abstraction then call can be simplified
Container.Single<DependencyImp>();
// we can specify tag
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
// every depedency can be removed from a Container at any given time

// remove all dependencies registed for a type
Container.Remove<DependencyType>();
// remove all dependencies registed with a tag
Container.Remove("tag");
// remove dependency registed for a type with a tag (type and tag create KEY)
Container.Remove<DependencyType>("tag");
// remove all dependencies that match predicate
Container.Remove(dep =>
	 dep.Type != typeof(DependencyType) ||
	 dep.Tag != null);
// remove dependency with a given KEY
var key = Container.Dependencies.ElementAt(0).Key;
Container.Remove(key);
// remove all dependencies by collection of KEYS
var keys = new DependencyKey[] {
	new DependencyKey(typeof(DependencyType), null),
	new DependencyKey(typeof(AnotherDependencyType), null),
 };
Container.Remove(keys);
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
// scope implements IDisposable interface as container does
// that's why it is advised to instantiate it with 'using' keyword
// or rememeber to call .Dispose() when it is no longed needed
using var scope = Container.CreateScope();
// scope is able to Get all kinds of dependencies
// it is performed the same way as with Container
var dependency = scope.Get<DependencyType>();
// or
var dependency = scope.Get<DependencyType>("tag");
```

 ```CSharp
// to automatically inject dependency with tag specified we can add WithTag attribute to constructor's parameter
public class DependencyWithTag
{
    public DependencyWithTag([WithTag("MyTag")] IMockDependency dependency) { }
}
// now we just have to registed required dependency
Container.Single<IMockDependency, MockDependency>("MyTag");
Container.Single<DependencyWithTag>();
// now registered IMockDependency with tag "MyTag" will be used to construct instance of DependencyWithTag
Container.Get<DependencyWithTag>();
```

 ```CSharp
// we can check if all registered dependencies are constructable in multiple ways

// will simply check whether continer is constructable or not
var isConstructable = Container.IsConstructable;

// will return an object telling if the container is constructable and what is missing
var constructabilityReport = Container.GetConstructabilityReport();

// will throw if container is not constructable
Container.ThrowIfNotConstructable();
```

 ```CSharp
 var container = new Container();
container.Single<DependencyType, DependencyImp>();
// to automatically invoke a delegate we have to define providers 
// which will contain arguments for it
var providers = new IArgumentsProvider[]
{
	// we can use our container to provider complex objects
    new ContainerArgumentsProvider(container),
	// and a string dictionary to provide simple values
    new DictionaryArgumentsProvider(
        new Dictionary<string, string>() {
            { "name", "David" },
            { "age", "25" },
            { "birthday", "1996-12-21 8:00:00" }
        }),
};
// aggreagted provider can be used to provide values from multiple sources
var provider = new AggregatedArgumentsProvider(providers);
// here we define delegate we want to invoke
var del = (DependencyType dependency, string name, int age, DateTime birthday) => dependency.DoSomething(name, age, birthday);
// then we can instantiate our invoker and invoke a delegate
var invoker = new DelegateInvoker(del, provider);
// async
var result = await invoker.InvokeAsync();
// or sync
var result = invoker.Invoke();
```

```CSharp
// when using AggregatedArgumentsProvider we can use FromProvider attribute
// to specify from which provider argument should be taken
 var container1 = new Container();
container1.Single<IPet, Zerg>();
container1.Single<IPerson, Alien>();

var container2 = new Container();
container2.Single<IPet, Dog>("Dog");
container2.Single<IPet, Zerg>("Zerg");
container2.Single<IPerson, Human>("Dawid");
container2.Single<IPerson, NinjeTurtle>("Rafael");

 var provider = new AggregatedArgumentsProvider(new IArgumentsProvider[]
 {
 		// here we register providers with tags specified
		new ContainerArgumentsProvider(container1, "1"),
		new ContainerArgumentsProvider(container2, "2"),
});
// here we specifie that instance of IHuman has to be taken from provider with tag "2"
// WithTag attribute can be used same as in standard DI scenario
var func = ([FromProvider("2")][WithTag("Rafael")] IPerson human) => human.Greet();
var invoker = new DelegateInvoker(func, provider);

invoker.Invoke();
```

### Todos

 - Write MORE Tests
 - Add method descriptions
 - Add example usecase classes
 - Add interface for 'Container'