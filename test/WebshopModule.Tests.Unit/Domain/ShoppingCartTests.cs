using Bogus;
using FluentAssertions;
using ProductsModule.Domain;
using SharedKernel;
using Webshop.Domain;
using Xunit;

namespace Webshop.Tests.Unit.Domain;

public class ShoppingCartTests
{
    [Fact]
    public void EmptyCartShouldHaveZeroTotal()
    {
        var cart = new ShoppingCart();

        cart.TotalPrice.Should().Be(Money.Zero);
        cart.IsEmpty.Should().BeTrue();
    }

    [Fact]
    public void FillingUpCartShouldRaiseTotal()
    {
        // Arrange
        var product1 = GetRandomProduct();
        var product2 = GetRandomProduct();

        // Act
        var cart = new ShoppingCart();
        cart.AddItem(product: product1, quantity: 1);
        cart.AddItem(product: product2, quantity: 1);

        // Assert
        cart.TotalPrice.Currency.Should().Be(Currency.EUR);
        cart.TotalPrice.Amount.Should().Be(product1.Price.Amount + product2.Price.Amount);
    }

    [Fact]
    public void AddingTheSameProductShouldIncreaseQuantity()
    {
        // Arrange
        var product = GetRandomProduct();

        // Act
        var cart = new ShoppingCart();
        cart.AddItem(product: product, quantity: 1);
        cart.AddItem(product: product, quantity: 2);

        // Assert
        cart.Items.Count().Should().Be(1);
        cart.Items.First().Quantity.Should().Be(3);
    }

    private static Product GetRandomProduct()
    {
        var faker = new Faker<Product>()
            .CustomInstantiator(f => new Product(
                Id: new Identifier<string>(IdentifiableFeature.Gtin, f.Commerce.Ean13()),
                Name: f.Commerce.ProductName(),
                Description: f.Commerce.ProductDescription(),
                Price: new Money(Currency.EUR, f.Finance.Amount())));

        return faker.Generate();
    }
}