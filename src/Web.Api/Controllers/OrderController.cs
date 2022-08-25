using Microsoft.AspNetCore.Mvc;
using OrderModule.Domain;
using SharedKernel;
using Webshop.Persistence;

namespace Web.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    // POST /order
    [HttpPost]
    public IActionResult Post()
    {
        // Get the shopping cart somehow...

        var cart = new ShoppingCartRepository().Load();
        
        var customer = new Customer(
            Id: new Identifier<Guid>(feature: IdentifiableFeature.Guid, Guid.NewGuid()),
            FirstName: "Peter",
            LastName: "Pan");
        var address = new Address(
            Street: "Mainstreet 123",
            City: "Berlin",
            Zip: "54545");

        var order = new OrderBuilder()
            .ForCustomer(customer)
            .WithBillingAddress(address)
            .WithItems(cart.Items.Select(OrderItem.FromCartItem))
            .Build();
        // Do something useful with the order

        return Created(
            uri: $"orders/{Guid.NewGuid()}",
            value: order);
    }
}