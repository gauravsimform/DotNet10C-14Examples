# EF Core 10 Examples

A comprehensive demonstration of new features in Entity Framework Core 10 with .NET 10 and PostgreSQL.

## Features Demonstrated

### 1. **pgvector Support**
Store and query vector embeddings for AI/ML applications directly in PostgreSQL.

**Use Cases:**
- Semantic search
- Similarity matching
- AI/ML embeddings storage
- Recommendation systems

**Example:**
```csharp
// Store product with embedding
var product = new Product
{
    Name = "Laptop",
    Embedding = new Vector(embeddingArray)
};

// Find similar products using cosine similarity
var similar = context.Products
    .OrderBy(p => p.Embedding.CosineDistance(queryEmbedding))
    .Take(10)
    .ToList();
```

### 2. **New LINQ Methods - Left/Right Join**
Native support for LEFT JOIN and RIGHT JOIN operations.

**Before EF Core 10:**
```csharp
// Complex GroupJoin + SelectMany pattern
var result = from c in context.Customers
             join o in context.Orders on c.Id equals o.CustomerId into orders
             from o in orders.DefaultIfEmpty()
             select new { c.Name, o?.OrderNumber };
```

**With EF Core 10:**
```csharp
// Clean and intuitive
var result = context.Customers
    .LeftJoin(context.Orders, 
        c => c.Id, 
        o => o.CustomerId,
        (c, o) => new { c.Name, o?.OrderNumber });
```

### 3. **Multiple Named Query Filters**
Apply multiple query filters and selectively ignore specific ones.

**Use Cases:**
- Soft delete + multi-tenancy
- Role-based filtering
- Time-based filtering (active/archived)

**Example:**
```csharp
// Define multiple named filters
modelBuilder.Entity<Customer>()
    .HasQueryFilter("IsNotDeleted", c => !c.IsDeleted)
    .HasQueryFilter("TenantFilter", c => c.TenantId == currentTenantId);

// Selectively ignore specific filters
var allTenants = context.Customers
    .IgnoreQueryFilter("TenantFilter")  // Show all tenants
    .ToList();

var includingDeleted = context.Customers
    .IgnoreQueryFilter("IsNotDeleted")  // Show deleted records
    .ToList();
```

### 4. **JSON Data Type Support**
Native JSON column support with PostgreSQL JSONB.

**Features:**
- Store complex nested JSON data
- Query JSON properties efficiently
- Update JSON fields
- Index JSON properties

**Example:**
```csharp
// Store product with JSON metadata
var product = new Product
{
    Name = "Laptop",
    Metadata = JsonDocument.Parse(@"{
        ""specs"": { ""cpu"": ""i9"", ""ram"": ""32GB"" },
        ""tags"": [""gaming"", ""portable""]
    }")
};

// Query JSON properties
var intelProducts = context.Products
    .Where(p => p.Metadata.RootElement
        .GetProperty("specs")
        .GetProperty("cpu")
        .GetString().Contains("i9"))
    .ToList();
```

## Project Structure

```
EFCore10Examples/
├── Models/
│   ├── Product.cs           # Product with vector embeddings and JSON
│   ├── Customer.cs          # Customer with query filters
│   └── Order.cs             # Order entity
├── Data/
│   └── AppDbContext.cs      # DbContext with configurations
├── Examples/
│   ├── PgVectorExample.cs              # Vector similarity search
│   ├── NewLinqMethodsExample.cs        # Left/Right join demos
│   ├── NamedQueryFiltersExample.cs     # Multiple named filters
│   └── JsonDataTypeExample.cs          # JSON column operations
├── Program.cs               # Entry point
└── README.md                # This file
```

## Prerequisites

### Required Software
1. **.NET 10 SDK** or later
2. **PostgreSQL 14+** with pgvector extension
3. **Visual Studio 2026** (18.4.1) or later

### PostgreSQL Setup

1. Install PostgreSQL:
```bash
# Windows (using Chocolatey)
choco install postgresql

# macOS (using Homebrew)
brew install postgresql

# Linux (Ubuntu/Debian)
sudo apt-get install postgresql
```

2. Install pgvector extension:
```sql
-- Connect to your database
psql -U postgres

-- Create extension
CREATE EXTENSION vector;
```

3. Create database:
```sql
CREATE DATABASE efcore10demo;
```

## Configuration

### Update Connection String

Edit `Data/AppDbContext.cs` and update the connection string:

```csharp
optionsBuilder.UseNpgsql(
    "Host=localhost;Database=efcore10demo;Username=YOUR_USERNAME;Password=YOUR_PASSWORD",
    npgsqlOptions => npgsqlOptions.UseVector());
```

## Getting Started

1. **Clone and navigate to the project:**
```bash
cd EFCore10Examples
```

2. **Restore packages:**
```bash
dotnet restore
```

3. **Update connection string** in `AppDbContext.cs`

4. **Run the project:**
```bash
dotnet run
```

## NuGet Packages Used

- `Microsoft.EntityFrameworkCore` (10.0.0)
- `Npgsql.EntityFrameworkCore.PostgreSQL` (10.0.0)
- `Pgvector.EntityFrameworkCore` (0.3.0)
- `Microsoft.EntityFrameworkCore.Design` (10.0.0)

## Examples Output

When you run the project, you'll see demonstrations of:

1. **pgvector Support**: Vector similarity search for products
2. **LEFT/RIGHT JOIN**: Different join operations with customers and orders
3. **Named Query Filters**: Selective filter application
4. **JSON Operations**: Querying and updating JSON columns

## Key Takeaways

### pgvector Benefits
- Native vector storage in database
- Efficient similarity searches
- Multiple distance metrics (cosine, L2, inner product)
- Perfect for AI/ML applications

### Left/Right Join Benefits
- More intuitive syntax
- Better performance than manual GroupJoin
- SQL-like LINQ expressions

### Named Query Filters Benefits
- Fine-grained control over filters
- Better multi-tenancy support
- Cleaner code organization

### JSON Support Benefits
- Flexible schema
- Rich query capabilities
- Better performance than string serialization
- Native PostgreSQL JSONB indexing

## Troubleshooting

### Common Issues

**Error: "extension vector does not exist"**
```sql
-- Solution: Install pgvector extension
CREATE EXTENSION vector;
```

**Error: "Connection refused"**
- Ensure PostgreSQL is running
- Check connection string credentials
- Verify PostgreSQL is listening on port 5432

**Error: "relation does not exist"**
- Database will be created automatically
- Ensure you have CREATE privileges

## Learning Resources

- [EF Core 10 Documentation](https://learn.microsoft.com/en-us/ef/core/)
- [pgvector GitHub](https://github.com/pgvector/pgvector)
- [PostgreSQL JSONB Documentation](https://www.postgresql.org/docs/current/datatype-json.html)

## License

This project is for educational purposes.

## Contributing

Feel free to add more examples or improve existing ones by submitting a pull request.

---

**Note:** Make sure PostgreSQL with pgvector extension is installed and running before executing the examples.
