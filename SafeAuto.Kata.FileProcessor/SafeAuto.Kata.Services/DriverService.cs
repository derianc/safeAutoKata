using Microsoft.Extensions.Logging;
using SafeAuto.Kata.Data;
using SafeAuto.Kata.Repositories.Interfaces;
using SafeAuto.Kata.Services.Extensions;
using SafeAuto.Kata.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace SafeAuto.Kata.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _userRepository;
        private readonly ILogger<DriverService> _logger;

        public DriverService(IDriverRepository userRepository,
                             ILogger<DriverService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public List<Driver> RegisterDrivers(string[] lines)
        {
            List<Driver> drivers = new List<Driver>();

            foreach (var line in lines)
            {
                var lineData = line.Split(' ');

                // verify correct number of columns
                lineData.IsCorrectFormatForNewUser(_logger);

                // if user has not already been registered, add to repo
                var userName = lineData[1].Trim();
                if (!IsDriverRegistered(userName))
                {
                    _logger.LogDebug($"Registering new user: {userName}");

                    var newDriver = new Driver
                    {
                        Name = userName
                    };

                    var driver = _userRepository.RegisterNewDriver(newDriver);
                    drivers.Add(driver);
                }
            }

            return drivers;
        }

        public void AddDriverTripDetails(string[] lines)
        {
            foreach (var line in lines)
            {
                var lineData = line.Split(' ');

                // verify we have the correct number of columns
                lineData.IsCorrectFormatForTripDetails(_logger);

                var driverName = lineData[1].Trim();
                var startTime = lineData[2].Trim();
                var stopTime = lineData[3].Trim();
                var milesDriven = lineData[4].Trim();

                // only add trip details for an existing user
                var existingUser = GetRegisteredDriver(driverName);
                if (existingUser != null)
                {
                    var tripDetails = new Trip
                    {
                        DriverName = driverName,
                        StartTime = DateTime.ParseExact(startTime, "HH:mm", System.Globalization.CultureInfo.InvariantCulture),
                        StopTime = DateTime.ParseExact(stopTime, "HH:mm", System.Globalization.CultureInfo.InvariantCulture),
                        MilesDriven = Convert.ToDouble(milesDriven)
                    };

                    _logger.LogDebug($"Adding trip details for user: {driverName} || {startTime} || {stopTime} || {milesDriven}");
                    existingUser.TripList.Add(tripDetails);
                }
                else
                {
                    _logger.LogError($"Cannot add details for a non-existing user: {driverName}");
                    throw new ArgumentException($"Cannot add trip details for a non-existant user: {driverName}");
                }
            }
        }

        public List<Driver> CalculateDriverTripDetails()
        {
            var driverList = GetAllRegisteredDrivers();

            foreach (var driver in driverList)
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

                            driver.TotalDistanceTraveled += trip.MilesDriven;
                            driver.TotalTripTime += trip.TripTime;
                        }
                        else
                            _logger.LogInformation("Trip does not meet speed criteria");
                    }
                }
            }

            return driverList;
        }

        public List<Driver> GetAllRegisteredDrivers()
        {
            return _userRepository.GetAllRegisteredDrivers();
        }

        public Driver GetRegisteredDriver(string userName)
        {
            return _userRepository.GetRegisteredDriver(userName);
        }

        public bool IsDriverRegistered(string userName)
        {
            return _userRepository.IsDriverRegistered(userName);
        }
    }
}
