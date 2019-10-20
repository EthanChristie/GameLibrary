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

            _library.AddGame("Gears of War 3", "Shoot 'em up");
            _library.AddGame("Step Up for Kinect", "Kinect adventure game");
            _library.AddGame("Dead Island", "Survival horror");
        }

        [Test]
        public void TestAddGame()
        {
            var worldOfWarcraft = new Game
            {
                Title = "World of Warcraft",
                Description = "Massively multi-player online role playing game by Blizzard"
            };

            _library.AddGame("World of Warcraft", "Massively multi-player online role playing game by Blizzard");

            var libraryHasWorldOfWarcraft = _library.GetAll().Select(g => g.Title).Contains("World of Warcraft");

            Assert.True(libraryHasWorldOfWarcraft);
            Assert.AreEqual(4, _library.GetAll().Length);
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

            Assert.AreEqual(Game.Empty, game);
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
        public void TestRateGameSingleRating()
        {
            _library.RateGame("Dead Island", 5);
            var rating = _library.GetRating("Dead Island");

            Assert.AreEqual(5, rating);
        }

        [Test]
        public void TestRateGameMultipleRatings()
        {
            _library.RateGame("Step Up for Kinect", 1);
            _library.RateGame("Step Up for Kinect", 2);
            _library.RateGame("Step Up for Kinect", 2);
            _library.RateGame("Step Up for Kinect", 3);

            var rating = _library.GetRating("Step Up for Kinect");
            Assert.AreEqual(2, rating);
        }
        
        [Test]
        public void TestRateGameMultipleRatingsRounded()
        {
            _library.RateGame("Step Up for Kinect", 1);
            _library.RateGame("Step Up for Kinect", 2);
            _library.RateGame("Step Up for Kinect", 3);
            _library.RateGame("Step Up for Kinect", 4);

            var rating = _library.GetRating("Step Up for Kinect");
            Assert.AreEqual(3, rating);
        }
        
        [TestCase(0)]
        [TestCase(-1)]  
        [TestCase(6)]
        public void TestRateGameRatingOutOfBounds(int rating)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _library.RateGame("Step Up for Kinect", rating));
        }

        [Test]
        public void TestGetRatingNoRatings()
        {
            var rating = _library.GetRating("Dead Island");
            Assert.AreEqual(0, rating);
        }

        [Test]
        public void TestGetSortedListDesc()
        {
            _library.RateGame("Gears of War 3", 5);
            _library.RateGame("Step Up for Kinect", 1);
            _library.RateGame("Dead Island", 3);

            var games = _library.GetAll(SortingMethod.Descending);

            Assert.AreEqual("Gears of War 3", games[0].Title);
            Assert.AreEqual("Dead Island", games[1].Title);
            Assert.AreEqual("Step Up for Kinect", games[2].Title);
        }
        
        [Test]
        public void TestGetSortedListAsc()
        {
            _library.RateGame("Gears of War 3", 5);
            _library.RateGame("Step Up for Kinect", 1);
            _library.RateGame("Dead Island", 3);

            var games = _library.GetAll(SortingMethod.Ascending);

            Assert.AreEqual("Step Up for Kinect", games[0].Title);
            Assert.AreEqual("Dead Island", games[1].Title);
            Assert.AreEqual("Gears of War 3", games[2].Title);
        }
    }

    
}