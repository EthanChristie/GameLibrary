using System;
using System.Linq;
using NUnit.Framework;
using GameLibrary.Core;

namespace GameLibrary.QualityAssurance
{
    public class Tests
    {
        private Library _library;

        [SetUp]
        public void Setup()
        {
            _library = new Library();

            var gow3 = new Game
            {
                Title = "Gears of War 3",
                Description = "Shoot 'em up"
            };

            var stepUp = new Game
            {
                Title = "Step Up for Kinect",
                Description = "Kinect adventure game"
            };
            var deadIsland = new Game
            {
                Title = "Dead Island",
                Description = "Survival horror"
            };

            _library.AddGame(gow3);
            _library.AddGame(stepUp);
            _library.AddGame(deadIsland);
        }

        [Test]
        public void TestAddGame()
        {
            var worldOfWarcraft = new Game
            {
                Title = "World of Warcraft",
                Description = "Massively multi-player online role playing game by Blizzard"
            };

            _library.AddGame(worldOfWarcraft);

            var libraryHasWorldOfWarcraft = _library.Games.Select(g => g.Title).Contains("World of Warcraft");

            Assert.True(libraryHasWorldOfWarcraft);
            Assert.AreEqual(4, _library.Games.Count);
        }

        [Test]
        public void GetGameExists()
        {
            var game = _library.Get("Dead Island");

            Assert.NotNull(game);
            Assert.AreEqual(game.Title, "Dead Island");
            Assert.AreEqual(game.Description, "Survival horror");
        }
        
        [Test]
        public void GetGameDoesNotExist()
        {
            var game = _library.Get("FooBar");

            Assert.Null(game);
        }

        [Test]
        public void TestEditDescription()
        {
            _library.EditDescription("Dead Island", "Survival action horror RPG");

            var deadIsland = _library.Get("Dead Island");

            Assert.AreEqual("Survival action horror RPG", deadIsland.Description);
        }
        
        [Test]
        public void TestEditDescriptionNonExistentGame()
        {
            Assert.Throws<InvalidOperationException>(() => _library.EditDescription("Foo", "Bar"));
        }

        [Test]
        public void TestRateGame()
        {
            _library.RateGame("Dead Island", 5);

            var rating = _library.GetRating("Dead Island");

            Assert.AreEqual(5, rating);
        }
    }
}