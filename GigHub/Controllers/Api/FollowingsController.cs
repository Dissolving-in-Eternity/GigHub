using System.Web.Http;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public FollowingsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

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

        [HttpDelete]
        public IHttpActionResult Unfollow(string id)
        {
            var userId = User.Identity.GetUserId();

            var following = _unitOfWork.Followings.GetFollowing(id, userId);

            if (following == null)
                return NotFound();

            _unitOfWork.Followings.Delete(following);
            _unitOfWork.Complete();

            return Ok(id);
        }
    }
}
