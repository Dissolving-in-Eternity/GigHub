using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface IGigRepository
    {
        Gig GetGig(int gigId);
        IEnumerable<Gig> GetUpcominGigs(string searchTerm = null);
        IEnumerable<Gig> GetUpcomingGigsByArtist(string userId);
        Gig GetGigWithAttendees(int gigId);
        IEnumerable<Gig> GetGigsUserAttending(string userId, string searchTerm = null);
        void Add(Gig gig);
    }
}