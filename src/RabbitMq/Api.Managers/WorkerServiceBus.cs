using System;
using System.Threading;
using System.Threading.Tasks;
using Api.Managers.RabbitMqConsumer;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Api.Managers;

internal sealed class WorkerServiceBus : IHostedService, IDisposable
{
    private readonly IRabbitMqConsumeService _rabbitMqConsumeService;
    
    public WorkerServiceBus(IRabbitMqConsumeService rabbitMqConsumeService, ILogger<WorkerServiceBus> logger)
    {
        _rabbitMqConsumeService = rabbitMqConsumeService;
    }
    
    public async Task StartAsync(CancellationToken stoppingToken)
    {
        await _rabbitMqConsumeService.RegisterOnMessageHandlerAndReceiveMessages(stoppingToken).ConfigureAwait(false);
    }
  
    public async Task StopAsync(CancellationToken stoppingToken)
    {
        await _rabbitMqConsumeService.CloseQueueAsync().ConfigureAwait(false);
    }

    public void Dispose()
    {
        Dispose(true);
        // ReSharper disable once GCSuppressFinalizeForTypeWithoutDestructor
        GC.SuppressFinalize(obj: this);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            _rabbitMqConsumeService.DisposeAsync();
        }
    }
}