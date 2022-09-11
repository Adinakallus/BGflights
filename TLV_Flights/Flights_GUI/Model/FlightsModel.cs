using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flights_BL;
using Flights_BE;

namespace Flights_GUI.Model
{
    public class FlightsModel
    {
        Flights_BL.BAL bl;

        #region allFlighs
        public List<FlightInfoPartial> IncomingFlights { get { return bl.GetCurrentFlights()["Incoming"]; } }
        public List<FlightInfoPartial> OutgoingFlights { get { return bl.GetCurrentFlights()["Outgoing"]; } }
        #endregion

        #region singleFlight
        public FlightInfo singleFlightInfo { get; set; }

        #endregion

        #region propertiesPerFlight
        public string sourseFlight { get { return this.singleFlightInfo.airport.origin.name; } }
        public string destinationFlight { get { return this.singleFlightInfo.airport.destination.name; } }

        #endregion

        #region propertiesPerHoliday
        public string getholiday
        {
            get
            {
                return this.bl.getHoliday();
            }
        }
        #endregion

        #region private
        private string code;
        #endregion

        #region constractor
        public FlightsModel()
        {
            BLFactory factory = new BLFactory();
            bl = factory.getInstacne(); //get from factory


        }
        #endregion

        #region setDataToModel
        public void setCodeFlight(string _code)
        {
            this.code = _code;
            this.singleFlightInfo = bl.GetSingleFlight(code);
        }
        #endregion

    }
}
