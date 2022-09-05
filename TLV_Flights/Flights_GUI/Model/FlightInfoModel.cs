using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flights_BE;
using Flights_BL;

namespace Flights_GUI.Model
{
    public class FlightInfoModel
    {
        private readonly BAL _bal;
        public FlightInfoModel(BAL bal)
        {
            _bal = new ();
        }   

        public async Task<FlightInfo> GetFlightInfo(FlightInfoPartial flight)
        {
            return await _bal.GetFlightInfo(flight);
        }
    }
}
