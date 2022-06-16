namespace Api.Managers.RabbitMqProducer;

public interface IRabbitMqProduceService
{
    void SendMessage(object obj, string queue);
    void SendMessage(string message, string queue);
}