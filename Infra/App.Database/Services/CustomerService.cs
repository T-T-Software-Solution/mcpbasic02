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
    public class CustomerService : ICustomerService
    {
        private readonly AppContext _context;
        public CustomerService(AppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync() =>
            await _context.Customers.Select(c => ToDto(c)).ToListAsync();

        public async Task<CustomerDto?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Customers.FindAsync(id);
            return entity == null ? null : ToDto(entity);
        }

        public async Task<CustomerDto> CreateAsync(CustomerDto customerDto)
        {
            var entity = FromDto(customerDto);
            entity.Id = Guid.NewGuid();
            _context.Customers.Add(entity);
            await _context.SaveChangesAsync();
            return ToDto(entity);
        }

        public async Task UpdateAsync(CustomerDto customerDto)
        {
            var entity = await _context.Customers.FindAsync(customerDto.Id);
            if (entity != null)
            {
                entity.Name = customerDto.Name;
                entity.Email = customerDto.Email;
                entity.Address = customerDto.Address;
                _context.Customers.Update(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Customers.FindAsync(id);
            if (entity != null)
            {
                _context.Customers.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CustomerDto>> SearchByNameAsync(string name)
        {
            return await _context.Customers
                .Where(c => c.Name != null && c.Name.Contains(name))
                .Select(c => ToDto(c)).ToListAsync();
        }

        public async Task<IEnumerable<CustomerDto>> SearchByEmailAsync(string email)
        {
            return await _context.Customers
                .Where(c => c.Email != null && c.Email.Contains(email))
                .Select(c => ToDto(c)).ToListAsync();
        }

        private static CustomerDto ToDto(Customer c) => new CustomerDto
        {
            Id = c.Id,
            Name = c.Name,
            Email = c.Email,
            Address = c.Address
        };

        private static Customer FromDto(CustomerDto dto) => new Customer
        {
            Id = dto.Id,
            Name = dto.Name,
            Email = dto.Email,
            Address = dto.Address
        };
    }
}
