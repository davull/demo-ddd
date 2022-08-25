using System.Net;
using System.Net.Http.Json;
using Xunit;
using Xunit.Abstractions;

namespace Web.Api.Tests.Integration.Controllers;

public class OrderControllerTests : IClassFixture<TestFixture>
{
    private readonly TestFixture _fixture;
    private readonly ITestOutputHelper _output;

    public OrderControllerTests(TestFixture fixture, ITestOutputHelper output)
    {
        _fixture = fixture;
        _output = output;
    }

    [Fact]
    public async Task CreateOrder_ReturnsCreated()
    {
        // Arrange
        var client = _fixture.GetClient(); 
        // Fill cart
        await PutItemIntoCart(client, ean: "2253325085315");
        await PutItemIntoCart(client, ean: "0320704074542", quantity: 2);

        // Act
        var response = await client.PostAsJsonAsync("/order", new { });
        _output.WriteLine(await response.Content.ReadAsStringAsync());

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    private static async Task PutItemIntoCart(HttpClient client, string ean, int quantity = 1)
    {
        await client.PutAsJsonAsync(
            requestUri: "/cart/products",
            value: new { ean, quantity });
    }
}