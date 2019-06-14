using SafeAuto.Kata.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SafeAuto.Kata.Services.Interfaces
{
    public interface IDriverService
    {
        bool IsDriverRegistered(string userName);
        List<Driver> RegisterDrivers(string[] lines);
        void AddDriverTripDetails(string[] lines);
        Driver GetRegisteredDriver(string userName);
        List<Driver> GetAllRegisteredDrivers();
        List<Driver> CalculateDriverTripDetails();
    }
}
