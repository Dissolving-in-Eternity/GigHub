using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class FollowingRepository : IFollowingRepository
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

        public void Add(Following following)
        {
            _context.Followings.Add(following);
        }

        public void Delete(Following following)
        {
            _context.Followings.Remove(following);
        }
    }
}