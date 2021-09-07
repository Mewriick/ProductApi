using Alza.Product.Domain.Events;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Alza.Product.Tests.Products
{
    public class ProductsTests
    {
        [Fact]
        public void ProductCreatedEventShouldExistsWhenNewProductIsCreated()
        {
            var product = new Domain.Product("Product 1", 100, "images/product1", "description 1");

            product.Events.Should().ContainItemsAssignableTo<ProductCreated>();
        }

        [Fact]
        public void ChangedDescEventShouldExistsWhenProductDescriptionIsChanged()
        {
            var product = new Domain.Product("Product 1", 100, "images/product1", "description 1");
            product.ChangeDescription("test");

            product.Events.OfType<ProductDescChanged>()
                .Should()
                .ContainSingle(e => e.OldDescription == "description 1" && e.Description == "test");

            product.Description.Value.Should().BeEquivalentTo("test");
        }
    }
}
