namespace Domain.Exceptions
{
    public class InvalidCustomerDocumentException : Exception
    {
    }

    public class MissingRequiredInformationException : Exception
    {
    }

    public class InvalidUserCredentials : Exception
    {
        public InvalidUserCredentials(string? message) : base(message)
        {
        }
    }
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException(string message)
        : base(message) { }

    }
}
