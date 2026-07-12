# Library Management System

Desktop application (.NET 10 WinForms + EF Core) for managing library operations — book catalog, members, borrowing/returning, and reporting. Single-machine, single-user at a time.

## Language

**Book**:
A title/edition in the catalog. Holds metadata (title, ISBN, publisher, year, description, shelf location, replacement cost). Does NOT represent a physical copy.
_Avoid_: Copy, Item

**BookCopy**:
A physical copy of a Book. Has its own ID and status (Available, Borrowed, Damaged, Lost). A Book has many BookCopies.
_Avoid_: Copy, Instance

**Member**:
A person registered with the library. Has contact info and membership status. No borrow limit — can borrow as many books as available.
_Avoid_: User, Patron, Account

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

**Publisher**:
A publishing house. Separate entity with full CRUD. A Book optionally belongs to one Publisher (nullable FK). A Publisher has many Books.
_Avoid_: Press, PublishingHouse

**Department**:
An organizational unit (e.g. department in a university). Has a name and unique code. A Department has many StudentClasses and many Members.
_Avoid_: Division, Unit

**StudentClass**:
A class/cohort within a Department. Has a name and belongs to one Department. A StudentClass has many Members.
_Avoid_: Class, Group, Cohort

**LibraryCard**:
A physical or digital card issued to a Member. One-to-one with Member. Tracks card number (unique), expiry date, and status.
_Avoid_: Card, MembershipCard

**Reservation**:
A hold placed by a Member on a Book title (not a specific copy). When a copy of the reserved Book becomes available, it can be fulfilled. Has reservation date, expiry date, and status.
_Avoid_: Hold, Request

**InventoryLog**:
A record of an inventory action performed on a specific BookCopy. Tracks the action type, quantity, optional note, and which ApplicationUser performed it.
_Avoid_: StockLog, InventoryEntry

**AuditLog**:
A system-wide log of entity mutations. Records which ApplicationUser performed what action on which entity, with optional details and timestamp. Separate from domain-specific audit trails on BorrowRecord/LateFee.
_Avoid_: ActivityLog, ChangeLog

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

**MemberType**:
Classification of a Member by their role in the institution: Student, Teacher, Staff, External.
_Avoid_: UserType, Role (use MemberType)

**CardStatus**:
Lifecycle state of a LibraryCard: Active, Expired, Locked.
_Avoid_: LibraryCardStatus

**FeeType**:
Classification of what kind of LateFee was incurred: Late, Lost, Damaged. Orthogonal to FeeStatus (payment state).
_Avoid_: ChargeType

**ReservationStatus**:
Lifecycle state of a Reservation: Pending, Ready, Cancelled, Expired.
_Avoid_: HoldStatus

**InventoryAction**:
Type of inventory action on a BookCopy: Import, Dispose, Transfer, Count, Lost, Damaged.
_Avoid_: StockAction

## Business Rules

- Borrow a BookCopy (not a Book title)
- No borrow limit — members can borrow as many books as they want
- Max 2 renewals per borrow, always allowed (no holds system)
- Late fee = daily rate x days overdue, calculated on return
- Lost book = mark copy Lost + charge member replacement cost
- Unpaid fees block new borrows
- Single role per ApplicationUser
- Authors and Categories are separate entities with full CRUD
- BorrowRecord tracks which ApplicationUser checked out and returned
- LateFee tracks which ApplicationUser waived the fee
- Members can reserve Book titles (not specific copies). Reserved books go to the first pending reservation when returned.
- FeeType (Late/Lost/Damaged) classifies the fee; FeeStatus (Unpaid/Paid/Waived) tracks payment state

## Architecture

- **UI**: WinForms (.NET 10)
- **ORM**: Entity Framework Core 10 (SQL Server)
- **Pattern**: Repository + Service layers
- **Auth**: Simple username/password with salted hash
- **Concurrency**: Single machine, one user at a time
