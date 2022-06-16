namespace Api.Configuration.AppSettings;

public interface IServicesConfigurations
{
    string Services_ProductApiBaseUrl { get; }
        
    string Services_CustomerApiBaseUrl { get; }
    
    string Services_OrderApiBaseUrl { get; }
}