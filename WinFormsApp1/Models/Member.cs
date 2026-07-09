using WinFormsApp1.Models.Enums;

namespace WinFormsApp1.Models
{
    public class Member : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public MemberStatus Status { get; set; } = MemberStatus.Active;

        public ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
        public ICollection<LateFee> LateFees { get; set; } = new List<LateFee>();
    }
}
