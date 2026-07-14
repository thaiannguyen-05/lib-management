using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Data;
using WinFormsApp1.Models;
using WinFormsApp1.Models.Enums;

namespace WinFormsApp1.Services
{
    public class LibraryCardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;

        public LibraryCardService(IUnitOfWork unitOfWork, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<LibraryCard?> GetByMemberIdAsync(int memberId)
        {
            var results = await _unitOfWork.Repository<LibraryCard>()
                .FindAsync(c => c.MemberId == memberId);
            return results.FirstOrDefault();
        }

        public async Task<bool> CheckCardValidAsync(int memberId)
        {
            var card = await GetByMemberIdAsync(memberId);
            return card != null && card.Status == CardStatus.Active && card.ExpiryDate > DateTime.Now;
        }

        public async Task<(bool Success, string Message)> IssueCardAsync(int memberId)
        {
            var member = await _unitOfWork.Repository<Member>().GetByIdAsync(memberId);
            if (member == null)
                return (false, "Member not found.");

            var existingCard = await GetByMemberIdAsync(memberId);
            if (existingCard != null)
                return (false, "Member already has a library card.");

            var cardNumber = await GenerateCardNumberAsync();

            var card = new LibraryCard
            {
                MemberId = memberId,
                CardNumber = cardNumber,
                ExpiryDate = DateTime.Now.AddYears(1),
                Status = CardStatus.Active,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Repository<LibraryCard>().AddAsync(card);
            await _unitOfWork.SaveChangesAsync();

            return (true, $"Card issued successfully. Card Number: {cardNumber}");
        }

        public async Task<(bool Success, string Message)> RenewCardAsync(int cardId, RenewalPeriod period)
        {
            var card = await _unitOfWork.Repository<LibraryCard>().GetByIdAsync(cardId);
            if (card == null)
                return (false, "Card not found.");

            if (card.Status == CardStatus.Locked)
                return (false, "Cannot renew a locked card.");

            int months = (int)period;

            if (card.Status == CardStatus.Active && card.ExpiryDate > DateTime.Now)
                card.ExpiryDate = card.ExpiryDate.AddMonths(months);
            else
                card.ExpiryDate = DateTime.Now.AddMonths(months);

            card.Status = CardStatus.Active;
            card.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Repository<LibraryCard>().UpdateAsync(card);
            await _unitOfWork.SaveChangesAsync();

            return (true, $"Card renewed for {months} month(s). New expiry: {card.ExpiryDate:yyyy-MM-dd}");
        }

        public async Task<(bool Success, string Message)> LockCardAsync(int cardId)
        {
            var card = await _unitOfWork.Repository<LibraryCard>().GetByIdAsync(cardId);
            if (card == null)
                return (false, "Card not found.");

            if (card.Status == CardStatus.Locked)
                return (false, "Card is already locked.");

            card.Status = CardStatus.Locked;
            card.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Repository<LibraryCard>().UpdateAsync(card);
            await _unitOfWork.SaveChangesAsync();

            return (true, "Card locked successfully.");
        }

        public async Task<(bool Success, string Message)> UnlockCardAsync(int cardId)
        {
            var card = await _unitOfWork.Repository<LibraryCard>().GetByIdAsync(cardId);
            if (card == null)
                return (false, "Card not found.");

            if (card.Status == CardStatus.Active)
                return (false, "Card is already active.");

            card.Status = CardStatus.Active;
            card.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Repository<LibraryCard>().UpdateAsync(card);
            await _unitOfWork.SaveChangesAsync();

            return (true, "Card unlocked successfully.");
        }

        private async Task<string> GenerateCardNumberAsync()
        {
            string datePart = DateTime.Now.ToString("yyyyMMdd");
            string prefix = $"TL-{datePart}-";

            var existingCards = await _context.LibraryCards
                .Where(c => c.CardNumber.StartsWith(prefix))
                .ToListAsync();

            int nextNumber = existingCards.Count + 1;

            return $"{prefix}{nextNumber:D3}";
        }
    }
}
