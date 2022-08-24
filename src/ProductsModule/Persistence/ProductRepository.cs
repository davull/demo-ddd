using Bogus;
using ProductsModule.Domain;
using SharedKernel;

namespace ProductsModule.Persistence;

public class ProductRepository
{
    private static readonly ICollection<string> _productGroupNames;
    private static readonly ILookup<string, Product> _productsByGroup;

    static ProductRepository()
    {
        var faker = new Faker();

        _productGroupNames = Enumerable
            .Range(start: 0, count: 10)
            .Select(_ => faker.Commerce.ProductMaterial())
            .Distinct()
            .ToList();

        var list = new List<(string, Product)>();

        foreach (var groupName in _productGroupNames)
            Enumerable
                .Range(start: 0, count: 15)
                .Select(_ => new Product(
                    Id: new Identifier<string>(IdentifiableFeature.Gtin, faker.Commerce.Ean13()),
                    Name: faker.Commerce.ProductName(),
                    Description: faker.Commerce.ProductDescription(),
                    Price: new Money(Currency.EUR, faker.Finance.Amount())))
                .Do(item => list.Add((groupName, item)));

        _productsByGroup = list
            .ToLookup(
                keySelector: tuple => tuple.Item1,
                elementSelector: tuple => tuple.Item2);
    }

    public IEnumerable<string> GetProductGroupNames() => _productGroupNames;

    public IEnumerable<Product> GetProductsByGroup(string group) => _productsByGroup[group];

    public Product? GetProduct(string ean)
    {
        var id = new Identifier<string>(IdentifiableFeature.Gtin, ean);
        return _productsByGroup
            .SelectMany(x => x)
            .FirstOrDefault(x => Equals(x.Id, id));
    }
}