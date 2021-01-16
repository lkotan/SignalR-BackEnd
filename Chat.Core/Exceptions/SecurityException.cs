using System;

namespace Chat.Core.Exceptions
{
    public class SecurityException : Exception
    {
        public SecurityException(string message) : base(message)
        { }
    }
}
