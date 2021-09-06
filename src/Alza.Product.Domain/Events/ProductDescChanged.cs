using JKang.EventSourcing.Events;
using System;

namespace Alza.Product.Domain.Events
{
    public sealed class ProductDescChanged : AggregateEvent<Guid>
    {
        public string OldDescription { get; }

        public string Description { get; }

        public ProductDescChanged(Guid aggregateId, int aggregateVersion, DateTime timestamp,
            string oldDescription, string description)
            : base(aggregateId, aggregateVersion, timestamp)
        {
            OldDescription = oldDescription;
            Description = description;
        }
    }
}
