using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Dtos;
using ProductApi.Entities;
using ProductApi.Interfaces;

namespace ProductApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }
        public async Task<Product> CreateProductAsync(CreateProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
            };
            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
            {
                return false;
            }
            _repository.Delete(product);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<bool> UpdateProductAsync(int id, UpdateProductDto dto)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
            {
                return false;
            }
            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Stock = dto.Stock;

            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
