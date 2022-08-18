using System;
using System.Runtime.Serialization;

namespace Flights_DAL
{
    
    public class JsonErrorException : Exception
    {
        public JsonErrorException()
        {
        }

        public JsonErrorException(string? message) : base(message)
        {
        }

        public JsonErrorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public override string ToString() => base.ToString() + $"Error recieving flights";

    }
}