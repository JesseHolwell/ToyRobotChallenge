using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ToyRobotChallenge
{
    class Simulation
    {
        private IBoardEntity entity;
        private const string instructionsFile = "instructions.txt";

        public Simulation()
        {
            this.entity = new Robot();
        }

        public string Run()
        {
            var response = string.Empty;

            var logFile = File.ReadAllLines(instructionsFile);
            var logList = new List<string>(logFile);

            var commands = new CommandParser(entity);

            foreach (var v in logFile)
            {
                response += $"{v}\n{commands.Execute(v)}\n";
            }

            return response;
        }
    }
}
