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
        
        DateTime from;
        DateTime to;

        public ObservableCollection<FlightInfo> flightsHistoryPerUser { get; set; }
        public FlightHistoryModel()
        {
            this.flightsHistoryPerUser = new ObservableCollection<FlightInfo>();
            this.from = DateTime.Today;
            this.to = DateTime.Today;

        }
        public void setDates(DateTime from, DateTime to)
        {
            ObservableCollection<FlightInfo> newList = new ObservableCollection<FlightInfo>();
            foreach (var flight in this.flightsHistoryPerUser)
            {
                //Dosn't work
               /* if (flight.time.scheduled.arrival >= from && flight.time.scheduled.arrival <= to)
                {
                    newList.Add(flight);
                }*/
            }
            this.flightsHistoryPerUser = newList;
        }

    }
}
