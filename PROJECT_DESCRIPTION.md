# Library Management System

## Project Description

A desktop application for managing library operations including book inventory, member registrations, borrowing/returning workflows, and reporting. Built with .NET 8 WinForms + EF Core as a study/learning project to demonstrate modern .NET desktop development practices.

---

## Table of Contents

1. [Project Overview](#1-project-overview)
2. [Core Features](#2-core-features)
3. [Technical Stack](#3-technical-stack)
4. [System Architecture](#4-system-architecture)
5. [Data Model](#5-data-model)
6. [User Roles](#6-user-roles)
7. [Development Phases](#7-development-phases)
8. [Future Enhancements](#8-future-enhancements)

---

## 1. Project Overview

### Purpose

This application provides a complete library management solution for small to medium-sized libraries. It replaces manual paper-based tracking with a digital system that manages the full lifecycle of books, members, and borrowing activities.

### Target Audience

- **Small libraries** (school libraries, community libraries, private collections)
- **Library staff** who need to track inventory and member activity
- **Students/developers** learning .NET Desktop development

### Goals

- Streamline book cataloging and inventory tracking
- Simplify member registration and management
- Automate borrowing, returning, and overdue tracking
- Provide actionable insights through reports and dashboards
- Serve as a comprehensive .NET WinForms + EF Core study project

---

## 2. Core Features

### 2.1 Book Management

| Feature | Description |
|---------|-------------|
| **Add Books** | Enter book details including title, ISBN, publisher, category, description, shelf location, and replacement cost |
| **Edit Books** | Update existing book information |
| **Delete Books** | Remove books from the catalog (with safeguards for borrowed copies) |
| **Copy Tracking** | Each physical copy is tracked individually with its own status (Available, Borrowed, Damaged, Lost) |
| **Author Management** | Full CRUD for authors. Many-to-many relationship with books |
| **Category Management** | Full CRUD for categories. Many-to-many relationship with books |
| **Search Books** | Search by title, author, ISBN, or category with real-time filtering |

### 2.2 Member Management

| Feature | Description |
|---------|-------------|
| **Register Members** | Create member profiles with contact information |
| **Edit Profiles** | Update member details and membership status |
| **Membership Tiers** | Admin-configurable tiers (e.g. "Basic", "Premium") with varying borrow limits |
| **Member History** | View complete borrowing history for any member |
| **Search Members** | Find members by name, ID, email, or phone |

### 2.3 Borrowing & Returning

| Feature | Description |
|---------|-------------|
| **Check Out** | Borrow a specific BookCopy to a Member, set due date automatically |
| **Check In** | Process book returns, calculate late fees if applicable |
| **Renewals** | Extend due dates for active borrows (max 2 per borrow, always allowed) |
| **Overdue Alerts** | Flag overdue borrows and display on dashboard |
| **Late Fee Calculation** | Auto-calculate fees based on configurable daily rate |
| **Fee Tracking** | Full payment tracking — fees can be Paid, Unpaid, or Waived |
| **Borrow Limits** | Enforce maximum active borrows per membership tier |
| **Lost Books** | Mark copy as Lost + charge member replacement cost |

### 2.4 Search & Discovery

| Feature | Description |
|---------|-------------|
| **Book Search** | Search by title, author, ISBN, or category with real-time filtering |
| **Advanced Filters** | Filter by copy availability, publication year, or date added |
| **Member Search** | Quick lookup by name or member ID |
| **Sort Columns** | Sort any data grid column ascending/descending |

### 2.5 Reports & Dashboard

| Feature | Description |
|---------|-------------|
| **Dashboard** | Overview of key metrics: total books, active borrows, overdue items, total members |
| **Overdue Report** | List all currently overdue borrows with member contact details |
| **Popular Books** | Most borrowed books over a configurable time period |
| **Member Activity** | Members with most/least borrowing activity |
| **Export** | Export reports to CSV |

---

## 3. Technical Stack

### Platform & Framework

| Component | Choice | Rationale |
|-----------|--------|-----------|
| **UI Framework** | WinForms (.NET 8) | Simple, familiar, rapid development for data-driven desktop apps |
| **Language** | C# 12+ / .NET 8+ | Latest LTS version with modern language features |
| **ORM** | Entity Framework Core 8 | Industry standard, LINQ queries, code-first migrations, change tracking |
| **Database** | SQLite | Zero-config, file-based, no server needed, ideal for desktop apps |

### Project Structure

```
LibraryManagementSystem/
│
├── Models/                         # Domain Entities
│   ├── Book.cs
│   ├── BookCopy.cs
│   ├── Member.cs
│   ├── MembershipTier.cs
│   ├── BorrowRecord.cs
│   ├── LateFee.cs
│   ├── FeePayment.cs
│   ├── Author.cs
│   ├── Category.cs
│   ├── BookAuthor.cs               # M:N junction
│   ├── BookCategory.cs             # M:N junction
│   └── ApplicationUser.cs
│
├── Data/                           # EF Core DbContext & Configurations
│   ├── LibraryDbContext.cs
│   ├── Configurations/             # EF Fluent API Configurations
│   ├── Migrations/                 # EF Core Migrations
│   └── SeedData.cs
│
├── Repositories/                   # Data Access Layer
│   ├── IRepository.cs              # Generic repository interface
│   ├── Repository.cs               # Generic repository implementation
│   ├── BookRepository.cs
│   ├── MemberRepository.cs
│   ├── BorrowRepository.cs
│   └── ...
│
├── Services/                       # Business Logic Layer
│   ├── IBookService.cs
│   ├── BookService.cs
│   ├── IMemberService.cs
│   ├── MemberService.cs
│   ├── IBorrowService.cs
│   ├── BorrowService.cs
│   ├── IReportService.cs
│   └── ReportService.cs
│
├── Forms/                          # WinForms UI
│   ├── MainForm.cs                 # Main navigation window
│   ├── Books/
│   │   ├── BookListForm.cs
│   │   └── BookEditForm.cs
│   ├── Members/
│   │   ├── MemberListForm.cs
│   │   └── MemberEditForm.cs
│   ├── Borrowing/
│   │   ├── CheckoutForm.cs
│   │   ├── ReturnForm.cs
│   │   └── BorrowHistoryForm.cs
│   ├── Reports/
│   │   └── DashboardForm.cs
│   └── Auth/
│       └── LoginForm.cs
│
├── Utilities/                      # Helpers & Extensions
├── Program.cs                      # Entry point
└── LibraryManagementSystem.csproj
```

### NuGet Packages

```xml
<PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="8.0.*" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.*" />
```

---

## 4. System Architecture

### Layered Architecture

```
┌─────────────────────────────────────────────────────┐
│               PRESENTATION LAYER                     │
│                                                     │
│  WinForms (Forms + Controls)                        │
│  - Data binding via DataSource/DataGridView         │
│  - Event handlers call Service interfaces           │
└─────────────────────────┬───────────────────────────┘
                          │ (Service Interfaces)
┌─────────────────────────▼───────────────────────────┐
│              BUSINESS LOGIC LAYER                    │
│                                                     │
│  Services (BookService, MemberService, etc.)        │
│  - Business rules validation                        │
│  - Borrowing/return logic                           │
│  - Late fee calculations                            │
│  - Report generation                                │
└─────────────────────────┬───────────────────────────┘
                          │ (Repository Pattern)
┌─────────────────────────▼───────────────────────────┐
│               DATA ACCESS LAYER                      │
│                                                     │
│  EF Core DbContext + Repositories                   │
│  - CRUD operations                                  │
│  - LINQ queries                                     │
│  - Migrations management                            │
└─────────────────────────┬───────────────────────────┘
                          │
┌─────────────────────────▼───────────────────────────┐
│                  DATABASE LAYER                      │
│                                                     │
│  SQLite (library.db)                                │
│  - Single file, no server required                  │
└─────────────────────────────────────────────────────┘
```

### Key Architectural Decisions

1. **Repository Pattern**: Generic `IRepository<T>` for common CRUD, specialized repositories for complex queries.

2. **Service Layer Isolation**: Services depend on repository interfaces, not implementations. Enables unit testing with mocks.

3. **Async/Await Throughout**: All database operations are async to keep the UI responsive.

4. **Code-First Migrations**: EF Core migrations manage schema. No manual SQL scripts.

---

## 5. Data Model

### Entity Relationship Diagram

```
┌──────────┐       ┌──────────────┐       ┌──────────┐
│  Author  │◄──────┤ BookAuthor   │───────►│  Book    │
│──────────│  M:N  │──────────────│  M:N  │──────────│
│ Id       │       │ BookId       │       │ Id       │
│ Name     │       │ AuthorId     │       │ Title    │
│ Bio      │       └──────────────┘       │ ISBN     │
└──────────┘                              │ Publisher│
                                          │ Year     │
                                          │ Desc     │
┌──────────┐       ┌──────────────┐       │ Location │
│ Category │───────│ BookCategory │       │ ReplCost │
│──────────│  M:N  │──────────────│       └────┬─────┘
│ Id       │       │ BookId       │            │
│ Name     │       │ CategoryId   │            │ 1
│ Desc     │       └──────────────┘            │
└──────────┘                                  │ N
                                          ┌────▼─────┐
                                          │ BookCopy │
                                          │──────────│
                                          │ Id       │
                                          │ BookId   │
                                          │ Status   │
                                          └────┬─────┘
                                               │
                                               │ 1
                                               │
                                               │ N
┌──────────────┐                        ┌──────▼──────┐
│ MembershipTier│                       │BorrowRecord │
│──────────────│                        │─────────────│
│ Id           │                        │ Id          │
│ Name         │                        │ BookCopyId  │
│ MaxBorrow    │◄───────────────────────│ MemberId    │
└──────────────┘        1:N             │ BorrowDate  │
      │                                 │ DueDate     │
      │ 1                               │ ReturnDate  │
      │                                 │ Status      │
      │ N                               │ RenewalCount│
┌─────▼──────┐                          └──────┬──────┘
│   Member   │                                 │
│────────────│                                 │ 1
│ Id         │                                 │
│ FirstName  │                                 │ N
│ LastName   │                          ┌──────▼──────┐
│ Email      │                          │  LateFee    │
│ Phone      │                          │─────────────│
│ Status     │                          │ Id          │
│ TierId     │                          │ BorrowRecId │
└────────────┘                          │ Amount      │
                                        │ Status      │
                                        └──────┬──────┘
                                               │ 1
                                               │
                                               │ N
                                        ┌──────▼──────┐
                                        │ FeePayment  │
                                        │─────────────│
                                        │ Id          │
                                        │ LateFeeId   │
                                        │ Amount      │
                                        │ PaymentDate │
                                        └─────────────┘

┌──────────────────┐
│  ApplicationUser │
│──────────────────│
│ Id               │
│ Username         │
│ PasswordHash     │
│ Role             │
│ IsActive         │
└──────────────────┘
```

### Entity Definitions

#### Book

```csharp
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public string? Publisher { get; set; }
    public int? PublishYear { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }        // Shelf location
    public decimal ReplacementCost { get; set; } // Cost if lost
    public DateTime DateAdded { get; set; }
    public DateTime? DateModified { get; set; }

    // Navigation
    public ICollection<BookCopy> BookCopies { get; set; } = new List<BookCopy>();
    public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();

    // Computed
    public int CopiesAvailable => BookCopies.Count(c => c.Status == CopyStatus.Available);
    public int CopiesTotal => BookCopies.Count;
}
```

#### BookCopy

```csharp
public class BookCopy
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public CopyStatus Status { get; set; } = CopyStatus.Available;

    // Navigation
    public Book Book { get; set; } = null!;
    public ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
}
```

#### Member

```csharp
public class Member
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public DateTime MembershipDate { get; set; }
    public MemberStatus Status { get; set; } = MemberStatus.Active;
    public int MembershipTierId { get; set; }

    // Navigation
    public MembershipTier MembershipTier { get; set; } = null!;
    public ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();

    // Computed
    public string FullName => $"{FirstName} {LastName}";
    public int ActiveBorrowCount => BorrowRecords.Count(b => b.Status == BorrowStatus.Active);
}
```

#### MembershipTier

```csharp
public class MembershipTier
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;   // "Basic", "Premium"
    public int MaxBorrowLimit { get; set; } = 3;

    // Navigation
    public ICollection<Member> Members { get; set; } = new List<Member>();
}
```

#### BorrowRecord

```csharp
public class BorrowRecord
{
    public int Id { get; set; }
    public int BookCopyId { get; set; }
    public int MemberId { get; set; }
    public DateTime BorrowDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public BorrowStatus Status { get; set; } = BorrowStatus.Active;
    public int RenewalCount { get; set; } = 0;

    // Navigation
    public BookCopy BookCopy { get; set; } = null!;
    public Member Member { get; set; } = null!;
    public ICollection<LateFee> LateFees { get; set; } = new List<LateFee>();
}
```

#### LateFee

```csharp
public class LateFee
{
    public int Id { get; set; }
    public int BorrowRecordId { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateIncurred { get; set; }
    public FeeStatus Status { get; set; } = FeeStatus.Unpaid;

    // Navigation
    public BorrowRecord BorrowRecord { get; set; } = null!;
    public ICollection<FeePayment> Payments { get; set; } = new List<FeePayment>();

    // Computed
    public decimal AmountPaid => Payments.Sum(p => p.Amount);
    public decimal AmountOwed => Amount - AmountPaid;
}
```

#### FeePayment

```csharp
public class FeePayment
{
    public int Id { get; set; }
    public int LateFeeId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }

    // Navigation
    public LateFee LateFee { get; set; } = null!;
}
```

#### Author

```csharp
public class Author
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Biography { get; set; }

    // Navigation
    public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
}
```

#### Category

```csharp
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    // Navigation
    public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
}
```

#### ApplicationUser

```csharp
public class ApplicationUser
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
    public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
    public UserRole Role { get; set; } = UserRole.Staff;
    public bool IsActive { get; set; } = true;
}
```

### Enums

```csharp
public enum CopyStatus
{
    Available,
    Borrowed,
    Damaged,
    Lost
}

public enum BorrowStatus
{
    Active,
    Returned,
    Overdue,
    Lost
}

public enum MemberStatus
{
    Active,
    Suspended,
    Expired
}

public enum FeeStatus
{
    Unpaid,
    Paid,
    Waived
}

public enum UserRole
{
    Admin,
    Librarian,
    Staff
}
```

---

## 6. User Roles

### Role Permissions Matrix

| Feature | Admin | Librarian | Staff |
|---------|:-----:|:---------:|:-----:|
| View Books | Yes | Yes | Yes |
| Add/Edit/Delete Books | Yes | Yes | No |
| View Members | Yes | Yes | Yes |
| Add/Edit/Delete Members | Yes | Yes | No |
| Process Check-Out | Yes | Yes | Yes |
| Process Check-In | Yes | Yes | Yes |
| Renew Borrows | Yes | Yes | Yes |
| Waive Late Fees | Yes | Yes | No |
| View Reports | Yes | Yes | Yes |
| Export Reports | Yes | Yes | No |
| Manage User Accounts | Yes | No | No |
| Configure Settings | Yes | No | No |
| Delete Records | Yes | No | No |

### Default Accounts

- **Admin**: Created on first run (seed data)
- **Librarian**: Created by Admin
- **Staff**: Created by Admin

### Authentication

- Simple username/password stored in SQLite with salted hash (BCrypt or similar)
- No external identity provider needed for a desktop study project
- Session persists until app is closed

---

## 7. Development Phases

### Phase 1: Foundation Setup

**Duration**: ~1 week

**Tasks**:
- Create .NET 8 WinForms project
- Configure EF Core with SQLite, set up DbContext
- Define all entity classes and relationships
- Create initial EF migration
- Set up seed data (sample books, members, admin user, default tiers)

**Deliverable**: Running app with empty main form, database created, seed data populated.

### Phase 2: Book Catalog Module

**Duration**: ~1-2 weeks

**Tasks**:
- Book list view with DataGridView
- Add/Edit Book form with validation
- Delete Book with confirmation (block if copies are checked out)
- BookCopy management (add/remove copies, view status)
- Author CRUD (separate form)
- Category CRUD (separate form)
- Search bar with real-time filtering

**Deliverable**: Full CRUD for books, authors, categories. Copy-level tracking.

### Phase 3: Member Management Module

**Duration**: ~1 week

**Tasks**:
- Member list view with DataGridView
- Add/Edit Member form
- MembershipTier management (admin config)
- Member status management (active/suspended/expired)
- Member detail view showing borrowing history
- Search members by name, email, or phone

**Deliverable**: Full CRUD for members with tier-based borrow limits.

### Phase 4: Borrowing & Returning System

**Duration**: ~2 weeks

**Tasks**:
- Check-out workflow: select member, select book copy, confirm loan
- Check-in workflow: process return, calculate late fee
- Automatic due date calculation (configurable loan period)
- Overdue detection and status updates
- Late fee calculation (configurable daily rate)
- Renewal functionality (max 2 per borrow)
- Fee payment tracking
- Lost book workflow (mark copy Lost + charge replacement cost)
- Enforce borrow limits per membership tier
- Block borrowing for suspended members or members with unpaid fees

**Deliverable**: Complete borrow/return/renew cycle with business rules.

### Phase 5: Reports & Dashboard

**Duration**: ~1-2 weeks

**Tasks**:
- Dashboard with metric cards (total books, active borrows, overdue, members)
- Overdue books report (list with member contact info)
- Popular books report (most borrowed)
- Member activity report
- Export to CSV functionality
- Date range filters for reports

**Deliverable**: Informative dashboard and exportable reports.

### Phase 6: Auth & Polish

**Duration**: ~1 week

**Tasks**:
- Login form with username/password
- Password hashing (BCrypt)
- Role-based access control on forms
- Input validation (required fields, format checks)
- Error handling with user-friendly messages
- Confirmation dialogs for destructive actions
- UI consistency review

**Deliverable**: Authenticated, polished application ready for use.

---

## 8. Future Enhancements

These features are out of scope for the initial implementation but represent logical extensions:

### Near-Term Enhancements

| Feature | Description |
|---------|-------------|
| **Barcode/QR Scanning** | Use USB barcode scanner for fast book/member lookup |
| **Print Receipts** | Generate and print borrowing/return receipts |
| **Email Notifications** | Send overdue reminders and return confirmations |
| **Book Reservations** | Allow members to reserve unavailable books |
| **Backup & Restore** | Export/import database for backup purposes |
| **Audit Logging** | Track who did what and when |

### Advanced Enhancements

| Feature | Description |
|---------|-------------|
| **REST API Backend** | Migrate to client-server with shared database |
| **Web Admin Portal** | ASP.NET Core web interface for remote management |
| **OPAC (Public Catalog)** | Self-service search kiosk for library visitors |
| **Multi-Branch Support** | Support multiple library locations with transfers |
| **Reporting Engine** | Advanced reporting with custom queries and charts |
| **Role-Based UI** | Dynamically hide/show UI elements based on user role |
| **ISBN Lookup API** | Auto-fill book details by querying external ISBN database |

---

## Technology Summary

| Layer | Technology |
|-------|------------|
| **UI Framework** | WinForms with .NET 8 |
| **Database** | SQLite |
| **ORM** | Entity Framework Core 8 |
| **Auth** | BCrypt password hashing |
| **Build** | dotnet CLI / Visual Studio |

---

## Getting Started

```bash
# Clone the repository
git clone <repository-url>
cd lib-managerment

# Restore dependencies
dotnet restore

# Apply database migrations
dotnet ef database update --project src/LibraryManagementSystem

# Run the application
dotnet run --project src/LibraryManagementSystem
```

---

*This document serves as the living specification for the Library Management System. It should be updated as the project evolves.*
