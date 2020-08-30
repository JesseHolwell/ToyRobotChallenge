using System;

namespace ToyRobotChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Toy Robot Challenge - Jesse Holwell");
            Console.WriteLine("Enter a command to run interactively,\n" +
                "'simulation' to run preprogrammed steps,\n" +
                "'help' for command info,\n" +
                "'exit' to quit >\n");

            string command = string.Empty;

            try
            {
                IBoardEntity entity = new Robot();
                var commands = new CommandParser(entity);
                var sim = new Simulation();

                while (!IsExitCommand(command))
                {
                    command = Console.ReadLine();

                    if (IsHelpCommand(command))
                        Console.WriteLine(HelpText);
                    else if (IsSimulationCommand(command))
                        Console.WriteLine(sim.Run());
                    else
                        Console.WriteLine(commands.Execute(command));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }

        private static bool IsExitCommand(string command)
        {
            return command.ToUpper().Equals("exit", StringComparison.CurrentCultureIgnoreCase);
        }

        private static bool IsHelpCommand(string command)
        {
            return command.ToUpper().Equals("help", StringComparison.CurrentCultureIgnoreCase);
        }

        private static bool IsSimulationCommand(string command)
        {
            return command.ToUpper().Equals("simulation", StringComparison.CurrentCultureIgnoreCase);
        }

        private static string HelpText =
            $"Commands are case insensitive\n" +
            $"Board size is {Tabletop.SizeX} x {Tabletop.SizeY}\n" +
            $"Available commands:\n" +
            $"PLACE(x,y)\n" +
            string.Join("\n", typeof(Commands).GetAllPublicConstantValues<string>());


    }
}
