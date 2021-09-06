namespace Alza.Product.Domain
{
    public record ProductDesc
    {
        public string Value { get; }

        public ProductDesc(string value)
        {
            Value = value;
        }
    }
}
