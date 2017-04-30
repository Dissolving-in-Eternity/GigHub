using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using GigHub.Core;

namespace GigHub.Tests.Persistence.Repositories
{
    [TestClass]
    public class GigRepositoryTests
    {
        private GigRepository _repository;
        private Mock<DbSet<Gig>> _mockGigs;
        private Mock<DbSet<Attendance>> _mockAttendances;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockGigs = new Mock<DbSet<Gig>>();
            _mockAttendances = new Mock<DbSet<Attendance>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.Gigs).Returns(_mockGigs.Object);
            mockContext.Setup(c => c.Attendances).Returns(_mockAttendances.Object);

            _repository = new GigRepository(mockContext.Object);

            _userId = "1";
        }

        #region GetUpcomingGigsByArtist

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsInThePast_ShouldNotBeReturned()
        {
            // Arrange
            var gig = new Gig { DateTime = DateTime.Today.AddDays(-1), ArtistId = "1" };

            _mockGigs.SetSource(new List<Gig>{ gig });

            // Act
            var gigs =_repository.GetUpcomingGigsByArtist("1");

            // Assert
            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsCanceled_ShouldNotBeReturned()
        {
            // Arrange
            var gig = new Gig { DateTime = DateTime.Today.AddDays(1), ArtistId = "1" };
            gig.Cancel();

            _mockGigs.SetSource(new List<Gig> { gig });

            // Act
            var gigs = _repository.GetUpcomingGigsByArtist("1");

            // Assert
            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsForADifferentArtist_ShouldNotBeReturned()
        {
            // Arrange
            var gig = new Gig { DateTime = DateTime.Today.AddDays(1), ArtistId = "1" };

            _mockGigs.SetSource(new List<Gig> { gig });

            // Act
            var gigs = _repository.GetUpcomingGigsByArtist(gig.ArtistId + "---");

            // Assert
            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsForGivenArtistAndInTheFuture_ShouldBeReturned()
        {
            // Arrange
            var gig = new Gig { DateTime = DateTime.Today.AddDays(1), ArtistId = "1" };

            _mockGigs.SetSource(new List<Gig> { gig });

            // Act
            var gigs = _repository.GetUpcomingGigsByArtist(gig.ArtistId);

            // Assert
            gigs.Should().Contain(gig);
        }

        #endregion

        #region GetGigsUserAttending

        [TestMethod]
        public void GetGigsUserAttending_GigIsInThePast_ShouldNotBeReturned()
        {
            // Arrange
            var gig = new Gig { DateTime = DateTime.Now.AddDays(-1) };
            var attendance = new Attendance { Gig = gig, AttendeeId = _userId };

            _mockAttendances.SetSource(new List<Attendance> { attendance });

            // Act
            var result = _repository.GetGigsUserAttending(_userId);

            // Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void GetGigsUserAttending_AttendanceForADifferentUser_ShouldNotBeReturned()
        {
            // Arrange
            var gig = new Gig { DateTime = DateTime.Now.AddDays(1) };
            var attendance = new Attendance { Gig = gig, AttendeeId = _userId };

            _mockAttendances.SetSource(new List<Attendance> { attendance });

            // Act
            var result = _repository.GetGigsUserAttending(_userId + "---");

            // Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void GetGigsUserAttending_ValidRequest_ShouldReturnGigsInTheFutureUserAttending()
        {
            var gig = new Gig { DateTime = DateTime.Now.AddDays(1) };
            var attendance = new Attendance { Gig = gig, AttendeeId = _userId };

            _mockAttendances.SetSource(new List<Attendance> { attendance });

            var result = _repository.GetGigsUserAttending(_userId);

            result.Should().Contain(gig);
        }

        #endregion
    }
}
