using SafeAuto.Kata.Data;
using SafeAuto.Kata.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SafeAuto.Kata.Services
{
    public class PrintService : IPrintService
    {
        public void PrintDriverTripDetails(List<Driver> drivers)
        {
            // sort by total distance traveled
            foreach(var driver in drivers.OrderByDescending(t => t.TotalDistanceTraveled))
            {
                var printString = $"{driver.UserName}: { Convert.ToInt32(driver.TotalDistanceTraveled) } miles";
                printString += driver.AvgSpeedInMph > 0 ? $" @ { Convert.ToInt32(driver.AvgSpeedInMph) } mph" : string.Empty;

                // output to console
                Console.WriteLine(printString);
            }
        }
    }
}
