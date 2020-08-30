using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ToyRobotChallenge
{
    public class CommandParser
    {
        private readonly IBoardEntity entity;

        public CommandParser(IBoardEntity entity)
        {
            this.entity = entity;
        }

        /// <summary>
        /// Check that the given command is valid, and if so executes it
        /// </summary>
        /// <param name="command">Command to pass to the robot</param>
        /// <returns>Returns a status update from the robot</returns>
        public string Execute(string command)
        {
            if (!IsValidCommand(command))
                return "Not a valid command";

            if (command.Equals(Commands.Move, StringComparison.CurrentCultureIgnoreCase))
                return entity.Move();
            else if (command.Equals(Commands.Left, StringComparison.CurrentCultureIgnoreCase))
                return entity.Left();
            else if (command.Equals(Commands.Right, StringComparison.CurrentCultureIgnoreCase))
                return entity.Right();
            else if (command.Equals(Commands.Report, StringComparison.CurrentCultureIgnoreCase))
                return entity.Report();
            else if (command.Equals(Commands.Place, StringComparison.CurrentCultureIgnoreCase))
                return entity.Place();
            else if (IsValidPlaceCommand(command))
            {
                var start = command.IndexOf("(");
                var end = command.IndexOf(")");

                var numbers = command.Substring(start +1, end - start-1);
                var numberParts = numbers.Split(',').ToArray();
                return entity.Place(int.Parse(numberParts[0]), int.Parse(numberParts[1]));
            }
            else
                throw new ArgumentException($"Command '{command}' is valid but could not be matched to an entity action");
        }

        /// <summary>
        /// Returns true if the command exists in the Commands object
        /// </summary>
        private bool IsValidCommand(string command)
        {
            return typeof(Commands).GetAllPublicConstantValues<string>().Contains(command.ToUpper())
                || IsValidPlaceCommand(command);
        }

        /// <summary>
        /// Returns true if command matches the Place(0,0) pattern
        /// </summary>
        private bool IsValidPlaceCommand(string command)
        {
            //is match
            //PLACE(0, 0)
            //PLACE(1234, 1234)
            //place(1, 2)

            //not match
            //PLACE(x, y)
            //PLACE()
            //PLACE(0, i)
            //PLACE(i, 0)
            //PLACE(-1,-1)
            //PLACE
            //1234PLACE(0, 0)
            //abcdplace(12, 12)

            var regex = new Regex(@"(?i)(\bplace\b)[(]\d+,\d+[)]");
            return regex.IsMatch(command);
        }
    }

    /// <summary>
    /// List of available commands that the robot can process
    /// </summary>
    internal static class Commands
    {
        public const string Place = "PLACE";
        public const string Move = "MOVE";
        public const string Left = "LEFT";
        public const string Right = "RIGHT";
        public const string Report = "REPORT";
    }

}
