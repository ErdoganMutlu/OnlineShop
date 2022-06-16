using Api.Managers.Products;
using Api.Managers.Products.Commands;
using Api.Managers.Products.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Api.Managers;

public static class ApiManagersModuleInitializer
{
    public static IServiceCollection InitializeManagers<TSettings>(this IServiceCollection services, IConfiguration configuration)
        where TSettings : IApiManagersConfigurations
    {
        services.AddSingleton(typeof(IApiManagersConfigurations), typeof(TSettings));
        services.AddTransient<IProductsQueryManager, ProductsQueryManager>();
        services.AddTransient<IProductsCommandManager, ProductsCommandManager>();

        return services;
    }
}

public interface IApiManagersConfigurations
{

}