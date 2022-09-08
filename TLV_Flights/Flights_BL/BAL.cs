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

        public User CreateUser(String userName, String password)//return user?
        {
            try
            {
                if (_dal.GetAllUsers().Find(u => u.Username == userName) == null)
                  return  _dal.CreateUser(userName, password);
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
            try
            {
                _dal.UpdatePassword(userName, password);
            }
            catch(Exception)
            {
                throw new NoUserException(userName);
            }
        }


        public User GetUserByUsername(String userName)
        {
            try
            {
                return _dal.GetUserByUsername(userName);
            }
            catch (Exception)
            {

                throw new NoUserException(userName);
            }
        }

        public List<User> GetAllUsers()
        {
            try
            {
                return _dal.GetAllUsers();
            }
            catch (Exception)
            {

                throw new ZeroUsersException();
            }
        }

        public List<User> GetUsersAndPasswords()
        {
            try
            {
                return _dal.GetAllUsers();
            }
            catch (Exception)
            {

                throw new ZeroUsersException();
            }
        }

        public void AddFlightToHistory(User user, FlightInfoPartial flight)
        {
            _dal.AddFlightToHistory(user, flight);            
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

        public Dictionary<DateTime, FlightInfoPartial> GetFlightsHistory(String userName)
        {
            try
            {
                return _dal.GetFlightsHistory(userName); 
            }
            catch (Exception)
            {

                throw new NoFlightsException(userName);
            }
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
