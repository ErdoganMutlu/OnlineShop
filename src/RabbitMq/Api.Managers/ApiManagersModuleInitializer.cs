using Api.Managers.OrderViews;
using Api.Managers.RabbitMqBase;
using Api.Managers.RabbitMqConsumer;
using Api.Managers.RabbitMqProducer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Api.Managers;

public static class ApiManagersModuleInitializer
{
    public static IServiceCollection InitializeManagers<TSettings>(this IServiceCollection services, IConfiguration configuration)
        where TSettings : IApiManagersConfigurations
    {
        services.AddSingleton(typeof(IApiManagersConfigurations), typeof(TSettings));
        services.AddTransient<IRabbitMqService, RabbitMqService>();
        services.AddTransient<IRabbitMqProduceService, RabbitMqProduceService>();
        services.AddTransient<IRabbitMqConsumeService, RabbitMqConsumeService>();
        services.AddTransient<IOrderViewsRabbitMqManager, OrderViewsRabbitMqManager>();
        services.AddHostedService<WorkerServiceBus>();
        
        return services;
    }
}

public interface IApiManagersConfigurations
{
    string RabbitMq_Username { get; }
        
    string RabbitMq_Password { get; }
        
    string RabbitMq_Host { get; }
    
    int RabbitMq_Port { get; }
}