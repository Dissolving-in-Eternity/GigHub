using GigHub.Core;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    /// <summary>
    /// Manages 'Who I Follow' page 
    /// </summary>
    [Authorize]
    public class FolloweesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public FolloweesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets artists followed by currently logged in user
        /// </summary>
        /// <returns>Followees/Index.cshtml populated with followees</returns>
        public ActionResult Index()
        {
            var artists = _unitOfWork.Users.GetArtistsFollowedBy(User.Identity.GetUserId());

            return View(artists);
        }
    }
}