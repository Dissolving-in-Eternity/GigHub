using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface IApplicationUserRepository
    {
        IEnumerable<ApplicationUser> GetArtistsFollowedBy(string userId);
        IEnumerable<ApplicationUser> GetFollowersWithMatchingCityFor(string userId, string city);
        List<ApplicationUser> GetNewBands();
    }
}