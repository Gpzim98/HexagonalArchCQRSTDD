using PublicWebSite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class CustomerCreator : ICreateCustomer
    {
        private readonly CustomerDbContext _dbContext;
        public CustomerCreator(CustomerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Domain.Entities.Customer>CreateCustomerAsync(
            CustomerDTO customer)
        {
            var domainObject = CustomerDTO.MapToDomain(customer);
            _dbContext.Customers.Add(domainObject);
            await _dbContext.SaveChangesAsync();
            return domainObject;
        }
    }
}
