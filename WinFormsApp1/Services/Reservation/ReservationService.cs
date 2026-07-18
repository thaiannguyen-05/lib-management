using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Data;
using WinFormsApp1.Models;
using WinFormsApp1.Models.Enums;

namespace WinFormsApp1.Services
{
    public class ReservationService
    {
        private readonly AppDbContext _context;

        public ReservationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Reservation>> GetAllAsync()
        {
            return await _context.Reservations
                .Include(r => r.Book)
                .Include(r => r.Member)
                .OrderBy(r => r.Id)
                .ToListAsync();
        }

        public async Task<Reservation?> GetByIdAsync(int id)
        {
            return await _context.Reservations
                .Include(r => r.Book)
                .Include(r => r.Member)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IReadOnlyList<Reservation>> GetByMemberIdAsync(int memberId)
        {
            return await _context.Reservations
                .Where(r => r.MemberId == memberId)
                .Include(r => r.Book)
                .OrderBy(r => r.Id)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Reservation>> GetPendingByBookIdAsync(int bookId)
        {
            return await _context.Reservations
                .Where(r => r.BookId == bookId && r.Status == ReservationStatus.Pending)
                .Include(r => r.Member)
                .OrderBy(r => r.Id)
                .ToListAsync();
        }

        public async Task<(bool Success, string Message)> CreateAsync(int bookId, int memberId)
        {
            var member = await _context.Members.FindAsync(memberId);
            if (member == null)
                return (false, "Member not found.");

            var hasAvailableCopy = await _context.BookCopies
                .AnyAsync(c => c.BookId == bookId && c.Status == CopyStatus.Available);

            if (hasAvailableCopy)
                return (false, "This book currently has available copies. No need to reserve.");

            var hasUnpaidFees = await _context.LateFees
                .AnyAsync(lf => lf.BorrowRecord.MemberId == memberId && lf.Status == FeeStatus.Unpaid);

            if (hasUnpaidFees)
                return (false, "Member has unpaid fees. Please clear dues before reserving.");

            if (member.ActiveReservationCount >= member.ReservationQuota)
                return (false, $"Member has reached the maximum of {member.ReservationQuota} active reservations. Return or cancel a reservation first.");

            var hasExisting = await _context.Reservations
                .AnyAsync(r => r.BookId == bookId
                    && r.MemberId == memberId
                    && (r.Status == ReservationStatus.Pending
                        || r.Status == ReservationStatus.Ready
                        || r.Status == ReservationStatus.Collected));

            if (hasExisting)
                return (false, "Member already has an active reservation for this book.");

            var reservation = new Reservation
            {
                BookId = bookId,
                MemberId = memberId,
                ReservationDate = DateTime.UtcNow,
                ExpiryDate = null,
                Status = ReservationStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            member.ActiveReservationCount--;
            member.UpdatedAt = DateTime.UtcNow;

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return (true, "Reservation created successfully.");
        }

        public async Task<(bool Success, string Message)> CancelAsync(int reservationId)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId);
            if (reservation == null)
                return (false, "Reservation not found.");

            if (reservation.Status != ReservationStatus.Pending && reservation.Status != ReservationStatus.Ready)
                return (false, $"Cannot cancel a reservation with status '{reservation.Status}'.");

            var member = await _context.Members.FindAsync(reservation.MemberId);
            if (member == null)
                return (false, "Member not found.");

            reservation.Status = ReservationStatus.Cancelled;
            reservation.UpdatedAt = DateTime.UtcNow;

            member.ActiveReservationCount++;
            member.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return (true, "Reservation cancelled successfully.");
        }

        public async Task<(bool Success, string Message)> FulfillAsync(int reservationId)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId);
            if (reservation == null)
                return (false, "Reservation not found.");

            if (reservation.Status != ReservationStatus.Pending)
                return (false, $"Cannot fulfill a reservation with status '{reservation.Status}'. Only Pending reservations can be fulfilled.");

            reservation.Status = ReservationStatus.Ready;
            reservation.ExpiryDate = DateTime.UtcNow.AddDays(7);
            reservation.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();  

            return (true, "Reservation fulfilled. Member has 7 days to collect the book.");
        }

        public async Task<int> CheckAndExpireAsync()
        {
            var expiredReservations = await _context.Reservations
                .Where(r => r.Status == ReservationStatus.Ready
                    && r.ExpiryDate != null
                    && r.ExpiryDate < DateTime.UtcNow)
                .ToListAsync();

            foreach (var reservation in expiredReservations)
            {
                reservation.Status = ReservationStatus.NoShow;
                reservation.UpdatedAt = DateTime.UtcNow;

                var member = await _context.Members.FindAsync(reservation.MemberId);
                if (member != null)
                {
                    member.ActiveReservationCount++;
                    member.UpdatedAt = DateTime.UtcNow;
                }
            }

            await _context.SaveChangesAsync();
            return expiredReservations.Count;
        }

        public async Task<int> AutoFulfillAllAsync()
        {
            var booksWithPendingReservations = await _context.Reservations
                .Where(r => r.Status == ReservationStatus.Pending)
                .Select(r => r.BookId)
                .Distinct()
                .ToListAsync();

            int totalFulfilled = 0;

            foreach (var bookId in booksWithPendingReservations)
            {
                var availableCopies = await _context.BookCopies
                    .CountAsync(c => c.BookId == bookId && c.Status == CopyStatus.Available);

                if (availableCopies <= 0) continue;

                var pendingReservations = await _context.Reservations
                    .Where(r => r.BookId == bookId && r.Status == ReservationStatus.Pending)
                    .OrderBy(r => r.Id)
                    .Take(availableCopies)
                    .ToListAsync();

                foreach (var reservation in pendingReservations)
                {
                    reservation.Status = ReservationStatus.Ready;
                    reservation.ExpiryDate = DateTime.UtcNow.AddDays(7);
                    reservation.UpdatedAt = DateTime.UtcNow;
                    totalFulfilled++;
                }
            }

            if (totalFulfilled > 0)
                await _context.SaveChangesAsync();

            return totalFulfilled;
        }

        public async Task<(bool Success, string Message)> NotifyBookAvailableAsync(int bookId)
        {
            var reservation = await _context.Reservations
                .Where(r => r.BookId == bookId && r.Status == ReservationStatus.Pending)
                .OrderBy(r => r.Id)
                .FirstOrDefaultAsync();

            if (reservation == null)
                return (false, "No pending reservations found for this book.");

            reservation.Status = ReservationStatus.Ready;
            reservation.ExpiryDate = DateTime.UtcNow.AddDays(7);
            reservation.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return (true, $"Reservation #{reservation.Id} is ready. Member has 7 days to collect.");
        }

        public async Task<(bool Success, string Message)> CollectAsync(int reservationId)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId);
            if (reservation == null)
                return (false, "Reservation not found.");

            if (reservation.Status != ReservationStatus.Ready)
                return (false, $"Cannot collect a reservation with status '{reservation.Status}'. Only Ready reservations can be collected.");

            reservation.Status = ReservationStatus.Collected;
            reservation.ExpiryDate = null;
            reservation.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return (true, "Book collected. Reservation marked as Collected.");
        }

        public async Task<(bool Success, string Message)> CompleteAsync(int reservationId)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId);
            if (reservation == null)
                return (false, "Reservation not found.");

            if (reservation.Status != ReservationStatus.Collected)
                return (false, $"Cannot complete a reservation with status '{reservation.Status}'. Only Collected reservations can be completed.");

            var member = await _context.Members.FindAsync(reservation.MemberId);
            if (member == null)
                return (false, "Member not found.");

            reservation.Status = ReservationStatus.Completed;
            reservation.UpdatedAt = DateTime.UtcNow;

            member.ActiveReservationCount++;
            member.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return (true, "Reservation completed. Book has been returned.");
        }
    }
}
