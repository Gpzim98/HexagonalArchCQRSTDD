namespace PublicWebSite
{
    public interface IProvideCustomerName
    { 
        Task<string> GetCustomerNameAsync();
    }
}
