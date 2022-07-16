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
using System;
using System.Collections;

namespace EventsTesting
{
    [TestFixture]
    public class Tests
    {
        private Mock<IDataRepository<Event>> mockDataRepository;
        private EventsController _eventsController;
        [SetUp]
        public void Setup()
        {
            mockDataRepository = new Mock<IDataRepository<Event>>();
            _eventsController = new EventsController(mockDataRepository.Object);
        }

        [Test]
        public void Test_GetAll_ReturnsList_whenDataIsPresent()
        {
            // Arrange
            IEnumerable<Event> events = new List<Event>();
            mockDataRepository.Setup(x => x.GetAll()).Returns(events);

            // Act
            var result = _eventsController.Get();
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void Test_GetAll_EmptyList_whenDataIsNotPresent()
        {
            // Arrange
            IEnumerable<Event> events = null;
            mockDataRepository.Setup(x => x.GetAll()).Returns(events);

            // Act
            var result = _eventsController.Get();
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }

        [Test]
        public void Test_Get_Event_UsingName_WhenExists()
        {
            // Arrange
            string name = "T20";
            Event events = new Event();
            mockDataRepository.Setup(x => x.GetByName(name)).Returns(events);

            // Act
            var result = _eventsController.GetByName(name);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }
        [Test]
        public void Test_Get_Event_UsingName_WhenNotExists()
        {
            // Arrange
            string name = "Rugby";

            //Event events = new Event();
            //mockDataRepository.Setup(x => x.GetByName(name)).Returns(events);

            // Act
            var result = _eventsController.GetByName(name);
            var BadResult = result as NotFoundObjectResult;

            // Assert
            Assert.IsNotNull(BadResult);
            Assert.AreEqual(404, BadResult.StatusCode);

        }
        //[Test]
        //public void Test_Get_Event_UsingID_WhenExists()
        //{
        //    // Arrange
        //    int id = 2;
        //    Event events = new Event();
        //    mockDataRepository.Setup(x => x.Get(id)).Returns(events);

        //    // Act
        //    var result = _eventsController.Get(id);
        //    var okResult = result as OkObjectResult;

        //    // Assert
        //    Assert.IsNotNull(okResult);
        //    Assert.AreEqual(200, okResult.StatusCode);

        //}
        //[Test]
        //public void Test_Get_Event_UsingID_WhenNotExists()
        //{
        //    // Arrange
        //    int id=100;

        //   // Event events = new Event();
        //   // mockDataRepository.Setup(x => x.Get(id)).Returns(events);

        //    // Act
        //    var result = _eventsController.Get(id);
        //    var BadResult = result as NotFoundObjectResult;

        //    // Assert
        //    Assert.IsNotNull(BadResult);
        //    Assert.AreEqual(404, BadResult.StatusCode);

        //}

        [Test]
        public void Test_Delete_Event_UsingID_WhenExists()
        {
            // Arrange
            int id = 2;
            Event events = new Event();
            mockDataRepository.Setup(x => x.Get(id)).Returns(events);
            mockDataRepository.Setup(x => x.Delete(events)).Returns(true);

            // Act
            var result = _eventsController.Delete(id);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }
        [Test]
        public void Test_Delete_Event_UsingID_WhenNotExists()
        {
            // Arrange
            int id = 100;

            

            // Act
            var result = _eventsController.Delete(id);
            var BadResult = result as NotFoundObjectResult;

            // Assert
            Assert.IsNotNull(BadResult);
            Assert.AreEqual(404, BadResult.StatusCode);

        }

        [Test]
        public void Test_Post_ValidEvent()
        {
            // Arrange
            Event events = new Event();
            {
                events.SportId = 1;
                events.EventName = "WorldCup";
                events.NoOfSlots = 100;
                events.Date = Convert.ToDateTime("13 - 08 - 2022");
            }
            mockDataRepository.Setup(x => x.Add(events)).Returns(true);

            // Act
            var result = _eventsController.Post(events);
            var okResult = result as CreatedAtRouteResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(201, okResult.StatusCode);

        }

        [Test]
        public void Test_Post_InValidParticipation()
        {
            // Arrange
            Event events = null;

            mockDataRepository.Setup(x => x.Add(events)).Returns(false);

            // Act
            var result = _eventsController.Post(events);
            var BadResult = result as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(BadResult);
            Assert.AreEqual(400, BadResult.StatusCode);

        }
    }
}