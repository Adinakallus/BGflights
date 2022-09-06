using Flights_GUI.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Flights_GUI.Commands;

namespace Flights_GUI.ViewModel
{
    public class LoginViewModel:ViewModelBase
    {

        #region constructer
        public LoginViewModel()
        {
            LoginCommand = new LoginCmnd(this);
        }

        #endregion
        #region properties
        private bool isLogin;
        public bool IsLogin
        {
            get { return isLogin; }
            set
            {
                isLogin = value;
                OnPropertyChanged(nameof(IsLogin));
            }
        }

        private string username;
        public string Username
        {
            get { return this.username; }
            set
            {
                this.username = value;
                OnPropertyChanged(nameof(this.username));
            }
        }
        private int password;
        public int Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged(nameof(password));
            }
        }
        #endregion

        public ICommand LoginCommand { get; set; } 
    }
}
