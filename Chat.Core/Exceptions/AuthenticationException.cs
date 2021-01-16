using System;

namespace Chat.Core.Exceptions
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException(string message) : base(message)
        { }
    }
}
