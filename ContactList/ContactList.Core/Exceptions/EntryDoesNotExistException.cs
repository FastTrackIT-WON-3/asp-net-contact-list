using System;

namespace ContactList.Core.Exceptions
{
    public class EntryDoesNotExistException : Exception
    {
        public EntryDoesNotExistException()
            : base()
        {
        }

        public EntryDoesNotExistException(string message)
            : base(message)
        {
        }

        public EntryDoesNotExistException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
