# Library Management System

Ứng dụng desktop quản lý thư viện — .NET 10 WinForms + EF Core SQLite.

## Chức năng

- **Quản lý Sách**: CRUD sách, tìm kiếm theo tên/ISBN/tác giả/thể loại, lọc theo trạng thái
- **Quản lý Bản sao**: Barcode tự động, gán kệ sách, đổi trạng thái
- **Quản lý Độc giả**: CRUD, phân loại SV/GV/NV/Ngoài trường, import CSV
- **Quản lý Thẻ thư viện**: Cấp thẻ, gia hạn, khóa/mở
- **Mượn/Trả sách**: Kiểm tra điều kiện, tính ngày hẹn trả, trả nhiều sách
- **Gia hạn**: Max 2 lần, chặn nếu có reservation hoặc nợ phạt
- **Đặt trước sách**: Hàng chờ FIFO, thông báo khi sách có lại
- **Phạt & Thanh toán**: Phạt trễ/mất/hỏng, thanh toán, miễn phạt
- **Kiểm kê kho**: Nhập sách, thanh lý, chuyển kệ, báo mất/hỏng
- **Báo cáo**: Sách mượn nhiều, quá hạn, tổng phạt, thống kê theo thời gian
- **Auth**: Đăng nhập, phân quyền Admin/Librarian/Staff, audit log

## Cấu trúc thư mục

```
WinFormsApp1/
├── Models/              # Entity classes
│   └── Enums/           # CopyStatus, BorrowStatus, MemberStatus, FeeStatus, UserRole
├── Data/                # AppDbContext, Repository, UnitOfWork, schema.sql, diagrams
├── Services/            # Business logic services
├── Helpers/             # Password hashing, session management
├── Forms/               # WinForms UI
└── Program.cs           # Entry point + DI setup
```

## Yêu cầu

- .NET 10 SDK
- SQLite (tự động tạo database khi chạy lần đầu)

## Cài đặt

```bash
# Clone
git clone https://github.com/thaiannguyen-05/lib-management.git
cd lib-management/WinFormsApp1

# Chạy
dotnet run
```

## Database Schema

18 tables | 5 enums | 16 relationships

```mermaid
%%{init: {'theme': 'base', 'themeVariables': { 'primaryColor': '#e3f2fd', 'primaryTextColor': '#333', 'primaryBorderColor': '#90caf9', 'lineColor': '#90caf9', 'secondaryColor': '#fff', 'tertiaryColor': '#f5f5f5', 'fontSize': '14px' }}}%%
erDiagram
    Authors {
        int Id PK
        text FirstName
        text LastName
        text Bio
    }

    Categories {
        int Id PK
        text Name
        text Description
    }

    Publishers {
        int Id PK
        text Name
        text Address
        text Phone
    }

    Books {
        int Id PK
        text Title
        text ISBN
        int PublisherId FK
        int PublicationYear
        text Description
        text ShelfLocation
        real ReplacementCost
    }

    BookCopies {
        int Id PK
        int BookId FK
        int Status
    }

    BookAuthors {
        int BookId FK
        int AuthorId FK
    }

    BookCategories {
        int BookId FK
        int CategoryId FK
    }

    Members {
        int Id PK
        text FirstName
        text LastName
        text Email
        text Phone
        int Status
        int MemberType
        text Department
    }

    LibraryCards {
        int Id PK
        int MemberId FK
        text CardNumber
        text ExpiryDate
        int Status
    }

    ApplicationUsers {
        int Id PK
        text Username
        text PasswordHash
        int Role
    }

    BorrowRecords {
        int Id PK
        int BookCopyId FK
        int MemberId FK
        text BorrowDate
        text DueDate
        text ReturnDate
        int Status
        int RenewalCount
        int CheckedOutByUserId FK
        int ReturnedByUserId FK
    }

    LateFees {
        int Id PK
        int BorrowRecordId FK
        int FeeType
        real Amount
        text DateIncurred
        int Status
        int WaivedByUserId FK
    }

    FeePayments {
        int Id PK
        int LateFeeId FK
        real Amount
        text PaymentDate
    }

    Reservations {
        int Id PK
        int BookId FK
        int MemberId FK
        text ReservationDate
        text ExpiryDate
        int Status
    }

    InventoryLogs {
        int Id PK
        int BookCopyId FK
        int Action
        int Quantity
        text Note
        int PerformedByUserId FK
    }

    AuditLogs {
        int Id PK
        int UserId FK
        text Action
        text EntityName
        int EntityId
        text Details
        text Timestamp
    }

    Publishers ||--o{ Books : "publishes"
    Books ||--o{ BookCopies : "has"
    Books ||--o{ BookAuthors : "written by"
    Authors ||--o{ BookAuthors : "authored"
    Books ||--o{ BookCategories : "categorized in"
    Categories ||--o{ BookCategories : "contains"
    Members ||--o{ LibraryCards : "has"
    Members ||--o{ BorrowRecords : "borrows"
    Members ||--o{ LateFees : "incurs"
    Members ||--o{ Reservations : "reserves"
    Books ||--o{ Reservations : "reserved"
    BookCopies ||--o{ BorrowRecords : "checked out"
    BookCopies ||--o{ InventoryLogs : "logged"
    BorrowRecords ||--o{ LateFees : "generates"
    LateFees ||--o{ FeePayments : "paid via"
    ApplicationUsers ||--o{ BorrowRecords : "CheckedOutBy"
    ApplicationUsers ||--o{ BorrowRecords : "ReturnedBy"
    ApplicationUsers ||--o{ LateFees : "WaivedBy"
    ApplicationUsers ||--o{ InventoryLogs : "performed"
    ApplicationUsers ||--o{ AuditLogs : "actions"
```

## Architecture

- **UI**: WinForms (.NET 10)
- **ORM**: Entity Framework Core 10 (SQLite)
- **Pattern**: Repository + Service layers
- **Auth**: Username/password with salted hash
- **Concurrency**: Single machine, single user

## Issues

[Xem danh sách issue](https://github.com/thaiannguyen-05/lib-management/issues)

## License

MIT
