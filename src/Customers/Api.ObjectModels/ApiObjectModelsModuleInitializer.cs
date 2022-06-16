using Api.ObjectModels.Repositories.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.ObjectModels;

public static class ApiObjectModelsModuleInitializer
{
    public static IServiceCollection InitializeObjectModels(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CustomerDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("CustomerDbContextConnStr")));

        //Public
        services.AddScoped<ICustomersRepository, CustomersRepository>();

        return services;
    }
}