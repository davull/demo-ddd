using ProductsModule.Domain;
using SharedKernel;

namespace Webshop.Domain;

public class ShoppingCartItem
{
    public Product Product { get; }
    public int Quantity { get; private set; }
    
    public ShoppingCartItem(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }

    public Money TotalPrice => new(
        currency: Product.Price.Currency,
        amount: Product.Price.Amount * Quantity);
    
    public void AddQuantity(int quantityToAdd) => Quantity += quantityToAdd;

    public void RemoveQuantity(int quantityToRemove) => AddQuantity(quantityToRemove * -1);

    public override string ToString() => $"{Product.Name} x {Quantity}";
}