using Microsoft.VisualStudio.TestTools.UnitTesting;
using MontyHallSimulatorApplication.Models;
using MontyHallSimulatorApplication.Service;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MontyHallSimulatorTest
{
    [TestClass]
    public class Validations
    {
        [TestCategory("InputValidation")]
        [TestMethod]
        public void PlayGame_Returns_Game_With_SwitchValue()
        {
            // Arrange
            var simulatorService = new SimulatorService();
            var game = new Game()
            {
                SwitchDoors = true
            };

            // Act
            var gameResponse = simulatorService.PlayGame(game);

            // Assert
            Assert.AreEqual(game.SwitchDoors, gameResponse.SwitchDoors);
        }

        [TestCategory("InputValidation")]
        [TestMethod]
        public void NumberOfGames_MinValue_Returns_IsValid()
        {
            // Arrange
            var model = new Game() 
            { 
                NumberOfGames = 1 
            };
            var validationContext = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestCategory("InputValidation")]
        [TestMethod]
        public void NumberOfGames_MinValue_Returns_IsNotValid()
        {
            // Arrange
            var model = new Game() 
            { 
                NumberOfGames = -1 // setting NumberOfGames to 0, which is less than the minimum allowed value of 1
            }; 
            var validationContext = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isValid); // expecting validation to fail since NumberOfGames is less than the minimum allowed value of 1
        }

        [TestCategory("InputValidation")]
        [TestMethod]
        public void NumberOfGames_InvalidValue_Returns_InvalidResponse()
        {
            // Arrange
            var model = new Game() { NumberOfGames = -1 }; // setting the number of games to an invalid value (-1)

            // Act and Assert
            // expecting an ArgumentException to be thrown because the number of games is invalid
        }

    }
}
