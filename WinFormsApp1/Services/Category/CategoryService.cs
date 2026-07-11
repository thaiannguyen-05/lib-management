using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Data;
using WinFormsApp1.Models;

namespace WinFormsApp1.Services
{
    public class CategoryService : CatalogEntityService<Category>, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork, AppDbContext context)
            : base(unitOfWork, context)
        {
        }

        public override async Task<IReadOnlyList<Category>> SearchByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return await GetAllAsync();

            var pattern = $"%{name}%";
            return await _unitOfWork.Repository<Category>()
                .FindAsync(c => EF.Functions.Like(c.Name, pattern));
        }

        public override async Task<bool> HasBooksAsync(int categoryId)
        {
            return await _context.Categories
                .Where(c => c.Id == categoryId)
                .SelectMany(c => c.Books)
                .AnyAsync();
        }
    }
}
