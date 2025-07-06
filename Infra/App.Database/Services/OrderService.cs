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
    public class OrderService : IOrderService
    {
        private readonly AppContext _context;
        public OrderService(AppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync() =>
            await _context.Orders.Include(o => o.OrderItems)
                .Select(o => ToDto(o)).ToListAsync();

        public async Task<OrderDto?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id);
            return entity == null ? null : ToDto(entity);
        }

        public async Task<OrderDto> CreateAsync(OrderDto orderDto)
        {
            var entity = FromDto(orderDto);
            entity.Id = Guid.NewGuid();
            _context.Orders.Add(entity);
            await _context.SaveChangesAsync();
            return ToDto(entity);
        }

        public async Task UpdateAsync(OrderDto orderDto)
        {
            var entity = await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == orderDto.Id);
            if (entity != null)
            {
                entity.CustomerId = orderDto.CustomerId;
                entity.OrderDate = orderDto.OrderDate;
                entity.TotalAmount = orderDto.TotalAmount;
                // Optionally update OrderItems here
                _context.Orders.Update(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Orders.FindAsync(id);
            if (entity != null)
            {
                _context.Orders.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<OrderDto>> SearchByCustomerNameAsync(string customerName)
        {
            return await _context.Orders.Include(o => o.OrderItems)
                .Include(o => o.Customer)
                .Where(o => o.Customer != null && o.Customer.Name != null && o.Customer.Name.Contains(customerName))
                .Select(o => ToDto(o)).ToListAsync();
        }

        public async Task<IEnumerable<OrderDto>> SearchByCustomerEmailAsync(string customerEmail)
        {
            return await _context.Orders.Include(o => o.OrderItems)
                .Include(o => o.Customer)
                .Where(o => o.Customer != null && o.Customer.Email != null && o.Customer.Email.Contains(customerEmail))
                .Select(o => ToDto(o)).ToListAsync();
        }

        private static OrderDto ToDto(Order o) => new OrderDto
        {
            Id = o.Id,
            CustomerId = o.CustomerId,
            OrderDate = o.OrderDate,
            TotalAmount = o.TotalAmount,
            OrderItems = o.OrderItems?.Select(oi => new OrderItemDto
            {
                Id = oi.Id,
                OrderId = oi.OrderId,
                StarshipId = oi.StarshipId,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice
            }).ToList() ?? new List<OrderItemDto>()
        };

        private static Order FromDto(OrderDto dto) => new Order
        {
            Id = dto.Id,
            CustomerId = dto.CustomerId,
            OrderDate = dto.OrderDate,
            TotalAmount = dto.TotalAmount,
            OrderItems = dto.OrderItems?.Select(oi => new OrderItem
            {
                Id = oi.Id,
                OrderId = oi.OrderId,
                StarshipId = oi.StarshipId,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice
            }).ToList() ?? new List<OrderItem>()
        };
    }
}
