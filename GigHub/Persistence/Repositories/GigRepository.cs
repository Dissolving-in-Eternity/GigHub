using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    /// <summary>
    /// Contains queries for Gig class. Implements Repository pattern.
    /// </summary>
    public class GigRepository : IGigRepository
    {
        private readonly IApplicationDbContext _context;

        public GigRepository(IApplicationDbContext context)
        {
            _context = context;
        }
        
        public Gig GetGig(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == gigId);
        }

        public IEnumerable<Gig> GetUpcominGigs(string searchTerm = null)
        {
            var upcomingGigs = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                upcomingGigs = upcomingGigs
                    .Where(g =>
                        g.Artist.Name.Contains(searchTerm) ||
                        g.Genre.Name.Contains(searchTerm) ||
                        g.Venue.Contains(searchTerm) ||
                        g.City.Contains(searchTerm));
            }

            return upcomingGigs.OrderBy(g => g.DateTime).ToList();
        }

        public IEnumerable<Gig> GetUpcomingGigsByArtist(string userId)
        {
            return _context.Gigs
                .Where(g =>
                    g.ArtistId == userId &&
                    g.DateTime > DateTime.Now &&
                    !g.IsCanceled)
                .Include(g => g.Genre)
                .OrderBy(g => g.DateTime)
                .ToList();
        }

        public Gig GetGigWithAttendees(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .SingleOrDefault(g => g.Id == gigId);
        }

        public IEnumerable<Gig> GetGigsUserAttending(string userId, string searchTerm = null)
        {
            var attending = _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                attending = attending
                    .Where(g =>
                        g.Artist.Name.Contains(searchTerm) ||
                        g.Genre.Name.Contains(searchTerm) ||
                        g.Venue.Contains(searchTerm) ||
                        g.City.Contains(searchTerm));
            }

            return attending.OrderBy(g => g.DateTime).ToList();
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }
    }
}