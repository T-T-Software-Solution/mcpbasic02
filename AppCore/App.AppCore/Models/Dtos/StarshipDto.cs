using System;

namespace App.AppCore.Models.Dtos
{
    public class StarshipDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public Guid ManufacturerId { get; set; }
        public int StockQuantity { get; set; }
    }
}
