using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Data;
using WinFormsApp1.Models;
using WinFormsApp1.Models.Enums;

namespace WinFormsApp1.Services
{
    public class BookCopyService
    {
        private readonly AppDbContext _context;

        public BookCopyService(AppDbContext context)
        {
            _context = context;
        }

        // Get all copies of a specific book
        public async Task<List<BookCopy>> GetByBookIdAsync(int bookId)
        {
            return await _context.BookCopies
                .Where(c => c.BookId == bookId)
                .Include(c => c.Book)
                .ToListAsync();
        }

        // Create a new copy with auto-generated barcode
        public async Task<BookCopy> CreateAsync(BookCopy copy)
        {
            copy.Barcode = await GenerateBarcodeAsync();
            copy.CreatedAt = DateTime.UtcNow;
            copy.UpdatedAt = DateTime.UtcNow;

            _context.BookCopies.Add(copy);
            await _context.SaveChangesAsync();
            return copy;
        }

        // Change the status of a copy
        public async Task UpdateStatusAsync(int copyId, CopyStatus status)
        {
            var copy = await _context.BookCopies.FindAsync(copyId);
            if (copy == null) return;

            copy.Status = status;
            copy.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        // Change the shelf location of a copy
        public async Task UpdateShelfLocationAsync(int copyId, string shelf)
        {
            var copy = await _context.BookCopies.FindAsync(copyId);
            if (copy == null) return;

            copy.ShelfLocation = shelf;
            copy.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        // Delete a copy (only if status is Available)
        public async Task<bool> DeleteAsync(int copyId)
        {
            var copy = await _context.BookCopies.FindAsync(copyId);
            if (copy == null) return false;

            // Only allow deleting copies that are Available
            if (copy.Status != CopyStatus.Available)
                return false;

            _context.BookCopies.Remove(copy);
            await _context.SaveChangesAsync();
            return true;
        }

        // Get only available copies of a book
        public async Task<List<BookCopy>> GetAvailableCopiesAsync(int bookId)
        {
            return await _context.BookCopies
                .Where(c => c.BookId == bookId && c.Status == CopyStatus.Available)
                .Include(c => c.Book)
                .ToListAsync();
        }

        // Generate a unique barcode like BC-20260716-001
        private async Task<string> GenerateBarcodeAsync()
        {
            string today = DateTime.Now.ToString("yyyyMMdd");
            string prefix = $"BC-{today}-";

            // Find how many barcodes exist today
            int count = await _context.BookCopies
                .Where(c => c.Barcode.StartsWith(prefix))
                .CountAsync();

            int nextNumber = count + 1;
            return $"{prefix}{nextNumber:D3}";
        }
    }
}
