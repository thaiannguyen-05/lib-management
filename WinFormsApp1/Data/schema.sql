-- Library Management System - Database Schema
-- Generated from WinFormsApp1 entity models
-- SQLite dialect

-- ============================================================
-- Enums stored as INTEGER (C# enum ordinal values)
-- ============================================================

-- CopyStatus: 0=Available, 1=Borrowed, 2=Damaged, 3=Lost
-- BorrowStatus: 0=Active, 1=Returned, 2=Overdue, 3=Lost
-- MemberStatus: 0=Active, 1=Suspended, 2=Expired
-- FeeStatus: 0=Unpaid, 1=Paid, 2=Waived
-- UserRole: 0=Admin, 1=Librarian, 2=Staff

-- ============================================================
-- Tables (created in dependency order)
-- ============================================================

CREATE TABLE IF NOT EXISTS MembershipTiers (
    Id              INTEGER PRIMARY KEY AUTOINCREMENT,
    Name            TEXT    NOT NULL,
    MaxBorrowLimit  INTEGER NOT NULL,
    CreatedAt       TEXT    NOT NULL DEFAULT (datetime('now')),
    UpdatedAt       TEXT    NOT NULL DEFAULT (datetime('now'))
);

CREATE TABLE IF NOT EXISTS Authors (
    Id        INTEGER PRIMARY KEY AUTOINCREMENT,
    FirstName TEXT    NOT NULL,
    LastName  TEXT    NOT NULL,
    Bio       TEXT,
    CreatedAt TEXT    NOT NULL DEFAULT (datetime('now')),
    UpdatedAt TEXT    NOT NULL DEFAULT (datetime('now'))
);

CREATE TABLE IF NOT EXISTS Categories (
    Id          INTEGER PRIMARY KEY AUTOINCREMENT,
    Name        TEXT NOT NULL,
    Description TEXT,
    CreatedAt   TEXT NOT NULL DEFAULT (datetime('now')),
    UpdatedAt   TEXT NOT NULL DEFAULT (datetime('now'))
);

CREATE TABLE IF NOT EXISTS Books (
    Id              INTEGER PRIMARY KEY AUTOINCREMENT,
    Title           TEXT    NOT NULL,
    ISBN            TEXT    NOT NULL,
    Publisher       TEXT,
    PublicationYear INTEGER,
    Description     TEXT,
    ShelfLocation   TEXT,
    ReplacementCost REAL    NOT NULL DEFAULT 0,
    CreatedAt       TEXT    NOT NULL DEFAULT (datetime('now')),
    UpdatedAt       TEXT    NOT NULL DEFAULT (datetime('now'))
);

-- Many-to-many: Book <-> Author
CREATE TABLE IF NOT EXISTS BookAuthors (
    BookId    INTEGER NOT NULL,
    AuthorId  INTEGER NOT NULL,
    PRIMARY KEY (BookId, AuthorId),
    FOREIGN KEY (BookId)   REFERENCES Books(Id)   ON DELETE CASCADE,
    FOREIGN KEY (AuthorId) REFERENCES Authors(Id)  ON DELETE CASCADE
);

-- Many-to-many: Book <-> Category
CREATE TABLE IF NOT EXISTS BookCategories (
    BookId     INTEGER NOT NULL,
    CategoryId INTEGER NOT NULL,
    PRIMARY KEY (BookId, CategoryId),
    FOREIGN KEY (BookId)     REFERENCES Books(Id)     ON DELETE CASCADE,
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS BookCopies (
    Id        INTEGER PRIMARY KEY AUTOINCREMENT,
    BookId    INTEGER NOT NULL,
    Status    INTEGER NOT NULL DEFAULT 0,  -- CopyStatus.Available
    CreatedAt TEXT    NOT NULL DEFAULT (datetime('now')),
    UpdatedAt TEXT    NOT NULL DEFAULT (datetime('now')),
    FOREIGN KEY (BookId) REFERENCES Books(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Members (
    Id               INTEGER PRIMARY KEY AUTOINCREMENT,
    FirstName        TEXT    NOT NULL,
    LastName         TEXT    NOT NULL,
    Email            TEXT    NOT NULL,
    Phone            TEXT,
    Status           INTEGER NOT NULL DEFAULT 0,  -- MemberStatus.Active
    MembershipTierId INTEGER NOT NULL,
    CreatedAt        TEXT    NOT NULL DEFAULT (datetime('now')),
    UpdatedAt        TEXT    NOT NULL DEFAULT (datetime('now')),
    FOREIGN KEY (MembershipTierId) REFERENCES MembershipTiers(Id) ON DELETE RESTRICT
);

CREATE TABLE IF NOT EXISTS ApplicationUsers (
    Id           INTEGER PRIMARY KEY AUTOINCREMENT,
    Username     TEXT    NOT NULL UNIQUE,
    PasswordHash TEXT    NOT NULL,
    Role         INTEGER NOT NULL DEFAULT 2,  -- UserRole.Staff
    CreatedAt    TEXT    NOT NULL DEFAULT (datetime('now')),
    UpdatedAt    TEXT    NOT NULL DEFAULT (datetime('now'))
);

CREATE TABLE IF NOT EXISTS BorrowRecords (
    Id               INTEGER PRIMARY KEY AUTOINCREMENT,
    BookCopyId       INTEGER NOT NULL,
    MemberId         INTEGER NOT NULL,
    BorrowDate       TEXT    NOT NULL,
    DueDate          TEXT    NOT NULL,
    ReturnDate       TEXT,
    Status           INTEGER NOT NULL DEFAULT 0,  -- BorrowStatus.Active
    RenewalCount     INTEGER NOT NULL DEFAULT 0,
    CheckedOutByUserId INTEGER NOT NULL,
    ReturnedByUserId   INTEGER,
    CreatedAt        TEXT    NOT NULL DEFAULT (datetime('now')),
    UpdatedAt        TEXT    NOT NULL DEFAULT (datetime('now')),
    FOREIGN KEY (BookCopyId)         REFERENCES BookCopies(Id)        ON DELETE RESTRICT,
    FOREIGN KEY (MemberId)           REFERENCES Members(Id)           ON DELETE RESTRICT,
    FOREIGN KEY (CheckedOutByUserId) REFERENCES ApplicationUsers(Id)  ON DELETE RESTRICT,
    FOREIGN KEY (ReturnedByUserId)   REFERENCES ApplicationUsers(Id)  ON DELETE SET NULL
);

CREATE TABLE IF NOT EXISTS LateFees (
    Id             INTEGER PRIMARY KEY AUTOINCREMENT,
    BorrowRecordId INTEGER NOT NULL,
    Amount         REAL    NOT NULL,
    DateIncurred   TEXT    NOT NULL,
    Status         INTEGER NOT NULL DEFAULT 0,  -- FeeStatus.Unpaid
    WaivedByUserId INTEGER,
    CreatedAt      TEXT    NOT NULL DEFAULT (datetime('now')),
    UpdatedAt      TEXT    NOT NULL DEFAULT (datetime('now')),
    FOREIGN KEY (BorrowRecordId) REFERENCES BorrowRecords(Id)       ON DELETE RESTRICT,
    FOREIGN KEY (WaivedByUserId) REFERENCES ApplicationUsers(Id)    ON DELETE SET NULL
);

CREATE TABLE IF NOT EXISTS FeePayments (
    Id          INTEGER PRIMARY KEY AUTOINCREMENT,
    LateFeeId   INTEGER NOT NULL,
    Amount      REAL    NOT NULL,
    PaymentDate TEXT    NOT NULL,
    CreatedAt   TEXT    NOT NULL DEFAULT (datetime('now')),
    UpdatedAt   TEXT    NOT NULL DEFAULT (datetime('now')),
    FOREIGN KEY (LateFeeId) REFERENCES LateFees(Id) ON DELETE RESTRICT
);

-- ============================================================
-- Indexes
-- ============================================================

CREATE INDEX IF NOT EXISTS IX_Books_ISBN ON Books(ISBN);
CREATE INDEX IF NOT EXISTS IX_BookCopies_BookId ON BookCopies(BookId);
CREATE INDEX IF NOT EXISTS IX_BookCopies_Status ON BookCopies(Status);
CREATE INDEX IF NOT EXISTS IX_Members_Email ON Members(Email);
CREATE INDEX IF NOT EXISTS IX_Members_MembershipTierId ON Members(MembershipTierId);
CREATE INDEX IF NOT EXISTS IX_Members_Status ON Members(Status);
CREATE INDEX IF NOT EXISTS IX_BorrowRecords_BookCopyId ON BorrowRecords(BookCopyId);
CREATE INDEX IF NOT EXISTS IX_BorrowRecords_MemberId ON BorrowRecords(MemberId);
CREATE INDEX IF NOT EXISTS IX_BorrowRecords_Status ON BorrowRecords(Status);
CREATE INDEX IF NOT EXISTS IX_BorrowRecords_CheckedOutByUserId ON BorrowRecords(CheckedOutByUserId);
CREATE INDEX IF NOT EXISTS IX_BorrowRecords_ReturnedByUserId ON BorrowRecords(ReturnedByUserId);
CREATE INDEX IF NOT EXISTS IX_LateFees_BorrowRecordId ON LateFees(BorrowRecordId);
CREATE INDEX IF NOT EXISTS IX_LateFees_Status ON LateFees(Status);
CREATE INDEX IF NOT EXISTS IX_LateFees_WaivedByUserId ON LateFees(WaivedByUserId);
CREATE INDEX IF NOT EXISTS IX_FeePayments_LateFeeId ON FeePayments(LateFeeId);
CREATE INDEX IF NOT EXISTS IX_ApplicationUsers_Username ON ApplicationUsers(Username);

-- ============================================================
-- Seed data: default MembershipTiers
-- ============================================================

INSERT OR IGNORE INTO MembershipTiers (Id, Name, MaxBorrowLimit) VALUES
    (1, 'Basic',    3),
    (2, 'Premium',  5),
    (3, 'VIP',      10);
