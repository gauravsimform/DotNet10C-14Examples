using EFCore10Examples.Data;
using EFCore10Examples.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.Json;

namespace EFCore10Examples.Examples
{
    public static class JsonDataTypeExample
    {
        public static void DemonstrateJsonDataType()
        {
            Console.WriteLine("\n=== EF Core 10: JSON Data Type Support ===");
            Console.WriteLine("Demonstrating native JSON column support with PostgreSQL\n");

            using var context = new AppDbContext();
            context.Database.EnsureCreated();

            // Clear existing products
            context.Products.RemoveRange(context.Products);
            context.SaveChanges();

            // Create products with JSON metadata
            var products = new[]
            {
                new Product
                {
                    Name = "Gaming Laptop",
                    Description = "High-end gaming laptop",
                    Price = 2499.99m,
                    Metadata = JsonDocument.Parse(@"{
                        ""specifications"": {
                            ""cpu"": ""Intel i9-13900HX"",
                            ""gpu"": ""NVIDIA RTX 4090"",
                            ""ram"": ""32GB DDR5"",
                            ""storage"": ""2TB NVMe SSD""
                        },
                        ""features"": [""RGB Keyboard"", ""144Hz Display"", ""WiFi 6E""],
                        ""warranty"": {
                            ""years"": 3,
                            ""type"": ""Premium""
                        },
                        ""tags"": [""gaming"", ""portable"", ""high-performance""]
                    }")
                },
                new Product
                {
                    Name = "Ultrabook",
                    Description = "Lightweight business laptop",
                    Price = 1299.99m,
                    Metadata = JsonDocument.Parse(@"{
                        ""specifications"": {
                            ""cpu"": ""Intel i7-1360P"",
                            ""gpu"": ""Intel Iris Xe"",
                            ""ram"": ""16GB LPDDR5"",
                            ""storage"": ""512GB NVMe SSD""
                        },
                        ""features"": [""Thunderbolt 4"", ""Long Battery Life"", ""Fingerprint Reader""],
                        ""warranty"": {
                            ""years"": 2,
                            ""type"": ""Standard""
                        },
                        ""tags"": [""business"", ""lightweight"", ""portable""]
                    }")
                },
                new Product
                {
                    Name = "Budget Laptop",
                    Description = "Affordable everyday laptop",
                    Price = 599.99m,
                    Metadata = JsonDocument.Parse(@"{
                        ""specifications"": {
                            ""cpu"": ""AMD Ryzen 5 5500U"",
                            ""gpu"": ""Integrated"",
                            ""ram"": ""8GB DDR4"",
                            ""storage"": ""256GB SSD""
                        },
                        ""features"": [""WiFi 6"", ""USB-C""],
                        ""warranty"": {
                            ""years"": 1,
                            ""type"": ""Basic""
                        },
                        ""tags"": [""budget"", ""everyday""]
                    }")
                }
            };

            context.Products.AddRange(products);
            context.SaveChanges();

            Console.WriteLine("✓ Created products with JSON metadata\n");

            // Query products with specific CPU (client-side evaluation for demonstration)
            Console.WriteLine("1. Query products with specific CPU:");

            var allProducts = context.Products.ToList();
            var intelProducts = allProducts
                .Where(p => p.Metadata != null &&
                           p.Metadata.RootElement.GetProperty("specifications")
                               .GetProperty("cpu").GetString()!.Contains("Intel"))
                .Select(p => new
                {
                    p.Name,
                    CPU = p.Metadata!.RootElement.GetProperty("specifications").GetProperty("cpu").GetString()
                })
                .ToList();

            foreach (var product in intelProducts)
            {
                Console.WriteLine($"   - {product.Name}: {product.CPU}");
            }

            // Query by JSON array contains
            Console.WriteLine("\n2. Products with 'portable' tag:");

            var portableProducts = allProducts
                .Where(p => p.Metadata != null &&
                           p.Metadata.RootElement.TryGetProperty("tags", out var tags) &&
                           tags.EnumerateArray().Any(t => t.GetString() == "portable"))
                .Select(p => new
                {
                    p.Name,
                    Tags = string.Join(", ", p.Metadata!.RootElement.GetProperty("tags")
                        .EnumerateArray().Select(t => t.GetString()))
                })
                .ToList();

            foreach (var product in portableProducts)
            {
                Console.WriteLine($"   - {product.Name}: [{product.Tags}]");
            }

            // Query by nested JSON property
            Console.WriteLine("\n3. Products with warranty >= 2 years:");

            var warrantyProducts = allProducts
                .Where(p => p.Metadata != null &&
                           p.Metadata.RootElement.GetProperty("warranty").GetProperty("years").GetInt32() >= 2)
                .Select(p => new
                {
                    p.Name,
                    WarrantyYears = p.Metadata!.RootElement.GetProperty("warranty").GetProperty("years").GetInt32(),
                    WarrantyType = p.Metadata!.RootElement.GetProperty("warranty").GetProperty("type").GetString()
                })
                .ToList();

            foreach (var product in warrantyProducts)
            {
                Console.WriteLine($"   - {product.Name}: {product.WarrantyYears} years ({product.WarrantyType})");
            }

            // Display full metadata for one product
            Console.WriteLine("\n4. Full JSON metadata example (Gaming Laptop):");
            var gamingLaptop = context.Products.First(p => p.Name == "Gaming Laptop");
            var jsonString = gamingLaptop.Metadata!.RootElement.ToString();
            var formattedJson = JsonSerializer.Serialize(
                JsonSerializer.Deserialize<JsonElement>(jsonString),
                new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine($"   {formattedJson}");

            // Update JSON property
            Console.WriteLine("\n5. Updating JSON property:");
            var budgetLaptop = context.Products.First(p => p.Name == "Budget Laptop");

            // Update warranty years
            var updatedMetadata = JsonDocument.Parse(@"{
                ""specifications"": {
                    ""cpu"": ""AMD Ryzen 5 5500U"",
                    ""gpu"": ""Integrated"",
                    ""ram"": ""8GB DDR4"",
                    ""storage"": ""256GB SSD""
                },
                ""features"": [""WiFi 6"", ""USB-C""],
                ""warranty"": {
                    ""years"": 2,
                    ""type"": ""Extended""
                },
                ""tags"": [""budget"", ""everyday""]
            }");

            budgetLaptop.Metadata = updatedMetadata;
            context.SaveChanges();

            Console.WriteLine($"   ✓ Updated Budget Laptop warranty to 2 years (Extended)");
        }
    }
}
