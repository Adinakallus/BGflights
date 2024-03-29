﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights_BE
{
    public class FlightInfoPartial
    {
        //general information about the flights
        //Save this information to the DB
        public int Id { get; set; }
        public string SourceId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime DateAndTime { get; set; }
        public String Source { get; set; }
        public String Destination { get; set; }
        public String FlightCode { get; set; }
    }
}
