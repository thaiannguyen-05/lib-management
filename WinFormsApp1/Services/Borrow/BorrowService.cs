using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Data;
using WinFormsApp1.Models;
using WinFormsApp1.Models.Enums;

namespace WinFormsApp1.Services;

public class BorrowService
{
    private readonly AppDbContext _context;
    private readonly LibraryCardService _libraryCardService;

    public BorrowService(AppDbContext context, LibraryCardService libraryCardService)
    {
        _context = context;
        _libraryCardService = libraryCardService;
    }

    public async Task<(bool Success, string Message)> CheckoutAsync(int memberId, int bookCopyId, int userId)
    {
        var member = await _context.Members.FindAsync(memberId);
        if (member == null)
            return (false, "Member not found.");

        if (member.Status != MemberStatus.Active)
            return (false, "Member account is not active.");

        var hasCard = await _libraryCardService.CheckCardValidAsync(memberId);
        if (!hasCard)
            return (false, "Library card is expired or not issued.");

        var hasUnpaidFees = await _context.LateFees
            .AnyAsync(lf => lf.BorrowRecord.MemberId == memberId && lf.Status == FeeStatus.Unpaid);
        if (hasUnpaidFees)
            return (false, "Member has unpaid late fees. Please settle them first.");

        var bookCopy = await _context.BookCopies
            .Include(bc => bc.Book)
            .FirstOrDefaultAsync(bc => bc.Id == bookCopyId);
        if (bookCopy == null)
            return (false, "Book copy not found.");

        if (bookCopy.Status != CopyStatus.Available)
            return (false, "This copy is not available for borrowing.");

        var borrowDate = DateTime.UtcNow;
        var dueDate = borrowDate.AddDays(14);

        var record = new BorrowRecord
        {
            BookCopyId = bookCopyId,
            MemberId = memberId,
            BorrowDate = borrowDate,
            DueDate = dueDate,
            Status = BorrowStatus.Active,
            RenewalCount = 0,
            CheckedOutByUserId = userId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        bookCopy.Status = CopyStatus.Borrowed;
        bookCopy.UpdatedAt = DateTime.UtcNow;

        _context.BorrowRecords.Add(record);
        await _context.SaveChangesAsync();

        return (true, $"Book '{bookCopy.Book.Title}' checked out successfully. Due: {dueDate:yyyy-MM-dd}");
    }

    public async Task<List<BorrowRecord>> GetActiveBorrowsAsync(int memberId)
    {
        return await _context.BorrowRecords
            .Where(br => br.MemberId == memberId && br.Status == BorrowStatus.Active)
            .Include(br => br.BookCopy)
                .ThenInclude(bc => bc.Book)
            .ToListAsync();
    }

    public async Task<List<BorrowRecord>> GetAllActiveAsync()
    {
        return await _context.BorrowRecords
            .Where(br => br.Status == BorrowStatus.Active)
            .Include(br => br.Member)
            .Include(br => br.BookCopy)
                .ThenInclude(bc => bc.Book)
            .ToListAsync();
    }

    public async Task<BorrowRecord?> GetByIdAsync(int id)
    {
        return await _context.BorrowRecords
            .Include(br => br.Member)
            .Include(br => br.BookCopy)
                .ThenInclude(bc => bc.Book)
            .Include(br => br.CheckedOutByUser)
            .FirstOrDefaultAsync(br => br.Id == id);
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

    public async Task<Member?> GetMemberByIdAsync(int memberId)
    {
        return await _context.Members
            .Include(m => m.LibraryCard)
            .FirstOrDefaultAsync(m => m.Id == memberId);
    }

    public async Task<BookCopy?> SearchBookCopyAsync(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return null;

        var term = keyword.Trim().ToLower();
        return await _context.BookCopies
            .Include(bc => bc.Book)
                .ThenInclude(b => b.Authors)
            .FirstOrDefaultAsync(bc =>
                bc.Barcode.ToLower().Contains(term) ||
                bc.Book.ISBN.ToLower().Contains(term) ||
                bc.Book.Title.ToLower().Contains(term));
    }

    public async Task<int> GetActiveBorrowCountAsync(int memberId)
    {
        return await _context.BorrowRecords
            .CountAsync(br => br.MemberId == memberId && br.Status == BorrowStatus.Active);
    }
}
