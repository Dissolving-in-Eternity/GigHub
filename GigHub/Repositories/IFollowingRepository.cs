using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string artistId, string userId);
        IEnumerable<Following> GetUserFollowings(string userId);
    }
}