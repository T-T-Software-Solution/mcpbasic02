using System;
using System.Collections.Generic;

namespace App.AppCore.Models
{
    public class Manufacturer
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Starship> Starships { get; set; } = new List<Starship>();
    }
}
