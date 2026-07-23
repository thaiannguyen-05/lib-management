using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Data;
using WinFormsApp1.Models;
using WinFormsApp1.Models.Enums;

namespace WinFormsApp1.Services
{
    public class FeePaymentService
    {
        private readonly AppDbContext _context;

        public FeePaymentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FeePayment> PayAsync(int lateFeeId, decimal amount)
        {
            if (amount <= 0)
                throw new InvalidOperationException("Số tiền phải lớn hơn 0.");

            var lateFee = await _context.LateFees
                .Include(lf => lf.Payments)
                .FirstOrDefaultAsync(lf => lf.Id == lateFeeId);

            if (lateFee == null)
                throw new InvalidOperationException("Late fee not found.");

            if (lateFee.Status == FeeStatus.Waived)
                throw new InvalidOperationException("This fee has been waived.");

            if (lateFee.Status == FeeStatus.Paid)
                throw new InvalidOperationException("This fee has already been paid.");

            var payment = new FeePayment
            {
                LateFeeId = lateFeeId,
                Amount = amount,
                PaymentDate = DateTime.UtcNow
            };

            _context.FeePayments.Add(payment);

            var totalPaid = lateFee.Payments.Sum(p => p.Amount) + amount;
            if (totalPaid >= lateFee.Amount)
            {
                lateFee.Status = FeeStatus.Paid;
            }
            
            lateFee.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return payment;
        }

        public async Task<List<FeePayment>> GetPaymentsByFeeAsync(int lateFeeId)
        {
            return await _context.FeePayments
                .Where(fp => fp.LateFeeId == lateFeeId)
                .OrderBy(fp => fp.PaymentDate)
                .ToListAsync();
        }
    }
}
