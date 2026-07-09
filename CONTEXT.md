# Library Management System

Desktop application (.NET 8 WinForms + EF Core) for managing library operations — book catalog, members, borrowing/returning, and reporting. Single-machine, single-user at a time.

## Language

**Book**:
A title/edition in the catalog. Holds metadata (title, ISBN, publisher, year, description, shelf location, replacement cost). Does NOT represent a physical copy.
_Avoid_: Copy, Item

**BookCopy**:
A physical copy of a Book. Has its own ID and status (Available, Borrowed, Damaged, Lost). A Book has many BookCopies.
_Avoid_: Copy, Instance

**Member**:
A person registered with the library. Has contact info, membership status, and a maximum borrow limit determined by their MembershipTier.
_Avoid_: User, Patron, Account

**MembershipTier**:
A named tier (e.g. "Basic", "Premium") that determines a Member's borrow limit. Stored in DB, admin-configurable, seeded with defaults.
_Avoid_: Role, Level

**BorrowRecord**:
A loan of a specific BookCopy to a specific Member. Tracks borrow date, due date, return date, status, renewal count, and audit trail (who checked out, who returned).
_Avoid_: Loan, Checkout

**LateFee**:
A fee incurred by a BorrowRecord when the book is returned after the due date or marked lost. Has amount, date incurred, status (Unpaid/Paid/Waived), and audit trail (who waived).
_Avoid_: Fine, Charge

**FeePayment**:
A payment record against a LateFee. Tracks amount paid and date.
_Avoid_: Transaction, Receipt

**Author**:
A person who wrote a book. Separate entity with full CRUD. Books and Authors have a many-to-many relationship.
_Avoid_: Writer

**Category**:
A subject classification for books (e.g. "Fiction", "Science"). Separate entity with full CRUD. Books and Categories have a many-to-many relationship.
_Avoid_: Genre, Tag

**ApplicationUser**:
A user who can log into the app. Has username, password hash, and a single role (Admin/Librarian/Staff). Not the same as Member. Referenced by BorrowRecord and LateFee for audit trail.
_Avoid_: User, Account, Staff (use Member or ApplicationUser)

## Enums

**CopyStatus**:
Status of a physical BookCopy: Available, Borrowed, Damaged, Lost.
_Avoid_: BookStatus

**BorrowStatus**:
Status of a loan: Active, Returned, Overdue, Lost.
_Avoid_: LoanStatus, CheckoutStatus

**MemberStatus**:
Membership state: Active, Suspended, Expired.
_Avoid_: AccountStatus

**FeeStatus**:
Payment state of a LateFee: Unpaid, Paid, Waived.
_Avoid_: PaymentStatus

**UserRole**:
Permission level for ApplicationUsers: Admin, Librarian, Staff.
_Avoid_: Role (use UserRole), Permission

## Business Rules

- Borrow a BookCopy (not a Book title)
- Max 2 renewals per borrow, always allowed (no holds system)
- Late fee = daily rate x days overdue, calculated on return
- Lost book = mark copy Lost + charge member replacement cost
- Unpaid fees block new borrows
- Membership tier determines max active borrows per member
- Single role per ApplicationUser
- Authors and Categories are separate entities with full CRUD
- BorrowRecord tracks which ApplicationUser checked out and returned
- LateFee tracks which ApplicationUser waived the fee

## Architecture

- **UI**: WinForms (.NET 8)
- **ORM**: Entity Framework Core 8 (SQLite)
- **Pattern**: Repository + Service layers
- **Auth**: Simple username/password with salted hash
- **Concurrency**: Single machine, one user at a time
