using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flights_BL;
using Flights_BE;

namespace Flights_GUI.Model
{
    public class LoginModel
    {
        private readonly BAL _bal;

        public LoginModel()
        {
            _bal = new BAL();
        }

        public bool CheckUserAndPassword(string user, string password)
        {
            return _bal.CheckUserAndPassword(user, password);
        }

        public void UpdatePassword(string username, string password)
        {
            try
            {
                _bal.UpdatePassword(username, password);
            }
            catch(Exception e)
            {
                throw new NoUserException(username);
            }

        }
        public User CreateUser(string username, string password)
        {
            try
            {
                return _bal.CreateUser(username, password);
            }
            catch(Exception)
            {
                throw new UserExistsException(username);
            }
        }

        public User GetUserByUsername(String userName)
        {
            try
            {
                return _bal.GetUserByUsername(userName);
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
                return _bal.GetAllUsers();
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
                return _bal.GetAllUsers();
            }
            catch (Exception)
            {

                throw new ZeroUsersException();
            }
        }

        public void AddFlightToHistory(User user, FlightInfoPartial flight)
        {
            _bal.AddFlightToHistory(user, flight);
        }
    }
}
