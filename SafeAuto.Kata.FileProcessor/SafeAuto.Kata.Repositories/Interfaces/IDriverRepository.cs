using System.Collections.Generic;
using SafeAuto.Kata.Data;

namespace SafeAuto.Kata.Repositories.Interfaces
{
    public interface IDriverRepository
    {
        bool IsDriverRegistered(string userName);
        Driver RegisterNewDriver(Driver userName);
        void AddDriverDetails(Trip tripDetails);
        Driver GetRegisteredDriver(string userName);
        List<Driver> GetAllRegisteredDrivers();
    }
}
