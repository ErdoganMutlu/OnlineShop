using Api.Managers;
using Api.RabbitMqServiceClients;
using Microsoft.Extensions.Configuration;


namespace Api.Configuration.AppSettings;

public class AppSettings : IApiConfigurations, IApiManagersConfigurations,  IRabbitMqServiceClientConfigurations
{
    private IConfiguration Configuration { get; }

    public AppSettings(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public string Api_Name => Configuration.GetValue<string>("Api:Name");
    public string Api_Version => Configuration.GetValue<string>("Api:Version");
    public string Api_BaseUrl => Configuration.GetValue<string>("Api:BaseUrl");
    
    public string Services_RabbitMqApiBaseUrl  => Configuration.GetValue<string>("Services:RabbitMqApiBaseUrl");
}