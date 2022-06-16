using System.Threading.Tasks;
using Api.Dtos.Orders;
using Api.Dtos.Products;

namespace Api.RabbitMqServiceClients.RabbitMq;

public interface IRabbitMqClientService
{
    Task<bool> SendOrder(OrderDto orderDto);
}