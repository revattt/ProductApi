using ProductApi.Dtos;
using ProductApi.Models;
using ProductApi.Entities;

namespace ProductApi.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponseDto>> GetProductsAsync();
        Task<ProductResponseDto?> GetProductByIdAsync(int id);
        Task<ProductResponseDto> CreateProductAsync(CreateProductDto dto);
        Task<ProductResponseDto> UpdateProductAsync(int id, UpdateProductDto dto);
        Task<bool> DeleteProductAsync(int id);
        Task<PagedResultDto<ProductResponseDto>> GetPagedProductsAsync(
            int page,
            int pageSize,
            string? search,
            string? sortBy,
            bool descending
            );
    }
}
