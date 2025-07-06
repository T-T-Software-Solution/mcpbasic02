using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.AppCore.Models.Dtos;

namespace App.AppCore.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<OrderDto?> GetByIdAsync(Guid id);
        Task<OrderDto> CreateAsync(OrderDto order);
        Task UpdateAsync(OrderDto order);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<OrderDto>> SearchByCustomerNameAsync(string customerName);
        Task<IEnumerable<OrderDto>> SearchByCustomerEmailAsync(string customerEmail);
    }
}
