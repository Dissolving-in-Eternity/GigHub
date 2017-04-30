using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    /// <summary>
    /// RESTful API which allows to attend specific gig or delete attendance
    /// </summary>
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendancesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // POST api/attendances
        /// <summary>
        /// Create attendance for currently logged in user
        /// </summary>
        /// <param name="dto">data transfer object for Gig Id</param>
        /// <returns>BadRequest if attendance already exists, OkResult otherwise</returns>
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();

            var attendance = _unitOfWork.Attendances.GetAttendance(dto.GigId, userId);

            if (attendance != null)
                return BadRequest("The attendance already exists.");

            attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };
            _unitOfWork.Attendances.Add(attendance);
            _unitOfWork.Complete();

            return Ok();
        }

        // DELETE api/attendances
        /// <summary>
        /// Delete specific gig attendance for currently logged in user
        /// </summary>
        /// <param name="id">Gig Id</param>
        /// <returns>NotFound, if attendance doesn't exist, OkResult with deleted gig id otherwise</returns>
        [HttpDelete]
        public IHttpActionResult DeleteAttendance(int id)
        {
            var userId = User.Identity.GetUserId();

            var attendance = _unitOfWork.Attendances.GetAttendance(id, userId);

            if (attendance == null)
                return NotFound();

            _unitOfWork.Attendances.Delete(attendance);
            _unitOfWork.Complete();

            return Ok(id);
        }
    }
}
