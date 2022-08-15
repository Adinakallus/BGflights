using System;
using System.Runtime.Serialization;

namespace Flights_DAL
{
    internal class NoUserException : Exception
    {
        public String UserName;
        public NoUserException(String userName) : base() => UserName = userName;

        public NoUserException(String userName, String? message) : 
            base(message) => UserName = userName;

        public NoUserException(String userName, String? message, Exception? innerException) : 
            base(message, innerException) => UserName = userName;

        public override string ToString() => base.ToString() + $", user {UserName} not found";
    }
}