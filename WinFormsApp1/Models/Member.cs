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
        public MemberType MemberType { get; set; } = MemberType.External;

        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }

        public int? StudentClassId { get; set; }
        public StudentClass? StudentClass { get; set; }

        public ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
        public ICollection<LateFee> LateFees { get; set; } = new List<LateFee>();
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public LibraryCard? LibraryCard { get; set; }
    }
}
