using GigHub.Core;
using GigHub.Core.ViewModels;
using Microsoft.AspNet.Identity;
using PagedList;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    /// <summary>
    /// Manages Home page data
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get upcoming gigs and display them on the homepage
        /// </summary>
        /// <param name="page">Current page number</param>
        /// <param name="query">Filter data</param>
        /// <returns>Gigs.chtml populated with gigs</returns>
        public ActionResult Index(int? page, string query = null)
        {
            int pageSize = 5;
            int pageNumber = page ?? 1;

            var upcomingGigs = _unitOfWork.Gigs.GetUpcominGigs(query);

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs.ToPagedList(pageNumber, pageSize),
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