# C# 14 Examples

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
C#14Examples/
├── Program.cs                    # Entry point demonstrating all features
├── ExtensionMembers.cs           # Extension members syntax
├── NullConditionalAssignment.cs  # Null-conditional assignment operator
├── FieldKeyword.cs               # ''field'' keyword in properties
├── PartialMembers.cs             # Enhanced partial members
└── README.md                     # This file
```

## Requirements

- **.NET 10** or later
- **Visual Studio 2026** (18.4.1) or later
- **C# 14** language version

## Getting Started

1. Clone or download this repository
2. Open the solution in Visual Studio 2026 or later
3. Build and run the project
4. Review the console output to see each feature in action
5. Explore individual files to understand the implementation details

## Running the Examples

Simply run the project. The `Program.cs` file executes demonstrations of all features:

```csharp
ExtensionMembers.ExtensionDemo();
NullConditionalAssignment.NullConditionDemo();
FieldKeyword.FieldKeywordDemo();
PartialMembers.PartialMembersDemo();
```

## Key Takeaways

- **Extension Members**: More organized and readable extension method definitions
- **Null-Conditional Assignment**: Safer, more concise null-checked assignments
- **`field` Keyword**: Simplified property implementations with less boilerplate
- **Partial Members**: Greater flexibility in partial class/method design

## Learning Resources

- [C# 14 Documentation](https://learn.microsoft.com/en-us/dotnet/csharp/)
- [What''s New in C# 14](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-14)

## License

This project is for educational purposes.

## Contributing

Feel free to add more examples or improve existing ones by submitting a pull request.

---

**Note:** C# 14 is part of .NET 10. Ensure your development environment supports these versions to run this project successfully.
