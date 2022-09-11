using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Flights_GUI.Model;
using Flights_GUI.ViewModel;
using Flights_BE;
using System.Collections.ObjectModel;

namespace Flights_GUI.Commands
{
    public class FlightDetailsCmnd : ICommand
    {
        private FlightsVM flightsVM;

        public event Action<FlightInfoPartial> openwndFlightDetails;

        public FlightDetailsCmnd(FlightsVM _vm)
        {
            this.flightsVM = _vm;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (this.flightsVM.usesrIslogin)
            {
                this.flightsVM.flightsHistoryPerUser = new ObservableCollection<FlightInfo>(this.flightsVM.userAccountModel.GetUserHistory());
            }
            else
            {
                this.flightsVM.flightsHistoryPerUser = new ObservableCollection<FlightInfo>();
            }
            this.flightsVM.IsHistory = true;
            //set all the rest to not visible
            this.flightsVM.IsSignIn = false;
            this.flightsVM.isFlightDetailsUC = false;
            this.flightsVM.islogIn = false;
            this.flightsVM.IsHome = false;
        }
    }
}
