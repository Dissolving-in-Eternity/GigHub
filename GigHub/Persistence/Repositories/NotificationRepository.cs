using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    /// <summary>
    /// Contains queries for Notification class. Implements Repository pattern.
    /// </summary>
    public class NotificationRepository : INotificationRepository
    {
        private readonly IApplicationDbContext _context;

        public NotificationRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Notification> GetNewNotificationsFor(string userId)
        {
            return _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .OrderByDescending(d => d.DateTime)
                .ToList();
        }

        public IEnumerable<Notification> GetRecentNotificationsFor(string userId)
        {
            return _context.UserNotifications
                .Where(un => un.UserId == userId)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .OrderByDescending(d => d.DateTime)
                .Take(4)
                .ToList();
        }
    }
}