using System.ComponentModel.DataAnnotations;

namespace MontyHallSimulatorApplication.Models
{
    public class Game
    {
        [Range(1, int.MaxValue, ErrorMessage = "The number of games must be at least 1.")]
        public int NumberOfGames { get; set; }
        public bool SwitchDoors { get; set; }
        public int SwitchWins { get; set; }
        public int StayWins { get; set; }
        public int StayLosses { get; set; }
        public int SwitchLosses { get; set; }
    }

}
