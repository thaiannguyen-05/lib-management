using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Data;
using WinFormsApp1.Models;

namespace WinFormsApp1.Services
{
    public class AuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;

        public AuthorService(IUnitOfWork unitOfWork, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<IReadOnlyList<Author>> GetAllAsync()
        {
            return await _unitOfWork.Repository<Author>().GetAllAsync();
        }

        public async Task<IReadOnlyList<Author>> SearchByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return await GetAllAsync();

            return await _unitOfWork.Repository<Author>()
                .FindAsync(s => s.FirstName.Contains(name) || s.LastName.Contains(name));
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Author>().GetByIdAsync(id);
        }

        public async Task<Author> CreateAsync(Author entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Repository<Author>().AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Author entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            await _unitOfWork.Repository<Author>().UpdateAsync(entity);
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
            return await _context.Authors
                .Where(a => a.Id == authorId)
                .SelectMany(a => a.Books)
                .AnyAsync();
        }
    }
}
