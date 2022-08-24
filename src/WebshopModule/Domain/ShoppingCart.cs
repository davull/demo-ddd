using System.Collections.ObjectModel;
using ProductsModule.Domain;
using SharedKernel;

namespace Webshop.Domain;

public class ShoppingCart
{
    public static ShoppingCart Empty { get; } = new();
    
    private readonly IList<ShoppingCartItem> _items = new List<ShoppingCartItem>();

    public IEnumerable<ShoppingCartItem> Items => new ReadOnlyCollection<ShoppingCartItem>(_items);

    public bool IsEmpty => _items.Count == 0;

    public Money TotalPrice => ShoppingCartSummarizer.SummaryPrice(this);
    

    public void AddItem(Product product, int quantity)
    {
        var item = _items.SingleOrDefault(i => i.Product == product);

        if (item == null)
            _items.Add(new ShoppingCartItem(product, quantity));
        else
            item.AddQuantity(quantity);
    }

    public void RemoveItem(Product product, int quantity)
    {
        var item = _items.SingleOrDefault(i => i.Product == product);
        item?.RemoveQuantity(quantity);
    }

    public void RemoveItemCompletely(Product product)
    {
        var item = _items.SingleOrDefault(i => i.Product == product);

        if (item != null)
            _items.Remove(item);
    }
    
    public void Clear() => _items.Clear();

    public override string ToString() => $"Items: {_items.Count}, TotalPrice: {TotalPrice}";
}