using Domain;

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

        // 100 - 119 Errors related to Authentication and Authorization
        FAILED_TO_AUTHENTICATE_USER = 100,

        //120 - 150
        USER_DOES_NOT_HAVE_PERMISSION_TO_QUERY_RECORD = 120,
        UNKNOWN = 999
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

    public class UserResponse : Response
    {
        public UserDTO Data;
        public string Token;
    }
}
