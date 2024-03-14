using PublicWebSite;

namespace Auth
{
    public interface IUserProvider
    {
        Response AuthenticateUser(Login login);
    }
}
