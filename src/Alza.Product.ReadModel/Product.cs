using System;

namespace Alza.Product.ReadModel
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageUri { get; set; }
    }
}
