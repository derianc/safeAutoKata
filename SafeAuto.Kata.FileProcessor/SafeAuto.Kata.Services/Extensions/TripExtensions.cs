using SafeAuto.Kata.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SafeAuto.Kata.Services.Extensions
{
    public static class TripExtensions
    {
        public static bool IsAvgSpeedLessThan5Mph(this TripDetails trip)
        {
            var mph = trip.MilesDriven / ((trip.StopTime.Value - trip.StartTime.Value).TotalHours);

            if (mph < 5)
                return true;
            return false;
        }

        public static bool IsAvgSpeedGreaterThan100Mph(this TripDetails trip)
        {
            var mph = trip.MilesDriven / ((trip.StopTime.Value - trip.StartTime.Value).TotalHours);

            if (mph > 100)
                return true;
            return false;
        }
    }
}
