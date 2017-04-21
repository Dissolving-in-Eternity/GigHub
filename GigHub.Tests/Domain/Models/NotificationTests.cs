using FluentAssertions;
using GigHub.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GigHub.Tests.Domain.Models
{
    [TestClass]
    public class NotificationTests
    {
        [TestMethod]
        public void GigCanceled_WhenCalled_ShouldReturnedANotificationForACanceledGig()
        {
            var gig = new Gig();

            var notification = Notification.GigCanceled(gig);

            notification.Type.Should().Be(NotificationType.GigCanceled);
            notification.Gig.Should().Be(gig);
        }
    }
}
