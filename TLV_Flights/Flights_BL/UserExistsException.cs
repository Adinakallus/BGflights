using System;
using System.Runtime.Serialization;

namespace Flights_BL
{
    public class UserExistsException : Exception
    {
        public String UserName;

        public UserExistsException(String username) : base() => UserName = username;


        public UserExistsException(String username, string? message) : base(message) => UserName = username;

        public UserExistsException(String username,String? message, Exception? innerException) : base(message, innerException) => UserName = username;

        public override string ToString() => base.ToString() + $"Username:{UserName} already exists";

    }
}