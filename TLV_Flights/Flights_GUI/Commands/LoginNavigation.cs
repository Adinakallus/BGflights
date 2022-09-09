using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Flights_GUI.Model;
using Flights_GUI.ViewModel;
using Flights_BE;

namespace Flights_GUI.Commands
{
    public class LoginNavigation:ICommand
    {
        private LoginViewModel loginViewModel;
        public LoginNavigation(LoginViewModel loginViewModel)
        {
            this.loginViewModel = loginViewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            this.loginViewModel.LoggedIn = false;

        }
    }
}
