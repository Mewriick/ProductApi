using Alza.Product.ReadModel;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Alza.Product.Infrastructure.ReadModels
{
    public class ProductSynchronizer : IProductSynchronizer
    {
        private readonly ProductsReadDbContext productsReadDbContext;
        private readonly ILogger<ProductSynchronizer> logger;

        public ProductSynchronizer(ProductsReadDbContext productsReadDbContext, ILogger<ProductSynchronizer> logger)
        {
            this.productsReadDbContext = productsReadDbContext ?? throw new ArgumentNullException(nameof(productsReadDbContext));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task SynchronizeProduct(Domain.Product product, CancellationToken cancellationToken = default)
        {
            try
            {
                var readProduct = await productsReadDbContext.Products.FindAsync(product.Id);
                if (readProduct != null)
                {
                    productsReadDbContext.Products.Remove(readProduct);
                }

                productsReadDbContext.Products.Add(new ReadModel.Product
                {
                    Id = product.Id,
                    Name = product.Name.Value,
                    Description = product.Description.Value,
                    ImageUri = product.Image.Uri,
                    Price = product.Price.Value
                });

                await productsReadDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Product [{id}] sync failed.", product.Id);

                throw;
            }
        }
    }
}
