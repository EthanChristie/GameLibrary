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

        public int Rating => (int) Math.Round(_ratings.Average());

        public void Rate(int rating)
        {
            _ratings.Add(rating);
        }
    }
}