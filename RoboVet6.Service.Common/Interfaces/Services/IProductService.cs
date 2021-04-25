using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RoboVet6.Service.Common.Models.API.ApiResponse;
using RoboVet6.Service.Common.Models.API.Colour;
using RoboVet6.Service.Common.Models.API.Product;

namespace RoboVet6.Service.Common.Interfaces.Services
{
    public interface IProductService
    {
        Task<ApiResponse<ProductToReturnDto>> GetProductByProductId(int productId);
        Task<ApiResponse<List<ProductToReturnDto>>> GetAllProducts(string name);
        Task<ApiResponse<ProductToReturnDto>> InsertProduct(ProductToInsertDto colour);
        Task<bool> ProductExists(int productId);
        Task<ApiResponse<ProductToReturnDto>> UpdateProduct(int productId, ProductToUpdateDto product);
    }
}
