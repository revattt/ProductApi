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
        private readonly ICategoryRepository _categoryRepository;

        private ProductResponseDto MapToDto(Product product)
        {
            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CreatedAt = product.CreatedAt,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name ?? "Unknown"
            };
        }
        public ProductService(IProductRepository repository, ICategoryRepository categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }
        public async Task<ProductResponseDto> CreateProductAsync(CreateProductDto dto)
        {
            bool categoryExists = await _categoryRepository.ExistsAsync(dto.CategoryId);
            if (!categoryExists) { throw new ArgumentException($"Category with ID {dto.CategoryId} does not exist."); }
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                Stock = dto.Stock,
                CreatedAt = DateTime.UtcNow
            };
            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();
            var savedProduct = await _repository.GetByIdAsync(product.Id);
            return MapToDto(savedProduct!);
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

        public async Task<PagedResultDto<ProductResponseDto>> GetPagedProductsAsync(
    int page,
    int pageSize,
    string? search,
    string? sortBy,
    bool descending)
        {
            if (page < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(page), "Page must be at least 1.");
            }

            if (pageSize < 1 || pageSize > 100)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(pageSize), "Page size must be between 1 and 100.");
            }

            var (items, totalCount) = await _repository.GetPagedAsync(
                page,
                pageSize,
                search,
                sortBy,
                descending
            );

            return new PagedResultDto<ProductResponseDto>
            {
                Items = items.Select(MapToDto),
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<ProductResponseDto?> GetProductByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if(product == null) { return null; }
            return MapToDto(product);
        }

        public async Task<IEnumerable<ProductResponseDto>> GetProductsAsync()
        {
            var products = await _repository.GetAllAsync();
            return products.Select(MapToDto);
        }

        public async Task<ProductResponseDto> UpdateProductAsync(int id, UpdateProductDto dto)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
            {
                return null!;
            }
            bool categoryExists = await _categoryRepository.ExistsAsync(dto.CategoryId);
            if (!categoryExists) { throw new ArgumentException($"Category with ID {dto.CategoryId} does not exist."); }
            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Stock = dto.Stock;
            product.CategoryId = dto.CategoryId;

            await _repository.SaveChangesAsync();
            return MapToDto(product);
        }
    }
}
