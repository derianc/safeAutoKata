using Microsoft.Extensions.Logging;
using SafeAuto.Kata.Data;
using SafeAuto.Kata.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SafeAuto.Kata.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly ILogger<DriverRepository> _logger;
        private List<Driver> _registeredDrivers = new List<Driver>();

        public DriverRepository(ILogger<DriverRepository> logger)
        {
            _logger = logger;
        }

        public bool IsDriverRegistered(string userName)
        {
            // perform case insensitive check
            if (_registeredDrivers.Where(u => string.Equals(u.Name, userName, System.StringComparison.CurrentCultureIgnoreCase)).Count() > 0)
                return true;
            return false;
        }

        public Driver RegisterNewDriver(Driver driver)
        {
            _logger.LogDebug($"Adding user to repository: {driver.Name}");

            _registeredDrivers.Add(driver);

            return driver;
        }

        public void AddDriverDetails(Trip tripDetails)
        {
            throw new System.NotImplementedException();
        }

        public Driver GetRegisteredDriver(string userName)
        {
            _logger.LogDebug($"Returning user, {userName}, from repository");

            // if user is registered, return.  else, return null
            if (IsDriverRegistered(userName))
                return _registeredDrivers.Where(u => string.Equals(u.Name, userName, System.StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            _logger.LogWarning($"User, {userName}, not found in repository");
            return null;
        }

        public List<Driver> GetAllRegisteredDrivers()
        {
            _logger.LogDebug($"Returning all users");

            return _registeredDrivers.ToList();
        }
    }
}
