using Microsoft.AspNetCore.Mvc.Testing;

namespace Web.Api.Tests.Integration;

public class TestFixture
{
    public TestWebApplicationFactory Factory { get; } = new();

    public HttpClient GetClient() => Factory.CreateClient();
}