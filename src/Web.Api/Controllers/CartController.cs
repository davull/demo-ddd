using Microsoft.AspNetCore.Mvc;
using ProductsModule.Persistence;
using Webshop.Persistence;

namespace Web.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CartController : ControllerBase
{
    private readonly ShoppingCartRepository _shoppingCartRepository = new();
    private readonly ProductRepository _productRepository = new();

    // GET /cart
    [HttpGet("/cart")]
    public IActionResult Get() => Ok(_shoppingCartRepository.Load());


    public record PutProductParameters(string ean, int quantity);
    
    // PUT /cart/products { ean: "..", quantity: 1 }
    [HttpPut("/cart/products")]
    public IActionResult PutProduct(PutProductParameters parameters)
    {
        var product = _productRepository.GetProduct(ean: parameters.ean);
        if (product is null)
            return NotFound();

        var cart = _shoppingCartRepository.Load();
        cart.AddItem(product, parameters.quantity);

        return CreatedAtAction(actionName: nameof(Get), value: cart);
    }
}