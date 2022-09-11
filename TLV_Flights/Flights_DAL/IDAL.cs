using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flights_DAL;
using Flights_BE;

namespace Flights_DAL
{
    public interface IDAL
    {
        #region User
        public bool CheckUserAndPassword(string user, string password);
        public User CreateUser(String userName, String password);
        public void UpdatePassword(String userName, String password);
        public User GetUserByUsername(String userName);
        public List<User> GetAllUsers();
        public List<User> GetUsersAndPasswords();
        User UserLogin(User user);
        public void AddFlightToHistory(User user, FlightInfoPartial flight);
        #endregion

        #region Flights
        public Dictionary<string, List<FlightInfoPartial>> GetCurrentFlights();
        public FlightInfo GetSingleFlight(string Id);
        public Dictionary<DateTime, FlightInfoPartial> GetFlightsHistory(String userName);
        #endregion

        #region Holidays
        string getHoliday();
        #endregion

        bool saveChanges(User _user);
    }
}
