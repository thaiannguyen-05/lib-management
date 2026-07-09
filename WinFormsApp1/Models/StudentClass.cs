namespace WinFormsApp1.Models
{
    public class StudentClass : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;

        public ICollection<Member> Members { get; set; } = new List<Member>();
    }
}
