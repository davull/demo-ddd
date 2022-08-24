using ProductsModule.Domain;

namespace ProductsModule.Persistence;

public class CatalogRepository
{
    private readonly ProductRepository _productRepository = new();

    public Catalog GetCatalog()
    {
        var productGroupNames = _productRepository.GetProductGroupNames();
        var productsByGroupName = productGroupNames
            .Select(group => new
            {
                group,
                products = _productRepository.GetProductsByGroup(group)
            })
            .SelectMany(item => item.products.Select(product => new { item.group, product }))
            .ToLookup(item => item.group, item => item.product);

        return new Catalog(productsByGroupName);
    }

    public IEnumerable<string> GetProductGroups() => _productRepository.GetProductGroupNames();

    public IEnumerable<Product> GetProductsInGroup(string group) => _productRepository.GetProductsByGroup(group);
}