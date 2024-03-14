using Auth;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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
    options.AddScoped<IProvideCustomerData, CustomerDataProvider>();
    options.AddScoped<IAuthProvider, TokenGeneration>();
    options.AddScoped<IUserProvider, UserProvider>();

    options.AddMvc().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.MaxDepth = 64;
        options.JsonSerializerOptions.IncludeFields = true;
    });

    // Authentication:
    options.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration.GetValue<string>("Jwt:Issuer"),
            ValidAudience = configuration.GetValue<string>("Jwt:Audience"),
            IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:Key")))
        };
    });

    options.AddAuthorization();

    options.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateCustomerCommandHandler>());
});

await apiConfiguration.RunAsync();