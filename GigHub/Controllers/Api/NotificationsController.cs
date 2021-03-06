﻿using AutoMapper;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebGrease.Css.Extensions;

namespace GigHub.Controllers.Api
{
    /// <summary>
    /// RESTfull API for notification management
    /// </summary>
    [Authorize]
    public class NotificationsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET api/notifications
        /// <summary>
        /// Get new notifications for currently logged in user
        /// </summary>
        /// <returns>Set of notifications</returns>
        public IEnumerable<NotificationDto> GetNewNotifications(bool isNewNotificationsRequired)
        {
            var userId = User.Identity.GetUserId();

            var notifications = isNewNotificationsRequired ?
                _unitOfWork.Notifications.GetNewNotificationsFor(userId) :
                _unitOfWork.Notifications.GetRecentNotificationsFor(userId);

            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }

        // POST api/notifications
        /// <summary>
        /// Marks notification as read
        /// </summary>
        /// <returns>OkResult if notification marked as read succesfully</returns>
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
