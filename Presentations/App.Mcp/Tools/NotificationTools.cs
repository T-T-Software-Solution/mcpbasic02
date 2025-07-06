using App.AppCore.Models;
using App.AppCore.Interfaces;
using ModelContextProtocol.Server;
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;

namespace App.Mcp.Tools;

[McpServerToolType]
public class NotificationTools
{
    private readonly INotificationService _notificationService;

    public NotificationTools(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [McpServerTool, Description("Send a notification to a customer. / ส่งการแจ้งเตือนไปยังลูกค้า")]
    public async Task SendNotification(
        [Description("Notification type (e.g., SMS, Email) / ประเภทการแจ้งเตือน (เช่น SMS, Email)")] string type,
        [Description("Notification content/message / เนื้อหาหรือข้อความการแจ้งเตือน")] string content)
    {
        var notification = new Notification
        {
            Id = Guid.NewGuid(),
            Type = type,
            Content = content,
            SentDate = DateTime.UtcNow
        };
        await _notificationService.SendNotificationAsync(notification);
    }    
}