using Api.CustomerServiceClients;
using Api.Managers;
using Api.OrderServiceClients;
using Api.ProductServiceClients;
using Microsoft.Extensions.Configuration;


namespace Api.Configuration.AppSettings;

public class AppSettings : IApiConfigurations, IApiManagersConfigurations, IRabbitMqConfigurations, IServicesConfigurations, IProductServiceClientConfigurations, IOrderServiceClientConfigurations, ICustomerServiceClientConfigurations
{
    private IConfiguration Configuration { get; }

    public AppSettings(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public string Api_Name => Configuration.GetValue<string>("Api:Name");
    public string Api_Version => Configuration.GetValue<string>("Api:Version");
    public string Api_BaseUrl => Configuration.GetValue<string>("Api:BaseUrl");
    
    public string RabbitMq_Username => Configuration.GetValue<string>("RabbitMq:Username");
    public string RabbitMq_Password => Configuration.GetValue<string>("RabbitMq:Password");
    public string RabbitMq_Host => Configuration.GetValue<string>("RabbitMq:Host");
    public int RabbitMq_Port => Configuration.GetValue<int>("RabbitMq:Port");

    public string Services_ProductApiBaseUrl => Configuration.GetValue<string>("Services:ProductApiBaseUrl");
    public string Services_CustomerApiBaseUrl => Configuration.GetValue<string>("Services:CustomerApiBaseUrl");
    public string Services_OrderApiBaseUrl => Configuration.GetValue<string>("Services:OrderApiBaseUrl");
}