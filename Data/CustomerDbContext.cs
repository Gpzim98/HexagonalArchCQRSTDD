using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class CustomerDbContext : DbContext  
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options) { }

        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        }
    }
}