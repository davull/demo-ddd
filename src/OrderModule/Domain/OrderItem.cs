using SharedKernel;
using Webshop.Domain;

namespace OrderModule.Domain;

using ProductIdentifier = Identifier<string>;

public class OrderItem
{
    public ProductIdentifier ProductId { get; }
    public string ProductName { get; }
    public string ProductDescription { get; }
    public Money ProductPrice { get; }
    public int Quantity { get; }

    public Money TotalPrice => new(
        currency: ProductPrice.Currency,
        amount: ProductPrice.Amount * Quantity);

    public OrderItem(
        ProductIdentifier productId,
        string productName,
        string productDescription,
        Money productPrice,
        int quantity)
    {
        ProductId = productId;
        ProductName = productName;
        ProductDescription = productDescription;
        ProductPrice = productPrice;
        Quantity = quantity;
    }

    public static OrderItem FromCartItem(ShoppingCartItem cartItem)
    {
        return new OrderItem(
            productId: cartItem.Product.Id,
            productName: cartItem.Product.Name,
            productDescription: cartItem.Product.Description,
            productPrice: cartItem.Product.Price,
            quantity: cartItem.Quantity);
    }

    public override string ToString() => $"{nameof(ProductName)}: {ProductName}, {nameof(Quantity)}: {Quantity}";
}

public class OrderItemCollection : List<OrderItem>
{
    public OrderItemCollection()
    {
    }

    public OrderItemCollection(IEnumerable<OrderItem> items)
        : base(items)
    {
    }
}