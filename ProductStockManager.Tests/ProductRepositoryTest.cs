using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ProductStockManager.Core;
using ProductStockManager.Infrastructure;
using ProductStockManager.Tests.TestData; // Import the factory
using System.Threading.Tasks;

namespace ProductStockManager.Tests;

[TestFixture]
public class ProductRepositoryTests
{
    private ProductContext _context;
    private ProductRepository _repository;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ProductContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_" + System.Guid.NewGuid())
            .Options;

        _context = new ProductContext(options);
        _repository = new ProductRepository(_context);
    }

    [Test]
    public async Task AddProduct_ShouldAssignProductId()
    {
        var product = ProductFactory.CreateDefault("100001");
        var result = await _repository.AddAsync(product);
        Assert.IsNotNull(result.ProductId);
    }

    [Test]
    public async Task GetProductById_ShouldReturnCorrectProduct()
    {
        var product = ProductFactory.CreateDefault("100002", "GetById Product");
        var created = await _repository.AddAsync(product);

        var fetched = await _repository.GetByIdAsync(created.ProductId);
        Assert.IsNotNull(fetched);
        Assert.AreEqual("GetById Product", fetched.Name);
    }

    [Test]
    public async Task UpdateProduct_ShouldModifyProductDetails()
    {
        var product = ProductFactory.CreateDefault("100003", "Old");
        var added = await _repository.AddAsync(product);
        added.Name = "Updated";

        var updated = await _repository.UpdateAsync(added);
        Assert.AreEqual("Updated", updated.Name);
    }

    [Test]
    public async Task DeleteProduct_ShouldRemoveProduct()
    {
        var product = ProductFactory.CreateDefault("100004", "ToDelete", 3);
        var added = await _repository.AddAsync(product);
        var result = await _repository.DeleteAsync(added.ProductId);

        Assert.IsTrue(result);
        Assert.IsNull(await _repository.GetByIdAsync(added.ProductId));
    }

    [Test]
    public async Task IncrementStock_ShouldIncreaseValue()
    {
        var product = ProductFactory.CreateDefault("100005", "Inc", 1);
        var added = await _repository.AddAsync(product);

        var result = await _repository.IncrementStockAsync(added.ProductId, 5);
        var updated = await _repository.GetByIdAsync(added.ProductId);

        Assert.IsTrue(result);
        Assert.AreEqual(6, updated.StockAvailable);
    }

    [Test]
    public async Task DecrementStock_ShouldDecreaseValue()
    {
        var product = ProductFactory.CreateDefault("100006", "Dec", 10);
        var added = await _repository.AddAsync(product);

        var result = await _repository.DecrementStockAsync(added.ProductId, 4);
        var updated = await _repository.GetByIdAsync(added.ProductId);

        Assert.IsTrue(result);
        Assert.AreEqual(6, updated.StockAvailable);
    }

    [Test]
    public async Task DecrementStock_ShouldFail_WhenNotEnoughStock()
    {
        var product = ProductFactory.CreateDefault("100007", "Limit", 2);
        var added = await _repository.AddAsync(product);

        var result = await _repository.DecrementStockAsync(added.ProductId, 5);
        Assert.IsFalse(result);
    }
}
