using System;
using System.Collections.Generic;
using LightCore;

public static class ModuleManager
{
    internal static ContainerBuilder _builder;
    internal static IContainer _container;

    static ModuleManager()
    {
        CleanUp();
    }

    public static LightCore.Fluent.IFluentRegistration Register<TContract, TImplementation>() where TImplementation : TContract
    {
        return _builder.Register<TContract, TImplementation>();
    }

    public static LightCore.Fluent.IFluentRegistration Register<TInstance>(TInstance instance)
    {
        return _builder.Register<TInstance>(instance);
    }

    public static LightCore.Fluent.IFluentRegistration Register(Type typeOfContract, Type typeOfImplementation)
    {
        return _builder.Register(typeOfContract, typeOfImplementation);
    }

    public static T Get<T>()
    {
        return _container.Resolve<T>();
    }


    public static TContract Resolve<TContract>()
    {
        return Get <TContract>();
    }

    public static TContract Resolve<TContract>(params object[] arguments)
    {
        return _container.Resolve<TContract>(arguments);
    }
    public static TContract Resolve<TContract>(IEnumerable<object> arguments)
    {
        return _container.Resolve<TContract>(arguments);
    }
    public static TContract Resolve<TContract>(IDictionary<string, object> namedArguments)
    {
        return _container.Resolve<TContract>(namedArguments);
    }

    public static TContract Resolve<TContract>(AnonymousArgument namedArguments)
    {
        return _container.Resolve<TContract>(namedArguments);
    }

    public static object Resolve(Type contractType)
    {
        return _container.Resolve(contractType);
    }

    public static object Resolve(Type contractType, params object[] arguments)
    {
        return _container.Resolve(contractType, arguments);
    }

    public static object Resolve(Type contractType, IEnumerable<object> arguments)
    {
        return _container.Resolve(contractType, arguments);
    }

    public static object Resolve(Type contractType, IDictionary<string, object> namedArguments)
    {
        return _container.Resolve(contractType, namedArguments);
    }

    public static IEnumerable<TContract> ResolveAll<TContract>()
    {
        return _container.ResolveAll<TContract>();
    }

    public static IEnumerable<object> ResolveAll(Type contractType)
    {
        return _container.ResolveAll(contractType);
    }
    public static IEnumerable<object> ResolveAll()
    {
        return _container.ResolveAll();
    }

    public static void InjectProperties(object instance)
    {
        _container.InjectProperties(instance);
    }

    public static bool HasRegistration(Type contractType)
    {
        return _container.HasRegistration(contractType);
    }

    public static bool HasRegistration<TContract>()
    {
        return _container.HasRegistration<TContract>();
    }

    public static void Build()
    {
        _container = _builder.Build();
    }

    public static void CleanUp()
    {
        _builder = null;
        _builder = new ContainerBuilder();
    }
}