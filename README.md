# MCPBasic02 Project

This repository is a modular .NET 8 solution demonstrating a Model Context Protocol (MCP) architecture for business applications. It features a clean separation of concerns across core logic, infrastructure, and presentation layers, and provides both API and console-based interfaces for interacting with business data and AI-powered tools.

## Solution Structure

- **AppCore/**
  - `App.AppCore/` — Core business logic, domain models, DTOs, and service interfaces.
    - `Models/` — Domain entities and DTOs (e.g., Customer, Order, Starship)
    - `Interfaces/` — Service contracts for business logic
    - `Applications/` — (Reserved for application services)
    - [App.AppCore README](AppCore/App.AppCore/README.md)

- **Infra/**
  - `App.Database/` — Infrastructure layer for data access using Entity Framework Core.
    - `AppContext.cs` — EF Core DbContext
    - `Migrations/` — Database migrations
    - `Services/` — Implementations of business services
    - `DbInitializer.cs` — Database seeding logic
    - [App.Database README](Infra/App.Database/README.md)

- **Presentations/**
  - `App.Mcp/` — ASP.NET Core MCP server exposing business logic and tools via HTTP and MCP endpoints.
    - `Controllers/` — API controllers
    - `Tools/` — MCP tool classes for Starship, Order, and Customer operations
    - `Program.cs` — Service registration and pipeline configuration
    - `appsettings.json` — Configuration
    - [App.Mcp README](Presentations/App.Mcp/README.md)
  - `App.Console/` — Console client integrating Azure OpenAI and MCP server for interactive AI chat with tool augmentation.
    - `Program.cs` — Main entry point and chat loop
    - `appsettings.json` — Configuration
    - [App.Console README](Presentations/App.Console/README.md)

## Key Features
- **MCP Server**: Exposes business logic as discoverable tools via HTTP and MCP protocol
- **Entity Framework Core**: SQL Server-backed data access with migrations and seeding
- **Azure OpenAI Integration**: Console client uses LLM for natural language interaction, augmented by MCP tools
- **Modular Design**: Clear separation of core, infrastructure, and presentation layers

## Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server instance
- Azure OpenAI resource for chat client

### Setup
1. Clone the repository
2. Configure connection strings and secrets in each project's `appsettings.json` and user secrets as needed
3. Build and run the MCP server (`App.Mcp`) to apply migrations and seed the database
4. Run the console client (`App.Console`) for interactive AI chat

### Build and Run Example
```powershell
# Build the solution
 dotnet build mcpbasic02.sln

# Run the MCP server
 cd Presentations\App.Mcp
 dotnet run

# In a new terminal, run the console client
 cd Presentations\App.Console
 dotnet run
```

## Documentation
- See each project's `README.md` for details on configuration, usage, and features (see links above).
- MCP tool classes are in `Presentations/App.Mcp/Tools/`.

## License
This project is for demonstration and educational purposes.
