using System;

namespace App.AppCore.Models.Dtos
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
