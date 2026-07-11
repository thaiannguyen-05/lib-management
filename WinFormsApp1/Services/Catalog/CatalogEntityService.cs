using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Data;
using WinFormsApp1.Models;

namespace WinFormsApp1.Services
{
    public abstract class CatalogEntityService<T> : ICatalogEntityService<T> where T : BaseEntity
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly AppDbContext _context;

        protected CatalogEntityService(IUnitOfWork unitOfWork, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _unitOfWork.Repository<T>().GetAllAsync();
        }

        public abstract Task<IReadOnlyList<T>> SearchByNameAsync(string name);

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Repository<T>().GetByIdAsync(id);
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Repository<T>().AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            await _unitOfWork.Repository<T>().UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            if (await HasBooksAsync(id))
                return false;

            await _unitOfWork.Repository<T>().DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public abstract Task<bool> HasBooksAsync(int entityId);
    }
}
