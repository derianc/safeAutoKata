using Microsoft.Extensions.Logging;
using SafeAuto.Kata.Data;
using SafeAuto.Kata.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SafeAuto.Kata.Services
{
    public class PrintService : IPrintService
    {
        private readonly ILogger<PrintService> _logger;

        public PrintService(ILogger<PrintService> logger)
        {
            _logger = logger;
        }

        public void PrintDriverTripDetails(List<Driver> drivers)
        {
            _logger.LogDebug("Printing results");

            // sort by total distance traveled
            foreach(var driver in drivers.OrderByDescending(t => t.TotalDistanceTraveled))
            {
                var printString = $"{driver.Name}: { Convert.ToInt32(driver.TotalDistanceTraveled) } miles";
                printString += driver.AvgSpeedInMph > 0 ? $" @ { Convert.ToInt32(driver.AvgSpeedInMph) } mph" : string.Empty;

                // output to console
                Console.WriteLine(printString);
            }
        }
    }
}
