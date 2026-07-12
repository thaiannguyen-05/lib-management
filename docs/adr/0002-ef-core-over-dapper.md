# EF Core over Dapper

The project uses Dapper with raw SQL. We're switching to Entity Framework Core 8.

EF Core 10 gives us code-first migrations (no manual SQL schema scripts), LINQ queries, change tracking, and easier repository patterns. The trade-off is slightly more abstraction and a learning curve for migrations, but for a study project this is the right trade-off — EF Core is the industry standard .NET ORM and the skills transfer directly.

Dapper is excellent for performance-critical apps, but a library management desktop app doesn't need that optimization. We'll use EF Core 10's SQLite provider to keep the zero-config file-based database.
