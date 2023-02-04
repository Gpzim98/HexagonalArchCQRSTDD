namespace PublicWebSite
{
    public enum ErrorCodes
    {
        // Customer related codes 1 to 99
        NOT_FOUND = 1,
        COULD_NOT_STORE_DATA = 2,
        INVALID_PERSON_ID = 3,
        MISSING_REQUIRED_INFORMATION = 4,
        INVALID_EMAIL = 5,
        CUSTOMER_NOT_FOUND = 6,
    }

    public abstract class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ErrorCodes ErrorCode { get; set; }
    }

    public class CustomerResponse : Response
    {
        public CustomerDTO Data;
    }
}
