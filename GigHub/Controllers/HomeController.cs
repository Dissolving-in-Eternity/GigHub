using System.Linq;
using System.Web.Mvc;
using GigHub.Core;
using GigHub.Core.ViewModels;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(string query = null)
        {
            var upcomingGigs = _unitOfWork.Gigs.GetUpcominGigs(query);

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchTerm = query,
                Attendances = _unitOfWork.Attendances
                    .GetFutureAttendances(User.Identity.GetUserId())
                    .ToLookup(a => a.GigId),
            };

            return View("Gigs", viewModel);
        }
    }
}