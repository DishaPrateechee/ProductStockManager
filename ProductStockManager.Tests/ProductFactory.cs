using System;
using ProductStockManager.Core;

namespace ProductStockManager.Tests.TestData
{
    public static class ProductFactory
    {
        public static Product CreateDefault(string id = "123456", string name = "Test Product", int stock = 10)
        {
            return new Product
            {
                ProductId = id,
                Name = name,
                Description = "Sample Description",
                CreatedOn = DateTime.UtcNow,
                StockAvailable = stock
            };
        }

        public static Product CreateOutOfStock(string id = "999999")
        {
            return new Product
            {
                ProductId = id,
                Name = "OutOfStock Product",
                StockAvailable = 0
            };
        }
    }
}