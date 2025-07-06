using System;
using System.Collections.Generic;

namespace App.AppCore.Models
{
    public class Starship
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
        public Guid ManufacturerId { get; set; }
        public Manufacturer? Manufacturer { get; set; }
        public int StockQuantity { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
