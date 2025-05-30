namespace ProductStockManager.Core;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(string id);
    Task<Product> AddAsync(Product product);
    Task<Product> UpdateAsync(Product product);
    Task<bool> DeleteAsync(string id);
    Task<bool> IncrementStockAsync(string id, int qty);
    Task<bool> DecrementStockAsync(string id, int qty);
}