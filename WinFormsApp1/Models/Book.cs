namespace WinFormsApp1.Models
{
    public class Book : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string? Publisher { get; set; }
        public int? PublicationYear { get; set; }
        public string? Description { get; set; }
        public string? ShelfLocation { get; set; }
        public decimal ReplacementCost { get; set; }

        public ICollection<BookCopy> Copies { get; set; } = new List<BookCopy>();
        public ICollection<Author> Authors { get; set; } = new List<Author>();
        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
