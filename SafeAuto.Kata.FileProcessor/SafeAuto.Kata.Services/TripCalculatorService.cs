using Microsoft.Extensions.Logging;
using SafeAuto.Kata.Data;
using SafeAuto.Kata.Repositories.Interfaces;
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

        public List<Output> CalculateDistanceAndSpeed()
        {
            List<Output> outputList = new List<Output>();

            foreach(var user in _userRepository.GetAllRegisteredUsers())
            {
                double? totalTime = 0;
                double? totalDistance = 0;

                if (user.TripDetails.Count > 0)
                {
                    foreach (var detail in user.TripDetails)
                    {
                        totalDistance += detail.MilesDriven;
                        totalTime += (detail.StopTime.Value - detail.StartTime.Value).TotalHours;
                    }

                    var mph = totalDistance.Value / totalTime.Value;

                    var output = new Output()
                    {
                        UserName = user.UserName,
                        DistanceTraveled = Convert.ToInt32(totalDistance.Value),
                        MilesPerHour = Convert.ToInt32(mph)
                    };

                    outputList.Add(output);
                }
                else
                    outputList.Add(new Output { UserName = user.UserName });
            }

            return outputList;
        }
    }
}
