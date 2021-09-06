using JKang.EventSourcing.Events;
using System;

namespace Alza.Product.Domain.Events
{
    public sealed class ProductCreated : AggregateCreatedEvent<Guid>
    {
        public string Name { get; }

        public decimal Price { get; }

        public string ImageUri { get; }

        public string Description { get; }

        public ProductCreated(Guid aggregateId, string name,
            decimal price, string imageUri, string description, DateTime timestamp)
            : base(aggregateId, timestamp)
        {
            Name = name;
            Price = price;
            ImageUri = imageUri;
            Description = description;
        }
    }
}
