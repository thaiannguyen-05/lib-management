using WinFormsApp1.Models.Enums;

namespace WinFormsApp1.Models
{
    public class LateFee : BaseEntity
    {
        public int BorrowRecordId { get; set; }
        public BorrowRecord BorrowRecord { get; set; } = null!;

        public decimal Amount { get; set; }
        public DateTime DateIncurred { get; set; }
        public FeeStatus Status { get; set; } = FeeStatus.Unpaid;

        public int? WaivedByUserId { get; set; }
        public ApplicationUser? WaivedByUser { get; set; }

        public ICollection<FeePayment> Payments { get; set; } = new List<FeePayment>();
    }
}
