using EFCore10Examples.Data;
using EFCore10Examples.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EFCore10Examples.Examples
{
    public static class NamedQueryFiltersExample
    {
        public static void DemonstrateNamedQueryFilters()
        {
            Console.WriteLine("\n=== EF Core 10: Query Filter Management ===");
            Console.WriteLine("Demonstrating flexible query filter control\n");

            using var context = new AppDbContext();
            context.Database.EnsureCreated();

            // Set current tenant
            context.CurrentTenantId = "tenant1";

            // Seed data
            SeedData(context);

            Console.WriteLine("Sample Data Created:");
            Console.WriteLine("  - 2 Customers in tenant1 (1 active, 1 deleted)");
            Console.WriteLine("  - 2 Customers in tenant2 (both active)");
            Console.WriteLine($"  - Current TenantId: {context.CurrentTenantId}\n");

            // Default: All query filters are applied
            Console.WriteLine("1. Default (all filters active):");
            var defaultCustomers = context.Customers.ToList();
            Console.WriteLine($"   Found {defaultCustomers.Count} customer(s)");
            foreach (var customer in defaultCustomers)
            {
                Console.WriteLine($"   - {customer.Name} (TenantId: {customer.TenantId}, Deleted: {customer.IsDeleted})");
            }

            // Ignore all query filters
            Console.WriteLine("\n2. Ignore all filters:");
            var allCustomers = context.Customers
                .IgnoreQueryFilters()
                .ToList();
            Console.WriteLine($"   Found {allCustomers.Count} customer(s) total");
            foreach (var customer in allCustomers)
            {
                Console.WriteLine($"   - {customer.Name} (TenantId: {customer.TenantId}, Deleted: {customer.IsDeleted})");
            }

            // Manual filtering to show deleted customers only
            Console.WriteLine("\n3. Show only deleted customers (manual filter):");
            var deletedCustomers = context.Customers
                .IgnoreQueryFilters()
                .Where(c => c.IsDeleted)
                .ToList();
            Console.WriteLine($"   Found {deletedCustomers.Count} deleted customer(s)");
            foreach (var customer in deletedCustomers)
            {
                Console.WriteLine($"   - {customer.Name} (TenantId: {customer.TenantId})");
            }

            // Manual filtering for specific tenant
            Console.WriteLine("\n4. Show all customers from tenant2 (manual filter):");
            var tenant2Customers = context.Customers
                .IgnoreQueryFilters()
                .Where(c => c.TenantId == "tenant2")
                .ToList();
            Console.WriteLine($"   Found {tenant2Customers.Count} customer(s) in tenant2");
            foreach (var customer in tenant2Customers)
            {
                Console.WriteLine($"   - {customer.Name} (Deleted: {customer.IsDeleted})");
            }

            // Demonstrate query filters with orders
            Console.WriteLine("\n5. Orders with applied filters:");
            var orders = context.Orders.ToList();
            Console.WriteLine($"   Found {orders.Count} order(s) in current tenant");
            foreach (var order in orders)
            {
                Console.WriteLine($"   - {order.OrderNumber} (CustomerId: {order.CustomerId}, TenantId: {order.TenantId})");
            }
        }

        private static void SeedData(AppDbContext context)
        {
            // Clear existing data
            var customersToRemove = context.Customers.IgnoreQueryFilters().ToList();
            var ordersToRemove = context.Orders.IgnoreQueryFilters().ToList();

            context.Orders.RemoveRange(ordersToRemove);
            context.Customers.RemoveRange(customersToRemove);
            context.SaveChanges();

            // Add customers
            var customers = new[]
            {
                new Customer { Name = "Alice (Tenant1)", Email = "alice@tenant1.com", TenantId = "tenant1", IsDeleted = false },
                new Customer { Name = "Bob (Tenant1 - Deleted)", Email = "bob@tenant1.com", TenantId = "tenant1", IsDeleted = true },
                new Customer { Name = "Charlie (Tenant2)", Email = "charlie@tenant2.com", TenantId = "tenant2", IsDeleted = false },
                new Customer { Name = "Diana (Tenant2)", Email = "diana@tenant2.com", TenantId = "tenant2", IsDeleted = false }
            };

            context.Customers.AddRange(customers);
            context.SaveChanges();

            // Add some orders
            var alice = context.Customers.IgnoreQueryFilters().First(c => c.Name == "Alice (Tenant1)");
            var orders = new[]
            {
                new Order { OrderNumber = "ORD-T1-001", CustomerId = alice.Id, TotalAmount = 100m, TenantId = "tenant1" }
            };

            context.Orders.AddRange(orders);
            context.SaveChanges();
        }
    }
}
