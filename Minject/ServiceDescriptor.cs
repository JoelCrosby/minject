namespace Minject;

public record ServiceDescriptor
{
    public Type Type { get; }
    public Type? ImplType { get; }

    public ServiceLifetime Lifetime { get; }
    
    public object? Implementation { get; private set; }
    
    public ServiceDescriptor(Type type, ServiceLifetime lifetime)
    {
        Type = type;
        Lifetime = lifetime;
    }

    public ServiceDescriptor(Type type, ServiceLifetime lifetime, object implementation)
    {
        Type = type;
        Lifetime = lifetime;
        Implementation = implementation;
    }

    public ServiceDescriptor(Type type, Type implType, ServiceLifetime lifetime)
    {
        Type = type;
        ImplType = implType;
        Lifetime = lifetime;
    }

    public void SetImplementation(object implementation)
    {
        Implementation = implementation;
    }
}