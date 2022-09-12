using Flights_BE;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flights_GUI.Model;
using Flights_GUI.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Flights_GUI.ViewModel
{
    public class FlightsHistoryVM : ViewModelBase
    {
        private MainWindow mainWindow { get; set; }
        public Model.FlightHistoryModel historyModel;

        #region Properties
        private FlightInfo _selectesFlight { get; set; }
        public FlightInfo SelectedFlight
        {
            get
            {
                return this._selectesFlight;
            }
            set
            {
                this._selectesFlight = value;
                OnPropertyChanged();
            }
        }

        private bool _Search;
        public bool Search
        {
            get
            {
                return this._Search;
            }
            set
            {
                this._Search = value;
                OnPropertyChanged();
            }
        }

        #endregion


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

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
        public void ShowHistory()
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
            this.historyModel.SetDates(from, to);
            this.UserFlightsHistory = this.historyModel.UserFlightsHistory;
        }

        //commands
        public FlightsHistoryCmnd flightsHistoryCmnd { get; set; }

        ////set command
        //flightsHistoryCmnd = new FlightsHistoryCmnd(this);

    }
}
