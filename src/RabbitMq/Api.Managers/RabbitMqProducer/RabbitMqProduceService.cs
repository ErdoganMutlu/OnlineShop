using System;
using System.Text;
using System.Text.Json;
using Api.Managers.RabbitMqBase;
using RabbitMQ.Client;

namespace Api.Managers.RabbitMqProducer;

public class RabbitMqProduceService : IRabbitMqProduceService
{
    private readonly IConnection _connection;
    private readonly IApiManagersConfigurations _configurations;

    public RabbitMqProduceService(IApiManagersConfigurations configurations, IRabbitMqService rabbitMqService)
    {
        _connection = rabbitMqService.CreateChannel();
        _configurations = configurations;
    }

    public void SendMessage(object obj, string queue)
    {
        var message = JsonSerializer.Serialize(obj);
        SendMessage(message, queue);
    }

    public void SendMessage(string message, string queue)
    {
        using var channel = _connection.CreateModel();
        channel.QueueDeclare(queue: queue,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "",
            routingKey: queue,
            basicProperties: null,
            body: body);
    }
}