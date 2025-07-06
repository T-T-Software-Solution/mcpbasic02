using System;

namespace App.AppCore.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }
        public Guid StarshipId { get; set; }
        public Starship? Starship { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
