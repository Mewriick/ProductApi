using Alza.Product.Application.Dtos;
using Alza.Product.Application.Patch;
using CSharpFunctionalExtensions;
using MediatR;
using System;

namespace Alza.Product.Application.Requests
{
    public class PatchProduct : IRequest<Result<bool, ValidationError>>
    {
        public Guid ProductId { get; }

        public JsonMergePatch<ProductDto> JsonMergePatch { get; }

        public PatchProduct(Guid productId, JsonMergePatch<ProductDto> jsonMergePatch)
        {
            ProductId = productId;
            JsonMergePatch = jsonMergePatch ?? throw new ArgumentNullException(nameof(jsonMergePatch));
        }
    }
}
