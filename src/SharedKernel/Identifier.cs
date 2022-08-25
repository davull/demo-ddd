namespace SharedKernel;

public struct Identifier<T> where T : notnull
{
    public static readonly Identifier<T> Empty = new(IdentifiableFeature.None, default!);

    public IdentifiableFeature Feature { get; }

    public T Value { get; }

    public Identifier(IdentifiableFeature feature, T value)
    {
        Feature = feature;
        Value = value;
    }

    public override string ToString() => $"{Feature}:{Value}";
}

public enum IdentifiableFeature
{
    Unknown = 0,
    None = 1,
    Gtin = 2,
    Guid = 3,
}