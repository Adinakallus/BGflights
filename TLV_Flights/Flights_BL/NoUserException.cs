using System;
using System.Runtime.Serialization;

namespace Flights_BL
{
    public class NoUserException : Exception
    {
        public String UserName;

        public NoUserException(String username) : base() => UserName = username;


        public NoUserException(String username, string? message) : base(message) => UserName = username;

        public NoUserException(String username,String? message, Exception? innerException) : base(message, innerException) => UserName = username;

        public override string ToString() => base.ToString() + $"Username:{UserName} does not exist";

    }
}