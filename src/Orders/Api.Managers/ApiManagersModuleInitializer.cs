using Api.Managers.Orders;
using Api.Managers.Orders.Commands;
using Api.Managers.Orders.Queries;
using Api.Managers.OrderViews.Commands;
using Api.Managers.OrderViews.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Api.Managers;

public static class ApiManagersModuleInitializer
{
    public static IServiceCollection InitializeManagers<TSettings>(this IServiceCollection services, IConfiguration configuration)
        where TSettings : IApiManagersConfigurations
    {
        services.AddSingleton(typeof(IApiManagersConfigurations), typeof(TSettings));
        services.AddTransient<IOrdersQueryManager, OrdersQueryManager>();
        services.AddTransient<IOrdersCommandManager, OrdersCommandManager>();
        services.AddTransient<IOrderViewsCommandManager, OrderViewsCommandManager>();
        services.AddTransient<IOrderViewsQueryManager, OrderViewsQueryManager>();
        
        return services;
    }
}

public interface IApiManagersConfigurations
{

}