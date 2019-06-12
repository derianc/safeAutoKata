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

            var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<Program>();
            var fileReaderService = serviceProvider.GetFileReaderService();
            var tripCalcService = serviceProvider.GetTripCalculatorService();
            var printService = serviceProvider.GetPrintService();

            logger.LogDebug("Starting Application");
            //var fp = @"C:\Project\Sandbox\safeAutoKata\exampleFile.txt";

            Console.Write("Enter File Path: ");
            var fp = Console.ReadLine();
            Console.WriteLine();

            if (File.Exists(fp))
            {
                // process input file
                fileReaderService.ProcessFile(fp);

                // cacluclate trip details
                var driverTripDetails = tripCalcService.CalculateDriverTripDetails();

                // get print output
                printService.PrintDriverTripDetails(driverTripDetails);

                // await input
                Console.ReadLine();
            }
            else
                logger.LogError($"File does not exist, {fp}");
        }
    }
}
