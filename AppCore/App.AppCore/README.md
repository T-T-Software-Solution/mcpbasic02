# App.AppCore

App.AppCore is the core business logic layer of the MCPBasic02 solution. It defines the domain models, data transfer objects (DTOs), and service interfaces that represent the application's business rules and entities. This project is referenced by both the infrastructure and presentation layers, ensuring a clean separation of concerns.

## Features
- Domain models for key business entities (e.g., Customer, Order, Starship, Category, Manufacturer)
- DTOs for safe and efficient data transfer between layers
- Service interfaces for business logic abstraction
- Designed for extensibility and testability

## Project Structure
- `Models/` — Domain entities and value objects
  - `Customer.cs`, `Order.cs`, `Starship.cs`, `Category.cs`, `Manufacturer.cs`, etc.
  - `Dtos/` — Data Transfer Objects for each entity
- `Interfaces/` — Service contracts (e.g., `ICustomerService`, `IOrderService`, `IStarshipService`, `IRepository`)
- `Applications/` — (Reserved for application services or use cases)
- `App.AppCore.csproj` — Project file and dependencies

## Usage
- This project is referenced by both the infrastructure (`App.Database`) and presentation (`App.Mcp`, `App.Console`) layers.
- All business logic and data contracts should be defined here to maintain a clean architecture.

## Extending
- Add new domain models to `Models/`
- Add new DTOs to `Models/Dtos/`
- Define new service interfaces in `Interfaces/`

## License
This project is for demonstration and educational purposes.
