using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Data;
using WinFormsApp1.Models;

namespace WinFormsApp1.Services
{
    public class AuthorService : CatalogEntityService<Author>, IAuthorService
    {
        public AuthorService(IUnitOfWork unitOfWork, AppDbContext context)
            : base(unitOfWork, context)
        {
        }

        public override async Task<IReadOnlyList<Author>> SearchByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return await GetAllAsync();

            var pattern = $"%{name}%";
            return await _unitOfWork.Repository<Author>()
                .FindAsync(a => EF.Functions.Like(a.FirstName, pattern)
                             || EF.Functions.Like(a.LastName, pattern));
        }

        public override async Task<bool> HasBooksAsync(int authorId)
        {
            return await _context.Authors
                .Where(a => a.Id == authorId)
                .SelectMany(a => a.Books)
                .AnyAsync();
        }
    }
}
