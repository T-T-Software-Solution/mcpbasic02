using System;

namespace App.AppCore.Models
{
    public class Notification
    {
        public Guid Id { get; set; }
        public string? Type { get; set; } // SMS, Email
        public string? Content { get; set; }
        public DateTime SentDate { get; set; }
    }
}
