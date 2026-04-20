using EFCore10Examples.Examples;
using System;

Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
Console.WriteLine("║    .NET 10 & EF Core 10 New Features Demonstration        ║");
Console.WriteLine("╚════════════════════════════════════════════════════════════╝");

Console.WriteLine("\nNOTE: This demo requires PostgreSQL with pgvector extension.");
Console.WriteLine("Update connection string in AppDbContext.cs before running.\n");

try
{
    // 1. Demonstrate pgvector support
    PgVectorExample.DemonstratePgVector();

    // 2. Demonstrate new LINQ methods (Left/Right Join)
    NewLinqMethodsExample.DemonstrateNewLinqMethods();

    // 3. Demonstrate multiple named query filters
    NamedQueryFiltersExample.DemonstrateNamedQueryFilters();

    // 4. Demonstrate JSON data type support
    JsonDataTypeExample.DemonstrateJsonDataType();

    Console.WriteLine("\n╔════════════════════════════════════════════════════════════╗");
    Console.WriteLine("║           All Examples Completed Successfully!            ║");
    Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
}
catch (Exception ex)
{
    Console.WriteLine($"\n❌ Error: {ex.Message}");
    Console.WriteLine("\nMake sure:");
    Console.WriteLine("  1. PostgreSQL is running");
    Console.WriteLine("  2. pgvector extension is installed: CREATE EXTENSION vector;");
    Console.WriteLine("  3. Connection string is updated in AppDbContext.cs");
    Console.WriteLine($"\nFull error: {ex}");
}
