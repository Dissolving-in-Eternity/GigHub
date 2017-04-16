using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using WebGrease.Css.Extensions;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET api/notifications
        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();

            var notifications = _unitOfWork.Notifications.GetNewNotificationsFor(userId);
            
            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }

        // POST api/notifications
        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();
            var userNotifications = _unitOfWork.UserNotifications.GetUserNotificationsFor(userId);

            userNotifications.ForEach(n => n.Read());

            _unitOfWork.Complete();

            return Ok();
        }
    }
}
