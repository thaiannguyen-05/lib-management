using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Data;
using WinFormsApp1.Models;
using WinFormsApp1.Models.Enums;

namespace WinFormsApp1.Services
{
    public class InventoryService
    {
        private readonly AppDbContext _context;
        private readonly BookCopyService _bookCopyService;

        public InventoryService(AppDbContext context, BookCopyService bookCopyService)
        {
            _context = context;
            _bookCopyService = bookCopyService;
        }

        public async Task<List<BookCopy>> ImportBooksAsync(int bookId, int quantity, int userId)
        {
            var copies = new List<BookCopy>();

            for (int i = 0; i < quantity; i++)
            {
                var copy = new BookCopy
                {
                    BookId = bookId,
                    Status = CopyStatus.Available
                };

                var created = await _bookCopyService.CreateAsync(copy);

                _context.InventoryLogs.Add(new InventoryLog
                {
                    BookCopyId = created.Id,
                    Action = InventoryAction.Import,
                    Quantity = 1,
                    PerformedByUserId = userId
                });

                copies.Add(created);
            }

            await _context.SaveChangesAsync();
            return copies;
        }

        public async Task<bool> DisposeBookAsync(int bookCopyId, string? reason, int userId)
        {
            var copy = await _context.BookCopies.FindAsync(bookCopyId);
            if (copy == null) return false;

            await _bookCopyService.UpdateStatusAsync(bookCopyId, CopyStatus.Lost);

            _context.InventoryLogs.Add(new InventoryLog
            {
                BookCopyId = bookCopyId,
                Action = InventoryAction.Dispose,
                Quantity = 1,
                Note = reason,
                PerformedByUserId = userId
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> TransferShelfAsync(int bookCopyId, string newShelf, int userId)
        {
            var copy = await _context.BookCopies.FindAsync(bookCopyId);
            if (copy == null) return false;

            await _bookCopyService.UpdateShelfLocationAsync(bookCopyId, newShelf);

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

            await _bookCopyService.UpdateStatusAsync(bookCopyId, CopyStatus.Lost);

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

            await _bookCopyService.UpdateStatusAsync(bookCopyId, CopyStatus.Damaged);

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
