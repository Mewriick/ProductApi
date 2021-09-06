using Xunit;

namespace Alza.Product.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var product = new Domain.Product("Product 1", 100, "/images/product1", "desc 1");


        }
    }
}
