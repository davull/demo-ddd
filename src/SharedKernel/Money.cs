namespace SharedKernel;

public struct Money
{
    public static Money Zero => new Money(Currency.Unknown, 0m);
    
    public Currency Currency { get; }
    public decimal Amount { get; }

    public Money(Currency currency, decimal amount)
    {
        Currency = currency;
        Amount = amount;
    }

    public override string ToString() => $"{Amount} {Currency}";
}

public enum Currency
{
    Unknown = 0,
    USD = 1,
    EUR = 2,
    GBP = 3,
    JPY = 4,
}