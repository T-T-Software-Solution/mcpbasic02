# App.Mcp

App.Mcp is a Model Context Protocol (MCP) server built with ASP.NET Core. It exposes business logic and data access for starships, customers, and orders via HTTP endpoints and MCP tools.

## Features
- MCP server with HTTP transport
- Swagger/OpenAPI documentation
- Entity Framework Core integration with SQL Server
- Business logic for Starships, Orders, and Customers
- Database seeding and automatic migrations
- Modular tool classes for each domain (Starship, Order, Customer)

## Project Structure
- `Controllers/` — API controllers (e.g., HealthController)
- `Tools/` — MCP tool classes for Starship, Order, and Customer operations
- `Program.cs` — Main entry point, service registration, and pipeline configuration
- `appsettings.json` — Application configuration

## Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server instance

### Configuration
1. Update the `ConnectionStrings:DefaultConnection` in `appsettings.json` to point to your SQL Server.

### Build and Run
```powershell
# Navigate to the App.Mcp project directory
cd Presentations\App.Mcp

# Restore dependencies
 dotnet restore

# Build the project
 dotnet build

# Run the server
 dotnet run
```

The API and MCP endpoints will be available at the configured URL (see console output). Swagger UI is enabled in development mode at `/swagger`.

### Database
- On startup, the application applies any pending migrations and seeds the database with initial data.

## MCP Tools
The following tool classes are available:
- `CustomerTools` — CRUD and search operations for customers
- `OrderTools` — CRUD and search operations for orders
- `StarshipTools` — CRUD and search operations for starships

Each tool is decorated with `[McpServerToolType]` and exposes methods for use via MCP clients.

## Extending
- Add new tools by creating classes in the `Tools/` directory and decorating them with `[McpServerToolType]`.
- Register new services in `Program.cs` as needed.

## License
This project is for demonstration and educational purposes.
