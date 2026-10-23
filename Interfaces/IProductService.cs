using ProductApi.Dtos;
using ProductApi.Entities;

namespace ProductApi.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(CreateProductDto dto);
        Task<bool> UpdateProductAsync(int id, UpdateProductDto dto);
        Task<bool> DeleteProductAsync(int id);
    }
}
