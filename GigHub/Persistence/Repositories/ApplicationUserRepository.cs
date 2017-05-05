using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    /// <summary>
    /// Contains queries for ApplicationUser class. Implements Repository pattern.
    /// </summary>
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetArtistsFollowedBy(string userId)
        {
            return _context.Followings
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Followee)
                .ToList();
        }

        public IEnumerable<ApplicationUser> GetFollowersWithMatchingCityFor(string groupId, string city)
        {
            return _context.Followings
                .Where(f => f.FolloweeId == groupId)
                .Select(f => f.Follower)
                .Where(f => f.City == city)
                .ToList();
        }
    }
}