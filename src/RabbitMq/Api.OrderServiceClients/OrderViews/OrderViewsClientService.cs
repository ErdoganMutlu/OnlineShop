using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Dtos.Orders;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Api.OrderServiceClients.OrderViews;

public class OrderViewsClientService : IOrderViewsClientService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IOrderServiceClientConfigurations _configurations;

    public OrderViewsClientService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor,
        IOrderServiceClientConfigurations configurations)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
        _configurations = configurations;
    }
        
    public async Task<bool> AddRange(IList<OrderViewDto> orderViewDtos)
    {
        var url = $"{_configurations.Services_OrderApiBaseUrl}/api/orderviews";
        var data = new StringContent(JsonConvert.SerializeObject(orderViewDtos), Encoding.UTF8, "application/json");

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