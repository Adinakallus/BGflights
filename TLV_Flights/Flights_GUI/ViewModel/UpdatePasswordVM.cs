using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Flights_GUI.Commands;
using Microsoft.Maps.MapControl.WPF;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Flights_GUI.UserControls;
using Flights_BE;

namespace Flights_GUI.ViewModel
{
    public class UpdatePasswordVM : ViewModelBase
    {
        #region properties
        private bool _IsloggedIn;
        public bool IsloggedIn
        {
            get
            {
                return this._IsloggedIn;
            }
            set
            {
                this._IsloggedIn = value;
                OnPropertyChanged(nameof(this._IsloggedIn));
            }
        }
        private bool _IsPassword;
        public bool IsPassword
        {
            get
            {
                return this._IsPassword;
            }
            set
            {
                this._IsPassword = value;
                OnPropertyChanged(nameof(this._IsPassword));
            }
        }
        private string _sendMessage;
        public string sendMessage
        {
            get
            {
                return this._sendMessage;
            }
            set
            {
                this._sendMessage = value;
                OnPropertyChanged(nameof(this._sendMessage));
            }
        }
        private bool _messageVisibility;

        public bool messageVisibility
        {
            get
            {
                return this._messageVisibility;
            }
            set
            {
                this._messageVisibility = value;
                OnPropertyChanged(nameof(this._messageVisibility));
            }
        }
        #endregion

        #region constructor
        public UpdatePasswordVM()
        {
            UpdatePswrdCommand = new UpdatePasswordCmnd(this);
        }
        

        //this.IsloggedIn = false;
        //this.IsPassword = false;
        //this.messageVisibility = false;

        #endregion

        #region openDetailsCommand
        private void OpenFlightDetailsCommand_openwndFlightDetails(FlightInfoPartial flight)
        {
            if (this.IsloggedIn)
            {
                this.IsPassword = true;
            }
            else
            {
                this.sendMessage = "You are not logged in, please log in first.";
                this.messageVisibility = true;
            }
        }

        #endregion
        public ICommand UpdatePswrdCommand { get; set; }
    }
}
