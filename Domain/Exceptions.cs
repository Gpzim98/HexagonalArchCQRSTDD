namespace Domain.Exceptions
{
    public class InvalidCustomerDocumentException : Exception
    {
    }

    public class MissingRequiredInformationException : Exception
    {
    }
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException(string message)
        : base(message) { }

    }
}
