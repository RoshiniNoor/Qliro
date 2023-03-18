using System;
using System.Linq;
using MontyHallSimulatorApplication.Models;

namespace MontyHallSimulatorApplication.Service
{
    public class SimulatorService
    {
        private readonly int NumOfDoors = 3;

        private Random random;
        
        public SimulatorService(Random random = default) 
        {
            this.random = random ?? new Random();
        }

        public Game PlayGame(Game game)
        {
            Game gameResponse = new Game();
            gameResponse.SwitchDoors = game.SwitchDoors;
          
            // Simulate the specified number of games
            for (int i = 0; i < game.NumberOfGames; i++)
            {
                // Randomly assign the car to one of the three doors
                int carDoor = random.Next(1, NumOfDoors + 1);

                // The player initially chooses a door at random
                int initialChoice = random.Next(1, NumOfDoors + 1);


                // Monty Hall reveals one of the two doors that the player did not choose, and that does not have the car
                int revealedDoor = (initialChoice == carDoor) ?
                    Enumerable.Range(1, NumOfDoors).Except(new[] { carDoor }).First()
                    : Enumerable.Range(1, NumOfDoors).Except(new[] { initialChoice, carDoor }).First();

                // If the player switches doors, they choose the remaining unopened door
                int finalChoice = (game.SwitchDoors) ? 
                    Enumerable.Range(1, NumOfDoors).Except(new[] { initialChoice, revealedDoor }).First()
                    : initialChoice;

                // Determine if the player has won or lost
                if (game.SwitchDoors)
                {
                    if (finalChoice == carDoor)
                    {
                        gameResponse.SwitchWins++;
                    }

                    else
                    {
                        gameResponse.SwitchLosses++;
                    }
                }
                else
                {
                    if (initialChoice == carDoor)
                    {
                        gameResponse.StayWins++;
                    }
                    else
                    {
                        gameResponse.StayLosses++;
                    }
                }
            
            }
            return gameResponse;
        }
    }
}