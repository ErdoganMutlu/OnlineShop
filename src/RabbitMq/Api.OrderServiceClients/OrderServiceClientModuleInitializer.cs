using Api.OrderServiceClients.OrderViews;
using Microsoft.Extensions.DependencyInjection;

namespace Api.OrderServiceClients;

public static class CustomerServiceClientModuleInitializer
{
    public static IServiceCollection InitializeOrderServiceClients<TSettings>(this IServiceCollection services) where TSettings : IOrderServiceClientConfigurations
    {
        services.AddSingleton(typeof(IOrderServiceClientConfigurations), typeof(TSettings));
        services.AddTransient<IOrderViewsClientService, OrderViewsClientService>();
        return services;
    }
}
    
public interface IOrderServiceClientConfigurations
{
    string Services_OrderApiBaseUrl { get; }
}