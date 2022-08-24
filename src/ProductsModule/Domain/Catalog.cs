namespace ProductsModule.Domain;

public class Catalog
{
    public ILookup<string, Product> ProductsByGroup { get; }

    public Catalog(ILookup<string, Product> productsByGroup)
    {
        ProductsByGroup = productsByGroup;
    }
}