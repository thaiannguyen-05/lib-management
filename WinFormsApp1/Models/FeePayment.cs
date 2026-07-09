namespace WinFormsApp1.Models
{
    public class FeePayment : BaseEntity
    {
        public int LateFeeId { get; set; }
        public LateFee LateFee { get; set; } = null!;

        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
