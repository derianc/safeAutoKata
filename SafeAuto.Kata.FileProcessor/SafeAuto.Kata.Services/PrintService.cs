using SafeAuto.Kata.Data;
using SafeAuto.Kata.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SafeAuto.Kata.Services
{
    public class PrintService : IPrintService
    {
        public List<string> PrintOutput(List<Output> tripDetails)
        {
            var retVal = new List<string>();

            foreach(var trip in tripDetails.OrderByDescending(t => t.DistanceTraveled))
            {
                var printString = $"{trip.UserName}: {trip.DistanceTraveled} miles ";
                printString += trip.MilesPerHour > 0 ? $"@ { trip.MilesPerHour} mph" : string.Empty;

                retVal.Add(printString);
            }

            return retVal;
        }
    }
}
