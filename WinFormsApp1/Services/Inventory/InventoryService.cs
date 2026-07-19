using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Data;
using WinFormsApp1.Models;
using WinFormsApp1.Models.Enums;

namespace WinFormsApp1.Services
{
    public class InventoryService
    {
        private readonly AppDbContext _context;

        public InventoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BookCopy>> ImportBooksAsync(int bookId, int quantity, int userId)
        {
            var copies = new List<BookCopy>();
            string today = DateTime.Now.ToString("yyyyMMdd");
            int startNum = await _context.BookCopies
                .Where(c => c.Barcode.StartsWith($"BC-{today}-"))
                .CountAsync() + 1;

            for (int i = 0; i < quantity; i++)
            {
                var copy = new BookCopy
                {
                    BookId = bookId,
                    Barcode = $"BC-{today}-{(startNum + i):D3}",
                    Status = CopyStatus.Available,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.BookCopies.Add(copy);
                copies.Add(copy);
            }

            await _context.SaveChangesAsync();

            foreach (var copy in copies)
            {
                _context.InventoryLogs.Add(new InventoryLog
                {
                    BookCopyId = copy.Id,
                    Action = InventoryAction.Import,
                    Quantity = 1,
                    PerformedByUserId = userId
                });
            }

            await _context.SaveChangesAsync();
            return copies;
        }

        public async Task<bool> DisposeBookAsync(int bookCopyId, string? reason, int userId)
        {
            var copy = await _context.BookCopies.FindAsync(bookCopyId);
            if (copy == null) return false;

            _context.InventoryLogs.Add(new InventoryLog
            {
                BookCopyId = bookCopyId,
                Action = InventoryAction.Dispose,
                Quantity = 1,
                Note = reason,
                PerformedByUserId = userId
            });

            _context.BookCopies.Remove(copy);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> TransferShelfAsync(int bookCopyId, string newShelf, int userId)
        {
            var copy = await _context.BookCopies.FindAsync(bookCopyId);
            if (copy == null) return false;

            copy.ShelfLocation = newShelf;
            copy.UpdatedAt = DateTime.UtcNow;

            _context.InventoryLogs.Add(new InventoryLog
            {
                BookCopyId = bookCopyId,
                Action = InventoryAction.Transfer,
                Quantity = 1,
                Note = newShelf,
                PerformedByUserId = userId
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Dictionary<CopyStatus, int>> CountInventoryAsync()
        {
            return await _context.BookCopies
                .GroupBy(c => c.Status)
                .ToDictionaryAsync(g => g.Key, g => g.Count());
        }

        public async Task<bool> ReportLostAsync(int bookCopyId, int userId)
        {
            var copy = await _context.BookCopies.FindAsync(bookCopyId);
            if (copy == null) return false;

            copy.Status = CopyStatus.Lost;
            copy.UpdatedAt = DateTime.UtcNow;

            _context.InventoryLogs.Add(new InventoryLog
            {
                BookCopyId = bookCopyId,
                Action = InventoryAction.Lost,
                Quantity = 1,
                PerformedByUserId = userId
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ReportDamagedAsync(int bookCopyId, int userId)
        {
            var copy = await _context.BookCopies.FindAsync(bookCopyId);
            if (copy == null) return false;

            copy.Status = CopyStatus.Damaged;
            copy.UpdatedAt = DateTime.UtcNow;

            _context.InventoryLogs.Add(new InventoryLog
            {
                BookCopyId = bookCopyId,
                Action = InventoryAction.Damaged,
                Quantity = 1,
                PerformedByUserId = userId
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<InventoryLog>> GetLogsAsync(int? bookCopyId = null, InventoryAction? action = null)
        {
            var query = _context.InventoryLogs
                .Include(l => l.BookCopy)
                    .ThenInclude(c => c.Book)
                .Include(l => l.PerformedByUser)
                .AsQueryable();

            if (bookCopyId.HasValue)
                query = query.Where(l => l.BookCopyId == bookCopyId.Value);

            if (action.HasValue)
                query = query.Where(l => l.Action == action.Value);

            return await query.OrderByDescending(l => l.CreatedAt).ToListAsync();
        }
    }
}
