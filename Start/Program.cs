using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PublicWebSite;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

IConfigurationRoot configuration = builder.Build();

var connectionString = configuration.GetValue<string>("ConnectionString");

var apiConfiguration = new APIConfiguration(args, options =>
{
    options.AddDbContext<CustomerDbContext>(
       options => options.UseSqlServer(connectionString));

    options.AddScoped<ICreateCustomer, CustomerCreator>();

    options.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateCustomerCommandHandler>());
});

await apiConfiguration.RunAsync();