using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.AppCore.Models.Dtos;

namespace App.AppCore.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllAsync();
        Task<CustomerDto?> GetByIdAsync(Guid id);
        Task<CustomerDto> CreateAsync(CustomerDto customer);
        Task UpdateAsync(CustomerDto customer);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<CustomerDto>> SearchByNameAsync(string name);
        Task<IEnumerable<CustomerDto>> SearchByEmailAsync(string email);
    }
}
