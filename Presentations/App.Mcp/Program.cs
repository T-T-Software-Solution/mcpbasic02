using Microsoft.EntityFrameworkCore;
using App.Database;
using App.Database.Services;
using App.AppCore.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Register MCP server and discover tools from the current assembly
builder.Services.AddMcpServer().WithHttpTransport().WithToolsFromAssembly();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext for MSSQL
builder.Services.AddDbContext<App.Database.AppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register business services
builder.Services.AddScoped<INotificationService, NotificationService>();

// Add controller support
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Map controllers
app.MapControllers();
app.MapMcp();

// Seed database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<App.Database.AppContext>();
    App.Database.DbInitializer.Seed(context);
}

app.Run();

