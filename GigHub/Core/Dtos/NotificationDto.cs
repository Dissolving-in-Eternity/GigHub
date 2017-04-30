using System;
using GigHub.Core.Models;

namespace GigHub.Core.Dtos
{
    /// <summary>
    /// Data transfer object for Notification class
    /// </summary>
    public class NotificationDto
    {
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }
        public GigDto Gig { get; set; }
    }
}