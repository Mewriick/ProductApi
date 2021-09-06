using System;
using System.Threading;
using System.Threading.Tasks;

namespace Alza.Product.Domain
{
    public interface IProductRepository
    {
        Task SaveProduct(Product product, CancellationToken cancellationToken = default);

        Task<Product> FindProduct(Guid id, CancellationToken cancellationToken = default);
    }
}
