using JwtAuthentication;
using JwtAuthentication.Controllers;
using JwtAuthentication.Database;
using JwtAuthentication.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace LoginControllerTest
{
    public class LoginTests
    {
        private LoginController loginControllerTest;
        private Mock<IJwtAuthenticationManager> mockJwtAuthenticationManager;
        private Mock<IRepository<Login>> mockLoginRepository;
        [SetUp]
        public void Setup()
        {
            mockJwtAuthenticationManager = new Mock<IJwtAuthenticationManager>();
            mockLoginRepository = new Mock<IRepository<Login>>();
            loginControllerTest = new LoginController(mockJwtAuthenticationManager.Object, mockLoginRepository.Object);
        }

        [Test]
        public void Authenticate_When_UserIsValid()
        {
            var user = new Login
            {
                Username = "User",
                Password = "Password"
            };
            mockLoginRepository.Setup(x => x.Get(user)).Returns(true);
            var result = loginControllerTest.Authenticate(user);

            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }
        [Test]
        public void Authenticate_When_UserIsNotValid()
        {
            var user = new Login
            {
                Username = "User",
                Password = "Password"
            };
            var result = loginControllerTest.Authenticate(user);
            var unauthorizedResult = result as UnauthorizedObjectResult;

            Assert.AreEqual(401,unauthorizedResult.StatusCode);

        }
        [TearDown]
        public void TearDown()
        {
            mockJwtAuthenticationManager = null;
            mockLoginRepository = null;
            loginControllerTest = null;
        }
    }
}