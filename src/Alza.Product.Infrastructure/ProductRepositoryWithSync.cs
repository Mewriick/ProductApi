using Alza.Product.Domain;
using Alza.Product.ReadModel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Alza.Product.Infrastructure
{
    public class ProductRepositoryWithSync : IProductRepository
    {
        private readonly IProductRepository productRepository;
        private readonly IProductSynchronizer productSynchronizer;

        public ProductRepositoryWithSync(IProductRepository productRepository, IProductSynchronizer productSynchronizer)
        {
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            this.productSynchronizer = productSynchronizer ?? throw new ArgumentNullException(nameof(productSynchronizer));
        }

        public Task<Domain.Product> FindProduct(Guid id, CancellationToken cancellationToken = default)
            => productRepository.FindProduct(id, cancellationToken);

        public async Task SaveProduct(Domain.Product product, CancellationToken cancellationToken = default)
        {
            await productRepository.SaveProduct(product, cancellationToken);
            await productSynchronizer.SynchronizeProduct(product, cancellationToken);
        }
    }
}
