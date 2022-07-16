using MicroService2.Database;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PlayerMicroService.Controllers;
using PlayerMicroService.Repositories;
using PlayersMicroService.Database;
using System.Collections.Generic;

namespace PlayerTest
{
    [TestFixture]
    public class TestPlayer
    {

        private Mock<IDataRepository<Player>> mockDataRepository;
        private PlayersController _playersController;
        [SetUp]
        public void Setup()
        {
            mockDataRepository = new Mock<IDataRepository<Player>>();
            _playersController = new PlayersController(mockDataRepository.Object);
        }

        [Test]
        public void Test_GetAll_ReturnsList_whenDataIsPresent()
        {
            IEnumerable<Player> players = new List<Player>();
            mockDataRepository.Setup(x => x.GetAll()).Returns(players);

            // Act
            var result = _playersController.GetPlayers();
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
        [Test]
        public void Test_Delete_InValidPlayer()
        {
            // Arrange
            long id = 1;
            Player player = new Player();
            {
                player.PlayerId = (int)id;

            }

            mockDataRepository.Setup(x => x.Delete(player.PlayerId)).Returns(false);

            // Act
            var result = _playersController.DeletePlayer(player.PlayerId);
            var BadResult = result as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(BadResult);
            Assert.AreEqual(400, BadResult.StatusCode);

        }
        [Test]
        public void Test_Delete_ValidPlayerId()
        {

            // Arrange
            //string status = "pending";
            long id = 1;
            Player player = new Player();
            {
                player.PlayerId = (int)id;

            }

            mockDataRepository.Setup(x => x.Delete(player.PlayerId)).Returns(true);

            // Act
            var result = _playersController.DeletePlayer(player.PlayerId);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }

        [Test]
        public void Test_Post_Player()
        {
            // Arrange

            Player player = new Player()
            {
                PlayerId = 1,
                SportId = 20,
                Sports=new Sport(),
                PlayerName = "Manish Panday",
                Age = 30,
                ContactNumber = "8974563012",
                Email = "manishpandet@gmail.com",
                Gender = GenderLevel.Male

            };
            mockDataRepository.Setup(x => x.Add(player));

            // Act
            var result = _playersController.PostPlayer(player);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }

    }
}