using System;
using System.Runtime.Serialization;

namespace Flights_BL
{
    internal class ZeroUsersException : Exception
    {
        public ZeroUsersException()
        {
        }

        public ZeroUsersException(string? message) : base(message)
        {
        }

        public ZeroUsersException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ZeroUsersException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override string ToString() => base.ToString() + $"Error getting usernames";

    }
}