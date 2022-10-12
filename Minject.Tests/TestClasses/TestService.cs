using System;

namespace Minject.Tests.TestClasses;

public interface ITestService
{
    Guid GetGuid { get; }
}
public class TestService : ITestService
{
    public Guid GetGuid { get; } = Guid.NewGuid();
}