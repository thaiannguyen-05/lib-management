using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Data;
using WinFormsApp1.Models;
using WinFormsApp1.Models.Enums;

namespace WinFormsApp1.Services;

public class ReturnService
{
    private readonly AppDbContext _context;

    public ReturnService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(bool Success, string Message)> ReturnAsync(int borrowRecordId, int userId, string condition)
    {
        var record = await _context.BorrowRecords
            .Include(br => br.BookCopy)
                .ThenInclude(bc => bc.Book)
            .FirstOrDefaultAsync(br => br.Id == borrowRecordId);

        if (record == null)
            return (false, "Borrow record not found.");

        if (record.Status != BorrowStatus.Active)
            return (false, "This borrow record is not active.");

        record.ReturnDate = DateTime.UtcNow;
        record.ReturnedByUserId = userId;
        record.Status = BorrowStatus.Returned;
        record.UpdatedAt = DateTime.UtcNow;

        switch (condition)
        {
            case "damaged":
                record.BookCopy.Status = CopyStatus.Damaged;
                break;
            case "lost":
                record.BookCopy.Status = CopyStatus.Lost;
                break;
            default:
                record.BookCopy.Status = CopyStatus.Available;
                break;
        }
        record.BookCopy.UpdatedAt = DateTime.UtcNow;

        if (record.ReturnDate > record.DueDate)
        {
            var daysOverdue = (record.ReturnDate.Value - record.DueDate).Days;
            var amount = daysOverdue * 5000m;

            var lateFee = new LateFee
            {
                BorrowRecordId = borrowRecordId,
                Amount = amount,
                DateIncurred = DateTime.UtcNow,
                Type = FeeType.Late,
                Status = FeeStatus.Unpaid,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _context.LateFees.Add(lateFee);
        }

        await _context.SaveChangesAsync();

        return (true, $"Book '{record.BookCopy.Book.Title}' returned successfully.");
    }

    public async Task<(int Returned, int Failed, string Message)> ReturnMultipleAsync(List<int> borrowRecordIds, int userId, string condition)
    {
        int returned = 0;
        int failed = 0;

        foreach (var id in borrowRecordIds)
        {
            var result = await ReturnAsync(id, userId, condition);
            if (result.Success)
                returned++;
            else
                failed++;
        }

        var msg = $"Returned {returned} book(s).";
        if (failed > 0)
            msg += $" Failed: {failed}.";

        return (returned, failed, msg);
    }

    public async Task<Member?> SearchMemberAsync(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return null;

        var term = keyword.Trim().ToLower();
        return await _context.Members
            .Include(m => m.LibraryCard)
            .FirstOrDefaultAsync(m =>
                m.Email.ToLower().Contains(term) ||
                (m.LibraryCard != null && m.LibraryCard.CardNumber.ToLower().Contains(term)) ||
                m.FirstName.ToLower().Contains(term) ||
                m.LastName.ToLower().Contains(term));
    }

    public async Task<List<BorrowRecord>> GetActiveBorrowsAsync(int memberId)
    {
        return await _context.BorrowRecords
            .Where(br => br.MemberId == memberId && br.Status == BorrowStatus.Active)
            .Include(br => br.BookCopy)
                .ThenInclude(bc => bc.Book)
            .ToListAsync();
    }

    public async Task<List<BorrowRecord>> GetOverdueAsync()
    {
        var now = DateTime.UtcNow;
        return await _context.BorrowRecords
            .Where(br => br.Status == BorrowStatus.Active && br.DueDate < now)
            .Include(br => br.Member)
            .Include(br => br.BookCopy)
                .ThenInclude(bc => bc.Book)
            .OrderBy(br => br.DueDate)
            .ToListAsync();
    }
}
