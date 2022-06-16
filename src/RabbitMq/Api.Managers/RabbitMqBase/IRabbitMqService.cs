using RabbitMQ.Client;

namespace Api.Managers.RabbitMqBase;

public interface IRabbitMqService
{
    IConnection CreateChannel();
}