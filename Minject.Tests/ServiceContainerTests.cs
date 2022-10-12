
using FluentAssertions;

using Minject.Tests.TestClasses;

using Xunit;

namespace Minject.Tests;

public class ServiceContainerTests
{
    [Fact]
    public void BuildServiceProvider_ShouldReturnProvider()
    {
        var services = new ServiceContainer();

        services.AddSingleton(new TestService());

        var provider = services.BuildServiceProvider();
        provider.Should().NotBeNull();
    }
    
    [Fact]
    public void GetService_ShouldReturnService_WhenInstanceRegistered()
    {
        var services = new ServiceContainer();

        services.AddSingleton(new TestService());

        var provider = services.BuildServiceProvider();
        var service = provider.GetService<TestService>();

        service.Should().NotBeNull();
    }
    
    [Fact]
    public void GetService_ShouldReturnService_WhenTypeRegistered()
    {
        var services = new ServiceContainer();

        services.AddSingleton<TestService>();

        var provider = services.BuildServiceProvider();
        var service = provider.GetService<TestService>();

        service.Should().NotBeNull();
    }
    
    [Fact]
    public void GetService_ShouldReturnSingleton_WhenTypeRegistered_AsSingleton()
    {
        var services = new ServiceContainer();

        services.AddSingleton<TestService>();

        var provider = services.BuildServiceProvider();
        var serviceA = provider.GetService<TestService>();
        var serviceB = provider.GetService<TestService>();

        serviceA.GetGuid.Should().Be(serviceB.GetGuid);
    }
    
    [Fact]
    public void GetService_ShouldReturnSingleton_WhenInterfaceRegistered_AsTransient()
    {
        var services = new ServiceContainer();

        services.AddSingleton<ITestService, TestService>();

        var provider = services.BuildServiceProvider();
        var serviceA = provider.GetService<ITestService>();
        var serviceB = provider.GetService<ITestService>();

        serviceA.GetGuid.Should().Be(serviceB.GetGuid);
    }
    
    [Fact]
    public void GetService_ShouldReturnTransient_WhenTypeRegistered_AsTransient()
    {
        var services = new ServiceContainer();

        services.AddTransient<TestService>();

        var provider = services.BuildServiceProvider();
        var serviceA = provider.GetService<TestService>();
        var serviceB = provider.GetService<TestService>();

        serviceA.GetGuid.Should().NotBe(serviceB.GetGuid);
    }
    
    [Fact]
    public void GetService_ShouldReturnTransient_WhenInterfaceRegistered_AsTransient()
    {
        var services = new ServiceContainer();

        services.AddTransient<ITestService, TestService>();

        var provider = services.BuildServiceProvider();
        var serviceA = provider.GetService<ITestService>();
        var serviceB = provider.GetService<ITestService>();

        serviceA.GetGuid.Should().NotBe(serviceB.GetGuid);
    }
    
    [Fact]
    public void GetService_ShouldReturnTransient_WhenConstructorContainsDependencies()
    {
        var services = new ServiceContainer();

        services.AddTransient<ITestService, TestService>();
        services.AddTransient<ITestServiceWithDependencies, TestServiceWithDependencies>();

        var provider = services.BuildServiceProvider();
        var serviceA = provider.GetService<ITestServiceWithDependencies>();

        serviceA.Should().NotBeNull();
    }
}