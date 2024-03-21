using Domain.Entities;
using PublicWebSite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class CustomerDataProvider : IProvideCustomerData
    {
        private readonly CustomerDbContext _dbContext;
        public CustomerDataProvider(CustomerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Customer> GetCustomerData(int customerId)
        {
            return await _dbContext.Customers.FindAsync(customerId);
        }
    }
}
