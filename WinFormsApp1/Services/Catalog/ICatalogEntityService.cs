using WinFormsApp1.Models;

namespace WinFormsApp1.Services
{
    public interface ICatalogEntityService<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> SearchByNameAsync(string name);
        Task<T?> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> HasBooksAsync(int entityId);
    }
}
