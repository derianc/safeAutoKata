using System;
using System.Collections.Generic;

namespace SafeAuto.Kata.Data
{
    public class Driver
    {
        public string Name { get; set; }
        public double TotalDistanceTraveled { get; set; }
        public double TotalTripTime { get; set; }
        public double AvgSpeedInMph
        {
            get
            {
                try
                {
                    if (TotalDistanceTraveled > 0 && TotalTripTime > 0)
                        return TotalDistanceTraveled / TotalTripTime;
                    return 0;
                }
                catch
                {
                    return 0;
                }
            }
        }

        // auto initialize trip list collection
        public List<Trip> TripList { get; set; } = new List<Trip>();
    }
}
