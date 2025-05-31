using Microsoft.AspNetCore.Mvc;
using ProductStockManager.Core;
using System.Net.Http;
using System.Net.Http.Json;

namespace ProductStockManager.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _client;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("ProductApi");
        }

        public async Task<IActionResult> Index()
        {
            var products = await _client.GetFromJsonAsync<List<Product>>("api/products");
            return View(products);
        }

        public async Task<IActionResult> Details(string id)
        {
            var product = await _client.GetFromJsonAsync<Product>($"api/products/{id}");
            return View(product);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var response = await _client.PostAsJsonAsync("api/products", product);
            if (response.IsSuccessStatusCode) return RedirectToAction(nameof(Index));
            return View(product);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var product = await _client.GetFromJsonAsync<Product>($"api/products/{id}");
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, Product product)
        {
            var response = await _client.PutAsJsonAsync($"api/products/{id}", product);
            if (response.IsSuccessStatusCode) return RedirectToAction(nameof(Index));
            return View(product);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var response = await _client.DeleteAsync($"api/products/{id}");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddStock(string id, int qty)
        {
            var response = await _client.PutAsync($"api/products/add-to-stock/{id}/{qty}", null);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DecreaseStock(string id, int qty)
        {
            var response = await _client.PutAsync($"api/products/decrement-stock/{id}/{qty}", null);
            return RedirectToAction(nameof(Index));
        }
    }
}
