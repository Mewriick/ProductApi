using Alza.Product.Application;
using Alza.Product.Application.Dtos;
using Alza.Product.Application.Patch;
using Alza.Product.Application.Requests.Handlers;
using Alza.Product.Domain;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Alza.Product.Tests.Application
{
    public class ChangeDescriptionTests
    {
        [Fact]
        public async Task ChangeDescriptonShouldReturnFailureWhenProductDoesNotExits()
        {
            var jsonDocument = JsonDocument.Parse("{ \"Description\": \"Test Desc\"}");
            var patchModel = JsonMergePatch<ProductDto>.Create(jsonDocument);
            var repositoryMock = new Mock<IProductRepository>();

            repositoryMock.Setup(p => p.FindProduct(It.IsAny<Guid>(), CancellationToken.None))
             .Returns(Task.FromResult(null as Domain.Product));

            var handler = new PatchProductHandler(
                repositoryMock.Object,
                NullLogger<PatchProductHandler>.Instance);

            var result = await handler.Handle(
                new Product.Application.Requests.PatchProduct(Guid.NewGuid(), patchModel),
                CancellationToken.None);

            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().BeEquivalentTo(ValidationErrorCodes.NotFound);
        }

        [Fact]
        public async Task ChangeDescriptonShouldReturnFailureWhenExceptionOccured()
        {
            var jsonDocument = JsonDocument.Parse("{ \"Description\": \"Test Desc\"}");
            var patchModel = JsonMergePatch<ProductDto>.Create(jsonDocument);
            var repositoryMock = new Mock<IProductRepository>();

            repositoryMock.Setup(p => p.FindProduct(It.IsAny<Guid>(), CancellationToken.None))
                .Throws<IOException>();

            var handler = new PatchProductHandler(
                repositoryMock.Object,
                NullLogger<PatchProductHandler>.Instance);

            var result = await handler.Handle(
                new Product.Application.Requests.PatchProduct(Guid.NewGuid(), patchModel),
                CancellationToken.None);

            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().BeEquivalentTo(ValidationErrorCodes.InternalError);
        }

        [Fact]
        public async Task ChangeDescriptonShouldChangeDescriptionAndReturnSuccessWhenProductExists()
        {
            var jsonDocument = JsonDocument.Parse("{ \"Description\": \"Test Desc\"}");
            var patchModel = JsonMergePatch<ProductDto>.Create(jsonDocument);
            var repositoryMock = new Mock<IProductRepository>();
            var product = new Domain.Product("Product 1", 100, "images/product1", "description 1");

            repositoryMock.Setup(p => p.FindProduct(It.IsAny<Guid>(), CancellationToken.None))
                .Returns(Task.FromResult(product));

            var handler = new PatchProductHandler(
                repositoryMock.Object,
                NullLogger<PatchProductHandler>.Instance);

            var result = await handler.Handle(
                new Product.Application.Requests.PatchProduct(Guid.NewGuid(), patchModel),
                CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            product.Description.Value.Should().BeEquivalentTo("Test Desc");
        }

        [Fact]
        public async Task ChangeDescriptonShouldNotChangeDescriptionWhenJsonNotContainProperty()
        {
            var jsonDocument = JsonDocument.Parse("{ \"Desc\": \"Test Desc\"}");
            var patchModel = JsonMergePatch<ProductDto>.Create(jsonDocument);
            var repositoryMock = new Mock<IProductRepository>();
            var product = new Domain.Product("Product 1", 100, "images/product1", "description 1");

            repositoryMock.Setup(p => p.FindProduct(It.IsAny<Guid>(), CancellationToken.None))
                .Returns(Task.FromResult(product));

            var handler = new PatchProductHandler(
                repositoryMock.Object,
                NullLogger<PatchProductHandler>.Instance);

            var result = await handler.Handle(
                new Product.Application.Requests.PatchProduct(Guid.NewGuid(), patchModel),
                CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            product.Description.Value.Should().BeEquivalentTo("description 1");
        }
    }
}
