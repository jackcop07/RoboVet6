using System.Collections.Generic;
using System.Threading.Tasks;
using RoboVet6.Blazor.UI.Models;

namespace RoboVet6.Blazor.UI.Interfaces.Services
{
    public interface IProductDataService
    {
        Task<Product> GetProductById(int productId);
        Task<IEnumerable<Product>> GetAllProducts(string name);
        Task UpdateProduct(Product productToUpdate);
        Task<Product> AddProduct(Product productToUpdate);
    }
}
