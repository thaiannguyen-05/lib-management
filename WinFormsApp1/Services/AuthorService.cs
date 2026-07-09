using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Data;
using WinFormsApp1.Models;

namespace WinFormsApp1.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<Author>> GetAllAsync()
        {
            return await _unitOfWork.Repository<Author>()
                .GetAllAsync();
        }

        public async Task<IReadOnlyList<Author>> SearchByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return await GetAllAsync();

            var lowerName = name.ToLower();
            return await _unitOfWork.Repository<Author>()
                .FindAsync(a => a.FirstName.ToLower().Contains(lowerName)
                             || a.LastName.ToLower().Contains(lowerName));
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Author>().GetByIdAsync(id);
        }

        public async Task<Author> CreateAsync(Author author)
        {
            author.CreatedAt = DateTime.UtcNow;
            author.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Repository<Author>().AddAsync(author);
            await _unitOfWork.SaveChangesAsync();
            return author;
        }

        public async Task UpdateAsync(Author author)
        {
            author.UpdatedAt = DateTime.UtcNow;
            await _unitOfWork.Repository<Author>().UpdateAsync(author);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (await HasBooksAsync(id))
                return false;

            await _unitOfWork.Repository<Author>().DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> HasBooksAsync(int authorId)
        {
            var author = await _unitOfWork.Repository<Author>().GetByIdAsync(authorId);
            return author?.Books?.Any() ?? false;
        }
    }
}
