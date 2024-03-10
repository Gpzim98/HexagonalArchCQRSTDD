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
}
