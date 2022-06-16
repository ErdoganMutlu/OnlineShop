using Api.Managers;
using Microsoft.Extensions.Configuration;


namespace Api.Configuration.AppSettings;

public class AppSettings : IApiConfigurations, IApiManagersConfigurations
{
    private IConfiguration Configuration { get; }

    public AppSettings(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public string Api_Name => Configuration.GetValue<string>("Api:Name");
    public string Api_Version => Configuration.GetValue<string>("Api:Version");
    public string Api_BaseUrl => Configuration.GetValue<string>("Api:BaseUrl");
}