using System.Threading;
using System.Threading.Tasks;

namespace Alza.Product.ReadModel
{
    public interface IProductSynchronizer
    {
        Task SynchronizeProduct(Domain.Product product, CancellationToken cancellationToken = default);
    }
}
