using WinFormsApp1.Models;

namespace WinFormsApp1.Services
{
    public interface IAuthorService
    {
        Task<IReadOnlyList<Author>> GetAllAsync();
        Task<IReadOnlyList<Author>> SearchByNameAsync(string name);
        Task<Author?> GetByIdAsync(int id);
        Task<Author> CreateAsync(Author author);
        Task UpdateAsync(Author author);
        Task<bool> DeleteAsync(int id);
        Task<bool> HasBooksAsync(int authorId);
    }
}
