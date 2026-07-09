using WinFormsApp1.Models.Enums;

namespace WinFormsApp1.Models
{
    public class LibraryCard : BaseEntity
    {
        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;

        public string CardNumber { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public CardStatus Status { get; set; } = CardStatus.Active;
    }
}
