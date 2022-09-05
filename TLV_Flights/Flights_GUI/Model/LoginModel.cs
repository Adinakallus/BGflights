using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flights_BL;

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
    }
}
