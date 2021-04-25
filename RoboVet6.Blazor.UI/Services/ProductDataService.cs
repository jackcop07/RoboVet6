using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RoboVet6.Blazor.UI.Interfaces;
using RoboVet6.Blazor.UI.Interfaces.Services;
using RoboVet6.Blazor.UI.Models;

namespace RoboVet6.Blazor.UI.Services
{
    public class ProductDataService : IProductDataService
    {
        private readonly HttpClient _httpClient;

        public ProductDataService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("RV6Api");
        }

        public async Task<Product> GetProductById(int productId)
        {
            return await JsonSerializer.DeserializeAsync<Product>
                (await _httpClient.GetStreamAsync($"api/products/{productId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<IEnumerable<Product>> GetAllProducts(string name)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Product>>
                (await _httpClient.GetStreamAsync($"api/products?name={name}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task UpdateProduct(Product productToUpdate)
        {
            var productJson =
                new StringContent(JsonSerializer.Serialize(productToUpdate), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync($"api/products/{productToUpdate.Id}", productJson);
        }

        public async Task<Product> AddProduct(Product productToAdd)
        {
            var productJson =
                new StringContent(JsonSerializer.Serialize(productToAdd), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/products", productJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Product>(await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true});
            }

            return null;
        }
    }
}
