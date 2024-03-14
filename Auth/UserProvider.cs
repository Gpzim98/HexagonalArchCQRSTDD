using Domain;
using Domain.Exceptions;
using PublicWebSite;

namespace Auth
{
    public enum ErroCodes
    {
        InvalidCredentials,
        UserIsNotActive,
        Unknown
    }
    public class Response
    {
        public bool Success { get; set; }   
        public User User { get; set; }
        public ErroCodes ErrorCode { get; set; }
        public string Message { get; set; }
    }
    public class UserProvider : IUserProvider
    {
        public Response AuthenticateUser(Login login)
        {

            try
            {
                var user = ValidateUserCredentials(login);
                return new Response
                {
                    Success = true,
                    User = user,
                };
            }
            catch(InvalidUserCredentials ex)
            {
                return new Response
                {
                    Success = false,
                    ErrorCode = ErroCodes.InvalidCredentials,
                    Message = "Credentials are invalid"
                };
            }
            catch(Exception)
            {
                return new Response
                {
                    Success = false,
                    ErrorCode = ErroCodes.Unknown,
                    Message = "There was a undetermined problem during your authentication"
                };
            }
        }

        public User ValidateUserCredentials(Login login)
        {
            if (login.Username != "user" || login.Password != "password")
            {
                throw new InvalidUserCredentials("Invalid Credentials");
            }

            var user = new User
            {
                Username = login.Username,
                Password = login.Password,
            };

            user.Permissions = new List<string> {
                "CreateCustomers",
                //"ReadCustomers",
                "UpdateCustomers",
                "DeleteCustomers"
            };

            user.Roles = new List<string> { "Admin", "User" };

            return user;
        }
    }
}
