using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Dtos.Products;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Api.ProductServiceClients.Products;

public class ProductsClientService : IProductsClientService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IProductServiceClientConfigurations _configurations;

    public ProductsClientService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor,
        IProductServiceClientConfigurations configurations)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
        _configurations = configurations;
    }

    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        var url = $"{_configurations.Services_ProductApiBaseUrl}/api/products/{id}";
        using var response = await _httpClient.GetAsync(new Uri(url));
        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<ProductDto>(responseData);
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

    public async Task<IList<ProductDto>> GetProductsByIdAsync(IList<int> productIds)
    {
        var urlParameters = new StringBuilder();
        foreach (var productId in productIds)
        {
            urlParameters.Append($"productIds={productId}&"); //TODO : could be a problem 	hundreds of thousands  
        }

        urlParameters.Remove(urlParameters.Length - 1, 1);

        var url = $"{_configurations.Services_ProductApiBaseUrl}/api/products/GetByIds?{urlParameters}";

        using var response = await _httpClient.GetAsync(new Uri(url));
        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<IList<ProductDto>>(responseData);
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