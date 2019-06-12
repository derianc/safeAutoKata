using Microsoft.Extensions.Logging;
using SafeAuto.Kata.Data;
using SafeAuto.Kata.Repositories.Interfaces;
using SafeAuto.Kata.Services.Extensions;
using SafeAuto.Kata.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace SafeAuto.Kata.Services
{
    public class TripCalculatorService : ITripCalculatorService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<TripCalculatorService> _logger;

        public TripCalculatorService(IUserRepository userRepository,
                                     ILoggerFactory loggerFactory)
        {
            _userRepository = userRepository;
            _logger = loggerFactory.CreateLogger<TripCalculatorService>();
        }

        public List<Driver> CalculateDriverTripDetails()
        {
            var driverList = _userRepository.GetAllRegisteredUsers();

            foreach(var driver in driverList)
            {
                if (driver.TripList.Count > 0)
                {
                    foreach (var trip in driver.TripList)
                    {
                        // verify avg trip speed > 5 and < 100
                        _logger.LogDebug("Verify trip meets speed criteria");

                        if (trip.IsValidSpeed())
                        {
                            _logger.LogDebug("Calculating trip details");

                            driver.TotalDistanceTraveled += trip.MilesDriven.Value;
                            driver.TotalTripTime += trip.TripTime.Value;
                        }
                        else
                            _logger.LogWarning("Trip does not meet speed criteria");
                    }
                }
            }

            return driverList;
        }
    }
}
