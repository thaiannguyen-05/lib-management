using System.Linq.Expressions;

namespace WinFormsApp1.Data
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<int> CountAsync();
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    }
}
