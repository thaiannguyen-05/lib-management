namespace WinFormsApp1.Models
{
    public class AuditLog : BaseEntity
    {
        public int UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        public string Action { get; set; } = string.Empty;
        public string EntityName { get; set; } = string.Empty;
        public int EntityId { get; set; }
        public string? Details { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
