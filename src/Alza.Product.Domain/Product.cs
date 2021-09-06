using Alza.Product.Domain.Events;
using JKang.EventSourcing.Domain;
using JKang.EventSourcing.Events;
using System;
using System.Collections.Generic;

namespace Alza.Product.Domain
{
    public class Product : Aggregate<Guid>
    {
        public ProductName Name { get; private set; }

        public ProductImage Image { get; private set; }

        public ProductPrice Price { get; private set; }

        public ProductDesc Description { get; private set; }

        public Product(string name, decimal price, string imageUri, string description)
            : base(new ProductCreated(Guid.NewGuid(), name, price, imageUri, description, DateTime.UtcNow))
        {
        }

        public Product(Guid id, IEnumerable<IAggregateEvent<Guid>> savedEvents)
            : base(id, savedEvents)
        {
        }

        public void ChangeDescription(string description)
            => ReceiveEvent(new ProductDescChanged(Id, GetNextVersion(), DateTime.UtcNow, Description.Value, description));

        protected override void ApplyEvent(IAggregateEvent<Guid> e)
        {
            switch (e)
            {
                case ProductCreated productCreated:
                    ApplyProductCreated(productCreated);
                    break;
                case ProductDescChanged productDescChanged:
                    ApplyDescriptionChanged(productDescChanged);
                    break;
                default:
                    throw new InvalidOperationException($"Unsupported event '{e.GetType()}'");
            }
        }

        private void ApplyProductCreated(ProductCreated productCreated)
        {
            Name = new ProductName(productCreated.Name);
            Image = new ProductImage(productCreated.ImageUri);
            Price = new ProductPrice(productCreated.Price);
            Description = new ProductDesc(productCreated.Description);
        }

        private void ApplyDescriptionChanged(ProductDescChanged productDescChanged)
        {
            Description = new ProductDesc(productDescChanged.Description);
        }
    }
}
