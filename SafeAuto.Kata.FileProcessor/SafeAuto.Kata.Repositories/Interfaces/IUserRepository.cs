using System.Collections.Generic;
using SafeAuto.Kata.Data;

namespace SafeAuto.Kata.Repositories.Interfaces
{
    public interface IUserRepository
    {
        bool IsUserRegistered(string userName);
        void RegisterNewUser(string userName);
        void AddUserDetails(TripDetails tripDetails);
        RegisteredUser GetRegisteredUser(string userName);
        List<RegisteredUser> GetAllRegisteredUsers();
    }
}
