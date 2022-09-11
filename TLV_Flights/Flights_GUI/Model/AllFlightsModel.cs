using System;
using System.Collections.Generic;
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

        public async Task<List<FlightInfoPartial>> GetFlights()
        {
            return await _bal.GetFlights();
        }
    }
}
