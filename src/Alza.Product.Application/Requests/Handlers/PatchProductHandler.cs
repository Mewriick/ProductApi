using Alza.Product.Domain;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Alza.Product.Application.Requests.Handlers
{
    public class PatchProductHandler : IRequestHandler<PatchProduct, Result<bool, ValidationError>>
    {
        private readonly IProductRepository productRepository;
        private readonly ILogger<PatchProductHandler> logger;

        public PatchProductHandler(IProductRepository productRepository, ILogger<PatchProductHandler> logger)
        {
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<bool, ValidationError>> Handle(PatchProduct request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await productRepository.FindProduct(request.ProductId, cancellationToken);
                if (product is null)
                {
                    return Result.Failure<bool, ValidationError>(ValidationError.EntityNotFound);
                }

                if (request.JsonMergePatch.IsDefined(p => p.Description, out var desciption))
                {
                    product.ChangeDescription(desciption);
                }

                await productRepository.SaveProduct(product, cancellationToken);

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Patch product [{id}] failed.", request.ProductId);

                return Result.Failure<bool, ValidationError>(ValidationError.InternalServerError);
            }
        }
    }
}
