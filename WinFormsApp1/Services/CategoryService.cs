using WinFormsApp1.Data;
using WinFormsApp1.Models;

namespace WinFormsApp1.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<Category>> GetAllAsync()
        {
            return await _unitOfWork.Repository<Category>()
                .GetAllAsync();
        }

        public async Task<IReadOnlyList<Category>> SearchByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return await GetAllAsync();

            var lowerName = name.ToLower();
            return await _unitOfWork.Repository<Category>()
                .FindAsync(c => c.Name.ToLower().Contains(lowerName));
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Category>().GetByIdAsync(id);
        }

        public async Task<Category> CreateAsync(Category category)
        {
            category.CreatedAt = DateTime.UtcNow;
            category.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Repository<Category>().AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
            return category;
        }

        public async Task UpdateAsync(Category category)
        {
            category.UpdatedAt = DateTime.UtcNow;
            await _unitOfWork.Repository<Category>().UpdateAsync(category);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (await HasBooksAsync(id))
                return false;

            await _unitOfWork.Repository<Category>().DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> HasBooksAsync(int categoryId)
        {
            var cat = await _unitOfWork.Repository<Category>().GetByIdAsync(categoryId);
            return cat?.Books?.Any() ?? false;
        }
    }
}
