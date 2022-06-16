using Api.ObjectModels.Repositories.Orders;
using Api.ObjectModels.Repositories.OrderViews;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.ObjectModels;

public static class ApiObjectModelsModuleInitializer
{
    public static IServiceCollection InitializeObjectModels(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OrderDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("OrderDbContextConnStr")));

        //Public
        services.AddScoped<IOrdersRepository, OrdersRepository>();
        services.AddScoped<IOrderViewsRepository, OrderViewsRepository>();

        return services;
    }
}