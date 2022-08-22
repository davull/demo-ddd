using SharedKernel;

namespace Webshop.Domain;

internal static class ShoppingCartSummarizer
{
    public static Money SummaryPrice(ShoppingCart cart)
    {
        if (cart.IsEmpty)
            return Money.Zero;

        // Assume that the currency is all the same for all items in the cart
        return new Money(
            currency: cart.Items.First().TotalPrice.Currency,
            amount: cart.Items.Sum(i => i.TotalPrice.Amount));
    }
}