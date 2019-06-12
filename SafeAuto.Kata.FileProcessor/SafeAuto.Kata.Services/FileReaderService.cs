using Microsoft.Extensions.Logging;
using SafeAuto.Kata.Data;
using SafeAuto.Kata.Repositories.Interfaces;
using SafeAuto.Kata.Services.Extensions;
using SafeAuto.Kata.Services.Interfaces;
using System;
using System.IO;
using System.Linq;

namespace SafeAuto.Kata.Services
{
    public class FileReaderService : IFileReaderService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<FileReaderService> _logger;

        public FileReaderService(IUserRepository userRepository,
                                 ILoggerFactory loggerFactory)
        {
            _userRepository = userRepository;
            _logger = loggerFactory.CreateLogger<FileReaderService>();
        }

        public void ProcessFile(string fileName)
        {
            _logger.LogDebug($"Processing file: {fileName}");

            // read entire file
            string[] lines = File.ReadAllLines(fileName);

            // decided to go this route in case input file is out of order
            // if trip details appear before user registration, this method always works
            var registrationLines = lines.Where(l => l.Split(' ')[0] == InputType.Driver.ToString()).ToArray();
            var tripDetailLines = lines.Where(l => l.Split(' ')[0] == InputType.Trip.ToString()).ToArray();

            // register users
            RegisterUsers(registrationLines);

            // add trip details
            AddTripDetails(tripDetailLines);
        }

        private void RegisterUsers(string[] lines)
        {
            foreach(var line in lines)
            {
                var lineData = line.Split(' ');

                // verify correct number of columns
                lineData.IsCorrectFormatForNewUser(_logger);

                // if user has not already been registered, add to repo
                var userName = lineData[1].Trim();
                if (!_userRepository.IsUserRegistered(userName))
                {
                    _logger.LogDebug($"Registering new user: {userName}");

                    _userRepository.RegisterNewUser(userName);
                }
            }
        }

        private void AddTripDetails(string[] lines)
        {
            foreach(var line in lines)
            {
                var lineData = line.Split(' ');

                // verify we have the correct number of columns
                lineData.IsCorrectFormatForTripDetails(_logger);

                var userName = lineData[1].Trim();
                var startTime = lineData[2].Trim();
                var stopTime = lineData[3].Trim();
                var milesDriven = lineData[4].Trim();

                // only add trip details for an existing user
                var existingUser = _userRepository.GetRegisteredUser(userName);
                if (existingUser != null)
                {
                    var tripDetails = new Trip
                    {
                        StartTime = DateTime.ParseExact(startTime, "HH:mm", System.Globalization.CultureInfo.InvariantCulture),
                        StopTime = DateTime.ParseExact(stopTime, "HH:mm", System.Globalization.CultureInfo.InvariantCulture),
                        MilesDriven = Convert.ToDouble(milesDriven)
                    };

                    _logger.LogDebug($"Adding trip details for user: {userName} || {startTime} || {stopTime} || {milesDriven}");
                    existingUser.TripList.Add(tripDetails);
                }
                else
                {
                    _logger.LogError($"Cannot add details for a non-existing user: {userName}");
                    throw new ArgumentException($"Cannot add trip details for a non-existant user: {userName}");
                }
            }
        }
    }
}
