using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class GigsControllerTests
    {
        private GigsController _controller;

        public GigsControllerTests()
        {
            
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            _controller = new GigsController(mockUnitOfWork.Object);
            _controller.MockCurrentUser("1", "user@domain.com");
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
