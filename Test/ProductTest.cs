using Data.Entities;
using Data.Exceptions;

namespace Test
{
    public class ProductTest
    {
        [Fact]
        public void Product_IncreaseInventoryCount_ShouldIncreaseCount()
        {
            var product = new Product("Test Product", inventoryCount: 10, 5000, 25);

            product.IncreaseInventoryCount(5);

            Assert.Equal(15, product.InventoryCount);
        }

        [Fact]
        public void Product_DecreaseInventoryCount_WhenValueGraterThanInventoryCount_ThrowsException()
        {
            var hasException = false;
            try
            {
                var product = new Product("Test Product", inventoryCount: 10, 5000, 25);

                product.DecreaseInventoryCount(15);
            }
            catch (Exception exception)
            {
                if (exception is EntityException)
                    hasException = true;
            }

            Assert.True(hasException);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("title-title-title-title-title-title-title-title-title")]
        public void CreateProduct_WhenTitleIsValid_ThrowsException(string title)
        {
            var hasException = false;
            try
            {
                _ = new Product(title, 10, 10, 0);
            }
            catch (Exception exception)
            {
                if (exception is ArgumentException or EntityException)
                    hasException = true;
            }

            Assert.True(hasException);
        }
    }
}