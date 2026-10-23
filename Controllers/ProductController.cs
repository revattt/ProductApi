using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Dtos;
using ProductApi.Entities;
using ProductApi.Interfaces;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productService.GetProductsAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(CreateProductDto dto)
        {
            var product = await _productService.CreateProductAsync(dto);
            return CreatedAtAction(
                nameof(GetProduct),
                new { id = product.Id },
                product
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto dto)
        {
            var success = await _productService.UpdateProductAsync(id, dto);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var success = await _productService.DeleteProductAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

