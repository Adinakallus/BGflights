using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flights_BE;
using Flights_BL;

namespace Flights_GUI.Model
{
    public class FlightHistoryModel
    {
        private readonly BAL _bal;

        DateTime from;
        DateTime to;
        
        public ObservableCollection<FlightInfo> UserFlightsHistory { get; set; }
        
        public FlightHistoryModel()
        {
            _bal = new BAL();
            this.UserFlightsHistory = new ObservableCollection<FlightInfo>();
            this.from = DateTime.Today;
            this.to = DateTime.Today;
        }


        public Dictionary<DateTime, FlightInfoPartial> GetFlightsHistory(String userName)
        {
            try
            {
               return _bal.GetFlightsHistory(userName);
            }
            catch(Exception)
            {
                throw new Exception("Flights history is empty");
            }
        }

        public void SetDates(DateTime from, DateTime to)
        {
            ObservableCollection<FlightInfo> newList = new ObservableCollection<FlightInfo>();
            foreach (var flight in this.UserFlightsHistory)
            {
                if (flight.time.scheduled.t_arrival.Date >= from && flight.time.scheduled.t_arrival.Date <= to)
                 {
                     newList.Add(flight);
                 }
            }
            this.UserFlightsHistory = newList;
        }

    }
}
