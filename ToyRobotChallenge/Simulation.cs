﻿using System;
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

        /// <summary>
        /// Executes all files found in instructions.txt and outputs the reponse
        /// </summary>
        /// <returns></returns>
        public string Run()
        {
            var response = string.Empty;

            var logFile = File.ReadAllLines(instructionsFile);

            var commands = new CommandParser(entity);

            foreach (var v in logFile)
            {
                if (!string.IsNullOrEmpty(v))
                    response += $"{v}\n{commands.Execute(v)}\n";
            }

            return response;
        }
    }
}
