using App.AppCore.Models;
using App.AppCore.Models.Dtos;
using App.AppCore.Interfaces;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace App.Mcp.Tools;

[McpServerToolType]
public class StarshipTools
{
    private readonly IStarshipService _starshipService;
    public StarshipTools(IStarshipService starshipService)
    {
        _starshipService = starshipService;
    }

    [McpServerTool, Description("Get all starships")]
    public async Task<IEnumerable<StarshipDto>> GetAllStarships() => await _starshipService.GetAllAsync();

    [McpServerTool, Description("Get a starship by Id")]
    public async Task<StarshipDto?> GetStarshipById(Guid id) => await _starshipService.GetByIdAsync(id);

    [McpServerTool, Description("Create a new starship")]
    public async Task<StarshipDto> CreateStarship(
        [Description("Starship name")] string name,
        [Description("Description")] string description,
        [Description("Price")] decimal price,
        [Description("Category Id")] Guid categoryId,
        [Description("Manufacturer Id")] Guid manufacturerId,
        [Description("Stock quantity")] int stockQuantity)
    {
        var starshipDto = new StarshipDto
        {
            Name = name,
            Description = description,
            Price = price,
            CategoryId = categoryId,
            ManufacturerId = manufacturerId,
            StockQuantity = stockQuantity
        };
        return await _starshipService.CreateAsync(starshipDto);
    }

    [McpServerTool, Description("Update a starship")]
    public async Task UpdateStarship(
        [Description("Starship Id")] Guid id,
        [Description("Starship name")] string name,
        [Description("Description")] string description,
        [Description("Price")] decimal price,
        [Description("Category Id")] Guid categoryId,
        [Description("Manufacturer Id")] Guid manufacturerId,
        [Description("Stock quantity")] int stockQuantity)
    {
        var starshipDto = new StarshipDto
        {
            Id = id,
            Name = name,
            Description = description,
            Price = price,
            CategoryId = categoryId,
            ManufacturerId = manufacturerId,
            StockQuantity = stockQuantity
        };
        await _starshipService.UpdateAsync(starshipDto);
    }

    [McpServerTool, Description("Delete a starship")]
    public async Task DeleteStarship(Guid id) => await _starshipService.DeleteAsync(id);
}
