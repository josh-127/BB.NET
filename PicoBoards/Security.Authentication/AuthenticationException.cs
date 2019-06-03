using System;

namespace PicoBoards.Security.Authentication
{
    public sealed class AuthenticationException : Exception
    {
        public AuthenticationException() { }

        public AuthenticationException(string message) : base(message) { }

        public AuthenticationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}