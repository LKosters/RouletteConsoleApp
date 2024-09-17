using System;
using System.Collections.Generic;

namespace RouletteConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to roulette! Press H for help and Q to quit.");

            bool appRunning = true;

            object[,] rouletteNumbers = new object[,]
            {
                { 0, "Green", true },
                { 1, "Red", false },
                { 2, "Black", true },
                { 3, "Red", false },
                { 4, "Black", true },
                { 5, "Red", false },
                { 6, "Black", true },
                { 7, "Red", false },
                { 8, "Black", true },
                { 9, "Red", false },
                { 10, "Black", true },
                { 11, "Black", false },
                { 12, "Red", true },
                { 13, "Black", false },
                { 14, "Red", true },
                { 15, "Black", false },
                { 16, "Red", true },
                { 17, "Black", false },
                { 18, "Red", true },
                { 19, "Red", false },
                { 20, "Black", true },
                { 21, "Red", false },
                { 22, "Black", true },
                { 23, "Red", false },
                { 24, "Black", true },
                { 25, "Red", false },
                { 26, "Black", true },
                { 27, "Red", false },
                { 28, "Black", true },
                { 29, "Black", false },
                { 30, "Red", true },
                { 31, "Black", false },
                { 32, "Red", true },
                { 33, "Black", false },
                { 34, "Red", true },
                { 35, "Black", false },
                { 36, "Red", true }
            };

            int score = 1000;
            List<Bet> currentBets = new List<Bet>();
            Random random = new Random();

            while (appRunning)
            {
                Console.WriteLine($"Your current score is {score}");
                Console.WriteLine("Hit space to start a new round or press Q to quit.");

                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Q)
                {
                    appRunning = false;
                }
                else if (key == ConsoleKey.Spacebar)
                {
                    currentBets.Clear();
                    bool betting = true;

                    while (betting)
                    {
                        Console.WriteLine("On what do you want to bet? (e.g., 'Red', 'Black', 'Odd', 'Even', or a number 0-36). Press space when done.");
                        string bet = Console.ReadLine().ToLower();

                        if (bet == "")
                        {
                            Console.WriteLine("Please enter a valid bet.");
                        }
                        else if (Console.ReadKey(true).Key == ConsoleKey.Spacebar)
                        {
                            betting = false;
                            Console.WriteLine("Your bets:");
                            foreach (var b in currentBets)
                            {
                                Console.WriteLine($"{b.Type} - ${b.Amount}");
                            }

                            int spinResult = random.Next(0, 37);
                            string color = (string)rouletteNumbers[spinResult, 1];
                            bool isEven = (bool)rouletteNumbers[spinResult, 2];

                            Console.WriteLine($"The ball landed on {spinResult} ({color}, {(isEven ? "Even" : "Odd")})");

                            foreach (var betItem in currentBets)
                            {
                                bool betWon = false;
                                switch (betItem.Type)
                                {
                                    case "red":
                                    case "black":
                                        if (color.ToLower() == betItem.Type)
                                        {
                                            betWon = true;
                                        }
                                        break;
                                    case "odd":
                                        if (!isEven && spinResult != 0)
                                        {
                                            betWon = true;
                                        }
                                        break;
                                    case "even":
                                        if (isEven && spinResult != 0)
                                        {
                                            betWon = true;
                                        }
                                        break;
                                    default:
                                        if (int.TryParse(betItem.Type, out int betNumber) && betNumber == spinResult)
                                        {
                                            betWon = true;
                                        }
                                        break;
                                }

                                if (betWon)
                                {
                                    score += (int)(betItem.Amount * 2);
                                    Console.WriteLine($"You won ${betItem.Amount * 2} on {betItem.Type}!");
                                }
                                else
                                {
                                    score -= (int)betItem.Amount;
                                    Console.WriteLine($"You lost ${betItem.Amount} on {betItem.Type}.");
                                }
                            }

                            if (score <= 0)
                            {
                                Console.WriteLine("You're out of money! Game over.");
                                appRunning = false;
                            }
                        }
                        else
                        {
                            Console.WriteLine("How much do you want to bet on " + bet + "?");
                            string betValue = Console.ReadLine();

                            if (decimal.TryParse(betValue, out decimal parsedValue))
                            {
                                currentBets.Add(new Bet(bet, parsedValue));
                                Console.WriteLine($"Added bet on {bet} with a value of {parsedValue}");
                            }
                            else
                            {
                                Console.WriteLine("Invalid bet value.");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Not the space bar");
                }
            }
        }
    }
}