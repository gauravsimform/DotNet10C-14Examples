using EFCore10Examples.Data;
using EFCore10Examples.Models;
using Microsoft.EntityFrameworkCore;
using Pgvector;
using Pgvector.EntityFrameworkCore;
using System;
using System.Linq;

namespace EFCore10Examples.Examples
{
    public static class PgVectorExample
    {
        public static void DemonstratePgVector()
        {
            Console.WriteLine("\n=== EF Core 10: pgvector Support ===");
            Console.WriteLine("Demonstrating vector similarity search for AI/ML applications\n");

            using var context = new AppDbContext();

            // Ensure database is created
            context.Database.EnsureCreated();

            // Enable pgvector extension
            context.Database.ExecuteSqlRaw("CREATE EXTENSION IF NOT EXISTS vector");

            // Create sample products with embeddings
            var products = new[]
            {
                new Product
                {
                    Name = "Laptop",
                    Description = "High-performance laptop for developers",
                    Price = 1299.99m,
                    Embedding = CreateSampleEmbedding(1536, 0.1f)
                },
                new Product
                {
                    Name = "Mouse",
                    Description = "Ergonomic wireless mouse",
                    Price = 29.99m,
                    Embedding = CreateSampleEmbedding(1536, 0.5f)
                },
                new Product
                {
                    Name = "Keyboard",
                    Description = "Mechanical keyboard with RGB lighting",
                    Price = 149.99m,
                    Embedding = CreateSampleEmbedding(1536, 0.15f)
                }
            };

            // Clear existing data
            context.Products.RemoveRange(context.Products);
            context.SaveChanges();

            // Add products
            context.Products.AddRange(products);
            context.SaveChanges();

            Console.WriteLine("✓ Added products with vector embeddings");

            // EF Core 10: Vector similarity search using cosine distance
            var queryEmbedding = CreateSampleEmbedding(1536, 0.12f);

            // Find similar products using vector similarity
            var similarProducts = context.Products
                .OrderBy(p => p.Embedding!.CosineDistance(queryEmbedding))
                .Take(2)
                .Select(p => new { p.Name, p.Description, p.Price })
                .ToList();

            Console.WriteLine("\nTop 2 similar products (using cosine distance):");
            foreach (var product in similarProducts)
            {
                Console.WriteLine($"  - {product.Name}: {product.Description} (${product.Price})");
            }

            // You can also use other distance metrics
            var l2Products = context.Products
                .OrderBy(p => p.Embedding!.L2Distance(queryEmbedding))
                .Take(2)
                .Select(p => new { p.Name })
                .ToList();

            Console.WriteLine("\nUsing L2 (Euclidean) distance:");
            foreach (var product in l2Products)
            {
                Console.WriteLine($"  - {product.Name}");
            }
        }

        private static Vector CreateSampleEmbedding(int dimensions, float seed)
        {
            var random = new Random((int)(seed * 1000));
            var values = new float[dimensions];
            for (int i = 0; i < dimensions; i++)
            {
                values[i] = (float)random.NextDouble() * seed;
            }
            return new Vector(values);
        }
    }
}
