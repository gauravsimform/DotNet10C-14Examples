# .NET 10 & C# 14 Examples

A comprehensive demonstration repository showcasing new features in .NET 10, C# 14, and Entity Framework Core 10.

## Projects

This repository contains two demonstration projects:

### 1. [C#14Examples](C%2314Examples/) - C# 14 Language Features
New language features introduced in C# 14.

### 2. [EFCore10Examples](EFCore10Examples/) - EF Core 10 & .NET 10 Features
Entity Framework Core 10 and .NET 10 data access features.

---

## C# 14 Examples

A demonstration project showcasing the new features introduced in C# 14, targeting .NET 10.

## Overview

This project contains practical examples of the key language features introduced in C# 14, making it easier to understand and adopt these new capabilities in your projects.

## Features Demonstrated

### 1. **Extension Members**
A new, cleaner syntax for defining extension methods within extension blocks.

**File:** `ExtensionMembers.cs`

**What''s New:**
- Use `extension(type)` syntax to group related extension methods
- More intuitive and organized way to extend existing types
- Example: Creating a `ToPalindrome()` extension for strings

```csharp
extension(string str)
{
    public string ToPalindrome()
    {
        // Implementation
    }
}
```

### 2. **Null-Conditional Assignment (`?.=`)**
Simplifies null-checked assignments with a more concise syntax.

**File:** `NullConditionalAssignment.cs`

**What''s New:**
- Assign values only if the object is not null
- Reduces boilerplate null checks
- Example: `user?.Name = "Bob";` only assigns if `user` is not null

**Before C# 14:**
```csharp
if (user != null)
{
    user.Name = "Alice";
}
```

**With C# 14:**
```csharp
user?.Name = "Bob";
```

### 3. **`field` Keyword in Property Setters**
Access the backing field directly in property accessors without explicitly declaring it.

**File:** `FieldKeyword.cs`

**What''s New:**
- Use `field` keyword to reference the auto-generated backing field
- Eliminates the need to manually declare private backing fields
- Cleaner property implementations with custom logic

**Before C# 14:**
```csharp
private string _cityName = string.Empty;
public string CityName
{
    get => _cityName;
    set => _cityName = value;
}
```

**With C# 14:**
```csharp
public string CityName
{
    get;
    set => field = value;  // ''field'' references the backing field
}
```

### 4. **Partial Members**
Enhanced partial methods with accessibility modifiers and return types.

**File:** `PartialMembers.cs`

**What''s New:**
- Partial methods can now have public/private accessibility
- Can return values (not just void)
- Better support for code generation scenarios

**Example:**
```csharp
public partial class Calculator
{
    public partial string Log(string message);  // Declaration
}

public partial class Calculator
{
    public partial string Log(string message)   // Implementation
    {
        Console.WriteLine($"Log: {message}");
        return message;
    }
}
```

## Project Structure

```
.NET10-CSharp14-Examples/
│
├── C#14Examples/                 # C# 14 Language Features
│   ├── Program.cs                # Entry point demonstrating all features
│   ├── ExtensionMembers.cs       # Extension members syntax
│   ├── NullConditionalAssignment.cs  # Null-conditional assignment
│   ├── FieldKeyword.cs           # 'field' keyword in properties
│   ├── PartialMembers.cs         # Enhanced partial members
│   └── README.md
│
├── EFCore10Examples/             # EF Core 10 & .NET 10 Features
│   ├── Models/
│   │   ├── Product.cs            # Vector embeddings & JSON support
│   │   ├── Customer.cs           # Query filter examples
│   │   └── Order.cs              # Relationship examples
│   ├── Data/
│   │   └── AppDbContext.cs       # DbContext with new features
│   ├── Examples/
│   │   ├── PgVectorExample.cs    # pgvector demonstrations
│   │   ├── NewLinqMethodsExample.cs     # Left/Right join
│   │   ├── NamedQueryFiltersExample.cs  # Multiple named filters
│   │   └── JsonDataTypeExample.cs       # JSON column support
│   ├── Program.cs
│   └── README.md
│
└── README.md                     # This file
```

## Requirements

- **.NET 10** or later
- **Visual Studio 2026** (18.4.1) or later
- **C# 14** language version
- **PostgreSQL 14+** (for EFCore10Examples with pgvector extension)

## Getting Started

### C# 14 Examples
1. Navigate to the `C#14Examples` folder
2. Open the solution in Visual Studio 2026 or later
3. Build and run the project
4. Review the console output to see each feature in action

### EF Core 10 Examples
1. Install PostgreSQL and pgvector extension
   ```sql
   CREATE EXTENSION vector;
   ```
2. Navigate to the `EFCore10Examples` folder
3. Update the connection string in `Data/AppDbContext.cs`
4. Build and run the project
5. Review the console output demonstrating EF Core 10 features

For detailed instructions, see the README.md file in each project folder.

## Running the Examples

### C# 14 Features
```bash
cd C#14Examples
dotnet run
```

The `Program.cs` file executes demonstrations of all C# 14 features:
```csharp
ExtensionMembers.ExtensionDemo();
NullConditionalAssignment.NullConditionDemo();
FieldKeyword.FieldKeywordDemo();
PartialMembers.PartialMembersDemo();
```

### EF Core 10 Features
```bash
cd EFCore10Examples
dotnet run
```

Demonstrates:
- pgvector support for vector similarity search
- New LINQ methods (LEFT JOIN, RIGHT JOIN)
- Multiple named query filters
- JSON data type support with PostgreSQL

## Key Takeaways

### C# 14 Features
- **Extension Members**: More organized and readable extension method definitions
- **Null-Conditional Assignment**: Safer, more concise null-checked assignments
- **`field` Keyword**: Simplified property implementations with less boilerplate
- **Partial Members**: Greater flexibility in partial class/method design

### EF Core 10 & .NET 10 Features
- **pgvector Support**: Store and query AI/ML embeddings directly in PostgreSQL
- **LEFT/RIGHT JOIN**: Native LINQ support for outer joins
- **Named Query Filters**: Apply multiple filters and selectively ignore specific ones
- **JSON Support**: Native JSONB column support with rich query capabilities

## Learning Resources

### C# 14
- [C# 14 Documentation](https://learn.microsoft.com/en-us/dotnet/csharp/)
- [What's New in C# 14](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-14)

### EF Core 10 & .NET 10
- [EF Core 10 Documentation](https://learn.microsoft.com/en-us/ef/core/)
- [What's New in EF Core 10](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-10.0/whatsnew)
- [pgvector GitHub Repository](https://github.com/pgvector/pgvector)
- [PostgreSQL JSONB Documentation](https://www.postgresql.org/docs/current/datatype-json.html)

## License

This project is for educational purposes.

## Contributing

Feel free to add more examples or improve existing ones by submitting a pull request.

---

**Note:** This repository demonstrates features from .NET 10, C# 14, and EF Core 10. Ensure your development environment supports these versions. The EFCore10Examples project requires PostgreSQL with the pgvector extension installed.
