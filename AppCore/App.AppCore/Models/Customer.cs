using System;
using System.Collections.Generic;

namespace App.AppCore.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
