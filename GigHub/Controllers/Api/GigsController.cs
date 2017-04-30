using GigHub.Core;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    /// <summary>
    /// RESTfull API, which allows to cancel a gig
    /// </summary>
    [Authorize]
    public class GigsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Cancel specific gig for currently logged in user
        /// </summary>
        /// <param name="id">Gig Id</param>
        /// <returns>
        /// NotFound, if gig doesn't exist or already cancelled
        /// Unauhorised, if id of the current user doen't correspond the id of user created a gig
        /// OkResult if gig succesfully cancelled
        /// </returns>
        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();

            var gig = _unitOfWork.Gigs.GetGigWithAttendees(id);

            if (gig == null || gig.IsCanceled)
                return NotFound();

            if (gig.ArtistId != userId)
                return Unauthorized();

            gig.Cancel();

            _unitOfWork.Complete();

            return Ok();
        }
    }
}
