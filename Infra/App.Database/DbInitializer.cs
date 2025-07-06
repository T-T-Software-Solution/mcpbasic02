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
