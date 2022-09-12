using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights_BE
{
    internal class HelperClass
    {
        public static DateTime EpochToDate(double epochTimeStamp)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            start = start.AddSeconds(epochTimeStamp).AddHours(3);
            return start;
        }
    }
}
