using System;
using RabbitMQ.Client;

namespace Api.Managers.RabbitMqBase;

public class RabbitMqService : IRabbitMqService
{
    private readonly IApiManagersConfigurations _configurations;
    
    public RabbitMqService(IApiManagersConfigurations configurations)
    {
        _configurations = configurations;
    }
    
    public IConnection CreateChannel()
    {
        var connection = new ConnectionFactory
        {
            UserName = _configurations.RabbitMq_Username,
            Password = _configurations.RabbitMq_Password,
            HostName = _configurations.RabbitMq_Host,
            Port = _configurations.RabbitMq_Port,
            DispatchConsumersAsync = true
        };
        var channel = connection.CreateConnection();
        return channel;
    }
}