using Microsoft.Extensions.Logging;
using SafeAuto.Kata.Data;
using System;

namespace SafeAuto.Kata.Services.Extensions
{
    public static class ValidationExtensions
    {
        public static bool IsCorrectFormatForNewUser(this string[] line, ILogger<DriverService> logger)
        {
            if (line.Length == 2)
                return true;

            logger.LogError($"Formatting Error: {line}");
            throw new FormatException("Formatting Error");
        }

        public static bool IsCorrectFormatForTripDetails(this string[] line, ILogger<DriverService> logger)
        {
            if (line.Length == 5)
                return true;

            logger.LogError($"Formatting Error: {line}");
            throw new FormatException("Formatting Error");
        }

        public static bool IsValidSpeed(this Trip trip)
        {
            return trip.IsAvgSpeedGreaterThan5Mph() && trip.IsAvgSpeedLessThan100Mph();
        }

        private static bool IsAvgSpeedGreaterThan5Mph(this Trip trip)
        {
            var mph = trip.MilesDriven / trip.TripTime;

            if (mph > 5)
                return true;
            return false;
        }

        private static bool IsAvgSpeedLessThan100Mph(this Trip trip)
        {
            var mph = trip.MilesDriven / trip.TripTime;

            if (mph < 100)
                return true;
            return false;
        }
    }
}
