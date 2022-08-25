using SharedKernel;

namespace OrderModule.Domain;

using CustomerIdentifier = Identifier<Guid>;

public record Customer(
    CustomerIdentifier Id,
    string FirstName,
    string LastName);