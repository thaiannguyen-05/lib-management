using WinFormsApp1.Models.Enums;

namespace WinFormsApp1.Models
{
    public class ApplicationUser : BaseEntity
    {
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Staff;

        public ICollection<BorrowRecord> CheckedOutBorrows { get; set; } = new List<BorrowRecord>();
        public ICollection<BorrowRecord> ReturnedBorrows { get; set; } = new List<BorrowRecord>();
        public ICollection<LateFee> WaivedFees { get; set; } = new List<LateFee>();
        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
        public ICollection<InventoryLog> PerformedInventoryLogs { get; set; } = new List<InventoryLog>();
    }
}
