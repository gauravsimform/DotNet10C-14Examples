using System;
using System.ComponentModel.DataAnnotations;

namespace EFCore10Examples.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public string OrderNumber { get; set; } = string.Empty;

        public int? CustomerId { get; set; }

        public Customer? Customer { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;

        public string? TenantId { get; set; }
    }
}
