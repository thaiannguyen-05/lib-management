using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Data;
using WinFormsApp1.Models;
using WinFormsApp1.Models.Enums;

namespace WinFormsApp1.Services
{
    public class LateFeeService
    {
        private readonly AppDbContext _context;

        public LateFeeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LateFee> CalculateLateFeeAsync(int borrowRecordId)
        {
            var borrowRecord = await _context.BorrowRecords
                .Include(br => br.BookCopy)
                    .ThenInclude(bc => bc.Book)
                .Include(br => br.LateFees)
                .FirstOrDefaultAsync(br => br.Id == borrowRecordId);//id không thấy thì báo k có ai mượn 

            if (borrowRecord == null)
                throw new InvalidOperationException("Borrow record not found.");

            var returnDate = borrowRecord.ReturnDate ?? DateTime.UtcNow;//ngày trả thực tế
            var dueDate = borrowRecord.DueDate;//ngày trả dự kiến

            if (borrowRecord.LateFees.Any(f => f.Type == FeeType.Late))//tránh trùng phí trả muộn 2 lần
                throw new InvalidOperationException("Late fee already exists for this borrow record.");
            //tính số ngày trả muộn
            var daysOverdue = Math.Max(0, (returnDate.Date - dueDate.Date).Days);
            if (daysOverdue <= 0)
                throw new InvalidOperationException("This borrow record is not overdue.");

            var replacementCost = borrowRecord.BookCopy.Book.ReplacementCost;
            var dailyRate = replacementCost * 0.01m;
            var amount =dailyRate * daysOverdue;//số tiền 1 ngày * số ngày quá hạn

            var lateFee = new LateFee
            {
                BorrowRecordId = borrowRecordId,
                Amount = amount,
                DateIncurred = DateTime.UtcNow,
                Type = FeeType.Late,
                Status = FeeStatus.Unpaid
            };

            _context.LateFees.Add(lateFee);
            await _context.SaveChangesAsync();
            return lateFee;
        }

        public async Task<decimal> CalculateLostFeeAsync(int bookCopyId)
        {
            var bookCopy = await _context.BookCopies
                .Include(bc => bc.Book)
                .FirstOrDefaultAsync(bc => bc.Id == bookCopyId);

            if (bookCopy == null)
                throw new InvalidOperationException("Book copy not found.");

            return bookCopy.Book.ReplacementCost;
        }

        public async Task<decimal> CalculateDamagedFeeAsync(int bookCopyId)
        {
            var bookCopy = await _context.BookCopies
                .Include(bc => bc.Book)
                .FirstOrDefaultAsync(bc => bc.Id == bookCopyId);

            if (bookCopy == null)
                throw new InvalidOperationException("Book copy not found.");

            return bookCopy.Book.ReplacementCost * 0.5m;
        }

        public async Task<bool> WaiveAsync(int lateFeeId, int userId)
        {
            var lateFee = await _context.LateFees.FirstOrDefaultAsync(lf => lf.Id == lateFeeId);
            if (lateFee == null)
                return false;

            if (lateFee.Status == FeeStatus.Paid || lateFee.Status == FeeStatus.Waived)
                return false;

            lateFee.Status = FeeStatus.Waived;
            lateFee.WaivedByUserId = userId;
            lateFee.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
        //lấy ra phí chưa trả của 1 user
        public async Task<List<LateFee>> GetUnpaidByMemberAsync(int memberId)
        {
            return await _context.LateFees
                .Include(lf => lf.BorrowRecord)
                    .ThenInclude(br => br.BookCopy)
                        .ThenInclude(bc => bc.Book)
                .Where(lf => lf.BorrowRecord.MemberId == memberId && lf.Status == FeeStatus.Unpaid)
                .OrderByDescending(lf => lf.DateIncurred)
                .ToListAsync();
        }
        //lấy toàn bộ phí của 1 user
        public async Task<List<LateFee>> GetAllByMemberAsync(int memberId)
        {
            return await _context.LateFees
                .Include(lf => lf.BorrowRecord)
                    .ThenInclude(br => br.BookCopy)
                        .ThenInclude(bc => bc.Book)
                .Include(lf => lf.WaivedByUser)
                .Include(lf => lf.Payments)
                .Where(lf => lf.BorrowRecord.MemberId == memberId)
                .OrderByDescending(lf => lf.DateIncurred)
                .ToListAsync();
        }
    }
}
