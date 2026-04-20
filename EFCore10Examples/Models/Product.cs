using Pgvector;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace EFCore10Examples.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        // EF Core 10: pgvector support for AI/ML embeddings
        public Vector? Embedding { get; set; }

        // EF Core 10: JSON column support
        public JsonDocument? Metadata { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
