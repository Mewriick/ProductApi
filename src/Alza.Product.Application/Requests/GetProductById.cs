using Alza.Product.Application.Dtos;
using MediatR;
using System;

namespace Alza.Product.Application.Requests
{
    public class GetProductById : IRequest<ProductDto>
    {
        public Guid Id { get; }

        public GetProductById(Guid id)
        {
            Id = id;
        }
    }
}
