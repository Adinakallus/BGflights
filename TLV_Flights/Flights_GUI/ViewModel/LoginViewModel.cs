using Flights_GUI.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Flights_GUI.Commands;
using Flights_GUI.Model;

namespace Flights_GUI.ViewModel
{
    public class LoginViewModel:ViewModelBase
    {
        #region properties
        private bool loggedIn;
        public bool LoggedIn
        {
            get { return loggedIn; }
            set
            {
                loggedIn = value;
                OnPropertyChanged(nameof(LoggedIn));
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
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged(nameof(password));
            }
        }
            private bool isSignUp;
        public bool IsSignUp
        {
            get { return isSignUp; }
            set
            {
                isSignUp = value;
                OnPropertyChanged(nameof(isSignUp));
            }
        }
        private string welcomeMessage;
        public string WelcomeMessage
        {
            get { return welcomeMessage; }
            set
            {
                welcomeMessage = value;
                OnPropertyChanged(nameof(welcomeMessage));
            }
        }
        private bool showInvalidInfo;
        public bool ShowInvalidInfo
        {
            get { return showInvalidInfo; }
            set
            {
                showInvalidInfo = value;
                OnPropertyChanged(nameof(showInvalidInfo));
            }
        }
        #endregion

        public  Model.LoginModel loginModel= new Model.LoginModel();
        #region constructor
        public LoginViewModel()
        {
            LoginCommand = new LoginCmnd(this);
            SignUpCommand = new SignUPCmnd(this);
            LoginNav = new LoginNavigation(this);

            loggedIn = true; 
            isSignUp=false;
            showInvalidInfo=false;
        }

        #endregion

      
        public ICommand LoginCommand { get; set; } 
        public ICommand SignUpCommand { get; set; }    
        public ICommand LoginNav { get; set; }  

    }
}
