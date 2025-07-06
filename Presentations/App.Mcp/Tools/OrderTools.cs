using App.AppCore.Models;
using App.AppCore.Models.Dtos;
using App.AppCore.Interfaces;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace App.Mcp.Tools;

[McpServerToolType]
public class OrderTools
{
    private readonly IOrderService _orderService;
    public OrderTools(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [McpServerTool, Description("Get all orders")]
    public async Task<IEnumerable<OrderDto>> GetAllOrders() => await _orderService.GetAllAsync();

    [McpServerTool, Description("Get an order by Id")]
    public async Task<OrderDto?> GetOrderById(Guid id) => await _orderService.GetByIdAsync(id);

    [McpServerTool, Description("Create a new order")]
    public async Task<OrderDto> CreateOrder(
        [Description("Customer Id")] Guid customerId,
        [Description("Order date")] DateTime orderDate,
        [Description("Total amount")] decimal totalAmount)
    {
        var orderDto = new OrderDto
        {
            CustomerId = customerId,
            OrderDate = orderDate,
            TotalAmount = totalAmount
        };
        return await _orderService.CreateAsync(orderDto);
    }

    [McpServerTool, Description("Update an order")]
    public async Task UpdateOrder(
        [Description("Order Id")] Guid id,
        [Description("Customer Id")] Guid customerId,
        [Description("Order date")] DateTime orderDate,
        [Description("Total amount")] decimal totalAmount)
    {
        var orderDto = new OrderDto
        {
            Id = id,
            CustomerId = customerId,
            OrderDate = orderDate,
            TotalAmount = totalAmount
        };
        await _orderService.UpdateAsync(orderDto);
    }

    [McpServerTool, Description("Delete an order")]
    public async Task DeleteOrder(Guid id) => await _orderService.DeleteAsync(id);
}
