using Microsoft.AspNetCore.Mvc;
using ProductStockManager.Core;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repo;
    public ProductsController(IProductRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var product = await _repo.GetByIdAsync(id);
        return product == null ? NotFound() : Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        var result = await _repo.AddAsync(product);
        return CreatedAtAction(nameof(GetById), new { id = result.ProductId }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Product product)
    {
        if (id != product.ProductId) return BadRequest();
        return Ok(await _repo.UpdateAsync(product));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
        => await _repo.DeleteAsync(id) ? Ok() : NotFound();

    [HttpPut("add-to-stock/{id}/{qty}")]
    public async Task<IActionResult> AddStock(string id, int qty)
        => await _repo.IncrementStockAsync(id, qty) ? Ok() : NotFound();

    [HttpPut("decrement-stock/{id}/{qty}")]
    public async Task<IActionResult> DecreaseStock(string id, int qty)
        => await _repo.DecrementStockAsync(id, qty) ? Ok() : NotFound();
}
