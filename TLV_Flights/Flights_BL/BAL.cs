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

        public void CreateUser(String userName, String password)//return user?
        {
            try
            {
                if (_dal.GetAllUsers().Find(u => u.Username == userName) == null)
                    _dal.CreateUser(userName, password);
                else
                    throw new Exception();   
            }
            catch (Exception)
            {
                throw new UserExistsException(userName);
            }
        }

        public void UpdatePassword(String userName, String password)
        {

        }


        public User GetUserByUsername(String userName)
        {

        }

        public List<User> GetAllUsers()
        {

        }

        public void AddFlightToHistory(User user, FlightInfoPartial flight)
        {

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

        public async Task<OpenWeather.Weather> GetWeather(FlightInfo.Airport airport)
        {
            return await _dal.GetWeather(airport);
        }

        public async Task<HebCal.Item> GetHebDate(DateTime date) //by current date?
        {
            return  await _dal.GetHebDate(date);
        }

        public async Task<bool> isErevChag()
        {
            for(int i=0; i < 7; i++)
            {
                HebCal.Item hebDate = await GetHebDate(DateTime.Today.AddDays(i));
                if (hebDate.title.StartsWith("Erev"))
                    return true;
            }
            return false;
        }

}
}
