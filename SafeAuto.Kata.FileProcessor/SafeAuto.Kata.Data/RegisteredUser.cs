using System.Collections.Generic;

namespace SafeAuto.Kata.Data
{
    public class RegisteredUser
    {
        public string UserName { get; set; }

        // auto initialize trip details collection
        public List<TripDetails> TripDetails { get; set; } = new List<TripDetails>();
    }
}
