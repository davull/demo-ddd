using System.Net;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Web.Api.Tests.Integration.Controllers;

public class CatalogControllerTests : IClassFixture<TestFixture>
{
    private readonly TestFixture _fixture;
    private readonly ITestOutputHelper _output;

    public CatalogControllerTests(TestFixture fixture, ITestOutputHelper output)
    {
        _fixture = fixture;
        _output = output;
    }

    [Fact]
    public async Task GetGroups_ReturnsSuccess()
    {
        // Arrange
        var client = _fixture.GetClient();

        // Act
        var response = await client.GetAsync("/catalog/groups");
        _output.WriteLine(await response.Content.ReadAsStringAsync());

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
    }

    [Fact]
    public async Task GetProductByUnknownGroup_ReturnsNotFound()
    {
        // Arrange
        var client = _fixture.GetClient();

        // Act
        var response = await client.GetAsync("/catalog/groups/unknown/products");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetProductsByGroup_ReturnsSuccess()
    {
        // Arrange
        var client = _fixture.GetClient();

        // Act
        var response = await client.GetAsync("/catalog/groups/Metal/products");
        _output.WriteLine(await response.Content.ReadAsStringAsync());

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
    }

    [Theory]
    [InlineData("2253325085315", true)]
    [InlineData("0320704074542", true)]
    [InlineData("6099436031478", true)]
    [InlineData("0000000000000", false)]
    [InlineData("abc", false)]
    public async Task GetProduct_ReturnsSuccessOrNotFound(string ean, bool expectedSuccess)
    {
        // Arrange
        var client = _fixture.GetClient();

        // Act
        var response = await client.GetAsync($"/catalog/products/{ean}");
        _output.WriteLine(await response.Content.ReadAsStringAsync());

        // Assert
        if (expectedSuccess)
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        else
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}