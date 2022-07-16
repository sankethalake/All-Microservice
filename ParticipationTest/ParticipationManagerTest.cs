using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using ParticipationMicroservice.Controllers;
using ParticipationMicroservice.DBContexts;
using ParticipationMicroservice.Model;
using ParticipationMicroservice.Models;
using ParticipationMicroservice.Models.DataManager;
using ParticipationMicroservice.Models.Repository;
using System.Collections.Generic;
using System.Net.Http;

namespace ParticipationTest
{
    [TestFixture]
    public class Tests
    {

        private Mock<IDataRepository<Participation>> mockDataRepository;
        private ParticipationController _participationController;
        [SetUp]
        public void Setup()
        {
            mockDataRepository = new Mock<IDataRepository<Participation>>();
            _participationController = new ParticipationController(mockDataRepository.Object);
        }

        [Test]
        public void Test_GetAll_ReturnsList_whenDataIsPresent()
        {
            // Arrange
            IEnumerable<Participation> participations = new List<Participation>();
            mockDataRepository.Setup(x => x.GetAll()).Returns(participations);

            // Act
            var result = _participationController.Get();
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }

        [Test]
        public void Test_GetAll_EmptyList_whenDataIsNotPresent()
        {
            // Arrange
            IEnumerable<Participation> participations = null;
            mockDataRepository.Setup(x => x.GetAll()).Returns(participations);

            // Act
            var result = _participationController.Get();
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }

        [Test]
        public void Test_GetByApproved_ValidStatus()
        {
            // Arrange
            string status = "approved";
            IEnumerable<Participation> participations = new List<Participation>();
            mockDataRepository.Setup(x => x.GetByStatus(status)).Returns(participations);

            // Act
            var result = _participationController.GetByApproved(status);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }

        [Test]
        public void Test_GetByApproved_InValidStatus()
        {
            // Arrange
            string status = "pending";

            // Act
            var result = _participationController.GetByApproved(status);
            var BadResult = result as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(BadResult);
            Assert.AreEqual(400, BadResult.StatusCode);

        }

        [Test]
        public void Test_GetByPending_ValidStatus()
        {
            // Arrange
            string status = "pending";
            IEnumerable<Participation> participations = new List<Participation>();
            mockDataRepository.Setup(x => x.GetByStatus(status)).Returns(participations);

            // Act
            var result = _participationController.GetByPending(status);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }

        [Test]
        public void Test_GetByPending_InValidStatus()
        {
            // Arrange
            string status = "Approved";

            // Act
            var result = _participationController.GetByPending(status);
            var BadResult = result as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(BadResult);
            Assert.AreEqual(400, BadResult.StatusCode);

        }

        [Test]
        public void Test_GetByDeclined_ValidStatus()
        {
            // Arrange
            string status = "declined";
            IEnumerable<Participation> participations = new List<Participation>();
            mockDataRepository.Setup(x => x.GetByStatus(status)).Returns(participations);

            // Act
            var result = _participationController.GetByDeclined(status);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }

        [Test]
        public void Test_GetByDeclined_InValidStatus()
        {
            // Arrange
            string status = "Approved";

            // Act
            var result = _participationController.GetByPending(status);
            var BadResult = result as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(BadResult);
            Assert.AreEqual(400, BadResult.StatusCode);

        }

        [Test]
        public void Test_Post_ValidParticipation()
        {
            // Arrange
            Participation participations = new Participation();
            {
                participations.ParticipationId = 1;
                participations.EventId = 1;
                participations.Events = new Event();
                participations.Status = ParticipationStatus.pending;
                participations.Player = new List<Player>();
            }
            mockDataRepository.Setup(x => x.Add(participations)).Returns(true);

            // Act
            var result = _participationController.Post(participations);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }

        [Test]
        public void Test_Post_InValidParticipation()
        {
            // Arrange
            Participation participations = null;

            mockDataRepository.Setup(x => x.Add(participations)).Returns(false);

            // Act
            var result = _participationController.Post(participations);
            var BadResult = result as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(BadResult);
            Assert.AreEqual(400, BadResult.StatusCode);

        }

        [Test]
        public void Test_Put_ValidParticipationId()
        {

            // Arrange
            StatusModel statusModel = new StatusModel() { Status = "Pending" };
            long id = 1;
            Participation participations = new Participation();
            {
                participations.ParticipationId = (int)id;
                participations.EventId = 1;
                participations.Events = new Event();
                participations.Status = ParticipationStatus.pending;
                participations.Player = new List<Player>();
            }
            mockDataRepository.Setup(x => x.Get(id)).Returns(participations);
            mockDataRepository.Setup(x => x.Update(participations, statusModel.Status)).Returns(true);
            

            // Act
            var result = _participationController.Put(id, statusModel);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }

        [Test]
        public void Test_Put_InValidParticipationId()
        {

            // Arrange
            long id = 1;
            Participation participations = null;

            mockDataRepository.Setup(x => x.Get(id)).Returns(participations);
            //mockDataRepository.Setup(x => x.Update(participations, status)).Returns(false);
            StatusModel statusModel = new StatusModel() { Status = "Pending"};


            // Act
            var result = _participationController.Put(id, statusModel);
            var badResult = result as NotFoundObjectResult;

            // Assert
            Assert.IsNotNull(badResult);
            Assert.AreEqual(404, badResult.StatusCode);

        }

        [Test]
        public void Test_Put_InValidParticipationStatus()
        {

            // Arrange
            StatusModel statusModel = new StatusModel() { Status = "Pending" };
            long id = 1;
            Participation participations = new Participation();
            {
                participations.ParticipationId = (int)id;
                participations.EventId = 1;
                participations.Events = new Event();
                participations.Status = ParticipationStatus.pending;
                participations.Player = new List<Player>();
            }

            mockDataRepository.Setup(x => x.Get(id)).Returns(participations);
            mockDataRepository.Setup(x => x.Update(participations, statusModel.Status)).Returns(false);
            

            // Act
            var result = _participationController.Put(id, statusModel);
            var badResult = result as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(badResult);
            Assert.AreEqual(400, badResult.StatusCode);

        }
    }
}