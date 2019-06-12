using SafeAuto.Kata.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SafeAuto.Kata.Services.Interfaces
{
    public interface IValidationService
    {
        bool IsTripValid(TripDetails tripDetails);
    }
}
