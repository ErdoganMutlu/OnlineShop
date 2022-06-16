#nullable enable
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Api.Managers.OrderViews;
using Api.Managers.RabbitMqBase;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Api.Managers.RabbitMqConsumer
{
    public class RabbitMqConsumeService : IRabbitMqConsumeService
    {
        private readonly IModel _channel;
        private readonly IConnection _connection;
        private readonly IOrderViewsRabbitMqManager _orderViewsRabbitMqManager;
        
        public RabbitMqConsumeService(IRabbitMqService rabbitMqService, IOrderViewsRabbitMqManager orderViewsRabbitMqManager)
        {
            _orderViewsRabbitMqManager = orderViewsRabbitMqManager;
            _connection = rabbitMqService.CreateChannel();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "Orders", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public Task RegisterOnMessageHandlerAndReceiveMessages(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new AsyncEventingBasicConsumer(_channel);
            
            consumer.Received += MessageReceivedAsync;

            _channel.BasicConsume("Orders", false, consumer);
            
            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(object sender, BasicDeliverEventArgs e)
        {
            var content = Encoding.UTF8.GetString(e.Body.ToArray());
            
            var result =  await _orderViewsRabbitMqManager.OrderReceivedAsync(content);
            
            _channel.BasicAck(e.DeliveryTag, false);
        }
        
        public void DisposeAsync()
        {
            _channel.Close();
            _connection.Close();
        }

        public Task CloseQueueAsync()
        {
            DisposeAsync();
            return Task.CompletedTask;
        }
    }
}