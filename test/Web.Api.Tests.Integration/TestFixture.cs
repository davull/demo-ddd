using Bogus;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Web.Api.Tests.Integration;

public class TestFixture
{
    public TestWebApplicationFactory Factory { get; } = new();

    public HttpClient GetClient() => Factory.CreateClient();

    public TestFixture()
    {
        // Seed Bogus to be deterministic
        Randomizer.Seed = new Random(42);
    }
}