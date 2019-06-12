using SafeAuto.Kata.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SafeAuto.Kata.Services.Extensions
{
    public static class TripExtensions
    {
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
