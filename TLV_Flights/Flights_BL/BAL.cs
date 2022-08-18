using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flights_DAL;
using Flights_BE;

namespace Flights_BL
{
    public class BAL
    {
        private readonly Dal _dal = new();
        #region User
        public bool CheckUserAndPassword(string user, string password)
        {
            var usersFromDb = _dal.GetUsersAndPasswords();
            return usersFromDb.Any(userFromDb => userFromDb.Username == user && userFromDb.Password == password);
        }

        #endregion

        #region Flights
        public async Task<List<FlightInfoPartial>> GetFlights()
        {
            return await _dal.GetFlightsFromAPI();
        }

        public async Task<FlightInfo> GetFlightInfo(FlightInfoPartial flight)
        {
            return await _dal.GetFlightInfo(flight);
        }
        #endregion

    }
}
