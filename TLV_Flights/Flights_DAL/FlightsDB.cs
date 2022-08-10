using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flights_BE;
using System.Data.Entity;

namespace Flights_DAL
{
    public class FlightsDB : DbContext
    {
        public FlightsDB() : base()
        {

        }

        public DbSet<User> UsersAndPasswords { get; set; }
        public DbSet<FlightInfoPartial> Planets { get; set; }
    }
}
