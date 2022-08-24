using Microsoft.AspNetCore.Mvc;
using ProductsModule.Persistence;

namespace Web.Api.Controllers;

// This is not about architecture, CQRS, etc.
// Don't put your repositories in here, don't return models, etc.

[ApiController]
[Route("[controller]")]
public class CatalogController : ControllerBase
{
    private static readonly CatalogRepository _catalogRepository = new();
    private static readonly ProductRepository _productRepository = new();

    // GET /catalog/groups
    [HttpGet("groups")]
    public IActionResult GetGroups()
    {
        var groups = _catalogRepository.GetProductGroups();
        return Ok(groups);
    }

    // GET /catalog/groups/{group}/products
    [HttpGet("groups/{group}/products")]
    public IActionResult GetProductsByGroup(string group)
    {
        if (!_catalogRepository.GetProductGroups().Contains(group))
            return NotFound();

        var products = _catalogRepository.GetProductsInGroup(group);
        return Ok(products);
    }

    // GET /catalog/products/{ean}
    [HttpGet("products/{ean}")]
    public IActionResult GetProductByEan(string ean)
    {
        var product = _productRepository.GetProduct(ean: ean);

        return product is not null
            ? Ok(product)
            : NotFound();
    }
}