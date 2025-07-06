using System;
using System.Linq;
using App.AppCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace App.Database
{
    public static class DbInitializer
    {
        public static void Seed(AppContext context)
        {
            if (context.Categories.Any() || context.Manufacturers.Any() || context.Starships.Any() || context.Customers.Any() || context.Orders.Any() || context.OrderItems.Any())
                return; // Already seeded

            // Categories
            var cat1 = new Category { Id = Guid.NewGuid(), Name = "Fighter", Description = "Small, fast starships" };
            var cat2 = new Category { Id = Guid.NewGuid(), Name = "Freighter", Description = "Large cargo starships" };
            context.Categories.AddRange(cat1, cat2);

            // Manufacturers
            var man1 = new Manufacturer { Id = Guid.NewGuid(), Name = "Corellian Engineering", Description = "Famous starship manufacturer" };
            var man2 = new Manufacturer { Id = Guid.NewGuid(), Name = "Kuat Drive Yards", Description = "Major shipbuilder" };
            context.Manufacturers.AddRange(man1, man2);

            // Starships
            var ship1 = new Starship { Id = Guid.NewGuid(), Name = "X-Wing", Description = "Rebel Alliance fighter", Price = 1200000, CategoryId = cat1.Id, ManufacturerId = man1.Id, StockQuantity = 10 };
            var ship2 = new Starship { Id = Guid.NewGuid(), Name = "Millennium Falcon", Description = "Legendary freighter", Price = 5000000, CategoryId = cat2.Id, ManufacturerId = man1.Id, StockQuantity = 2 };
            var ship3 = new Starship { Id = Guid.NewGuid(), Name = "Imperial Star Destroyer", Description = "Empire's capital ship", Price = 150000000, CategoryId = cat2.Id, ManufacturerId = man2.Id, StockQuantity = 1 };
            context.Starships.AddRange(ship1, ship2, ship3);

            // Customers
            var cust1 = new Customer { Id = Guid.NewGuid(), Name = "Luke Skywalker", Email = "luke@rebels.com", Address = "Tatooine" };
            var cust2 = new Customer { Id = Guid.NewGuid(), Name = "Han Solo", Email = "han@falcon.com", Address = "Corellia" };
            context.Customers.AddRange(cust1, cust2);

            // Orders
            var order1 = new Order { Id = Guid.NewGuid(), CustomerId = cust1.Id, OrderDate = DateTime.UtcNow.AddDays(-10), TotalAmount = 1200000 };
            var order2 = new Order { Id = Guid.NewGuid(), CustomerId = cust2.Id, OrderDate = DateTime.UtcNow.AddDays(-5), TotalAmount = 5000000 };
            context.Orders.AddRange(order1, order2);

            // OrderItems
            var oi1 = new OrderItem { Id = Guid.NewGuid(), OrderId = order1.Id, StarshipId = ship1.Id, Quantity = 1, UnitPrice = 1200000 };
            var oi2 = new OrderItem { Id = Guid.NewGuid(), OrderId = order2.Id, StarshipId = ship2.Id, Quantity = 1, UnitPrice = 5000000 };
            context.OrderItems.AddRange(oi1, oi2);

            context.SaveChanges();
        }

        // Call this in Program.cs after app.MapControllers()
        public static void EnsureSeedData(this IHost app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<App.Database.AppContext>();
            context.Database.Migrate(); // Ensure DB is up to date
            Seed(context);
        }
    }
}
