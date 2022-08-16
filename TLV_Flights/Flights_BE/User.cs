using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights_BE
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Dictionary<DateTime,FlightInfoPartial> FlightsHistory { get; set; } //history of the flight the user looked for (by the date he searched them)
    }
}
