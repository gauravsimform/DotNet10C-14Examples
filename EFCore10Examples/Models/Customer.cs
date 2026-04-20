using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFCore10Examples.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;

        public string? TenantId { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
