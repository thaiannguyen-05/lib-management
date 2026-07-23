using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Data;
using WinFormsApp1.Models;
using WinFormsApp1.Models.Enums;

namespace WinFormsApp1.Services;

public class RenewalService
{
    private const int MaxRenewals = 2;
    private const int RenewalDays = 14;

    private readonly AppDbContext _context;

    public RenewalService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(bool Success, string Message)> RenewAsync(int borrowRecordId)
    {
        var borrowRecord = await _context.BorrowRecords
            .Include(br => br.BookCopy)
                .ThenInclude(bc => bc.Book)
            .Include(br => br.Member)
            .FirstOrDefaultAsync(br => br.Id == borrowRecordId);

        if (borrowRecord == null)
            return (false, "Borrow record not found.");

        if (borrowRecord.Status != BorrowStatus.Active)
            return (false, "This borrow record is not active.");

        if (borrowRecord.RenewalCount >= MaxRenewals)
            return (false, $"This borrow record has already been renewed the maximum of {MaxRenewals} times.");

        var hasPendingReservation = await _context.Reservations
            .AnyAsync(r => r.BookId == borrowRecord.BookCopy.BookId && r.Status == ReservationStatus.Pending);

        if (hasPendingReservation)
            return (false, "This book has a pending reservation. Renewal is not allowed.");

        var hasUnpaidLateFee = await _context.LateFees
            .AnyAsync(lf => lf.BorrowRecord.MemberId == borrowRecord.MemberId && lf.Status == FeeStatus.Unpaid);

        if (hasUnpaidLateFee)
            return (false, "This member has unpaid late fees. Please settle them first.");

        borrowRecord.DueDate = borrowRecord.DueDate.AddDays(RenewalDays);
        borrowRecord.RenewalCount++;
        borrowRecord.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return (true, $"Renewal successful. New due date: {borrowRecord.DueDate:yyyy-MM-dd}");
    }

    public async Task<int> GetRenewalInfoAsync(int borrowRecordId)
    {
        var borrowRecord = await _context.BorrowRecords
            .AsNoTracking()
            .FirstOrDefaultAsync(br => br.Id == borrowRecordId);

        if (borrowRecord == null)
            throw new InvalidOperationException("Borrow record not found.");

        return Math.Max(0, MaxRenewals - borrowRecord.RenewalCount);
    }
}
