using SharedKernel;

namespace ProductsModule.Domain;

using ProductIdentifier = Identifier<string>;

public record Product(
    ProductIdentifier Id,
    string Name,
    string Description,
    Money Price);