namespace Alza.Product.Domain
{
    public record ProductName
    {
        public string Value { get; }

        public ProductName(string value)
        {
            Value = string.IsNullOrWhiteSpace(value)
                ? throw new System.ArgumentException($"'{nameof(value)}' cannot be null or whitespace.", nameof(value))
                : value;
        }
    }
}
