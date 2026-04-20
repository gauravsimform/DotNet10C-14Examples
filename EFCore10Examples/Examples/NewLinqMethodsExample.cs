using EFCore10Examples.Data;
using EFCore10Examples.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EFCore10Examples.Examples
{
    public static class NewLinqMethodsExample
    {
        public static void DemonstrateNewLinqMethods()
        {
            Console.WriteLine("\n=== EF Core 10: New LINQ Join Methods ===");
            Console.WriteLine("Demonstrating new LeftJoin() and RightJoin() methods\n");

            using var context = new AppDbContext();
            context.Database.EnsureCreated();

            // Seed data
            SeedData(context);

            Console.WriteLine("Sample Data:");
            Console.WriteLine("  Customers: Alice, Bob, Charlie");
            Console.WriteLine("  Orders: Only Alice and Bob have orders\n");

            // LEFT JOIN pattern - Get all customers and their orders (including customers without orders)
            Console.WriteLine("LEFT JOIN Pattern: All customers with their orders (null for customers without orders)");

            var leftJoinResults = context.Customers
                .LeftJoin(
                    context.Orders,
                    customer => customer.Id,
                    order => order.CustomerId,
                     (customer, order) => new
                     {
                         CustomerName = customer != null ? customer.Name : "Unknown Customer",
                         OrderNumber = order != null ? order.OrderNumber : "No Order",
                         TotalAmount = order != null ? order.TotalAmount : 0
                     })
                .ToList();

            foreach (var result in leftJoinResults)
            {
                Console.WriteLine($"  Customer: {result.CustomerName}, Order: {result.OrderNumber}, Amount: ${result.TotalAmount}");
            }

            // RIGHT JOIN - All orders with their customers
            Console.WriteLine("\nRIGHT JOIN (NEW in EF Core 10): All orders with their customers");

            var rightJoinResults = context.Customers
                .RightJoin(
                    context.Orders,
                    customer => customer.Id,
                    order => order.CustomerId,
                    (customer, order) => new
                    {
                        CustomerName = customer != null ? customer.Name : "Unknown Customer",
                        OrderNumber =  order != null ? order.OrderNumber : "No Order",
                        TotalAmount = order != null ? order.TotalAmount : 0
                    })
                .ToList();

            foreach (var result in rightJoinResults)
            {
                Console.WriteLine($"  Customer: {result.CustomerName}, Order: {result.OrderNumber}, Amount: ${result.TotalAmount}");
            }

            // Traditional INNER JOIN (for comparison)
            Console.WriteLine("\nINNER JOIN (traditional): Only customers with orders");

            var innerJoinResults = context.Customers
                .Join(
                    context.Orders,
                    customer => customer.Id,
                    order => order.CustomerId,
                    (customer, order) => new
                    {
                        CustomerName = customer.Name,
                        OrderNumber = order.OrderNumber,
                        TotalAmount = order.TotalAmount
                    })
                .ToList();

            foreach (var result in innerJoinResults)
            {
                Console.WriteLine($"  Customer: {result.CustomerName}, Order: {result.OrderNumber}, Amount: ${result.TotalAmount}");
            }
        }

        private static void SeedData(AppDbContext context)
        {
            // Clear existing data - ignore query filters temporarily
            var customersToRemove = context.Customers.IgnoreQueryFilters().ToList();
            var ordersToRemove = context.Orders.IgnoreQueryFilters().ToList();

            context.Orders.RemoveRange(ordersToRemove);
            context.Customers.RemoveRange(customersToRemove);
            context.SaveChanges();

            // Add customers
            var customers = new[]
            {
                new Customer { Id = 1, Name = "Alice", Email = "alice@example.com", TenantId = "tenant1" },
                new Customer { Id = 2, Name = "Bob", Email = "bob@example.com", TenantId = "tenant1" },
                new Customer { Id = 3, Name = "Charlie", Email = "charlie@example.com", TenantId = "tenant1" }
            };

            context.Customers.AddRange(customers);
            context.SaveChanges();

            // Add orders (only for Alice and Bob)
            var orders = new[]
            {
                new Order { OrderNumber = "ORD-001", CustomerId = 1, TotalAmount = 299.99m, TenantId = "tenant1" },
                new Order { OrderNumber = "ORD-002", CustomerId = 1, TotalAmount = 149.99m, TenantId = "tenant1" },
                new Order { OrderNumber = "ORD-003", CustomerId = 2, TotalAmount = 599.99m, TenantId = "tenant1" }
            };

            context.Orders.AddRange(orders);
            context.SaveChanges();
        }
    }
}
