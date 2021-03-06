﻿using System;

namespace SafeAuto.Kata.Data
{
    public class Trip
    {
        public string DriverName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
        public double MilesDriven { get; set; }
        public double TripTime
        {
            get
            {
                return (StopTime - StartTime).TotalHours;
            }
        }
    }
}
