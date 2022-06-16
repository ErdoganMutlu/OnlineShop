using System;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Dtos.Customers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Api.CustomerServiceClients.Customers;

public class CustomersClientService : ICustomersClientService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICustomerServiceClientConfigurations _configurations;

    public CustomersClientService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor,
        ICustomerServiceClientConfigurations configurations)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
        _configurations = configurations;
    }
        
    public async Task<CustomerDto> GetCustomerByIdAsync(int id)
    {
        var url = $"{_configurations.Services_CustomerApiBaseUrl}/api/customers/{id}";
        using var response = await _httpClient.GetAsync(new Uri(url));
        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<CustomerDto>(responseData);
            return responseModel;
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