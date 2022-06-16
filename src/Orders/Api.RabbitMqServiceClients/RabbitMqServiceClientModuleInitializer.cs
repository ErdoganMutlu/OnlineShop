using Api.RabbitMqServiceClients.RabbitMq;
using Microsoft.Extensions.DependencyInjection;

namespace Api.RabbitMqServiceClients;

public static class RabbitMqServiceClientModuleInitializer
{
    public static IServiceCollection InitializeRabbitMqServiceClients<TSettings>(this IServiceCollection services) where TSettings : IRabbitMqServiceClientConfigurations
    {
        services.AddSingleton(typeof(IRabbitMqServiceClientConfigurations), typeof(TSettings));
        services.AddTransient<IRabbitMqClientService, RabbitMqClientService>();


        return services;
    }
}
    
public interface IRabbitMqServiceClientConfigurations
{
    string Services_RabbitMqApiBaseUrl { get; }
}