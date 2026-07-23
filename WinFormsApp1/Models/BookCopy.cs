using WinFormsApp1.Models.Enums;

namespace WinFormsApp1.Models
{
    public class BookCopy : BaseEntity
    {
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
        public string Barcode { get; set; } = string.Empty;
        public string? ShelfLocation { get; set; }
        public CopyStatus Status { get; set; } = CopyStatus.Available;

        public ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
        public ICollection<InventoryLog> InventoryLogs { get; set; } = new List<InventoryLog>();
    }
}
