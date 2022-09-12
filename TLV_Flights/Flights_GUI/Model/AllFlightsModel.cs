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
    public class AllFlightsModel
    {
        private readonly BAL _bal;
        public AllFlightsModel()
        {
            _bal = new BAL();
        }

       /* public async Task<List<FlightInfoPartial>> GetFlightsAsync()
        {
            return await _bal.GetFlightsAsync();
        }
       */
        public List<FlightInfoPartial> GetFlights()
        {
            return _bal.GetFlights();
        }
        public ObservableCollection<FlightInfoPartial> UpdateFlights()
        {
            List<FlightInfoPartial> flightList =  GetFlights();

            ObservableCollection<FlightInfoPartial> flights = new();
            foreach (var flight in flightList)
            {
                flights.Add(flight);
            }
            return flights;
        }
       /* public async Task<ObservableCollection<FlightInfoPartial>> UpdateFlightsAsync()
        {
            List<FlightInfoPartial> flightList = await GetFlightsAsync();
            ObservableCollection<FlightInfoPartial> flights = new();
            foreach (var flight in flightList)
            {
                flights.Add(flight);
            }
            return  flights;
        }*/

    }
}
