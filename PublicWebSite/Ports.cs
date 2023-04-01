namespace PublicWebSite
{
    public interface IProvideCustomerName
    { 
        Task<string> GetCustomerNameAsync();
    }

    public interface ICreateCustomer
    {
        Task<int> CreateCustomerAsync(CustomerDTO customer);
    }
}
