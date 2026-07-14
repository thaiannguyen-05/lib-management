using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models;
using WinFormsApp1.Models.Enums;

namespace WinFormsApp1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books => Set<Book>();
        public DbSet<BookCopy> BookCopies => Set<BookCopy>();
        public DbSet<Member> Members => Set<Member>();
        public DbSet<BorrowRecord> BorrowRecords => Set<BorrowRecord>();
        public DbSet<LateFee> LateFees => Set<LateFee>();
        public DbSet<FeePayment> FeePayments => Set<FeePayment>();
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
        public DbSet<Publisher> Publishers => Set<Publisher>();
        public DbSet<LibraryCard> LibraryCards => Set<LibraryCard>();
        public DbSet<Reservation> Reservations => Set<Reservation>();
        public DbSet<InventoryLog> InventoryLogs => Set<InventoryLog>();
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ── Book ──────────────────────────────────────────────
            modelBuilder.Entity<Book>(e =>
            {
                e.HasIndex(b => b.ISBN).IsUnique();
                e.HasOne(b => b.Publisher)
                    .WithMany(p => p.Books)
                    .HasForeignKey(b => b.PublisherId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // ── BookCopy ─────────────────────────────────────────
            modelBuilder.Entity<BookCopy>(e =>
            {
                e.HasIndex(bc => bc.BookId);
                e.HasIndex(bc => bc.Status);
                e.HasOne(bc => bc.Book)
                    .WithMany(b => b.Copies)
                    .HasForeignKey(bc => bc.BookId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ── Author (M:N with Book) ───────────────────────────
            modelBuilder.Entity<Author>(e =>
            {
                e.HasIndex(a => a.LastName);
            });

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Authors)
                .WithMany(a => a.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookAuthors",
                    j => j.HasOne<Author>().WithMany().HasForeignKey("AuthorId").OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<Book>().WithMany().HasForeignKey("BookId").OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("BookId", "AuthorId");
                        j.ToTable("BookAuthors");
                    });

            // ── Category (M:N with Book) ─────────────────────────
            modelBuilder.Entity<Category>(e =>
            {
                e.HasIndex(c => c.Name).IsUnique();
            });

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Categories)
                .WithMany(c => c.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookCategories",
                    j => j.HasOne<Category>().WithMany().HasForeignKey("CategoryId").OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<Book>().WithMany().HasForeignKey("BookId").OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("BookId", "CategoryId");
                        j.ToTable("BookCategories");
                    });

            // ── Publisher ─────────────────────────────────────────
            modelBuilder.Entity<Publisher>(e =>
            {
                e.HasIndex(p => p.Name);
            });

            // ── Member ────────────────────────────────────────────
            modelBuilder.Entity<Member>(e =>
            {
                e.HasIndex(m => m.Email).IsUnique();
                e.HasIndex(m => m.Status);
            });

            // ── LibraryCard (1:1 with Member) ─────────────────────
            modelBuilder.Entity<LibraryCard>(e =>
            {
                e.HasIndex(lc => lc.MemberId).IsUnique();
                e.HasIndex(lc => lc.CardNumber).IsUnique();
                e.HasOne(lc => lc.Member)
                    .WithOne(m => m.LibraryCard)
                    .HasForeignKey<LibraryCard>(lc => lc.MemberId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ── ApplicationUser ───────────────────────────────────
            modelBuilder.Entity<ApplicationUser>(e =>
            {
                e.HasIndex(u => u.Username).IsUnique();
            });

            // ── BorrowRecord ──────────────────────────────────────
            modelBuilder.Entity<BorrowRecord>(e =>
            {
                e.HasIndex(br => br.BookCopyId);
                e.HasIndex(br => br.MemberId);
                e.HasIndex(br => br.Status);
                e.HasIndex(br => br.CheckedOutByUserId);
                e.HasIndex(br => br.ReturnedByUserId);

                e.HasOne(br => br.BookCopy)
                    .WithMany(bc => bc.BorrowRecords)
                    .HasForeignKey(br => br.BookCopyId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(br => br.Member)
                    .WithMany(m => m.BorrowRecords)
                    .HasForeignKey(br => br.MemberId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(br => br.CheckedOutByUser)
                    .WithMany(u => u.CheckedOutBorrows)
                    .HasForeignKey(br => br.CheckedOutByUserId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(br => br.ReturnedByUser)
                    .WithMany(u => u.ReturnedBorrows)
                    .HasForeignKey(br => br.ReturnedByUserId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // ── LateFee ───────────────────────────────────────────
            modelBuilder.Entity<LateFee>(e =>
            {
                e.HasIndex(lf => lf.BorrowRecordId);
                e.HasIndex(lf => lf.Status);
                e.HasIndex(lf => lf.WaivedByUserId);

                e.HasOne(lf => lf.BorrowRecord)
                    .WithMany(br => br.LateFees)
                    .HasForeignKey(lf => lf.BorrowRecordId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(lf => lf.WaivedByUser)
                    .WithMany(u => u.WaivedFees)
                    .HasForeignKey(lf => lf.WaivedByUserId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // ── FeePayment ────────────────────────────────────────
            modelBuilder.Entity<FeePayment>(e =>
            {
                e.HasIndex(fp => fp.LateFeeId);
                e.HasOne(fp => fp.LateFee)
                    .WithMany(lf => lf.Payments)
                    .HasForeignKey(fp => fp.LateFeeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ── Reservation ───────────────────────────────────────
            modelBuilder.Entity<Reservation>(e =>
            {
                e.HasIndex(r => r.BookId);
                e.HasIndex(r => r.MemberId);
                e.HasIndex(r => r.Status);

                e.HasOne(r => r.Book)
                    .WithMany(b => b.Reservations)
                    .HasForeignKey(r => r.BookId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(r => r.Member)
                    .WithMany(m => m.Reservations)
                    .HasForeignKey(r => r.MemberId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ── InventoryLog ──────────────────────────────────────
            modelBuilder.Entity<InventoryLog>(e =>
            {
                e.HasIndex(il => il.BookCopyId);
                e.HasIndex(il => il.PerformedByUserId);

                e.HasOne(il => il.BookCopy)
                    .WithMany(bc => bc.InventoryLogs)
                    .HasForeignKey(il => il.BookCopyId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(il => il.PerformedByUser)
                    .WithMany(u => u.PerformedInventoryLogs)
                    .HasForeignKey(il => il.PerformedByUserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ── AuditLog ──────────────────────────────────────────
            modelBuilder.Entity<AuditLog>(e =>
            {
                e.HasIndex(al => al.UserId);
                e.HasIndex(al => new { al.EntityName, al.EntityId });
                e.HasIndex(al => al.Timestamp);

                e.HasOne(al => al.User)
                    .WithMany(u => u.AuditLogs)
                    .HasForeignKey(al => al.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ── Seed ──────────────────────────────────────────────
            var seedDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            // ── ApplicationUsers ──────────────────────────────────
            // Passwords are BCrypt hashed. Seed passwords: admin, lib123, staff123
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                    {
        Id = 1,
        Username = "admin",
        PasswordHash = "$2b$11$lhf1zGV3kq/9pE7ZIrdQ8eC.DAA/NDLETCdkCeftgPDLGizL8fMO2", // Hash of "admin"
        Role = UserRole.Admin,
        CreatedAt = seedDate,
        UpdatedAt = seedDate
    },
    new ApplicationUser
    {
        Id = 2,
        Username = "librarian1",
        PasswordHash = "$2b$11$CHb9zP70T2g5ijeKUDXr3.FzZ5SOF0pp6WUnJ81qGeWsM4kEYmZsu", // Hash of "lib123"
        Role = UserRole.Librarian,
        CreatedAt = seedDate,
        UpdatedAt = seedDate
    },
    new ApplicationUser
    {
        Id = 3,
        Username = "staff1",
        PasswordHash = "$2b$11$Nh4YOgnZF5sD4.l//qmBpu5aH7sSP/1l/1JEv0A4fyWsgbBBuX5w.", // Hash of "staff123"
        Role = UserRole.Staff,
        CreatedAt = seedDate,
        UpdatedAt = seedDate
    });

            // ── Publishers ────────────────────────────────────────
            modelBuilder.Entity<Publisher>().HasData(
                new Publisher { Id = 1, Name = "O'Reilly Media", Address = "1005 Gravenstein Hwy, Sebastopol, CA", Phone = "707-827-7000", CreatedAt = seedDate, UpdatedAt = seedDate },
                new Publisher { Id = 2, Name = "Pearson Education", Address = "One Lake Street, Upper Saddle River, NJ", Phone = "201-236-7000", CreatedAt = seedDate, UpdatedAt = seedDate },
                new Publisher { Id = 3, Name = "Springer", Address = "Tiergartenstr. 17, 69121 Heidelberg, DE", Phone = "+49-6221-487-0", CreatedAt = seedDate, UpdatedAt = seedDate },
                new Publisher { Id = 4, Name = "NXB Tre", Address = "51 Nguyen Du, Ha Noi, VN", Phone = "024-3943-4630", CreatedAt = seedDate, UpdatedAt = seedDate });

            // ── Authors ───────────────────────────────────────────
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, FirstName = "Robert", LastName = "Martin", Bio = "Author of Clean Code", CreatedAt = seedDate, UpdatedAt = seedDate },
                new Author { Id = 2, FirstName = "Thomas", LastName = "Cormen", Bio = "Co-author of CLRS", CreatedAt = seedDate, UpdatedAt = seedDate },
                new Author { Id = 3, FirstName = "Erich", LastName = "Gamma", Bio = "Author of Design Patterns", CreatedAt = seedDate, UpdatedAt = seedDate },
                new Author { Id = 4, FirstName = "Nguyen", LastName = "Viet", Bio = "Vietnamese author", CreatedAt = seedDate, UpdatedAt = seedDate });

            // ── Categories ────────────────────────────────────────
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Programming", Description = "Software development and programming languages", CreatedAt = seedDate, UpdatedAt = seedDate },
                new Category { Id = 2, Name = "Algorithms", Description = "Data structures and algorithms", CreatedAt = seedDate, UpdatedAt = seedDate },
                new Category { Id = 3, Name = "Design Patterns", Description = "Software design patterns and architecture", CreatedAt = seedDate, UpdatedAt = seedDate },
                new Category { Id = 4, Name = "Fiction", Description = "Novels and short stories", CreatedAt = seedDate, UpdatedAt = seedDate },
                new Category { Id = 5, Name = "Science", Description = "Natural sciences", CreatedAt = seedDate, UpdatedAt = seedDate });

            // ── Books ─────────────────────────────────────────────
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Clean Code", ISBN = "978-0132350884", PublisherId = 1, PublicationYear = 2008, Description = "A Handbook of Agile Software Craftsmanship", ShelfLocation = "A-01-01", ReplacementCost = 45.99m, CreatedAt = seedDate, UpdatedAt = seedDate },
                new Book { Id = 2, Title = "Introduction to Algorithms", ISBN = "978-0262033848", PublisherId = 2, PublicationYear = 2009, Description = "CLRS 3rd Edition", ShelfLocation = "A-01-02", ReplacementCost = 89.99m, CreatedAt = seedDate, UpdatedAt = seedDate },
                new Book { Id = 3, Title = "Design Patterns", ISBN = "978-0201633610", PublisherId = 1, PublicationYear = 1994, Description = "Elements of Reusable Object-Oriented Software", ShelfLocation = "A-02-01", ReplacementCost = 59.99m, CreatedAt = seedDate, UpdatedAt = seedDate },
                new Book { Id = 4, Title = "SQL Fundamentals", ISBN = "978-0135164525", PublisherId = 2, PublicationYear = 2019, Description = "Database programming with SQL", ShelfLocation = "B-01-01", ReplacementCost = 39.99m, CreatedAt = seedDate, UpdatedAt = seedDate },
                new Book { Id = 5, Title = "Toi thay hoa vang tren co xanh", ISBN = "978-6041044333", PublisherId = 4, PublicationYear = 2010, Description = "Novel by Nguyen Nhat Anh", ShelfLocation = "C-01-01", ReplacementCost = 55000m, CreatedAt = seedDate, UpdatedAt = seedDate });

            // ── BookCopies ────────────────────────────────────────
            modelBuilder.Entity<BookCopy>().HasData(
                new BookCopy { Id = 1, BookId = 1, Status = CopyStatus.Available, CreatedAt = seedDate, UpdatedAt = seedDate },
                new BookCopy { Id = 2, BookId = 1, Status = CopyStatus.Available, CreatedAt = seedDate, UpdatedAt = seedDate },
                new BookCopy { Id = 3, BookId = 2, Status = CopyStatus.Available, CreatedAt = seedDate, UpdatedAt = seedDate },
                new BookCopy { Id = 4, BookId = 3, Status = CopyStatus.Available, CreatedAt = seedDate, UpdatedAt = seedDate },
                new BookCopy { Id = 5, BookId = 4, Status = CopyStatus.Available, CreatedAt = seedDate, UpdatedAt = seedDate },
                new BookCopy { Id = 6, BookId = 5, Status = CopyStatus.Available, CreatedAt = seedDate, UpdatedAt = seedDate });

            // ── Members ───────────────────────────────────────────
            modelBuilder.Entity<Member>().HasData(
                new Member { Id = 1, FirstName = "Nguyen Van", LastName = "A", Email = "nguyenvana@example.com", Phone = "0901234567", Status = MemberStatus.Active, MemberType = MemberType.Student, Department = "Computer Science", StudentId = "SV001", CreatedAt = seedDate, UpdatedAt = seedDate },
                new Member { Id = 2, FirstName = "Tran Thi", LastName = "B", Email = "tranthib@example.com", Phone = "0912345678", Status = MemberStatus.Active, MemberType = MemberType.Student, Department = "Computer Science", StudentId = "SV002", CreatedAt = seedDate, UpdatedAt = seedDate },
                new Member { Id = 3, FirstName = "Le Van", LastName = "C", Email = "levanc@example.com", Phone = "0923456789", Status = MemberStatus.Active, MemberType = MemberType.Teacher, Department = "Mathematics", StudentId = "GV001", CreatedAt = seedDate, UpdatedAt = seedDate },
                new Member { Id = 4, FirstName = "Pham Minh", LastName = "D", Email = "phamminhd@example.com", Phone = "0934567890", Status = MemberStatus.Active, MemberType = MemberType.External, Department = null, StudentId = null, CreatedAt = seedDate, UpdatedAt = seedDate });

            // ── LibraryCards ───────────────────────────────────────
            modelBuilder.Entity<LibraryCard>().HasData(
                new LibraryCard { Id = 1, MemberId = 1, CardNumber = "LIB-2025-0001", ExpiryDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Status = CardStatus.Active, CreatedAt = seedDate, UpdatedAt = seedDate },
                new LibraryCard { Id = 2, MemberId = 2, CardNumber = "LIB-2025-0002", ExpiryDate = new DateTime(2026, 12, 31, 0, 0, 0, DateTimeKind.Utc), Status = CardStatus.Active, CreatedAt = seedDate, UpdatedAt = seedDate },
                new LibraryCard { Id = 3, MemberId = 3, CardNumber = "LIB-2025-0003", ExpiryDate = new DateTime(2027, 12, 31, 0, 0, 0, DateTimeKind.Utc), Status = CardStatus.Active, CreatedAt = seedDate, UpdatedAt = seedDate },
                new LibraryCard { Id = 4, MemberId = 4, CardNumber = "LIB-2025-0004", ExpiryDate = new DateTime(2026, 6, 30, 0, 0, 0, DateTimeKind.Utc), Status = CardStatus.Active, CreatedAt = seedDate, UpdatedAt = seedDate });

            // ── BookAuthors (M:N) ────────────────────────────────
            modelBuilder.Entity("BookAuthors").HasData(
                new { BookId = 1, AuthorId = 1 },  // Clean Code — Robert Martin
                new { BookId = 2, AuthorId = 2 },  // CLRS — Thomas Cormen
                new { BookId = 3, AuthorId = 3 },  // Design Patterns — Erich Gamma
                new { BookId = 5, AuthorId = 4 }); // Toi thay hoa vang — Nguyen Viet

            // ── BookCategories (M:N) ──────────────────────────────
            modelBuilder.Entity("BookCategories").HasData(
                new { BookId = 1, CategoryId = 1 },  // Clean Code — Programming
                new { BookId = 2, CategoryId = 2 },  // CLRS — Algorithms
                new { BookId = 3, CategoryId = 3 },  // Design Patterns — Design Patterns
                new { BookId = 3, CategoryId = 1 },  // Design Patterns — Programming
                new { BookId = 4, CategoryId = 1 },  // SQL Fundamentals — Programming
                new { BookId = 5, CategoryId = 4 }); // Toi thay hoa vang — Fiction
        }
    }
}
