using System;
using System.Runtime.Serialization;

namespace Flights_BL
{
    internal class NoFlightsException : Exception
    {
        public String UserName;
        public NoFlightsException(String userName) : base() => UserName = userName;

        public NoFlightsException(String userName, String? message) :
            base(message) => UserName = userName;

        public NoFlightsException(String userName, String? message, Exception? innerException) :
            base(message, innerException) => UserName = userName;

        public override string ToString() => base.ToString() + $", No flights found for {UserName}";
    }
}