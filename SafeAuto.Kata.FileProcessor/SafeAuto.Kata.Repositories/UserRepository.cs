using Microsoft.Extensions.Logging;
using SafeAuto.Kata.Data;
using SafeAuto.Kata.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SafeAuto.Kata.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private List<Driver> _registeredUsers = new List<Driver>();

        public UserRepository(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<UserRepository>();
        }

        public bool IsUserRegistered(string userName)
        {
            // perform case insensitive check
            if (_registeredUsers.Where(u => string.Equals(u.UserName, userName, System.StringComparison.CurrentCultureIgnoreCase)).Count() > 0)
                return true;
            return false;
        }

        public void RegisterNewUser(string userName)
        {
            var newUser = new Driver
            {
                UserName = userName
            };

            _logger.LogDebug($"Adding user to repository: {userName}");

            _registeredUsers.Add(newUser);
        }

        public void AddUserDetails(Trip tripDetails)
        {
            throw new System.NotImplementedException();
        }

        public Driver GetRegisteredUser(string userName)
        {
            _logger.LogDebug($"Returning user, {userName}, from repository");

            // if user is registered, return.  else, return null
            if (IsUserRegistered(userName))
                return _registeredUsers.Where(u => string.Equals(u.UserName, userName, System.StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            _logger.LogWarning($"User, {userName}, not found in repository");
            return null;
        }

        public List<Driver> GetAllRegisteredUsers()
        {
            _logger.LogDebug($"Returning all users");

            return _registeredUsers.ToList();
        }
    }
}
