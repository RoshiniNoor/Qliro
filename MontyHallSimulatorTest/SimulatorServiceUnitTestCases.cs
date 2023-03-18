using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MontyHallSimulatorApplication.Models;
using MontyHallSimulatorApplication.Service;
using Moq;

namespace MontyHallSimulatorTest
{
    [TestClass]
    public class SimulatorServiceUnitTestCases
    {      
        [TestMethod]
        public void Return_Valid_Results_When_SwitchDoors_False()
        {
            // Arrange
            var game = new Game
            {
                NumberOfGames = 1000,
                SwitchDoors = false
            };
            SimulatorService simulatorService = new SimulatorService();
            // Act
            var result = simulatorService.PlayGame(game);

            // Assert
            Assert.AreEqual(1000, result.StayWins + result.StayLosses);
            Assert.AreEqual(0, result.SwitchWins + result.SwitchLosses);
        }

        [TestMethod]
        public void Return_Valid_Results_When_SwitchDoorsTrue()
        {
            //Arrange
            var game = new Game
            {
                NumberOfGames = 1000,
                SwitchDoors = true,
            };
            SimulatorService simulatorService = new SimulatorService();

            //Act
            var result = simulatorService.PlayGame(game);

            //Assert
            Assert.AreEqual(0, result.StayWins + result.StayLosses);
            Assert.AreEqual(1000, result.SwitchWins + result.SwitchLosses);
        }

        //Initial choice is not a car door, users opts to switch door and wins
        [TestMethod]
        public void InitialChoice_Is_Not_CarDoor_And_Switched_Then_Wins()
        {
            // Arrange
            Game game = new Game
            {
                NumberOfGames = 1,
                SwitchDoors = true
            };

            var mockRandom = new Mock<Random>();
            mockRandom.SetupSequence(r => r.Next(1, 4))
                      .Returns(1) // The car is behind door #1
                      .Returns(2) // The player initially chooses door #2
                      .Returns(3); // Monty reveals door #3

            var gameService = new SimulatorService(mockRandom.Object);

            // Act
            var result = gameService.PlayGame(game);

            // Assert
            Assert.AreEqual(1, result.SwitchWins);
            Assert.AreEqual(0, result.SwitchLosses);
            Assert.AreEqual(0, result.StayWins);
            Assert.AreEqual(0, result.StayLosses);
        }

        //Initial choice is a car door, users opts to switch door and lost
        [TestMethod]
        public void InitialChoice_Is_CarDoor_And_Switched_Then_Lost()
        {
            // Arrange
            Game game = new Game
            {
                NumberOfGames = 1,
                SwitchDoors = true
            };

            var mockRandom = new Mock<Random>();
            mockRandom.SetupSequence(r => r.Next(1, 4))
                      .Returns(1) // The car is behind door #1
                      .Returns(1) // The player initially chooses door #2
                      .Returns(3); // Monty reveals door #3

            var gameService = new SimulatorService(mockRandom.Object);

            // Act
            var result = gameService.PlayGame(game);

            // Assert
            Assert.AreEqual(0, result.SwitchWins);
            Assert.AreEqual(1, result.SwitchLosses);
            Assert.AreEqual(0, result.StayWins);
            Assert.AreEqual(0, result.StayLosses);
        }

        //Initial choice is a car door, users stays with initial choice and wins
        [TestMethod]
        public void InitialChoice_Is_CarDoor_And_Stays_Then_Wins()
        {
            // Arrange
            Game game = new Game
            {
                NumberOfGames = 1,
                SwitchDoors = false
            };

            var mockRandom = new Mock<Random>();
            mockRandom.SetupSequence(r => r.Next(1, 4))
                      .Returns(1) // The car is behind door #1
                      .Returns(1) // The player initially chooses door #1
                      .Returns(2); // Monty reveals door #2

            var gameService = new SimulatorService(mockRandom.Object);

            // Act
            var result = gameService.PlayGame(game);

            // Assert
            Assert.AreEqual(0, result.SwitchWins);
            Assert.AreEqual(0, result.SwitchLosses);
            Assert.AreEqual(1, result.StayWins);
            Assert.AreEqual(0, result.StayLosses);
        }

        //Initial choice is not a car door, users stays with initial choice and lost
        [TestMethod]
        public void InitialChoice_Is_Not_CarDoor_And_Stays_Then_Lost()
        {
            // Arrange
            Game game = new Game
            {
                NumberOfGames = 1,
                SwitchDoors = false
            };

            var mockRandom = new Mock<Random>();
            mockRandom.SetupSequence(r => r.Next(1, 4))
                      .Returns(1) // The car is behind door #1
                      .Returns(2) // The player initially chooses door #2
                      .Returns(3); // Monty reveals door #3

            var gameService = new SimulatorService(mockRandom.Object);

            // Act
            var result = gameService.PlayGame(game);

            // Assert
            Assert.AreEqual(0, result.SwitchWins);
            Assert.AreEqual(0, result.SwitchLosses);
            Assert.AreEqual(0, result.StayWins);
            Assert.AreEqual(1, result.StayLosses);
        }
    }
}
