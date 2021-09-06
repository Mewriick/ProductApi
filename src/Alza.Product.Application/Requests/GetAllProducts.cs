using Alza.Product.Application.Dtos;
using MediatR;
using System.Collections.Generic;

namespace Alza.Product.Application.Requests
{
    public class GetAllProducts : IRequest<IEnumerable<ProductDto>>
    {
        private const int DefaultTake = 10;

        public int Skip { get; }

        public int Take { get; }

        public GetAllProducts(int skip, int take)
        {
            Skip = skip < 0 ? 0 : skip;
            Take = take < 0 ? DefaultTake : take;
        }
    }
}
