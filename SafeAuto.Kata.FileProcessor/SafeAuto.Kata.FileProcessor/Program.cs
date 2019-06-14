using System;
using System.IO;
using SafeAuto.Kata.FileProcessor.Config;

namespace SafeAuto.Kata.FileProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            // configure DI Containers
            var serviceProvider = Setup.ConfigureDependencyInjection();

            var fileReaderService = serviceProvider.GetFileReaderService();
            var printService = serviceProvider.GetPrintService();
            var driverService = serviceProvider.GetDriverService();

            Console.Write("Enter File Path: ");
            var fp = Console.ReadLine();
            Console.WriteLine();

            if (File.Exists(fp))
            {
                // process input file
                var inputFileDetails = fileReaderService.ProcessFile(fp);

                // register drivers
                driverService.RegisterDrivers(inputFileDetails.DriverDetails);

                // add driver trip details
                driverService.AddDriverTripDetails(inputFileDetails.TripDetails);

                // cacluclate driver trip details
                var driverTripDetails = driverService.CalculateDriverTripDetails();

                // get print output
                printService.PrintDriverTripDetails(driverTripDetails);

                // await input
                Console.ReadLine();
            }
            else
                throw new FileNotFoundException($"file, {fp}, not found");
        }
    }
}
