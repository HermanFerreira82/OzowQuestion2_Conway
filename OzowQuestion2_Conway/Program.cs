using System;
using System.Diagnostics.CodeAnalysis;

namespace OzowQuestion2_Conway
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine($"Invalid arguments: Please use:");
                Console.WriteLine($"OzowQuestion2_Conway [rows] [columns] [generationCount]");
                Console.WriteLine($"For example: OzowQuestion2_Conway 10 10 5 for a 10x10 world and 5 generations.");
                return;
            }

            int.TryParse(args[0], out var rowNumber);
            int.TryParse(args[1], out var colNumber);
            int.TryParse(args[2], out var generations);

            if (rowNumber <= 0) Console.WriteLine($"A valid row count was not provided.");
            if (colNumber <= 0) Console.WriteLine($"A valid column count was not provided.");
            if (generations <= 0) Console.WriteLine($"A valid generation count was not provided.");

            var testWorld = CreateRandomWorld(rowNumber, colNumber);
            
            var life = new Life(testWorld);

            Console.WriteLine($"\nGeneration 1");
            Console.WriteLine($"-----------------");
            Print(life.ToString("[]", "__"));

            for (int i = 1; i < generations; i++)
            {
                Console.WriteLine($"\nGeneration {i + 1}");
                Console.WriteLine($"-----------------");
                life.Evolve();
                Print(life.ToString("[]", "__"));
            }
        }

        /// <summary>
        /// Generates a random world
        /// </summary>
        /// <param name="rowCount">Row number</param>
        /// <param name="colCount">Column number</param>
        /// <returns>Life World as Life.State[,]</returns>
        private static Life.State[,] CreateRandomWorld(int rowCount, int colCount)
        {
            var testWorld = new Life.State[rowCount, colCount];
            Random rand = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    testWorld[i, j] = (Life.State)rand.Next(2);
                }
            }

            return testWorld;
        }

        /// <summary>
        /// Generates a pre-made world for testing
        /// </summary>
        /// <returns>Life World as Life.State[,]</returns>
        private static Life.State[,] CreatePreMadeWorld()
        {
            var testWorld = new Life.State[10, 10];
            testWorld[0, 0] = Life.State.Alive;
            testWorld[0, 1] = Life.State.Alive;
            testWorld[1, 0] = Life.State.Alive;

            testWorld[10 - 2, 0] = Life.State.Alive;
            testWorld[10 - 1, 0] = Life.State.Alive;
            testWorld[10 - 1, 1] = Life.State.Alive;

            testWorld[0, 10 - 2] = Life.State.Alive;
            testWorld[0, 10 - 1] = Life.State.Alive;
            testWorld[1, 10 - 1] = Life.State.Alive;

            testWorld[10 - 2, 10 - 1] = Life.State.Alive;
            testWorld[10 - 1, 10 - 1] = Life.State.Alive;
            testWorld[10 - 1, 10 - 2] = Life.State.Alive;

            return testWorld;
        }

        /// <summary>
        /// Print world to console.
        /// </summary>
        public static void Print(string world)
        {
            //Console.SetCursorPosition(0, 1);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(world);
        }

    }
}
