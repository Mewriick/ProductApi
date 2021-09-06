namespace Alza.Product.Domain
{
    public record ProductPrice
    {
        public decimal Value { get; }

        public ProductPrice(decimal value)
        {
            Value = value <= 0
                ? throw new System.ArgumentException($"'{nameof(value)}' must be greater than zero.", nameof(value))
                : value;
        }
    }
}
