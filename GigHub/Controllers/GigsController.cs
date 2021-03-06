﻿using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using Microsoft.AspNet.Identity;
using PagedList;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    /// <summary>
    /// Manages gigs (CRUD + filtering)
    /// </summary>
    public class GigsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets a set of gigs user attending
        /// </summary>
        /// <returns>Gigs/Mine.cshtml populated with upcoming gigs for the current user</returns>
        [Authorize]
        public ViewResult Mine()
        {
            var gigs = _unitOfWork.Gigs.GetUpcomingGigsByArtist(User.Identity.GetUserId());

            return View(gigs);
        }

        /// <summary>
        /// Manages 'Gigs I'm attending' page
        /// </summary>
        /// <param name="page">Current page number</param>
        /// <returns>Gigs.chtml populated with gigs user attending</returns>
        [Authorize]
        public ActionResult Attending(int? page, string query = null)
        {
            int pageSize = 4;
            int pageNumber = page ?? 1;

            var userId = User.Identity.GetUserId();
            var gigs = _unitOfWork.Gigs.GetGigsUserAttending(userId, query);

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = gigs.ToPagedList(pageNumber, pageSize),
                ItemsCount = gigs.Count(),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending",
                SearchTerm = query
            };

            return View("Gigs", viewModel);
        }

        /// <summary>
        /// Edit gig details
        /// </summary>
        /// <param name="id">Gig Id</param>
        /// <returns>GigForm.cshtml populated with initial gig data</returns>
        [Authorize]
        public ActionResult Edit(int id)
        {
            var gig = _unitOfWork.Gigs.GetGig(id);

            if(gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            var viewModel = new GigFormViewModel()
            {
                Heading = "Edit a Gig",
                Id = gig.Id,
                Genres = _unitOfWork.Genres.GetGenres(),
                Date = gig.DateTime.ToString("dd.MM.yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue,
                City = gig.City
            };
        
            return View("GigForm", viewModel);
        }

        /// <summary>
        /// Save modified gig details
        /// Extract data from view model and add it to the db
        /// </summary>
        /// <param name="viewModel">Modified view model</param>
        /// <returns>Redirect to 'My upcoming gigs'</returns>
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
                return View("GigForm", viewModel);
            }

            var gig = _unitOfWork.Gigs.GetGigWithAttendees(viewModel.Id);

            if (gig == null)
                return HttpNotFound();

            if(gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre, viewModel.City);

            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel()
            {
                Genres = _unitOfWork.Genres.GetGenres(),
                Heading = "Add a Gig"
            };

            return View("GigForm", viewModel);
        }

        /// <summary>
        /// Create a gig
        /// Extract data from view model and add it to the db
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns>Redirect to 'My upcoming gigs'</returns>
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Heading = "Add a Gig";
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
                return View("GigForm", viewModel);
            }

            var userId = User.Identity.GetUserId();
            var gig = new Gig
            {
                ArtistId = userId,
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue,
                City = viewModel.City
            };

            _unitOfWork.Gigs.Add(gig);

            // Get followers for the current group
            var followers = _unitOfWork.Users.GetFollowersWithMatchingCityFor(userId, viewModel.City);
            // Notify all followers about new gig creation
            if(followers != null)
                gig.Create(followers);

            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }

        /// <summary>
        /// Pass query string to Home controller
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns>Redirect to Home/Index action</returns>
        //[HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            if (viewModel.Heading == "Upcoming Gigs")
                return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
            return RedirectToAction("Attending", "Gigs", new { query = viewModel.SearchTerm });
        }

        /// <summary>
        /// View gig details
        /// </summary>
        /// <param name="id">Gig Id</param>
        /// <returns>Details view</returns>
        public ActionResult Details(int id)
        {
            var gig = _unitOfWork.Gigs.GetGig(id);

            if (gig == null)
                return HttpNotFound();

            var viewModel = new GigDetailsViewModel { Gig = gig };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                viewModel.IsAttending = _unitOfWork.Attendances.GetAttendance(gig.Id, userId) != null;

                viewModel.IsFollowing = _unitOfWork.Followings.GetFollowing(gig.ArtistId, userId) != null;
            }

            return View("Details", viewModel);
        }
    }
}