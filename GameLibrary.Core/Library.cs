using System;
using System.Collections.Generic;
using System.Linq;

namespace GameLibrary.Core
{
    public class Library
    {
        private List<Game> Games { get; } = new List<Game>();

        public void AddGame(string title, string description)
        {
            if (string.IsNullOrEmpty(title))
                throw new InvalidOperationException("Game must have a title");

            if (Get(title) != Game.Empty)
                throw new InvalidOperationException("Game already exists");

            var game = new Game
            {
                Title = title,
                Description = description
            };

            Games.Add(game);
        }

        public void EditDescription(string title, string description)
        {
            var game = Get(title);
            if (Get(title) == Game.Empty)
                throw new InvalidOperationException("Game does not exist");

            game.Description = description;
        }

        public Game Get(string title)
        {
            var game = Games.SingleOrDefault(g => g.Title == title);
            return game ?? Game.Empty;
        }

        public Game[] GetAll(SortingMethod sortingMethod = SortingMethod.None)
        {
            Game[] allGames = {};
            switch (sortingMethod)
            {
                case SortingMethod.None:
                    allGames = Games.ToArray();
                    break;
                case SortingMethod.Ascending:
                    allGames = Games.OrderBy(g => g.Rating).ToArray();
                    break;
                case SortingMethod.Descending:
                    allGames = Games.OrderByDescending(g => g.Rating).ToArray();
                    break;
            }

            return allGames;
        }

        public void RateGame(string title, int rating)
        {
            if (rating < 1 || rating > 5)
                throw new ArgumentOutOfRangeException(nameof(rating), rating, "Rating must be between 1-5");

            Get(title).Rate(rating);
        }

        public int GetRating(string title)
        {
            var game = Get(title);
            return game.GetRating();
        }
    }
}
