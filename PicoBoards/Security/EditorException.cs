using System;

namespace PicoBoards.Security
{
    public sealed class EditorException : Exception
    {
        public EditorException() { }

        public EditorException(string message) : base(message) { }

        public EditorException(string message, Exception innerException) : base(message, innerException) { }
    }
}