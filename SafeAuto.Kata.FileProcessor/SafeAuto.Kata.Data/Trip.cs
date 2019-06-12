using System;

namespace SafeAuto.Kata.Data
{
    public class Trip
    {
        public DateTime? StartTime { get; set; }
        public DateTime? StopTime { get; set; }
        public double? MilesDriven { get; set; }
        public double? TripTime
        {
            get
            {
                if (StopTime.HasValue && StartTime.HasValue)
                    return (StopTime.Value - StartTime.Value).TotalHours;
                return 0;
            }
        }
    }
}
