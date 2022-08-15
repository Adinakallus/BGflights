using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flights_DAL;

namespace Flights_BL
{
    public class BAL
    {
        private readonly Dal _dal = new();


        public bool CheckUserAndPassword(string user, string password)
        {
            var usersFromDb = _dal.GetUsersAndPasswords();
            return usersFromDb.Any(userFromDb => userFromDb.Username == user && userFromDb.Password == password);
        }
    }
}
