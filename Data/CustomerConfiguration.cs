using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.OwnsOne(x => x.CustomerDocument)
                .Property(x => x.IdNumber);

            builder.OwnsOne(x => x.CustomerDocument)
                .Property(x => x.DocumentType);
        }
    }
}
