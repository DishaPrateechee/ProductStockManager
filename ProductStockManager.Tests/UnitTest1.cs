using ProductStockManager.Infrastructure;
using ProductStockManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using NUnit.Framework;

namespace ProductStockManager.Tests;

[TestFixture]
public class ProductRepositoryTests
{
    private readonly ProductContext _context;
    public ProductRepositoryTests(ProductContext context)
    {
        _context = context;
    }
    private ProductRepository _repository;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ProductContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        _repository = new ProductRepository(_context);
    }

    [Test]
    public async Task AddProduct_ShouldReturnProductWithId()
    {
        var product = new Product { Name = "Test", Description = "Test", StockAvailable = 10 };
        var result = await _repository.AddAsync(product);
        Assert.IsNotNull(result.ProductId);
    }

    [Test]
    public async Task DecrementStock_ShouldFail_WhenStockTooLow()
    {
        var product = new Product { Name = "LowStock", StockAvailable = 1 };
        await _repository.AddAsync(product);
        var result = await _repository.DecrementStockAsync(product.ProductId, 5);
        Assert.IsFalse(result);
    }
}
