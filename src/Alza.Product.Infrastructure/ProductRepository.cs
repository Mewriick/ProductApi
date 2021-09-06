using Alza.Product.Domain;
using JKang.EventSourcing.Persistence;
using JKang.EventSourcing.Snapshotting.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Alza.Product.Infrastructure
{
    public class ProductRepository : AggregateRepository<Domain.Product, Guid>, IProductRepository
    {
        public ProductRepository(
            IEventStore<Domain.Product, Guid> eventStore,
            ISnapshotStore<Domain.Product, Guid> snapshotStore)
            : base(eventStore, snapshotStore)
        {
        }

        public Task<Domain.Product> FindProduct(Guid id, CancellationToken cancellationToken = default)
            => FindAggregateAsync(id, cancellationToken: cancellationToken);


        public Task SaveProduct(Domain.Product product, CancellationToken cancellationToken = default)
            => SaveAggregateAsync(product, cancellationToken);
    }
}
