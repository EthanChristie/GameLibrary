﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace GameLibrary.Core
{
    public class Library
    {
        public void AddGame(Game game)
        {
            if (Get(game.Title) != Game.Empty)
                throw new InvalidOperationException("Game already exists");

            Games.Add(game);
        }

        public List<Game> Games { get; } = new List<Game>();

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

        public void RateGame(string title, int rating)
        {
            if (rating < 1 || rating > 5)
                throw new ArgumentOutOfRangeException(nameof(rating), rating, "Rating must be between 1-5");

            Get(title).Rate(rating);
        }

        public int GetRating(string title)
        {
            var game = Get(title);
            return game.Rating;
        }
    }
}