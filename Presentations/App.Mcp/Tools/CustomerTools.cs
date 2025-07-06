using App.AppCore.Models;
using App.AppCore.Models.Dtos;
using App.AppCore.Interfaces;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace App.Mcp.Tools;

[McpServerToolType]
public class CustomerTools
{
    private readonly ICustomerService _customerService;
    public CustomerTools(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [McpServerTool, Description("Get all customers")]
    public async Task<IEnumerable<CustomerDto>> GetAllCustomers() => await _customerService.GetAllAsync();

    [McpServerTool, Description("Get a customer by Id")]
    public async Task<CustomerDto?> GetCustomerById(Guid id) => await _customerService.GetByIdAsync(id);

    [McpServerTool, Description("Create a new customer")]
    public async Task<CustomerDto> CreateCustomer(
        [Description("Customer name")] string name,
        [Description("Customer email")] string email,
        [Description("Customer address")] string address)
    {
        var customerDto = new CustomerDto
        {
            Name = name,
            Email = email,
            Address = address
        };
        return await _customerService.CreateAsync(customerDto);
    }

    [McpServerTool, Description("Update a customer")]
    public async Task UpdateCustomer(
        [Description("Customer Id")] Guid id,
        [Description("Customer name")] string name,
        [Description("Customer email")] string email,
        [Description("Customer address")] string address)
    {
        var customerDto = new CustomerDto
        {
            Id = id,
            Name = name,
            Email = email,
            Address = address
        };
        await _customerService.UpdateAsync(customerDto);
    }

    [McpServerTool, Description("Delete a customer")]
    public async Task DeleteCustomer(Guid id) => await _customerService.DeleteAsync(id);
}
