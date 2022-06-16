using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Dtos.Orders;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Api.RabbitMqServiceClients.RabbitMq;

public class RabbitMqClientService : IRabbitMqClientService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRabbitMqServiceClientConfigurations _configurations;

    public RabbitMqClientService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor,
        IRabbitMqServiceClientConfigurations configurations)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
        _configurations = configurations;
    }

    public async Task<bool> SendOrder(OrderDto orderDto)
    {
        var url = $"{_configurations.Services_RabbitMqApiBaseUrl}/api/rabbitmq/orders";
        var data = new StringContent(JsonConvert.SerializeObject(orderDto), Encoding.UTF8, "application/json");

        using var response = await _httpClient.PostAsync(new Uri(url), data);
        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        else
        {
            if (response.RequestMessage != null)
                throw new Exception(response.RequestMessage.ToString());
            else
                throw new Exception("response.RequestMessage is null");
        }
    }
}