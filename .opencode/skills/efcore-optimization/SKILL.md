# Skill: EF Core + LINQ Production Optimization Rules

Use this skill when writing .NET + EF Core + LINQ code. Enforce these rules to avoid N+1 queries, optimize SQL, and prevent over-loading data.

## Trigger

- Writing EF Core queries or LINQ
- Reviewing code that uses DbContext
- Optimizing database access
- Code review for data layer

---

## Rules

### 1. Lazy Loading — NEVER Enable

```csharp
// ❌ NEVER
services.AddDbContext<AppDbContext>(options =>
    options.UseLazyLoadingProxies()
);

// ✅ DO
services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString)
);
```

Lazy loading causes N+1. Every navigation property access fires a separate SQL query.

---

### 2. Projection Over Include (Read-Only)

```csharp
// ❌ BAD — loads entire Order entities
var users = await db.Users
    .Include(x => x.Orders)
    .ToListAsync();

// ✅ GOOD — only selects what's needed
var users = await db.Users
    .Select(x => new UserDto
    {
        Id = x.Id,
        Name = x.Name,
        OrderCount = x.Orders.Count
    })
    .ToListAsync();
```

EF generates subquery for count. No full entity materialization.

---

### 3. Include Only When Entity Needed for Mutation

```csharp
// ❌ BAD — reading only, no need for Include
var users = await db.Users
    .Include(x => x.Orders)
    .ToListAsync();

// ✅ GOOD — reading only
var users = await db.Users
    .Select(x => new { x.Id, x.Name, OrderCount = x.Orders.Count })
    .ToListAsync();

// ✅ GOOD — need entity to update
var order = await db.Orders
    .Include(x => x.Items)
    .FirstAsync();

order.Items.Add(new OrderItem { ... });
await db.SaveChangesAsync();
```

---

### 4. No Deep ThenInclude Chains

```csharp
// ❌ BAD — loads Orders + Items + Products + Categories
var companies = await db.Companies
    .Include(x => x.Orders)
        .ThenInclude(x => x.Items)
            .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Category)
    .ToListAsync();

// ✅ GOOD — only need product name
var companies = await db.Companies
    .Select(x => new
    {
        x.Name,
        ProductNames = x.Orders
            .SelectMany(o => o.Items)
            .Select(i => i.Product.Name)
            .Distinct()
    })
    .ToListAsync();
```

---

### 5. Never ToList Too Early

```csharp
// ❌ BAD — materializes entire table
var users = await db.Users.ToListAsync();
var result = users.Where(x => x.Active).Select(x => x.Name);

// ✅ GOOD — filters in DB
var result = await db.Users
    .Where(x => x.Active)
    .Select(x => x.Name)
    .ToListAsync();
```

---

### 6. Never Foreach Then Query (N+1 Classic)

```csharp
// ❌ BAD — N+1
var users = await db.Users.ToListAsync();
foreach (var user in users)
{
    user.Orders = await db.Orders
        .Where(x => x.UserId == user.Id)
        .ToListAsync();
}

// ✅ GOOD
var users = await db.Users
    .Include(x => x.Orders)
    .ToListAsync();

// ✅ BETTER — projection
var users = await db.Users
    .Select(x => new UserDto
    {
        Id = x.Id,
        Name = x.Name,
        Orders = x.Orders.Select(o => new OrderDto { ... }).ToList()
    })
    .ToListAsync();
```

---

### 7. Never Any() in Loop

```csharp
// ❌ BAD — N queries
foreach (var user in users)
{
    var exists = await db.Orders.AnyAsync(x => x.UserId == user.Id);
}

// ✅ GOOD — single query
var userIds = users.Select(x => x.Id);
var usersWithOrders = await db.Orders
    .Where(x => userIds.Contains(x.UserId))
    .Select(x => x.UserId)
    .Distinct()
    .ToListAsync();

var hasOrders = new HashSet<int>(usersWithOrders);
```

---

### 8. Never Count() in Loop

```csharp
// ❌ BAD — N queries
foreach (var user in users)
{
    var count = await db.Orders.CountAsync(x => x.UserId == user.Id);
}

// ✅ GOOD — single grouped query
var orderCounts = await db.Orders
    .GroupBy(x => x.UserId)
    .Select(x => new { UserId = x.Key, Count = x.Count() })
    .ToDictionaryAsync(x => x.UserId, x => x.Count);
```

---

### 9. Never First() in Loop

```csharp
// ❌ BAD — N queries
foreach (var id in ids)
{
    var user = await db.Users.FirstAsync(x => x.Id == id);
}

// ✅ GOOD — single query
var users = await db.Users
    .Where(x => ids.Contains(x.Id))
    .ToListAsync();
```

---

### 10. AsNoTracking for Read-Only Queries

```csharp
// ✅ Read-only query
var users = await db.Users
    .AsNoTracking()
    .Where(x => x.Active)
    .ToListAsync();

// ✅ Need to update — no AsNoTracking
var user = await db.Users
    .FirstAsync(x => x.Id == id);

user.Name = "Updated";
await db.SaveChangesAsync();
```

Reduces memory. Prevents change tracker overhead.

---

### 11. Never Select All Columns

```csharp
// ❌ BAD — SELECT *
var users = await db.Users.ToListAsync();

// ✅ GOOD — SELECT only needed columns
var users = await db.Users
    .Select(x => new { x.Id, x.Name })
    .ToListAsync();
```

---

### 12. Never Include Large Collections

```csharp
// ❌ BAD — Cartesian explosion
// 1000 companies × 100 orders × 20 items = 2,000,000 rows
var companies = await db.Companies
    .Include(x => x.Orders)
        .ThenInclude(x => x.Items)
    .ToListAsync();

// ✅ GOOD — query separately or project
var companyDtos = await db.Companies
    .Select(x => new CompanyDto
    {
        Id = x.Id,
        Name = x.Name,
        OrderCount = x.Orders.Count
    })
    .ToListAsync();
```

---

### 13. SplitQuery for Multiple Collection Includes

```csharp
// ✅ Use when including multiple collections from same root
var orders = await db.Orders
    .Include(x => x.Items)
    .Include(x => x.Payments)
    .AsSplitQuery()
    .ToListAsync();

// Without SplitQuery, EF generates Cartesian product
```

---

### 14. Never Distinct After Include

```csharp
// ❌ BAD — smell
var users = await db.Users
    .Include(x => x.Orders)
    .Distinct()
    .ToListAsync();

// ✅ GOOD — use projection
var users = await db.Users
    .Select(x => new { x.Id, x.Name })
    .Distinct()
    .ToListAsync();
```

---

### 15. No Client Evaluation

```csharp
// ❌ BAD — EF cannot translate to SQL
var users = await db.Users
    .Where(x => MyCustomMethod(x.Name))
    .ToListAsync();

// ✅ GOOD — use expression EF can translate
var users = await db.Users
    .Where(x => x.Name.StartsWith("A"))
    .ToListAsync();

// ✅ GOOD — if must filter client-side, do it after ToList
var users = (await db.Users.ToListAsync())
    .Where(x => MyCustomMethod(x.Name))
    .ToList();
```

---

### 16. Pagination Always in DB

```csharp
// ❌ BAD — loads all, then skips
var users = await db.Users.ToListAsync();
var page = users.Skip(20).Take(10).ToList();

// ✅ GOOD — pagination in SQL
var page = await db.Users
    .OrderBy(x => x.Id)
    .Skip(20)
    .Take(10)
    .ToListAsync();
```

---

### 17. Single SaveChanges per Batch

```csharp
// ❌ BAD — multiple round-trips
foreach (var item in items)
{
    db.Add(item);
    await db.SaveChangesAsync();
}

// ✅ GOOD — single round-trip
db.AddRange(items);
await db.SaveChangesAsync();
```

---

### 18. Batch Update/Delete (EF Core 7+)

```csharp
// ❌ BAD — SELECT then loop UPDATE
var users = await db.Users.Where(x => !x.Active).ToListAsync();
foreach (var user in users)
{
    user.Status = MemberStatus.Expired;
}
await db.SaveChangesAsync();

// ✅ GOOD — single UPDATE statement
await db.Users
    .Where(x => !x.Active)
    .ExecuteUpdateAsync(s => s
        .SetProperty(x => x.Status, MemberStatus.Expired)
    );

// ✅ GOOD — batch delete
await db.Users
    .Where(x => x.CreatedAt < cutoffDate)
    .ExecuteDeleteAsync();
```

---

### 19. Never Include for Count

```csharp
// ❌ BAD — loads all Orders into memory
var user = await db.Users
    .Include(x => x.Orders)
    .FirstAsync(x => x.Id == id);
var count = user.Orders.Count;

// ✅ GOOD — count in DB
var count = await db.Users
    .Where(x => x.Id == id)
    .Select(x => x.Orders.Count)
    .FirstAsync();
```

---

### 20. Log SQL in Development

```csharp
// In Program.cs or DbContext
options
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors()
    .LogTo(Console.WriteLine, LogLevel.Information);
```

If you see multiple SELECT statements for what should be one query, you have N+1.

---

### 21. Avoid Contains on Large Lists

```csharp
// ⚠️ OK for small lists (tens to hundreds)
var users = await db.Users
    .Where(x => userIds.Contains(x.Id))
    .ToListAsync();

// ❌ BAD — thousands of IDs
var ids = Enumerable.Range(1, 50000);
var users = await db.Users
    .Where(x => ids.Contains(x.Id))  // IN (...) very long
    .ToListAsync();

// ✅ GOOD — batch or use temp table
var batchSize = 1000;
var allUsers = new List<User>();
foreach (var batch in userIds.Chunk(batchSize))
{
    var batchUsers = await db.Users
        .Where(x => batch.Contains(x.Id))
        .ToListAsync();
    allUsers.AddRange(batchUsers);
}
```

---

### 22. Never Materialize Mid-Query

```csharp
// ❌ BAD — ToList() before finishing query
var query = db.Users
    .Where(x => x.Active)
    .ToList();  // materialized!

var result = query
    .Select(x => x.Name)
    .OrderBy(x => x)
    .ToList();

// ✅ GOOD — keep IQueryable until final
var result = await db.Users
    .Where(x => x.Active)
    .Select(x => x.Name)
    .OrderBy(x => x)
    .ToListAsync();
```

---

### 23. Select Before Include for DTOs

```csharp
// ❌ BAD — Include then Select
var users = await db.Users
    .Include(x => x.Orders)
    .Select(x => new UserDto
    {
        Id = x.Id,
        Name = x.Name,
        Orders = x.Orders.Select(o => new OrderDto { ... }).ToList()
    })
    .ToListAsync();

// ✅ GOOD — Select without Include (EF handles JOINs)
var users = await db.Users
    .Select(x => new UserDto
    {
        Id = x.Id,
        Name = x.Name,
        Orders = x.Orders.Select(o => new OrderDto
        {
            Id = o.Id,
            Total = o.Total
        }).ToList()
    })
    .ToListAsync();
```

EF Core generates proper SQL from navigation properties in Select expressions. Include is unnecessary.

---

## Quick Reference Checklist

| Rule | Pattern | Fix |
|------|---------|-----|
| N+1 | foreach + query | Include or batch query |
| Over-load | Include for reading | Projection with Select |
| Deep chain | ThenInclude 3+ levels | Flatten with Select |
| Too early | ToList before filter | Chain Where/Select first |
| Client eval | Custom method in Where | Move to post-materialization |
| Cartesian | Multiple Include collections | AsSplitQuery or separate queries |
| Large IN | Contains(10000+ items) | Batch or temp table |
| Round-trips | SaveChanges in loop | AddRange + single SaveChanges |
