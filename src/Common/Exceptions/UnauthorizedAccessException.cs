using System;

namespace Common.Exceptions
{
    public class UnauthorizedAccessException : Exception
    {
        public UnauthorizedAccessException()
            : base("You do not have permission to access this resource.")
        {
        }

        public UnauthorizedAccessException(string message)
            : base(message)
        {
        }

        public UnauthorizedAccessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
