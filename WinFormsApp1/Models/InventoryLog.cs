using WinFormsApp1.Models.Enums;

namespace WinFormsApp1.Models
{
    public class InventoryLog : BaseEntity
    {
        public int BookCopyId { get; set; }
        public BookCopy BookCopy { get; set; } = null!;

        public InventoryAction Action { get; set; }
        public int Quantity { get; set; }
        public string? Note { get; set; }

        public int PerformedByUserId { get; set; }
        public ApplicationUser PerformedByUser { get; set; } = null!;
    }
}
