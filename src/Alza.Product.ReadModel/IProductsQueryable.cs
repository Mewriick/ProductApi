using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alza.Product.ReadModel
{
    public interface IProductsQueryable
    {
        Task<List<Product>> GetAllProducts(
            int skip,
            int take,
            CancellationToken cancellationToken = default);

        Task<Product> GetProduct(
            Guid id,
            CancellationToken cancellationToken = default);
    }
}
