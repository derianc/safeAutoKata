using Microsoft.Extensions.Logging;
using SafeAuto.Kata.Data;
using SafeAuto.Kata.Repositories.Interfaces;
using SafeAuto.Kata.Services.Extensions;
using SafeAuto.Kata.Services.Interfaces;
using System;
using System.IO;

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

            // read individual lines.
            string[] lines = File.ReadAllLines(fileName);
            foreach (var line in lines)
                ProcessLine(line);
        }
        
        private void ProcessLine(string line)
        {
            var lineData = line.Split(' ');

            // determine if we're registering a new user or entering trip data
            var controlWord = lineData[0].Trim();
            if (controlWord == InputType.Driver.ToString())
            {
                // verify we have the correct number of columns
                lineData.IsCorrectFormatForNewUser(_logger);

                var userName = lineData[1].Trim();
                if(!_userRepository.IsUserRegistered(userName))
                {
                    _logger.LogDebug($"Registering new user: {userName}");

                    _userRepository.RegisterNewUser(userName);
                }
            }
            else if (controlWord == InputType.Trip.ToString())
            {
                // verify we have the correct number of columns
                lineData.IsCorrectFormatForTripDetails(_logger);

                var userName = lineData[1].Trim();
                var startTime = lineData[2].Trim();
                var stopTime = lineData[3].Trim();
                var milesDriven = lineData[4].Trim();

                var existingUser = _userRepository.GetRegisteredUser(userName);
                if (existingUser != null)
                {
                    var tripDetails = new TripDetails
                    {
                        StartTime = DateTime.ParseExact(startTime, "HH:mm", System.Globalization.CultureInfo.InvariantCulture),
                        StopTime = DateTime.ParseExact(stopTime, "HH:mm", System.Globalization.CultureInfo.InvariantCulture),
                        MilesDriven = Convert.ToDouble(milesDriven)
                    };

                    if (tripDetails.IsTripValid())
                    {
                        _logger.LogDebug($"Adding trip details for user: {userName} || {startTime} || {stopTime} || {milesDriven}");
                        existingUser.TripDetails.Add(tripDetails);
                    }
                    else
                    {
                        _logger.LogWarning($"Invalid trip {tripDetails}");
                    }
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
