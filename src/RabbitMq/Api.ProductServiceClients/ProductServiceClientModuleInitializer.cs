using Api.ProductServiceClients.Products;
using Microsoft.Extensions.DependencyInjection;

namespace Api.ProductServiceClients;

public static class ProductServiceClientModuleInitializer
{
    public static IServiceCollection InitializeProductServiceClients<TSettings>(this IServiceCollection services) where TSettings : IProductServiceClientConfigurations
    {
        services.AddSingleton(typeof(IProductServiceClientConfigurations), typeof(TSettings));
        services.AddTransient<IProductsClientService, ProductsClientService>();


        return services;
    }
}
    
public interface IProductServiceClientConfigurations
{
    string Services_ProductApiBaseUrl { get; }
}