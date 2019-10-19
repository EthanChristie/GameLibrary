using System;
using System.Collections.Generic;
using System.Linq;

namespace GameLibrary.Core
{
    public class Game
    {
        public static Game Empty = new Game()
        {
            Title = "",
            Description = ""
        };

        public string Description { get; set; }
        public string Title { get; set; }

        private readonly List<int> _ratings = new List<int>();

        public int GetRating()
        {
            if (_ratings.Count == 0)
                return 0;

            // Math.Round() always rounds to even (banker's rounding) e.g. 2.5 will round down to 2, but 3.5 will round up to 4
            // By my intuition, the midpoints (2.5, 3.5) should always round up, so I prefer to choose the MidpointRounding.AwayFromZero option
            // https://stackoverflow.com/questions/977796/
            return (int) Math.Round(_ratings.Average(), MidpointRounding.AwayFromZero);
        }

        public void Rate(int rating)
        {
            _ratings.Add(rating);
        }
    }
}