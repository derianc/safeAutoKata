using SafeAuto.Kata.Data;
using System.Collections.Generic;

namespace SafeAuto.Kata.Services.Interfaces
{
    public interface ITripCalculatorService
    {
        List<Driver> CalculateDriverTripDetails();
    }
}
