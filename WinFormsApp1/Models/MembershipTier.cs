namespace WinFormsApp1.Models
{
    public class MembershipTier : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int MaxBorrowLimit { get; set; }

        public ICollection<Member> Members { get; set; } = new List<Member>();
    }
}
