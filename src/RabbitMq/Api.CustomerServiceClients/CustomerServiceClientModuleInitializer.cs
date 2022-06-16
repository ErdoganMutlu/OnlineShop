using Api.CustomerServiceClients.Customers;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CustomerServiceClients;

public static class CustomerServiceClientModuleInitializer
{
    public static IServiceCollection InitializeCustomerServiceClients<TSettings>(this IServiceCollection services) where TSettings : ICustomerServiceClientConfigurations
    {
        services.AddSingleton(typeof(ICustomerServiceClientConfigurations), typeof(TSettings));
        services.AddTransient<ICustomersClientService, CustomersClientService>();
        return services;
    }
}
    
public interface ICustomerServiceClientConfigurations
{
    string Services_CustomerApiBaseUrl { get; }
}