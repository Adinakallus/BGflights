using Flights_BE;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flights_GUI.Model;
using Flights_GUI.Commands;

namespace Flights_GUI.ViewModel
{
    public class FlightsHistoryVM : ViewModelBase
    {
        private MainWindow mainWindow { get; set; }

        public Model.FlightHistoryModel historyModel;

        public ObservableCollection<FlightInfo> UserFlightsHistory
        {
            get
            {
                return this.historyModel.UserFlightsHistory;
            }
            set
            {
                this.historyModel.UserFlightsHistory = value;
                OnPropertyChanged();
            }
        }
        public void showHistory()
        {
            DateTime from = DateTime.Today;
            DateTime to = DateTime.Today;
            try
            {
                from = this.mainWindow.FlightsHistory.From.SelectedDate.Value;
                to = this.mainWindow.FlightsHistory.To.SelectedDate.Value;
            }
            catch
            {

            }
            //update dates on history model
            this.historyModel.setDates(from, to);
            this.UserFlightsHistory = this.historyModel.UserFlightsHistory;
        }

        //commands
        public FlightsHistoryCmnd flightsHistoryCmnd { get; set; }

        //set command
        userFlightsHistory = new UserFlightsHistory(this);

    }
}
