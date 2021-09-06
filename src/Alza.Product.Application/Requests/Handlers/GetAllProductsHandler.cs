using Alza.Product.Application.Dtos;
using Alza.Product.ReadModel;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alza.Product.Application.Requests.Handlers
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProducts, IEnumerable<ProductDto>>
    {
        private readonly IProductsQueryable productsQueryable;
        private readonly IMapper mapper;

        public GetAllProductsHandler(IProductsQueryable productsQueryable, IMapper mapper)
        {
            this.productsQueryable = productsQueryable ?? throw new System.ArgumentNullException(nameof(productsQueryable));
            this.mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetAllProducts request, CancellationToken cancellationToken)
        {
            var products = await productsQueryable.GetAllProducts(request.Skip, request.Take, cancellationToken);

            return mapper.Map<IEnumerable<ProductDto>>(products);
        }
    }
}
