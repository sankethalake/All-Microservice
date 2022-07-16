using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using SportsEventsMicroService.Controllers;
using SportsEventsMicroService.Database;
using SportsEventsMicroService.Database.Repository;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;

namespace SportsTesting
{
    [TestFixture]
    public class Tests
    {
        private Mock<ISportDataRepository<Sport>> mockDataRepository;
        private SportsController _sportsController;
        [SetUp]
        public void Setup()
        {
            mockDataRepository = new Mock<ISportDataRepository<Sport>>();
            _sportsController = new SportsController(mockDataRepository.Object);
        }

        [Test]
        public void Test_GetAll_ReturnsList_whenDataIsPresent()
        {
            // Arrange
            IEnumerable<Sport> sports = new List<Sport>();
            mockDataRepository.Setup(x => x.GetAll()).Returns(sports);

            // Act
            var result = _sportsController.Get();
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void Test_GetAll_EmptyList_whenDataIsNotPresent()
        {
            // Arrange
            IEnumerable<Sport> sports = null;
            mockDataRepository.Setup(x => x.GetAll()).Returns(sports);

            // Act
            var result = _sportsController.Get();
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }

        [Test]
        public void Test_Get_Sport_UsingName_WhenExists()
        {
            // Arrange
            string name = "Cricket";
            Sport sports = new Sport();
            mockDataRepository.Setup(x => x.GetByName(name)).Returns(sports);

            // Act
            var result = _sportsController.GetByName(name);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }
        
        [Test]
        public void Test_Get_Sport_UsingName_WhenNotExists()
        {
            // Arrange
            string name = "Rugby";

            

            // Act
            var result = _sportsController.GetByName(name);
            var BadResult = result as NotFoundObjectResult;

            // Assert
            Assert.IsNotNull(BadResult);
            Assert.AreEqual(404, BadResult.StatusCode);

        }
        
    }
}