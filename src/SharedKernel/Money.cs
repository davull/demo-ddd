namespace SharedKernel;

public struct Money
{
    public Currency Currency { get; }
    public double Amount { get; }

    public Money(Currency currency, double amount)
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