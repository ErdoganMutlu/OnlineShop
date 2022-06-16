using System.Linq;
using System.Threading.Tasks;
using Api.CustomerServiceClients.Customers;
using Api.Dtos.Orders;
using Api.OrderServiceClients.OrderViews;
using Api.ProductServiceClients.Products;
using Newtonsoft.Json;

namespace Api.Managers.OrderViews;

public class OrderViewsRabbitMqManager : IOrderViewsRabbitMqManager
{
    private readonly IApiManagersConfigurations _configurations;
    private readonly IProductsClientService _productsClientService;
    private readonly ICustomersClientService _customersClientService;
    private readonly IOrderViewsClientService _orderViewsClientService;

    public OrderViewsRabbitMqManager(IOrderViewsClientService orderViewsClientService,
        IProductsClientService productsClientService,
        ICustomersClientService customersClientService, IApiManagersConfigurations configurations)
    {
        _configurations = configurations;
        _productsClientService = productsClientService;
        _customersClientService = customersClientService;
        _orderViewsClientService = orderViewsClientService;
    }

    public async Task<bool> OrderReceivedAsync(string content)
    {
        var orderDto = JsonConvert.DeserializeObject<OrderDto>(content);

        var customerDto = await _customersClientService.GetCustomerByIdAsync(orderDto.CustomerId);
        var productsDto =
            await _productsClientService.GetProductsByIdAsync(orderDto.OrderProducts.Select(a => a.ProductId).ToList());

        var orderViews = productsDto.Select(p =>
            new OrderViewDto()
            {
                OrderId = orderDto.Id,
                OrderDateTime = orderDto.OrderDateTime,
                CustomerId = customerDto.Id,
                CustomerName = customerDto.Name,
                CustomerSurname = customerDto.Surname,
                ProductId = p.Id,
                ProductName = p.Name,
                ProductPrice = p.Price,
                ProductDetails = p.Details
            }).ToList();

        var result = await _orderViewsClientService.AddRange(orderViews);

        return result;
    }
}