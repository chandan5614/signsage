using System;

namespace Common.Exceptions
{
    public class DocumentNotFoundException : Exception
    {
        public DocumentNotFoundException()
            : base("The requested document was not found.")
        {
        }

        public DocumentNotFoundException(string message)
            : base(message)
        {
        }

        public DocumentNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
