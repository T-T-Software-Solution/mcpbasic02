using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.AppCore.Models.Dtos;

namespace App.AppCore.Interfaces
{
    public interface IStarshipService
    {
        Task<IEnumerable<StarshipDto>> GetAllAsync();
        Task<StarshipDto?> GetByIdAsync(Guid id);
        Task<StarshipDto> CreateAsync(StarshipDto starship);
        Task UpdateAsync(StarshipDto starship);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<StarshipDto>> SearchByManufacturerNameAsync(string manufacturerName);
    }
}
