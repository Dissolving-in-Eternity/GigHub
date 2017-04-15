using System.Collections.Generic;
using System.Linq;
using GigHub.Models;

namespace GigHub.Repositories
{
    public class FollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Following GetFollowing(string artistId, string userId)
        {
            return _context.Followings
                .SingleOrDefault(f => f.FolloweeId == artistId && f.FollowerId == userId);
        }

        public IEnumerable<Following> GetUserFollowings(string userId)
        {
            return _context.Followings
                .Where(f => f.FollowerId == userId)
                .ToList();
        }
    }
}