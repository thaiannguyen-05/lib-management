using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Data;
using WinFormsApp1.Models;

namespace WinFormsApp1.Services
{
    public class PublisherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;

        public PublisherService(IUnitOfWork unitOfWork, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<IReadOnlyList<Publisher>> GetAllAsync()
        {
            return await _unitOfWork.Repository<Publisher>().GetAllAsync();
        }

        public async Task<IReadOnlyList<Publisher>> SearchByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return await GetAllAsync();
            }
            else
            {
                return await _unitOfWork.Repository<Publisher>().FindAsync(p => p.Name.ToLower().Contains(name));
            }
        }

        public async Task<Publisher?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Publisher>().GetByIdAsync(id);
        }

        public async Task<Publisher> CreateAsync(Publisher entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Repository<Publisher>().AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Publisher entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            await _unitOfWork.Repository<Publisher>().UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (await HasBooksAsync(id))
                return false;

            await _unitOfWork.Repository<Publisher>().DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> HasBooksAsync(int publisherId)
        {
            return await _context.Publishers
                .Where(p => p.Id == publisherId)
                .SelectMany(p => p.Books)
                .AnyAsync();
        }
    }
}
