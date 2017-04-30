using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    /// <summary>
    /// RESTful API which allows to follow an artist or delete following
    /// </summary>
    [Authorize]
    public class FollowingsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public FollowingsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // POST api/followings
        /// <summary>
        /// Create following for currently logged in user
        /// </summary>
        /// <param name="dto">data transfer object for Followee Id (Artist Id)</param>
        /// <returns>BadRequest if following already exists, OkResult otherwise</returns>
        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var followerId = User.Identity.GetUserId();

            var following = _unitOfWork.Followings.GetFollowing(dto.FolloweeId, followerId);

            if(following != null)
                return BadRequest("The following already exists.");

            following = new Following
            {
                FolloweeId = dto.FolloweeId,
                FollowerId = followerId
            };

            _unitOfWork.Followings.Add(following);
            _unitOfWork.Complete();

            return Ok();
        }

        // DELETE api/followings
        /// <summary>
        /// Delete specific following for currently logged in user
        /// </summary>
        /// <param name="id">Artist Id</param>
        /// <returns>NotFound, if following doesn't exist, OkResult with deleted artist id otherwise</returns>
        [HttpDelete]
        public IHttpActionResult Unfollow(string id)
        {
            var followerId = User.Identity.GetUserId();

            var following = _unitOfWork.Followings.GetFollowing(id, followerId);

            if (following == null)
                return NotFound();

            _unitOfWork.Followings.Delete(following);
            _unitOfWork.Complete();

            return Ok(id);
        }
    }
}
