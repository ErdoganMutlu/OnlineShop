using Api.Managers.Customers.Commands;
using Api.Managers.Customers.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Api.Managers;

public static class ApiManagersModuleInitializer
{
    public static IServiceCollection InitializeManagers<TSettings>(this IServiceCollection services, IConfiguration configuration)
        where TSettings : IApiManagersConfigurations
    {
        services.AddSingleton(typeof(IApiManagersConfigurations), typeof(TSettings));
        services.AddTransient<ICustomersQueryManager, CustomersQueryManager>();
        services.AddTransient<ICustomersCommandManager, CustomersCommandManager>();

        return services;
    }
}

public interface IApiManagersConfigurations
{

}