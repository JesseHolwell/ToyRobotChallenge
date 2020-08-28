using System;

namespace ToyRobotChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Toy Robot Challenge - Jesse Holwell - 2020");

            string command = string.Empty;

            //var tabletop = new Tabletop();
            IBoardEntity entity = new Robot();
            //tabletop.Entity = entity;

            while (!ExitCommand(command))
            {
                command = Console.ReadLine();

                //if its a valid command

                Console.WriteLine(entity.Report());

                Console.WriteLine(entity.Place());

                Console.WriteLine(entity.Report());

                Console.WriteLine(entity.Move());

                Console.WriteLine(entity.Report());

                Console.WriteLine(entity.Right());

                Console.WriteLine(entity.Report());

                Console.WriteLine(entity.Move());

                Console.WriteLine(entity.Report());

                Console.WriteLine(entity.Left());

                Console.WriteLine(entity.Report());

                Console.WriteLine(entity.Move());

                Console.WriteLine(entity.Report());
            }
        }

        private static bool ExitCommand(string command)
        {
            return string.Equals(command, "exit");                
        }
    }
}
