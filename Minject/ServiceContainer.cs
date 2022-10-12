namespace Minject;

public class ServiceContainer
{
    private readonly Dictionary<Type, ServiceDescriptor> _descriptors = new();

    public ServiceProvider BuildServiceProvider()
    {
        return new (_descriptors);
    }
    
    public ServiceContainer AddSingleton<T>(T implementation) where T : class
    {
        if (implementation is null)
        {
            throw new ArgumentNullException(nameof(implementation), "implementation cannot be null");
        }

        var type = implementation.GetType();
        var descriptor = new ServiceDescriptor(type, ServiceLifetime.Singleton, implementation);
        
        _descriptors.TryAdd(type, descriptor);

        return this;
    }
    
    public ServiceContainer AddSingleton<T>() where T : class
    {
        var type = typeof(T);
        var descriptor = new ServiceDescriptor(type, ServiceLifetime.Singleton);
        
        _descriptors.TryAdd(type, descriptor);

        return this;
    }

    public ServiceContainer AddTransient<T>() where T : class
    {
        var type = typeof(T);
        var descriptor = new ServiceDescriptor(type, ServiceLifetime.Transient);
        
        _descriptors.TryAdd(type, descriptor);

        return this;
    }
    
    public ServiceContainer AddTransient<TService, TImplementation>() where TImplementation : TService
    {
        var type = typeof(TService);
        var implType = typeof(TImplementation);
        var descriptor = new ServiceDescriptor(type, implType, ServiceLifetime.Transient);
        
        _descriptors.TryAdd(type, descriptor);

        return this;
    }
    
    public ServiceContainer AddSingleton<TService, TImplementation>() where TImplementation : TService
    {
        var type = typeof(TService);
        var implType = typeof(TImplementation);
        var descriptor = new ServiceDescriptor(type, implType, ServiceLifetime.Singleton);
        
        _descriptors.TryAdd(type, descriptor);

        return this;
    }
}   