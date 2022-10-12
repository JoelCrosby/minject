namespace Minject;

public class ServiceProvider
{
    private readonly IReadOnlyDictionary<Type, ServiceDescriptor> _descriptors;

    public ServiceProvider(IReadOnlyDictionary<Type, ServiceDescriptor> descriptors)
    {
        _descriptors = descriptors;
    }

    public T GetService<T>()
    {
        var type = typeof(T);
        return (T) GetService(type);
    }
    
    private object GetService(Type type)
    {
        if (!_descriptors.TryGetValue(type, out var descriptor))
        {
            throw new InvalidOperationException($"service of type {type} was not registered");
        }
        
        return descriptor.Lifetime switch
        {
            ServiceLifetime.Singleton => GetSingleton(descriptor),
            ServiceLifetime.Transient => GetTransient(descriptor),
            _ => throw new InvalidOperationException($"could not resolve instance for service descriptor of type {type}"),
        };
    }

    private object GetTransient(ServiceDescriptor descriptor)
    {
        return GetInstance(descriptor);
    }

    private object GetSingleton(ServiceDescriptor descriptor)
    {
        if (descriptor.Implementation is not null)
        {
            return descriptor.Implementation;
        }

        var impl = GetInstance(descriptor);
        descriptor.SetImplementation(impl);
        return impl;
    }

    private object GetInstance(ServiceDescriptor descriptor)
    {
        var initType = descriptor.ImplType ?? descriptor.Type;
        var constructorInfo = initType.GetConstructors().FirstOrDefault(); 
        
        if (constructorInfo is null)
        {
            throw new InvalidOperationException($"A constructor for type '{initType}' was not found.");
        }

        var parameters = constructorInfo.GetParameters();
        var args = parameters.Select(p => GetService(p.ParameterType)).ToArray();
        
        return constructorInfo.Invoke(args);
    }
}