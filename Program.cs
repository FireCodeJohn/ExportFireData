using System;
using System.IO;
using System.Collections.Generic;
using ExportFireData.BusinessLogic;
using ExportFireData.BusinessObject;

namespace ExportFireData
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                DateTime startingDate, endingDate; // starting and ending dates
                DirectoryInfo outputDir; // output directory
                ResponseIncident? respIncEnum = null; // Export all responses or export all incidencts

                // Get starting date
                Console.WriteLine("Please enter a date range with a starting date and an ending date");
                Console.WriteLine("");             
                Console.WriteLine("Enter a starting date (inclusive): ");
                startingDate = ReadDate();
                Console.WriteLine("");

                // Get ending Date
                Console.WriteLine("Enter an ending date (inclusive): ");
                endingDate = ReadDate();
                Console.WriteLine("");

                // Export all responses or all incidents
                while (respIncEnum == null)
                {
                    Console.WriteLine("Export all responses or export all incidents?");
                    Console.WriteLine("Press r for export all responses and i for export all incidents...");
                    ConsoleKey key = Console.ReadKey().Key;
                    if (key == ConsoleKey.R)
                        respIncEnum = ResponseIncident.Response;
                    else if (key == ConsoleKey.I)
                        respIncEnum = ResponseIncident.Incident;
                    else
                    {
                        Console.WriteLine("");
                        Console.WriteLine("User did not press r or i, try again.");
                        Console.WriteLine("");
                    }
                }
                Console.WriteLine("");
                Console.WriteLine("");

                // Get output file directory
                Console.WriteLine("Enter your output directory: ");
                outputDir = ReadOutDir();
                Console.WriteLine();       

                // Tell user what settings they selected, give chance to re-enter
                string respIncStr;
                if (respIncEnum == ResponseIncident.Incident)
                    respIncStr = "Incidents";
                else
                    respIncStr = "Responses";
                Console.WriteLine("Will get data from {0}/{1}/{2} until {3}/{4}/{5}", startingDate.Month, startingDate.Day, startingDate.Year, endingDate.Month, endingDate.Day, endingDate.Year);
                Console.WriteLine(string.Format("Will export all {0} to {1}", respIncStr, outputDir.FullName));
                Console.WriteLine("");
                Console.WriteLine("Press any key to export data from the SF data repository. Press Esc to re-enter data.");
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("");
                    Console.WriteLine("");
                    continue;
                }

                // Get data from SF data repository
                Console.WriteLine("");
                Console.WriteLine("Getting data from SF data repository...");
                Console.WriteLine("");
                List<Response> responses = DataImportManager.GetData_SFRepo_Https(startingDate, endingDate, 0);
                if (responses == null)
                {
                    Console.WriteLine("Got null response.  Try again...");
                    Console.WriteLine("");
                    continue;
                }

                // Write Output to JSON files, ask if user wants to do another operation
                Console.WriteLine("Found {0} Results", responses.Count);
                Console.WriteLine("");
                Console.WriteLine("Creating json output...");
                Console.WriteLine("");
                JsonOutput.WriteJsonFiles(outputDir, responses, (ResponseIncident) respIncEnum);
                Console.WriteLine("Press Esc to exit, Press any other key to export more data.");
                if (Console.ReadKey().Key == ConsoleKey.Escape)
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
