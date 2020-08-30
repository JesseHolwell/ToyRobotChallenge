using System;
using System.Linq;

namespace ToyRobotChallenge
{
    public interface IBoardEntity
    {
        string Left();
        string Move();
        string Place(int? x = null, int? y = null);
        string Report();
        string Right();
    }

    class Robot : IBoardEntity
    {
        private const string NotPlacedText = "Robot hasnt been placed";
        private const string OutOfBoundsText = "Cannot place robot out of bounds";

        /// <summary>
        /// The direciton the robot is currently facing
        /// </summary>
        private CardinalDirection _direction { get; set; }
        private CardinalDirection Direction
        {
            get
            {
                return _direction;
            }
            set
            {
                //a circular loop to enable --/++ operations;
                if ((int)value < Enum.GetValues(typeof(CardinalDirection)).Cast<int>().Min())
                    _direction = Enum.GetValues(typeof(CardinalDirection)).Cast<CardinalDirection>().Max();
                else if ((int)value > Enum.GetValues(typeof(CardinalDirection)).Cast<int>().Max())
                    _direction = Enum.GetValues(typeof(CardinalDirection)).Cast<CardinalDirection>().Min();
                else
                    _direction = value;
            }
        }

        /// <summary>
        /// The robots current position on the X axis
        /// </summary>
        private int PositionX { get; set; }

        /// <summary>
        /// The robots current position on the Y axis
        /// </summary>
        private int PositionY { get; set; }

        /// <summary>
        /// Returns true if the robot has been placed on the tabletop
        /// </summary>
        private bool HasBeenPlaced { get; set; }

        /// <summary>
        /// Place the robot on the tabletop
        /// </summary>
        public string Place(int? x, int? y)
        {
            if (x < 0 || x > Tabletop.SizeX
                || y < 0 || y > Tabletop.SizeY)
                return OutOfBoundsText;

            PositionX = x ?? 0;
            PositionY = y ?? 0;

            HasBeenPlaced = true;

            return $"Placed at {PositionX},{PositionY}";
        }

        /// <summary>
        /// Move the robot one step in the direction its facing
        /// </summary>
        public string Move()
        {
            if (!HasBeenPlaced)
                return NotPlacedText;

            if (Direction == CardinalDirection.North)
                PositionY = Math.Min(5, PositionY + 1);
            else if (Direction == CardinalDirection.East)
                PositionX = Math.Min(5, PositionX + 1);
            else if (Direction == CardinalDirection.South)
                PositionY = Math.Max(0, PositionY - 1);
            else if (Direction == CardinalDirection.West)
                PositionX = Math.Max(0, PositionX - 1);

            return $"Moved forwards: {PositionX},{PositionY}";
        }

        /// <summary>
        /// Rotate the robot to the left
        /// </summary>
        public string Left()
        {
            if (!HasBeenPlaced)
                return NotPlacedText;

            Direction--;
            return $"Rotated left: {this.Direction}";
        }

        /// <summary>
        /// Rotate the robot to the right
        /// </summary>
        public string Right()
        {
            if (!HasBeenPlaced)
                return NotPlacedText;

            Direction++;
            return $"Rotated right: {this.Direction}";
        }

        /// <summary>
        /// Report the robots current position on the tabletop
        /// </summary>
        /// <returns></returns>
        public string Report()
        {
            if (!HasBeenPlaced)
                return NotPlacedText;

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
