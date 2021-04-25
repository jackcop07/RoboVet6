using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Internal;
using RoboVet6.Data.Models.RoboVet6;
using RoboVet6.DataAccess.Common.Interfaces;
using RoboVet6.Service.Common.Interfaces.Helpers;
using RoboVet6.Service.Common.Interfaces.Services;
using RoboVet6.Service.Common.Models.API.ApiResponse;
using RoboVet6.Service.Common.Models.API.Product;

namespace RoboVet6.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductIRepository _productIRepository;
        private readonly IMapper _mapper;
        private readonly IProductHelper _productHelper;

        public ProductService(IProductIRepository productIRepository, IMapper mapper, IProductHelper productHelper)
        {
            _productIRepository = productIRepository
                                  ?? throw new ArgumentNullException(nameof(productIRepository));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
            _productHelper = productHelper;
        }


        public async Task<ApiResponse<ProductToReturnDto>> GetProductByProductId(int productId)
        {
            var response = new ApiResponse<ProductToReturnDto>();

            try
            {
                var productFromRepo = await _productIRepository.GetProductByProductId(productId);

                if (productFromRepo == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                var productToReturn = _mapper.Map<ProductToReturnDto>(productFromRepo);

                response.StatusCode = HttpStatusCode.OK;
                response.Data = productToReturn;

                return response;

            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Error = e.Message;

                return response;
            }
        }

        public async Task<ApiResponse<List<ProductToReturnDto>>> GetAllProducts(string name)
        {
            var response = new ApiResponse<List<ProductToReturnDto>>();

            try
            {
                var productsFromRepo = await _productIRepository.GetAllProducts(name);

                if (!productsFromRepo.Any())
                {
                    response.StatusCode = HttpStatusCode.NoContent;
                    return response;
                }

                var productsToReturn = _mapper.Map<List<ProductToReturnDto>>(productsFromRepo);

                response.StatusCode = HttpStatusCode.OK;
                response.Data = productsToReturn;

                return response;
            }
            catch (Exception e)
            {
                response.Error = e.Message;
                response.StatusCode= HttpStatusCode.InternalServerError;

                return response;
            }
        }

        public async Task<ApiResponse<ProductToReturnDto>> InsertProduct(ProductToInsertDto colour)
        {
            var response = new ApiResponse<ProductToReturnDto>();

            try
            {
                var productMapped = _mapper.Map<ProductModel>(colour);

                productMapped.PriceExcVat = _productHelper.CalculatePriceExcVat(20, productMapped.PriceIncVat);

                await _productIRepository.InsertProduct(productMapped);

                response.StatusCode = HttpStatusCode.Created;
                response.Data = _mapper.Map<ProductToReturnDto>(productMapped);

                return response;
            }
            catch (Exception e)
            {
                response.Error = e.Message;
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<bool> ProductExists(int productId)
        {
            return await _productIRepository.ProductExists(productId);
        }

        public async Task<ApiResponse<ProductToReturnDto>> UpdateProduct(int productId, ProductToUpdateDto product)
        {
            var response = new ApiResponse<ProductToReturnDto>();

            try
            {
                var productFromRepo = await _productIRepository.GetProductByProductId(productId);

                if (productFromRepo == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                _mapper.Map(product, productFromRepo);

                productFromRepo.PriceExcVat = _productHelper.CalculatePriceExcVat(20, productFromRepo.PriceIncVat);

                await _productIRepository.UpdateProduct(productFromRepo);

                response.StatusCode = HttpStatusCode.NoContent;
                return response;
            }
            catch (Exception e)
            {
                response.Error = e.Message;
                response.StatusCode = HttpStatusCode.InternalServerError;

                return response;
            }
        }
    }
}
