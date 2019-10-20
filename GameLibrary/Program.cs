using System;
using System.Linq;
using ConsoleTables.Core;
using GameLibrary.Core;

namespace GameLibrary.Runner
{
    public class Program
    {
        private static Library _library;

        public static void Main(string[] args)
        {
            _library = new Library();
            Console.WriteLine("Setting up...");
            Setup();
            Console.WriteLine("Done.");

            PrintAll();

            PrintUsage();

            while (true)
            {
                var line = Console.ReadLine();
                var input = ParsingHelper.QuotedSplit2(line, " ").ToArray();
                if (!input.Any())
                {
                    PrintUsage();
                    continue;
                }
                    
                var function = input.First();
                var otherArguments = input
                    .Skip(1)
                    .Select(i => i.Trim('\"'))
                    .ToArray();

                switch (function)
                {
                    case "add":
                        AddGame(otherArguments);
                        break;
                    case "edit":
                        EditGame(otherArguments);
                        break;
                    case "rate":
                        RateGame(otherArguments);
                        break;
                    case "view":
                        View(otherArguments);
                        break;
                    case "usage":
                        PrintUsage();
                        break;
                    case "exit":
                        return;
                    default:
                        PrintUsage();
                        break;
                }
            }
        }

        private static void RateGame(string[] input)
        {
            if (!CheckInputLengthAndPrintUsageIfWrong(input, 2))
                return;

            if (!int.TryParse(input[1], out var rating) || rating < 1 || rating > 5)
            {
                Console.WriteLine("Rating should be a whole number between 1-5");
                return;
            }

            _library.RateGame(input[0], rating);
        }

        private static void View(string[] input)
        {
            if (!CheckInputLengthAndPrintUsageIfWrong(input, 0, 1))
                return;

            if (input.Length == 0)
            {
                PrintAll();
                return;
            }

            var sortOption = input[0];
            SortingMethod sortingMethod;
            switch (sortOption)
            {
                case "asc":
                    sortingMethod = SortingMethod.Ascending;
                    break;
                case "desc":
                    sortingMethod = SortingMethod.Descending;
                    break;
                default:
                    Console.WriteLine("Usage: view [asc|desc]");
                    return;
            }

            PrintAll(sortingMethod);
        }

        private static void EditGame(string[] input)
        {
            if (!CheckInputLengthAndPrintUsageIfWrong(input, 2))
                return;

            try
            {
                _library.EditDescription(input[0], input[1]);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static void AddGame(string[] input)
        {
            if (!CheckInputLengthAndPrintUsageIfWrong(input, 2))
                return;

            try
            {
                _library.AddGame(input[0], input[1]);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static bool CheckInputLengthAndPrintUsageIfWrong(string[] input, params int[] lengths)
        {
            if (lengths.Any(possibleLength => input.Length == possibleLength))
                return true;
            

            Console.WriteLine("Incorrect arguments supplied");
            PrintUsage();
            return false;
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Usage: ");
            Console.WriteLine("add title description");
            Console.WriteLine("\tAdds a game to the library");
            Console.WriteLine("edit title description");
            Console.WriteLine("\tEdits a game's title'");
            Console.WriteLine("rate title rating");
            Console.WriteLine("\tRates a game (1-5)");
            Console.WriteLine("view [asc|desc]");
            Console.WriteLine("\tPrints the library, optionally in ascending/descending order");
            Console.WriteLine("usage");
            Console.WriteLine("\tPrints this message again");
            Console.WriteLine("exit");
            Console.WriteLine("\tExits the program");
            Console.WriteLine();
            Console.WriteLine("Use quotes to enclose arguments with multiple words e.g. add \"Dota 2\" \"Multiplayer Online Battle Arena\"");
            Console.WriteLine();
        }

        private static void PrintAll(SortingMethod sortingMethod = SortingMethod.None)
        {
            var rows = _library.GetAll(sortingMethod);

            var table = new ConsoleTable("Title", "Description", "Rating");

            foreach (var game in rows)
            {
                table.AddRow(PrettyFormat(game));
            }

            table.Write(Format.Alternative);
        }

        private static object[] PrettyFormat(Game game)
        {
            var stars = Enumerable.Repeat('*', game.Rating);
            var prettyRating = string.Join(" ", stars);

            return new object[] {game.Title, game.Description, prettyRating};
        }

        private static void Setup()
        {
            _library.AddGame("Gears of War 3", "Shoot 'em up");
            _library.AddGame("Step Up for Kinect", "Kinect adventure game");
            _library.AddGame("Dead Island", "Survival horror");

            _library.RateGame("Gears of War 3", 5);
            _library.RateGame("Step Up for Kinect", 1);
            _library.RateGame("Dead Island", 3);
        }
    }
}
