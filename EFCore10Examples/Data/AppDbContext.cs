using Microsoft.EntityFrameworkCore;
using EFCore10Examples.Models;
using System.Text.Json;

namespace EFCore10Examples.Data
{
    public class AppDbContext : DbContext
    {
        public string? CurrentTenantId { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Using PostgreSQL with pgvector extension
            // Note: Update connection string with your PostgreSQL credentials
            optionsBuilder.UseNpgsql(
                "Host=localhost;Database=efcore10demo;Username=postgres;Password=postgres",
                npgsqlOptions => npgsqlOptions.UseVector());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Product entity
            modelBuilder.Entity<Product>(entity =>
            {
                // EF Core 10: Vector column for embeddings
                entity.Property(p => p.Embedding)
                    .HasColumnType("vector(1536)"); // OpenAI ada-002 embedding size

                // EF Core 10: JSON column support
                entity.Property(p => p.Metadata)
                    .HasColumnType("jsonb");

                entity.HasIndex(p => p.Embedding)
                    .HasMethod("ivfflat") // Using IVFFlat index for vector similarity search
                    .HasOperators("vector_cosine_ops");
            });

            // EF Core 10: Query filters for multi-tenancy and soft delete
            modelBuilder.Entity<Customer>(entity =>
            {
                // Combined query filter for soft delete and multi-tenancy
                entity.HasQueryFilter(c => !c.IsDeleted && c.TenantId == CurrentTenantId);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                // Combined query filter for soft delete and multi-tenancy
                entity.HasQueryFilter(o => !o.IsDeleted && o.TenantId == CurrentTenantId);

                // Configure relationships
                entity.HasOne(o => o.Customer)
                    .WithMany(c => c.Orders)
                    .HasForeignKey(o => o.CustomerId);
            });
        }
    }
}
