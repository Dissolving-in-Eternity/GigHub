using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GigHub.Core.Models
{
    /// <summary>
    /// Domain class. Represents a Gig and provides editing functional for an instance
    /// </summary>
    public class Gig
    {
        public int Id { get; set; }

        public bool IsCanceled { get; private set; }

        public ApplicationUser Artist { get; set; }

        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        public string City { get; set; }

        public string Venue { get; set; }

        public Genre Genre { get; set; }

        public byte GenreId { get; set; }

        public ICollection<Attendance> Attendances { get; private set; }

        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

        public void Create(IEnumerable<ApplicationUser> followers)
        {
            var notification = Notification.GigCreated(this);

            foreach (var follower in followers)
                follower.Notify(notification);
        }

        public void Cancel()
        {
            IsCanceled = true;

            var notification = Notification.GigCanceled(this);
            NotifyAll(notification);
        }

        public void Modify(DateTime dateTime, string venue, byte genre, string city)
        {
            var notification = Notification.GigUpdated(this, DateTime, Venue);

            DateTime = dateTime;
            Venue = venue;
            GenreId = genre;
            City = city;

            NotifyAll(notification);
        }

        private void NotifyAll(Notification notification)
        {
            foreach (var attendee in Attendances.Select(a => a.Attendee))
                attendee.Notify(notification);
        }
    }
}