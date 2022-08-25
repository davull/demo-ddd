using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Web.Api.Tests.Integration.Controllers;

public class CartControllerTests : IClassFixture<TestFixture>
{
    private readonly TestFixture _fixture;
    private readonly ITestOutputHelper _output;

    public CartControllerTests(TestFixture fixture, ITestOutputHelper output)
    {
        _fixture = fixture;
        _output = output;
    }

    [Fact]
    public async Task GetEmptyCart_ReturnsSuccess()
    {
        // Arrange
        var client = _fixture.GetClient();

        // Act
        var response = await client.GetAsync("/cart");
        _output.WriteLine(await response.Content.ReadAsStringAsync());

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
    }

    [Fact]
    public async Task GetNonEmptyCart_ReturnsSuccess()
    {
        // Arrange
        var client = _fixture.GetClient();
        // Fill cart
        await PutItemIntoCart(client, ean: "2253325085315");
        await PutItemIntoCart(client, ean: "0320704074542", quantity: 2);

        // Act
        var response = await client.GetAsync("/cart");
        _output.WriteLine(await response.Content.ReadAsStringAsync());

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
    }

    [Fact]
    public async Task AddItemToCart_ReturnsSuccess()
    {
        // Arrange
        var client = _fixture.GetClient();

        // Act
        var response = await PutItemIntoCart(client, ean: "7089371143717");
        _output.WriteLine(await response.Content.ReadAsStringAsync());

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created); // 201
    }

    private static async Task<HttpResponseMessage> PutItemIntoCart(HttpClient client, string ean, int quantity = 1)
    {
        return await client.PutAsJsonAsync(
            requestUri: "/cart/products",
            value: new { ean, quantity });
    }
}