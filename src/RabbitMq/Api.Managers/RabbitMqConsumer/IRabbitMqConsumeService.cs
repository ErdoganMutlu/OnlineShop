using System.Threading;
using System.Threading.Tasks;

namespace Api.Managers.RabbitMqConsumer
{
    public interface IRabbitMqConsumeService
    {
        Task RegisterOnMessageHandlerAndReceiveMessages(CancellationToken stoppingToken);
        Task CloseQueueAsync();
        void DisposeAsync();
    }
}