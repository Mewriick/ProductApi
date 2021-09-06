using Alza.Product.Application.Dtos;
using Alza.Product.ReadModel;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Alza.Product.Application.Requests.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductById, ProductDto>
    {
        private readonly IProductsQueryable productsQueryable;
        private readonly IMapper mapper;

        public GetProductByIdHandler(IProductsQueryable productsQueryable, IMapper mapper)
        {
            this.productsQueryable = productsQueryable ?? throw new System.ArgumentNullException(nameof(productsQueryable));
            this.mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }

        public async Task<ProductDto> Handle(GetProductById request, CancellationToken cancellationToken)
        {
            var product = await productsQueryable.GetProduct(request.Id, cancellationToken);

            return mapper.Map<ProductDto>(product);
        }
    }
}
