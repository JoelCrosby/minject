using System;

namespace Minject.Tests.TestClasses;

public interface ITestServiceWithDependencies
{
    void PrintId();
}

public class TestServiceWithDependencies : ITestServiceWithDependencies
{
    private readonly ITestService _testService;

    public TestServiceWithDependencies(ITestService testService)
    {
        _testService = testService;
    }

    public void PrintId()
    {
        Console.WriteLine(_testService.GetGuid);
    }
}