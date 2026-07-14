# Library Management System — UML Diagrams

## 1. Use Case Diagram

```
┌─────────────────────────────────────────────────────────────────────────────┐
│                        Library Management System                            │
│                                                                             │
│  ┌──────────────┐                                                           │
│  │    Admin      │──── Manage Users                                         │
│  │              │──── Manage Authors                                        │
│  │              │──── Manage Categories                                     │
│  │              │──── View Reports                                          │
│  │              │──── Manage Books                                          │
│  │              │──── Manage Members                                        │
│  │              │──── Process Borrowing/Returning                           │
│  │              │──── Manage Reservations                                   │
│  │              │──── Waive Late Fees                                       │
│  └──────────────┘                                                           │
│                                                                             │
│  ┌──────────────┐                                                           │
│  │  Librarian    │──── Manage Authors                                       │
│  │              │──── Manage Categories                                     │
│  │              │──── View Reports                                          │
│  │              │──── Manage Books                                          │
│  │              │──── Manage Members                                        │
│  │              │──── Process Borrowing/Returning                           │
│  │              │──── Manage Reservations                                   │
│  │              │──── Waive Late Fees                                       │
│  └──────────────┘                                                           │
│                                                                             │
│  ┌──────────────┐                                                           │
│  │    Staff      │──── Process Borrowing/Returning                          │
│  │              │──── Manage Books                                          │
│  │              │──── Manage Members                                        │
│  └──────────────┘                                                           │
│                                                                             │
│  ┌──────────────┐                                                           │
│  │   Member      │──── Search Books                                         │
│  │              │──── Reserve Books                                         │
│  │              │──── View Borrow History                                   │
│  │              │──── Pay Late Fees                                         │
│  └──────────────┘                                                           │
│                                                                             │
│  ┌──────────────┐                                                           │
│  │   System      │──── Authenticate User (Login/Logout)                     │
│  │              │──── Log Audit Trail                                       │
│  │              │──── Calculate Late Fees                                   │
│  │              │──── Send Reservation Notifications                        │
│  └──────────────┘                                                           │
└─────────────────────────────────────────────────────────────────────────────┘
```

### Actors

| Actor | Description |
|---|---|
| Admin | Full system access. Manages users, authors, categories, reports. |
| Librarian | Manages catalog (authors, categories), reports, books, members, borrowing. |
| Staff | Limited access. Processes borrowing/returning, manages books/members. |
| Member | Library patron. Searches books, reserves, views history, pays fees. |
| System | Automated processes (auth, audit, fee calculation, notifications). |

### Use Cases

| Use Case | Actor(s) | Description |
|---|---|---|
| Authenticate | All users | Login with username/password, session management |
| Manage Users | Admin | CRUD ApplicationUser accounts, assign roles |
| Manage Authors | Admin, Librarian | CRUD Author entities |
| Manage Categories | Admin, Librarian | CRUD Category entities |
| Manage Books | Admin, Librarian, Staff | CRUD Book, BookCopy, Publisher |
| Manage Members | Admin, Librarian, Staff | CRUD Member, LibraryCard |
| Process Borrowing | Admin, Librarian, Staff | Create BorrowRecord, update CopyStatus |
| Process Returning | Admin, Librarian, Staff | Update BorrowRecord, calculate fees |
| Reserve Books | Member | Create Reservation for a Book title |
| View Reports | Admin, Librarian | Generate borrowing, overdue, member reports |
| Waive Late Fees | Admin, Librarian | Change FeeStatus to Waived |
| Pay Late Fees | Member | Create FeePayment against LateFee |
| Log Audit | System | Record all entity mutations in AuditLog |

---

## 2. Activity Diagram — Borrowing Process

```
                    ┌───────────────┐
                    │  Staff/Libr.   │
                    │  initiates     │
                    │  borrow        │
                    └───────┬───────┘
                            │
                            ▼
                 ┌─────────────────────┐
                 │  Search/Select       │
                 │  Member              │
                 └─────────┬───────────┘
                           │
                           ▼
                 ┌─────────────────────┐
                 │  Check: Member       │
                 │  Status == Active?   │
                 └─────────┬───────────┘
                      ┌────┴────┐
                     YES        NO
                      │         │
                      ▼         ▼
          ┌──────────────┐  ┌──────────────┐
          │ Check unpaid  │  │ Show error:  │
          │ fees?         │  │ "Member not  │
          └──────┬───────┘  │  active"     │
            ┌────┴────┐     └──────────────┘
           YES        NO
            │         │
            ▼         ▼
   ┌──────────────┐  ┌──────────────────┐
   │ Show error:  │  │ Search/Select     │
   │ "Unpaid fees │  │ BookCopy          │
   │  exist"      │  └────────┬─────────┘
   └──────────────┘           │
                              ▼
                   ┌─────────────────────┐
                   │ Check: CopyStatus    │
                   │ == Available?        │
                   └─────────┬───────────┘
                        ┌────┴────┐
                       YES        NO
                        │         │
                        ▼         ▼
            ┌──────────────┐  ┌──────────────┐
            │ Create       │  │ Show error:  │
            │ BorrowRecord │  │ "Copy not    │
            │              │  │  available"  │
            │ Status=Active│  └──────────────┘
            │ BorrowDate=now│
            │ DueDate=now+14│
            └──────┬───────┘
                   │
                   ▼
          ┌────────────────────┐
          │ Update BookCopy     │
          │ Status = Borrowed   │
          └─────────┬──────────┘
                    │
                    ▼
          ┌────────────────────┐
          │ Create AuditLog     │
          │ Action = "Checkout" │
          └─────────┬──────────┘
                    │
                    ▼
            ┌───────────────┐
            │     DONE      │
            └───────────────┘
```

---

## 3. Class Diagram

```
┌─────────────────────────────┐         ┌─────────────────────────────┐
│       <<abstract>>          │         │         <<enum>>            │
│       BaseEntity            │         │       UserRole              │
├─────────────────────────────┤         ├─────────────────────────────┤
│ +Id : int                   │         │ Admin                       │
│ +CreatedAt : DateTime       │         │ Librarian                   │
│ +UpdatedAt : DateTime       │         │ Staff                       │
└──────────┬──────────────────┘         └─────────────────────────────┘
           │
           │ inherits
           │
┌──────────┴──────────────────────────────────────────────────────────┐
│                                                                     │
│  ┌──────────────────┐  ┌──────────────────┐  ┌──────────────────┐  │
│  │  ApplicationUser  │  │      Book        │  │     BookCopy     │  │
│  ├──────────────────┤  ├──────────────────┤  ├──────────────────┤  │
│  │ Username         │  │ Title            │  │ BookId (FK)      │  │
│  │ PasswordHash     │  │ ISBN [unique]    │  │ Status           │  │
│  │ Role             │  │ PublisherId (FK?)│  │ : CopyStatus     │  │
│  │ : UserRole       │  │ PublicationYear  │  └────────┬─────────┘  │
│  └────────┬─────────┘  │ Description      │           │            │
│           │             │ ShelfLocation    │           │            │
│           │             │ ReplacementCost  │           │            │
│           │             └────────┬─────────┘           │            │
│           │                      │                     │            │
│  ┌────────┴─────────┐  ┌────────┴─────────┐  ┌────────┴─────────┐  │
│  │   CheckedOut     │  │     Copies       │  │  BorrowRecords   │  │
│  │   Borrows        │  │     1:N          │  │     1:N          │  │
│  │   Returned       │  └──────────────────┘  └────────┬─────────┘  │
│  │   Borrows        │                                  │            │
│  │   WaivedFees     │  ┌──────────────────┐  ┌────────┴─────────┐  │
│  │   AuditLogs      │  │     Author       │  │  BorrowRecord    │  │
│  │   Performed      │  ├──────────────────┤  ├──────────────────┤  │
│  │   InventoryLogs  │  │ FirstName        │  │ BookCopyId (FK)  │  │
│  └──────────────────┘  │ LastName         │  │ MemberId (FK)    │  │
│                        │ Bio              │  │ BorrowDate       │  │
│  ┌──────────────────┐  └────────┬─────────┘  │ DueDate          │  │
│  │     Member       │           │            │ ReturnDate       │  │
│  ├──────────────────┤           │            │ Status           │  │
│  │ FirstName        │  ┌────────┴─────────┐  │ : BorrowStatus   │  │
│  │ LastName         │  │  BookAuthors     │  │ RenewalCount     │  │
│  │ Email [unique]   │  │  (join table)    │  │ CheckedOutById   │  │
│  │ Phone            │  ├──────────────────┤  │ ReturnedById     │  │
│  │ Status           │  │ BookId (FK)      │  └────────┬─────────┘  │
│  │ : MemberStatus   │  │ AuthorId (FK)    │           │            │
│  │ MemberType       │  └──────────────────┘           │            │
│  │ : MemberType     │                                 │            │
│  │ Department?      │  ┌──────────────────┐  ┌────────┴─────────┐  │
│  │ (string)         │  │    Category       │  │    LateFee       │  │
│  └────────┬─────────┘  ├──────────────────┤  ├──────────────────┤  │
│           │             │ Name [unique]    │  │ BorrowRecordId   │  │
│           │             │ Description      │  │ Amount           │  │
│           │             └────────┬─────────┘  │ DateIncurred     │  │
│           │                      │            │ Type : FeeType   │  │
│           │             ┌────────┴─────────┐  │ Status           │  │
│           │             │ BookCategories   │  │ : FeeStatus      │  │
│           │             │ (join table)     │  │ WaivedByUserId   │  │
│           │             ├──────────────────┤  └────────┬─────────┘  │
│           │             │ BookId (FK)      │           │            │
│           │             │ CategoryId (FK)  │  ┌────────┴─────────┐  │
│           │             └──────────────────┘  │   FeePayment     │  │
│           │                                   ├──────────────────┤  │
│  ┌────────┴─────────┐  ┌──────────────────┐  │ LateFeeId (FK)   │  │
│  │  BorrowRecords   │  │    Publisher      │  │ Amount           │  │
│  │  LateFees        │  ├──────────────────┤  │ PaymentDate      │  │
│  │  Reservations    │  │ Name             │  └──────────────────┘  │
│  │  LibraryCard     │  │ Address          │                        │
│  └──────────────────┘  │ Phone            │  ┌──────────────────┐  │
│                        └────────┬─────────┘  │   Reservation    │  │
│  ┌──────────────────┐           │            ├──────────────────┤  │
│  │   LibraryCard    │  ┌────────┴─────────┐  │ BookId (FK)      │  │
│  ├──────────────────┤  │     Books        │  │ MemberId (FK)    │  │
│  │ MemberId (FK)    │  │     1:N          │  │ ReservationDate  │  │
│  │ CardNumber       │  └──────────────────┘  │ ExpiryDate       │  │
│  │ ExpiryDate       │                        │ Status           │  │
│  │ Status           │  ┌──────────────────┐  │ : Reservation-   │  │
│  │ : CardStatus     │  │   AuditLog       │  │   Status        │  │
│  └──────────────────┘  ├──────────────────┤  └──────────────────┘  │
│                        │ UserId (FK)      │                        │
│                        │ Action           │  ┌──────────────────┐  │
│                        │ EntityName       │  │  InventoryLog    │  │
│                        │ EntityId         │  ├──────────────────┤  │
│                        │ Details          │  │ BookCopyId (FK)  │  │
│                        │ Timestamp        │  │ Action           │  │
│                        └──────────────────┘  │ : Inventory-     │  │
│                                              │   Action         │  │
│                                              │ Quantity         │  │
│                                              │ Note             │  │
│                                              │ PerformedBy-     │  │
│                                              │   UserId (FK)    │  │
│                                              └──────────────────┘  │
└─────────────────────────────────────────────────────────────────────┘
```

### Relationships Summary

| Relationship | Type | FK | Delete Behavior |
|---|---|---|---|
| Publisher → Book | 1:N | Book.PublisherId | SetNull |
| Book → BookCopy | 1:N | BookCopy.BookId | Cascade |
| Book ↔ Author | M:N | BookAuthors join table | Cascade |
| Book ↔ Category | M:N | BookCategories join table | Cascade |
| Member ↔ LibraryCard | 1:1 | LibraryCard.MemberId | Cascade |
| BookCopy → BorrowRecord | 1:N | BorrowRecord.BookCopyId | Restrict |
| Member → BorrowRecord | 1:N | BorrowRecord.MemberId | Restrict |
| ApplicationUser → BorrowRecord (checkout) | 1:N | BorrowRecord.CheckedOutByUserId | Restrict |
| ApplicationUser → BorrowRecord (return) | 1:N | BorrowRecord.ReturnedByUserId | SetNull |
| BorrowRecord → LateFee | 1:N | LateFee.BorrowRecordId | Restrict |
| ApplicationUser → LateFee (waived) | 1:N | LateFee.WaivedByUserId | SetNull |
| LateFee → FeePayment | 1:N | FeePayment.LateFeeId | Restrict |
| Book → Reservation | 1:N | Reservation.BookId | Restrict |
| Member → Reservation | 1:N | Reservation.MemberId | Restrict |
| BookCopy → InventoryLog | 1:N | InventoryLog.BookCopyId | Restrict |
| ApplicationUser → InventoryLog | 1:N | InventoryLog.PerformedByUserId | Restrict |
| ApplicationUser → AuditLog | 1:N | AuditLog.UserId | Restrict |

---

## 4. ERD (Entity Relationship Diagram)

```
┌──────────────────┐       ┌──────────────────┐       ┌──────────────────┐
│  publishers      │       │     books         │       │   book_copies    │
├──────────────────┤       ├──────────────────┤       ├──────────────────┤
│ PK id            │──┐    │ PK id            │──┐    │ PK id            │
│    name          │  │    │    title         │  │    │ FK book_id       │──┐
│    address       │  │    │    isbn [unique] │  │    │    status        │  │
│    phone         │  │    │ FK publisher_id  │──┘    └────────┬─────────┘  │
│    created_at    │  │    │    pub_year      │               │            │
│    updated_at    │  │    │    description   │               │            │
└──────────────────┘  │    │    shelf_loc     │               │            │
                      │    │    repl_cost     │               │            │
                      │    │    created_at    │               │            │
                      │    │    updated_at    │               │            │
                      │    └────────┬─────────┘               │            │
                      │             │                         │            │
                      │    ┌────────┴─────────┐               │            │
                      │    │  book_authors    │               │            │
                      │    │  (join table)    │               │            │
                      │    ├──────────────────┤               │            │
                      │    │ PK,FK book_id    │               │            │
                      │    │ PK,FK author_id  │               │            │
                      │    └──────────────────┘               │            │
                      │                                      │            │
                      │    ┌──────────────────┐               │            │
                      │    │ book_categories  │               │            │
                      │    │  (join table)    │               │            │
                      │    ├──────────────────┤               │            │
                      │    │ PK,FK book_id    │               │            │
                      │    │ PK,FK category_id│               │            │
                      │    └──────────────────┘               │            │
                      │                                      │            │
                      │    ┌──────────────────┐               │            │
                      │    │    authors       │               │            │
                      │    ├──────────────────┤               │            │
                      │    │ PK id            │               │            │
                      │    │    first_name    │               │            │
                      │    │    last_name     │               │            │
                      │    │    bio           │               │            │
                      │    │    created_at    │               │            │
                      │    │    updated_at    │               │            │
                      │    └──────────────────┘               │            │
                      │                                      │            │
                      │    ┌──────────────────┐               │            │
                      │    │   categories     │               │            │
                      │    ├──────────────────┤               │            │
                      │    │ PK id            │               │            │
                      │    │    name [unique] │               │            │
                      │    │    description   │               │            │
                      │    │    created_at    │               │            │
                      │    │    updated_at    │               │            │
                      │    └──────────────────┘               │            │
                      │                                      │            │
                      │    ┌──────────────────┐               │            │
                      │    │  reservations    │               │            │
                      │    ├──────────────────┤               │            │
                      │    │ PK id            │               │            │
                      │    │ FK book_id       │───────────────┘            │
                      │    │ FK member_id     │──┐                         │
                      │    │    reserve_date  │  │                         │
                      │    │    expiry_date   │  │                         │
                      │    │    status        │  │                         │
                      │    │    created_at    │  │                         │
                      │    │    updated_at    │  │                         │
                      │    └──────────────────┘  │                         │
                      │                          │                         │
                      │    ┌──────────────────┐  │                         │
                      │    │    members       │  │                         │
                      │    ├──────────────────┤  │                         │
                      │    │ PK id            │──┘                         │
                      │    │    first_name    │                            │
                      │    │    last_name     │                            │
                      │    │    email [unique]│                            │
                      │    │    phone         │                            │
                      │    │    status        │                            │
                      │    │    member_type   │                            │
                      │    │    department?   │                            │
                      │    │    created_at    │                            │
                      │    │    updated_at    │                            │
                      │    └──────────────────┘                            │
                      │                                                  │
                      │    ┌──────────────────┐                           │
                      │    │  library_cards   │                           │
                      │    ├──────────────────┤                           │
                      │    │ PK id            │                           │
                      │    │ FK member_id     │ [unique]                  │
                      │    │    card_number   │ [unique]                  │
                      │    │    expiry_date   │                           │
                      │    │    status        │                           │
                      │    │    created_at    │                           │
                      │    │    updated_at    │                           │
                      │    └──────────────────┘                           │
                      │                                                  │
                      │    ┌──────────────────┐     ┌──────────────────┐  │
                      │    │ borrow_records   │     │   application_   │  │
                      │    ├──────────────────┤     │     users        │  │
                      │    │ PK id            │     ├──────────────────┤  │
                      │    │ FK book_copy_id  │────>│ PK id            │  │
                      │    │ FK member_id     │────>│    username      │  │
                      │    │    borrow_date   │     │    password_hash │  │
                      │    │    due_date      │     │    role          │  │
                      │    │    return_date   │     │    created_at    │  │
                      │    │    status        │     │    updated_at    │  │
                      │    │    renewal_count │     └────────┬─────────┘  │
                      │    │ FK checked_out_by│──────────────┘            │
                      │    │ FK returned_by   │──────────────┘            │
                      │    │    created_at    │                           │
                      │    │    updated_at    │     ┌──────────────────┐  │
                      │    └────────┬─────────┘     │   audit_logs     │  │
                      │             │               ├──────────────────┤  │
                      │    ┌────────┴─────────┐     │ PK id            │  │
                      │    │   late_fees      │     │ FK user_id       │──┘
                      │    ├──────────────────┤     │    action        │
                      │    │ PK id            │     │    entity_name   │
                      │    │ FK borrow_rec_id │     │    entity_id     │
                      │    │    amount        │     │    details       │
                      │    │    date_incurred │     │    timestamp     │
                      │    │    type          │     │    created_at    │
                      │    │    status        │     │    updated_at    │
                      │    │ FK waived_by_uid │────>└──────────────────┘
                      │    │    created_at    │
                      │    │    updated_at    │     ┌──────────────────┐
                      │    └────────┬─────────┘     │  inventory_logs  │
                      │             │               ├──────────────────┤
                      │    ┌────────┴─────────┐     │ PK id            │
                      │    │  fee_payments    │     │ FK book_copy_id  │──┘
                      │    ├──────────────────┤     │    action        │
                      │    │ PK id            │     │    quantity      │
                      │    │ FK late_fee_id   │     │    note          │
                      │    │    amount        │     │ FK performed_by  │──┘
                      │    │    payment_date  │     │    created_at    │
                      │    │    created_at    │     │    updated_at    │
                      │    │    updated_at    │     └──────────────────┘
                      │    └──────────────────┘
```

### Cardinality Summary

| Relationship | Cardinality |
|---|---|
| Publisher → Book | 1:N (optional) |
| Book → BookCopy | 1:N (required) |
| Book ↔ Author | M:N |
| Book ↔ Category | M:N |
| Member → LibraryCard | 1:1 |
| BookCopy → BorrowRecord | 1:N |
| Member → BorrowRecord | 1:N |
| ApplicationUser → BorrowRecord (checkout) | 1:N |
| ApplicationUser → BorrowRecord (return) | 1:N (optional) |
| BorrowRecord → LateFee | 1:N |
| ApplicationUser → LateFee (waived) | 1:N (optional) |
| LateFee → FeePayment | 1:N |
| Book → Reservation | 1:N |
| Member → Reservation | 1:N |
| BookCopy → InventoryLog | 1:N |
| ApplicationUser → InventoryLog | 1:N |
| ApplicationUser → AuditLog | 1:N |
