using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flights_BL;
using Flights_BE;

namespace Flights_GUI.Model
{
    public class TimeModel
    {
        BAL bal;
        string code;
        public Flights_BE.FlightInfo currentFlight
        {
            get
            {
                return this.bal.GetSingleFlight(this.code);
            }
        }
        public TimeModel()
        {
            BLFactory factory = new BLFactory();
            bal = factory.getInstacne(); //get from factory
        }
        public void setCode(string code)
        {
            this.code = code;
        }
    }
}
