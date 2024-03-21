using Domain;

namespace PublicWebSite
{
    public interface IProvideCustomerName
    { 
        Task<string> GetCustomerNameAsync();
    }

    public interface ICreateCustomer
    {
        Task<Domain.Entities.Customer> CreateCustomerAsync(CustomerDTO customer);
    }

    public interface IProvideCustomerData
    {
        Task<Domain.Entities.Customer> GetCustomerData(int customerId);
    }

    public interface IAuthProvider
    {
        public UserResponse GenerateJwtToken(Login login);
    }
}
