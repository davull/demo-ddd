using Webshop.Domain;

namespace Webshop.Persistence;

public class ShoppingCartRepository
{
    private ShoppingCart _shoppingCart = ShoppingCart.Empty;
    
    public void Save(ShoppingCart cart) => _shoppingCart = cart;

    public ShoppingCart Load() => _shoppingCart;
}