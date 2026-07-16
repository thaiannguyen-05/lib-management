using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Data;
using WinFormsApp1.Models;
using WinFormsApp1.Models.Enums;

namespace WinFormsApp1.Services;

public class BookService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly AuditService _auditService;
    private readonly AppDbContext _context;

    public BookService(IUnitOfWork unitOfWork, AuditService auditService, AppDbContext context)
    {
        _unitOfWork = unitOfWork;
        _auditService = auditService;
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _context.Books
            .AsNoTracking()
            .Include(b => b.Publisher)
            .Include(b => b.Authors)
            .Include(b => b.Categories)
            .Include(b => b.Copies)
            .ToListAsync();
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
        return await _context.Books
            .Include(b => b.Publisher)
            .Include(b => b.Authors)
            .Include(b => b.Categories)
            .Include(b => b.Copies)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Book?> GetBookByIsbnAsync(string isbn)
    {
        return await _context.Books
            .Include(b => b.Publisher)
            .Include(b => b.Authors)
            .Include(b => b.Categories)
            .FirstOrDefaultAsync(b => b.ISBN == isbn);
    }

    public async Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return await GetAllBooksAsync();

        var term = searchTerm.Trim();

        return await _context.Books
            .AsNoTracking()
            .Include(b => b.Publisher)
            .Include(b => b.Authors)
            .Include(b => b.Categories)
            .Include(b => b.Copies)
            .Where(b =>
                b.Title.ToLower().Trim().Contains(term) ||
                b.ISBN.Contains(term) ||
                b.Authors.Any(a => a.FirstName.ToLower().Trim().Contains(term) || a.LastName.ToLower().Trim().Contains(term)) ||
                b.Categories.Any(c => c.Name.ToLower().Trim().Contains(term)) ||
                (b.Publisher != null && b.Publisher.Name.ToLower().Trim().Contains(term)))
            .ToListAsync();
    }

    public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(int authorId)
    {
        return await _context.Books
            .AsNoTracking()
            .Include(b => b.Publisher)
            .Include(b => b.Authors)
            .Include(b => b.Categories)
            .Include(b => b.Copies)
            .Where(b => b.Authors.Any(a => a.Id == authorId))
            .ToListAsync();
    }

    public async Task<IEnumerable<Book>> GetBooksByCategoryAsync(int categoryId)
    {
        return await _context.Books
            .AsNoTracking()
            .Include(b => b.Publisher)
            .Include(b => b.Authors)
            .Include(b => b.Categories)
            .Include(b => b.Copies)
            .Where(b => b.Categories.Any(c => c.Id == categoryId))
            .ToListAsync();
    }

    public async Task<IEnumerable<Book>> GetBooksByPublisherAsync(int publisherId)
    {
        return await _context.Books
            .AsNoTracking()
            .Include(b => b.Publisher)
            .Include(b => b.Authors)
            .Include(b => b.Categories)
            .Include(b => b.Copies)
            .Where(b => b.PublisherId == publisherId)
            .ToListAsync();
    }

    public async Task<int> GetAvailableCopiesCountAsync(int bookId)
    {
        return await _context.BookCopies
            .Where(bc => bc.BookId == bookId && bc.Status == CopyStatus.Available)
            .CountAsync();
    }

    public async Task<Book> AddBookAsync(Book book, int[] authorIds, int[] categoryIds)
    {
        book.CreatedAt = DateTime.UtcNow;
        book.UpdatedAt = DateTime.UtcNow;

        if (authorIds.Length > 0)
        {
            book.Authors = await _context.Authors
                .Where(a => authorIds.Contains(a.Id))
                .ToListAsync();
        }

        if (categoryIds.Length > 0)
        {
            book.Categories = await _context.Categories
                .Where(c => categoryIds.Contains(c.Id))
                .ToListAsync();
        }

        await _unitOfWork.Repository<Book>().AddAsync(book);
        await _unitOfWork.SaveChangesAsync();
        return book;
    }

    public async Task UpdateBookAsync(Book book, int[] authorIds, int[] categoryIds)
    {
        var existing = await _context.Books
            .Include(b => b.Authors)
            .Include(b => b.Categories)
            .FirstOrDefaultAsync(b => b.Id == book.Id);

        if (existing == null) return;

        existing.Title = book.Title;
        existing.ISBN = book.ISBN;
        existing.PublisherId = book.PublisherId;
        existing.PublicationYear = book.PublicationYear;
        existing.Description = book.Description;
        existing.ShelfLocation = book.ShelfLocation;
        existing.ReplacementCost = book.ReplacementCost;
        existing.UpdatedAt = DateTime.UtcNow;

        existing.Authors.Clear();
        if (authorIds.Length > 0)
        {
            existing.Authors = await _context.Authors
                .Where(a => authorIds.Contains(a.Id))
                .ToListAsync();
        }

        existing.Categories.Clear();
        if (categoryIds.Length > 0)
        {
            existing.Categories = await _context.Categories
                .Where(c => categoryIds.Contains(c.Id))
                .ToListAsync();
        }

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        var book = await _context.Books
            .Include(b => b.Copies)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book == null) return false;

        if (book.Copies.Any(c => c.Status == CopyStatus.Borrowed))
            return false;

        await _unitOfWork.Repository<Book>().DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<BookCopy> AddBookCopyAsync(BookCopy copy)
    {
        copy.CreatedAt = DateTime.UtcNow;
        copy.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Repository<BookCopy>().AddAsync(copy);
        await _unitOfWork.SaveChangesAsync();
        return copy;
    }

    public async Task UpdateBookCopyAsync(BookCopy copy)
    {
        var existing = await _context.BookCopies.FindAsync(copy.Id);
        if (existing == null) return;

        existing.Status = copy.Status;
        existing.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> DeleteBookCopyAsync(int copyId)
    {
        var copy = await _context.BookCopies
            .Include(bc => bc.BorrowRecords)
            .FirstOrDefaultAsync(bc => bc.Id == copyId);

        if (copy == null) return false;

        if (copy.BorrowRecords.Any(br => br.Status == BorrowStatus.Active))
            return false;

        await _unitOfWork.Repository<BookCopy>().DeleteAsync(copyId);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<BookCopy>> GetCopiesByBookIdAsync(int bookId)
    {
        return await _context.BookCopies
            .AsNoTracking()
            .Where(bc => bc.BookId == bookId)
            .ToListAsync();
    }

    public async Task<BookCopy?> GetBookCopyByIdAsync(int id)
    {
        return await _context.BookCopies
            .Include(bc => bc.Book)
            .FirstOrDefaultAsync(bc => bc.Id == id);
    }

    public async Task<IEnumerable<Publisher>> GetAllPublishersAsync()
    {
        return await _context.Publishers.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
    {
        return await _context.Authors.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await _context.Categories.AsNoTracking().ToListAsync();
    }
}
