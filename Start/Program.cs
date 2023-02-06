using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

IConfigurationRoot configuration = builder.Build();

var connectionString = configuration.GetValue<string>("ConnectionString");

var apiConfiguration = new APIConfiguration(args, options =>
{
    options.AddDbContext<CustomerDbContext>(
       options => options.UseSqlServer(connectionString));
});

await apiConfiguration.RunAsync();