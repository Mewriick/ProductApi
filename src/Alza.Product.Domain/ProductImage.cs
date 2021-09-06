namespace Alza.Product.Domain
{
    public record ProductImage
    {
        public string Uri { get; }

        public ProductImage(string uri)
        {
            Uri = string.IsNullOrWhiteSpace(uri)
                ? throw new System.ArgumentException($"'{nameof(uri)}' cannot be null or whitespace.", nameof(uri))
                : uri;
        }
    }
}
