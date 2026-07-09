using WinFormsApp1.Models.Enums;

namespace WinFormsApp1.Models
{
    public class BorrowRecord : BaseEntity
    {
        public int BookCopyId { get; set; }
        public BookCopy BookCopy { get; set; } = null!;

        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;

        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public BorrowStatus Status { get; set; } = BorrowStatus.Active;
        public int RenewalCount { get; set; }

        public int CheckedOutByUserId { get; set; }
        public ApplicationUser CheckedOutByUser { get; set; } = null!;

        public int? ReturnedByUserId { get; set; }
        public ApplicationUser? ReturnedByUser { get; set; }

        public ICollection<LateFee> LateFees { get; set; } = new List<LateFee>();
    }
}
