using WinFormsApp1.Models;

namespace WinFormsApp1.Services
{
    public interface ICategoryService
    {
        Task<IReadOnlyList<Category>> GetAllAsync();
        Task<IReadOnlyList<Category>> SearchByNameAsync(string name);
        Task<Category?> GetByIdAsync(int id);
        Task<Category> CreateAsync(Category category);
        Task UpdateAsync(Category category);
        Task<bool> DeleteAsync(int id);
        Task<bool> HasBooksAsync(int categoryId);
    }
}
