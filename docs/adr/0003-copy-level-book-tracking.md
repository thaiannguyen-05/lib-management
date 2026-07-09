# Copy-Level Book Tracking

Books are catalog entries (title, ISBN, metadata). Physical copies are tracked individually via BookCopy entities with their own status (Available, Borrowed, Damaged, Lost).

This replaces the title-level CopiesTotal/CopiesAvailable model. The trade-off is more tables and slightly more complex queries, but we gain: individual copy status, accurate loss/damage tracking, and realistic borrow workflows (you borrow a specific copy, not an abstract title).

Computed properties like Book.AvailableCount become LINQ queries over BookCopies rather than stored fields.
