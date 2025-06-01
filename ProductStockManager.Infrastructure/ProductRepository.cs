using ProductStockManager.Core;
using Microsoft.EntityFrameworkCore;

namespace ProductStockManager.Infrastructure;

public class ProductRepository : IProductRepository
{
    private readonly ProductContext _context;
    public ProductRepository(ProductContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product> GetByIdAsync(string id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<Product> AddAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> IncrementStockAsync(string id, int qty)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;
        product.StockAvailable += qty;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DecrementStockAsync(string id, int qty)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null || product.StockAvailable < qty) return false;
        product.StockAvailable -= qty;
        await _context.SaveChangesAsync();
        return true;
    }

    
}