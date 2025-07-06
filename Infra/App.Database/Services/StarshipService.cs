using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using App.AppCore.Models;
using App.AppCore.Models.Dtos;
using App.AppCore.Interfaces;
using System.Linq;

namespace App.Database.Services
{
    public class StarshipService : IStarshipService
    {
        private readonly AppContext _context;
        public StarshipService(AppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StarshipDto>> GetAllAsync() =>
            await _context.Starships.Select(s => ToDto(s)).ToListAsync();

        public async Task<StarshipDto?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Starships.FindAsync(id);
            return entity == null ? null : ToDto(entity);
        }

        public async Task<StarshipDto> CreateAsync(StarshipDto starshipDto)
        {
            var entity = FromDto(starshipDto);
            entity.Id = Guid.NewGuid();
            _context.Starships.Add(entity);
            await _context.SaveChangesAsync();
            return ToDto(entity);
        }

        public async Task UpdateAsync(StarshipDto starshipDto)
        {
            var entity = await _context.Starships.FindAsync(starshipDto.Id);
            if (entity != null)
            {
                entity.Name = starshipDto.Name;
                entity.Description = starshipDto.Description;
                entity.Price = starshipDto.Price;
                entity.CategoryId = starshipDto.CategoryId;
                entity.ManufacturerId = starshipDto.ManufacturerId;
                entity.StockQuantity = starshipDto.StockQuantity;
                _context.Starships.Update(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Starships.FindAsync(id);
            if (entity != null)
            {
                _context.Starships.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<StarshipDto>> SearchByManufacturerNameAsync(string manufacturerName)
        {
            return await _context.Starships
                .Include(s => s.Manufacturer)
                .Where(s => s.Manufacturer != null && s.Manufacturer.Name != null && s.Manufacturer.Name.Contains(manufacturerName))
                .Select(s => ToDto(s))
                .ToListAsync();
        }

        private static StarshipDto ToDto(Starship s) => new StarshipDto
        {
            Id = s.Id,
            Name = s.Name,
            Description = s.Description,
            Price = s.Price,
            CategoryId = s.CategoryId,
            ManufacturerId = s.ManufacturerId,
            StockQuantity = s.StockQuantity
        };

        private static Starship FromDto(StarshipDto dto) => new Starship
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            CategoryId = dto.CategoryId,
            ManufacturerId = dto.ManufacturerId,
            StockQuantity = dto.StockQuantity
        };
    }
}
