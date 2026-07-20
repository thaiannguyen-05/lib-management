using WinFormsApp1.Models.Enums;

namespace WinFormsApp1.Models
{
    public class Reservation : BaseEntity
    {
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;

        public DateTime ReservationDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
    }
}
