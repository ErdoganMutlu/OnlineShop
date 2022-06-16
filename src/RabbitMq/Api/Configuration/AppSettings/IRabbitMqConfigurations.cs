namespace Api.Configuration.AppSettings;

public interface IRabbitMqConfigurations
{
    string RabbitMq_Username { get; }
        
    string RabbitMq_Password { get; }
        
    string RabbitMq_Host { get; }
    
    int RabbitMq_Port { get; }
}