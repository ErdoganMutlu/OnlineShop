using Api.ObjectModels.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.ObjectModels;

public static class ApiObjectModelsModuleInitializer
{
    public static IServiceCollection InitializeObjectModels(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProductDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("ProductDbContextConnStr")));

        //Public
        services.AddScoped<IProductsRepository, ProductsRepository>();

        return services;
    }
}