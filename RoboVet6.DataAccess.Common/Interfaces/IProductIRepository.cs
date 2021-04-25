using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RoboVet6.Data.Models.RoboVet6;

namespace RoboVet6.DataAccess.Common.Interfaces
{
    public interface IProductIRepository
    {
        Task<List<ProductModel>> GetAllProducts(string name);
        Task<ProductModel> GetProductByProductId(int productId);
        Task InsertProduct(ProductModel product);
        Task UpdateProduct(ProductModel product);
        Task<bool> ProductExists(int productId);
    }
}
