using SafeAuto.Kata.Data;
using SafeAuto.Kata.Services.Extensions;
using SafeAuto.Kata.Services.Interfaces;

namespace SafeAuto.Kata.Services
{
    public class ValidationService : IValidationService
    {
        public bool IsTripValid(TripDetails tripDetails)
        {
            // if avg speed is less than 5 or greater than 100, trip is invalid
            return !tripDetails.IsAvgSpeedLessThan5Mph() && 
                   !tripDetails.IsAvgSpeedGreaterThan100Mph();
                           
        }
    }
}
