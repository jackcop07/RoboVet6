using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using RoboVet6.Service.Common.Interfaces.Services;
using RoboVet6.Service.Common.Models.API.Product;

namespace RoboVet6.API.Controllers
{
    [Route("api/Products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService
                ?? throw new ArgumentNullException(nameof(productService));
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ProductToReturnDto>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetAllProducts(string name)
        {
            var result = await _productService.GetAllProducts(name);

            if (result.StatusCode == HttpStatusCode.NoContent)
            {
                return NoContent();
            }

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return Ok(result.Data);
            }

            return StatusCode(500, result.Error);
        }

        [HttpGet("{productId}", Name = "GetProductByProductId")]
        [ProducesResponseType(200, Type = typeof(ProductToReturnDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetProductByProductId(int productId)
        {
            var result = await _productService.GetProductByProductId(productId);

            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return Ok(result.Data);
            }

            return StatusCode(500, result.Error);
        }


        [HttpPut("{productId}")]
        [ProducesResponseType(200, Type = typeof(ProductToReturnDto))]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> UpdateProduct(int productId, ProductToUpdateDto product)
        {
            var result = await _productService.UpdateProduct(productId, product);

            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }

            if (result.StatusCode == HttpStatusCode.NoContent)
            {
                return NoContent();
            }

            return StatusCode(500, result.Error);
        }


        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ProductToReturnDto))]
        [ProducesResponseType(403)]
        public async Task<IActionResult> InsertProduct(ProductToInsertDto product)
        {
            var result = await _productService.InsertProduct(product);

            if (result.StatusCode == HttpStatusCode.Created)
            {
                return CreatedAtRoute("GetProductByProductId", new { productId = result.Data.Id }, result.Data);
            }

            return StatusCode(500, result.Error);
        }


    }
}
