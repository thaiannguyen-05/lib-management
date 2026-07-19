using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Data;
using WinFormsApp1.Models.Enums;

namespace WinFormsApp1.Services
{
    public class ReportService
    {
        private readonly AppDbContext _context;

        public ReportService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<object>> GetCurrentlyBorrowedAsync(DateTime? from, DateTime? to)
        {
            var query = _context.BorrowRecords
                .Where(b => b.Status == BorrowStatus.Active)
                .Include(b => b.BookCopy)
                    .ThenInclude(c => c.Book)
                .Include(b => b.Member)
                .AsQueryable();

            if (from.HasValue)
                query = query.Where(b => b.BorrowDate >= from.Value);
            if (to.HasValue)
                query = query.Where(b => b.BorrowDate <= to.Value);

            return await query
                .OrderByDescending(b => b.BorrowDate)
                .Select(b => new
                {
                    BookTitle = b.BookCopy.Book.Title,
                    Barcode = b.BookCopy.Barcode,
                    MemberName = b.Member.FirstName + " " + b.Member.LastName,
                    BorrowDate = b.BorrowDate,
                    DueDate = b.DueDate
                })
                .ToListAsync<object>();
        }

        public async Task<List<object>> GetOverdueBooksAsync(DateTime? from, DateTime? to)
        {
            var query = _context.BorrowRecords
                .Where(b => b.Status == BorrowStatus.Active && b.DueDate < DateTime.UtcNow)
                .Include(b => b.BookCopy)
                    .ThenInclude(c => c.Book)
                .Include(b => b.Member)
                .AsQueryable();

            if (from.HasValue)
                query = query.Where(b => b.BorrowDate >= from.Value);
            if (to.HasValue)
                query = query.Where(b => b.BorrowDate <= to.Value);

            return await query
                .OrderBy(b => b.DueDate)
                .Select(b => new
                {
                    BookTitle = b.BookCopy.Book.Title,
                    Barcode = b.BookCopy.Barcode,
                    MemberName = b.Member.FirstName + " " + b.Member.LastName,
                    DueDate = b.DueDate,
                    DaysOverdue = EF.Functions.DateDiffDay(b.DueDate, DateTime.UtcNow)
                })
                .ToListAsync<object>();
        }

        public async Task<List<object>> GetMostBorrowedBooksAsync(int top, DateTime? from, DateTime? to)
        {
            var query = _context.BorrowRecords
                .Include(b => b.BookCopy)
                    .ThenInclude(c => c.Book)
                .AsQueryable();

            if (from.HasValue)
                query = query.Where(b => b.BorrowDate >= from.Value);
            if (to.HasValue)
                query = query.Where(b => b.BorrowDate <= to.Value);

            return await query
                .GroupBy(b => new { b.BookCopy.BookId, b.BookCopy.Book.Title })
                .Select(g => new
                {
                    BookTitle = g.Key.Title,
                    BorrowCount = g.Count()
                })
                .OrderByDescending(x => x.BorrowCount)
                .Take(top)
                .ToListAsync<object>();
        }

        public async Task<List<object>> GetMostActiveMembersAsync(int top, DateTime? from, DateTime? to)
        {
            var query = _context.BorrowRecords
                .Include(b => b.Member)
                .AsQueryable();

            if (from.HasValue)
                query = query.Where(b => b.BorrowDate >= from.Value);
            if (to.HasValue)
                query = query.Where(b => b.BorrowDate <= to.Value);

            return await query
                .GroupBy(b => new { b.MemberId, b.Member.FirstName, b.Member.LastName })
                .Select(g => new
                {
                    MemberName = g.Key.FirstName + " " + g.Key.LastName,
                    BorrowCount = g.Count()
                })
                .OrderByDescending(x => x.BorrowCount)
                .Take(top)
                .ToListAsync<object>();
        }

        public async Task<decimal> GetTotalFeesAsync(DateTime? from, DateTime? to)
        {
            var query = _context.LateFees.AsQueryable();

            if (from.HasValue)
                query = query.Where(f => f.DateIncurred >= from.Value);
            if (to.HasValue)
                query = query.Where(f => f.DateIncurred <= to.Value);

            return await query.SumAsync(f => f.Amount);
        }

        public async Task<List<object>> GetLostDamagedBooksAsync(DateTime? from, DateTime? to)
        {
            var query = _context.BookCopies
                .Where(c => c.Status == CopyStatus.Lost || c.Status == CopyStatus.Damaged)
                .Include(c => c.Book)
                .AsQueryable();

            if (from.HasValue)
                query = query.Where(c => c.CreatedAt >= from.Value);
            if (to.HasValue)
                query = query.Where(c => c.CreatedAt <= to.Value);

            return await query
                .Select(c => new
                {
                    BookTitle = c.Book.Title,
                    Barcode = c.Barcode,
                    Status = c.Status.ToString(),
                    ShelfLocation = c.ShelfLocation ?? ""
                })
                .ToListAsync<object>();
        }
    }
}
