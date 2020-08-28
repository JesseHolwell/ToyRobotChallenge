using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobotChallenge
{

    interface IBoardEntity
    {
        string Left();
        string Move();
        string Place(int? x = null, int? y = null);
        string Report();
        string Right();
    }

    class Robot : IBoardEntity
    {
        /// <summary>
        /// The direciton the robot is currently facing
        /// </summary>
        private CardinalDirection Direction { get; set; }

        /// <summary>
        /// Current position on the tabletop
        /// </summary>
        private Tuple<int, int> Position { get; set; }

        private int PositionX { get; set; }

        private int PositionY { get; set; }

        private bool HasBeenPlaced { get; set; }

        /// <summary>
        /// Place the robot on the tabletop
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>returns true if place was successful</returns>
        public string Place(int? x, int? y)
        {
            if (x < 0 || x > 5
                || y < 0 || y > 5)
                return "Cannot place robot out of bounds";

            PositionX = x ?? 0;
            PositionY = y ?? 0;

            HasBeenPlaced = true;

            return $"Placed at {PositionX},{PositionY}";
        }

        /// <summary>
        /// Move the robot one step in the direction its facing
        /// </summary>
        /// <returns>returns true if movement was successful</returns>
        public string Move()
        {
            if (!HasBeenPlaced)
                return "Robot hasnt been placed";

            if (Direction == CardinalDirection.North)
                PositionY = Math.Min(5, PositionY + 1);
            else if (Direction == CardinalDirection.East)
                PositionX = Math.Min(5, PositionX + 1);
            else if (Direction == CardinalDirection.South)
                PositionY = Math.Max(0, PositionY - 1);
            else if (Direction == CardinalDirection.West)
                PositionX = Math.Max(0, PositionX - 1);

            return $"Moved forwards:{PositionX},{PositionY}";
        }

        public string Left()
        {
            Direction--;
            return "Rotated to the LEFT";
        }

        public string Right()
        {
            Direction++;
            return "Rotated to the RIGHT";
        }

        public string Report()
        {
            return $"\n{Direction}\n" +
                $"X:{PositionX}\n" +
                $"Y:{PositionY}";
        }

    }

    enum CardinalDirection
    {
        North,
        East,
        South,
        West
    }
}
