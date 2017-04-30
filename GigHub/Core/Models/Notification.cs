using System;

namespace GigHub.Core.Models
{
    /// <summary>
    /// Domain class. Contains reference to Gig. 
    /// Responsible for creation of a new Gig object in case of 
    /// modification/deletion of already existing gig
    /// or creation of a new one.
    /// </summary>
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }

        public Gig Gig { get; private set; }

        protected Notification() { }

        private Notification(NotificationType type, Gig gig)
        {
            if(gig == null)
                throw new ArgumentNullException(nameof(gig));

            DateTime = DateTime.Now;
            Gig = gig;
            Type = type;
        }

        // Factory methods
        public static Notification GigCreated(Gig gig)
        {
            return new Notification(NotificationType.GigCreated, gig);
        }

        public static Notification GigUpdated(Gig newGig, DateTime originalDateTime, string originalVenue)
        {
            var notification = new Notification(NotificationType.GigUpdated, newGig);
            notification.OriginalDateTime = originalDateTime;
            notification.OriginalVenue = originalVenue;

            return notification;
        }

        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(NotificationType.GigCanceled, gig);
        }
    }
}