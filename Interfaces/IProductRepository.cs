using ProductApi.Entities;

namespace ProductApi.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task AddAsync (Product product);
        void Delete(Product product);
        Task SaveChangesAsync();
        Task<(IEnumerable<Product> Items, int TotalCount)> GetPagedAsync(
    int page,
    int pageSize,
    string? search,
    string? sortBy,
    bool descending);

    }
}
