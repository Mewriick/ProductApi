using Alza.Product.ReadModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Alza.Product.Infrastructure.ReadModels
{
    public class ProductsQueryable : IProductsQueryable
    {
        private readonly ProductsReadDbContext productsReadDbContext;
        private readonly ILogger<ProductsQueryable> logger;

        public ProductsQueryable(ProductsReadDbContext productsReadDbContext, ILogger<ProductsQueryable> logger)
        {
            this.productsReadDbContext = productsReadDbContext ?? throw new ArgumentNullException(nameof(productsReadDbContext));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<List<ReadModel.Product>> GetAllProducts(
            int skip,
            int take,
            CancellationToken cancellationToken = default)
        {
            try
            {
                return productsReadDbContext.Products
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Fetching products failed");

                throw;
            }
        }

        public async Task<ReadModel.Product> GetProduct(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var product = await productsReadDbContext.Products
                    .SingleOrDefaultAsync(
                        p => p.Id == id,
                        cancellationToken: cancellationToken);

                return product;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Fetching product failed");

                throw;
            }
        }
    }
}
