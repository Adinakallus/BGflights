using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flights_GUI.ViewModel;

namespace Flights_GUI.Commands
{
    public class SignUPCmnd:CommandBase
    {
        private LoginViewModel loginViewModel;
        public SignUPCmnd(LoginViewModel loginViewModel)
        {
            this.loginViewModel = loginViewModel;
        }
        public override void Execute(object paramater)
        {
            throw new NotImplementedException();    
        }

    }
}
