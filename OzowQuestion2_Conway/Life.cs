using System;
using System.Text;

namespace OzowQuestion2_Conway
{
    public class Life : IEvolutionEngine
    {
        private readonly State[,] _world;
        
        /// <summary>
        /// Constructor allowing the injection of a world of type State[,]
        /// </summary>
        /// <param name="world">A world of type State[,]</param>
        public Life(State[,] world)
        {
            _world = world ?? throw new ArgumentNullException(nameof(world));
        }

        /// <summary>
        /// Count alive neighbours horizontally, vertically, and diagonally adjacent.
        /// </summary>
        /// <param name="currentState">A world of type State[,]</param>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <returns>Alive neighbours counts</returns>
        private static int AliveNeighbourCount(State[,] currentState, int x, int y)
        {
            int aliveCount = 0;
            int maxY = currentState.GetLength(0);
            int maxX = currentState.GetLength(1);

            // 0..
            // ...
            // ...
            if (y > 0 && x > 0 && currentState[y - 1, x - 1] == State.Alive) aliveCount++;

            // .0.
            // ...
            // ...
            if (y > 0 && currentState[y - 1, x] == State.Alive) aliveCount++;

            // ..0
            // ...
            // ...
            if (y > 0 && x < maxX - 1 && currentState[y - 1, x + 1] == State.Alive) aliveCount++;

            // ...
            // 0..
            // ...
            if (x > 0 && currentState[y, x - 1] == State.Alive) aliveCount++;

            // ...
            // ..0
            // ...
            if (x < maxX - 1 && currentState[y, x + 1] == State.Alive) aliveCount++;

            // ...
            // ...
            // 0..
            if (y < maxY - 1 && x > 0 && currentState[y + 1, x - 1] == State.Alive) aliveCount++;

            // ...
            // ...
            // .0.
            if (y < maxY - 1 && currentState[y + 1, x] == State.Alive) aliveCount++;

            // ...
            // ...
            // ..0
            if (y < maxY - 1 && x < maxX - 1 && currentState[y + 1, x + 1] == State.Alive) aliveCount++;

            return aliveCount;
        }

        /// <summary>
        /// Evolve to next x generation(s) | Apply Business Rules
        /// </summary>
        /// <param name="generations">number of generations to evolve</param>
        /// <returns>world as State[,]</returns>
        public void Evolve(int generations = 1)
        {
            if(generations < 0) throw new ArgumentException("Generations must be a positive integer");

            for (int i = 0; i < generations; i++)
            {
                var currentState = _world.Clone() as State[,];
                if (currentState == null) throw new ApplicationException("Invalid state.");

                int maxY = _world.GetLength(0);
                int maxX = _world.GetLength(1);

                for (int y = 0; y < maxY; y++)
                {
                    for (int x = 0; x < maxX; x++)
                    {
                        int aliveCount = AliveNeighbourCount(currentState, x, y);

                        // Rule: Any live cell with two or three live neighbours survives.
                        if (aliveCount == 2 || aliveCount == 3)
                        {
                            _world[y, x] = State.Alive;
                        }
                        else
                        {
                            _world[y, x] = State.Dead;
                        }

                        // Any dead cell with three live neighbours becomes a live cell.
                        if (currentState[y, x] == State.Dead && aliveCount == 3) _world[y, x] = State.Alive;
                    }
                }
            }
        }

        /// <summary>
        /// Returns a String version of the World for display
        /// </summary>
        /// <returns>Current State of World as String</returns>
        public override string ToString()
        {
            const string aliveChar = "()";
            const string deadChar = "..";
            var pixels = new StringBuilder();
            for (var row = 0; row < _world.GetLength(0); row++)
            {
                for (var column = 0; column < _world.GetLength(1); column++)
                {
                    var pixel = _world[row, column];
                    pixels.Append(pixel == State.Alive ? aliveChar : deadChar);
                }

                pixels.AppendLine();
            }

            return pixels.ToString();
        }

        /// <summary>
        /// Returns a String version of the World for display
        /// </summary>
        /// <param name="aliveAvatar">Character(s) to represent a live cell</param>
        /// <param name="deadAvatar">Character(s) to represent a dead cell</param>
        /// <returns>Current State of World as String</returns>
        public string ToString(string aliveAvatar, string deadAvatar)
        {
            var pixels = new StringBuilder();
            for (var row = 0; row < _world.GetLength(0); row++)
            {
                for (var column = 0; column < _world.GetLength(1); column++)
                {
                    var pixel = _world[row, column];
                    pixels.Append(pixel == State.Alive ? aliveAvatar : deadAvatar);
                }

                pixels.AppendLine();
            }

            return pixels.ToString();
        }

        /// <summary>
        /// Possible game states
        /// </summary>
        public enum State
        {
            Dead = 0,
            Alive = 1
        }
    }
}
