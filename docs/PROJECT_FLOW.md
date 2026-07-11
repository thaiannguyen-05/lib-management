# Library Management System — Flow of Actions

## Architecture Overview

```
┌─────────────────────────────────────────────────────────┐
│                      Program.cs                         │
│  - Configures DI (DbContext, Repositories, Services)    │
│  - Auto-migration (EnsureCreated)                       │
│  - Starts LoginForm                                      │
└──────────────────────┬──────────────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────────────┐
│                    LoginForm                            │
│  - User enters username + password                      │
│  - Calls IAuthService.LoginAsync()                      │
│  - On success → SessionManager.Login(user)              │
│             → AuditService.Log("Login")                 │
│             → Opens MainForm, hides LoginForm            │
└──────────────────────┬──────────────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────────────┐
│                    MainForm                             │
│  - Shows welcome message + role                         │
│  - Buttons visible based on role (Admin/Librarian/Staff)│
│  - Logout → SessionManager.Logout()                     │
│          → AuditService.Log("Logout")                   │
│          → Back to LoginForm                             │
└─────────────────────────────────────────────────────────┘
```

---

## Authentication Flow

```
User types username + password
        │
        ▼
┌─ LoginForm.btnLogin_Click() ────────────────────────────┐
│  1. Validate inputs (not empty)                         │
│  2. IAuthService.LoginAsync(username, password)          │
│     └─ AuthService:                                      │
│        a. Find user by username in DB                    │
│        b. BCrypt.Verify(password, storedHash)            │
│        c. Return ApplicationUser or null                 │
│  3. If null → show "Invalid credentials"                 │
│  4. If user found:                                       │
│     a. SessionManager.Login(user)  ← stores in memory   │
│     b. AuditService.Log("Login")   ← writes to DB       │
│     c. Open MainForm                                     │
└──────────────────────────────────────────────────────────┘
```

---

## Role-Based Access Control

| Action | Admin | Librarian | Staff |
|--------|:-----:|:---------:|:-----:|
| View Books | ✓ | ✓ | ✓ |
| View Members | ✓ | ✓ | ✓ |
| Borrowing Operations | ✓ | ✓ | ✓ |
| User Management | ✓ | ✗ | ✗ |
| Reports | ✓ | ✓ | ✗ |
| Audit Logs | ✓ | ✗ | ✗ |

**How it works:**
- `SessionManager.CurrentUser.Role` stores the logged-in user's role
- MainForm checks `SessionManager.IsAdmin`, `IsLibrarian`, `IsStaff` to show/hide buttons
- Future forms should check role before allowing actions

---

## Data Flow — Repository Pattern

```
Form (UI)
  │
  ▼
Service (Business Logic)
  │
  ▼
IRepository<T> / IUnitOfWork (Data Access)
  │
  ▼
AppDbContext (EF Core) → SQLite DB
```

**Example — Milestone 3 (Books):**
```
BookForm
  → BookService.GetAllAsync()
    → _repo.GetAllAsync()
      → _context.Books.ToListAsync()
```

---

## Seed Data Flow

On first run, `Program.cs` calls:
```csharp
db.Database.EnsureDeleted();  // ← for dev, remove later
db.Database.EnsureCreated();  // ← creates DB + seeds data
```

`AppDbContext.OnModelCreating()` seeds:
- **3 ApplicationUsers** (admin, librarian1, staff1) — passwords BCrypt hashed
- **4 Publishers** (O'Reilly, Pearson, Springer, NXB Tre)
- **3 Departments** (CS, Math, Physics)
- **3 StudentClasses** (CS2023A, CS2023B, MATH2024A)
- **4 Authors** (Robert Martin, Thomas Cormen, Erich Gamma, Nguyen Viet)
- **5 Categories** (Programming, Algorithms, Design Patterns, Fiction, Science)
- **5 Books** (Clean Code, CLRS, Design Patterns, SQL Fundamentals, Toi thay hoa vang)
- **6 BookCopies** (2 Clean Code, 1 CLRS, 1 Design Patterns, 1 SQL, 1 Fiction)
- **4 Members** (2 Students, 1 Teacher, 1 External)
- **4 LibraryCards** (one per member)

---

## Key Files Reference

| File | Role |
|------|------|
| `Program.cs` | Entry point, DI configuration, auto-migration |
| `Data/AppDbContext.cs` | EF Core context, model configuration, seed data |
| `Data/IRepository.cs` | Generic repository interface |
| `Data/Repository.cs` | Generic repository implementation |
| `Data/IUnitOfWork.cs` | Unit of work interface |
| `Data/UnitOfWork.cs` | Unit of work implementation |
| `Helpers/PasswordHasher.cs` | BCrypt hash + verify |
| `Helpers/SessionManager.cs` | Static login state + role checks |
| `Services/IAuthService.cs` | Auth service interface |
| `Services/AuthService.cs` | Login logic |
| `Services/IAuditService.cs` | Audit log interface |
| `Services/AuditService.cs` | Audit log writes |
| `Forms/LoginForm.cs` | Login screen |
| `Forms/MainForm.cs` | Post-login dashboard |
| `Models/ApplicationUser.cs` | User entity (username, password hash, role) |
| `Models/AuditLog.cs` | Audit trail entity |
| `Models/Enums/UserRole.cs` | Admin, Librarian, Staff |

---

## Milestone Roadmap

| Milestone | Feature | Status |
|-----------|---------|--------|
| 1 | Data Layer (DbContext, Models, Repositories) | ✓ Done |
| 2 | Authentication & Authorization | ✓ Done |
| 3 | Book Catalog Management | → Next |
| 4 | Member Management | Planned |
| 5 | Borrowing & Returning | Planned |
| 6 | Late Fees & Payments | Planned |
| 7 | Reports & Dashboard | Planned |
| 8 | Reservations | Planned |
| 9 | Inventory Logs | Planned |
| 10 | Polish & Testing | Planned |
