using System.Linq;
using System.Web.Mvc;
using GigHub.Models;
using GigHub.Repositories;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly GigRepository _gigRepository;
        private readonly AttendanceRepository _attendanceRepository;
        private readonly FollowingRepository _followingRepository;

        public HomeController()
        {
            _context = new ApplicationDbContext();
            _gigRepository = new GigRepository(_context);
            _attendanceRepository = new AttendanceRepository(_context);
            _followingRepository = new FollowingRepository(_context);
        }

        public ActionResult Index(string query = null)
        {
            var upcomingGigs = _gigRepository.GetUpcominGigs();

            if (!string.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs
                    .Where(g =>
                        g.Artist.Name.Contains(query) ||
                        g.Genre.Name.Contains(query) ||
                        g.Venue.Contains(query));
            }

            var userId = User.Identity.GetUserId();

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchTerm = query,
                Attendances = _attendanceRepository.GetFutureAttendances(userId)
                    .ToLookup(a => a.GigId),
                Followings = _followingRepository.GetUserFollowings(userId)
                    .ToLookup(f => f.FolloweeId)
            };

            return View("Gigs", viewModel);
        }
    }
}