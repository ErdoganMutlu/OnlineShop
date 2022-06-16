namespace Api.Configuration.AppSettings;

public interface IApiConfigurations
{
    string Api_Name { get; }
        
    string Api_Version { get; }
        
    string Api_BaseUrl { get; }
}