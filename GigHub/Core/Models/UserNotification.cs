using System;

namespace GigHub.Core.Models
{
    /// <summary>
    /// Domain class. Responsible for notifying a user about gig's addition/modification/deletion
    /// </summary>
    public class UserNotification
    {
        public string UserId { get; private set; }

        public int NotificationId { get; private set; }

        public ApplicationUser User { get; private set; }

        public Notification Notification { get; private set; }

        public bool IsRead { get; private set; }

        protected UserNotification() { }

        public UserNotification(ApplicationUser user, Notification notification)
        {
            if(user == null)
                throw new ArgumentNullException(nameof(user));

            if(notification == null)
                throw new ArgumentNullException(nameof(notification));

            User = user;
            UserId = user.Id;
            Notification = notification;
            NotificationId = notification.Id;
        }

        public void Read()
        {
            IsRead = true;
        }
    }
}