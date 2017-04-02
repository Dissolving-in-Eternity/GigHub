﻿using System.Linq;
using System.Web.Http;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var followerId = User.Identity.GetUserId();

            if(_context.Followings.Any(f => f.FollowerId == followerId && f.FolloweeId == dto.FolloweeId))
                return BadRequest("The following already exists.");

            var following = new Following
            {
                FolloweeId = dto.FolloweeId,
                FollowerId = followerId
            };

            _context.Followings.Add(following);
            _context.SaveChanges();

            return Ok();
        }
    }
}
