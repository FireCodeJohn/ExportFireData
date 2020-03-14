﻿using System;
using System.IO;

namespace ExportFireData
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Please enter a date range with a starting date and an ending date");
                Console.WriteLine("");

                // Get starting date
                Console.WriteLine("Enter a starting date: ");
                DateTime startingDate = ReadDate();
                Console.WriteLine("");

                // Get ending Date
                Console.WriteLine("Enter an ending date: ");
                DateTime endingDate = ReadDate();
                Console.WriteLine("");

                // Get output file directory
                Console.WriteLine("Enter your output directory: ");
                DirectoryInfo outputDir = ReadOutDir();
                Console.WriteLine();

                // Test, then exit or go again?
                Console.WriteLine("Will get data from " + startingDate.Date + " until " + endingDate.Date);
                Console.WriteLine("Will output Json in " + outputDir.FullName);
                Console.WriteLine("");
                Console.WriteLine("Press Esc to exit, Press any other key to go again.");
                ConsoleKeyInfo command = Console.ReadKey();
                if (command.Key == ConsoleKey.Escape)
                    return;
                Console.WriteLine("");
            }
        }

        private static DateTime ReadDate()
        {
            DateTime userDateTime;
            if (DateTime.TryParse(Console.ReadLine(), out userDateTime))
            {
                return userDateTime;
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("You have entered an invalid date.  Try again...");
                return ReadDate();
            }
        }

        private static DirectoryInfo ReadOutDir()
        {
            string dir = Console.ReadLine();
            try
            {
                return Directory.CreateDirectory(dir);
            }
            catch(Exception e)
            {
                Console.WriteLine("");
                Console.WriteLine("Exception Caught: " + e.Message);
                Console.WriteLine("");
                Console.WriteLine("Try again... enter your output directory");
                return ReadOutDir();
            }
        }
    }
}
