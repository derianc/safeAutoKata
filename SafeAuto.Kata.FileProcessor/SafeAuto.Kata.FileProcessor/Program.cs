using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SafeAuto.Kata.FileProcessor.Config;
using SafeAuto.Kata.Services.Interfaces;

namespace SafeAuto.Kata.FileProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            // configure DI Containers
            var serviceProvider = Setup.ConfigureDependencyInjection();

            // create concrete classes from
            var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<Program>();
            var fileReaderService = serviceProvider.GetRequiredService<IFileReaderService>();
            var tripCalculatorService = serviceProvider.GetRequiredService<ITripCalculatorService>();
            var printService = serviceProvider.GetRequiredService<IPrintService>();

            logger.LogDebug("Starting Application");
            var fp = @"C:\Project\Sandbox\safeAutoKata\exampleFile.txt";

            Console.Write("Enter File Path: " + fp);
            Console.WriteLine();

            //var fp = Console.ReadLine();

            if (File.Exists(fp))
            {
                // process input file
                fileReaderService.ProcessFile(fp);

                // cacluclate trip details
                var tripDetails = tripCalculatorService.CalculateDistanceAndSpeed();

                // get print output
                var printOutput = printService.PrintOutput(tripDetails);

                // print to console.
                foreach (var line in printOutput)
                    Console.WriteLine(line);

                // await input
                Console.ReadLine();
            }
            else
                logger.LogError($"File does not exist, {fp}");
        }
    }
}
