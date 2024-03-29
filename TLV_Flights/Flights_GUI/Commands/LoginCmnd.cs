﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flights_GUI.Model;
using Flights_GUI.ViewModel;
using Flights_BE;
namespace Flights_GUI.Commands
{
    public class LoginCmnd : CommandBase
    {
        private LoginViewModel loginViewModel;
        public LoginCmnd(LoginViewModel _loginViewModel)
        {
            this.loginViewModel = _loginViewModel;
        }

        public override void Execute(Object paramater)
        {
            var user = paramater as User;
            if (loginViewModel.loginModel.CheckUserAndPassword(user.Username, user.Password))
            {
                loginViewModel.WelcomeMessage = $"Welcome, {user.Username}";
                //ClosewindowCommand = true;
            }
            else
            {
                loginViewModel.ShowInvalidInfo = true;
            }
        }
    }
}


