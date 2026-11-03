using ProductApi.Entities;
using ProductApi.Models;

namespace ProductApi.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task AddAsync(Category category);
        void Delete(Category category);
        Task<bool> HasProductsAsync(int categoryId);
        Task SaveChangesAsync();
        Task<bool> ExistsAsync(int categoryId);
    }
}
